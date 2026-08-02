Imports System.Drawing.Drawing2D

''' <summary>
'''     イコライザー画面（10バンド・グラフィック・イコライザー＋スペクトラム表示）
''' </summary>
Public Class EqualizerForm

#Region "定数"

    ''' <summary>バンドの数</summary>
    Private Const BandCount As Integer = 10

    ''' <summary>ゲインの範囲（dB）</summary>
    Private Const MinGain As Double = -12.0
    Private Const MaxGain As Double = 12.0

#End Region

#Region "フィールド"

    Private _trackBars() As TrackBar
    Private _gainLabels() As Label

    ''' <summary>各バンドの表示中レベル（スムージング用）</summary>
    Private _bandLevels(BandCount - 1) As Double

    ''' <summary>各バンドのピーク値（減衰アニメーション用）</summary>
    Private _bandPeaks(BandCount - 1) As Double

    ''' <summary>各バンドのランダム変動用</summary>
    Private _random As New Random()

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

        ' スペクトラム表示開始
        TimerSpectrum.Start()
    End Sub

    Private Sub EqualizerForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        TimerSpectrum.Stop()
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
        Dim player = GetPlayer()
        Dim isPlaying As Boolean = False

        If player IsNot Nothing AndAlso Not player.IsDisposed AndAlso Not player.IsIdle Then
            isPlaying = player.IsPlaying
        End If

        For i As Integer = 0 To BandCount - 1
            ' ゲインを 0～1 に正規化
            Dim gainNorm As Double = (_trackBars(i).Value + 120) / 240.0

            ' ランダム変動を追加（各バンドに異なる周波数のざらつき）
            Dim jitter As Double = (_random.NextDouble() - 0.5) * 0.3

            ' 再生中のみレベルを表示
            Dim targetLevel As Double = 0
            If isPlaying Then
                ' ゲインが高いほどレベルが上がり、ランダム変動で自然な揺らぎを再現
                targetLevel = 0.3 + gainNorm * 0.7 + jitter
            End If
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

        For i As Integer = 0 To BandCount - 1
            Dim x As Single = CSng(padding + i * (barWidth + padding))
            Dim barH As Single = CSng(_bandLevels(i) * (area.Height - 20))
            Dim peakH As Single = CSng(_bandPeaks(i) * (area.Height - 20))
            Dim y As Single = area.Height - barH - 16
            Dim peakY As Single = area.Height - peakH - 16

            ' ゲインに応じて色を変える
            Dim gainVal = _trackBars(i).Value / 10.0
            Dim barColor As Color = GetBarColor(gainVal, CSng(_bandLevels(i)))

            ' バー本体（グラデーション）
            If barH > 1 Then
                Using brush As New LinearGradientBrush(
                    New RectangleF(x, y, CSng(barWidth), barH),
                    ControlPaint.Dark(barColor, 0.3F), barColor, LinearGradientMode.Vertical)
                    g.FillRectangle(brush, x, y, CSng(barWidth), barH)
                End Using
            End If

            ' ピークインジケーター（細い水平線）
            If peakH > 2 Then
                Using pen As New Pen(Color.White, 2)
                    g.DrawLine(pen, x, peakY, x + CSng(barWidth), peakY)
                End Using
            End If
        Next

        ' ラベル描画（下部に周波数）
        Using font As New Font("MS UI Gothic", 7)
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
    '''     ゲインとレベルに応じたバーの色を取得する
    ''' </summary>
    Private Shared Function GetBarColor(gainDb As Double, level As Single) As Color
        ' ゲインが正の場合は緑→黄→赤、負の場合は青→シアン
        If gainDb >= 6 Then
            Return Color.FromArgb(200, 50 + CInt(level * 100), 30, 30)
        ElseIf gainDb >= 0 Then
            Return Color.FromArgb(200, 30 + CInt(level * 120), 80 + CInt(level * 80), 30)
        ElseIf gainDb >= -6 Then
            Return Color.FromArgb(200, 30, 80 + CInt(level * 80), 30 + CInt(level * 120))
        Else
            Return Color.FromArgb(200, 30, 30 + CInt(level * 100), 80 + CInt(level * 80))
        End If
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
