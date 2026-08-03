Imports System.Drawing.Drawing2D

''' <summary>
'''     イコライザー画面（10バンド・グラフィック・イコライザー＋スペクトラム表示）
''' </summary>
Public Class EqualizerForm

#Region "定数"

    ''' <summary>バンドの数</summary>
    Private Const BandCount As Integer = 10

    ''' <summary>ゲインの範囲（dB）</summary>
    Private Const MinGain As Double = -30.0
    Private Const MaxGain As Double = 30.0

    ''' <summary>LEDメーター1バンドあたりのセグメント数</summary>
    Private Const SegmentCount As Integer = 16

    ''' <summary>LEDセグメント間の隙間（ピクセル）</summary>
    Private Const SegmentGapPx As Integer = 2

    ''' <summary>
    '''     無音とみなす音量下限（dB）。WASAPIループバックで測定される実際の音量は聴感より
    '''     かなり小さい値になる（スピーカー側の増幅より手前を測定するため）ので、広めに取っている。
    ''' </summary>
    Private Const RmsFloorDb As Double = -90.0

    ''' <summary>
    '''     メーターが満杯（1.0）になる音量(dB)。WASAPIループバックの実測値は聴感よりかなり小さいため、
    '''     0dBではなくこの値をそのまま満杯とみなす。値を下げるほどメーターが敏感になる。
    ''' </summary>
    Private Const CeilingDb As Double = -50.0

    ''' <summary>各バンドの中心周波数(Hz)。AudioSpectrumAnalyzerへ渡す解析対象帯域。</summary>
    Private Shared ReadOnly BandFrequenciesHz() As Integer = {31, 62, 125, 250, 500, 1000, 2000, 4000, 8000, 16000}

#End Region

#Region "フィールド"

    Private _trackBars() As TrackBar
    Private _gainLabels() As Label

    ''' <summary>各バンドの表示中レベル（スムージング用）</summary>
    Private _bandLevels(BandCount - 1) As Double

    ''' <summary>各バンドのピーク値（減衰アニメーション用）</summary>
    Private _bandPeaks(BandCount - 1) As Double

    ''' <summary>WASAPIループバック録音+FFTで実際の音声の帯域別音量を解析するアナライザー</summary>
    Private _spectrumAnalyzer As AudioSpectrumAnalyzer

#End Region

#Region "フォームイベント"

    Private Sub EqualizerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' コントロール配列を構築
        _trackBars = {
            TrackBar31, TrackBar62, TrackBar125, TrackBar250, TrackBar500,
            TrackBar1k, TrackBar2k, TrackBar4k, TrackBar8k, TrackBar16k
        }
        _gainLabels = {
            LabelGain31, LabelGain62, LabelGain125, LabelGain250, LabelGain500,
            LabelGain1k, LabelGain2k, LabelGain4k, LabelGain8k, LabelGain16k
        }

        ' パネルのダブルバッファリング有効化
        SetDoubleBuffered(PanelSpectrum)

        ' メインフォームの右側に配置
        PositionRelativeToMainForm()

        ' WASAPIループバック録音を開始し、スペクトラム表示を開始
        _spectrumAnalyzer = New AudioSpectrumAnalyzer(BandFrequenciesHz)
        TimerSpectrum.Start()
    End Sub

    Private Sub EqualizerForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        TimerSpectrum.Stop()
        If _spectrumAnalyzer IsNot Nothing Then
            _spectrumAnalyzer.Dispose()
            _spectrumAnalyzer = Nothing
        End If
    End Sub

    ''' <summary>
    '''     メインフォームの右側に配置
    ''' </summary>
    Private Sub PositionRelativeToMainForm()
        If MainPlayerForm.Instance IsNot Nothing Then
            Dim mainForm = MainPlayerForm.Instance
            Left = mainForm.Right + 10
            Top = mainForm.Top

            If Right > Screen.PrimaryScreen.WorkingArea.Right Then
                Left = Math.Max(10, mainForm.Left - Width - 10)
            End If

            If Bottom > Screen.PrimaryScreen.WorkingArea.Bottom Then
                Top = Math.Max(10, Screen.PrimaryScreen.WorkingArea.Bottom - Height - 10)
            End If
        End If
    End Sub

#End Region

#Region "トラックバーイベント"

    Private Sub TrackBar_ValueChanged(sender As Object, e As EventArgs) _
        Handles TrackBar31.ValueChanged, TrackBar62.ValueChanged, TrackBar125.ValueChanged,
                TrackBar250.ValueChanged, TrackBar500.ValueChanged, TrackBar1k.ValueChanged,
                TrackBar2k.ValueChanged, TrackBar4k.ValueChanged, TrackBar8k.ValueChanged,
                TrackBar16k.ValueChanged

        For i As Integer = 0 To BandCount - 1
            Dim gainDb As Double = _trackBars(i).Value / 10.0
            _gainLabels(i).Text = gainDb.ToString("+0;-0;0")
        Next

        ApplyEqualizer()
    End Sub

#End Region

#Region "ボタンイベント"

    Private Sub ButtonReset_Click(sender As Object, e As EventArgs) Handles ButtonReset.Click
        For Each tb In _trackBars
            tb.Value = 0
        Next
    End Sub

    Private _noiseCancelEnabled As Boolean = False

    Private Sub ButtonNC_Click(sender As Object, e As EventArgs) Handles ButtonNC.Click
        Dim player = GetPlayer()
        If player Is Nothing OrElse player.IsDisposed Then Return

        _noiseCancelEnabled = Not _noiseCancelEnabled
        If _noiseCancelEnabled Then
            player.SetNoiseCancel()
            ButtonNC.BackColor = Color.FromArgb(80, 180, 80)
        Else
            player.ClearNoiseCancel()
            ButtonNC.BackColor = SystemColors.Control
        End If
    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Close()
    End Sub

    Private Sub ChannelRoutingChanged(sender As Object, e As EventArgs) _
        Handles CheckBox1.CheckedChanged, CheckBox2.CheckedChanged,
                CheckBox3.CheckedChanged, CheckBox4.CheckedChanged
        Dim player = GetPlayer()
        If player Is Nothing OrElse player.IsDisposed Then Return
        player.SetChannelRouting(CheckBox1.Checked, CheckBox2.Checked,
                                 CheckBox3.Checked, CheckBox4.Checked)
        ButtonMono.BackColor = SystemColors.Control
    End Sub

    Private _monoEnabled As Boolean = False

    Private Sub ButtonMono_Click(sender As Object, e As EventArgs) Handles ButtonMono.Click
        Dim player = GetPlayer()
        If player Is Nothing OrElse player.IsDisposed Then Return

        _monoEnabled = Not _monoEnabled
        If _monoEnabled Then
            CheckBox1.Checked = True
            CheckBox2.Checked = True
            CheckBox3.Checked = True
            CheckBox4.Checked = True
            ButtonMono.BackColor = Color.FromArgb(80, 180, 80)
        Else
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            CheckBox4.Checked = False
            ButtonMono.BackColor = SystemColors.Control
        End If
    End Sub

#End Region

#Region "スペクトラム表示"

    Private Sub TimerSpectrum_Tick(sender As Object, e As EventArgs) Handles TimerSpectrum.Tick
        UpdateSpectrumLevels()
        PanelSpectrum.Invalidate()
    End Sub

    ''' <summary>
    '''     各バンドの表示レベルを更新する
    ''' </summary>
    Private Sub UpdateSpectrumLevels()
        Dim bandsDb = _spectrumAnalyzer.GetBandLevelsDb()

        For i As Integer = 0 To BandCount - 1
            ' ゲインを 0～1 に正規化（帯域ごとの見た目の強調に使用）。TrackBarのMinimum/Maximumから算出する
            Dim trackBar = _trackBars(i)
            Dim gainNorm As Double = (trackBar.Value - trackBar.Minimum) / CDbl(trackBar.Maximum - trackBar.Minimum)

            ' 実測音量（RMS dB）を0～1に正規化（下限=0、CeilingDb=1）
            Dim normalized = Math.Max(0, Math.Min(1, (bandsDb(i) - RmsFloorDb) / (CeilingDb - RmsFloorDb)))
            Dim targetLevel As Double = normalized * (0.7 + gainNorm * 0.3)
            targetLevel = Math.Max(0, Math.Min(1, targetLevel))

            ' スムージング（EMA）
            _bandLevels(i) = _bandLevels(i) * 0.7 + targetLevel * 0.3

            ' ピーク値の減衰
            If _bandLevels(i) > _bandPeaks(i) Then
                _bandPeaks(i) = _bandLevels(i)
            Else
                _bandPeaks(i) = _bandPeaks(i) * 0.95
            End If
        Next
    End Sub

#End Region

#Region "描画"

    Private Sub PanelSpectrum_Paint(sender As Object, e As PaintEventArgs) Handles PanelSpectrum.Paint
        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        Dim area = PanelSpectrum.ClientRectangle
        If area.Width <= 0 OrElse area.Height <= 0 Then Return

        ' バンド間の余白
        Dim padding As Integer = 2
        Dim totalPadding = padding * (BandCount + 1)
        Dim barWidth = (area.Width - totalPadding) / BandCount

        ' LEDメーター用の縦幅（下部は周波数ラベル領域として確保）
        Dim topMargin As Integer = 4
        Dim labelAreaHeight As Integer = 16
        Dim meterHeight As Single = Math.Max(0, area.Height - topMargin - labelAreaHeight)
        Dim totalGap As Single = SegmentGapPx * (SegmentCount - 1)
        Dim segmentHeight As Single = Math.Max(1.0F, (meterHeight - totalGap) / SegmentCount)

        For i As Integer = 0 To BandCount - 1
            Dim x As Single = CSng(padding + i * (barWidth + padding))

            Dim litCount As Integer = CInt(Math.Round(_bandLevels(i) * SegmentCount))
            litCount = Math.Max(0, Math.Min(SegmentCount, litCount))

            ' ピークセグメント（現在のレベルより上にある場合のみ、白く浮かせて表示）
            Dim peakSegment As Integer = CInt(Math.Round(_bandPeaks(i) * SegmentCount)) - 1

            For j As Integer = 0 To SegmentCount - 1
                Dim segY As Single = topMargin + (SegmentCount - 1 - j) * (segmentHeight + SegmentGapPx)
                Dim segRect As New RectangleF(x, segY, CSng(barWidth), segmentHeight)

                Dim baseColor As Color = GetSegmentColor(j, SegmentCount)
                Dim isLit As Boolean = j < litCount
                Dim isPeakMarker As Boolean = j = peakSegment AndAlso peakSegment >= litCount

                Dim fillColor As Color
                If isPeakMarker Then
                    fillColor = Color.White
                ElseIf isLit Then
                    fillColor = baseColor
                Else
                    fillColor = DimColor(baseColor)
                End If

                Using brush As New SolidBrush(fillColor)
                    g.FillRectangle(brush, segRect)
                End Using

                ' 点灯セグメントのみ上部にツヤ（グロス）を乗せて立体感を出す
                If isLit OrElse isPeakMarker Then
                    Using glossBrush As New SolidBrush(Color.FromArgb(70, 255, 255, 255))
                        g.FillRectangle(glossBrush, segRect.X, segRect.Y,
                                        segRect.Width, Math.Max(1.0F, segRect.Height * 0.35F))
                    End Using
                End If
            Next
        Next

        ' ラベル描画（下部に周波数）
        Using font As New Font("メイリオ", 7)
            Using brush As New SolidBrush(Color.LightGray)
                For i As Integer = 0 To BandCount - 1
                    Dim x As Single = CSng(padding + i * (barWidth + padding))
                    Dim freqText = GetBandFrequencyText(i)
                    Dim sz = g.MeasureString(freqText, font)
                    g.DrawString(freqText, font, brush,
                                 x + (barWidth - sz.Width) / 2,
                                 area.Height - 14)
                Next
            End Using
        End Using
    End Sub

    ''' <summary>
    '''     セグメント位置（下から何番目か）に応じたLEDの基準色を取得する（クラシックなVUメーター配色）
    ''' </summary>
    Private Shared Function GetSegmentColor(segmentIndex As Integer, segmentCount As Integer) As Color
        Dim fraction As Double = segmentIndex / CDbl(segmentCount - 1)
        If fraction < 0.6 Then
            Return Color.FromArgb(255, 40, 200, 90)
        ElseIf fraction < 0.85 Then
            Return Color.FromArgb(255, 230, 200, 40)
        Else
            Return Color.FromArgb(255, 220, 60, 50)
        End If
    End Function

    ''' <summary>
    '''     消灯中のLEDセグメント色（同系色を暗くした状態）を取得する
    ''' </summary>
    Private Shared Function DimColor(baseColor As Color) As Color
        Const dimFactor As Double = 0.15
        Return Color.FromArgb(255, CInt(baseColor.R * dimFactor),
                               CInt(baseColor.G * dimFactor), CInt(baseColor.B * dimFactor))
    End Function

    ''' <summary>
    '''     バンド番号に対応する周波数テキストを取得する
    ''' </summary>
    Private Shared Function GetBandFrequencyText(index As Integer) As String
        Select Case index
            Case 0 : Return "31"
            Case 1 : Return "62"
            Case 2 : Return "125"
            Case 3 : Return "250"
            Case 4 : Return "500"
            Case 5 : Return "1k"
            Case 6 : Return "2k"
            Case 7 : Return "4k"
            Case 8 : Return "8k"
            Case 9 : Return "16k"
            Case Else : Return ""
        End Select
    End Function

#End Region

#Region "ヘルパー"

    ''' <summary>
    '''     メディアプレイヤーを取得する
    ''' </summary>
    Private Shared Function GetPlayer() As MpvPlayerWrapper
        If MainPlayerForm.Instance Is Nothing Then Return Nothing
        Return MainPlayerForm.Instance.GetMediaPlayer()
    End Function

    ''' <summary>
    '''     コントロールのダブルバッファリングを有効にする
    ''' </summary>
    Private Shared Sub SetDoubleBuffered(ctrl As Control)
        Dim prop = ctrl.GetType().GetProperty("DoubleBuffered",
                    Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
        If prop IsNot Nothing Then
            prop.SetValue(ctrl, True)
        End If
    End Sub

    ''' <summary>
    '''     現在のゲイン設定を mpv に適用する
    ''' </summary>
    Private Sub ApplyEqualizer()
        Dim player = GetPlayer()
        If player Is Nothing OrElse player.IsDisposed Then Return

        Dim bands(BandCount - 1) As Double
        For i As Integer = 0 To BandCount - 1
            bands(i) = _trackBars(i).Value / 10.0
        Next

        player.SetEqualizer(bands)
    End Sub

#End Region

End Class
