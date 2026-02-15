''' <summary>
'''     グローバルホットキーを管理するモジュール
''' </summary>
Public Module HotKeyManager

#Region "Win32 API"

    Public Declare Function RegisterHotKey Lib "user32" _
        (hwnd As IntPtr, id As Integer,
         fsModifiers As Integer, vk As Keys) As Integer

    Public Declare Function UnregisterHotKey Lib "user32" _
        (hwnd As IntPtr, id As Integer) As Integer

    Private Declare Function GlobalAddAtom Lib "kernel32" Alias "GlobalAddAtomA" _
        (lpString As String) As Short

    Private Declare Function GlobalDeleteAtom Lib "kernel32" _
        (nAtom As Short) As Short

    ' 修飾キー定数
    Public Const ModNone As Integer = 0
    Public Const ModAlt As Integer = &H1
    Public Const ModControl As Integer = &H2
    Public Const ModShift As Integer = &H4

    Public Const WmHotkey As Integer = &H312

#End Region

#Region "ホットキーID辞書"

    ''' <summary>
    '''     ホットキー種別とアトムIDの対応
    ''' </summary>
    Public ReadOnly HotKeyAtoms As New Dictionary(Of HotKeyType, Short)

    ''' <summary>
    '''     ホットキー種別の列挙
    ''' </summary>
    Public Enum HotKeyType
        PlayPause = 0
        PlayPauseWithCounterCopy1
        PlayPauseWithCounterCopy2
        PlayPauseWithCounterCopy3
        StopPlayback
        CounterCopy1
        CounterCopy2
        CounterCopy3
        AddBookmark
        PlayPauseWithBookmark
        SpeedUp
        SpeedDown
        SpeedResetTo1X
        SpeedSetToHalf
        SpeedSetToDouble
        BringWindowToFront
        JumpHotkey1
        JumpHotkey2
        JumpHotkey3
        JumpHotkey4
        JumpHotkey5
        JumpHotkey6
        SpeedControlButton1
        SpeedControlButton2
        SpeedControlButton3
        SpeedControlButton4
        SpeedControlButton5
        SpeedControlButton6
        SpeedControlButton7
        ClipboardJump
    End Enum

#End Region

#Region "修飾キー変換"

    ''' <summary>
    '''     修飾キー設定値とWin32修飾キーの対応
    ''' </summary>
    Private ReadOnly ModifierMapping As New Dictionary(Of Integer, Integer) From {
        {0, ModNone},
        {3, ModControl},
        {4, ModAlt},
        {5, ModShift},
        {7, ModControl Or ModAlt},
        {8, ModControl Or ModShift},
        {9, ModAlt Or ModShift},
        {12, ModControl Or ModShift Or ModAlt}
        }

    ''' <summary>
    '''     設定値からWin32修飾キー値を取得
    ''' </summary>
    Public Function GetModifierValue(settingValue As Integer) As Integer
        If ModifierMapping.ContainsKey(settingValue) Then
            Return ModifierMapping(settingValue)
        End If
        Return ModNone
    End Function

    ''' <summary>
    '''     修飾キー設定値から表示用文字列を取得
    ''' </summary>
    Public Function GetModifierDisplayText(settingValue As Integer) As String
        Select Case settingValue
            Case 0 : Return String.Empty
            Case 3 : Return "Ctrl"
            Case 4 : Return "Alt"
            Case 5 : Return "Shift"
            Case 7 : Return "Ctrl + Alt"
            Case 8 : Return "Ctrl + Shift"
            Case 9 : Return "Alt + Shift"
            Case 12 : Return "Ctrl + Alt + Shift"
            Case Else : Return String.Empty
        End Select
    End Function

#End Region

#Region "ホットキーアトム管理"

    ''' <summary>
    '''     フォームハンドルに対して全ホットキーを生成
    ''' </summary>
    Public Sub CreateHotKeyAtoms(formHandle As IntPtr)
        Dim hashCode As String = formHandle.GetHashCode().ToString()

        For Each hotkeyType As HotKeyType In [Enum].GetValues(GetType(HotKeyType))
            Dim atomName As String = $"GlobalHotKey_{hotkeyType}_{hashCode}"
            HotKeyAtoms(hotkeyType) = GlobalAddAtom(atomName)
        Next
    End Sub

    ''' <summary>
    '''     全ホットキーを解除してアトムを削除
    ''' </summary>
    Public Sub DisposeHotKeys(formHandle As IntPtr)
        For Each kvp As KeyValuePair(Of HotKeyType, Short) In HotKeyAtoms
            UnregisterHotKey(formHandle, kvp.Value)
            GlobalDeleteAtom(kvp.Value)
        Next
        HotKeyAtoms.Clear()
    End Sub

#End Region

#Region "設定プロパティマッピング"

    ''' <summary>
    '''     ホットキー種別と設定プロパティの対応
    ''' </summary>
    Public Function GetSettingModifierProperty(hotkeyType As HotKeyType) As String
        Select Case hotkeyType
            Case HotKeyType.PlayPause : Return "SKAA"
            Case HotKeyType.PlayPauseWithCounterCopy1 : Return "SKBA"
            Case HotKeyType.PlayPauseWithCounterCopy2 : Return "SKCA"
            Case HotKeyType.StopPlayback : Return "SKDA"
            Case HotKeyType.PlayPauseWithCounterCopy3 : Return "SKEA"
            Case HotKeyType.CounterCopy1 : Return "SKTA"
            Case HotKeyType.CounterCopy2 : Return "SKFA"
            Case HotKeyType.CounterCopy3 : Return "SKGA"
            Case HotKeyType.AddBookmark : Return "SKHA"
            Case HotKeyType.PlayPauseWithBookmark : Return "SKIA"
            Case HotKeyType.SpeedUp : Return "SKJA"
            Case HotKeyType.SpeedDown : Return "SKKA"
            Case HotKeyType.SpeedResetTo1X : Return "SKLA"
            Case HotKeyType.SpeedSetToHalf : Return "SKMA"
            Case HotKeyType.SpeedSetToDouble : Return "SKNA"
            Case HotKeyType.BringWindowToFront : Return "SKOA"
            Case HotKeyType.JumpHotkey1 : Return "SKM1A"
            Case HotKeyType.JumpHotkey2 : Return "SKM2A"
            Case HotKeyType.JumpHotkey3 : Return "SKM3A"
            Case HotKeyType.JumpHotkey4 : Return "SKH1A"
            Case HotKeyType.JumpHotkey5 : Return "SKH2A"
            Case HotKeyType.JumpHotkey6 : Return "SKH3A"
            Case HotKeyType.SpeedControlButton1 : Return "SC1A"
            Case HotKeyType.SpeedControlButton2 : Return "SC2A"
            Case HotKeyType.SpeedControlButton3 : Return "SC3A"
            Case HotKeyType.SpeedControlButton4 : Return "SC4A"
            Case HotKeyType.SpeedControlButton5 : Return "SC5A"
            Case HotKeyType.SpeedControlButton6 : Return "SC6A"
            Case HotKeyType.SpeedControlButton7 : Return "SC7A"
            Case HotKeyType.ClipboardJump : Return "SKPA"
            Case Else : Return String.Empty
        End Select
    End Function

    ''' <summary>
    '''     ホットキー種別と設定キー値プロパティの対応
    ''' </summary>
    Public Function GetSettingKeyProperty(hotkeyType As HotKeyType) As String
        Select Case hotkeyType
            Case HotKeyType.PlayPause : Return "SKA"
            Case HotKeyType.PlayPauseWithCounterCopy1 : Return "SKB"
            Case HotKeyType.PlayPauseWithCounterCopy2 : Return "SKC"
            Case HotKeyType.StopPlayback : Return "SKD"
            Case HotKeyType.PlayPauseWithCounterCopy3 : Return "SKE"
            Case HotKeyType.CounterCopy1 : Return "SKT"
            Case HotKeyType.CounterCopy2 : Return "SKF"
            Case HotKeyType.CounterCopy3 : Return "SKG"
            Case HotKeyType.AddBookmark : Return "SKH"
            Case HotKeyType.PlayPauseWithBookmark : Return "SKI"
            Case HotKeyType.SpeedUp : Return "SKJ"
            Case HotKeyType.SpeedDown : Return "SKK"
            Case HotKeyType.SpeedResetTo1X : Return "SKL"
            Case HotKeyType.SpeedSetToHalf : Return "SKM"
            Case HotKeyType.SpeedSetToDouble : Return "SKN"
            Case HotKeyType.BringWindowToFront : Return "SKO"
            Case HotKeyType.JumpHotkey1 : Return "SKM1"
            Case HotKeyType.JumpHotkey2 : Return "SKM2"
            Case HotKeyType.JumpHotkey3 : Return "SKM3"
            Case HotKeyType.JumpHotkey4 : Return "SKH1"
            Case HotKeyType.JumpHotkey5 : Return "SKH2"
            Case HotKeyType.JumpHotkey6 : Return "SKH3"
            Case HotKeyType.SpeedControlButton1 : Return "SKSC1"
            Case HotKeyType.SpeedControlButton2 : Return "SKSC2"
            Case HotKeyType.SpeedControlButton3 : Return "SKSC3"
            Case HotKeyType.SpeedControlButton4 : Return "SKSC4"
            Case HotKeyType.SpeedControlButton5 : Return "SKSC5"
            Case HotKeyType.SpeedControlButton6 : Return "SKSC6"
            Case HotKeyType.SpeedControlButton7 : Return "SKSC7"
            Case HotKeyType.ClipboardJump : Return "SKP"
            Case Else : Return String.Empty
        End Select
    End Function

#End Region
End Module

