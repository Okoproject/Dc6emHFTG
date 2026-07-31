Imports System.ComponentModel
Imports System.Diagnostics.Eventing.Reader
Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography.X509Certificates
Imports System.Windows.Forms

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
    ' しおりパネル表示時にフォーム幅を広げた量（非表示時に同じ量だけ戻すため）
    Private _shioriWidthDelta As Integer = 0
    ' プレイリストパネル表示時にフォーム幅を広げた量（非表示時に同じ量だけ戻すため）
    Private _playlistWidthDelta As Integer = 0

    ''' <summary>
    '''     カスタムタイトルバーの高さ
    ''' </summary>
    Private Const CustomTitleBarHeight As Integer = 28

    ''' <summary>
    '''     リサイズボーダーの幅
    ''' </summary>
    Private Const ResizeBorderWidth As Integer = 25

    ''' <summary>
    '''     ウィンドウ状態の保存用
    ''' </summary>
    Private _previousWindowState As FormWindowState = FormWindowState.Normal
    Private _previousBounds As Rectangle

    ''' <summary>
    '''     コンストラクタ：ダブルバッファリングとコンポジット描画を有効化
    ''' </summary>
    Public Sub New()
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint, True)
        Me.FormBorderStyle = FormBorderStyle.None
        Me.Padding = New Padding(0, CustomTitleBarHeight, 0, 0)
        InitializeComponent()
    End Sub

#Region "Win32 API (カスタムタイトルバー用)"
    <DllImport("user32.dll")>
    Private Shared Function ReleaseCapture() As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function SendMessage(hWnd As IntPtr, Msg As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetWindowRect(hWnd As IntPtr, ByRef lpRect As RECT) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function SetWindowPos(hWnd As IntPtr, hWndInsertAfter As IntPtr, X As Integer, Y As Integer, cx As Integer, cy As Integer, uFlags As UInteger) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetMonitorInfo(hMonitor As IntPtr, ByRef lpmi As MONITORINFO) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function MonitorFromWindow(hwnd As IntPtr, dwFlags As UInteger) As IntPtr
    End Function

    <StructLayout(LayoutKind.Sequential)>
    Private Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Private Structure MONITORINFO
        Public cbSize As Integer
        Public rcMonitor As RECT
        Public rcWork As RECT
        Public dwFlags As UInteger
    End Structure

    Private Const WM_NCHITTEST As Integer = &H84
    Private Const WM_NCCALCSIZE As Integer = &H83
    Private Const WM_SYSCOMMAND As Integer = &H112
    Private Const WM_GETMINMAXINFO As Integer = &H24
    Private Const HTCLIENT As Integer = 1
    Private Const HTCAPTION As Integer = 2
    Private Const HTLEFT As Integer = 10
    Private Const HTRIGHT As Integer = 11
    Private Const HTTOP As Integer = 12
    Private Const HTTOPLEFT As Integer = 13
    Private Const HTTOPRIGHT As Integer = 14
    Private Const HTBOTTOM As Integer = 15
    Private Const HTBOTTOMLEFT As Integer = 16
    Private Const HTBOTTOMRIGHT As Integer = 17
    Private Const SC_MAXIMIZE As Integer = &HF030
    Private Const SC_MINIMIZE As Integer = &HF020
    Private Const SC_RESTORE As Integer = &HF120
    Private Const SC_CLOSE As Integer = &HF060
    Private Const SWP_FRAMECHANGED As UInteger = &H20
    Private Const SWP_NOACTIVATE As UInteger = &H10
    Private Const SWP_NOMOVE As UInteger = &H2
    Private Const SWP_NOSIZE As UInteger = &H1
    Private Const SWP_NOZORDER As UInteger = &H4
    Private Const MONITOR_DEFAULTTONEAREST As UInteger = 2

    <StructLayout(LayoutKind.Sequential)>
    Private Structure MINMAXINFO
        Public ptReserved As Win32Point
        Public ptMaxSize As Win32Point
        Public ptMaxPosition As Win32Point
        Public ptMinTrackSize As Win32Point
        Public ptMaxTrackSize As Win32Point
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Private Structure Win32Point
        Public X As Integer
        Public Y As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Private Structure NCCALCSIZE_PARAMS
        Public rgrc0 As RECT
        Public rgrc1 As RECT
        Public rgrc2 As RECT
        Public lppos As IntPtr
    End Structure
#End Region

    ''' <summary>
    '''     ダブルバッファリングでちらつきを抑制（WS_EX_COMPOSITEDを無効化して透過問題回避）
    ''' </summary>
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp = MyBase.CreateParams
            ' WS_EX_COMPOSITEDを無効化（透過問題の原因になるため）
            ' cp.ExStyle = cp.ExStyle Or &H2000000 ' WS_EX_COMPOSITED
            Return cp
        End Get
    End Property

    <DllImport("user32.dll")> Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As Boolean, lParam As IntPtr) As IntPtr
    End Function

    ''' <summary>
    ''' 指定ウィンドウとその子孫コントロールすべての再描画をまとめて抑制する。
    ''' WM_SETREDRAWは対象ウィンドウ自身にしか効かず、SplitContainerやDataGridViewのように
    ''' 個別のハンドルを持つ子コントロールの再描画は抑制できないため使用する。
    ''' </summary>
    <DllImport("user32.dll")> Private Shared Function LockWindowUpdate(hWndLock As IntPtr) As Boolean
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

    ''' <summary>
    '''     リサイズグリップ用の状態変数
    ''' </summary>
    Private _isResizing As Boolean = False
    Private _resizeStartPoint As Point
    Private _resizeStartSize As Size
    Private _resizeStartLocation As Point
    Private _resizeDirection As String = ""
    Private _lastResizeTime As Long = 0

#End Region

#Region "フォームイベント"

    Private Sub MainPlayerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.SuspendLayout()
        SendMessage(Me.Handle, WM_SETREDRAW, False, IntPtr.Zero)

        Instance = Me
        InitializeWindowPosition()
        InitializeMediaPlayer()
        InitializeHotKeys()
        InitializeCustomTitleBar()
        ' InitializeResizeGrips()  ' サイズ調整用グリップ（左上、右上、左下、右下）を無効化
        LoadDefaultSettings()
        ApplyUiSettings()
        UpdateControllerMinSize()
        UpdateJumpButtonLabels()

        ' プレイリストの復元
        RestorePlaylist()
        ScanPlaylistDurations()

        ' しおりパネルの背景色を明示的に設定（透明化防止）
        If SplitContainer1.Panel2 IsNot Nothing Then
            SplitContainer1.Panel2.BackColor = SystemColors.ControlDarkDark
        End If
        If DataGridView1 IsNot Nothing Then
            DataGridView1.BackgroundColor = SystemColors.ControlDarkDark
        End If

        ' ボタンフォントサイズの初期調整
        AdjustTableLayoutPanelButtonFonts()

        ' Button200（再生/一時停止）のアイコンを中央配置（引き伸ばし防止）
        Dim btn200 = Me.Controls.Find("Button200", True).FirstOrDefault()
        If btn200 IsNot Nothing Then
            Dim prop = btn200.GetType().GetProperty("ImageLayout")
            If prop IsNot Nothing Then prop.SetValue(btn200, System.Windows.Forms.ImageLayout.Center, Nothing)
        End If

        ' Button400（停止）のアイコンを中央配置（引き伸ばし防止）
        Dim btn400 = Me.Controls.Find("Button400", True).FirstOrDefault()
        If btn400 IsNot Nothing Then
            Dim prop = btn400.GetType().GetProperty("ImageLayout")
            If prop IsNot Nothing Then prop.SetValue(btn400, System.Windows.Forms.ImageLayout.Center, Nothing)
        End If

        ' カスタムタイトルバーを最前面に固定
        If CustomTitleBar IsNot Nothing Then
            Me.Controls.SetChildIndex(CustomTitleBar, 0)
        End If

        ' ピッチ補正（タイムストレッチ）チェックボックスの初期化
        InitializePitchCorrectionCheckbox()

        ' しおりパネルの背景色設定（透過防止）
        SplitContainer1.Panel2.BackColor = SystemColors.ControlDarkDark
        SetDoubleBuffered(SplitContainer1.Panel2, True)
        If TableLayoutPanel2 IsNot Nothing Then
            TableLayoutPanel2.BackColor = SystemColors.ControlDarkDark
            SetDoubleBuffered(TableLayoutPanel2, True)
            AddHandler TableLayoutPanel2.Paint, AddressOf TableLayoutPanel2_Paint
        End If
        If DataGridView1 IsNot Nothing Then
            DataGridView1.BackgroundColor = SystemColors.ControlDarkDark
            DataGridView1.DefaultCellStyle.BackColor = SystemColors.ControlDarkDark
            DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = SystemColors.ControlDarkDark
            SetDoubleBuffered(DataGridView1, True)
        End If
        ' Panel2のPaintイベントで背景を確実に描画
        AddHandler SplitContainer1.Panel2.Paint, AddressOf Panel2_Paint

        ' パネル表示切替でリサイズされる主要なコンテナにもダブルバッファリングを適用し、ちらつきを低減する
        ' （WS_EX_COMPOSITEDはカスタムタイトルバーの透過問題を避けるため無効化されているため、代わりに
        ' 　コントロール単位でダブルバッファリングを行う）
        SetDoubleBuffered(SplitContainer1, True)
        SetDoubleBuffered(SplitContainer1.Panel1, True)
        SetDoubleBuffered(SplitContainer2, True)
        SetDoubleBuffered(SplitContainer2.Panel1, True)
        SetDoubleBuffered(SplitContainer2.Panel2, True)
        SetDoubleBuffered(SplitContainer3, True)
        SetDoubleBuffered(SplitContainer3.Panel2, True)
        SetDoubleBuffered(TableLayoutPanel1, True)
        SetDoubleBuffered(DataGridView2, True)

        Me.ResumeLayout()
        SendMessage(Me.Handle, WM_SETREDRAW, True, IntPtr.Zero)
        Me.Refresh()


    End Sub

    ''' <summary>
    ''' TableLayoutPanel1内のボタンフォントサイズを自動調整
    ''' </summary>
    Private Sub AdjustTableLayoutPanelButtonFonts()
        If TableLayoutPanel1 Is Nothing Then Return

        For Each ctrl As Control In GetAllControls(TableLayoutPanel1)
            If TypeOf ctrl Is System.Windows.Forms.Button Then
                Dim btn = DirectCast(ctrl, System.Windows.Forms.Button)
                ' ボタンの高さから適切なフォントサイズを計算（パディング考慮）
                Dim availableHeight = btn.Height - btn.Margin.Vertical - 4
                Dim fontSize = CSng(Math.Max(7, Math.Min(14, availableHeight * 0.45)))

                ' サイズが変わっていない場合にFontを作り直すと、幅だけの変化（例：プレイリスト開閉）でも
                ' 全ボタンが不要に再描画されてしまうため、実際にサイズが変わる場合のみ再代入する
                If Math.Abs(btn.Font.Size - fontSize) > 0.01F Then
                    btn.Font = New Font(btn.Font.FontFamily, fontSize, btn.Font.Style)
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' ボタンの画像とImageLayoutを同時に設定するヘルパー
    ''' （Image変更時にImageLayoutがリセットされるのを防ぐ）
    ''' </summary>
    Private Sub SetButtonImage(btn As Control, img As System.Drawing.Image)
        If btn IsNot Nothing AndAlso img IsNot Nothing Then
            Dim t = btn.GetType()
            Dim propImage = t.GetProperty("Image")
            If propImage IsNot Nothing Then propImage.SetValue(btn, img, Nothing)
            Dim propLayout = t.GetProperty("ImageLayout")
            If propLayout IsNot Nothing Then propLayout.SetValue(btn, System.Windows.Forms.ImageLayout.Center, Nothing)
        End If
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
    '''     コントロールの DoubleBuffered プロパティをリフレクションで設定
    ''' </summary>
    Private Sub SetDoubleBuffered(ctrl As Control, value As Boolean)
        If ctrl Is Nothing Then Return
        Try
            Dim prop = ctrl.GetType().GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
            If prop IsNot Nothing Then
                prop.SetValue(ctrl, value, Nothing)
            End If
        Catch
            ' 無視
        End Try
    End Sub

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
            SetButtonImage(Button200, My.Resources.Pause_16x)
        Else
            _mediaPlayer.Pause()
            SetButtonImage(Button200, My.Resources.Run_16x)
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
            CallByName(My.Settings, "SK" & (i + 1).ToString(), CallType.Set, jumpValues(i))
        Next
    End Sub

    ''' <summary>
    '''     速度コントロールボタン設定の初期化
    ''' </summary>
    Private Sub InitializeSpeedButtonSettings()
        Dim speedValues As Double() = {5, 10, 12, 13, 14, 15, 20}

        For i = 0 To speedValues.Length - 1
            CallByName(My.Settings, "SC" & (i + 1).ToString(), CallType.Set, speedValues(i))
        Next
    End Sub

    ''' <summary>
    '''     ピッチ補正（タイムストレッチ）チェックボックスの初期化
    ''' </summary>
    Private Sub InitializePitchCorrectionCheckbox()
        ' TableLayoutPanel1 の速度調整エリア（Row 5, Column 8-16 あたり）に配置
        Dim chkPitch = New CheckBox With {
            .Name = "CheckBoxPitchCorrection",
            .Text = "",
            .AutoSize = True,
            .Anchor = AnchorStyles.Left,
            .Font = New Font("Meiryo UI", 8.0F, FontStyle.Regular),
            .ForeColor = Color.White,
            .BackColor = Color.Transparent
        }

        ' TrackBar2 の ColumnSpan を 9 -> 8 に縮小し、最後の列（Column 16）にチェックボックス配置
        If TableLayoutPanel1.Controls.Contains(TrackBar2) Then
            Dim cellPos = TableLayoutPanel1.GetCellPosition(TrackBar2)
            ' 現在の ColumnSpan を取得して 8 に変更
            TableLayoutPanel1.SetColumnSpan(TrackBar2, 8)
            ' TrackBar2 が Column 8 から始まるとして、Column 16 (8+8) にチェックボックス配置
            TableLayoutPanel1.Controls.Add(chkPitch, cellPos.Column + 8, cellPos.Row)
            TableLayoutPanel1.SetColumnSpan(chkPitch, 1)
        Else
            ' フォールバック
            TableLayoutPanel1.Controls.Add(chkPitch, 16, 5)
        End If

        ' ツールチップ設定
        ToolTip1.SetToolTip(chkPitch, "タイムストレッチ（音程維持）")

        ' 設定から復元（設定がない場合は True = 有効）
        Dim savedValue As Boolean = True
        Try
            savedValue = CBool(My.Settings("PitchCorrection"))
        Catch
            ' 設定が存在しない場合はデフォルト True
        End Try
        chkPitch.Checked = savedValue
        _mediaPlayer.PitchCorrection = savedValue

        AddHandler chkPitch.CheckedChanged, AddressOf CheckBoxPitchCorrection_CheckedChanged
    End Sub

    ''' <summary>
    '''     ピッチ補正チェックボックス変更時
    ''' </summary>
    Private Sub CheckBoxPitchCorrection_CheckedChanged(sender As Object, e As EventArgs)
        Dim chk = DirectCast(sender, CheckBox)
        _mediaPlayer.PitchCorrection = chk.Checked
        My.Settings("PitchCorrection") = chk.Checked
        My.Settings.Save()
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
        ' ※このメソッドはMe.SuspendLayout()中に呼ばれるため、SplitContainer1.Widthなど子コントロールの
        ' 　サイズはまだMe.ClientSizeに追従しておらず古い値のままとなる。Me.ClientSizeはフォーム自身の
        ' 　プロパティで即座に反映されるため、計算には必ずMe.ClientSizeを使う
        If My.Settings.shiori = True Then
            Dim panelWidth As Integer = If(My.Settings.Shiori_Width > 0, My.Settings.Shiori_Width, 250)
            SplitContainer1.FixedPanel = FixedPanel.Panel2
            ' ButtonShiori_Clickで非表示にする際、ここで広げた量だけ正確に戻すために記録する
            _shioriWidthDelta = panelWidth + SplitContainer1.SplitterWidth
            Me.Width += _shioriWidthDelta
            SplitContainer1.Panel2Collapsed = False
            SplitContainer1.SplitterDistance = Me.ClientSize.Width - panelWidth - SplitContainer1.SplitterWidth

            ' 背景色を確実に設定（Panel2, TableLayoutPanel2, DataGridView1 すべて）
            SplitContainer1.Panel2.BackColor = SystemColors.ControlDarkDark
            SetDoubleBuffered(SplitContainer1.Panel2, True)
            If TableLayoutPanel2 IsNot Nothing Then
                TableLayoutPanel2.BackColor = SystemColors.ControlDarkDark
                SetDoubleBuffered(TableLayoutPanel2, True)
            End If
            If DataGridView1 IsNot Nothing Then
                DataGridView1.BackgroundColor = SystemColors.ControlDarkDark
                DataGridView1.DefaultCellStyle.BackColor = SystemColors.ControlDarkDark
                DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = SystemColors.ControlDarkDark
                SetDoubleBuffered(DataGridView1, True)
            End If

            SplitContainer1.Panel2.Invalidate()
            SplitContainer1.Panel2.Update()
        Else
            SplitContainer1.Panel2Collapsed = True
            SplitContainer1.FixedPanel = FixedPanel.None
        End If

        ' プレイリストパネルの復元（左に飛び出し）
        ' ※SplitterDistanceは絶対値（panelWidth）で指定するため、子コントロールのサイズが
        ' 　まだMe.ClientSizeに追従していなくても影響を受けない
        If My.Settings.PL = True Then
            Dim panelWidth As Integer = If(My.Settings.PL_Width > 0, My.Settings.PL_Width, 300)
            SplitContainer2.FixedPanel = FixedPanel.Panel2
            ' Button40_Clickで非表示にする際、ここで広げた量だけ正確に戻すために記録する
            ' スプリッター分の幅も含めないと、メインプレイヤー側がスプリッター幅の分だけ狭くなる
            _playlistWidthDelta = panelWidth + SplitContainer2.SplitterWidth
            Me.Left -= _playlistWidthDelta
            Me.Width += _playlistWidthDelta
            SplitContainer2.Panel1Collapsed = False
            SplitContainer2.SplitterDistance = panelWidth
            Button40.Text = "PL >"
            Button40.ForeColor = Color.Green
        Else
            SplitContainer2.Panel1Collapsed = True
            Button40.Text = "< PL"
            Button40.ForeColor = Color.Black
        End If

        ' カスタムタイトルバーを最前面に（しおりパネル展開時に隠れないよう）
        If CustomTitleBar IsNot Nothing Then
            CustomTitleBar.BringToFront()
        End If
    End Sub

    ''' <summary>
    '''     ジャンプボタンのラベルを設定値で更新
    ''' </summary>
    Private Sub UpdateJumpButtonLabels()
        For i As Integer = 1 To 20
            Dim btn As System.Windows.Forms.Button = TryCast(Me.Controls.Find("Button" & i, True).FirstOrDefault(), System.Windows.Forms.Button)
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
    '''     ウィンドウプロシージャ（ホットキー処理用、カスタムタイトルバー対応）
    ''' </summary>
    Protected Overrides Sub WndProc(ByRef m As Message)
        Select Case m.Msg
            Case WM_NCCALCSIZE
                ' クライアント領域をウィンドウ全体に拡張（標準タイトルバーを非表示化）
                If m.WParam <> IntPtr.Zero Then
                    Dim ncParams = DirectCast(Marshal.PtrToStructure(m.LParam, GetType(NCCALCSIZE_PARAMS)), NCCALCSIZE_PARAMS)
                    ncParams.rgrc0.Top += 0 ' タイトルバー分のスペースを確保しない
                    Marshal.StructureToPtr(ncParams, m.LParam, True)
                End If
                m.Result = IntPtr.Zero
                Return

            Case WM_NCHITTEST
                ' カスタムタイトルバー領域でのヒットテスト
                Dim result = HitTestNCA(m.LParam)
                If result <> HTCLIENT Then
                    m.Result = New IntPtr(result)
                    Return
                End If

            Case WM_SYSCOMMAND
                ' システムコマンド（最大化/最小化/閉じる）をフック
                If (m.WParam.ToInt32() And &HFFF0) = SC_MAXIMIZE Then
                    MaximizeWindow()
                    Return
                ElseIf (m.WParam.ToInt32() And &HFFF0) = SC_RESTORE Then
                    RestoreWindow()
                    Return
                ElseIf (m.WParam.ToInt32() And &HFFF0) = SC_MINIMIZE Then
                    Me.WindowState = FormWindowState.Minimized
                    Return
                ElseIf (m.WParam.ToInt32() And &HFFF0) = SC_CLOSE Then
                    Me.Close()
                    Return
                End If

            Case WM_GETMINMAXINFO
                ' 最大化時のサイズ/位置をモニタのワークエリアに制限 + 最小サイズ設定
                Dim mmi = DirectCast(Marshal.PtrToStructure(m.LParam, GetType(MINMAXINFO)), MINMAXINFO)
                Dim monitor = MonitorFromWindow(Me.Handle, MONITOR_DEFAULTTONEAREST)
                If monitor <> IntPtr.Zero Then
                    Dim mi As New MONITORINFO()
                    mi.cbSize = Marshal.SizeOf(mi)
                    If GetMonitorInfo(monitor, mi) Then
                        mmi.ptMaxPosition.X = mi.rcWork.Left - mi.rcMonitor.Left
                        mmi.ptMaxPosition.Y = mi.rcWork.Top - mi.rcMonitor.Top
                        mmi.ptMaxSize.X = mi.rcWork.Right - mi.rcWork.Left
                        mmi.ptMaxSize.Y = mi.rcWork.Bottom - mi.rcWork.Top
                    End If
                End If
                ' 最小トラッキングサイズ設定（デザイナのMinimumSizeを使用）
                mmi.ptMinTrackSize.X = Me.MinimumSize.Width
                mmi.ptMinTrackSize.Y = Me.MinimumSize.Height
                Marshal.StructureToPtr(mmi, m.LParam, True)
                Return
        End Select

        If m.Msg = WmHotkey Then
            HandleHotKey(m.WParam.ToInt32())
        End If
        MyBase.WndProc(m)
    End Sub

    ''' <summary>
    '''     非クライアント領域のヒットテスト（カスタムタイトルバー・リサイズ対応）
    ''' </summary>
    Private Function HitTestNCA(lParam As IntPtr) As Integer
        Dim pt = New Point(CInt(lParam.ToInt64() And &HFFFF), CInt(lParam.ToInt64() >> 16))
        pt = Me.PointToClient(pt)

        ' タイトルバー領域の高さ（カスタムタイトルバーの高さ）
        Const TitleBarHeight As Integer = 28
        Dim clientRect = Me.ClientRectangle
        Dim resizeBorder = 25

        ' タイトルバー領域（ドラッグ移動可能）
        If pt.Y <= TitleBarHeight AndAlso pt.X >= 0 AndAlso pt.X < clientRect.Width Then
            ' ウィンドウボタン領域を除外
            If pt.X > clientRect.Width - 130 Then
                ' 閉じる/最大化/最小化ボタンの領域は HTCLIENT にしてボタンイベントを通す
                Return HTCLIENT
            End If
            Return HTCAPTION
        End If

        ' リサイズボーダー判定
        Dim isLeft = pt.X < resizeBorder
        Dim isRight = pt.X >= clientRect.Width - resizeBorder
        Dim isTop = pt.Y < resizeBorder
        Dim isBottom = pt.Y >= clientRect.Height - resizeBorder

        If isTop AndAlso isLeft Then Return HTTOPLEFT
        If isTop AndAlso isRight Then Return HTTOPRIGHT
        If isBottom AndAlso isLeft Then Return HTBOTTOMLEFT
        If isBottom AndAlso isRight Then Return HTBOTTOMRIGHT
        If isLeft Then Return HTLEFT
        If isRight Then Return HTRIGHT
        If isTop Then Return HTTOP
        If isBottom Then Return HTBOTTOM

        Return HTCLIENT
    End Function

    ''' <summary>
    '''     ウィンドウを最大化
    ''' </summary>
    Private Sub MaximizeWindow()
        If Me.WindowState = FormWindowState.Maximized Then
            RestoreWindow()
        Else
            Me.WindowState = FormWindowState.Maximized
        End If
    End Sub

    ''' <summary>
    '''     ウィンドウを元のサイズに戻す
    ''' </summary>
    Private Sub RestoreWindow()
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
        End If
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
            SetButtonImage(Button200, My.Resources.Run_16x)
        Else
            _mediaPlayer.Play()
            SetButtonImage(Button200, My.Resources.Pause_16x)
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

        Clipboard.SetText(prefix & timeCode & suffix)
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

        Return String.Format("{0:D2}:{1:D2}:{2:D2}.{3:D2}", hours, minutes, seconds, frames)
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
        Label4.Text = "x" & speed.ToString("F1")
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

        ' ウィンドウサイズの変更とパネル表示切替を1回の再描画にまとめ、ちらつきを防ぐ
        LockWindowUpdate(Me.Handle)
        Try
            If isShowing Then
                ' 動画表示時：Row 0を非表示（高さ0）
                If TableLayoutPanel1.RowStyles.Count > 0 Then
                    TableLayoutPanel1.RowStyles(0).Height = 0
                    TableLayoutPanel1.RowStyles(0).SizeType = SizeType.Absolute
                End If

                Dim panelHeight As Integer = If(ScrHeight > 0, ScrHeight, 300)
                ' フォームを上に拡張（Topを減らし、Heightを増やす）
                Me.Top -= panelHeight
                Me.Height += panelHeight
                SplitContainer3.Panel1Collapsed = False
                SplitContainer3.SplitterDistance = panelHeight

                ' SplitContainer3.SplitterMovedがScrHeightを上書きする可能性があるため、最後に確定値を再設定する
                ScrHeight = panelHeight
            Else
                ' 動画非表示時：Row 0を高さ32で表示
                If TableLayoutPanel1.RowStyles.Count > 0 Then
                    TableLayoutPanel1.RowStyles(0).Height = 32
                    TableLayoutPanel1.RowStyles(0).SizeType = SizeType.Absolute
                End If

                ' 非表示：高さを保存してからPanel1を折りたたみ、その後にフォームを上に縮小する
                ' （折りたたむ前に高さを縮めると、FixedPanel未設定のためPanel1も比例縮小し、
                ' 　SplitterMovedでScrHeightに縮んだ値が入ってしまう）
                Dim actualPanelHeight As Integer = SplitContainer3.Panel1.Height
                ScrHeight = actualPanelHeight
                SplitContainer3.Panel1Collapsed = True
                Me.Top += actualPanelHeight
                Me.Height -= actualPanelHeight

                ' SplitContainer3.SplitterMovedがScrHeightを上書きする可能性があるため、最後に確定値を再設定する
                ScrHeight = actualPanelHeight
                My.Settings.Gamen_Height = ScrHeight
                My.Settings.Save()
            End If
            UpdateControllerMinSize()
        Finally
            LockWindowUpdate(IntPtr.Zero)
        End Try
        Me.Refresh()
    End Sub

#End Region

#Region "ボタンイベントハンドラ"

    ' 速度ボタン
    Private Sub SpeedButtons_Click(sender As Object, e As EventArgs) _
        Handles Button21.Click, Button22.Click, Button23.Click, Button24.Click, Button25.Click, Button26.Click,
                Button27.Click
        Dim btn = TryCast(sender, System.Windows.Forms.Button)
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
        SetButtonImage(Button200, My.Resources.Run_16x)

        Label1.Text = TimeSpan.FromSeconds(_mediaPlayer.Position).ToString("hh\:mm\:ss") &
              My.Resources.TimeSeparator &
              TimeSpan.FromSeconds(_mediaPlayer.Duration).ToString("hh\:mm\:ss")

        TrackBar1.Value = 0

    End Sub

    Private Sub Button200_Click(sender As Object, e As EventArgs) Handles Button200.Click


        If _mediaPlayer.IsPlaying Then
            _mediaPlayer.Pause()
            SetButtonImage(Button200, My.Resources.Run_16x)
        Else
            _mediaPlayer.Play()
            SetButtonImage(Button200, My.Resources.Pause_16x)
        End If

    End Sub

    ' ジャンプボタン
    Private Sub JumpButtons_Click(sender As Object, e As EventArgs) _
        Handles Button1.Click, Button11.Click, Button2.Click, Button12.Click, Button3.Click, Button13.Click,
                Button4.Click, Button14.Click, Button5.Click, Button15.Click, Button6.Click, Button16.Click,
                Button7.Click, Button17.Click, Button8.Click, Button18.Click, Button9.Click, Button19.Click,
                Button10.Click, Button20.Click
        Dim btn = TryCast(sender, System.Windows.Forms.Button)
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
    '''     DataGridViewに行を追加
    ''' </summary>
    Private Sub AddBookmarkRow(timeDisplay As String, memo As String, seconds As Integer)
        Dim row As String() = {timeDisplay, memo, seconds.ToString(), "削除"}
        DataGridView1.Rows.Add(row)
    End Sub

    ''' <summary>
    '''     テキストから抽出したしおりをDataGridViewに追加
    ''' </summary>
    Private Sub ParseTextContentForBookmarks(content As String)
        Dim entries = BookmarkTextParser.Parse(content, My.Settings.Fuka, My.Settings.Fumei,
                                               My.Settings.Fumei2, My.Settings.Sonota)
        For Each entry As BookmarkEntry In entries
            AddBookmarkRow(entry.TimeDisplay, entry.Memo, entry.PositionSeconds)
        Next
    End Sub

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
                    Dim content As String = BookmarkTextReader.ReadWordDocument(filePath)
                    ParseTextContentForBookmarks(content)
                Catch ex As Exception
                    MsgBox(String.Format(My.Resources.WordFileLoadFailed, ex.Message), vbOKOnly)
                End Try

            Case ".txt"
                Dim content As String = BookmarkTextReader.ReadTextFile(filePath)
                ParseTextContentForBookmarks(content)

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

        ' ウィンドウ幅の変更とパネル表示切替を1回の再描画にまとめ、ちらつきを防ぐ
        LockWindowUpdate(Me.Handle)
        Try
            If SplitContainer1.Panel2Collapsed Then
                ' 表示：FixedPanelを先に設定（Panel2の幅を固定）
                SplitContainer1.FixedPanel = FixedPanel.Panel2
                Dim panelWidth As Integer = If(BMWidth > 0, BMWidth, 250)
                _shioriWidthDelta = panelWidth + SplitContainer1.SplitterWidth

                ' 先にPanel2(しおり)の幅がpanelWidthになるよう分割位置を決めてから、あとでウィンドウを広げる。
                ' FixedPanel=Panel2のため、広げた分は自動的にPanel1（メインプレイヤー側）に割り当てられ、
                ' Panel2はpanelWidthのまま保たれる。先に広げてからMe.ClientSize.Widthを読んで分割位置を
                ' 計算する順序だと、リサイズ完了タイミングとのずれで数px誤差が生じ、開閉のたびにズレが蓄積する
                SplitContainer1.Panel2Collapsed = False
                SplitContainer1.SplitterDistance = SplitContainer1.Width - panelWidth - SplitContainer1.SplitterWidth
                Me.Width += _shioriWidthDelta

                ' Me.Width変更中にMainPlayerForm_ResizeがBMWidthを一時的な値で上書きしてしまうため、
                ' 最後に確定値を再設定する
                BMWidth = panelWidth

                ' 背景色を確実に設定（Panel2, TableLayoutPanel2, DataGridView1 すべて）
                SplitContainer1.Panel2.BackColor = SystemColors.ControlDarkDark
                SetDoubleBuffered(SplitContainer1.Panel2, True)
                If TableLayoutPanel2 IsNot Nothing Then
                    TableLayoutPanel2.BackColor = SystemColors.ControlDarkDark
                    SetDoubleBuffered(TableLayoutPanel2, True)
                End If
                If DataGridView1 IsNot Nothing Then
                    DataGridView1.BackgroundColor = SystemColors.ControlDarkDark
                    DataGridView1.DefaultCellStyle.BackColor = SystemColors.ControlDarkDark
                    DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = SystemColors.ControlDarkDark
                    SetDoubleBuffered(DataGridView1, True)
                End If
            Else
                ' 非表示：次回表示時の幅として現在の実幅を保存しつつ、フォーム幅は表示時に広げた量を
                ' そのまま戻す（実幅を使うと、開閉を繰り返すたびに誤差が蓄積するため）
                Dim actualPanelWidth As Integer = SplitContainer1.Panel2.Width
                BMWidth = actualPanelWidth
                SplitContainer1.Panel2Collapsed = True
                SplitContainer1.FixedPanel = FixedPanel.None
                Me.Width -= _shioriWidthDelta

                ' Me.Width変更中にMainPlayerForm_ResizeがBMWidthをPanel2折りたたみ中の値（0など）で
                ' 上書きしてしまうため、最後に確定値を再設定する
                BMWidth = actualPanelWidth
            End If

            ' カスタムタイトルバーを最前面に（しおりパネル表示/非表示でフォーム幅が変わるため）
            If CustomTitleBar IsNot Nothing Then
                CustomTitleBar.BringToFront()
            End If
        Finally
            LockWindowUpdate(IntPtr.Zero)
        End Try
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

        For Each row As String() In BookmarkCsvStore.Load(csvFile)
            DataGridView1.Rows.Add(row)
        Next
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

            BookmarkCsvStore.Save(filePath, arrData)
            Return True

        Catch ex As Exception
            MsgBox(ex.Message, vbOKOnly)
            Return False
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
    '''     TextBox1でEnterキーが押されたときにYouTube URL等を再生
    ''' </summary>
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode <> Keys.Enter Then Return
        Dim url = TextBox1.Text.Trim()
        If String.IsNullOrEmpty(url) Then Return

        ' YouTube等のURL判定（mpvはyt-dlpがあれば直接再生可能）
        If url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) OrElse
           url.StartsWith("https://", StringComparison.OrdinalIgnoreCase) Then

            ' yt-dlp の存在確認（YouTube等のストリーミング再生に必要）
            If Not IsYtDlpAvailable() Then
                MsgBox("yt-dlp が見つかりません。YouTube 等の URL を再生するには yt-dlp が必要です。", vbOKOnly Or vbExclamation)
                Return
            End If

            _suppressAutoPlay = False
            _mediaPlayer.LoadFile(url)

            ' 即座にクリアせず、MediaChanged イベントで正常読み込みを確認してからクリアする
            ' ここではクリアしない（失敗時のユーザー確認のため）
        End If
    End Sub

    ''' <summary>
    '''     yt-dlp が PATH に存在するか確認
    ''' </summary>
    Private Function IsYtDlpAvailable() As Boolean
        Try
            Dim psi As New ProcessStartInfo("yt-dlp", "--version") With {
                .UseShellExecute = False,
                .CreateNoWindow = True,
                .RedirectStandardOutput = True,
                .RedirectStandardError = True
            }
            Using proc = Process.Start(psi)
                If proc Is Nothing Then Return False
                proc.WaitForExit(3000)
                Return proc.ExitCode = 0
            End Using
        Catch
            Return False
        End Try
    End Function

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
        For col = 0 To 20
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
        'If e.Column = 20 AndAlso (e.Row = 3 OrElse e.Row = 4 OrElse e.Row = 5) Then
        'e.Graphics.FillRectangle(darkBrush, e.CellBounds)
        'End If

        '音量調整部分
        For row = 3 To 5
            If e.Column = 20 AndAlso e.Row Then
                e.Graphics.FillRectangle(darkBrush, e.CellBounds)
            End If
        Next

        ' 速度調整部分
        If (e.Column = 6 OrElse e.Column >= 8 AndAlso e.Column <= 18) AndAlso e.Row = 5 Then
            e.Graphics.FillRectangle(darkBrush2, e.CellBounds)
        End If


    End Sub

#End Region

#Region "カスタムタイトルバー"

    ''' <summary>
    '''     カスタムタイトルバーの初期化
    ''' </summary>
    Private Sub InitializeCustomTitleBar()
        ' タイトルバーの基本設定 - 絶対配置でフォーム最上部に固定
        If CustomTitleBar IsNot Nothing Then
            CustomTitleBar.Height = CustomTitleBarHeight
            CustomTitleBar.Dock = DockStyle.None
            CustomTitleBar.Location = New Point(0, 0)
            CustomTitleBar.Size = New Size(Me.ClientSize.Width, CustomTitleBarHeight)
            CustomTitleBar.BackColor = Color.FromArgb(30, 30, 30)
            CustomTitleBar.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            CustomTitleBar.BringToFront()
        End If

        ' タイトルラベル
        If LblTitle IsNot Nothing Then
            LblTitle.Text = Me.Text
            LblTitle.ForeColor = Color.White
            LblTitle.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            LblTitle.AutoSize = False
            LblTitle.TextAlign = ContentAlignment.MiddleLeft
            LblTitle.Dock = DockStyle.Fill
            LblTitle.Padding = New Padding(12, 0, 0, 0)
            LblTitle.BackColor = Color.Transparent
        End If

        ' 最小化ボタン
        If BtnMinimize IsNot Nothing Then
            BtnMinimize.Text = "━"
            BtnMinimize.Size = New Size(46, CustomTitleBarHeight)
            BtnMinimize.Location = New Point(0, 0)
            BtnMinimize.Dock = DockStyle.Right
            BtnMinimize.FlatStyle = FlatStyle.Flat
            BtnMinimize.FlatAppearance.BorderSize = 0
            BtnMinimize.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 60, 60)
            BtnMinimize.FlatAppearance.MouseDownBackColor = Color.FromArgb(80, 80, 80)
            BtnMinimize.BackColor = Color.Transparent
            BtnMinimize.ForeColor = Color.White
            BtnMinimize.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            BtnMinimize.TabStop = False
            BtnMinimize.Cursor = Cursors.Hand
            AddHandler BtnMinimize.Click, AddressOf BtnMinimize_Click
        End If

        ' 最大化/復元ボタン
        If BtnMaximize IsNot Nothing Then
            BtnMaximize.Text = "□"
            BtnMaximize.Size = New Size(46, CustomTitleBarHeight)
            BtnMaximize.Location = New Point(0, 0)
            BtnMaximize.Dock = DockStyle.Right
            BtnMaximize.FlatStyle = FlatStyle.Flat
            BtnMaximize.FlatAppearance.BorderSize = 0
            BtnMaximize.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 60, 60)
            BtnMaximize.FlatAppearance.MouseDownBackColor = Color.FromArgb(80, 80, 80)
            BtnMaximize.BackColor = Color.Transparent
            BtnMaximize.ForeColor = Color.White
            BtnMaximize.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            BtnMaximize.TabStop = False
            BtnMaximize.Cursor = Cursors.Hand
            AddHandler BtnMaximize.Click, AddressOf BtnMaximize_Click
        End If

        ' 閉じるボタン
        If BtnClose IsNot Nothing Then
            BtnClose.Text = "✕"
            BtnClose.Size = New Size(46, CustomTitleBarHeight)
            BtnClose.Location = New Point(0, 0)
            BtnClose.Dock = DockStyle.Right
            BtnClose.FlatStyle = FlatStyle.Flat
            BtnClose.FlatAppearance.BorderSize = 0
            BtnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 17, 35)
            BtnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(196, 43, 28)
            BtnClose.BackColor = Color.Transparent
            BtnClose.ForeColor = Color.White
            BtnClose.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            BtnClose.TabStop = False
            BtnClose.Cursor = Cursors.Hand
            AddHandler BtnClose.Click, AddressOf BtnClose_Click
        End If

        ' コントロール追加順：Dock=Rightなので最初に追加したものが右端、最後に追加したものが左端
        ' 左から：閉じる、最大化、最小化 の順にするため、追加順は逆（最小化→最大化→閉じる）
        If CustomTitleBar IsNot Nothing Then
            ' 既にデザイナで追加済みなのでクリアしてから追加
            CustomTitleBar.Controls.Clear()
            CustomTitleBar.Controls.Add(BtnMinimize)   ' 最初に追加＝右端
            CustomTitleBar.Controls.Add(BtnMaximize)   ' 中央
            CustomTitleBar.Controls.Add(BtnClose)      ' 最後に追加＝左端
            CustomTitleBar.Controls.Add(LblTitle)      ' 左側残り全部
            CustomTitleBar.BringToFront()
        End If

        ' タイトルバーでのドラッグ移動を有効化
        AddHandler CustomTitleBar.MouseDown, AddressOf TitleBar_MouseDown
        AddHandler LblTitle.MouseDown, AddressOf TitleBar_MouseDown

        ' タイトルバーでのダブルクリックで最大化/復元
        AddHandler CustomTitleBar.DoubleClick, AddressOf CustomTitleBar_DoubleClick
        If LblTitle IsNot Nothing Then AddHandler LblTitle.DoubleClick, AddressOf CustomTitleBar_DoubleClick
    End Sub

    ''' <summary>
    '''     タイトルバーでのマウスダウンでドラッグ移動開始
    ''' </summary>
    Private Sub TitleBar_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            ReleaseCapture()
            SendMessage(Me.Handle, &HA1, New IntPtr(2), IntPtr.Zero) ' WM_NCLBUTTONDOWN, HTCAPTION
        End If
    End Sub

    ''' <summary>
    '''     最小化ボタンクリック
    ''' </summary>
    Private Sub BtnMinimize_Click(sender As Object, e As EventArgs)
        Me.WindowState = FormWindowState.Minimized
    End Sub

    ''' <summary>
    '''     最大化/復元ボタンクリック
    ''' </summary>
    Private Sub BtnMaximize_Click(sender As Object, e As EventArgs)
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
            BtnMaximize.Text = "□"
        Else
            Me.WindowState = FormWindowState.Maximized
            BtnMaximize.Text = "❐"
        End If
    End Sub

    ''' <summary>
    '''     閉じるボタンクリック
    ''' </summary>
    Private Sub BtnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    ''' <summary>
    '''     タイトルバーダブルクリックで最大化/復元
    ''' </summary>
    Private Sub CustomTitleBar_DoubleClick(sender As Object, e As EventArgs)
        BtnMaximize_Click(Nothing, Nothing)
    End Sub

    ''' <summary>
    '''     しおりパネルの背景を確実に描画（透過防止）
    ''' </summary>
    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs)
        Dim pnl = DirectCast(sender, Panel)
        Using brush As New SolidBrush(pnl.BackColor)
            e.Graphics.FillRectangle(brush, e.ClipRectangle)
        End Using
    End Sub

    ''' <summary>
    '''     TableLayoutPanel2の背景を確実に描画（透過防止）
    ''' </summary>
    Private Sub TableLayoutPanel2_Paint(sender As Object, e As PaintEventArgs)
        Dim tlp = DirectCast(sender, TableLayoutPanel)
        Using brush As New SolidBrush(tlp.BackColor)
            e.Graphics.FillRectangle(brush, e.ClipRectangle)
        End Using
    End Sub

    ''' <summary>
    '''     リサイズグリップ（コーナーのハンドル）を初期化
    ''' </summary>
    Private Sub InitializeResizeGrips()
        Dim gripSize As Integer = 16
        Dim gripColor As Color = Color.FromArgb(80, 80, 80)

        ' 右下グリップ（メイン）
        Dim gripBottomRight As New Panel With {
            .Name = "GripBottomRight",
            .Size = New Size(gripSize, gripSize),
            .Anchor = AnchorStyles.Bottom Or AnchorStyles.Right,
            .BackColor = Color.Transparent,
            .Cursor = Cursors.SizeNWSE
        }
        AddHandler gripBottomRight.MouseDown, AddressOf Grip_MouseDown
        AddHandler gripBottomRight.MouseMove, AddressOf Grip_MouseMove
        AddHandler gripBottomRight.MouseUp, AddressOf Grip_MouseUp
        AddHandler gripBottomRight.Paint, Sub(s, e)
                                              Using pen As New Pen(gripColor, 2)
                                                  Dim r = gripBottomRight.ClientRectangle
                                                  e.Graphics.DrawLine(pen, r.Right - 4, r.Bottom - 1, r.Right - 1, r.Bottom - 4)
                                                  e.Graphics.DrawLine(pen, r.Right - 7, r.Bottom - 1, r.Right - 1, r.Bottom - 7)
                                                  e.Graphics.DrawLine(pen, r.Right - 10, r.Bottom - 1, r.Right - 1, r.Bottom - 10)
                                              End Using
                                          End Sub
        Me.Controls.Add(gripBottomRight)
        gripBottomRight.BringToFront()

        ' 右上グリップ
        Dim gripTopRight As New Panel With {
            .Name = "GripTopRight",
            .Size = New Size(gripSize, gripSize),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
            .BackColor = Color.Transparent,
            .Cursor = Cursors.SizeNESW
        }
        AddHandler gripTopRight.MouseDown, AddressOf Grip_MouseDown
        AddHandler gripTopRight.MouseMove, AddressOf Grip_MouseMove
        AddHandler gripTopRight.MouseUp, AddressOf Grip_MouseUp
        AddHandler gripTopRight.Paint, Sub(s, e)
                                           Using pen As New Pen(gripColor, 2)
                                               Dim r = gripTopRight.ClientRectangle
                                               e.Graphics.DrawLine(pen, r.Right - 4, r.Top + 1, r.Right - 1, r.Top + 4)
                                               e.Graphics.DrawLine(pen, r.Right - 7, r.Top + 1, r.Right - 1, r.Top + 7)
                                               e.Graphics.DrawLine(pen, r.Right - 10, r.Top + 1, r.Right - 1, r.Top + 10)
                                           End Using
                                       End Sub
        Me.Controls.Add(gripTopRight)
        gripTopRight.BringToFront()

        ' 左下グリップ
        Dim gripBottomLeft As New Panel With {
            .Name = "GripBottomLeft",
            .Size = New Size(gripSize, gripSize),
            .Anchor = AnchorStyles.Bottom Or AnchorStyles.Left,
            .BackColor = Color.Transparent,
            .Cursor = Cursors.SizeNESW
        }
        AddHandler gripBottomLeft.MouseDown, AddressOf Grip_MouseDown
        AddHandler gripBottomLeft.MouseMove, AddressOf Grip_MouseMove
        AddHandler gripBottomLeft.MouseUp, AddressOf Grip_MouseUp
        AddHandler gripBottomLeft.Paint, Sub(s, e)
                                             Using pen As New Pen(gripColor, 2)
                                                 Dim r = gripBottomLeft.ClientRectangle
                                                 e.Graphics.DrawLine(pen, r.Left + 4, r.Bottom - 1, r.Left + 1, r.Bottom - 4)
                                                 e.Graphics.DrawLine(pen, r.Left + 7, r.Bottom - 1, r.Left + 1, r.Bottom - 7)
                                                 e.Graphics.DrawLine(pen, r.Left + 10, r.Bottom - 1, r.Left + 1, r.Bottom - 10)
                                             End Using
                                         End Sub
        Me.Controls.Add(gripBottomLeft)
        gripBottomLeft.BringToFront()

        ' 左上グリップ
        Dim gripTopLeft As New Panel With {
            .Name = "GripTopLeft",
            .Size = New Size(gripSize, gripSize),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Left,
            .BackColor = Color.Transparent,
            .Cursor = Cursors.SizeNWSE
        }
        AddHandler gripTopLeft.MouseDown, AddressOf Grip_MouseDown
        AddHandler gripTopLeft.MouseMove, AddressOf Grip_MouseMove
        AddHandler gripTopLeft.MouseUp, AddressOf Grip_MouseUp
        AddHandler gripTopLeft.Paint, Sub(s, e)
                                          Using pen As New Pen(gripColor, 2)
                                              Dim r = gripTopLeft.ClientRectangle
                                              e.Graphics.DrawLine(pen, r.Left + 4, r.Top + 1, r.Left + 1, r.Top + 4)
                                              e.Graphics.DrawLine(pen, r.Left + 7, r.Top + 1, r.Left + 1, r.Top + 7)
                                              e.Graphics.DrawLine(pen, r.Left + 10, r.Top + 1, r.Left + 1, r.Top + 10)
                                          End Using
                                      End Sub
        Me.Controls.Add(gripTopLeft)
        gripTopLeft.BringToFront()

        ' 全グリップを最前面に
        gripBottomRight.BringToFront()
        gripTopRight.BringToFront()
        gripBottomLeft.BringToFront()
        gripTopLeft.BringToFront()
    End Sub

    ''' <summary>
    '''     グリップでマウスムーブ：リサイズ実行（チラつき防止のためスロットル）
    ''' </summary>
    Private Sub Grip_MouseMove(sender As Object, e As MouseEventArgs)
        If Not _isResizing Then Return

        ' スロットル: 16ms (約60fps) 以上間隔を開ける
        Dim now = Environment.TickCount
        If now - _lastResizeTime < 16 Then Return
        _lastResizeTime = now

        Dim grip = DirectCast(sender, Control)
        Dim currentPoint = grip.PointToScreen(e.Location)
        Dim dx = currentPoint.X - _resizeStartPoint.X
        Dim dy = currentPoint.Y - _resizeStartPoint.Y

        Select Case _resizeDirection
            Case "BottomRight"
                Me.Size = New Size(_resizeStartSize.Width + dx, _resizeStartSize.Height + dy)
            Case "TopRight"
                Me.Size = New Size(_resizeStartSize.Width + dx, Math.Max(Me.MinimumSize.Height, _resizeStartSize.Height - dy))
                If dy > 0 Then Me.Location = New Point(_resizeStartLocation.X, _resizeStartLocation.Y + dy)
            Case "BottomLeft"
                Me.Size = New Size(Math.Max(Me.MinimumSize.Width, _resizeStartSize.Width - dx), _resizeStartSize.Height + dy)
                If dx > 0 Then Me.Location = New Point(_resizeStartLocation.X + dx, _resizeStartLocation.Y)
            Case "TopLeft"
                Me.Size = New Size(Math.Max(Me.MinimumSize.Width, _resizeStartSize.Width - dx), Math.Max(Me.MinimumSize.Height, _resizeStartSize.Height - dy))
                If dx > 0 Then Me.Location = New Point(_resizeStartLocation.X + dx, _resizeStartLocation.Y)
                If dy > 0 Then Me.Location = New Point(Me.Location.X, _resizeStartLocation.Y + dy)
        End Select
    End Sub

    ''' <summary>
    '''     グリップでマウスアップ：リサイズ終了
    ''' </summary>
    Private Sub Grip_MouseUp(sender As Object, e As MouseEventArgs)
        _isResizing = False
        SendMessage(Me.Handle, WM_SETREDRAW, True, IntPtr.Zero)
        Me.Refresh()
    End Sub

    ''' <summary>
    '''     グリップでマウスダウン：リサイズ開始
    ''' </summary>
    Private Sub Grip_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim grip = DirectCast(sender, Control)
            _isResizing = True
            _resizeStartPoint = grip.PointToScreen(e.Location)
            _resizeStartSize = Me.Size
            _resizeStartLocation = Me.Location
            _resizeDirection = DirectCast(sender, Panel).Name.Replace("Grip", "")
            SendMessage(Me.Handle, WM_SETREDRAW, False, IntPtr.Zero)
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
        PLWidth = SplitContainer2.Panel1.Width
        ScrHeight = SplitContainer3.Panel1.Height

        If BtnMaximize IsNot Nothing Then
            If Me.WindowState = FormWindowState.Maximized Then
                BtnMaximize.Text = "❐"
            Else
                BtnMaximize.Text = "□"
            End If
        End If

        ' カスタムタイトルバーを再描画（リサイズ時）
        If CustomTitleBar IsNot Nothing Then
            CustomTitleBar.Invalidate()
            CustomTitleBar.BringToFront()
            CustomTitleBar.Width = Me.ClientSize.Width
        End If
    End Sub

    Private Sub MainPlayerForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ' プレイリストの最初のファイルを読み込む（自動再生は抑制）
        If _playlistItems.Count > 0 Then
            _suppressAutoPlay = True
            _currentPlaylistIndex = 0
            _mediaPlayer.LoadFile(_playlistItems(0).FilePath)
        End If

        ' リサイズグリップを最前面に配置
        For Each ctrl As Control In Me.Controls
            If ctrl.Name.StartsWith("Grip") Then
                ctrl.BringToFront()
            End If
        Next

        ' カスタムタイトルバーを最前面に
        If CustomTitleBar IsNot Nothing Then
            CustomTitleBar.BringToFront()
        End If
    End Sub

    'PlayListの表示・非表示切替
    Private Sub Button40_Click(sender As Object, e As EventArgs) Handles Button40.Click

        ' ウィンドウの移動とパネル表示切替を1回の再描画にまとめ、ちらつきを防ぐ
        LockWindowUpdate(Me.Handle)
        Try
            If SplitContainer2.Panel1Collapsed = True Then
                ' 表示する場合
                Dim panelWidth As Integer = If(PLWidth > 0, PLWidth, 300)

                ' FixedPanelを先に設定（Panel2＝メインプレイヤー側の幅を固定）
                SplitContainer2.FixedPanel = FixedPanel.Panel2

                ' Panel1(プレイリスト)は新規に幅を割り当てる側なので、先にウィンドウを広げてから
                ' Panel1の幅を絶対値（panelWidth）で確定させる。
                ' （逆に先にSplitterDistanceを設定すると、FixedPanel=Panel2により
                ' 　あとで広げた分がPanel1に二重加算され、幅が肥大化する）
                ' スプリッター分の幅も含めて広げないと、メインプレイヤー側がスプリッター幅の分だけ
                ' 狭くなり、TableLayoutPanel1内のボタン配置がずれる
                _playlistWidthDelta = panelWidth + SplitContainer2.SplitterWidth
                Me.SetBounds(Me.Left - _playlistWidthDelta, Me.Top, Me.Width + _playlistWidthDelta, Me.Height)
                SplitContainer2.Panel1Collapsed = False
                SplitContainer2.SplitterDistance = panelWidth

                Button40.Text = "PL >"
                Button40.ForeColor = Color.Green

            Else
                ' 非表示にする場合：次回表示時の幅として現在の実幅を保存しつつ、
                ' フォーム幅は表示時に広げた量をそのまま戻す（実幅を使うと誤差が蓄積するため）
                PLWidth = SplitContainer2.Panel1.Width

                ' Panel1を非表示
                SplitContainer2.Panel1Collapsed = True

                Me.SetBounds(Me.Left + _playlistWidthDelta, Me.Top, Me.Width - _playlistWidthDelta, Me.Height)

                Button40.Text = "< PL"
                Button40.ForeColor = Color.Black

            End If
        Finally
            LockWindowUpdate(IntPtr.Zero)
        End Try
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
        M3u8PlaylistStore.Save(filePath, _playlistItems)
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

        Dim loadedItems As List(Of PlaylistItem) = M3u8PlaylistStore.Load(filePath)
        _playlistItems.AddRange(loadedItems)
        For Each item As PlaylistItem In _playlistItems
            AddPlaylistRow(item)
        Next
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

