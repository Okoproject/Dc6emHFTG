Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

''' <summary>
''' WMP API互換のmpv (libmpv) ラッパークラス。
''' P/Invoke経由でlibmpv-2.dllを直接呼び出す。
''' </summary>
Public Class MpvPlayerWrapper
    Implements IDisposable

#Region "libmpv P/Invoke declarations"

    Private Const MpvDll As String = "libmpv-2.dll"

    ' mpv_create / mpv_initialize / mpv_destroy / mpv_terminate_destroy
    <DllImport(MpvDll, CallingConvention:=CallingConvention.Cdecl)>
    Private Shared Function mpv_create() As IntPtr
    End Function

    <DllImport(MpvDll, CallingConvention:=CallingConvention.Cdecl)>
    Private Shared Function mpv_initialize(handle As IntPtr) As Integer
    End Function

    <DllImport(MpvDll, CallingConvention:=CallingConvention.Cdecl)>
    Private Shared Sub mpv_terminate_destroy(handle As IntPtr)
    End Sub

    ' mpv_command / mpv_command_string
    <DllImport(MpvDll, CallingConvention:=CallingConvention.Cdecl)>
    Private Shared Function mpv_command(handle As IntPtr, args() As IntPtr) As Integer
    End Function

    ' mpv_set_option_string
    <DllImport(MpvDll, CallingConvention:=CallingConvention.Cdecl, CharSet:=CharSet.Ansi)>
    Private Shared Function mpv_set_option_string(handle As IntPtr, name As String, data As String) As Integer
    End Function

    ' mpv_set_property_string
    <DllImport(MpvDll, CallingConvention:=CallingConvention.Cdecl, CharSet:=CharSet.Ansi)>
    Private Shared Function mpv_set_property_string(handle As IntPtr, name As String, data As String) As Integer
    End Function

    ' mpv_get_property_string (returns pointer to string that must be freed with mpv_free)
    <DllImport(MpvDll, CallingConvention:=CallingConvention.Cdecl, CharSet:=CharSet.Ansi)>
    Private Shared Function mpv_get_property_string(handle As IntPtr, name As String) As IntPtr
    End Function

    ' mpv_free
    <DllImport(MpvDll, CallingConvention:=CallingConvention.Cdecl)>
    Private Shared Sub mpv_free(data As IntPtr)
    End Sub

    ' mpv_set_option (for wid - int64)
    Private Const MPV_FORMAT_INT64 As Integer = 4
    Private Const MPV_FORMAT_DOUBLE As Integer = 5
    Private Const MPV_FORMAT_FLAG As Integer = 3

    <DllImport(MpvDll, CallingConvention:=CallingConvention.Cdecl, CharSet:=CharSet.Ansi)>
    Private Shared Function mpv_set_option(handle As IntPtr, name As String, format As Integer, ByRef data As Long) As Integer
    End Function

    <DllImport(MpvDll, CallingConvention:=CallingConvention.Cdecl, CharSet:=CharSet.Ansi)>
    Private Shared Function mpv_get_property(handle As IntPtr, name As String, format As Integer, ByRef data As Double) As Integer
    End Function

    <DllImport(MpvDll, CallingConvention:=CallingConvention.Cdecl, CharSet:=CharSet.Ansi)>
    Private Shared Function mpv_set_property(handle As IntPtr, name As String, format As Integer, ByRef data As Double) As Integer
    End Function

    ' mpv_observe_property / mpv_wait_event
    <DllImport(MpvDll, CallingConvention:=CallingConvention.Cdecl, CharSet:=CharSet.Ansi)>
    Private Shared Function mpv_observe_property(handle As IntPtr, reply_userdata As ULong, name As String, format As Integer) As Integer
    End Function

    Private Const MPV_FORMAT_NONE As Integer = 0

    <DllImport(MpvDll, CallingConvention:=CallingConvention.Cdecl)>
    Private Shared Function mpv_wait_event(handle As IntPtr, timeout As Double) As IntPtr
    End Function

    ' mpv_event structure (simplified)
    <StructLayout(LayoutKind.Sequential)>
    Private Structure MpvEvent
        Public event_id As Integer
        Public [error] As Integer
        Public reply_userdata As ULong
        Public data As IntPtr
    End Structure

    Private Const MPV_EVENT_NONE As Integer = 0
    Private Const MPV_EVENT_FILE_LOADED As Integer = 8
    Private Const MPV_EVENT_SHUTDOWN As Integer = 1
    Private Const MPV_EVENT_PROPERTY_CHANGE As Integer = 22

#End Region

    Private _mpvHandle As IntPtr = IntPtr.Zero
    Private _hostPanel As Panel
    Private _sourceURL As String = ""
    Private _mediaName As String = ""
    Private _disposed As Boolean = False
    Private _eventThread As Threading.Thread
    Private _running As Boolean = False

    Public Event MediaChanged()

    ''' <summary>
    ''' mpvプレーヤーを初期化し、指定されたPanelに映像を埋め込む。
    ''' </summary>
    Public Sub New(hostPanel As Panel)
        _hostPanel = hostPanel

        _mpvHandle = mpv_create()
        If _mpvHandle = IntPtr.Zero Then
            Throw New InvalidOperationException("mpv_create failed. libmpv-2.dll が見つからないか、初期化に失敗しました。")
        End If

        ' パネルのウィンドウハンドルをmpvのwid (window ID)に設定
        Dim wid As Long = _hostPanel.Handle.ToInt64()
        mpv_set_option(_mpvHandle, "wid", MPV_FORMAT_INT64, wid)

        ' 高精度シーク有効化
        mpv_set_option_string(_mpvHandle, "hr-seek", "yes")

        ' OSD無効化 (WMPのuiMode="none"相当)
        mpv_set_option_string(_mpvHandle, "osd-level", "0")

        ' キーバインド無効化 (入力はForm1が処理)
        mpv_set_option_string(_mpvHandle, "input-default-bindings", "no")
        mpv_set_option_string(_mpvHandle, "input-vo-keyboard", "no")

        ' 自動再生しない (Kidou=Trueの初回paused動作と同等)
        mpv_set_option_string(_mpvHandle, "pause", "yes")

        ' keep-open: ファイル終了時に自動で閉じない
        mpv_set_option_string(_mpvHandle, "keep-open", "yes")

        Dim err As Integer = mpv_initialize(_mpvHandle)
        If err < 0 Then
            mpv_terminate_destroy(_mpvHandle)
            _mpvHandle = IntPtr.Zero
            Throw New InvalidOperationException("mpv_initialize failed with error code: " & err)
        End If

        ' イベントループスレッド開始
        _running = True
        _eventThread = New Threading.Thread(AddressOf EventLoop)
        _eventThread.IsBackground = True
        _eventThread.Name = "mpv-event-loop"
        _eventThread.Start()
    End Sub

    Private Sub EventLoop()
        While _running AndAlso _mpvHandle <> IntPtr.Zero
            Dim evtPtr As IntPtr = mpv_wait_event(_mpvHandle, 0.5)
            If evtPtr = IntPtr.Zero Then Continue While

            Dim evt As MpvEvent = Marshal.PtrToStructure(Of MpvEvent)(evtPtr)

            Select Case evt.event_id
                Case MPV_EVENT_FILE_LOADED
                    ' UIスレッドでMediaChangedイベント発火
                    If _hostPanel IsNot Nothing AndAlso _hostPanel.IsHandleCreated Then
                        Try
                            _hostPanel.BeginInvoke(Sub() RaiseEvent MediaChanged())
                        Catch ex As ObjectDisposedException
                            ' フォームが既に閉じている場合は無視
                        End Try
                    End If

                Case MPV_EVENT_SHUTDOWN
                    _running = False
                    Exit While
            End Select
        End While
    End Sub

#Region "Properties (WMP互換)"

    ''' <summary>
    ''' 現在の再生位置 (秒)。WMPのCtlcontrols.currentPosition相当。
    ''' </summary>
    Public Property CurrentPosition As Double
        Get
            If _mpvHandle = IntPtr.Zero Then Return 0
            Dim pos As Double = 0
            Dim err = mpv_get_property(_mpvHandle, "time-pos", MPV_FORMAT_DOUBLE, pos)
            If err < 0 Then Return 0
            Return pos
        End Get
        Set(value As Double)
            If _mpvHandle = IntPtr.Zero Then Return
            If value < 0 Then value = 0
            mpv_set_property(_mpvHandle, "time-pos", MPV_FORMAT_DOUBLE, value)
        End Set
    End Property

    ''' <summary>
    ''' メディアの長さ (秒)。WMPのcurrentMedia.duration相当。
    ''' </summary>
    Public ReadOnly Property Duration As Double
        Get
            If _mpvHandle = IntPtr.Zero Then Return 0
            Dim dur As Double = 0
            Dim err = mpv_get_property(_mpvHandle, "duration", MPV_FORMAT_DOUBLE, dur)
            If err < 0 Then Return 0
            Return dur
        End Get
    End Property

    ''' <summary>
    ''' 再生速度。WMPのsettings.rate相当。
    ''' </summary>
    Public Property Rate As Double
        Get
            If _mpvHandle = IntPtr.Zero Then Return 1.0
            Dim spd As Double = 1.0
            mpv_get_property(_mpvHandle, "speed", MPV_FORMAT_DOUBLE, spd)
            Return spd
        End Get
        Set(value As Double)
            If _mpvHandle = IntPtr.Zero Then Return
            If value < 0.1 Then value = 0.1
            If value > 10.0 Then value = 10.0
            mpv_set_property(_mpvHandle, "speed", MPV_FORMAT_DOUBLE, value)
        End Set
    End Property

    ''' <summary>
    ''' 音量 (0-100)。WMPのsettings.volume相当。
    ''' </summary>
    Public Property Volume As Integer
        Get
            If _mpvHandle = IntPtr.Zero Then Return 0
            Dim vol As Double = 0
            mpv_get_property(_mpvHandle, "volume", MPV_FORMAT_DOUBLE, vol)
            Return CInt(vol)
        End Get
        Set(value As Integer)
            If _mpvHandle = IntPtr.Zero Then Return
            If value < 0 Then value = 0
            If value > 100 Then value = 100
            Dim vol As Double = CDbl(value)
            mpv_set_property(_mpvHandle, "volume", MPV_FORMAT_DOUBLE, vol)
        End Set
    End Property

    ''' <summary>
    ''' 再生状態。WMPのplayState相当。1=Stop, 2=Pause, 3=Play。
    ''' </summary>
    Public ReadOnly Property PlayState As Integer
        Get
            If _mpvHandle = IntPtr.Zero Then Return 1

            ' idle-activeチェック (ファイルが読み込まれていない場合)
            Dim idleStr As String = GetPropertyString("idle-active")
            If idleStr = "yes" Then Return 1 ' Stop

            ' pauseチェック
            Dim pauseStr As String = GetPropertyString("pause")
            If pauseStr = "yes" Then Return 2 ' Pause

            Return 3 ' Play
        End Get
    End Property

    ''' <summary>
    ''' メディアファイルのURL。WMPのcurrentMedia.sourceURL相当。
    ''' </summary>
    Public ReadOnly Property SourceURL As String
        Get
            Return _sourceURL
        End Get
    End Property

    ''' <summary>
    ''' メディアファイルの名前。WMPのcurrentMedia.name相当。
    ''' </summary>
    Public ReadOnly Property MediaName As String
        Get
            Return _mediaName
        End Get
    End Property

    ''' <summary>
    ''' URLを設定してファイルを読み込む。WMPの.URL = path相当。
    ''' loadfile コマンドで読み込み、pause状態で開始。
    ''' </summary>
    Public WriteOnly Property URL As String
        Set(value As String)
            If _mpvHandle = IntPtr.Zero Then Return
            If String.IsNullOrEmpty(value) Then Return

            _sourceURL = value
            _mediaName = Path.GetFileName(value)

            ' loadfileコマンドを実行
            DoMpvCommand("loadfile", value)
        End Set
    End Property

#End Region

#Region "Methods (WMP互換)"

    ''' <summary>
    ''' 再生。WMPのCtlcontrols.play()相当。
    ''' </summary>
    Public Sub Play()
        If _mpvHandle = IntPtr.Zero Then Return
        mpv_set_property_string(_mpvHandle, "pause", "no")
    End Sub

    ''' <summary>
    ''' 一時停止。WMPのCtlcontrols.pause()相当。
    ''' </summary>
    Public Sub Pause()
        If _mpvHandle = IntPtr.Zero Then Return
        mpv_set_property_string(_mpvHandle, "pause", "yes")
    End Sub

    ''' <summary>
    ''' 停止。WMPのCtlcontrols.stop()相当。先頭に戻してpause。
    ''' </summary>
    Public Sub [Stop]()
        If _mpvHandle = IntPtr.Zero Then Return
        DoMpvCommand("stop")
    End Sub

#End Region

#Region "Private helpers"

    Private Sub DoMpvCommand(ParamArray args() As String)
        If _mpvHandle = IntPtr.Zero Then Return

        ' null-terminated array of UTF-8 strings
        Dim ptrs(args.Length) As IntPtr ' +1 for null terminator
        Try
            For i = 0 To args.Length - 1
                Dim bytes = System.Text.Encoding.UTF8.GetBytes(args(i) & Chr(0))
                ptrs(i) = Marshal.AllocHGlobal(bytes.Length)
                Marshal.Copy(bytes, 0, ptrs(i), bytes.Length)
            Next
            ptrs(args.Length) = IntPtr.Zero ' null terminator

            mpv_command(_mpvHandle, ptrs)
        Finally
            For i = 0 To args.Length - 1
                If ptrs(i) <> IntPtr.Zero Then
                    Marshal.FreeHGlobal(ptrs(i))
                End If
            Next
        End Try
    End Sub

    Private Function GetPropertyString(name As String) As String
        If _mpvHandle = IntPtr.Zero Then Return ""
        Dim ptr As IntPtr = mpv_get_property_string(_mpvHandle, name)
        If ptr = IntPtr.Zero Then Return ""
        Try
            Return Marshal.PtrToStringAnsi(ptr)
        Finally
            mpv_free(ptr)
        End Try
    End Function

#End Region

#Region "IDisposable"

    Public Sub Dispose() Implements IDisposable.Dispose
        If _disposed Then Return
        _disposed = True

        _running = False

        If _eventThread IsNot Nothing AndAlso _eventThread.IsAlive Then
            _eventThread.Join(2000)
        End If

        If _mpvHandle <> IntPtr.Zero Then
            mpv_terminate_destroy(_mpvHandle)
            _mpvHandle = IntPtr.Zero
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Dispose()
    End Sub

#End Region

End Class
