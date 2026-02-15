Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading

''' <summary>
'''     mpv (libmpv) プレーヤーのラッパークラス
''' </summary>
Public Class MpvPlayerWrapper
    Implements IDisposable

#Region "libmpv P/Invoke declarations"

    Private Const MpvDll As String = "libmpv-2.dll"

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

    Private Const MpvEventFileLoaded As Integer = 8
    Private Const MpvEventShutdown As Integer = 1

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

    ''' <summary>
    '''     mpv プレーヤーを初期化し、指定された Panel に映像を埋め込む。
    ''' </summary>
    Public Sub New(hostPanel As Panel)
        _hostPanel = hostPanel

        _mpvHandle = mpv_create()
        If _mpvHandle = IntPtr.Zero Then
            Throw New InvalidOperationException("mpv_create 失敗。 libmpv-2.dll が見つからないか、初期化に失敗しました。")
        End If

        ' パネルのウィンドウハンドルを mpv の wid (window ID) に設定
        Dim wid As Long = _hostPanel.Handle.ToInt64()
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
    End Sub

    Private Sub EventLoop()
        While _running AndAlso _mpvHandle <> IntPtr.Zero
            Dim evtPtr As IntPtr = mpv_wait_event(_mpvHandle, EventTimeoutSeconds)
            If evtPtr = IntPtr.Zero Then Continue While

            Dim evt = Marshal.PtrToStructure (Of MpvEvent)(evtPtr)

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
            If value < 0 Then value = 0
            mpv_set_property(_mpvHandle, "time-pos", MpvFormatDouble, value)
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
            If value < MinSpeed Then value = MinSpeed
            If value > MaxSpeed Then value = MaxSpeed
            mpv_set_property(_mpvHandle, "speed", MpvFormatDouble, value)
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
            If value < MinVolume Then value = MinVolume
            If value > MaxVolume Then value = MaxVolume
            Dim vol = CDbl(value)
            mpv_set_property(_mpvHandle, "volume", MpvFormatDouble, vol)
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
    '''     停止。先頭に戻してstopコマンドを実行。
    ''' </summary>
    Public Sub [Stop]()
        If _mpvHandle = IntPtr.Zero Then Return
        Position = 0
        DoMpvCommand("stop")
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

    Protected Overrides Sub Finalize()
        Dispose()
    End Sub

#End Region
End Class

