Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading
Imports System.IO

''' <summary>
'''     mpv (libmpv) プレーヤーのラッパークラス
''' </summary>
Public Class MpvPlayerWrapper
    Implements IDisposable

#Region "libmpv P/Invoke declarations"

    Private Const MpvDll As String = "libmpv-2.dll"

    <DllImport("kernel32.dll", CharSet := CharSet.Auto, SetLastError := True)>
    Private Shared Function SetDllDirectory(lpPathName As String) As Boolean
    End Function

    ' mpv_create / mpv_initialize / mpv_destroy / mpv_terminate_destroy
    <DllImport(MpvDll, CallingConvention := CallingConvention.Cdecl)>
    Private Shared Function mpv_create() As IntPtr
    End Function

    <DllImport(MpvDll, CallingConvention := CallingConvention.Cdecl)>
    Private Shared Function mpv_initialize(handle As IntPtr) As Integer
    End Function

    <DllImport(MpvDll, CallingConvention := CallingConvention.Cdecl)>
    Private Shared Sub mpv_terminate_destroy(handle As IntPtr)
    End Sub

    ' mpv_command / mpv_command_string
    <DllImport(MpvDll, CallingConvention := CallingConvention.Cdecl)>
    Private Shared Function mpv_command(handle As IntPtr, args() As IntPtr) As Integer
    End Function

    <DllImport(MpvDll, CallingConvention := CallingConvention.Cdecl, CharSet := CharSet.Ansi)>
    Private Shared Function mpv_command_string(handle As IntPtr, command As String) As Integer
    End Function

    ' mpv_set_option_string
    <DllImport(MpvDll, CallingConvention := CallingConvention.Cdecl, CharSet := CharSet.Ansi)>
    Private Shared Function mpv_set_option_string(handle As IntPtr, name As String, data As String) As Integer
    End Function

    ' mpv_set_property_string
    <DllImport(MpvDll, CallingConvention := CallingConvention.Cdecl, CharSet := CharSet.Ansi)>
    Private Shared Function mpv_set_property_string(handle As IntPtr, name As String, data As String) As Integer
    End Function

    ' mpv_get_property_string (returns a pointer to a string that must be freed with mpv_free)
    <DllImport(MpvDll, CallingConvention := CallingConvention.Cdecl, CharSet := CharSet.Ansi)>
    Private Shared Function mpv_get_property_string(handle As IntPtr, name As String) As IntPtr
    End Function

    ' mpv_free
    <DllImport(MpvDll, CallingConvention := CallingConvention.Cdecl)>
    Private Shared Sub mpv_free(data As IntPtr)
    End Sub

    ' mpv_set_option (for wid - int64)
    Private Const MpvFormatInt64 As Integer = 4
    Private Const MpvFormatDouble As Integer = 5

    <DllImport(MpvDll, CallingConvention := CallingConvention.Cdecl, CharSet := CharSet.Ansi)>
    Private Shared Function mpv_set_option(handle As IntPtr, name As String, format As Integer, ByRef data As Long) _
        As Integer
    End Function

    <DllImport(MpvDll, CallingConvention := CallingConvention.Cdecl, CharSet := CharSet.Ansi)>
    Private Shared Function mpv_get_property(handle As IntPtr, name As String, format As Integer, ByRef data As Double) _
        As Integer
    End Function

    <DllImport(MpvDll, CallingConvention := CallingConvention.Cdecl, CharSet := CharSet.Ansi)>
    Private Shared Function mpv_set_property(handle As IntPtr, name As String, format As Integer, ByRef data As Double) _
        As Integer
    End Function

    ' mpv_wait_event
    <DllImport(MpvDll, CallingConvention := CallingConvention.Cdecl)>
    Private Shared Function mpv_wait_event(handle As IntPtr, timeout As Double) As IntPtr
    End Function

    ' mpv_event structure (simplified)
    <StructLayout(LayoutKind.Sequential)>
    Private Structure MpvEvent
        Public EventId As Integer
        Public [Error] As Integer
        Public ReplyUserdata As ULong
        Public Data As IntPtr
    End Structure

    ' mpv_event_end_file structure
    <StructLayout(LayoutKind.Sequential)>
    Private Structure MpvEndFileEventData
        Public Reason As Integer
        Public [Error] As Integer
        Public PlaybackUid As ULong
    End Structure

    Private Const MpvEventFileLoaded As Integer = 8
    Private Const MpvEventShutdown As Integer = 1
    Private Const MpvEventEndFile As Integer = 7

#End Region

#Region "定数"

    ' イベントタイムアウト（秒）
    Private Const EventTimeoutSeconds As Double = 0.5

    ' 再生速度の範囲
    Private Const MinSpeed As Double = 0.1
    Private Const MaxSpeed As Double = 10.0

    ' 音量の範囲
    Private Const MinVolume As Integer = 0
    Private Const MaxVolume As Integer = 100

    ' スレッド終了待機タイムアウト（ミリ秒）
    Private Const ThreadJoinTimeoutMs As Integer = 2000

#End Region

    Private ReadOnly _hostPanel As Panel
    Private _mpvHandle As IntPtr = IntPtr.Zero
    Private _filePath As String = String.Empty
    Private _fileName As String = String.Empty
    Private _disposed As Boolean = False
    Private ReadOnly _eventThread As Thread
    Private _running As Boolean = False

    Public Event MediaChanged()
    Public Event Initialized()
    ''' <summary>ファイルの再生が終了したときに発生（通常の終了のみ）</summary>
    Public Event PlaybackEnded()

    ''' <summary>
    '''     mpv プレーヤーを初期化し、指定された Panel に映像を埋め込む。
    ''' </summary>
    Public Sub New(hostPanel As Panel)
        If hostPanel Is Nothing Then
            Throw New ArgumentNullException("hostPanel")
        End If

        _hostPanel = hostPanel

        ' ClickOnce展開時はdllサブフォルダにlibmpv-2.dllが配置されるため、DLL検索パスに追加
        Dim dllDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dll")
        If Directory.Exists(dllDir) Then
            SetDllDirectory(dllDir)
        End If

        _mpvHandle = mpv_create()
        If _mpvHandle = IntPtr.Zero Then
            Throw New InvalidOperationException("mpv_create 失敗。 libmpv-2.dll が見つからないか、初期化に失敗しました。")
        End If

        ' パネルのウィンドウハンドルを mpv の wid (window ID) に設定
        ' Handle プロパティへのアクセスでネイティブウィンドウハンドルの作成を強制する
        Dim wid As Long = _hostPanel.Handle.ToInt64()

        If wid = 0 Then
            ' ハンドルが0の場合は初期化に失敗する可能性がある
            Throw New InvalidOperationException("Panel のウィンドウハンドルが作成されていません。")
        End If

        mpv_set_option(_mpvHandle, "wid", MpvFormatInt64, wid)

        ' 高精度シーク有効化
        mpv_set_option_string(_mpvHandle, "hr-seek", "yes")

        ' OSD 無効化
        mpv_set_option_string(_mpvHandle, "osd-level", "0")

        ' キーバインド無効化（入力はフォーム側が処理）
        mpv_set_option_string(_mpvHandle, "input-default-bindings", "no")
        mpv_set_option_string(_mpvHandle, "input-vo-keyboard", "no")

        ' 自動再生しない（pause 状態で開始）
        mpv_set_option_string(_mpvHandle, "pause", "yes")

        ' keep-open: ファイル終了時に自動で閉じない
        mpv_set_option_string(_mpvHandle, "keep-open", "yes")

        Dim err As Integer = mpv_initialize(_mpvHandle)
        If err < 0 Then
            mpv_terminate_destroy(_mpvHandle)
            _mpvHandle = IntPtr.Zero
            Throw New InvalidOperationException("mpv_initialize 失敗。 error code: " & err)
        End If

        ' イベントループスレッド開始
        _running = True
        _eventThread = New Thread(AddressOf EventLoop)
        _eventThread.IsBackground = True
        _eventThread.Name = "mpv-event-loop"
        _eventThread.Start()

        Task.Delay(100).ContinueWith(
            Sub()
                If _hostPanel IsNot Nothing AndAlso _hostPanel.IsHandleCreated Then
                    Try
                        _hostPanel.BeginInvoke(Sub() RaiseEvent Initialized())
                    Catch ex As ObjectDisposedException
                    End Try
                End If
            End Sub)
    End Sub

    Private Sub EventLoop()
        While _running AndAlso _mpvHandle <> IntPtr.Zero
            Dim evtPtr As IntPtr = mpv_wait_event(_mpvHandle, EventTimeoutSeconds)
            If evtPtr = IntPtr.Zero Then Continue While

            Dim evt = Marshal.PtrToStructure(Of MpvEvent)(evtPtr)

            Select Case evt.EventId
                Case MpvEventFileLoaded
                    ' UIスレッドでMediaChangedイベント発火
                    If _hostPanel IsNot Nothing AndAlso _hostPanel.IsHandleCreated Then
                        Try
                            _hostPanel.BeginInvoke(Sub() RaiseEvent MediaChanged())
                        Catch ex As ObjectDisposedException
                            ' フォームが既に閉じている場合は無視
                        End Try
                    End If

                Case MpvEventEndFile
                    Dim endFileData = Marshal.PtrToStructure(Of MpvEndFileEventData)(evt.Data)
                    ' Reason 0 = MPV_END_FILE_REASON_EOF (通常の再生終了時のみ)
                    If endFileData.Reason = 0 AndAlso _hostPanel IsNot Nothing AndAlso _hostPanel.IsHandleCreated Then
                        Try
                            _hostPanel.BeginInvoke(Sub() RaiseEvent PlaybackEnded())
                        Catch ex As ObjectDisposedException
                        End Try
                    End If

                Case MpvEventShutdown
                    _running = False
                    Exit While
            End Select
        End While
    End Sub

#Region "プロパティ"

    ''' <summary>
    '''     現在の再生位置 (秒)。mpvの time-pos プロパティ。
    ''' </summary>
    Public Overridable Property Position As Double
        Get
            If _mpvHandle = IntPtr.Zero Then Return 0
            Dim pos As Double = 0
            Dim err = mpv_get_property(_mpvHandle, "time-pos", MpvFormatDouble, pos)
            If err < 0 Then Return 0
            Return pos
        End Get
        Set
            If _mpvHandle = IntPtr.Zero Then Return
            If Value < 0 Then Value = 0
            mpv_set_property(_mpvHandle, "time-pos", MpvFormatDouble, Value)
        End Set
    End Property

    ''' <summary>
    '''     メディアの長さ (秒)。mpvの duration プロパティ。
    ''' </summary>
    Public Overridable ReadOnly Property Duration As Double
        Get
            If _mpvHandle = IntPtr.Zero Then Return 0
            Dim dur As Double = 0
            Dim err = mpv_get_property(_mpvHandle, "duration", MpvFormatDouble, dur)
            If err < 0 Then Return 0
            Return dur
        End Get
    End Property

    ''' <summary>
    '''     再生速度。mpvの speed プロパティ。
    ''' </summary>
    Public Overridable Property Speed As Double
        Get
            If _mpvHandle = IntPtr.Zero Then Return 1.0
            Dim spd = 1.0
            Dim err = mpv_get_property(_mpvHandle, "speed", MpvFormatDouble, spd)
            Return If(err < 0, 1.0, spd)
        End Get
        Set
            If _mpvHandle = IntPtr.Zero Then Return
            If Value < MinSpeed Then Value = MinSpeed
            If Value > MaxSpeed Then Value = MaxSpeed
            mpv_set_property(_mpvHandle, "speed", MpvFormatDouble, Value)
        End Set
    End Property

    ''' <summary>
    '''     音量 (0-100)。mpvの volume プロパティ。
    ''' </summary>
    Public Overridable Property Volume As Integer
        Get
            If _mpvHandle = IntPtr.Zero Then Return 0
            Dim vol As Double = 0
            Dim err = mpv_get_property(_mpvHandle, "volume", MpvFormatDouble, vol)
            Return If(err < 0, 0, CInt(vol))
        End Get
        Set
            If _mpvHandle = IntPtr.Zero Then Return
            If Value < MinVolume Then Value = MinVolume
            If Value > MaxVolume Then Value = MaxVolume
            Dim vol = CDbl(Value)
            mpv_set_property(_mpvHandle, "volume", MpvFormatDouble, vol)
        End Set
    End Property

    ''' <summary>
    '''     ピッチ補正（タイムストレッチ）の有効/無効。mpvの audio-pitch-correction プロパティ。
    ''' true=速度変更時も音程を維持、false=速度に合わせて音程も変化
    ''' </summary>
    Public Property PitchCorrection As Boolean
        Get
            If _mpvHandle = IntPtr.Zero Then Return True
            Return GetPropertyString("audio-pitch-correction") = "yes"
        End Get
        Set
            If _mpvHandle = IntPtr.Zero Then Return
            mpv_set_property_string(_mpvHandle, "audio-pitch-correction", If(Value, "yes", "no"))
        End Set
    End Property

    ''' <summary>
    '''     再生中かどうか。mpvの pause プロパティが "no" かつアイドルでない場合 True。
    ''' </summary>
    Public ReadOnly Property IsPlaying As Boolean
        Get
            If _mpvHandle = IntPtr.Zero Then Return False
            If GetPropertyString("idle-active") = "yes" Then Return False
            Return GetPropertyString("pause") = "no"
        End Get
    End Property

    ''' <summary>
    '''     一時停止中かどうか。mpvの pause プロパティが "yes" かつアイドルでない場合 True。
    ''' </summary>
    Public ReadOnly Property IsPaused As Boolean
        Get
            If _mpvHandle = IntPtr.Zero Then Return False
            If GetPropertyString("idle-active") = "yes" Then Return False
            Return GetPropertyString("pause") = "yes"
        End Get
    End Property

    ''' <summary>
    '''     アイドル状態かどうか（ファイルが読み込まれていない）。mpvの idle-active プロパティ。
    ''' </summary>
    Public ReadOnly Property IsIdle As Boolean
        Get
            If _mpvHandle = IntPtr.Zero Then Return True
            Return GetPropertyString("idle-active") = "yes"
        End Get
    End Property

    ''' <summary>
    '''     現在の音声レベル（dB）。mpvの audio-level プロパティ。
    ''' </summary>
    Public ReadOnly Property AudioLevel As Double
        Get
            If _mpvHandle = IntPtr.Zero Then Return -100
            Dim level As Double = -100
            Dim err = mpv_get_property(_mpvHandle, "audio-level", MpvFormatDouble, level)
            If err < 0 Then Return -100
            Return level
        End Get
    End Property

    ''' <summary>
    '''     メディアファイルのフルパス。
    ''' </summary>
    Public ReadOnly Property FilePath As String
        Get
            Return _filePath
        End Get
    End Property

    ''' <summary>
    '''     メディアファイルのファイル名。
    ''' </summary>
    Public ReadOnly Property FileName As String
        Get
            Return _fileName
        End Get
    End Property

#End Region

#Region "メソッド"

    ''' <summary>
    '''     ファイルを読み込む。mpv の loadfile コマンドで読み込み、pause 状態で開始。
    ''' </summary>
    Public Sub LoadFile(path As String)
        If _mpvHandle = IntPtr.Zero Then Return
        If String.IsNullOrEmpty(path) Then Return

        _filePath = path
        _fileName = IO.Path.GetFileName(path)

        ' loadfile コマンドを実行
        DoMpvCommand("loadfile", path)
    End Sub

    ''' <summary>
    '''     再生。
    ''' </summary>
    Public Sub Play()
        If _mpvHandle = IntPtr.Zero Then Return
        mpv_set_property_string(_mpvHandle, "pause", "no")
    End Sub

    ''' <summary>
    '''     一時停止。
    ''' </summary>
    Public Sub Pause()
        If _mpvHandle = IntPtr.Zero Then Return
        mpv_set_property_string(_mpvHandle, "pause", "yes")
    End Sub

    ''' <summary>
    '''     動画フレームをキャプチャしてファイルに保存
    ''' </summary>
    ''' <param name="outputPath">保存先パス（指定しない場合は自動生成）</param>
    ''' <param name="includeSubtitles">字幕を含めるか</param>
    ''' <returns>保存されたファイルのパス（失敗時はString.Empty）</returns>
    Public Function TakeScreenshot(Optional outputPath As String = "", Optional includeSubtitles As Boolean = True) As String
        If IsDisposed Then Return String.Empty

        ' ファイル名未指定の場合は自動生成
        If String.IsNullOrEmpty(outputPath) Then
            Dim dir = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "OkoshiMAX")
            IO.Directory.CreateDirectory(dir)
            outputPath = IO.Path.Combine(dir, "capture_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".png")
        End If

        ' 絶対パスに変換（mpvは相対パスだと作業ディレクトリ基準になるため）
        outputPath = IO.Path.GetFullPath(outputPath)

        Try
            ' screenshot-to-file コマンド: screenshot-to-file <path> [subtitles|video|window]
            Dim mode = If(includeSubtitles, "subtitles", "video")
            DoMpvCommand("screenshot-to-file", outputPath, mode)
            
            ' 少し待ってファイル存在確認（mpvの書き込みは非同期の場合がある）
            System.Threading.Thread.Sleep(100)
            If IO.File.Exists(outputPath) Then
                Return outputPath
            End If
            
            ' 失敗時は代替コマンドを試行
            DoMpvCommand("screenshot", mode, outputPath)
            System.Threading.Thread.Sleep(100)
            If IO.File.Exists(outputPath) Then
                Return outputPath
            End If
            
            Return String.Empty
        Catch
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    '''     停止。先頭に戻してstopコマンドを実行。
    ''' </summary>
    Public Sub [Stop]()
        If _mpvHandle = IntPtr.Zero Then Return
        Position = 0
        DoMpvCommand("stop")
    End Sub

    ''' <summary>
    '''     イコライザーの周波数帯ごとのゲインを設定する（dB、-12～+12）。
    '''     bands は 10 要素の配列で、31Hz, 62Hz, 125Hz, 250Hz, 500Hz, 1kHz, 2kHz, 4kHz, 8kHz, 16kHz の順。
    ''' </summary>
    Public Sub SetEqualizer(bands() As Double)
        If _mpvHandle = IntPtr.Zero Then Return
        If bands Is Nothing OrElse bands.Length <> 10 Then Return

        Dim freqs() As Integer = {31, 62, 125, 250, 500, 1000, 2000, 4000, 8000, 16000}
        Dim labels() As String = {"eq0", "eq1", "eq2", "eq3", "eq4",
                                  "eq5", "eq6", "eq7", "eq8", "eq9"}

        For i As Integer = 0 To 9
            ' まず既存のバンドをラベルで個別除去（存在しなければ無視）
            DoMpvCommandString("af remove @" & labels(i))

            ' ゲインが 0 以外の場合のみフィルタをラベル付きで追加
            If bands(i) <> 0 Then
                Dim filterStr = String.Format(
                    "equalizer=f={0}:width_type=h:width=100:g={1}",
                    freqs(i), bands(i).ToString("0.0"))
                DoMpvCommandString("af add @" & labels(i) & ":" & filterStr)
            End If
        Next
    End Sub

    ''' <summary>
    '''     ノイズキャンセリングフィルタを追加する。
    ''' </summary>
    Public Sub SetNoiseCancel()
        If _mpvHandle = IntPtr.Zero Then Return
        DoMpvCommandString("af remove @noiseCancel")
        DoMpvCommandString("af add @noiseCancel:afftdn=nf=-25")
    End Sub

    ''' <summary>
    '''     ノイズキャンセリングフィルタを除去する。
    ''' </summary>
    Public Sub ClearNoiseCancel()
        If _mpvHandle = IntPtr.Zero Then Return
        DoMpvCommandString("af remove @noiseCancel")
    End Sub

    ''' <summary>
    '''     ステレオチャンネルのルーティングを設定する。
    ''' </summary>
    ''' <param name="leftToLeft">左→左</param>
    ''' <param name="leftToRight">左→右</param>
    ''' <param name="rightToRight">右→右</param>
    ''' <param name="rightToLeft">右→左</param>
    Public Sub SetChannelRouting(leftToLeft As Boolean, leftToRight As Boolean,
                                  rightToRight As Boolean, rightToLeft As Boolean)
        If _mpvHandle = IntPtr.Zero Then Return
        DoMpvCommandString("af remove @channelRoute")

        Dim outLeftParts As New List(Of String)
        Dim outRightParts As New List(Of String)
        If leftToLeft Then outLeftParts.Add("c0")
        If rightToLeft Then outLeftParts.Add("c1")
        If leftToRight Then outRightParts.Add("c0")
        If rightToRight Then outRightParts.Add("c1")

        Dim outLeft = If(outLeftParts.Count > 0, String.Join("+", outLeftParts), "0")
        Dim outRight = If(outRightParts.Count > 0, String.Join("+", outRightParts), "0")

        Dim filterStr = String.Format("pan=2:{0}:{1}", outLeft, outRight)
        DoMpvCommandString("af add @channelRoute:" & filterStr)
    End Sub

    ''' <summary>
    '''     チャンネルルーティングフィルタを除去する。
    ''' </summary>
    Public Sub ClearChannelRouting()
        If _mpvHandle = IntPtr.Zero Then Return
        DoMpvCommandString("af remove @channelRoute")
    End Sub

    ''' <summary>
    '''     イコライザーをリセット（全バンドのフィルタを除去）する。
    ''' </summary>
    Public Sub ClearEqualizer()
        If _mpvHandle = IntPtr.Zero Then Return
        For i As Integer = 0 To 9
            DoMpvCommandString("af remove @eq" & i)
        Next
    End Sub

#End Region

#Region "Private helpers"

    Private Sub DoMpvCommand(ParamArray args() As String)
        If _mpvHandle = IntPtr.Zero Then Return

        ' null-terminated array of UTF-8 strings
        Dim pointers(args.Length) As IntPtr ' +1 for null terminator
        Try
            For i = 0 To args.Length - 1
                Dim bytes = Encoding.UTF8.GetBytes(args(i) & Chr(0))
                pointers(i) = Marshal.AllocHGlobal(bytes.Length)
                Marshal.Copy(bytes, 0, pointers(i), bytes.Length)
            Next
            pointers(args.Length) = IntPtr.Zero ' null terminator

            mpv_command(_mpvHandle, pointers)
        Finally
            For i = 0 To args.Length - 1
                If pointers(i) <> IntPtr.Zero Then
                    Marshal.FreeHGlobal(pointers(i))
                End If
            Next
        End Try
    End Sub

    ''' <summary>
    '''     mpv のコマンドを文字列で実行する（af 系コマンド用）
    ''' </summary>
    Private Sub DoMpvCommandString(command As String)
        If _mpvHandle = IntPtr.Zero Then Return
        mpv_command_string(_mpvHandle, command)
    End Sub

    Private Function GetPropertyString(name As String) As String
        If _mpvHandle = IntPtr.Zero Then Return String.Empty
        Dim ptr As IntPtr = mpv_get_property_string(_mpvHandle, name)
        If ptr = IntPtr.Zero Then Return String.Empty
        Try
            Return Marshal.PtrToStringAnsi(ptr)
        Finally
            mpv_free(ptr)
        End Try
    End Function

    ''' <summary>
    '''     一時的な mpv インスタンスを使ってファイルの長さを取得する（画面表示なし）
    ''' </summary>
    Public Shared Function GetFileDuration(path As String) As Double
        If String.IsNullOrEmpty(path) OrElse Not IO.File.Exists(path) Then Return 0

        Dim handle = mpv_create()
        If handle = IntPtr.Zero Then Return 0

        ' 映像出力なし
        mpv_set_option_string(handle, "vo", "null")
        mpv_set_option_string(handle, "pause", "yes")

        If mpv_initialize(handle) < 0 Then
            mpv_terminate_destroy(handle)
            Return 0
        End If

        ' loadfile コマンド送信（null-terminated array）
        Dim args(2) As IntPtr
        Dim cmdStr = "loadfile"
        Dim pathStr = path
        Try
            Dim cmdBytes = Encoding.UTF8.GetBytes(cmdStr & Chr(0))
            args(0) = Marshal.AllocHGlobal(cmdBytes.Length)
            Marshal.Copy(cmdBytes, 0, args(0), cmdBytes.Length)

            Dim pathBytes = Encoding.UTF8.GetBytes(pathStr & Chr(0))
            args(1) = Marshal.AllocHGlobal(pathBytes.Length)
            Marshal.Copy(pathBytes, 0, args(1), pathBytes.Length)

            args(2) = IntPtr.Zero ' null terminator

            mpv_command(handle, args)
        Finally
            If args(0) <> IntPtr.Zero Then Marshal.FreeHGlobal(args(0))
            If args(1) <> IntPtr.Zero Then Marshal.FreeHGlobal(args(1))
        End Try

        ' duration プロパティが取得できるまでポーリング（最大3秒）
        Dim dur As Double = 0
        For i As Integer = 0 To 60
            Threading.Thread.Sleep(50)
            Dim err = mpv_get_property(handle, "duration", MpvFormatDouble, dur)
            If err >= 0 AndAlso dur > 0 Then Exit For
        Next

        mpv_terminate_destroy(handle)
        Return If(dur > 0, dur, 0)
    End Function

#End Region

#Region "IDisposable"

    Public Sub Dispose() Implements IDisposable.Dispose
        If _disposed Then Return
        _disposed = True

        _running = False

        If _eventThread IsNot Nothing AndAlso _eventThread.IsAlive Then
            _eventThread.Join(ThreadJoinTimeoutMs)
        End If

        If _mpvHandle <> IntPtr.Zero Then
            mpv_terminate_destroy(_mpvHandle)
            _mpvHandle = IntPtr.Zero
        End If
    End Sub

    Public ReadOnly Property IsDisposed As Boolean
        Get
            Return _disposed OrElse _mpvHandle = IntPtr.Zero
        End Get
    End Property

    Protected Overrides Sub Finalize()
        Dispose()
    End Sub

#End Region
End Class

