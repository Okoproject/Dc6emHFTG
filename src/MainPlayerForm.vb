Imports System.ComponentModel
Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Text

''' <summary>
'''     メイン動画プレイヤーフォーム
''' </summary>
Public Class MainPlayerForm

    <DllImport("user32.dll")>
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As Boolean, lParam As IntPtr) As IntPtr
    End Function

    Private Const WM_SETREDRAW As Integer = &HB

#Region "定数"

    ' ウィンドウ位置の最小値
    Private Const MinWindowPosition As Integer = 50

    ' 速度調整の係数
    Private Const SpeedMultiplier As Double = 0.1

    ' 速度ボタンのオフセット (Button21 -> SC1)
    Private Const SpeedButtonOffset As Integer = 20

    ' タイムラベルの長さ
    Private Const TimeLabelLength As Integer = 8

    ' カウンタの最大桁数
    Private Const MaxCounterDigits As Integer = 6

    ' ドラッグ＆ドロップのCtrlキーマスク
    Private Const CtrlMask As Integer = 8

    ' ファイル解析用の文字列長
    Private Const TimestampLength As Integer = 10
    Private Const TimeDisplayLength As Integer = 8

    ' カウンタ解析用の桁数
    Private Const HourDigits As Integer = 2
    Private Const MinuteDigits As Integer = 2
    Private Const SecondDigits As Integer = 2

#End Region

#Region "メンバー変数"

    Private _mediaPlayer As MpvPlayerWrapper
    Private _currentPlaybackSpeed As Double = 1.0

    ''' <summary>
    '''     メインフォームのインスタンス（シングルトン）
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
        ApplyUiSettings()
        UpdateControllerMinSize()
    End Sub

    Private Sub MainPlayerForm_Closing(sender As Object, e As CancelEventArgs) Handles MyBase.Closing
        SaveCurrentSettings()
        DisposeHotKeys()
        DisposeMediaPlayer()
    End Sub

#End Region

#Region "初期化処理"

    ''' <summary>
    '''     ウィンドウ位置の初期化
    ''' </summary>
    Private Sub InitializeWindowPosition()
        If Left < MinWindowPosition Then
            Left = (Screen.PrimaryScreen.Bounds.Width - Width) \ 2
        End If
        If Top < MinWindowPosition Then
            Top = (Screen.PrimaryScreen.Bounds.Height - Height) \ 2
        End If
    End Sub

    ''' <summary>
    '''     メディアプレイヤーの初期化
    ''' </summary>
    Private Sub InitializeMediaPlayer()
        AllowDrop = True

        ' MpvPanelのウィンドウハンドルが確実に作成されるように強制作成
        ' これをしないとmpvのwid設定が正しく行われない可能性がある

        ' ハンドルの強制確保
        'If Not MpvPanel.IsHandleCreated Then
        '    Dim handle = MpvPanel.Handle
        'End If


        Dim handle = MpvPanel.Handle

        _mediaPlayer = New MpvPlayerWrapper(MpvPanel)

        'イベントの登録
        'MpvPlayer初期化イベント
        AddHandler _mediaPlayer.Initialized, AddressOf OnMpvReady
        'MpvPlayer再生ファイル変更イベント
        AddHandler _mediaPlayer.MediaChanged, AddressOf OnMediaChanged

        _mediaPlayer.Volume = My.Settings.Onryou
        TrackBar6.Value = _mediaPlayer.Volume
        Label5.Text = String.Format(My.Resources.VolumeFormat, _mediaPlayer.Volume)

        ' ファイル未読み込み時はTrackBar1を無効化
        TrackBar1.Enabled = False

        Application.DoEvents()

    End Sub
    ''' <summary>
    '''     MpvPalerの準備ができたときの処理
    ''' </summary>
    Private _mpvReady As Boolean = False

    Private Sub OnMpvReady()
        _mpvReady = True
        Debug.WriteLine("mpvの準備が完了しました。これで動画を読み込めます。")
    End Sub

    ''' <summary>
    '''     メディア変更時の処理
    ''' </summary>
    Private Sub OnMediaChanged()
        ' TrackBar1の最大値をメディアの長さに設定
        Dim dur As Double = _mediaPlayer.Duration
        If dur > 0 Then
            TrackBar1.Enabled = True
            TrackBar1.Maximum = CInt(dur)
            Label1.Text = String.Format(My.Resources.TimeFormat, TimeSpan.FromSeconds(dur).ToString("hh\:mm\:ss"))

            _mediaPlayer.Position = 0

        Else
            TrackBar1.Enabled = False
        End If
        TextBox1.Text = _mediaPlayer.FileName
        TrackBar2.Value = CInt(_mediaPlayer.Speed / SpeedMultiplier)
        Label4.Text = String.Format(My.Resources.SpeedFormat, (TrackBar2.Value * SpeedMultiplier).ToString("0.0"))

        UpdateControllerMinSize()
        'ファイル読込時、自動再生とする（初期化完了を最大10秒待機）
        Dim waitCount As Integer = 0
        While Not _mpvReady AndAlso waitCount < 100
            Threading.Thread.Sleep(100)
            waitCount += 1
        End While
        _mediaPlayer.Play()
    End Sub

    ''' <summary>
    '''     コントローラー部の最小サイズを更新
    ''' </summary>
    Private Sub UpdateControllerMinSize()
        TableLayoutPanel1.MinimumSize = New Size(500, 75)
        SplitContainer3.Panel2MinSize = 75
    End Sub

    ''' <summary>
    '''     ホットキーの初期化
    ''' </summary>
    Private Sub InitializeHotKeys()
        CreateHotKeyAtoms(Me.Handle)

        ' 各種ホットキーを登録
        RegisterAllHotKeys()
    End Sub

    ''' <summary>
    '''     全ホットキーを登録
    ''' </summary>
    Private Sub RegisterAllHotKeys()
        For Each hotkeyType As HotKeyType In [Enum].GetValues(GetType(HotKeyType))
            Dim modifierProp As String = GetSettingModifierProperty(hotkeyType)
            Dim keyProp As String = GetSettingKeyProperty(hotkeyType)

            If String.IsNullOrEmpty(modifierProp) OrElse String.IsNullOrEmpty(keyProp) Then
                Continue For
            End If

            Dim modifierValue = CInt(CallByName(My.Settings, modifierProp, CallType.Get))
            Dim keyValue = CType(CallByName(My.Settings, keyProp, CallType.Get), Keys)

            RegisterSingleHotKey(hotkeyType, modifierValue, keyValue)
        Next
    End Sub

    ''' <summary>
    '''     単一のホットキーを登録
    ''' </summary>
    Private Sub RegisterSingleHotKey(hotkeyType As HotKeyType, modifierSetting As Integer, key As Keys)
        Dim modifier As Integer = GetModifierValue(modifierSetting)
        Dim atomId As Short = HotKeyAtoms(hotkeyType)

        RegisterHotKey(Me.Handle, atomId, modifier, key)
    End Sub

    ''' <summary>
    '''     デフォルト設定の読み込み
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
    '''     ジャンプボタン設定の初期化
    ''' </summary>
    Private Sub InitializeJumpButtonSettings()
        Dim jumpValues As Integer() =
                {1, 3, 5, 10, 15, 30, 60, 180, 300, 600, -1, -3, -5, -10, -15, -30, -60, -180, -300, -600}

        For i = 0 To jumpValues.Length - 1
            CallByName(My.Settings, $"SK{i + 1}", CallType.Set, jumpValues(i))
        Next
    End Sub

    ''' <summary>
    '''     速度コントロールボタン設定の初期化
    ''' </summary>
    Private Sub InitializeSpeedButtonSettings()
        Dim speedValues As Double() = {5, 10, 12, 13, 14, 15, 20}

        For i = 0 To speedValues.Length - 1
            CallByName(My.Settings, $"SC{i + 1}", CallType.Set, speedValues(i))
        Next
    End Sub

    ''' <summary>
    '''     パネル高さの適用
    ''' </summary>
    Private Sub ApplyPanelHeights()
        MpvPanel.Height = My.Settings.p21_height
    End Sub

    ''' <summary>
    '''     UI設定の復元
    ''' </summary>
    Private Sub ApplyUiSettings()
        ' フォームサイズの復元
        If My.Settings.MyClientSize.Width > 0 Then
            Me.ClientSize = My.Settings.MyClientSize
        End If

        ' 動画表示画面の復元
        ' gamen = False のとき動画画面を表示（CheckBox2.Checked = True）
        If My.Settings.gamen = False Then
            ' 動画画面を表示する場合、まずパネルを展開してからサイズを設定
            SplitContainer3.Panel1Collapsed = False
            If My.Settings.SC3_Distance > 0 Then
                SplitContainer3.SplitterDistance = My.Settings.SC3_Distance
            End If
            CheckBoxMpvPamel.Checked = True
        Else
            ' 動画画面を非表示にする場合
            SplitContainer3.Panel1Collapsed = True
            If My.Settings.SC3_Distance > 0 Then
                SplitContainer3.SplitterDistance = My.Settings.SC3_Distance
            End If
            CheckBoxMpvPamel.Checked = False
        End If

        ' しおりパネルの復元
        ' shiori = True のときしおりパネルを表示
        If My.Settings.shiori = True Then
            ' パネルを展開
            SplitContainer1.Panel2Collapsed = False
            ' SC1_Distanceが0の場合、デフォルト値を使用
            If My.Settings.SC1_Distance > 0 Then
                SplitContainer1.SplitterDistance = My.Settings.SC1_Distance
            Else
                SplitContainer1.SplitterDistance = SplitContainer1.Width - 125
            End If
        Else
            SplitContainer1.Panel2Collapsed = True
        End If

        ' プレイリストパネルの復元
        ' PL = True のときプレイリストを表示（CheckBox1.Checked = True）
        ' PL = False のときプレイリストを非表示（CheckBox1.Checked = False）
        If My.Settings.PL = True Then
            ' プレイリストを表示する場合、まずパネルを展開してからサイズを設定
            SplitContainer2.Panel1Collapsed = False
            If My.Settings.SC2_Distance > 0 Then
                SplitContainer2.SplitterDistance = My.Settings.SC2_Distance
            Else
                SplitContainer2.SplitterDistance = SplitContainer2.Width \ 4
            End If
            CheckBoxPlayList.Checked = True
        Else
            SplitContainer2.Panel1Collapsed = True
            CheckBoxPlayList.Checked = False
        End If
    End Sub

#End Region

#Region "終了処理"

    ''' <summary>
    '''     現在の設定を保存
    ''' </summary>
    Private Sub SaveCurrentSettings()
        ' 再生情報の保存
        My.Settings.LastOpenedFile = _mediaPlayer.FilePath
        My.Settings.LastIchi = _mediaPlayer.Position

        ' UI状態の保存
        My.Settings.gamen = Not CheckBoxMpvPamel.Checked
        My.Settings.shiori = Not SplitContainer1.Panel2Collapsed
        My.Settings.PL = CheckBoxPlayList.Checked

        ' SplitterDistanceを常に保存（非表示時も最後の値を保持）
        My.Settings.SC1_Distance = SplitContainer1.SplitterDistance
        My.Settings.SC2_Distance = SplitContainer2.SplitterDistance
        My.Settings.SC3_Distance = SplitContainer3.SplitterDistance

        ' しおりパネルが表示中ならPanel2の実幅を保存
        If Not SplitContainer1.Panel2Collapsed Then
            My.Settings.Shiori_Width = SplitContainer1.Panel2.Width
        End If

        My.Settings.MyClientSize = ClientSize
    End Sub

    ''' <summary>
    '''     ホットキーの解放
    ''' </summary>
    Private Sub DisposeHotKeys()
        HotKeyManager.DisposeHotKeys(Me.Handle)
    End Sub

    ''' <summary>
    '''     メディアプレイヤーの解放
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
    '''     ウィンドウプロシージャ（ホットキー処理用）
    ''' </summary>
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WmHotkey Then
            HandleHotKey(m.WParam.ToInt32())
        End If
        MyBase.WndProc(m)
    End Sub

    ''' <summary>
    '''     ホットキー処理
    ''' </summary>
    Private Sub HandleHotKey(hotkeyId As Integer)
        ' ホットキーIDに対応する処理を実行
        For Each kvp As KeyValuePair(Of HotKeyType, Short) In HotKeyAtoms
            If kvp.Value = hotkeyId Then
                ExecuteHotKeyAction(kvp.Key)
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    '''     ホットキーアクションの実行
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
                AdjustPlaybackSpeed(SpeedMultiplier)
            Case HotKeyType.SpeedDown
                AdjustPlaybackSpeed(-SpeedMultiplier)
            Case HotKeyType.SpeedResetTo1X
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
    '''     再生/一時停止の切り替え
    ''' </summary>
    Private Sub TogglePlayPause()
        If _mediaPlayer.IsPlaying Then
            _mediaPlayer.Pause()
        Else
            _mediaPlayer.Play()
        End If
    End Sub

    ''' <summary>
    '''     再生/一時停止とカウンタコピー
    ''' </summary>
    Private Sub TogglePlayPauseWithCounterCopy(counterIndex As Integer)
        CopyCounterToClipboard(counterIndex)
        TogglePlayPause()
    End Sub

    ''' <summary>
    '''     停止
    ''' </summary>
    Private Sub StopPlayback()
        _mediaPlayer.Stop()
    End Sub

    ''' <summary>
    '''     カウンタをクリップボードにコピー
    ''' </summary>
    Private Sub CopyCounterToClipboard(counterIndex As Integer)
        ' カウンタコピー処理
        Dim timeCode As String = GetCurrentTimeCode()
        Dim prefix As String = GetTimeCodePrefix(counterIndex)
        Dim suffix As String = GetTimeCodeSuffix(counterIndex)

        Clipboard.SetText($"{prefix}{timeCode}{suffix}")
    End Sub

    ''' <summary>
    '''     現在のタイムコードを取得
    ''' </summary>
    Private Function GetCurrentTimeCode() As String
        Dim position As Double = _mediaPlayer.Position
        Dim hours As Integer = CInt(position) \ 3600
        Dim minutes As Integer = (CInt(position) Mod 3600) \ 60
        Dim seconds As Integer = CInt(position) Mod 60
        Dim frames = CInt((position - Math.Floor(position)) * 30)

        Return $"{hours:D2}:{minutes:D2}:{seconds:D2}.{frames:D2}"
    End Function

    ''' <summary>
    '''     タイムコード修飾（頭）を取得
    ''' </summary>
    Private Function GetTimeCodePrefix(index As Integer) As String
        Select Case index
            Case 1 : Return My.Settings.Atama
            Case 2 : Return My.Settings.Atama2
            Case 3 : Return My.Settings.Atama3
            Case Else : Return String.Empty
        End Select
    End Function

    ''' <summary>
    '''     タイムコード修飾（末尾）を取得
    ''' </summary>
    Private Function GetTimeCodeSuffix(index As Integer) As String
        Select Case index
            Case 1 : Return My.Settings.Oshiri
            Case 2 : Return My.Settings.Oshiri2
            Case 3 : Return My.Settings.Oshiri3
            Case Else : Return String.Empty
        End Select
    End Function

    ''' <summary>
    '''     しおりに追加
    ''' </summary>
    Private Sub AddBookmark()
        ' しおり追加処理
    End Sub

    ''' <summary>
    '''     再生/一時停止としおり追加
    ''' </summary>
    Private Sub TogglePlayPauseWithBookmark()
        AddBookmark()
        TogglePlayPause()
    End Sub

    ''' <summary>
    '''     再生速度の調整
    ''' </summary>
    Private Sub AdjustPlaybackSpeed(delta As Double)
        _currentPlaybackSpeed = _mediaPlayer.Speed + delta
        SetPlaybackSpeed(_currentPlaybackSpeed)
    End Sub

    ''' <summary>
    '''     再生速度を設定
    ''' </summary>
    Private Sub SetPlaybackSpeed(speed As Double)
        _currentPlaybackSpeed = speed
        _mediaPlayer.Speed = speed
        Label4.Text = $"x{speed:F1}"
    End Sub

    ''' <summary>
    '''     ウィンドウを最前面に
    ''' </summary>
    Private Sub BringWindowToFront()
        TopMost = Not TopMost
    End Sub

    ''' <summary>
    '''     ジャンプホットキーの実行
    ''' </summary>
    Private Sub ExecuteJumpHotkey(hotkeyType As HotKeyType)
        Dim jumpSeconds = 0

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
    '''     速度コントロールホットキーの実行
    ''' </summary>
    Private Sub ExecuteSpeedControlHotkey(hotkeyType As HotKeyType)
        Dim speed = 1.0

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
    '''     指定位置にジャンプ
    ''' </summary>
    Private Sub JumpToPosition(position As Double)
        _mediaPlayer.Position = Math.Max(0, position)
    End Sub

    ''' <summary>
    '''     クリップボードの位置にジャンプ
    ''' </summary>
    Private Sub JumpToClipboardPosition()
        ' クリップボードから位置を読み込んでジャンプ
    End Sub

#End Region

#Region "UI制御"

    ''' <summary>
    '''     動画表示画面の表示/非表示切り替え
    ''' </summary>
    Private Sub CheckBoxMpvPanel_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxMpvPamel.CheckedChanged
        Dim isShowing As Boolean = CheckBoxMpvPamel.Checked
        Me.SuspendLayout()
        SendMessage(Me.Handle, WM_SETREDRAW, False, IntPtr.Zero)
        If isShowing Then
            ' 表示する場合
            Dim panelHeight As Integer = If(My.Settings.SC3_Distance > 0, My.Settings.SC3_Distance, 300)

            ' まずフォームサイズを拡張（Panel2のサイズを変えないように）
            Me.ClientSize = New Size(Me.ClientSize.Width, Me.ClientSize.Height + panelHeight)

            SplitContainer3.Panel1Collapsed = False
            SplitContainer3.SplitterDistance = panelHeight
        Else
            ' 非表示にする前にパネルの実際のサイズを保存
            Dim actualPanelHeight As Integer = SplitContainer3.Panel1.Height
            My.Settings.SC3_Distance = SplitContainer3.SplitterDistance
            My.Settings.Save()

            ' フォームサイズをパネルの実際の高さ分縮小（Panel2のサイズを変えないように）
            Me.ClientSize = New Size(Me.ClientSize.Width, Math.Max(100, Me.ClientSize.Height - actualPanelHeight))

            SplitContainer3.Panel1Collapsed = True
        End If
        UpdateControllerMinSize()

        Me.ResumeLayout()
        SendMessage(Me.Handle, WM_SETREDRAW, True, IntPtr.Zero)
        Me.Refresh()
    End Sub

    ''' <summary>
    '''     プレイリストの表示/非表示切り替え
    ''' </summary>
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPlayList.CheckedChanged
        Dim isShowing As Boolean = CheckBoxPlayList.Checked

        Me.SuspendLayout()
        SendMessage(Me.Handle, WM_SETREDRAW, False, IntPtr.Zero)
        If isShowing Then
            ' 表示する場合
            Dim panelWidth As Integer = If(My.Settings.SC2_Distance > 0, My.Settings.SC2_Distance, 300)

            SplitContainer2.Panel1Collapsed = False
            SplitContainer2.SplitterDistance = panelWidth
            ' フォームサイズを拡張し、Leftを調整して右端位置を固定（旧Form1.vbと同じ処理）
            Me.Width += panelWidth
            Me.Left -= panelWidth

        Else
            ' 非表示にする前にパネルの実際のサイズを保存
            Dim actualPanelWidth As Integer = SplitContainer2.Panel1.Width
            My.Settings.SC2_Distance = SplitContainer2.SplitterDistance
            My.Settings.Save()

            ' フォームサイズを縮小し、Leftを調整して右端位置を固定（旧Form1.vbと同じ処理）
            SplitContainer2.Panel1Collapsed = True
            Me.Width -= actualPanelWidth
            Me.Left += actualPanelWidth
        End If
        Me.ResumeLayout()
        SendMessage(Me.Handle, WM_SETREDRAW, True, IntPtr.Zero)
        Me.Refresh()
    End Sub

#End Region

#Region "ボタンイベントハンドラ"

    ' 速度ボタン
    Private Sub SpeedButtons_Click(sender As Object, e As EventArgs) _
        Handles Button21.Click, Button22.Click, Button23.Click, Button24.Click, Button25.Click, Button26.Click,
                Button27.Click
        Dim btn = TryCast(sender, Button)
        If btn Is Nothing Then Return

        ' ボタン名から番号を取得（例: "Button21" -> "1"）
        ' Button21 は SC1, Button22 は SC2 ...
        Dim buttonIndex As Integer
        If Integer.TryParse(btn.Name.Replace("Button", ""), buttonIndex) Then
            Dim scIndex = buttonIndex - SpeedButtonOffset
            Dim settingName = "SC" & scIndex

            Try
                'TrackBar2.Value = CInt(CDbl(My.Settings(settingName)) * SpeedMultiplier)
                TrackBar2.Value = My.Settings(settingName)
                UpdateSpeedFromTrackBar()
            Catch ex As Exception
                ' 設定が見つからない場合などは何もしない
            End Try
        End If
    End Sub

    Private Sub UpdateSpeedFromTrackBar()
        Label4.Text = String.Format(My.Resources.SpeedFormat, (TrackBar2.Value * SpeedMultiplier).ToString("0.0"))
        _mediaPlayer.Speed = TrackBar2.Value * SpeedMultiplier
    End Sub

    Private Sub TrackBar2_Scroll(sender As Object, e As EventArgs) Handles TrackBar2.Scroll
        UpdateSpeedFromTrackBar()
    End Sub

    Private Sub Button400_Click(sender As Object, e As EventArgs) Handles Button400.Click
        _mediaPlayer.Pause()
        _mediaPlayer.Position = 0

        Label1.Text = TimeSpan.FromSeconds(_mediaPlayer.Position).ToString("hh\:mm\:ss") &
              My.Resources.TimeSeparator &
              TimeSpan.FromSeconds(_mediaPlayer.Duration).ToString("hh\:mm\:ss")

    End Sub

    Private Sub Button200_Click(sender As Object, e As EventArgs) Handles Button200.Click


        If _mediaPlayer.IsPlaying Then
            _mediaPlayer.Pause()
        Else
            _mediaPlayer.Play()
        End If

    End Sub

    ' ジャンプボタン
    Private Sub JumpButtons_Click(sender As Object, e As EventArgs) _
        Handles Button1.Click, Button11.Click, Button2.Click, Button12.Click, Button3.Click, Button13.Click,
                Button4.Click, Button14.Click, Button5.Click, Button15.Click, Button6.Click, Button16.Click,
                Button7.Click, Button17.Click, Button8.Click, Button18.Click, Button9.Click, Button19.Click,
                Button10.Click, Button20.Click
        Dim btn = TryCast(sender, Button)
        If btn Is Nothing Then Return

        ' ボタン名から番号を取得（例: "Button1" -> "1"）
        Dim buttonNumber = btn.Name.Replace("Button", "")
        ' 対応する設定名（例: "SK1"）
        Dim settingName = "SK" & buttonNumber

        ' 設定値を取得して移動
        Try
            Dim jumpValue = CInt(My.Settings(settingName))
            _mediaPlayer.Position = TrackBar1.Value + jumpValue
        Catch ex As Exception
            ' 設定が見つからない場合などは何もしない
        End Try
    End Sub

    Private Sub TrackBar6_Scroll(sender As Object, e As EventArgs) Handles TrackBar6.Scroll
        _mediaPlayer.Volume = TrackBar6.Value
        My.Settings.Onryou = TrackBar6.Value
        Label5.Text = String.Format(My.Resources.VolumeFormat, TrackBar6.Value)
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        DataGridView1.Rows.Add()
        Dim i As Integer = DataGridView1.Rows.Count - 1
        Dim appPath As String = Assembly.GetExecutingAssembly().Location

        If String.IsNullOrEmpty(My.Settings.autoBMDir) Then
            My.Settings.autoBMDir = Path.GetDirectoryName(appPath)
        End If

        DataGridView1.Rows(i).Cells(0).Value = Strings.Left(Label1.Text, 8)
        DataGridView1.Rows(i).Cells(2).Value = TrackBar1.Value
        DataGridView1.CurrentCell = DataGridView1(0, i)

        If My.Settings.autoBM Then
            WriteCsvFromDgv(TextBox1.Text)
        Else
            WriteCsvFromDgv("om_tmp")
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) _
        Handles DataGridView1.CellContentClick
        Dim colIndex As Integer = e.ColumnIndex

        ' 行インデックスが有効か確認
        If e.RowIndex < 0 OrElse e.RowIndex >= DataGridView1.Rows.Count Then
            Exit Sub
        End If

        If colIndex <> 0 And colIndex <> 3 Then
            Exit Sub
        End If

        Select Case colIndex
            Case 0 ' ジャンプ
                Dim i As Integer = e.RowIndex
                Dim jumpValue As Integer = CInt(DataGridView1.Rows(i).Cells(2).Value)
                ' TrackBar1の範囲内に収める
                TrackBar1.Value = Math.Min(jumpValue, TrackBar1.Maximum)
                ' メディアプレイヤーには実際の値を設定（Durationを超えないように）
                _mediaPlayer.Position = Math.Min(jumpValue, _mediaPlayer.Duration)
                If My.Settings.shiori_PS Then
                    _mediaPlayer.Play()
                Else
                    _mediaPlayer.Play()
                    _mediaPlayer.Pause()
                End If
            Case 3 ' 削除
                Dim i As Integer = e.RowIndex
                DataGridView1.Rows.RemoveAt(i)
        End Select
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        If DataGridView1.RowCount = 0 OrElse DataGridView1.SelectedCells.Count = 0 Then Exit Sub
        Dim i As Integer = DataGridView1.SelectedCells(0).RowIndex
        DataGridView1.Rows.RemoveAt(i)
    End Sub

    Private Function ParseCounterToSeconds(inputText As String, ByRef resultSeconds As Integer,
                                           ByRef formattedCounter As String) As Boolean
        Dim cCounter As String = String.Empty

        ' 数字のみ抽出
        For Each ch As Char In inputText
            If Char.IsDigit(ch) Then
                cCounter &= ch
            End If
        Next

        If cCounter.Length > MaxCounterDigits Then
            MsgBox(My.Resources.DigitsExceeded, vbOKOnly)
            Return False
        End If

        ' 6桁にパディング
        cCounter = cCounter.PadLeft(MaxCounterDigits, "0"c)

        Dim hours = Integer.Parse(cCounter.Substring(0, HourDigits))
        Dim minutes = Integer.Parse(cCounter.Substring(HourDigits, MinuteDigits))
        Dim seconds = Integer.Parse(cCounter.Substring(HourDigits + MinuteDigits, SecondDigits))

        resultSeconds = (hours * 3600) + (minutes * 60) + seconds
        formattedCounter = String.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds)

        ' 動画の長さチェック（実際の再生時間と比較）
        If resultSeconds > _mediaPlayer.Duration Then
            MsgBox(My.Resources.CounterExceedsDuration)
            Return False
        End If

        Return True
    End Function

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        If String.IsNullOrEmpty(TextBox3.Text) Then Exit Sub

        Dim seconds As Integer
        Dim formattedCounter As String = String.Empty

        If ParseCounterToSeconds(TextBox3.Text, seconds, formattedCounter) Then
            DataGridView1.Rows.Add()
            Dim i As Integer = DataGridView1.Rows.Count - 1
            DataGridView1.Rows(i).Cells(0).Value = formattedCounter
            DataGridView1.Rows(i).Cells(2).Value = seconds
            DataGridView1.CurrentCell = DataGridView1(0, i)
        Else
            TextBox3.Clear()
        End If
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        If String.IsNullOrEmpty(TextBox2.Text) Then Exit Sub

        Dim seconds As Integer
        Dim formattedCounter As String = String.Empty

        If ParseCounterToSeconds(TextBox2.Text, seconds, formattedCounter) Then
            TrackBar1.Value = seconds
            _mediaPlayer.Position = TrackBar1.Value
            _mediaPlayer.Play()
            TextBox2.Clear()
        Else
            TextBox2.Clear()
        End If
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        Try
            If WriteCsvFromDgv(TextBox1.Text) = True Then
                MsgBox(My.Resources.ExportComplete, vbOKOnly)
            Else
                MsgBox(My.Resources.ExportFailed, vbOKOnly)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbOKOnly)
        End Try
    End Sub

#Region "ファイル解析ヘルパー"

    ''' <summary>
    '''     タイムスタンプ文字列から秒数を計算
    ''' </summary>
    Private Function ParseTimestampToSeconds(timestamp As String) As Integer
        ' フォーマット: (HH:MM:SS) の10文字
        Return (Integer.Parse(timestamp.Substring(1, 2)) * 3600) +
               (Integer.Parse(timestamp.Substring(4, 2)) * 60) +
               Integer.Parse(timestamp.Substring(7, 2))
    End Function

    ''' <summary>
    '''     DataGridViewに行を追加
    ''' </summary>
    Private Sub AddBookmarkRow(timeDisplay As String, memo As String, seconds As Integer)
        Dim row As String() = {timeDisplay, memo, seconds.ToString(), "削除"}
        DataGridView1.Rows.Add(row)
    End Sub

    ''' <summary>
    '''     テキスト内容を解析してしおりを追加
    ''' </summary>
    Private Sub ParseTextContentForBookmarks(content As String)
        Dim fukaChar As String = My.Settings.Fuka
        Dim fumeiChar As String = My.Settings.Fumei
        Dim fumei2Char As String = My.Settings.Fumei2
        Dim sonotaChar As String = My.Settings.Sonota

        For n = 0 To content.Length - TimestampLength
            Dim currentChar As String = content.Substring(n, 1)

            If currentChar = fukaChar Then
                ' 不可パターン
                ParseFukaPattern(content, n)
            ElseIf currentChar = fumeiChar Then
                ' 不明パターン
                ParseFumeiPattern(content, n, fumei2Char)
            ElseIf currentChar = sonotaChar Then
                ' その他パターン
                ParseSonotaPattern(content, n)
            End If
        Next
    End Sub

    ''' <summary>
    '''     「不可」パターンを解析
    ''' </summary>
    Private Sub ParseFukaPattern(content As String, startIndex As Integer)
        If startIndex + TimestampLength + 1 > content.Length Then Return

        Dim timestamp As String = content.Substring(startIndex + 1, TimestampLength)
        Dim seconds As Integer = ParseTimestampToSeconds(timestamp)
        Dim timeDisplay As String = content.Substring(startIndex + 2, TimeDisplayLength)
        AddBookmarkRow(timeDisplay, "聞き取り不可", seconds)
    End Sub

    ''' <summary>
    '''     「不明」パターンを解析
    ''' </summary>
    Private Sub ParseFumeiPattern(content As String, startIndex As Integer, endMarker As String)
        For i As Integer = startIndex + 1 To content.Length - TimestampLength - 1
            If content.Substring(i, 1) = endMarker Then
                Dim memo As String = content.Substring(startIndex + 1, i - startIndex - 1)
                Dim timestamp As String = content.Substring(i + 1, TimestampLength)
                Dim seconds As Integer = ParseTimestampToSeconds(timestamp)
                Dim timeDisplay As String = content.Substring(i + 2, TimeDisplayLength)
                AddBookmarkRow(timeDisplay, memo & "？", seconds)
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    '''     「その他」パターンを解析
    ''' </summary>
    Private Sub ParseSonotaPattern(content As String, startIndex As Integer)
        For i As Integer = startIndex + 1 To content.Length - TimestampLength - 1
            Dim checkChar As String = content.Substring(i, 1)
            If checkChar = "(" OrElse checkChar = "（" Then
                Dim memo As String = content.Substring(startIndex, i - startIndex)
                Dim timestamp As String = content.Substring(i, TimestampLength)
                Dim seconds As Integer = ParseTimestampToSeconds(timestamp)
                Dim timeDisplay As String = content.Substring(i + 1, TimeDisplayLength)
                AddBookmarkRow(timeDisplay, memo, seconds)
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    '''     Word文書からテキストを抽出
    ''' </summary>
    Private Function ExtractTextFromWord(filePath As String) As String
        Dim objWord As Object = Nothing
        Dim objDoc As Object = Nothing
        Dim extractedText As String = String.Empty

        Try
            objWord = CreateObject("Word.Application")
            objWord.Visible = False
            objDoc = objWord.Documents.Open(filePath)
            objDoc.Range.Copy()
            extractedText = Clipboard.GetText()
            Return extractedText
        Finally
            ' クリップボードのクリーンアップ
            Try
                Clipboard.Clear()
            Catch
                ' クリップボードのクリアに失敗しても無視
            End Try

            ' COMオブジェクトの解放
            If objDoc IsNot Nothing Then
                Try
                    objDoc.Close(False)
                Catch
                End Try
                Marshal.ReleaseComObject(objDoc)
                objDoc = Nothing
            End If
            If objWord IsNot Nothing Then
                Try
                    objWord.Quit()
                Catch
                End Try
                Marshal.ReleaseComObject(objWord)
                objWord = Nothing
            End If
        End Try
    End Function

#End Region

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        If OpenFileDialog1.ShowDialog() <> DialogResult.OK Then
            Return
        End If

        Dim filePath As String = OpenFileDialog1.FileName
        Dim extension As String = Path.GetExtension(filePath).ToLower()

        Select Case extension
            Case ".csv"
                CsvReader()

            Case ".doc", ".docx"
                Try
                    Dim content As String = ExtractTextFromWord(filePath)
                    ParseTextContentForBookmarks(content)
                Catch ex As Exception
                    MsgBox(String.Format(My.Resources.WordFileLoadFailed, ex.Message), vbOKOnly)
                End Try

            Case ".txt"
                Using reader As New StreamReader(filePath, Encoding.GetEncoding("Shift_JIS"))
                    Dim content As String = reader.ReadToEnd()
                    ParseTextContentForBookmarks(content)
                End Using

            Case Else
                MsgBox(My.Resources.FileFormatNotSupported, vbOKOnly)
        End Select
    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        MsgBox(My.Resources.ScreenCaptureNotImplemented, vbOKOnly)
    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        Dim f2 As New SettingsForm()
        ' オーナーを指定せずにモーダル表示（個別ウィンドウとして移動可能）
        f2.ShowDialog()
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        ' ファイルが読み込まれていない場合は処理しない
        If _mediaPlayer.Duration <= 0 Then
            TrackBar1.Value = 0
            Return
        End If

        ToolTip1.SetToolTip(TrackBar1, TimeSpan.FromSeconds(TrackBar1.Value).ToString("hh\:mm\:ss"))
        Label1.Text = TimeSpan.FromSeconds(TrackBar1.Value).ToString("hh\:mm\:ss") & My.Resources.TimeSeparator &
                      TimeSpan.FromSeconds(_mediaPlayer.Duration).ToString("hh\:mm\:ss")
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
        TopMost = Not TopMost
        If TopMost Then
            Button35.Image = My.Resources.AlwaysVisible_16x
        Else
            Button35.Image = My.Resources.PinnedItem_16x
        End If
    End Sub


    Private Sub ButtonSiori_Click(sender As Object, e As EventArgs) Handles ButtonSiori.Click

        Me.SuspendLayout()
        SendMessage(Me.Handle, WM_SETREDRAW, False, IntPtr.Zero)
        If SplitContainer1.Panel2Collapsed Then
            ' しおりパネルを開く
            ' Panel2（しおり）の幅を使ってフォームを拡張する
            Dim panelWidth As Integer
            If My.Settings.Shiori_Width > 0 Then
                panelWidth = CInt(My.Settings.Shiori_Width)
            Else
                ' デフォルトのしおりパネル幅
                panelWidth = 125
            End If

            ' Panel2 + スプリッター幅の合計分だけフォームを拡張
            Dim expandWidth As Integer = panelWidth + SplitContainer1.SplitterWidth
            Me.ClientSize = New Size(Me.ClientSize.Width + expandWidth, Me.ClientSize.Height)

            SplitContainer1.Panel2Collapsed = False
            ' SC1_Distance（Panel1の幅）が保存されていれば復元
            If My.Settings.SC1_Distance > 0 Then
                SplitContainer1.SplitterDistance = CInt(My.Settings.SC1_Distance)
            End If
        Else
            ' しおりパネルを閉じる
            ' 非表示にする前にパネルの実際のサイズを保存（Panel2幅 + スプリッター幅）
            Dim actualPanelWidth As Integer = SplitContainer1.Panel2.Width
            Dim shrinkWidth As Integer = actualPanelWidth + SplitContainer1.SplitterWidth
            My.Settings.Shiori_Width = actualPanelWidth
            My.Settings.SC1_Distance = SplitContainer1.SplitterDistance
            My.Settings.Save()

            ' フォームサイズをPanel2+スプリッター幅分縮小（Panel1のサイズを変えないように）
            Me.ClientSize = New Size(Math.Max(100, Me.ClientSize.Width - shrinkWidth), Me.ClientSize.Height)

            SplitContainer1.Panel2Collapsed = True
        End If
        Me.ResumeLayout()
        SendMessage(Me.Handle, WM_SETREDRAW, True, IntPtr.Zero)
        Me.Refresh()
    End Sub

#End Region

#Region "Timer"

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If _mediaPlayer.IsPlaying Then
            Dim pos = CInt(_mediaPlayer.Position)
            If pos > TrackBar1.Maximum Then
                pos = TrackBar1.Maximum
            End If
            TrackBar1.Value = pos
            Label1.Text = TimeSpan.FromSeconds(_mediaPlayer.Position).ToString("hh\:mm\:ss") &
                          My.Resources.TimeSeparator &
                          TimeSpan.FromSeconds(_mediaPlayer.Duration).ToString("hh\:mm\:ss")
        End If
    End Sub

#End Region

#Region "ドラッグ＆ドロップ"

    ''' <summary>
    '''     ドラッグ＆ドロップでファイルを処理
    ''' </summary>
    Private Sub HandleFileDragDrop(e As DragEventArgs)
        If Not e.Data.GetDataPresent(DataFormats.FileDrop) Then Return

        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        'If files.Length = 0 Then Return

        _mediaPlayer.LoadFile(files(0))

        My.Settings.LastOpenedFile = files(0)

        'System.Threading.Thread.Sleep(5000)

    End Sub

    ''' <summary>
    '''     ドラッグ＆ドロップの効果を設定
    ''' </summary>
    Private Sub HandleFileDragEnter(e As DragEventArgs)
        If Not e.Data.GetDataPresent(DataFormats.FileDrop) Then Return

        If (e.KeyState And CtrlMask) = CtrlMask Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.Move
        End If
    End Sub

    Private Sub MainPlayerForm_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        HandleFileDragDrop(e)
    End Sub

    Private Sub MainPlayerForm_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
        HandleFileDragEnter(e)
    End Sub

#End Region

#Region "CSV入出力"

    ''' <summary>
    '''     CSVファイル読み込み
    ''' </summary>
    Private Sub CsvReader()
        Dim csvFile As String = OpenFileDialog1.FileName

        DataGridView1.Rows.Clear()

        Using sr As New StreamReader(csvFile, Encoding.GetEncoding("shift_jis"))
            ' ヘッダー行をスキップ
            If sr.ReadLine() Is Nothing Then Return

            Dim conStr As String
            Do
                conStr = sr.ReadLine()
                If conStr Is Nothing Then Exit Do
                conStr = Replace(conStr, """", "")
                Dim rowPlus() As String = conStr.Split(",")
                DataGridView1.Rows.Add(rowPlus)
            Loop
        End Using
    End Sub

    ''' <summary>
    '''     DataGridViewからCSVファイルへの書込処理
    ''' </summary>
    Private Function WriteCsvFromDgv(fileName As String) As Boolean
        Try
            Dim arrData()() As String = Nothing
            Dim arrHead As String() = Nothing
            Dim filePath As String

            ' ファイルパスの構築
            If fileName.Contains("\") OrElse fileName.Contains("/") Then
                ' フルパスまたは相対パスが指定された場合
                filePath = fileName
            Else
                ' ファイル名のみの場合、autoBMDirを使用
                If String.IsNullOrEmpty(My.Settings.autoBMDir) Then
                    filePath = Application.StartupPath & "\" & fileName & ".csv"
                Else
                    filePath = My.Settings.autoBMDir & "\" & fileName & ".csv"
                End If
            End If

            For col = 0 To DataGridView1.Columns.Count - 1
                ReDim Preserve arrHead(col)
                arrHead(col) = CStr(DataGridView1.Columns(col).HeaderCell.Value)
            Next
            ReDim Preserve arrData(0)
            arrData(0) = arrHead

            For row = 0 To DataGridView1.Rows.Count - 1
                If DataGridView1.Rows(row).IsNewRow Then
                    Continue For
                End If

                Dim arrLine As String() = Nothing
                For col = 0 To DataGridView1.Columns.Count - 1
                    ReDim Preserve arrLine(col)
                    arrLine(col) = CStr(DataGridView1.Rows(row).Cells(col).Value)
                Next

                ReDim Preserve arrData(row + 1)
                arrData(row + 1) = arrLine
            Next

            Return WriteCsv(filePath, arrData)

        Catch ex As Exception
            MsgBox(ex.Message, vbOKOnly)
            Return False
        End Try
    End Function

    ''' <summary>
    '''     CSVファイルの書込処理
    ''' </summary>
    Private Function WriteCsv(csvPath As String, csvData As String()()) As Boolean
        Dim sw As StreamWriter = Nothing

        Try
            Dim enc As Encoding = Encoding.GetEncoding("Shift_JIS")
            sw = New StreamWriter(csvPath, False, enc)

            For Each arrLine() As String In csvData
                Dim isFirst = True
                For Each str As String In arrLine
                    If Not isFirst Then
                        sw.Write(",")
                    End If
                    isFirst = False
                    sw.Write("""" & str & """")
                Next
                sw.Write(vbCrLf)
            Next

            Return True

        Catch ex As Exception
            MsgBox(ex.Message, vbOKOnly)
            Return False
        Finally
            If sw IsNot Nothing Then
                sw.Close()
            End If
        End Try
    End Function

#End Region

#Region "その他イベントハンドラ"

    ''' <summary>
    '''     TextBox2でEnterキーが押されたときにジャンプ
    ''' </summary>
    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        ' 早期リターン: Enterキー以外は無視
        If e.KeyCode <> Keys.Enter Then Return

        ' 早期リターン: 空欄の場合は無視
        If String.IsNullOrEmpty(TextBox2.Text) Then Return

        Dim seconds As Integer
        Dim formattedCounter As String = String.Empty

        ' ParseCounterToSecondsメソッドでパース処理を共通化
        If Not ParseCounterToSeconds(TextBox2.Text, seconds, formattedCounter) Then
            TextBox2.Clear()
            Return
        End If

        TrackBar1.Value = seconds
        _mediaPlayer.Position = seconds
        _mediaPlayer.Play()
        TextBox2.Clear()
    End Sub

    ''' <summary>
    '''     SplitContainer1のスプリッター移動時
    ''' </summary>
    Private Sub SplitContainer1_SplitterMoved(sender As Object, e As SplitterEventArgs) _
        Handles SplitContainer1.SplitterMoved
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
    '''     SplitContainer3のスプリッター移動時
    ''' </summary>
    Private Sub SplitContainer3_SplitterMoved(sender As Object, e As SplitterEventArgs) _
        Handles SplitContainer3.SplitterMoved
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
    '''     TableLayoutPanel1のセル描画時（着色）
    ''' </summary>
    Private Sub TableLayoutPanel1_CellPaint(sender As Object, e As TableLayoutCellPaintEventArgs) _
        Handles TableLayoutPanel1.CellPaint
        Dim darkBrush2 As New SolidBrush(Color.FromArgb(50, 50, 50))

        ' Row 0
        For col = 0 To 16
            If e.Column = col AndAlso e.Row = 0 Then
                e.Graphics.FillRectangle(darkBrush2, e.CellBounds)
            End If
        Next

        ' Row 1
        For col = 0 To 20
            If e.Column = col AndAlso e.Row = 1 Then
                e.Graphics.FillRectangle(darkBrush2, e.CellBounds)
            End If
        Next

        ' 音量調整部分
        Dim darkBrush As New SolidBrush(Color.FromArgb(64, 64, 64))
        If e.Column = 20 AndAlso (e.Row = 3 OrElse e.Row = 4 OrElse e.Row = 5) Then
            e.Graphics.FillRectangle(darkBrush, e.CellBounds)
        End If

        ' 再生・停止部分
        For col = 0 To 5
            If e.Column = col AndAlso (e.Row = 5 OrElse e.Row = 6) Then
                e.Graphics.FillRectangle(darkBrush, e.CellBounds)
            End If
        Next

        ' 速度調整部分
        If (e.Column = 6 OrElse e.Column >= 8 AndAlso e.Column <= 18) AndAlso e.Row = 5 Then
            e.Graphics.FillRectangle(darkBrush2, e.CellBounds)
        End If
    End Sub

#End Region

#Region "テスト用ヘルパー"

    ''' <summary>
    '''     テスト用にメディアプレイヤーを設定
    ''' </summary>
    Friend Sub SetMediaPlayerForTest(player As MpvPlayerWrapper)
        _mediaPlayer = player
    End Sub

    ''' <summary>
    '''     テスト用にメディアプレイヤーを取得
    ''' </summary>
    Friend Function GetMediaPlayerForTest() As MpvPlayerWrapper
        Return _mediaPlayer
    End Function

    Private Sub MainPlayerForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        TextBox2.Text = Me.Width & "," & Me.Height & "|" & TableLayoutPanel1.Width & "," & TableLayoutPanel1.Height
    End Sub

    Private Sub MainPlayerForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ' フォームが画面にパッと出てから初期化を始める
        'InitializeMediaPlayer()

        ' 初期化の直後に少しだけOSに処理を戻す（おまじない）
        'Application.DoEvents()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Timer2.Stop() ' 1回だけ実行したいので止める

        ' ここで初めて動画を読み込む！
        ' 例: _mediaPlayer.Load("C:\test.mp4")

        Debug.WriteLine("mpvの準備時間を確保しました。再生を開始します。")
    End Sub

    Private Sub MainPlayerForm_MenuStart(sender As Object, e As EventArgs) Handles Me.MenuStart

    End Sub

#End Region
End Class

