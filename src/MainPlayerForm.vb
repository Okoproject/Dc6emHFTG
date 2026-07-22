Imports System.ComponentModel
Imports System.Diagnostics.Eventing.Reader
Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography.X509Certificates
Imports System.Text

''' <summary>
'''     メイン動画プレイヤーフォーム
''' </summary>
Public Class MainPlayerForm

    'BMパネルの幅を保存する変数
    Public BMWidth As Integer
    'PLパネルの幅を保存する変数
    Public PLWidth As Integer
    'フォーム自体の高さを保存する変数
    Public MainHeight As Integer
    'フォーム自体の幅を保存する変数
    Public MainWidth As Integer
    '動画表示パネルの高さを保存する変数
    Public ScrHeight As Integer


    ''' <summary>
    ''' コンストラクタ：ダブルバッファリングとコンポジット描画を有効化
    ''' </summary>
    Public Sub New()
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint, True)
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' WS_EX_COMPOSITED を追加して子コントロールのちらつきを抑制
    ''' </summary>
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000 ' WS_EX_COMPOSITED
            Return cp
        End Get
    End Property

    <DllImport("user32.dll")> Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As Boolean, lParam As IntPtr) As IntPtr
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

    Private _playlistItems As New List(Of PlaylistItem)
    Private _currentPlaylistIndex As Integer = -1

    Private Const ColFileName As Integer = 0
    Private Const ColFileLength As Integer = 1
    Private Const ColFileMemo As Integer = 2
    Private Const ColFileDelete As Integer = 3
    Private Const ColFilePosition As Integer = 4
    Private Const ColFileProgress As Integer = 5

    Private Shared ReadOnly MediaExtensions() As String = {
        ".mp4", ".avi", ".mkv", ".mov", ".wmv", ".flv", ".webm",
        ".mpg", ".mpeg", ".ts", ".m2ts", ".mts", ".ogv", ".3gp", ".m4v",
        ".mp3", ".wav", ".flac", ".ogg", ".m4a", ".aac", ".wma", ".opus"
    }

    ''' <summary>
    '''     メインフォームのインスタンス（シングルトン）
    ''' </summary>
    Public Shared Instance As MainPlayerForm

#End Region

#Region "フォームイベント"

    Private Sub MainPlayerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SendMessage(Me.Handle, WM_SETREDRAW, False, IntPtr.Zero)

        Instance = Me
        InitializeWindowPosition()
        InitializeMediaPlayer()
        InitializeHotKeys()
        LoadDefaultSettings()
        ApplyUiSettings()
        UpdateControllerMinSize()
        UpdateJumpButtonLabels()

        ' プレイリストの復元
        RestorePlaylist()
        ScanPlaylistDurations()

        ' ボタンフォントサイズの初期調整
        AdjustTableLayoutPanelButtonFonts()

        SendMessage(Me.Handle, WM_SETREDRAW, True, IntPtr.Zero)
        Me.Refresh()


    End Sub

    ''' <summary>
    ''' TableLayoutPanel1内のボタンフォントサイズを自動調整
    ''' </summary>
    Private Sub AdjustTableLayoutPanelButtonFonts()
        If TableLayoutPanel1 Is Nothing Then Return

        For Each ctrl As Control In GetAllControls(TableLayoutPanel1)
            If TypeOf ctrl Is Button Then
                Dim btn = DirectCast(ctrl, Button)
                ' ボタンの高さから適切なフォントサイズを計算（パディング考慮）
                Dim availableHeight = btn.Height - btn.Margin.Vertical - 4
                Dim fontSize = CSng(Math.Max(7, Math.Min(14, availableHeight * 0.45)))
                btn.Font = New Font(btn.Font.FontFamily, fontSize, btn.Font.Style)
            End If
        Next
    End Sub

    ''' <summary>
    ''' コントロールツリーから全ての子コントロールを取得
    ''' </summary>
    Private Iterator Function GetAllControls(parent As Control) As IEnumerable(Of Control)
        For Each ctrl As Control In parent.Controls
            Yield ctrl
            For Each child In GetAllControls(ctrl)
                Yield child
            Next
        Next
    End Function

    ''' <summary>
    ''' TableLayoutPanel1リサイズ時の処理
    ''' </summary>
    Private Sub TableLayoutPanel1_Resize(sender As Object, e As EventArgs) Handles TableLayoutPanel1.Resize
        AdjustTableLayoutPanelButtonFonts()
    End Sub

    Private Sub MainPlayerForm_Closing(sender As Object, e As CancelEventArgs) Handles MyBase.Closing
        SavePlaylist()
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
        AddHandler _mediaPlayer.PlaybackEnded, AddressOf OnPlaybackEnded

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
    Private _suppressAutoPlay As Boolean = False

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

            ' プレイリスト項目のDurationを更新
            If _currentPlaylistIndex >= 0 AndAlso _currentPlaylistIndex < _playlistItems.Count Then
                _playlistItems(_currentPlaylistIndex).Duration = dur
                DataGridView2.Rows(_currentPlaylistIndex).Cells(ColFileLength).Value = TimeSpan.FromSeconds(dur).ToString("hh\:mm\:ss")
            End If

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
        '_mediaPlayer.Play()

        AutoPlayHandan()
    End Sub

    Private Sub AutoPlayHandan()
        If _suppressAutoPlay Then
            _suppressAutoPlay = False
            Return
        End If
        If My.Settings.AutoPlay = True Then
            _mediaPlayer.Play()
            Button200.Image = My.Resources.Pause_16x
        Else
            _mediaPlayer.Pause()
            Button200.Image = My.Resources.Run_16x
        End If
    End Sub

    ''' <summary>
    '''     コントローラー部の最小サイズを更新
    ''' </summary>
    Private Sub UpdateControllerMinSize()
        'TableLayoutPanel1.MinimumSize = New Size(500, 75)
        'TableLayoutPanel1.MinimumSize = New Size(514, 194)
        'SplitContainer3.Panel2MinSize = 75
        'SplitContainer3.SplitterDistance = Me.Height - 194
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
    '''     UI設定の復元
    ''' </summary>
    Private Sub ApplyUiSettings()
        ' ベースサイズの計算（保存時ClientSizeから、保存されていたパネル分を減算）
        Dim baseWidth As Integer = My.Settings.MyClientSize.Width
        Dim baseHeight As Integer = My.Settings.MyClientSize.Height

        If My.Settings.PL = True AndAlso My.Settings.PL_Width > 0 Then
            baseWidth -= My.Settings.PL_Width
        End If
        If My.Settings.shiori = True AndAlso My.Settings.Shiori_Width > 0 Then
            baseWidth -= My.Settings.Shiori_Width + SplitContainer1.SplitterWidth
        End If
        If My.Settings.gamen = True AndAlso My.Settings.Gamen_Height > 0 Then
            baseHeight -= My.Settings.Gamen_Height
        End If

        ' 最小サイズチェック
        baseWidth = Math.Max(baseWidth, MinimumSize.Width)
        baseHeight = Math.Max(baseHeight, MinimumSize.Height)

        Me.ClientSize = New Size(baseWidth, baseHeight)

        ' 動画表示画面の復元（上に飛び出し）
        If My.Settings.gamen = True Then
            Dim panelHeight As Integer = If(My.Settings.Gamen_Height > 0, My.Settings.Gamen_Height, 300)
            Me.Height += panelHeight
            Me.Top -= panelHeight
            SplitContainer3.Panel1Collapsed = False
            SplitContainer3.SplitterDistance = panelHeight
            CheckBoxMpvPamel.Checked = True
        Else
            SplitContainer3.Panel1Collapsed = True
            CheckBoxMpvPamel.Checked = False
        End If

        ' しおりパネルの復元（右に飛び出し）
        If My.Settings.shiori = True Then
            Dim panelWidth As Integer = If(My.Settings.Shiori_Width > 0, My.Settings.Shiori_Width, 250)
            Me.Width += panelWidth + SplitContainer1.SplitterWidth
            SplitContainer1.Panel2Collapsed = False
            SplitContainer1.SplitterDistance = Me.ClientSize.Width - panelWidth - SplitContainer1.SplitterWidth
        Else
            SplitContainer1.Panel2Collapsed = True
        End If

        ' プレイリストパネルの復元（左に飛び出し）
        If My.Settings.PL = True Then
            Dim panelWidth As Integer = If(My.Settings.PL_Width > 0, My.Settings.PL_Width, 300)
            Me.Left -= panelWidth
            Me.Width += panelWidth
            SplitContainer2.Panel1Collapsed = False
            SplitContainer2.SplitterDistance = panelWidth
            Button40.Text = "PL >"
            Button40.ForeColor = Color.Green
        Else
            SplitContainer2.Panel1Collapsed = True
            Button40.Text = "< PL"
            Button40.ForeColor = Color.Black
        End If

    End Sub

    ''' <summary>
    '''     ジャンプボタンのラベルを設定値で更新
    ''' </summary>
    Private Sub UpdateJumpButtonLabels()
        For i As Integer = 1 To 20
            Dim btn As Button = TryCast(Me.Controls.Find("Button" & i, True).FirstOrDefault(), Button)
            If btn IsNot Nothing Then
                Dim value As Integer = CInt(My.Settings("SK" & i))
                btn.Text = FormatJumpValue(value)
            End If
        Next
    End Sub

    ''' <summary>
    '''     ジャンプ値を表示用文字列に変換（例: 60 → "+1M", -5 → "-5S"）
    ''' </summary>
    Private Function FormatJumpValue(seconds As Integer) As String
        Dim sign As String = If(seconds >= 0, "+", "")
        If Math.Abs(seconds) >= 60 Then
            Return sign & (seconds \ 60) & "M"
        Else
            Return sign & seconds & "S"
        End If
    End Function

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
        '動画再生パネルが表示されているかどうか
        My.Settings.gamen = CheckBoxMpvPamel.Checked
        'しおりパネルが表示されているかどうか
        My.Settings.shiori = Not SplitContainer1.Panel2Collapsed
        'プレイリストパネルが表示されているかどうか
        My.Settings.PL = Not SplitContainer2.Panel1Collapsed

        'BMパネルの幅を保存
        My.Settings.Shiori_Width = BMWidth
        'PLパネルの幅を保存
        My.Settings.PL_Width = PLWidth
        '動画表示パネルの高さを保存
        My.Settings.Gamen_Height = ScrHeight
        'フォームのサイズを保存（コアサイズ＝パネル拡張分を除いたサイズ）
        Dim coreWidth As Integer = ClientSize.Width
        Dim coreHeight As Integer = ClientSize.Height
        If Not SplitContainer2.Panel1Collapsed Then coreWidth -= SplitContainer2.Panel1.Width
        If Not SplitContainer1.Panel2Collapsed Then coreWidth -= SplitContainer1.Panel2.Width + SplitContainer1.SplitterWidth
        If Not SplitContainer3.Panel1Collapsed Then coreHeight -= SplitContainer3.Panel1.Height
        My.Settings.MyClientSize = New Size(coreWidth, coreHeight)

        ' 設定をディスクに保存
        My.Settings.Save()
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
            If My.Settings.AutoBack <> 0 Then
                _mediaPlayer.Position -= My.Settings.AutoBack / 100
            End If
            Button200.Image = My.Resources.Run_16x
        Else
            _mediaPlayer.Play()
            Button200.Image = My.Resources.Pause_16x
        End If

        Label1.Text = TimeSpan.FromSeconds(_mediaPlayer.Position).ToString("hh\:mm\:ss") &
              My.Resources.TimeSeparator &
              TimeSpan.FromSeconds(_mediaPlayer.Duration).ToString("hh\:mm\:ss")
        TrackBar1.Value = _mediaPlayer.Position

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
    '''     動画表示画面の表示/非表示切り替え（上に飛び出し）
    ''' </summary>
    Private Sub CheckBoxMpvPanel_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxMpvPamel.CheckedChanged
        Dim isShowing As Boolean = CheckBoxMpvPamel.Checked
        SendMessage(Me.Handle, WM_SETREDRAW, False, IntPtr.Zero)
        If isShowing Then
            Dim panelHeight As Integer = If(ScrHeight > 0, ScrHeight, 300)
            ' フォームを上に拡張（Topを減らし、Heightを増やす）
            Me.Top -= panelHeight
            Me.Height += panelHeight
            SplitContainer3.Panel1Collapsed = False
            SplitContainer3.SplitterDistance = panelHeight
        Else
            ' 非表示：高さを保存してフォームを上に縮小
            ScrHeight = SplitContainer3.Panel1.Height
            My.Settings.Gamen_Height = ScrHeight
            My.Settings.Save()
            Me.Top += SplitContainer3.Panel1.Height
            Me.Height -= SplitContainer3.Panel1.Height
            SplitContainer3.Panel1Collapsed = True
        End If
        UpdateControllerMinSize()
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
        Button200.Image = My.Resources.Run_16x

        Label1.Text = TimeSpan.FromSeconds(_mediaPlayer.Position).ToString("hh\:mm\:ss") &
              My.Resources.TimeSeparator &
              TimeSpan.FromSeconds(_mediaPlayer.Duration).ToString("hh\:mm\:ss")

        TrackBar1.Value = 0

    End Sub

    Private Sub Button200_Click(sender As Object, e As EventArgs) Handles Button200.Click


        If _mediaPlayer.IsPlaying Then
            _mediaPlayer.Pause()
            Button200.Image = My.Resources.Run_16x
        Else
            _mediaPlayer.Play()
            Button200.Image = My.Resources.Pause_16x
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
        'Try
        'Dim jumpValue = CInt(My.Settings(settingName))
        'TrackBar1.Value += jumpValue
        '_mediaPlayer.Position = TrackBar1.Value

        'ToolTip1.SetToolTip(TrackBar1, TimeSpan.FromSeconds(TrackBar1.Value).ToString("hh\:mm\:ss"))
        'Label1.Text = TimeSpan.FromSeconds(TrackBar1.Value).ToString("hh\:mm\:ss") & My.Resources.TimeSeparator &
        'TimeSpan.FromSeconds(_mediaPlayer.Duration).ToString("hh\:mm\:ss")


        'Catch ex As Exception
        ' 設定が見つからない場合などは何もしない
        '再生位置がファイルの長さを超える場合と0以下の場合も例外が発生するため、そちらもキャッチする
        'End Try

        Dim JumpValue2 = CInt(My.Settings(settingName))
        Select Case TrackBar1.Value + JumpValue2

            'ファイルの長さ以上にジャンプしようとした場合はファイルの長さに移動
            Case Is > TrackBar1.Maximum

                TrackBar1.Value = TrackBar1.Maximum
                _mediaPlayer.Position = _mediaPlayer.Duration

            '0以下にジャンプしようとした場合は0に移動
            Case Is < TrackBar1.Minimum

                TrackBar1.Value = TrackBar1.Minimum
                _mediaPlayer.Position = 0


                'それ以外は通常通りジャンプ
            Case Else

                TrackBar1.Value += JumpValue2
                _mediaPlayer.Position = TrackBar1.Value

        End Select

        ToolTip1.SetToolTip(TrackBar1, TimeSpan.FromSeconds(TrackBar1.Value).ToString("hh\:mm\:ss"))
        Label1.Text = TimeSpan.FromSeconds(TrackBar1.Value).ToString("hh\:mm\:ss") & My.Resources.TimeSeparator &
                      TimeSpan.FromSeconds(_mediaPlayer.Duration).ToString("hh\:mm\:ss")



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
        Finally
            ' クリップボードのクリーンアップ
            Try
                Clipboard.Clear()
            Catch
            End Try

            ' COMオブジェクトの解放（ReleaseComObject → Quit の順で実行）
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

            ' 強制ガベージコレクションでCOMプロキシを確実に解放
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try

        Return extractedText
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
        If _mediaPlayer Is Nothing OrElse _mediaPlayer.IsDisposed Then
            MsgBox(My.Resources.NoMediaLoaded, vbOKOnly)
            Return
        End If
        Dim savedPath = _mediaPlayer.TakeScreenshot()
        If Not String.IsNullOrEmpty(savedPath) Then
            MsgBox(String.Format(My.Resources.ScreenCaptureSaved, savedPath), vbOKOnly)
        Else
            MsgBox(My.Resources.ScreenCaptureFailed, vbOKOnly)
        End If
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

    'ファイルを開くボタン
    Private Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            _mediaPlayer.LoadFile(OpenFileDialog1.FileName)
            My.Settings.LastOpenedFile = OpenFileDialog1.FileName
            DataGridView1.Rows.Clear()
        End If
    End Sub

    '常に手前に表示の切り替え
    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        TopMost = Not TopMost
        If TopMost Then
            Button35.Image = My.Resources.AlwaysVisible_16x
        Else
            Button35.Image = My.Resources.PinnedItem_16x
        End If
    End Sub

    'しおりパネルの表示/非表示切り替え（右側に飛び出し）
    Private Sub ButtonShiori_Click(sender As Object, e As EventArgs) Handles ButtonShiori.Click

        SendMessage(Me.Handle, WM_SETREDRAW, False, IntPtr.Zero)
        If SplitContainer1.Panel2Collapsed Then
            ' 表示：フォーム幅を右に拡張
            Dim panelWidth As Integer = If(BMWidth > 0, BMWidth, 250)
            Me.Width += panelWidth + SplitContainer1.SplitterWidth
            SplitContainer1.Panel2Collapsed = False
            SplitContainer1.SplitterDistance = Me.ClientSize.Width - panelWidth - SplitContainer1.SplitterWidth
        Else
            ' 非表示：幅を保存してフォーム幅を縮小
            BMWidth = SplitContainer1.Panel2.Width
            Me.Width -= SplitContainer1.Panel2.Width + SplitContainer1.SplitterWidth
            SplitContainer1.Panel2Collapsed = True
        End If
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
            ToolTip1.SetToolTip(TrackBar1, TimeSpan.FromSeconds(TrackBar1.Value).ToString("hh\:mm\:ss"))

            ' プレイリストの再生位置・進捗を更新
            If _currentPlaylistIndex >= 0 AndAlso _currentPlaylistIndex < _playlistItems.Count Then
                Dim item = _playlistItems(_currentPlaylistIndex)
                item.Position = _mediaPlayer.Position
                Dim row = DataGridView2.Rows(_currentPlaylistIndex)
                row.Cells(ColFilePosition).Value = TimeSpan.FromSeconds(item.Position).ToString("hh\:mm\:ss")
                Dim progress = If(item.Duration > 0, CInt((item.Position / item.Duration) * 100) & "%", "0%")
                row.Cells(ColFileProgress).Value = progress
            End If
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
        If files Is Nothing OrElse files.Length = 0 Then Return

        ' 元のコードと同じ直接再生（確実に動作させるため）
        _mediaPlayer.LoadFile(files(0))
        My.Settings.LastOpenedFile = files(0)

        ' プレイリストに追加
        _currentPlaylistIndex = 0
        _playlistItems.Clear()
        DataGridView2.Rows.Clear()
        For Each f In files
            _playlistItems.Add(New PlaylistItem(f))
            AddPlaylistRow(_playlistItems.Last())
        Next
        ScanPlaylistDurations()
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

        BMWidth = SplitContainer1.Panel2.Width

    End Sub

    ''' <summary>
    '''     SplitContainer3のスプリッター移動時
    ''' </summary>
    Private Sub SplitContainer3_SplitterMoved(sender As Object, e As SplitterEventArgs) _
        Handles SplitContainer3.SplitterMoved

        ScrHeight = SplitContainer3.Panel1.Height

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
        BMWidth = SplitContainer1.Panel2.Width
    End Sub

    Private Sub MainPlayerForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ' プレイリストの最初のファイルを読み込む（自動再生は抑制）
        If _playlistItems.Count > 0 Then
            _suppressAutoPlay = True
            _currentPlaylistIndex = 0
            _mediaPlayer.LoadFile(_playlistItems(0).FilePath)
        End If
    End Sub

    'PlayListの表示・非表示切替
    Private Sub Button40_Click(sender As Object, e As EventArgs) Handles Button40.Click

        SendMessage(Me.Handle, WM_SETREDRAW, False, IntPtr.Zero)
        If SplitContainer2.Panel1Collapsed = True Then
            ' 表示する場合
            Dim panelWidth As Integer = If(PLWidth > 0, PLWidth, 300)

            ' FixedPanelを先に設定（Panel2の幅を固定）
            SplitContainer2.FixedPanel = FixedPanel.Panel2

            ' フォーム幅を拡張（左方向に伸ばす）
            Me.Left -= panelWidth
            Me.Width += panelWidth

            ' Panel1を表示
            SplitContainer2.Panel1Collapsed = False
            SplitContainer2.SplitterDistance = panelWidth

            Button40.Text = "PL >"
            Button40.ForeColor = Color.Green

        Else
            ' 非表示にする場合
            Dim actualPanelWidth As Integer = SplitContainer2.Panel1.Width

            ' PlayListの幅を保存
            PLWidth = actualPanelWidth

            ' フォーム幅を縮小（右方向に縮める）
            Me.Left += actualPanelWidth
            Me.Width -= actualPanelWidth

            ' Panel1を非表示
            SplitContainer2.Panel1Collapsed = True

            Button40.Text = "< PL"
            Button40.ForeColor = Color.Black

        End If
        SendMessage(Me.Handle, WM_SETREDRAW, True, IntPtr.Zero)
        Me.Refresh()

    End Sub

    Private Sub MainPlayerForm_ResizeBegin(sender As Object, e As EventArgs) Handles Me.ResizeBegin
        BMWidth = SplitContainer1.Panel2.Width
        PLWidth = SplitContainer2.Panel1.Width
        ScrHeight = SplitContainer3.Panel1.Height
    End Sub

    Private Sub MainPlayerForm_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        BMWidth = SplitContainer1.Panel2.Width
        PLWidth = SplitContainer2.Panel1.Width
        ScrHeight = SplitContainer3.Panel1.Height
    End Sub

    Private Sub SplitContainer2_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer2.SplitterMoved
        PLWidth = SplitContainer2.Panel1.Width
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 OrElse e.RowIndex >= _playlistItems.Count Then Return

        If e.ColumnIndex = ColFileDelete Then
            RemovePlaylistItem(e.RowIndex)
            Return
        End If

        If e.ColumnIndex <> ColFileMemo Then
            PlayPlaylistItem(e.RowIndex)
        End If
    End Sub

    Private Sub DataGridView2_RowHeaderMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        If e.RowIndex >= 0 AndAlso e.RowIndex < _playlistItems.Count Then
            PlayPlaylistItem(e.RowIndex)
        End If
    End Sub

    Private Sub DataGridView2_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 OrElse e.RowIndex >= _playlistItems.Count Then Return
        If e.ColumnIndex = ColFileMemo Then
            Dim value = DataGridView2.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            _playlistItems(e.RowIndex).Memo = If(value IsNot Nothing, value.ToString(), "")
        End If
    End Sub

#End Region

#Region "プレイリスト"

    Private Sub AddPlaylistRow(item As PlaylistItem)
        Dim row As New DataGridViewRow()
        row.CreateCells(DataGridView2)
        row.Cells(0).Value = If(item.FileName, "")
        row.Cells(1).Value = "00:00:00"
        row.Cells(2).Value = If(item.Memo, "")
        row.Cells(3).Value = "削除"
        row.Cells(4).Value = ""
        row.Cells(5).Value = "0%"
        DataGridView2.Rows.Add(row)
    End Sub

    Private Sub RemovePlaylistItem(index As Integer)
        If index < 0 OrElse index >= _playlistItems.Count Then Return
        _playlistItems.RemoveAt(index)
        DataGridView2.Rows.RemoveAt(index)
        If index = _currentPlaylistIndex Then
            _currentPlaylistIndex = -1
        ElseIf index < _currentPlaylistIndex Then
            _currentPlaylistIndex -= 1
        End If
    End Sub

    Private Sub PlayPlaylistItem(index As Integer)
        If index < 0 OrElse index >= _playlistItems.Count Then Return
        _currentPlaylistIndex = index
        _mediaPlayer.LoadFile(_playlistItems(index).FilePath)
        DataGridView2.ClearSelection()
        DataGridView2.Rows(index).Selected = True
    End Sub

    Private Sub OnPlaybackEnded()
        Dim nextIndex = _currentPlaylistIndex + 1
        If nextIndex >= 0 AndAlso nextIndex < _playlistItems.Count Then
            PlayPlaylistItem(nextIndex)
        Else
            _currentPlaylistIndex = -1
        End If
    End Sub

    Private Sub Button41_Click(sender As Object, e As EventArgs) Handles Button41.Click
        Using fbd As New FolderBrowserDialog()
            fbd.Description = "メディアファイルがあるフォルダを選択"
            If fbd.ShowDialog() = DialogResult.OK Then
                Dim dir = fbd.SelectedPath
                Dim files = IO.Directory.GetFiles(dir)
                Dim addedCount = 0
                Dim firstIndex = _playlistItems.Count
                For Each f In files
                    If IsMediaFile(f) Then
                        Dim newItem As New PlaylistItem(f)
                        _playlistItems.Add(newItem)
                        AddPlaylistRow(newItem)
                        addedCount += 1
                    End If
                Next
                If addedCount = 0 Then
                    MsgBox("メディアファイルが見つかりませんでした。")
                End If
                ScanPlaylistDurations()
                If addedCount > 0 Then
                    _suppressAutoPlay = True
                    _currentPlaylistIndex = firstIndex
                    _mediaPlayer.LoadFile(_playlistItems(firstIndex).FilePath)
                End If
            End If
        End Using
    End Sub

    Private Sub Button42_Click(sender As Object, e As EventArgs) Handles Button42.Click
        If _playlistItems.Count = 0 Then Return
        Dim nextIndex = _currentPlaylistIndex + 1
        If nextIndex >= _playlistItems.Count Then nextIndex = 0
        PlayPlaylistItem(nextIndex)
    End Sub

    Private Sub Button43_Click(sender As Object, e As EventArgs) Handles Button43.Click
        If DataGridView2.SelectedCells.Count = 0 Then Return
        Dim rowIndex = DataGridView2.SelectedCells(0).RowIndex
        If rowIndex >= 0 AndAlso rowIndex < _playlistItems.Count Then
            RemovePlaylistItem(rowIndex)
        End If
    End Sub

    Private Sub Button44_Click(sender As Object, e As EventArgs) Handles Button44.Click
        Using ofd As New OpenFileDialog()
            ofd.Multiselect = False
            ofd.Title = "プレイリストに追加するファイルを選択"
            ofd.Filter = "メディアファイル|*.mp4;*.avi;*.mkv;*.mov;*.wmv;*.flv;*.webm;*.mpg;*.mpeg;*.ts;*.m2ts;*.mts;*.ogv;*.3gp;*.m4v;*.mp3;*.wav;*.flac;*.ogg;*.m4a;*.aac;*.wma;*.opus|すべてのファイル|*.*"
            If ofd.ShowDialog() = DialogResult.OK Then
                Dim newItem As New PlaylistItem(ofd.FileName)
                _playlistItems.Add(newItem)
                AddPlaylistRow(newItem)
                ScanPlaylistDurations()
            End If
        End Using
    End Sub

    Private Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        If _playlistItems.Count = 0 Then
            MsgBox("プレイリストが空です。")
            Return
        End If
        Using sfd As New SaveFileDialog()
            sfd.Title = "プレイリストを保存"
            sfd.Filter = "M3U8 プレイリスト|*.m3u8|すべてのファイル|*.*"
            sfd.DefaultExt = "m3u8"
            sfd.FileName = "playlist.m3u8"
            If sfd.ShowDialog() = DialogResult.OK Then
                SavePlaylist(sfd.FileName)
            End If
        End Using
    End Sub

    Private Sub Button46_Click(sender As Object, e As EventArgs) Handles Button46.Click
        Using ofd As New OpenFileDialog()
            ofd.Title = "プレイリストを読み込む"
            ofd.Filter = "M3U8 プレイリスト|*.m3u8|すべてのファイル|*.*"
            If ofd.ShowDialog() = DialogResult.OK Then
                LoadPlaylist(ofd.FileName)
                ScanPlaylistDurations()
            End If
        End Using
    End Sub

    Private Shared Function IsMediaFile(path As String) As Boolean
        Dim ext = IO.Path.GetExtension(path)
        If String.IsNullOrEmpty(ext) Then Return False
        Return Array.IndexOf(MediaExtensions, ext.ToLowerInvariant()) >= 0
    End Function

    Private Shared Function GetDefaultPlaylistDir() As String
        Return IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OkoshiMAX")
    End Function

    Private Shared Function GetDefaultPlaylistPath() As String
        Return IO.Path.Combine(GetDefaultPlaylistDir(), "last_playlist.m3u8")
    End Function

    Private Sub SavePlaylist()
        If _playlistItems.Count = 0 Then
            Dim path = GetDefaultPlaylistPath()
            If IO.File.Exists(path) Then IO.File.Delete(path)
            Return
        End If
        Dim dir = GetDefaultPlaylistDir()
        If Not IO.Directory.Exists(dir) Then IO.Directory.CreateDirectory(dir)
        SavePlaylist(GetDefaultPlaylistPath())
    End Sub

    Private Sub SavePlaylist(filePath As String)
        Using sw As New IO.StreamWriter(filePath, False, System.Text.Encoding.UTF8)
            sw.WriteLine("#EXTM3U")
            For Each item In _playlistItems
                sw.WriteLine("#EXTINF:" & item.Duration & "," & item.FileName)
                If Not String.IsNullOrEmpty(item.Memo) Then
                    sw.WriteLine("#OKM-MEMO:" & item.Memo)
                End If
                If item.Position > 0 Then
                    sw.WriteLine("#OKM-POS:" & item.Position)
                End If
                sw.WriteLine(item.FilePath)
            Next
        End Using
    End Sub

    Private Sub RestorePlaylist()
        Dim path = GetDefaultPlaylistPath()
        If IO.File.Exists(path) Then
            LoadPlaylist(path)
        End If
    End Sub

    Private Sub LoadPlaylist(filePath As String)
        _playlistItems.Clear()
        DataGridView2.Rows.Clear()

        Using sr As New IO.StreamReader(filePath, System.Text.Encoding.UTF8)
            Dim currentItem As PlaylistItem = Nothing
            Do
                Dim line = sr.ReadLine()
                If line Is Nothing Then Exit Do
                If String.IsNullOrWhiteSpace(line) Then Continue Do
                If line.StartsWith("#EXTM3U") Then Continue Do
                If line.StartsWith("#EXTINF:") Then
                    currentItem = New PlaylistItem()
                    Dim dataPart = line.Substring(8)
                    Dim commaPos = dataPart.IndexOf(","c)
                    If commaPos > 0 Then
                        Double.TryParse(dataPart.Substring(0, commaPos), currentItem.Duration)
                    End If
                    Continue Do
                End If
                If line.StartsWith("#OKM-MEMO:") AndAlso currentItem IsNot Nothing Then
                    currentItem.Memo = line.Substring(10)
                    Continue Do
                End If
                If line.StartsWith("#OKM-POS:") AndAlso currentItem IsNot Nothing Then
                    Double.TryParse(line.Substring(9), currentItem.Position)
                    Continue Do
                End If
                If line.StartsWith("#") Then Continue Do
                ' ファイルパス行
                If currentItem IsNot Nothing Then
                    currentItem.FilePath = line
                Else
                    currentItem = New PlaylistItem(line)
                End If
                _playlistItems.Add(currentItem)
                AddPlaylistRow(currentItem)
                currentItem = Nothing
            Loop
        End Using
    End Sub

    ''' <summary>
    '''     mpv を使ってファイルの長さを取得する（vo=null で画面表示なし）
    ''' </summary>
    Private Shared Function GetFileDuration(path As String) As Double
        Return MpvPlayerWrapper.GetFileDuration(path)
    End Function

    ''' <summary>
    '''     プレイリストの全項目の長さを事前取得する
    ''' </summary>
    Private Sub ScanPlaylistDurations()
        For i As Integer = 0 To _playlistItems.Count - 1
            If _playlistItems(i).Duration <= 0 Then
                Dim dur = GetFileDuration(_playlistItems(i).FilePath)
                If dur > 0 Then
                    _playlistItems(i).Duration = dur
                    DataGridView2.Rows(i).Cells(ColFileLength).Value = TimeSpan.FromSeconds(dur).ToString("hh\:mm\:ss")
                End If
            End If
        Next
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        MsgBox("autoBM=" & My.Settings.autoBM & vbCr _
               & "PL=" & My.Settings.PL & vbCr _
               & "PL_Width=" & My.Settings.PL_Width & vbCr _
               & "Gamen_Height=" & My.Settings.Gamen_Height & vbCr _
               & "Shiori_Width=" & My.Settings.Shiori_Width & vbCr _
               & "Main_Height=" & My.Settings.Main_Height & vbCr _
               & "Main_Width=" & My.Settings.Main_Width & vbCr _
               & "AutoPlay=" & My.Settings.AutoPlay & vbCr _
               & "BM_ap=" & My.Settings.BM_ap & vbCr _
               & "shokai=" & My.Settings.shokai & vbCr _
               & "gamen=" & My.Settings.gamen & vbCr _
               & "shiori=" & My.Settings.shiori & vbCr _
               & "AutoBack=" & My.Settings.AutoBack & vbCr _
               & "LastOpenedFile=" & My.Settings.LastOpenedFile & vbCr _
               & "LastIchi=" & My.Settings.LastIchi)
    End Sub

#End Region
End Class

