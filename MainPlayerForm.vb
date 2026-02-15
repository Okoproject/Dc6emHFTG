Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Diagnostics
Imports System.Collections.Generic
Imports Microsoft.Office.Interop

''' <summary>
''' メイン動画プレイヤーフォーム
''' </summary>
Public Class MainPlayerForm

#Region "定数"

    Private Const DANDAN_INTERVAL As Integer = 26
    Private ReadOnly TimeColumnOffsets As Integer() = {7, 33, 59, 85}

#End Region

#Region "メンバー変数"

    Private _mediaPlayer As MpvPlayerWrapper
    Private _currentPlaybackSpeed As Double = 1.0
    Private _isAutoBackupEnabled As Integer
    Private _originalVideoPath As String
    Private _isWindowInitialized As Boolean = True

    ''' <summary>
    ''' メインフォームのインスタンス（シングルトン）
    ''' </summary>
    Public Shared Instance As MainPlayerForm

#End Region

#Region "フォームイベント"

    Private Sub MainPlayerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Instance = Me
        InitializeWindowPosition()
        InitializeMediaPlayer()
        InitializeHotKeys()
        LoadDefaultSettings()
        ApplyPanelHeights()
        ApplyUISettings()
    End Sub

    Private Sub MainPlayerForm_Closing(sender As Object, e As CancelEventArgs) Handles MyBase.Closing
        SaveCurrentSettings()
        DisposeHotKeys()
        DisposeMediaPlayer()
    End Sub

#End Region

#Region "初期化処理"

    ''' <summary>
    ''' ウィンドウ位置の初期化
    ''' </summary>
    Private Sub InitializeWindowPosition()
        If Me.Left < 50 Then
            Me.Left = (Screen.PrimaryScreen.Bounds.Width - Me.Width) \ 2
        End If
        If Me.Top < 50 Then
            Me.Top = (Screen.PrimaryScreen.Bounds.Height - Me.Height) \ 2
        End If
    End Sub

    ''' <summary>
    ''' メディアプレイヤーの初期化
    ''' </summary>
    Private Sub InitializeMediaPlayer()
        Me.AllowDrop = True
        _isWindowInitialized = True

        _mediaPlayer = New MpvPlayerWrapper(MpvPanel)
        AddHandler _mediaPlayer.MediaChanged, AddressOf OnMediaChanged

        _mediaPlayer.Volume = My.Settings.Onryou
        TrackBar6.Value = _mediaPlayer.Volume
        Label5.Text = $"{_mediaPlayer.Volume}%"
    End Sub

    ''' <summary>
    ''' メディア変更時の処理
    ''' </summary>
    Private Sub OnMediaChanged()
        ' TrackBar1の最大値をメディアの長さに設定
        Dim dur As Double = _mediaPlayer.Duration
        If dur > 0 Then
            TrackBar1.Maximum = CInt(dur)
            Label1.Text = "00:00:00 / " & TimeSpan.FromSeconds(dur).ToString("hh\:mm\:ss")
        End If
        TextBox1.Text = _mediaPlayer.FileName
        TrackBar2.Value = CInt(_mediaPlayer.Speed * 10)
        Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
    End Sub

    ''' <summary>
    ''' ホットキーの初期化
    ''' </summary>
    Private Sub InitializeHotKeys()
        HotKeyManager.CreateHotKeyAtoms(Me.Handle)

        ' 各種ホットキーを登録
        RegisterAllHotKeys()
    End Sub

    ''' <summary>
    ''' 全ホットキーを登録
    ''' </summary>
    Private Sub RegisterAllHotKeys()
        For Each hotkeyType As HotKeyType In [Enum].GetValues(GetType(HotKeyType))
            Dim modifierProp As String = HotKeyManager.GetSettingModifierProperty(hotkeyType)
            Dim keyProp As String = HotKeyManager.GetSettingKeyProperty(hotkeyType)

            If String.IsNullOrEmpty(modifierProp) OrElse String.IsNullOrEmpty(keyProp) Then
                Continue For
            End If

            Dim modifierValue As Integer = CInt(CallByName(My.Settings, modifierProp, CallType.Get))
            Dim keyValue As Keys = CType(CallByName(My.Settings, keyProp, CallType.Get), Keys)

            RegisterSingleHotKey(hotkeyType, modifierValue, keyValue)
        Next
    End Sub

    ''' <summary>
    ''' 単一のホットキーを登録
    ''' </summary>
    Private Sub RegisterSingleHotKey(hotkeyType As HotKeyType, modifierSetting As Integer, key As Keys)
        Dim modifier As Integer = HotKeyManager.GetModifierValue(modifierSetting)
        Dim atomId As Short = HotKeyManager.HotKeyAtoms(hotkeyType)

        HotKeyManager.RegisterHotKey(Me.Handle, atomId, modifier, key)
    End Sub

    ''' <summary>
    ''' デフォルト設定の読み込み
    ''' </summary>
    Private Sub LoadDefaultSettings()
        If My.Settings.shokai = 1 Then
            Return
        End If

        ' ジャンプボタンの初期値設定
        InitializeJumpButtonSettings()

        ' 速度コントロールボタンの初期値設定
        InitializeSpeedButtonSettings()

        My.Settings.shokai = 1
    End Sub

    ''' <summary>
    ''' ジャンプボタン設定の初期化
    ''' </summary>
    Private Sub InitializeJumpButtonSettings()
        Dim jumpValues As Integer() = {1, 3, 5, 10, 15, 30, 60, 180, 300, 600, -1, -3, -5, -10, -15, -30, -60, -180, -300, -600}

        For i As Integer = 0 To jumpValues.Length - 1
            CallByName(My.Settings, $"SK{i + 1}", CallType.Set, jumpValues(i))
        Next
    End Sub

    ''' <summary>
    ''' 速度コントロールボタン設定の初期化
    ''' </summary>
    Private Sub InitializeSpeedButtonSettings()
        Dim speedValues As Double() = {5, 8, 10, 12, 13, 14, 15, 20}

        For i As Integer = 0 To speedValues.Length - 1
            CallByName(My.Settings, $"SC{i + 1}", CallType.Set, speedValues(i))
        Next
    End Sub

    ''' <summary>
    ''' パネル高さの適用
    ''' </summary>
    Private Sub ApplyPanelHeights()
        MpvPanel.Height = My.Settings.p21_height
    End Sub

    ''' <summary>
    ''' UI設定の復元
    ''' </summary>
    Private Sub ApplyUISettings()
        ' 動画表示画面の復元
        ' gamen = False のとき動画画面を表示（CheckBox2.Checked = True）
        If My.Settings.gamen = False Then
            CheckBox2.Checked = True
        Else
            SplitContainer3.SplitterDistance = My.Settings.SC3_Distance
            CheckBox2.Checked = False
        End If

        ' しおりパネルの復元
        ' shiori = True のときしおりパネルを表示
        If My.Settings.shiori = True Then
            SplitContainer1.Panel2Collapsed = False
            ' SC1_Distanceが0の場合、デフォルト値を使用
            If My.Settings.SC1_Distance > 0 Then
                SplitContainer1.SplitterDistance = My.Settings.SC1_Distance
            Else
                SplitContainer1.SplitterDistance = SplitContainer1.Width - 250
            End If
        Else
            SplitContainer1.Panel2Collapsed = True
        End If

        ' プレイリストパネルの復元
        ' PL = True のときプレイリストを表示（CheckBox1.Checked = True）
        ' PL = False のときプレイリストを非表示（CheckBox1.Checked = False）
        If My.Settings.PL = True Then
            CheckBox1.Checked = True
            SplitContainer2.Panel1Collapsed = False
            If My.Settings.SC2_Distance > 0 Then
                SplitContainer2.SplitterDistance = My.Settings.SC2_Distance
            Else
                SplitContainer2.SplitterDistance = 298
            End If
        Else
            CheckBox1.Checked = False
            SplitContainer2.Panel1Collapsed = True
        End If
    End Sub

#End Region

#Region "終了処理"

    ''' <summary>
    ''' 現在の設定を保存
    ''' </summary>
    Private Sub SaveCurrentSettings()
        ' 再生情報の保存
        My.Settings.LastOpenedFile = _mediaPlayer.FilePath
        My.Settings.LastIchi = _mediaPlayer.Position

        ' UI状態の保存
        My.Settings.gamen = Not CheckBox2.Checked
        My.Settings.SC3_Distance = SplitContainer3.SplitterDistance
        My.Settings.shiori = Not SplitContainer1.Panel2Collapsed
        My.Settings.SC1_Distance = SplitContainer1.SplitterDistance
        My.Settings.PL = Not CheckBox1.Checked
        My.Settings.SC2_Distance = SplitContainer2.SplitterDistance
        My.Settings.MyClientSize = Me.ClientSize
    End Sub

    ''' <summary>
    ''' ホットキーの解放
    ''' </summary>
    Private Sub DisposeHotKeys()
        HotKeyManager.DisposeHotKeys(Me.Handle)
    End Sub

    ''' <summary>
    ''' メディアプレイヤーの解放
    ''' </summary>
    Private Sub DisposeMediaPlayer()
        If _mediaPlayer IsNot Nothing Then
            _mediaPlayer.Dispose()
            _mediaPlayer = Nothing
        End If
    End Sub

#End Region

#Region "イベントハンドラ"

    ''' <summary>
    ''' メディア変更時の処理
    ''' </summary>
    Private Sub OnMediaChanged(sender As Object, e As EventArgs)
        ' メディア変更時の処理
    End Sub

    ''' <summary>
    ''' ウィンドウプロシージャ（ホットキー処理用）
    ''' </summary>
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = HotKeyManager.WM_HOTKEY Then
            HandleHotKey(m.WParam.ToInt32())
        End If
        MyBase.WndProc(m)
    End Sub

    ''' <summary>
    ''' ホットキー処理
    ''' </summary>
    Private Sub HandleHotKey(hotkeyId As Integer)
        ' ホットキーIDに対応する処理を実行
        For Each kvp As KeyValuePair(Of HotKeyType, Short) In HotKeyManager.HotKeyAtoms
            If kvp.Value = hotkeyId Then
                ExecuteHotKeyAction(kvp.Key)
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    ''' ホットキーアクションの実行
    ''' </summary>
    Private Sub ExecuteHotKeyAction(hotkeyType As HotKeyType)
        Select Case hotkeyType
            Case HotKeyType.PlayPause
                TogglePlayPause()
            Case HotKeyType.PlayPauseWithCounterCopy1
                TogglePlayPauseWithCounterCopy(1)
            Case HotKeyType.PlayPauseWithCounterCopy2
                TogglePlayPauseWithCounterCopy(2)
            Case HotKeyType.PlayPauseWithCounterCopy3
                TogglePlayPauseWithCounterCopy(3)
            Case HotKeyType.StopPlayback
                StopPlayback()
            Case HotKeyType.CounterCopy1
                CopyCounterToClipboard(1)
            Case HotKeyType.CounterCopy2
                CopyCounterToClipboard(2)
            Case HotKeyType.CounterCopy3
                CopyCounterToClipboard(3)
            Case HotKeyType.AddBookmark
                AddBookmark()
            Case HotKeyType.PlayPauseWithBookmark
                TogglePlayPauseWithBookmark()
            Case HotKeyType.SpeedUp
                AdjustPlaybackSpeed(0.1)
            Case HotKeyType.SpeedDown
                AdjustPlaybackSpeed(-0.1)
            Case HotKeyType.SpeedResetTo1x
                SetPlaybackSpeed(1.0)
            Case HotKeyType.SpeedSetToHalf
                SetPlaybackSpeed(0.5)
            Case HotKeyType.SpeedSetToDouble
                SetPlaybackSpeed(2.0)
            Case HotKeyType.BringWindowToFront
                BringWindowToFront()
            Case HotKeyType.JumpHotkey1 To HotKeyType.JumpHotkey6
                ExecuteJumpHotkey(hotkeyType)
            Case HotKeyType.SpeedControlButton1 To HotKeyType.SpeedControlButton7
                ExecuteSpeedControlHotkey(hotkeyType)
            Case HotKeyType.ClipboardJump
                JumpToClipboardPosition()
        End Select
    End Sub

#End Region

#Region "再生制御"

    ''' <summary>
    ''' 再生/一時停止の切り替え
    ''' </summary>
    Private Sub TogglePlayPause()
        If _mediaPlayer.IsPlaying Then
            _mediaPlayer.Pause()
        Else
            _mediaPlayer.Play()
        End If
    End Sub

    ''' <summary>
    ''' 再生/一時停止とカウンタコピー
    ''' </summary>
    Private Sub TogglePlayPauseWithCounterCopy(counterIndex As Integer)
        CopyCounterToClipboard(counterIndex)
        TogglePlayPause()
    End Sub

    ''' <summary>
    ''' 停止
    ''' </summary>
    Private Sub StopPlayback()
        _mediaPlayer.Stop()
    End Sub

    ''' <summary>
    ''' カウンタをクリップボードにコピー
    ''' </summary>
    Private Sub CopyCounterToClipboard(counterIndex As Integer)
        ' カウンタコピー処理
        Dim timeCode As String = GetCurrentTimeCode()
        Dim prefix As String = GetTimeCodePrefix(counterIndex)
        Dim suffix As String = GetTimeCodeSuffix(counterIndex)

        Clipboard.SetText($"{prefix}{timeCode}{suffix}")
    End Sub

    ''' <summary>
    ''' 現在のタイムコードを取得
    ''' </summary>
    Private Function GetCurrentTimeCode() As String
        Dim position As Double = _mediaPlayer.Position
        Dim hours As Integer = CInt(position) \ 3600
        Dim minutes As Integer = (CInt(position) Mod 3600) \ 60
        Dim seconds As Integer = CInt(position) Mod 60
        Dim frames As Integer = CInt((position - Math.Floor(position)) * 30)

        Return $"{hours:D2}:{minutes:D2}:{seconds:D2}.{frames:D2}"
    End Function

    ''' <summary>
    ''' タイムコード修飾（頭）を取得
    ''' </summary>
    Private Function GetTimeCodePrefix(index As Integer) As String
        Select Case index
            Case 1 : Return My.Settings.Atama
            Case 2 : Return My.Settings.Atama2
            Case 3 : Return My.Settings.Atama3
            Case Else : Return ""
        End Select
    End Function

    ''' <summary>
    ''' タイムコード修飾（末尾）を取得
    ''' </summary>
    Private Function GetTimeCodeSuffix(index As Integer) As String
        Select Case index
            Case 1 : Return My.Settings.Oshiri
            Case 2 : Return My.Settings.Oshiri2
            Case 3 : Return My.Settings.Oshiri3
            Case Else : Return ""
        End Select
    End Function

    ''' <summary>
    ''' しおりに追加
    ''' </summary>
    Private Sub AddBookmark()
        ' しおり追加処理
    End Sub

    ''' <summary>
    ''' 再生/一時停止としおり追加
    ''' </summary>
    Private Sub TogglePlayPauseWithBookmark()
        AddBookmark()
        TogglePlayPause()
    End Sub

    ''' <summary>
    ''' 再生速度の調整
    ''' </summary>
    Private Sub AdjustPlaybackSpeed(delta As Double)
        _currentPlaybackSpeed = _mediaPlayer.Speed + delta
        SetPlaybackSpeed(_currentPlaybackSpeed)
    End Sub

    ''' <summary>
    ''' 再生速度を設定
    ''' </summary>
    Private Sub SetPlaybackSpeed(speed As Double)
        _currentPlaybackSpeed = speed
        _mediaPlayer.Speed = speed
        Label4.Text = $"x{speed:F1}"
    End Sub

    ''' <summary>
    ''' ウィンドウを最前面に
    ''' </summary>
    Private Sub BringWindowToFront()
        Me.TopMost = Not Me.TopMost
    End Sub

    ''' <summary>
    ''' ジャンプホットキーの実行
    ''' </summary>
    Private Sub ExecuteJumpHotkey(hotkeyType As HotKeyType)
        Dim jumpSeconds As Integer = 0

        Select Case hotkeyType
            Case HotKeyType.JumpHotkey1 : jumpSeconds = My.Settings.MM1
            Case HotKeyType.JumpHotkey2 : jumpSeconds = My.Settings.MM2
            Case HotKeyType.JumpHotkey3 : jumpSeconds = My.Settings.MM3
            Case HotKeyType.JumpHotkey4 : jumpSeconds = My.Settings.HO1
            Case HotKeyType.JumpHotkey5 : jumpSeconds = My.Settings.HO2
            Case HotKeyType.JumpHotkey6 : jumpSeconds = My.Settings.HO3
        End Select

        If jumpSeconds <> 0 Then
            JumpToPosition(_mediaPlayer.Position + jumpSeconds)
        End If
    End Sub

    ''' <summary>
    ''' 速度コントロールホットキーの実行
    ''' </summary>
    Private Sub ExecuteSpeedControlHotkey(hotkeyType As HotKeyType)
        Dim speed As Double = 1.0

        Select Case hotkeyType
            Case HotKeyType.SpeedControlButton1 : speed = My.Settings.SC1 / 10.0
            Case HotKeyType.SpeedControlButton2 : speed = My.Settings.SC2 / 10.0
            Case HotKeyType.SpeedControlButton3 : speed = My.Settings.SC3 / 10.0
            Case HotKeyType.SpeedControlButton4 : speed = My.Settings.SC4 / 10.0
            Case HotKeyType.SpeedControlButton5 : speed = My.Settings.SC5 / 10.0
            Case HotKeyType.SpeedControlButton6 : speed = My.Settings.SC6 / 10.0
            Case HotKeyType.SpeedControlButton7 : speed = My.Settings.SC7 / 10.0
        End Select

        SetPlaybackSpeed(speed)
    End Sub

    ''' <summary>
    ''' 指定位置にジャンプ
    ''' </summary>
    Private Sub JumpToPosition(position As Double)
        _mediaPlayer.Position = Math.Max(0, position)
    End Sub

    ''' <summary>
    ''' クリップボードの位置にジャンプ
    ''' </summary>
    Private Sub JumpToClipboardPosition()
        ' クリップボードから位置を読み込んでジャンプ
    End Sub

#End Region

#Region "UI制御"

    ''' <summary>
    ''' 動画表示画面の表示/非表示切り替え
    ''' </summary>
    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        SplitContainer3.Panel1Collapsed = Not CheckBox2.Checked
    End Sub

    ''' <summary>
    ''' プレイリストの表示/非表示切り替え
    ''' </summary>
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        SplitContainer2.Panel1Collapsed = Not CheckBox1.Checked
    End Sub

#End Region

#Region "ボタンイベントハンドラ"

    ' 速度ボタン
    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        TrackBar2.Value = My.Settings.SC1 \ 10
        Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
        _mediaPlayer.Speed = TrackBar2.Value * 0.1
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        TrackBar2.Value = My.Settings.SC2 \ 10
        Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
        _mediaPlayer.Speed = TrackBar2.Value * 0.1
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        TrackBar2.Value = My.Settings.SC3 \ 10
        Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
        _mediaPlayer.Speed = TrackBar2.Value * 0.1
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        TrackBar2.Value = My.Settings.SC4 \ 10
        Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
        _mediaPlayer.Speed = TrackBar2.Value * 0.1
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        TrackBar2.Value = My.Settings.SC5 \ 10
        Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
        _mediaPlayer.Speed = TrackBar2.Value * 0.1
    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        TrackBar2.Value = My.Settings.SC6 \ 10
        Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
        _mediaPlayer.Speed = TrackBar2.Value * 0.1
    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        TrackBar2.Value = My.Settings.SC7 \ 10
        Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
        _mediaPlayer.Speed = TrackBar2.Value * 0.1
    End Sub

    Private Sub TrackBar2_Scroll(sender As Object, e As EventArgs) Handles TrackBar2.Scroll
        Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
        _mediaPlayer.Speed = TrackBar2.Value * 0.1
    End Sub

    Private Sub Button400_Click(sender As Object, e As EventArgs) Handles Button400.Click
        _mediaPlayer.Pause()
        _mediaPlayer.Position = 0
    End Sub

    Private Sub Button200_Click(sender As Object, e As EventArgs) Handles Button200.Click
        If _mediaPlayer.IsPlaying Then
            _mediaPlayer.Pause()
        Else
            _mediaPlayer.Play()
        End If
    End Sub

    ' ジャンプボタン
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK1
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK11
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK2
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK12
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK3
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK13
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK4
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK14
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK5
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK15
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK6
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK16
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK7
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK17
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK8
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK18
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK9
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK19
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK10
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        _mediaPlayer.Position = TrackBar1.Value + My.Settings.SK20
    End Sub

    Private Sub TrackBar6_Scroll(sender As Object, e As EventArgs) Handles TrackBar6.Scroll
        _mediaPlayer.Volume = TrackBar6.Value
        My.Settings.Onryou = TrackBar6.Value
        Label5.Text = TrackBar6.Value & "%"
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        DataGridView1.Rows.Add()
        Dim i As Integer = DataGridView1.Rows.Count - 1
        Dim appPath As String = System.Reflection.Assembly.GetExecutingAssembly().Location

        If My.Settings.autoBMDir = "" Then
            My.Settings.autoBMDir = IO.Path.GetDirectoryName(appPath)
        End If

        DataGridView1.Rows(i).Cells(0).Value = Strings.Left(Label1.Text, 8)
        DataGridView1.Rows(i).Cells(2).Value = TrackBar1.Value
        DataGridView1.CurrentCell = DataGridView1(0, i)

        If My.Settings.autoBM Then
            WriteCsvFromDGV(DataGridView1, TextBox1.Text)
        Else
            WriteCsvFromDGV(DataGridView1, "om_tmp")
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim colIndex As Integer = e.ColumnIndex
        Dim rowIndex As Integer = e.RowIndex

        If colIndex <> 0 And colIndex <> 3 Then
            Exit Sub
        End If

        Select Case colIndex
            Case 0 ' ジャンプ
                Dim i As Integer = DataGridView1.SelectedCells(0).RowIndex
                TrackBar1.Value = DataGridView1.Rows(i).Cells(2).Value
                _mediaPlayer.Position = TrackBar1.Value
                If My.Settings.shiori_PS Then
                    _mediaPlayer.Play()
                Else
                    _mediaPlayer.Play()
                    _mediaPlayer.Pause()
                End If
            Case 3 ' 削除
                Dim i As Integer = DataGridView1.SelectedCells(0).RowIndex
                DataGridView1.Rows.RemoveAt(i)
        End Select
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        If DataGridView1.RowCount = 0 Then Exit Sub
        Dim i As Integer = DataGridView1.SelectedCells(0).RowIndex
        DataGridView1.Rows.RemoveAt(i)
    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        If TextBox3.Text = "" Then Exit Sub
        DataGridView1.Rows.Add()
        Dim i As Integer = DataGridView1.Rows.Count - 1
        Dim n As Integer = TextBox3.Text.Length
        Dim a As Integer
        Dim cCounter As String = ""

        For a = 1 To n
            Dim ch As String = Strings.Mid(TextBox3.Text, a, 1)
            If ch = "0" OrElse ch = "1" OrElse ch = "2" OrElse ch = "3" OrElse ch = "4" OrElse ch = "5" OrElse ch = "6" OrElse ch = "7" OrElse ch = "8" OrElse ch = "9" Then
                cCounter &= ch
            End If
        Next

        If cCounter.Length > 6 Then
            MsgBox("数字が7桁以上入力されています", vbOKOnly)
            TextBox2.Clear()
            Exit Sub
        End If

        Select Case cCounter.Length
            Case 1 : cCounter = "00000" & cCounter
            Case 2 : cCounter = "0000" & cCounter
            Case 3 : cCounter = "000" & cCounter
            Case 4 : cCounter = "00" & cCounter
            Case 5 : cCounter = "0" & cCounter
        End Select

        If (Integer.Parse(Strings.Mid(cCounter, 1, 2)) * 3600) + (Integer.Parse(Strings.Mid(cCounter, 3, 2)) * 60) + (Integer.Parse(Strings.Mid(cCounter, 5, 2))) > _mediaPlayer.Duration Then
            MsgBox("入力されたカウンタがファイルの長さを超えています")
            TextBox2.Clear()
            Exit Sub
        End If

        DataGridView1.Rows(i).Cells(0).Value = Strings.Mid(cCounter, 1, 2) & ":" & Strings.Mid(cCounter, 3, 2) & ":" & Strings.Mid(cCounter, 5, 2)
        DataGridView1.Rows(i).Cells(2).Value = (Integer.Parse(Strings.Mid(cCounter, 1, 2)) * 3600) + (Integer.Parse(Strings.Mid(cCounter, 3, 2)) * 60) + (Integer.Parse(Strings.Mid(cCounter, 5, 2)))
        DataGridView1.CurrentCell = DataGridView1(0, i)
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        If TextBox2.Text = "" Then Exit Sub
        Dim n As Integer = TextBox2.Text.Length
        Dim a As Integer
        Dim cCounter As String = ""

        For a = 1 To n
            Dim ch As String = Strings.Mid(TextBox2.Text, a, 1)
            If ch = "0" OrElse ch = "1" OrElse ch = "2" OrElse ch = "3" OrElse ch = "4" OrElse ch = "5" OrElse ch = "6" OrElse ch = "7" OrElse ch = "8" OrElse ch = "9" Then
                cCounter &= ch
            End If
        Next

        If cCounter.Length > 6 Then
            MsgBox("数字が7桁以上入力されています", vbOKOnly)
            TextBox2.Clear()
            Exit Sub
        End If

        Select Case cCounter.Length
            Case 1 : cCounter = "00000" & cCounter
            Case 2 : cCounter = "0000" & cCounter
            Case 3 : cCounter = "000" & cCounter
            Case 4 : cCounter = "00" & cCounter
            Case 5 : cCounter = "0" & cCounter
        End Select

        If (Integer.Parse(Strings.Mid(cCounter, 1, 2)) * 3600) + (Integer.Parse(Strings.Mid(cCounter, 3, 2)) * 60) + (Integer.Parse(Strings.Mid(cCounter, 5, 2))) > _mediaPlayer.Duration Then
            MsgBox("入力されたカウンタがファイルの長さを超えています")
            TextBox2.Clear()
            Exit Sub
        End If

        TrackBar1.Value = (Integer.Parse(Strings.Mid(cCounter, 1, 2)) * 3600) + (Integer.Parse(Strings.Mid(cCounter, 3, 2)) * 60) + (Integer.Parse(Strings.Mid(cCounter, 5, 2)))
        _mediaPlayer.Position = TrackBar1.Value
        _mediaPlayer.Play()
        TextBox2.Clear()
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        Try
            If WriteCsvFromDGV(Me.DataGridView1, TextBox1.Text) = True Then
                MsgBox("書出完了", vbOKOnly)
            Else
                MsgBox("書出失敗", vbOKOnly)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbOKOnly)
        End Try
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        Dim nFile As String = Nothing
        Dim strMemo As String
        Dim n As Integer
        Dim iLine As String
        Dim cCounter As String

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            nFile = OpenFileDialog1.FileName
            Select Case Path.GetExtension(nFile).ToLower()
                Case ".csv"
                    CsvReader()
                Case ".doc", ".docx"
                    Dim objWord As Object
                    Dim objDoc As Object
                    Dim txtNakami As String

                    Try
                        objWord = CreateObject("Word.Application")
                        objWord.Visible = False
                        objDoc = objWord.Documents.Open(OpenFileDialog1.FileName)

                        objDoc.Range.Copy()
                        txtNakami = Clipboard.GetText()
                        For n = 0 To txtNakami.Length - 1
                            Select Case txtNakami.Substring(n, 1)
                                Case My.Settings.Fuka
                                    cCounter = txtNakami.Substring(n + 1, 10)
                                    cCounter = (Integer.Parse(cCounter.Substring(1, 2)) * 3600) + (Integer.Parse(cCounter.Substring(4, 2)) * 60) + (Integer.Parse(cCounter.Substring(7, 2)))
                                    iLine = txtNakami.Substring(n + 2, 8) & "," & "聞き取り不可" & "," & cCounter & "," & "削除"
                                    Dim RowPlus1() As String = iLine.Split(",")
                                    DataGridView1.Rows.Add(RowPlus1)
                                Case My.Settings.Fumei
                                    For i = n + 1 To txtNakami.Length - 1
                                        If txtNakami.Substring(i, 1) = My.Settings.Fumei2 Then
                                            strMemo = txtNakami.Substring(n + 1, i - n - 1)
                                            cCounter = txtNakami.Substring(i + 1, 10)
                                            cCounter = (Integer.Parse(cCounter.Substring(1, 2)) * 3600) + (Integer.Parse(cCounter.Substring(4, 2)) * 60) + (Integer.Parse(cCounter.Substring(7, 2)))
                                            iLine = txtNakami.Substring(i + 2, 8) & "," & strMemo & "？," & cCounter & "," & "削除"
                                            Dim RowPlus2() As String = iLine.Split(",")
                                            DataGridView1.Rows.Add(RowPlus2)
                                            Exit For
                                        End If
                                    Next i
                                Case My.Settings.Sonota
                                    For i = n + 1 To txtNakami.Length - 1
                                        If txtNakami.Substring(i, 1) = "(" OrElse txtNakami.Substring(i, 1) = "（" Then
                                            strMemo = txtNakami.Substring(n, i - n)
                                            cCounter = txtNakami.Substring(i, 10)
                                            cCounter = (Integer.Parse(cCounter.Substring(1, 2)) * 3600) + (Integer.Parse(cCounter.Substring(4, 2)) * 60) + (Integer.Parse(cCounter.Substring(7, 2)))
                                            iLine = txtNakami.Substring(i + 1, 8) & "," & strMemo & "," & cCounter & "," & "削除"
                                            Dim RowPlus3() As String = iLine.Split(",")
                                            DataGridView1.Rows.Add(RowPlus3)
                                            Exit For
                                        End If
                                    Next i
                            End Select
                        Next n

                        objDoc = Nothing
                        objWord = Nothing
                    Catch ex As Exception
                        MsgBox("Wordファイルの読み込みに失敗しました: " & ex.Message, vbOKOnly)
                    End Try
                Case ".txt"
                    Using reader = New StreamReader(nFile, Encoding.GetEncoding("Shift_JIS"))
                        Dim txtNakami As String = reader.ReadToEnd()
                        For n = 0 To txtNakami.Length - 1
                            Select Case txtNakami.Substring(n, 1)
                                Case My.Settings.Fuka
                                    cCounter = txtNakami.Substring(n + 1, 10)
                                    cCounter = (Integer.Parse(cCounter.Substring(1, 2)) * 3600) + (Integer.Parse(cCounter.Substring(4, 2)) * 60) + (Integer.Parse(cCounter.Substring(7, 2)))
                                    iLine = txtNakami.Substring(n + 2, 8) & "," & "聞き取り不可" & "," & cCounter & "," & "削除"
                                    Dim RowPlus4() As String = iLine.Split(",")
                                    DataGridView1.Rows.Add(RowPlus4)
                                Case My.Settings.Fumei
                                    For i = n + 1 To txtNakami.Length - 1
                                        If txtNakami.Substring(i, 1) = My.Settings.Fumei2 Then
                                            strMemo = txtNakami.Substring(n + 1, i - n - 1)
                                            cCounter = txtNakami.Substring(i + 1, 10)
                                            cCounter = (Integer.Parse(cCounter.Substring(1, 2)) * 3600) + (Integer.Parse(cCounter.Substring(4, 2)) * 60) + (Integer.Parse(cCounter.Substring(7, 2)))
                                            iLine = txtNakami.Substring(i + 2, 8) & "," & strMemo & "？," & cCounter & "," & "削除"
                                            Dim RowPlus5() As String = iLine.Split(",")
                                            DataGridView1.Rows.Add(RowPlus5)
                                            Exit For
                                        End If
                                    Next i
                                Case My.Settings.Sonota
                                    For i = n + 1 To txtNakami.Length - 1
                                        If txtNakami.Substring(i, 1) = "(" OrElse txtNakami.Substring(i, 1) = "（" Then
                                            strMemo = txtNakami.Substring(n, i - n)
                                            cCounter = txtNakami.Substring(i, 10)
                                            cCounter = (Integer.Parse(cCounter.Substring(1, 2)) * 3600) + (Integer.Parse(cCounter.Substring(4, 2)) * 60) + (Integer.Parse(cCounter.Substring(7, 2)))
                                            iLine = txtNakami.Substring(i + 1, 8) & "," & strMemo & "," & cCounter & "," & "削除"
                                            Dim RowPlus6() As String = iLine.Split(",")
                                            DataGridView1.Rows.Add(RowPlus6)
                                            Exit For
                                        End If
                                    Next i
                            End Select
                        Next n
                    End Using
                Case Else
                    MsgBox("CSVファイル、txtファイル、docファイル、docxファイルのみ対応です", vbOKOnly)
                    Exit Sub
            End Select
        End If
    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        MsgBox("画面キャプチャ機能は未実装です", vbOKOnly)
    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        Dim f2 As New SettingsForm()
        f2.ShowDialog()
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        ToolTip1.SetToolTip(TrackBar1, TimeSpan.FromSeconds(TrackBar1.Value).ToString("hh\:mm\:ss"))
        Label1.Text = TimeSpan.FromSeconds(TrackBar1.Value).ToString("hh\:mm\:ss") & " / " & TimeSpan.FromSeconds(_mediaPlayer.Duration).ToString("hh\:mm\:ss")
        _mediaPlayer.Position = TrackBar1.Value
    End Sub

    Private Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            _mediaPlayer.LoadFile(OpenFileDialog1.FileName)
            My.Settings.LastOpenedFile = OpenFileDialog1.FileName
            DataGridView1.Rows.Clear()
        End If
    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        Me.TopMost = Not Me.TopMost
        If Me.TopMost Then
            Button35.Image = My.Resources.AlwaysVisible_16x
        Else
            Button35.Image = My.Resources.PinnedItem_16x
        End If
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        If SplitContainer1.Panel2Collapsed Then
            SplitContainer1.Panel2Collapsed = False
        Else
            SplitContainer1.Panel2Collapsed = True
        End If
    End Sub

#End Region

#Region "Timer"

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If _mediaPlayer.IsPlaying Then
            Dim pos As Integer = CInt(_mediaPlayer.Position)
            If pos > TrackBar1.Maximum Then
                pos = TrackBar1.Maximum
            End If
            TrackBar1.Value = pos
            Label1.Text = TimeSpan.FromSeconds(_mediaPlayer.Position).ToString("hh\:mm\:ss") & " / " & TimeSpan.FromSeconds(_mediaPlayer.Duration).ToString("hh\:mm\:ss")
        End If
    End Sub

#End Region

#Region "ドラッグ＆ドロップ"

    Private Const CtrlMask As Integer = 8

    Private Sub MainPlayerForm_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            If files.Length > 0 Then
_mediaPlayer.LoadFile(files(0))

                My.Settings.LastOpenedFile = files(0)
            End If
        End If
    End Sub

    Private Sub MainPlayerForm_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            If (e.KeyState And CtrlMask) = CtrlMask Then
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.Move
            End If
        End If
    End Sub

    Private Sub TableLayoutPanel1_DragDrop(sender As Object, e As DragEventArgs) Handles TableLayoutPanel1.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            If files.Length > 0 Then
_mediaPlayer.LoadFile(files(0))

                My.Settings.LastOpenedFile = files(0)
            End If
        End If
    End Sub

    Private Sub TableLayoutPanel1_DragEnter(sender As Object, e As DragEventArgs) Handles TableLayoutPanel1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            If (e.KeyState And CtrlMask) = CtrlMask Then
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.Move
            End If
        End If
    End Sub

    Private Sub TrackBar1_DragDrop(sender As Object, e As DragEventArgs) Handles TrackBar1.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            If files.Length > 0 Then
_mediaPlayer.LoadFile(files(0))

                My.Settings.LastOpenedFile = files(0)
            End If
        End If
    End Sub

    Private Sub TrackBar1_DragEnter(sender As Object, e As DragEventArgs) Handles TrackBar1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            If (e.KeyState And CtrlMask) = CtrlMask Then
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.Move
            End If
        End If
    End Sub

    Private Sub SplitContainer2_DragDrop(sender As Object, e As DragEventArgs) Handles SplitContainer2.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            If files.Length > 0 Then
_mediaPlayer.LoadFile(files(0))

                My.Settings.LastOpenedFile = files(0)
            End If
        End If
    End Sub

    Private Sub SplitContainer2_DragEnter(sender As Object, e As DragEventArgs) Handles SplitContainer2.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            If (e.KeyState And CtrlMask) = CtrlMask Then
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.Move
            End If
        End If
    End Sub

#End Region

#Region "CSV入出力"

    ''' <summary>
    ''' CSVファイル読み込み
    ''' </summary>
    Private Sub CsvReader()
        Dim csvFile As String = OpenFileDialog1.FileName

        DataGridView1.Rows.Clear()

        Dim SR As New StreamReader(csvFile, System.Text.Encoding.GetEncoding("shift_jis"))
        Dim con_str As String

        con_str = SR.ReadLine()
        If con_str Is Nothing Then Exit Sub

        Do
            con_str = SR.ReadLine()
            If con_str Is Nothing Then Exit Do
            con_str = Replace(con_str, """", "")
            Dim RowPlus() As String = con_str.Split(",")
            DataGridView1.Rows.Add(RowPlus)
        Loop

        SR.Close()
    End Sub

    ''' <summary>
    ''' DataGridViewからCSVファイルへの書込処理
    ''' </summary>
    Private Function WriteCsvFromDGV(ByVal dgv As DataGridView, ByVal astrFileName As String) As Boolean
        WriteCsvFromDGV = False

        Try
            Dim arrData()() As String = Nothing
            Dim arrHead As String() = Nothing
            Dim filePath As String

            ' ファイルパスの構築
            If astrFileName.Contains("\") OrElse astrFileName.Contains("/") Then
                ' フルパスまたは相対パスが指定された場合
                filePath = astrFileName
            Else
                ' ファイル名のみの場合、autoBMDirを使用
                If My.Settings.autoBMDir = "" Then
                    filePath = Application.StartupPath & "\" & astrFileName & ".csv"
                Else
                    filePath = My.Settings.autoBMDir & "\" & astrFileName & ".csv"
                End If
            End If

            For col As Integer = 0 To Me.DataGridView1.Columns.Count - 1
                ReDim Preserve arrHead(col)
                arrHead(col) = CStr(Me.DataGridView1.Columns(col).HeaderCell.Value)
            Next
            ReDim Preserve arrData(0)
            arrData(0) = arrHead

            For row As Integer = 0 To Me.DataGridView1.Rows.Count - 1
                If Me.DataGridView1.Rows(row).IsNewRow Then
                    Continue For
                End If

                Dim arrLine As String() = Nothing
                For col As Integer = 0 To Me.DataGridView1.Columns.Count - 1
                    ReDim Preserve arrLine(col)
                    arrLine(col) = CStr(Me.DataGridView1.Rows(row).Cells(col).Value)
                Next

                ReDim Preserve arrData(row + 1)
                arrData(row + 1) = arrLine
            Next

            Return WriteCsv(filePath, arrData)

        Catch ex As Exception
            MsgBox(ex.Message, vbOKOnly)
        End Try
    End Function

    ''' <summary>
    ''' CSVファイルの書込処理
    ''' </summary>
    Private Function WriteCsv(ByVal astrFileName As String, ByVal aarrData As String()()) As Boolean
        WriteCsv = False
        Dim sw As System.IO.StreamWriter = Nothing

        Try
            Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
            sw = New System.IO.StreamWriter(astrFileName, False, enc)

            For Each arrLine() As String In aarrData
                Dim blnFirst As Boolean = True
                For Each str As String In arrLine
                    If blnFirst = False Then
                        sw.Write(",")
                    End If
                    blnFirst = False
                    str = """" & str & """"
                    sw.Write(str)
                Next
                sw.Write(vbCrLf)
            Next

            Return True

        Catch ex As Exception
            MsgBox(ex.Message, vbOKOnly)
        Finally
            If sw IsNot Nothing Then
                sw.Close()
            End If
        End Try
    End Function

#End Region

#Region "その他イベントハンドラ"

    ''' <summary>
    ''' TextBox2でEnterキーが押されたときにジャンプ
    ''' </summary>
    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim n As Integer
            Dim a As Integer
            Dim cCounter As String = ""

            If TextBox2.Text = "" Then Exit Sub

            n = TextBox2.Text.Length

            For a = 1 To n
                Dim ch As String = Strings.Mid(TextBox2.Text, a, 1)
                If ch = "0" OrElse ch = "1" OrElse ch = "2" OrElse ch = "3" OrElse ch = "4" OrElse ch = "5" OrElse ch = "6" OrElse ch = "7" OrElse ch = "8" OrElse ch = "9" Then
                    cCounter &= ch
                End If
            Next a

            If cCounter.Length > 6 OrElse cCounter.Length < 0 Then
                MsgBox("数字が含まれていないか、7桁以上の数字が入力されています", vbOKOnly)
                TextBox2.Clear()
                Exit Sub
            End If

            Select Case cCounter.Length
                Case 1 : cCounter = "00000" & cCounter
                Case 2 : cCounter = "0000" & cCounter
                Case 3 : cCounter = "000" & cCounter
                Case 4 : cCounter = "00" & cCounter
                Case 5 : cCounter = "0" & cCounter
            End Select

            If (Integer.Parse(Strings.Mid(cCounter, 1, 2)) * 3600) + (Integer.Parse(Strings.Mid(cCounter, 3, 2)) * 60) + (Integer.Parse(Strings.Mid(cCounter, 5, 2))) > _mediaPlayer.Duration Then
                MsgBox("入力されたカウンタがファイルの長さを超えています", vbOKOnly)
                TextBox2.Clear()
                Exit Sub
            End If

            TrackBar1.Value = (Integer.Parse(Strings.Mid(cCounter, 1, 2)) * 3600) + (Integer.Parse(Strings.Mid(cCounter, 3, 2)) * 60) + (Integer.Parse(Strings.Mid(cCounter, 5, 2)))
            _mediaPlayer.Position = TrackBar1.Value
            _mediaPlayer.Play()
            TextBox2.Clear()
        End If
    End Sub

    ''' <summary>
    ''' SplitContainer1のスプリッター移動時
    ''' </summary>
    Private Sub SplitContainer1_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer1.SplitterMoved
        My.Settings.p11_height = SplitContainer1.Panel1.Height
        My.Settings.p11_width = SplitContainer1.Panel2.Width
        My.Settings.p12_height = SplitContainer1.Panel2.Height
        My.Settings.p12_width = SplitContainer1.Panel2.Width
        My.Settings.p21_height = SplitContainer3.Panel1.Height
        My.Settings.p21_width = SplitContainer3.Panel1.Width
        My.Settings.p22_height = SplitContainer3.Panel2.Height
        My.Settings.p22_width = SplitContainer3.Panel2.Width
    End Sub

    ''' <summary>
    ''' SplitContainer3のスプリッター移動時
    ''' </summary>
    Private Sub SplitContainer3_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer3.SplitterMoved
        My.Settings.p11_height = SplitContainer1.Panel1.Height
        My.Settings.p11_width = SplitContainer1.Panel2.Width
        My.Settings.p12_height = SplitContainer1.Panel2.Height
        My.Settings.p12_width = SplitContainer1.Panel2.Width
        My.Settings.p21_height = SplitContainer3.Panel1.Height
        My.Settings.p21_width = SplitContainer3.Panel1.Width
        My.Settings.p22_height = SplitContainer3.Panel2.Height
        My.Settings.p22_width = SplitContainer3.Panel2.Width
    End Sub

    ''' <summary>
    ''' TableLayoutPanel1のセル描画時（着色）
    ''' </summary>
    Private Sub TableLayoutPanel1_CellPaint(sender As Object, e As TableLayoutCellPaintEventArgs) Handles TableLayoutPanel1.CellPaint
        Dim KuroppoiColor2 As New SolidBrush(Color.FromArgb(50, 50, 50))

        ' Row 0
        For col As Integer = 0 To 16
            If e.Column = col AndAlso e.Row = 0 Then
                e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
            End If
        Next

        ' Row 1
        For col As Integer = 0 To 20
            If e.Column = col AndAlso e.Row = 1 Then
                e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
            End If
        Next

        ' 音量調整部分
        Dim KuroppoiColor As New SolidBrush(Color.FromArgb(64, 64, 64))
        If e.Column = 20 AndAlso (e.Row = 3 OrElse e.Row = 4 OrElse e.Row = 5) Then
            e.Graphics.FillRectangle(KuroppoiColor, e.CellBounds)
        End If

        ' 再生・停止部分
        For col As Integer = 0 To 5
            If e.Column = col AndAlso (e.Row = 5 OrElse e.Row = 6) Then
                e.Graphics.FillRectangle(KuroppoiColor, e.CellBounds)
            End If
        Next

        ' 速度調整部分
        If (e.Column = 6 OrElse e.Column >= 8 AndAlso e.Column <= 18) AndAlso e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
    End Sub

#End Region

End Class
