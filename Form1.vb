Imports System.ComponentModel
Imports AxWMPLib
Imports System.IO
Imports System.Text
Imports System.Drawing.Imaging
Imports System.Diagnostics
Imports Microsoft.Office.Interop
'Imports Microsoft.VisualBasic.FileIO '使ってない


Module mHotKeys
    'APIの定義
    Public Declare Function RegisterHotKey Lib "user32" _
(ByVal hwnd As IntPtr, ByVal id As Integer,
ByVal fsModifiers As Integer, ByVal vk As Keys) As Integer
    Public Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr,
ByVal id As Integer) As Integer
    Public Declare Function GlobalAddAtom Lib "kernel32" Alias "GlobalAddAtomA" _
(ByVal lpString As String) As Short
    Public Declare Function GlobalDeleteAtom Lib "kernel32" (ByVal nAtom As Short) As Short
    Public Const MOD_ALT As Integer = &H1
    Public Const MOD_CONTROL As Integer = &H2
    Public Const MOD_SHIFT As Integer = &H4
    Public Const MOD_WIN As Integer = &H8
    Public Const WM_HOTKEY As Integer = &H312

    Public hotkeyID_T As Short '
    Public hotkeyID_D As Short '
    Public hotkeyID_A As Short '
    Public hotkeyID_B As Short '
    Public hotkeyID_C As Short '
    Public hotkeyID_E As Short '
    Public hotkeyID_F As Short '

    Public hotkeyID_G As Short '
    Public hotkeyID_H As Short '
    Public hotkeyID_I As Short '
    Public hotkeyID_J As Short '
    Public hotkeyID_K As Short '
    Public hotkeyID_L As Short '
    Public hotkeyID_M As Short '
    Public hotkeyID_N As Short '
    Public hotkeyID_O As Short '
    Public hotkeyID_P As Short '

    Public hotkeyID_Q As Short '
    Public hotkeyID_R As Short '
    Public hotkeyID_S As Short '
    Public hotkeyID_U As Short '
    Public hotkeyID_V As Short '
    Public hotkeyID_W As Short '
    Public hotkeyID_X As Short '
    Public hotkeyID_Y As Short '
    Public hotkeyID_Z As Short '

    Public hotkeyID_MM1 As Short '
    Public hotkeyID_MM2 As Short '
    Public hotkeyID_MM3 As Short '
    Public hotkeyID_HO1 As Short '
    Public hotkeyID_HO2 As Short '
    Public hotkeyID_HO3 As Short '

    Public hotkeyID_SC1 As Short '
    Public hotkeyID_SC2 As Short '
    Public hotkeyID_SC3 As Short '
    Public hotkeyID_SC4 As Short '
    Public hotkeyID_SC5 As Short '
    Public hotkeyID_SC6 As Short '
    Public hotkeyID_SC7 As Short '

    Public Saizenmen As Boolean

End Module
Public Class Form1

    Const CtrlMask As Byte = 8
    Const DanDan As Integer = 26
    Public Sonomamaka As String
    Public AutoB As Integer
    Public Kidou As Boolean = True

    '以下、使ってないが残してる
    Const dan1 As Integer = 7
    Const dan2 As Integer = 33
    Const dan3 As Integer = 59
    Const dan4 As Integer = 85

    '要修正箇所
    '再生終了後、頭に巻き戻る際に実際の再生速度は1.0に戻るが、表示は元のままになっている


    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles MyBase.Closing
        'ホットキーの登録を解除し、アトムを削除する
        UnregisterHotKey(Me.Handle, hotkeyID_T)
        GlobalDeleteAtom(hotkeyID_T)
        UnregisterHotKey(Me.Handle, hotkeyID_D)
        GlobalDeleteAtom(hotkeyID_D)
        UnregisterHotKey(Me.Handle, hotkeyID_A)
        GlobalDeleteAtom(hotkeyID_A)
        UnregisterHotKey(Me.Handle, hotkeyID_B)
        GlobalDeleteAtom(hotkeyID_B)
        UnregisterHotKey(Me.Handle, hotkeyID_C)
        GlobalDeleteAtom(hotkeyID_C)
        UnregisterHotKey(Me.Handle, hotkeyID_E)
        GlobalDeleteAtom(hotkeyID_E)
        UnregisterHotKey(Me.Handle, hotkeyID_F)
        GlobalDeleteAtom(hotkeyID_F)
        UnregisterHotKey(Me.Handle, hotkeyID_G)
        GlobalDeleteAtom(hotkeyID_G)
        UnregisterHotKey(Me.Handle, hotkeyID_H)
        GlobalDeleteAtom(hotkeyID_H)
        UnregisterHotKey(Me.Handle, hotkeyID_I)
        GlobalDeleteAtom(hotkeyID_I)
        UnregisterHotKey(Me.Handle, hotkeyID_J)
        GlobalDeleteAtom(hotkeyID_J)
        UnregisterHotKey(Me.Handle, hotkeyID_K)
        GlobalDeleteAtom(hotkeyID_K)
        UnregisterHotKey(Me.Handle, hotkeyID_L)
        GlobalDeleteAtom(hotkeyID_L)
        UnregisterHotKey(Me.Handle, hotkeyID_M)
        GlobalDeleteAtom(hotkeyID_M)
        UnregisterHotKey(Me.Handle, hotkeyID_N)
        GlobalDeleteAtom(hotkeyID_N)
        UnregisterHotKey(Me.Handle, hotkeyID_O)
        GlobalDeleteAtom(hotkeyID_O)
        UnregisterHotKey(Me.Handle, hotkeyID_P)
        GlobalDeleteAtom(hotkeyID_P)
        UnregisterHotKey(Me.Handle, hotkeyID_Q)
        GlobalDeleteAtom(hotkeyID_Q)
        UnregisterHotKey(Me.Handle, hotkeyID_R)
        GlobalDeleteAtom(hotkeyID_R)
        UnregisterHotKey(Me.Handle, hotkeyID_S)
        GlobalDeleteAtom(hotkeyID_S)
        UnregisterHotKey(Me.Handle, hotkeyID_U)
        GlobalDeleteAtom(hotkeyID_U)
        UnregisterHotKey(Me.Handle, hotkeyID_V)
        GlobalDeleteAtom(hotkeyID_V)
        UnregisterHotKey(Me.Handle, hotkeyID_W)
        GlobalDeleteAtom(hotkeyID_W)
        UnregisterHotKey(Me.Handle, hotkeyID_X)
        GlobalDeleteAtom(hotkeyID_X)
        UnregisterHotKey(Me.Handle, hotkeyID_Y)
        GlobalDeleteAtom(hotkeyID_Y)
        UnregisterHotKey(Me.Handle, hotkeyID_Z)
        GlobalDeleteAtom(hotkeyID_Z)

        UnregisterHotKey(Me.Handle, hotkeyID_MM1)
        GlobalDeleteAtom(hotkeyID_MM1)
        UnregisterHotKey(Me.Handle, hotkeyID_MM2)
        GlobalDeleteAtom(hotkeyID_MM2)
        UnregisterHotKey(Me.Handle, hotkeyID_MM3)
        GlobalDeleteAtom(hotkeyID_MM3)
        UnregisterHotKey(Me.Handle, hotkeyID_HO1)
        GlobalDeleteAtom(hotkeyID_HO1)
        UnregisterHotKey(Me.Handle, hotkeyID_HO2)
        GlobalDeleteAtom(hotkeyID_HO2)
        UnregisterHotKey(Me.Handle, hotkeyID_HO3)
        GlobalDeleteAtom(hotkeyID_HO3)

        UnregisterHotKey(Me.Handle, hotkeyID_SC1)
        GlobalDeleteAtom(hotkeyID_SC1)
        UnregisterHotKey(Me.Handle, hotkeyID_SC2)
        GlobalDeleteAtom(hotkeyID_SC2)
        UnregisterHotKey(Me.Handle, hotkeyID_SC3)
        GlobalDeleteAtom(hotkeyID_SC3)
        UnregisterHotKey(Me.Handle, hotkeyID_SC4)
        GlobalDeleteAtom(hotkeyID_SC4)
        UnregisterHotKey(Me.Handle, hotkeyID_SC5)
        GlobalDeleteAtom(hotkeyID_SC5)
        UnregisterHotKey(Me.Handle, hotkeyID_SC6)
        GlobalDeleteAtom(hotkeyID_SC6)
        UnregisterHotKey(Me.Handle, hotkeyID_SC7)
        GlobalDeleteAtom(hotkeyID_SC7)


        On Error Resume Next

        My.Settings.LastOpenedFile = AxWindowsMediaPlayer1.currentMedia.sourceURL
        My.Settings.LastIchi = AxWindowsMediaPlayer1.Ctlcontrols.currentPosition
        If CheckBox2.Checked = True Then
            My.Settings.gamen = True
        End If

        My.Settings.MyClientSize = Me.ClientSize

        If SplitContainer1.Panel2Collapsed = True Then
            My.Settings.shiori = False
        Else
            My.Settings.shiori = True
        End If

        If SplitContainer2.Panel1Collapsed = True Then
            My.Settings.gamen = False
        Else
            My.Settings.gamen = True
        End If





    End Sub




    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim identity As System.Security.Principal.WindowsIdentity = Security.Principal.WindowsIdentity.GetCurrent()
        Dim path As String = String.Format("{0}\Software\Microsoft\MediaPlayer\Preferences", identity.User.Value)
        Dim key As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.Users.OpenSubKey(path, True)
        If key Is Nothing Then
            Throw New Exception("Error! Registry not found!")
        End If
        key.SetValue("CurrentEffectPreset", identity.User.Value, Microsoft.Win32.RegistryValueKind.DWord)



        If Me.Left < 50 Then
            Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - Me.Width) / 2
        End If
        If Me.Top < 50 Then
            Me.Top = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - Me.Height) / 2
        End If

        AxWindowsMediaPlayer1.Height = My.Settings.p21_height


        If My.Settings.shokai <> 1 Then
            My.Settings.SK1 = 1
            My.Settings.SK2 = 3
            My.Settings.SK3 = 5
            My.Settings.SK4 = 10
            My.Settings.SK5 = 15
            My.Settings.SK6 = 30
            My.Settings.SK7 = 60
            My.Settings.SK8 = 180
            My.Settings.SK9 = 300
            My.Settings.SK10 = 600
            My.Settings.SK11 = -1
            My.Settings.SK12 = -3
            My.Settings.SK13 = -5
            My.Settings.SK14 = -10
            My.Settings.SK15 = -15
            My.Settings.SK16 = -30
            My.Settings.SK17 = -60
            My.Settings.SK18 = -180
            My.Settings.SK19 = -300
            My.Settings.SK20 = -600

            My.Settings.SC1 = 5
            My.Settings.SC2 = 8
            My.Settings.SC3 = 10
            My.Settings.SC3 = 12
            My.Settings.SC4 = 13
            My.Settings.SC5 = 14
            My.Settings.SC6 = 15
            My.Settings.SC7 = 20

            My.Settings.shokai = 1

        End If


        Dim PuraMai As String


        Me.AllowDrop = True

        'AxWindowsMediaPlayer1.settings.autoStart = False
        Kidou = True

        AxWindowsMediaPlayer1.settings.volume = My.Settings.Onryou
        TrackBar6.Value = AxWindowsMediaPlayer1.settings.volume
        Label5.Text = TrackBar6.Value & "%" 'なぜか反映されない

        '===========================================================================
        ' HotKey Id 生成
        '===========================================================================
        ' ホットキーの唯一無二のIDを生成する
        hotkeyID_D = GlobalAddAtom("GlobalHotKey_D " & Me.GetHashCode().ToString())
        hotkeyID_A = GlobalAddAtom("GlobalHotKey_A " & Me.GetHashCode().ToString())
        hotkeyID_B = GlobalAddAtom("GlobalHotKey_B " & Me.GetHashCode().ToString())
        hotkeyID_C = GlobalAddAtom("GlobalHotKey_C " & Me.GetHashCode().ToString())
        hotkeyID_E = GlobalAddAtom("GlobalHotKey_E " & Me.GetHashCode().ToString())
        hotkeyID_F = GlobalAddAtom("GlobalHotKey_F " & Me.GetHashCode().ToString())

        hotkeyID_G = GlobalAddAtom("GlobalHotKey_G " & Me.GetHashCode().ToString())
        hotkeyID_H = GlobalAddAtom("GlobalHotKey_H " & Me.GetHashCode().ToString())
        hotkeyID_I = GlobalAddAtom("GlobalHotKey_I " & Me.GetHashCode().ToString())
        hotkeyID_J = GlobalAddAtom("GlobalHotKey_J " & Me.GetHashCode().ToString())
        hotkeyID_K = GlobalAddAtom("GlobalHotKey_K " & Me.GetHashCode().ToString())
        hotkeyID_L = GlobalAddAtom("GlobalHotKey_L " & Me.GetHashCode().ToString())
        hotkeyID_M = GlobalAddAtom("GlobalHotKey_M " & Me.GetHashCode().ToString())
        hotkeyID_N = GlobalAddAtom("GlobalHotKey_N " & Me.GetHashCode().ToString())
        hotkeyID_O = GlobalAddAtom("GlobalHotKey_O " & Me.GetHashCode().ToString())
        hotkeyID_P = GlobalAddAtom("GlobalHotKey_P " & Me.GetHashCode().ToString())

        hotkeyID_Q = GlobalAddAtom("GlobalHotKey_Q " & Me.GetHashCode().ToString())
        hotkeyID_R = GlobalAddAtom("GlobalHotKey_R " & Me.GetHashCode().ToString())
        hotkeyID_S = GlobalAddAtom("GlobalHotKey_S " & Me.GetHashCode().ToString())
        hotkeyID_T = GlobalAddAtom("GlobalHotKey_T " & Me.GetHashCode().ToString())
        hotkeyID_U = GlobalAddAtom("GlobalHotKey_U " & Me.GetHashCode().ToString())
        hotkeyID_V = GlobalAddAtom("GlobalHotKey_V " & Me.GetHashCode().ToString())
        hotkeyID_W = GlobalAddAtom("GlobalHotKey_W " & Me.GetHashCode().ToString())
        hotkeyID_X = GlobalAddAtom("GlobalHotKey_X " & Me.GetHashCode().ToString())
        hotkeyID_Y = GlobalAddAtom("GlobalHotKey_Y " & Me.GetHashCode().ToString())
        hotkeyID_Z = GlobalAddAtom("GlobalHotKey_Z " & Me.GetHashCode().ToString())

        hotkeyID_MM1 = GlobalAddAtom("GlobalHotKey_MM1 " & Me.GetHashCode().ToString())
        hotkeyID_MM2 = GlobalAddAtom("GlobalHotKey_MM2 " & Me.GetHashCode().ToString())
        hotkeyID_MM3 = GlobalAddAtom("GlobalHotKey_MM3 " & Me.GetHashCode().ToString())
        hotkeyID_HO1 = GlobalAddAtom("GlobalHotKey_HO1 " & Me.GetHashCode().ToString())
        hotkeyID_HO2 = GlobalAddAtom("GlobalHotKey_HO2 " & Me.GetHashCode().ToString())
        hotkeyID_HO3 = GlobalAddAtom("GlobalHotKey_HO3 " & Me.GetHashCode().ToString())

        hotkeyID_SC1 = GlobalAddAtom("GlobalHotKey_SC1 " & Me.GetHashCode().ToString())
        hotkeyID_SC2 = GlobalAddAtom("GlobalHotKey_SC2 " & Me.GetHashCode().ToString())
        hotkeyID_SC3 = GlobalAddAtom("GlobalHotKey_SC3 " & Me.GetHashCode().ToString())
        hotkeyID_SC4 = GlobalAddAtom("GlobalHotKey_SC4 " & Me.GetHashCode().ToString())
        hotkeyID_SC5 = GlobalAddAtom("GlobalHotKey_SC5 " & Me.GetHashCode().ToString())
        hotkeyID_SC6 = GlobalAddAtom("GlobalHotKey_SC6 " & Me.GetHashCode().ToString())
        hotkeyID_SC7 = GlobalAddAtom("GlobalHotKey_SC7 " & Me.GetHashCode().ToString())



        Select Case My.Settings.SKAA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_A, 0, My.Settings.SKA)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_A, MOD_CONTROL, My.Settings.SKA)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_A, MOD_ALT, My.Settings.SKA)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_A, MOD_SHIFT, My.Settings.SKA)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_A, MOD_CONTROL Or MOD_ALT, My.Settings.SKA)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_A, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKA)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_A, MOD_ALT Or MOD_SHIFT, My.Settings.SKA)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_A, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKA)

        End Select

        Select Case My.Settings.SKBA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_B, 0, My.Settings.SKB)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_B, MOD_CONTROL, My.Settings.SKB)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_B, MOD_ALT, My.Settings.SKB)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_B, MOD_SHIFT, My.Settings.SKB)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_B, MOD_CONTROL Or MOD_ALT, My.Settings.SKB)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_B, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKB)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_B, MOD_ALT Or MOD_SHIFT, My.Settings.SKB)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_B, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKB)

        End Select


        Select Case My.Settings.SKCA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_C, 0, My.Settings.SKC)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_C, MOD_CONTROL, My.Settings.SKC)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_C, MOD_ALT, My.Settings.SKC)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_C, MOD_SHIFT, My.Settings.SKC)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_C, MOD_CONTROL Or MOD_ALT, My.Settings.SKC)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_C, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKC)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_C, MOD_ALT Or MOD_SHIFT, My.Settings.SKC)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_C, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKC)

        End Select

        Select Case My.Settings.SKDA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_D, 0, My.Settings.SKD)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_D, MOD_CONTROL, My.Settings.SKD)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_D, MOD_ALT, My.Settings.SKD)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_D, MOD_SHIFT, My.Settings.SKD)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_D, MOD_CONTROL Or MOD_ALT, My.Settings.SKD)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_D, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKD)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_D, MOD_ALT Or MOD_SHIFT, My.Settings.SKD)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_D, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKD)

        End Select

        Select Case My.Settings.SKEA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_E, 0, My.Settings.SKE)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_E, MOD_CONTROL, My.Settings.SKE)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_E, MOD_ALT, My.Settings.SKE)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_E, MOD_SHIFT, My.Settings.SKE)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_E, MOD_CONTROL Or MOD_ALT, My.Settings.SKE)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_E, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKE)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_E, MOD_ALT Or MOD_SHIFT, My.Settings.SKE)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_E, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKE)

        End Select

        Select Case My.Settings.SKFA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_F, 0, My.Settings.SKF)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_F, MOD_CONTROL, My.Settings.SKF)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_F, MOD_ALT, My.Settings.SKF)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_F, MOD_SHIFT, My.Settings.SKF)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_F, MOD_CONTROL Or MOD_ALT, My.Settings.SKF)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_F, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKF)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_F, MOD_ALT Or MOD_SHIFT, My.Settings.SKF)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_F, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKF)

        End Select

        Select Case My.Settings.SKGA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_G, 0, My.Settings.SKG)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_G, MOD_CONTROL, My.Settings.SKG)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_G, MOD_ALT, My.Settings.SKG)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_G, MOD_SHIFT, My.Settings.SKG)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_G, MOD_CONTROL Or MOD_ALT, My.Settings.SKG)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_G, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKG)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_G, MOD_ALT Or MOD_SHIFT, My.Settings.SKG)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_G, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKG)

        End Select

        Select Case My.Settings.SKHA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_H, 0, My.Settings.SKH)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_H, MOD_CONTROL, My.Settings.SKH)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_H, MOD_ALT, My.Settings.SKH)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_H, MOD_SHIFT, My.Settings.SKH)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_H, MOD_CONTROL Or MOD_ALT, My.Settings.SKH)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_H, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKH)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_H, MOD_ALT Or MOD_SHIFT, My.Settings.SKH)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_H, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKH)

        End Select

        Select Case My.Settings.SKIA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_I, 0, My.Settings.SKI)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_I, MOD_CONTROL, My.Settings.SKI)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_I, MOD_ALT, My.Settings.SKI)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_I, MOD_SHIFT, My.Settings.SKI)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_I, MOD_CONTROL Or MOD_ALT, My.Settings.SKI)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_I, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKI)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_I, MOD_ALT Or MOD_SHIFT, My.Settings.SKI)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_I, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKI)

        End Select

        Select Case My.Settings.SKJA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_J, 0, My.Settings.SKJ)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_J, MOD_CONTROL, My.Settings.SKJ)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_J, MOD_ALT, My.Settings.SKJ)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_J, MOD_SHIFT, My.Settings.SKJ)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_J, MOD_CONTROL Or MOD_ALT, My.Settings.SKJ)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_J, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKJ)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_J, MOD_ALT Or MOD_SHIFT, My.Settings.SKJ)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_J, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKJ)

        End Select

        Select Case My.Settings.SKKA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_K, 0, My.Settings.SKK)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_K, MOD_CONTROL, My.Settings.SKK)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_K, MOD_ALT, My.Settings.SKK)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_K, MOD_SHIFT, My.Settings.SKK)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_K, MOD_CONTROL Or MOD_ALT, My.Settings.SKK)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_K, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKK)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_K, MOD_ALT Or MOD_SHIFT, My.Settings.SKK)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_K, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKK)

        End Select

        Select Case My.Settings.SKLA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_L, 0, My.Settings.SKL)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_L, MOD_CONTROL, My.Settings.SKL)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_L, MOD_ALT, My.Settings.SKL)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_L, MOD_SHIFT, My.Settings.SKL)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_L, MOD_CONTROL Or MOD_ALT, My.Settings.SKL)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_L, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKL)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_L, MOD_ALT Or MOD_SHIFT, My.Settings.SKL)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_L, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKL)

        End Select

        Select Case My.Settings.SKMA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_M, 0, My.Settings.SKM)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_M, MOD_CONTROL, My.Settings.SKM)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_M, MOD_ALT, My.Settings.SKM)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_M, MOD_SHIFT, My.Settings.SKM)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_M, MOD_CONTROL Or MOD_ALT, My.Settings.SKM)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_M, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKM)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_M, MOD_ALT Or MOD_SHIFT, My.Settings.SKM)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_M, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKM)

        End Select

        Select Case My.Settings.SKNA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_N, 0, My.Settings.SKN)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_N, MOD_CONTROL, My.Settings.SKN)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_N, MOD_ALT, My.Settings.SKN)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_N, MOD_SHIFT, My.Settings.SKN)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_N, MOD_CONTROL Or MOD_ALT, My.Settings.SKN)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_N, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKN)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_N, MOD_ALT Or MOD_SHIFT, My.Settings.SKN)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_N, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKN)

        End Select

        Select Case My.Settings.SKOA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_O, 0, My.Settings.SKO)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_O, MOD_CONTROL, My.Settings.SKO)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_O, MOD_ALT, My.Settings.SKO)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_O, MOD_SHIFT, My.Settings.SKO)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_O, MOD_CONTROL Or MOD_ALT, My.Settings.SKO)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_O, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKO)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_O, MOD_ALT Or MOD_SHIFT, My.Settings.SKO)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_O, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKO)

        End Select

        Select Case My.Settings.SKPA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_P, 0, My.Settings.SKP)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_P, MOD_CONTROL, My.Settings.SKP)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_P, MOD_ALT, My.Settings.SKP)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_P, MOD_SHIFT, My.Settings.SKP)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_P, MOD_CONTROL Or MOD_ALT, My.Settings.SKP)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_P, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKP)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_P, MOD_ALT Or MOD_SHIFT, My.Settings.SKP)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_P, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKP)

        End Select

        Select Case My.Settings.SKQA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_Q, 0, My.Settings.SKQ)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_Q, MOD_CONTROL, My.Settings.SKQ)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_Q, MOD_ALT, My.Settings.SKQ)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_Q, MOD_SHIFT, My.Settings.SKQ)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_Q, MOD_CONTROL Or MOD_ALT, My.Settings.SKQ)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_Q, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKQ)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_Q, MOD_ALT Or MOD_SHIFT, My.Settings.SKQ)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_Q, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKQ)

        End Select

        Select Case My.Settings.SKRA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_R, 0, My.Settings.SKR)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_R, MOD_CONTROL, My.Settings.SKR)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_R, MOD_ALT, My.Settings.SKR)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_R, MOD_SHIFT, My.Settings.SKR)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_R, MOD_CONTROL Or MOD_ALT, My.Settings.SKR)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_R, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKR)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_R, MOD_ALT Or MOD_SHIFT, My.Settings.SKR)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_R, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKR)

        End Select

        Select Case My.Settings.SKSA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_S, 0, My.Settings.SKS)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_S, MOD_CONTROL, My.Settings.SKS)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_S, MOD_ALT, My.Settings.SKS)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_S, MOD_SHIFT, My.Settings.SKS)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_S, MOD_CONTROL Or MOD_ALT, My.Settings.SKS)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_S, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKS)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_S, MOD_ALT Or MOD_SHIFT, My.Settings.SKS)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_S, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKS)

        End Select

        Select Case My.Settings.SKTA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_T, 0, My.Settings.SKT)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_T, MOD_CONTROL, My.Settings.SKT)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_T, MOD_ALT, My.Settings.SKT)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_T, MOD_SHIFT, My.Settings.SKT)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_T, MOD_CONTROL Or MOD_ALT, My.Settings.SKT)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_T, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKT)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_T, MOD_ALT Or MOD_SHIFT, My.Settings.SKT)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_T, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKT)

        End Select

        Select Case My.Settings.SKUA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_U, 0, My.Settings.SKU)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_U, MOD_CONTROL, My.Settings.SKU)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_U, MOD_ALT, My.Settings.SKU)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_U, MOD_SHIFT, My.Settings.SKU)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_U, MOD_CONTROL Or MOD_ALT, My.Settings.SKU)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_U, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKU)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_U, MOD_ALT Or MOD_SHIFT, My.Settings.SKU)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_U, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKU)

        End Select

        Select Case My.Settings.SKVA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_V, 0, My.Settings.SKV)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_V, MOD_CONTROL, My.Settings.SKV)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_V, MOD_ALT, My.Settings.SKV)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_V, MOD_SHIFT, My.Settings.SKV)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_V, MOD_CONTROL Or MOD_ALT, My.Settings.SKV)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_V, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKV)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_V, MOD_ALT Or MOD_SHIFT, My.Settings.SKV)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_V, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKV)

        End Select

        Select Case My.Settings.SKWA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_W, 0, My.Settings.SKW)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_W, MOD_CONTROL, My.Settings.SKW)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_W, MOD_ALT, My.Settings.SKW)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_W, MOD_SHIFT, My.Settings.SKW)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_W, MOD_CONTROL Or MOD_ALT, My.Settings.SKW)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_W, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKW)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_W, MOD_ALT Or MOD_SHIFT, My.Settings.SKW)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_W, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKW)

        End Select

        Select Case My.Settings.SKXA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_X, 0, My.Settings.SKX)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_X, MOD_CONTROL, My.Settings.SKX)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_X, MOD_ALT, My.Settings.SKX)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_X, MOD_SHIFT, My.Settings.SKX)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_X, MOD_CONTROL Or MOD_ALT, My.Settings.SKX)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_X, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKX)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_X, MOD_ALT Or MOD_SHIFT, My.Settings.SKX)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_X, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKX)

        End Select

        Select Case My.Settings.SKYA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_Y, 0, My.Settings.SKY)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_Y, MOD_CONTROL, My.Settings.SKY)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_Y, MOD_ALT, My.Settings.SKY)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_Y, MOD_SHIFT, My.Settings.SKY)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_Y, MOD_CONTROL Or MOD_ALT, My.Settings.SKY)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_Y, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKY)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_Y, MOD_ALT Or MOD_SHIFT, My.Settings.SKY)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_Y, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKY)

        End Select

        Select Case My.Settings.SKZA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_Z, 0, My.Settings.SKZ)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_Z, MOD_CONTROL, My.Settings.SKZ)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_Z, MOD_ALT, My.Settings.SKZ)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_Z, MOD_SHIFT, My.Settings.SKZ)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_Z, MOD_CONTROL Or MOD_ALT, My.Settings.SKZ)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_Z, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKZ)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_Z, MOD_ALT Or MOD_SHIFT, My.Settings.SKZ)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_Z, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKZ)

        End Select

        Select Case My.Settings.SKM1A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_MM1, 0, My.Settings.SKM1)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_MM1, MOD_CONTROL, My.Settings.SKM1)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_MM1, MOD_ALT, My.Settings.SKM1)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_MM1, MOD_SHIFT, My.Settings.SKM1)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_MM1, MOD_CONTROL Or MOD_ALT, My.Settings.SKM1)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_MM1, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKM1)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_MM1, MOD_ALT Or MOD_SHIFT, My.Settings.SKM1)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_MM1, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKM1)

        End Select

        Select Case My.Settings.SKM2A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_MM2, 0, My.Settings.SKM2)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_MM2, MOD_CONTROL, My.Settings.SKM2)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_MM2, MOD_ALT, My.Settings.SKM2)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_MM2, MOD_SHIFT, My.Settings.SKM2)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_MM2, MOD_CONTROL Or MOD_ALT, My.Settings.SKM2)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_MM2, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKM2)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_MM2, MOD_ALT Or MOD_SHIFT, My.Settings.SKM2)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_MM2, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKM2)

        End Select

        Select Case My.Settings.SKM3A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_MM3, 0, My.Settings.SKM3)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_MM3, MOD_CONTROL, My.Settings.SKM3)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_MM3, MOD_ALT, My.Settings.SKM3)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_MM3, MOD_SHIFT, My.Settings.SKM3)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_MM3, MOD_CONTROL Or MOD_ALT, My.Settings.SKM3)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_MM3, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKM3)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_MM3, MOD_ALT Or MOD_SHIFT, My.Settings.SKM3)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_MM3, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKM3)

        End Select

        Select Case My.Settings.SKH1A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_HO1, 0, My.Settings.SKH1)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_HO1, MOD_CONTROL, My.Settings.SKH1)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_HO1, MOD_ALT, My.Settings.SKH1)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_HO1, MOD_SHIFT, My.Settings.SKH1)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_HO1, MOD_CONTROL Or MOD_ALT, My.Settings.SKH1)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_HO1, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKH1)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_HO1, MOD_ALT Or MOD_SHIFT, My.Settings.SKH1)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_HO1, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKH1)

        End Select

        Select Case My.Settings.SKH2A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_HO2, 0, My.Settings.SKH2)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_HO2, MOD_CONTROL, My.Settings.SKH2)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_HO2, MOD_ALT, My.Settings.SKH2)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_HO2, MOD_SHIFT, My.Settings.SKH2)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_HO2, MOD_CONTROL Or MOD_ALT, My.Settings.SKH2)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_HO2, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKH2)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_HO2, MOD_ALT Or MOD_SHIFT, My.Settings.SKH2)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_HO2, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKH2)

        End Select

        Select Case My.Settings.SKH3A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_HO3, 0, My.Settings.SKH3)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_HO3, MOD_CONTROL, My.Settings.SKH3)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_HO3, MOD_ALT, My.Settings.SKH3)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_HO3, MOD_SHIFT, My.Settings.SKH3)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_HO3, MOD_CONTROL Or MOD_ALT, My.Settings.SKH3)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_HO3, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKH3)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_HO3, MOD_ALT Or MOD_SHIFT, My.Settings.SKH3)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_HO3, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKH3)

        End Select

        Select Case My.Settings.SC1A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_SC1, 0, My.Settings.SKSC1)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_SC1, MOD_CONTROL, My.Settings.SKSC1)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_SC1, MOD_ALT, My.Settings.SKSC1)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_SC1, MOD_SHIFT, My.Settings.SKSC1)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_SC1, MOD_CONTROL Or MOD_ALT, My.Settings.SKSC1)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_SC1, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKSC1)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_SC1, MOD_ALT Or MOD_SHIFT, My.Settings.SKSC1)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_SC1, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKSC1)

        End Select

        Select Case My.Settings.SC2A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_SC2, 0, My.Settings.SKSC2)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_SC2, MOD_CONTROL, My.Settings.SKSC2)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_SC2, MOD_ALT, My.Settings.SKSC2)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_SC2, MOD_SHIFT, My.Settings.SKSC2)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_SC2, MOD_CONTROL Or MOD_ALT, My.Settings.SKSC2)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_SC2, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKSC2)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_SC2, MOD_ALT Or MOD_SHIFT, My.Settings.SKSC2)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_SC2, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKSC2)

        End Select

        Select Case My.Settings.SC3A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_SC3, 0, My.Settings.SKSC3)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_SC3, MOD_CONTROL, My.Settings.SKSC3)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_SC3, MOD_ALT, My.Settings.SKSC3)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_SC3, MOD_SHIFT, My.Settings.SKSC3)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_SC3, MOD_CONTROL Or MOD_ALT, My.Settings.SKSC3)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_SC3, MOD_CONTROL Or MOD_SHIFT, My.Settings.SC3)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_SC3, MOD_ALT Or MOD_SHIFT, My.Settings.SKSC3)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_SC3, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKSC3)

        End Select

        Select Case My.Settings.SC4A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_SC4, 0, My.Settings.SKSC4)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_SC4, MOD_CONTROL, My.Settings.SKSC4)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_SC4, MOD_ALT, My.Settings.SKSC4)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_SC4, MOD_SHIFT, My.Settings.SKSC4)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_SC4, MOD_CONTROL Or MOD_ALT, My.Settings.SKSC4)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_SC4, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKSC4)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_SC4, MOD_ALT Or MOD_SHIFT, My.Settings.SKSC4)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_SC4, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKSC4)

        End Select

        Select Case My.Settings.SC5A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_SC5, 0, My.Settings.SKSC5)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_SC5, MOD_CONTROL, My.Settings.SKSC5)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_SC5, MOD_ALT, My.Settings.SKSC5)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_SC5, MOD_SHIFT, My.Settings.SKSC5)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_SC5, MOD_CONTROL Or MOD_ALT, My.Settings.SKSC5)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_SC5, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKSC5)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_SC5, MOD_ALT Or MOD_SHIFT, My.Settings.SKSC5)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_SC5, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKSC5)

        End Select

        Select Case My.Settings.SC6A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_SC6, 0, My.Settings.SKSC6)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_SC6, MOD_CONTROL, My.Settings.SKSC6)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_SC6, MOD_ALT, My.Settings.SKSC6)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_SC6, MOD_SHIFT, My.Settings.SKSC6)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_SC6, MOD_CONTROL Or MOD_ALT, My.Settings.SKSC6)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_SC6, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKSC6)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_SC6, MOD_ALT Or MOD_SHIFT, My.Settings.SKSC6)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_SC6, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKSC6)

        End Select

        Select Case My.Settings.SC7A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_SC7, 0, My.Settings.SKSC7)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_SC7, MOD_CONTROL, My.Settings.SKSC7)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_SC7, MOD_ALT, My.Settings.SKSC7)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_SC7, MOD_SHIFT, My.Settings.SKSC7)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_SC7, MOD_CONTROL Or MOD_ALT, My.Settings.SKSC7)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_SC7, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKSC7)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_SC7, MOD_ALT Or MOD_SHIFT, My.Settings.SKSC7)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_SC7, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKSC7)

        End Select

        My.Settings.Save()



        'Player本体のコントロールをすべて無効
        AxWindowsMediaPlayer1.uiMode = "none"

        'ジャンプボタン表示用
        PuraMai = ""
        If System.Math.Abs(My.Settings.SK1) >= 60 Then
            If My.Settings.SK1 > 0 Then
                PuraMai = "+"
            End If
            Button1.Text = PuraMai & My.Settings.SK1 / 60 & "m"
        Else
            If My.Settings.SK1 > 0 Then
                PuraMai = "+"
            End If
            Button1.Text = PuraMai & My.Settings.SK1 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK11) >= 60 Then

            If My.Settings.SK11 > 0 Then
                PuraMai = "+"
            End If
            Button11.Text = PuraMai & My.Settings.SK11 / 60 & "m"
        Else
            If My.Settings.SK11 > 0 Then
                PuraMai = "+"
            End If
            Button11.Text = PuraMai & My.Settings.SK11 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK2) >= 60 Then
            If My.Settings.SK2 > 0 Then
                PuraMai = "+"
            End If
            Button2.Text = PuraMai & My.Settings.SK2 / 60 & "m"
        Else
            If My.Settings.SK2 > 0 Then
                PuraMai = "+"
            End If
            Button2.Text = PuraMai & My.Settings.SK2 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK12) >= 60 Then
            If My.Settings.SK12 > 0 Then
                PuraMai = "+"
            End If
            Button12.Text = PuraMai & My.Settings.SK12 / 60 & "m"
        Else
            If My.Settings.SK12 > 0 Then
                PuraMai = "+"
            End If
            Button12.Text = PuraMai & My.Settings.SK12 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK3) >= 60 Then
            If My.Settings.SK3 > 0 Then
                PuraMai = "+"
            End If
            Button3.Text = PuraMai & My.Settings.SK3 / 60 & "m"
        Else
            If My.Settings.SK3 > 0 Then
                PuraMai = "+"
            End If
            Button3.Text = PuraMai & My.Settings.SK3 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK13) >= 60 Then
            If My.Settings.SK13 > 0 Then
                PuraMai = "+"
            End If
            Button13.Text = PuraMai & My.Settings.SK13 / 60 & "m"
        Else
            If My.Settings.SK13 > 0 Then
                PuraMai = "+"
            End If
            Button13.Text = PuraMai & My.Settings.SK13 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK4) >= 60 Then
            If My.Settings.SK4 > 0 Then
                PuraMai = "+"
            End If
            Button4.Text = PuraMai & My.Settings.SK4 / 60 & "m"
        Else
            If My.Settings.SK4 > 0 Then
                PuraMai = "+"
            End If
            Button4.Text = PuraMai & My.Settings.SK4 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK14) >= 60 Then
            If My.Settings.SK14 > 0 Then
                PuraMai = "+"
            End If
            Button14.Text = PuraMai & My.Settings.SK14 / 60 & "m"
        Else
            If My.Settings.SK14 > 0 Then
                PuraMai = "+"
            End If
            Button14.Text = PuraMai & My.Settings.SK14 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK5) >= 60 Then
            If My.Settings.SK5 > 0 Then
                PuraMai = "+"
            End If
            Button5.Text = PuraMai & My.Settings.SK5 / 60 & "m"
        Else
            If My.Settings.SK5 > 0 Then
                PuraMai = "+"
            End If
            Button5.Text = PuraMai & My.Settings.SK5 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK15) >= 60 Then
            If My.Settings.SK15 > 0 Then
                PuraMai = "+"
            End If
            Button15.Text = PuraMai & My.Settings.SK15 / 60 & "m"
        Else
            If My.Settings.SK15 > 0 Then
                PuraMai = "+"
            End If
            Button15.Text = PuraMai & My.Settings.SK15 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK6) >= 60 Then
            If My.Settings.SK6 > 0 Then
                PuraMai = "+"
            End If
            Button6.Text = PuraMai & My.Settings.SK6 / 60 & "m"
        Else
            If My.Settings.SK6 > 0 Then
                PuraMai = "+"
            End If
            Button6.Text = PuraMai & My.Settings.SK6 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK16) >= 60 Then
            If My.Settings.SK16 > 0 Then
                PuraMai = "+"
            End If
            Button16.Text = PuraMai & My.Settings.SK16 / 60 & "m"
        Else
            If My.Settings.SK16 > 0 Then
                PuraMai = "+"
            End If
            Button16.Text = PuraMai & My.Settings.SK16 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK7) >= 60 Then
            If My.Settings.SK7 > 0 Then
                PuraMai = "+"
            End If
            Button7.Text = PuraMai & My.Settings.SK7 / 60 & "m"
        Else
            If My.Settings.SK7 > 0 Then
                PuraMai = "+"
            End If
            Button7.Text = PuraMai & My.Settings.SK7 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK17) >= 60 Then
            If My.Settings.SK17 > 0 Then
                PuraMai = "+"
            End If
            Button17.Text = PuraMai & My.Settings.SK17 / 60 & "m"
        Else
            If My.Settings.SK17 > 0 Then
                PuraMai = "+"
            End If
            Button17.Text = PuraMai & My.Settings.SK17 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK8) >= 60 Then
            If My.Settings.SK8 > 0 Then
                PuraMai = "+"
            End If
            Button8.Text = PuraMai & My.Settings.SK8 / 60 & "m"
        Else
            If My.Settings.SK8 > 0 Then
                PuraMai = "+"
            End If
            Button8.Text = PuraMai & My.Settings.SK8 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK18) >= 60 Then
            If My.Settings.SK18 > 0 Then
                PuraMai = "+"
            End If
            Button18.Text = PuraMai & My.Settings.SK18 / 60 & "m"
        Else
            If My.Settings.SK18 > 0 Then
                PuraMai = "+"
            End If
            Button18.Text = PuraMai & My.Settings.SK18 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK9) >= 60 Then
            If My.Settings.SK9 > 0 Then
                PuraMai = "+"
            End If
            Button9.Text = PuraMai & My.Settings.SK9 / 60 & "m"
        Else
            If My.Settings.SK9 > 0 Then
                PuraMai = "+"
            End If
            Button9.Text = PuraMai & My.Settings.SK9 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK19) >= 60 Then
            If My.Settings.SK19 > 0 Then
                PuraMai = "+"
            End If
            Button19.Text = PuraMai & My.Settings.SK19 / 60 & "m"
        Else
            If My.Settings.SK19 > 0 Then
                PuraMai = "+"
            End If
            Button19.Text = PuraMai & My.Settings.SK19 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK10) >= 60 Then
            If My.Settings.SK10 > 0 Then
                PuraMai = "+"
            End If
            Button10.Text = PuraMai & My.Settings.SK10 / 60 & "m"
        Else
            If My.Settings.SK10 > 0 Then
                PuraMai = "+"
            End If
            Button10.Text = PuraMai & My.Settings.SK10 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK20) >= 60 Then
            If My.Settings.SK20 > 0 Then
                PuraMai = "+"
            End If
            Button20.Text = PuraMai & My.Settings.SK20 / 60 & "m"
        Else
            If My.Settings.SK20 > 0 Then
                PuraMai = "+"
            End If
            Button20.Text = PuraMai & My.Settings.SK20 & "s"
        End If

        Button21.Text = (My.Settings.SC1 / 100).ToString("0.0")
        Button22.Text = (My.Settings.SC2 / 100).ToString("0.0")
        Button23.Text = (My.Settings.SC3 / 100).ToString("0.0")
        Button24.Text = (My.Settings.SC4 / 100).ToString("0.0")
        Button25.Text = (My.Settings.SC5 / 100).ToString("0.0")
        Button26.Text = (My.Settings.SC6 / 100).ToString("0.0")
        Button27.Text = (My.Settings.SC7 / 100).ToString("0.0")




        '------------
        'ボタン等のコントロールに対する通常の処理内容
        '------------
        'ボタンへの表示内容を指定
        'Button1.Text = "OK"
        'ピクチャボックスに表示する画像ファイルを指定
        'PictureBox1.Image = New Bitmap("C:\背景.bmp") ' 画像
        'ピクチャボックスの範囲内に画像が収まる表示にするように指定
        'PictureBox1.SizeMode = PictureBoxSizeMode.Zoom

        '------------
        ' スケーリングに対する処理内容
        '------------



        Me.StartPosition = FormStartPosition.CenterScreen
        '自動巻戻時間の設定
        AutoB = My.Settings.AutoBack
        '動画再生画面の表示・非表示
        If My.Settings.gamen = True Then
            CheckBox2.Checked = True
            SplitContainer2.Panel1Collapsed = False
        Else
            CheckBox2.Checked = False
            SplitContainer2.Panel1Collapsed = True
        End If

        'しおり一覧の表示・非表示
        If My.Settings.shiori = True Then
            SplitContainer1.Panel2Collapsed = False
        Else
            SplitContainer1.Panel2Collapsed = True
        End If

        'On Error Resume Next

        'AxWindowsMediaPlayer1.settings.autoStart = False



        AxWindowsMediaPlayer1.URL = My.Settings.LastOpenedFile
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = My.Settings.LastIchi

        ToolTip1.SetToolTip(TrackBar1, TimeSpan.FromSeconds(TrackBar1.Value).ToString("hh\:mm\:ss"))

        Saizenmen = False
        Me.TopMost = False



        Me.ClientSize = My.Settings.MyClientSize


        'If Kidou = True Then
        'AxWindowsMediaPlayer1.Ctlcontrols.pause()
        'Kidou = False
        'End If



    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        My.Settings.p11_height = SplitContainer1.Panel1.Height
        My.Settings.p11_width = SplitContainer1.Panel2.Width
        My.Settings.p12_height = SplitContainer1.Panel2.Height
        My.Settings.p12_width = SplitContainer1.Panel2.Width
        My.Settings.p21_height = SplitContainer2.Panel1.Height
        My.Settings.p21_width = SplitContainer2.Panel1.Width
        My.Settings.p22_height = SplitContainer2.Panel2.Height
        My.Settings.p22_width = SplitContainer2.Panel2.Width
        My.Settings.Save()

    End Sub


    Private Sub SplitContainer2_Resize(sender As Object, e As EventArgs) Handles SplitContainer2.Resize
        My.Settings.p11_height = SplitContainer1.Panel1.Height
        My.Settings.p11_width = SplitContainer1.Panel2.Width
        My.Settings.p12_height = SplitContainer1.Panel2.Height
        My.Settings.p12_width = SplitContainer1.Panel2.Width
        My.Settings.p21_height = SplitContainer2.Panel1.Height
        My.Settings.p21_width = SplitContainer2.Panel1.Width
        My.Settings.p22_height = SplitContainer2.Panel2.Height
        My.Settings.p22_width = SplitContainer2.Panel2.Width
        My.Settings.Save()
    End Sub
    'ホットキーが押された時の処理
    Protected Overrides Sub WndProc(ByRef m As Message)

        Dim timeCode As String

        On Error Resume Next

        MyBase.WndProc(m)

        If m.Msg = WM_HOTKEY Then
            Select Case m.WParam

                'Play / Pause(0)
                Case hotkeyID_A
                    'axWindowsMediaPlayer1.Ctlcontrols.stop()
                    Select Case (AxWindowsMediaPlayer1.playState) 'Stop:1 Pause:2 Play:3
                        Case 1
                            AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = AxWindowsMediaPlayer1.Ctlcontrols.currentPosition - (AutoB / 10)
                            AxWindowsMediaPlayer1.Ctlcontrols.play()
                            'Button200.Text = "一時停止"
                            Button200.Image = My.Resources.Pause_16x
                        Case 2
                            AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = AxWindowsMediaPlayer1.Ctlcontrols.currentPosition - (AutoB / 10)
                            AxWindowsMediaPlayer1.Ctlcontrols.play()
                            'Button200.Text = "一時停止"
                            Button200.Image = My.Resources.Pause_16x
                        Case 3
                            AxWindowsMediaPlayer1.Ctlcontrols.pause()
                            'Button200.Text = "再生"
                            Button200.Image = My.Resources.Run_16x
                            'AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = AxWindowsMediaPlayer1.Ctlcontrols.currentPosition - (AutoB / 10)
                    End Select
                'Play / Pause & Copying Counter1(1)
                Case hotkeyID_B
                    Select Case (AxWindowsMediaPlayer1.playState) 'Stop:1 Pause:2 Play:3
                        Case 1
                            AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = AxWindowsMediaPlayer1.Ctlcontrols.currentPosition - (AutoB / 10)
                            AxWindowsMediaPlayer1.Ctlcontrols.play()
                            'Button200.Text = "一時停止"
                            Button200.Image = My.Resources.Pause_16x
                        Case 2
                            AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = AxWindowsMediaPlayer1.Ctlcontrols.currentPosition - (AutoB / 10)
                            AxWindowsMediaPlayer1.Ctlcontrols.play()
                            'Button200.Text = "一時停止"
                            Button200.Image = My.Resources.Pause_16x
                        Case 3
                            AxWindowsMediaPlayer1.Ctlcontrols.pause()
                            'Button200.Text = "再生"
                            Button200.Image = My.Resources.Run_16x
                            Select Case My.Settings.TimeCode
                                Case 0
                                    Clipboard.SetText(My.Settings.Atama & Strings.Left(Label1.Text, 8) & My.Settings.Oshiri)
                                Case 1
                                    timeCode = StrConv(Strings.Left(Label1.Text, 8), VbStrConv.Wide)
                                    Clipboard.SetText(My.Settings.Atama & timeCode & My.Settings.Oshiri)
                                Case 2
                                    timeCode = Strings.Left(Strings.Left(Label1.Text, 8), 2) & StrConv(Strings.Mid(Strings.Left(Label1.Text, 8), 3, 1), VbStrConv.Wide) & Strings.Mid(Strings.Left(Label1.Text, 8), 4, 2) & StrConv(Strings.Mid(Strings.Left(Label1.Text, 8), 6, 1), VbStrConv.Wide) & Strings.Right(Strings.Left(Label1.Text, 8), 2)
                                    Clipboard.SetText(My.Settings.Atama & timeCode & My.Settings.Oshiri)
                            End Select
                    End Select

                'Play / Pause & Copying Counter2(2)
                Case hotkeyID_C
                    Select Case (AxWindowsMediaPlayer1.playState) 'Stop:1 Pause:2 Play:3
                        Case 1
                            AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = AxWindowsMediaPlayer1.Ctlcontrols.currentPosition - (AutoB / 10)
                            AxWindowsMediaPlayer1.Ctlcontrols.play()
                            'Button200.Text = "一時停止"
                            Button200.Image = My.Resources.Pause_16x
                        Case 2
                            AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = AxWindowsMediaPlayer1.Ctlcontrols.currentPosition - (AutoB / 10)
                            AxWindowsMediaPlayer1.Ctlcontrols.play()
                            'Button200.Text = "一時停止"
                            Button200.Image = My.Resources.Pause_16x
                        Case 3
                            AxWindowsMediaPlayer1.Ctlcontrols.pause()
                            'Button200.Text = "再生"
                            Button200.Image = My.Resources.Run_16x
                            Select Case My.Settings.TimeCode
                                Case 0
                                    Clipboard.SetText(My.Settings.Atama2 & Strings.Left(Label1.Text, 8) & My.Settings.Oshiri2)
                                Case 1
                                    timeCode = StrConv(Strings.Left(Label1.Text, 8), VbStrConv.Wide)
                                    Clipboard.SetText(My.Settings.Atama2 & timeCode & My.Settings.Oshiri2)
                                Case 2
                                    timeCode = Strings.Left(Strings.Left(Label1.Text, 8), 2) & StrConv(Strings.Mid(Strings.Left(Label1.Text, 8), 3, 1), VbStrConv.Wide) & Strings.Mid(Strings.Left(Label1.Text, 8), 4, 2) & StrConv(Strings.Mid(Strings.Left(Label1.Text, 8), 6, 1), VbStrConv.Wide) & Strings.Right(Strings.Left(Label1.Text, 8), 2)
                                    Clipboard.SetText(My.Settings.Atama2 & timeCode & My.Settings.Oshiri2)
                            End Select

                    End Select

                'Stop(3)
                Case hotkeyID_D
                    AxWindowsMediaPlayer1.Ctlcontrols.stop()
                    'Button200.Text = "再生"
                    Button200.Image = My.Resources.Run_16x

                'Play / Pause & Copying counter3(4)
                Case hotkeyID_E
                    Select Case (AxWindowsMediaPlayer1.playState) 'Stop:1 Pause:2 Play:3
                        Case 1
                            AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = AxWindowsMediaPlayer1.Ctlcontrols.currentPosition - (AutoB / 10)
                            AxWindowsMediaPlayer1.Ctlcontrols.play()
                            'Button200.Text = "一時停止"
                            Button200.Image = My.Resources.Pause_16x
                        Case 2
                            AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = AxWindowsMediaPlayer1.Ctlcontrols.currentPosition - (AutoB / 10)
                            AxWindowsMediaPlayer1.Ctlcontrols.play()
                            'Button200.Text = "一時停止"
                            Button200.Image = My.Resources.Pause_16x
                        Case 3
                            AxWindowsMediaPlayer1.Ctlcontrols.pause()
                            'Button200.Text = "再生"
                            Button200.Image = My.Resources.Run_16x
                            Select Case My.Settings.TimeCode
                                Case 0
                                    Clipboard.SetText(My.Settings.Atama3 & Strings.Left(Label1.Text, 8) & My.Settings.Oshiri3)
                                Case 1
                                    timeCode = StrConv(Strings.Left(Label1.Text, 8), VbStrConv.Wide)
                                    Clipboard.SetText(My.Settings.Atama3 & timeCode & My.Settings.Oshiri3)
                                Case 2
                                    timeCode = Strings.Left(Strings.Left(Label1.Text, 8), 2) & StrConv(Strings.Mid(Strings.Left(Label1.Text, 8), 3, 1), VbStrConv.Wide) & Strings.Mid(Strings.Left(Label1.Text, 8), 4, 2) & StrConv(Strings.Mid(Strings.Left(Label1.Text, 8), 6, 1), VbStrConv.Wide) & Strings.Right(Strings.Left(Label1.Text, 8), 2)
                                    Clipboard.SetText(My.Settings.Atama3 & timeCode & My.Settings.Oshiri3)
                            End Select
                    End Select

                'Copying Counter1(5)
                Case hotkeyID_T
                    Select Case My.Settings.TimeCode
                        Case 0
                            Clipboard.SetText(My.Settings.Atama & Strings.Left(Label1.Text, 8) & My.Settings.Oshiri)
                        Case 1
                            timeCode = StrConv(Strings.Left(Label1.Text, 8), VbStrConv.Wide)
                            Clipboard.SetText(My.Settings.Atama & timeCode & My.Settings.Oshiri)
                        Case 2
                            timeCode = Strings.Left(Strings.Left(Label1.Text, 8), 2) & StrConv(Strings.Mid(Strings.Left(Label1.Text, 8), 3, 1), VbStrConv.Wide) & Strings.Mid(Strings.Left(Label1.Text, 8), 4, 2) & StrConv(Strings.Mid(Strings.Left(Label1.Text, 8), 6, 1), VbStrConv.Wide) & Strings.Right(Strings.Left(Label1.Text, 8), 2)
                            Clipboard.SetText(My.Settings.Atama & timeCode & My.Settings.Oshiri)
                    End Select

                'Copying Counter2(6)
                Case hotkeyID_F
                    Select Case My.Settings.TimeCode
                        Case 0
                            Clipboard.SetText(My.Settings.Atama2 & Strings.Left(Label1.Text, 8) & My.Settings.Oshiri2)
                        Case 1
                            timeCode = StrConv(Strings.Left(Label1.Text, 8), VbStrConv.Wide)
                            Clipboard.SetText(My.Settings.Atama2 & timeCode & My.Settings.Oshiri2)
                        Case 2
                            timeCode = Strings.Left(Strings.Left(Label1.Text, 8), 2) & StrConv(Strings.Mid(Strings.Left(Label1.Text, 8), 3, 1), VbStrConv.Wide) & Strings.Mid(Strings.Left(Label1.Text, 8), 4, 2) & StrConv(Strings.Mid(Strings.Left(Label1.Text, 8), 6, 1), VbStrConv.Wide) & Strings.Right(Strings.Left(Label1.Text, 8), 2)
                            Clipboard.SetText(My.Settings.Atama2 & timeCode & My.Settings.Oshiri2)
                    End Select

                'Copying Counter3(7)
                Case hotkeyID_G
                    Select Case My.Settings.TimeCode
                        Case 0
                            Clipboard.SetText(My.Settings.Atama3 & Strings.Left(Label1.Text, 8) & My.Settings.Oshiri3)
                        Case 1
                            timeCode = StrConv(Strings.Left(Label1.Text, 8), VbStrConv.Wide)
                            Clipboard.SetText(My.Settings.Atama3 & timeCode & My.Settings.Oshiri3)
                        Case 2
                            timeCode = Strings.Left(Strings.Left(Label1.Text, 8), 2) & StrConv(Strings.Mid(Strings.Left(Label1.Text, 8), 3, 1), VbStrConv.Wide) & Strings.Mid(Strings.Left(Label1.Text, 8), 4, 2) & StrConv(Strings.Mid(Strings.Left(Label1.Text, 8), 6, 1), VbStrConv.Wide) & Strings.Right(Strings.Left(Label1.Text, 8), 2)
                            Clipboard.SetText(My.Settings.Atama3 & timeCode & My.Settings.Oshiri3)
                    End Select

                'Add Bookmark(8)
                Case hotkeyID_H
                    DataGridView1.Rows.Add()
                    Dim i As Integer
                    i = DataGridView1.Rows.Count - 1

                    DataGridView1.Rows(i).Cells(0).Value = Strings.Left(Label1.Text, 8)
                    DataGridView1.Rows(i).Cells(2).Value = TrackBar1.Value

                    DataGridView1.CurrentCell = DataGridView1(0, i)



                'Play / Pause & Add Bookmark(9)
                Case hotkeyID_I
                    Select Case (AxWindowsMediaPlayer1.playState) 'Stop:1 Pause:2 Play:3
                        Case 1
                            AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = AxWindowsMediaPlayer1.Ctlcontrols.currentPosition - (AutoB / 10)
                            AxWindowsMediaPlayer1.Ctlcontrols.play()
                            'Button200.Text = "一時停止"
                            Button200.Image = My.Resources.Pause_16x
                        Case 2
                            AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = AxWindowsMediaPlayer1.Ctlcontrols.currentPosition - (AutoB / 10)
                            AxWindowsMediaPlayer1.Ctlcontrols.play()
                            'Button200.Text = "一時停止"
                            Button200.Image = My.Resources.Pause_16x
                        Case 3
                            AxWindowsMediaPlayer1.Ctlcontrols.pause()
                            'Button200.Text = "再生"
                            Button200.Image = My.Resources.Run_16x
                            DataGridView1.Rows.Add()
                            Dim i As Integer
                            i = DataGridView1.Rows.Count - 1

                            DataGridView1.Rows(i).Cells(0).Value = Strings.Left(Label1.Text, 8)
                            DataGridView1.Rows(i).Cells(2).Value = TrackBar1.Value

                            DataGridView1.CurrentCell = DataGridView1(0, i)
                    End Select



                'Speed Control(Up(+0.1))(16)
                Case hotkeyID_J
                    TrackBar2.Value += 1
                    Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
                    AxWindowsMediaPlayer1.Ctlcontrols.pause()
                    AxWindowsMediaPlayer1.settings.rate = TrackBar2.Value * 0.1
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    'Button200.Text = "一時停止"
                    Button200.Image = My.Resources.Pause_16x


                'Speed Control(Down(-0.1))(17)
                Case hotkeyID_K
                    TrackBar2.Value -= 1
                    Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
                    AxWindowsMediaPlayer1.Ctlcontrols.pause()
                    AxWindowsMediaPlayer1.settings.rate = TrackBar2.Value * 0.1
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    'Button200.Text = "一時停止"
                    Button200.Image = My.Resources.Pause_16x

                'Speed Control(x1.0)(18)
                Case hotkeyID_L
                    TrackBar2.Value = 10
                    Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
                    AxWindowsMediaPlayer1.Ctlcontrols.pause()
                    AxWindowsMediaPlayer1.settings.rate = TrackBar2.Value * 0.1
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    'Button200.Text = "一時停止"
                    Button200.Image = My.Resources.Pause_16x

                'Speed Control(x.0.5)(19)
                Case hotkeyID_M
                    TrackBar2.Value = 5
                    Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
                    AxWindowsMediaPlayer1.Ctlcontrols.pause()
                    AxWindowsMediaPlayer1.settings.rate = TrackBar2.Value * 0.1
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    'Button200.Text = "一時停止"
                    Button200.Image = My.Resources.Pause_16x

                'Speed Control(x2.0)(20)
                Case hotkeyID_N
                    TrackBar2.Value = 20
                    Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
                    AxWindowsMediaPlayer1.Ctlcontrols.pause()
                    AxWindowsMediaPlayer1.settings.rate = TrackBar2.Value * 0.1
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    'Button200.Text = "一時停止"
                    Button200.Image = My.Resources.Pause_16x

                'TopMost(21)
                Case hotkeyID_O
                    Me.Activate()
                    TextBox2.Focus()

                Case hotkeyID_P
                    Dim n As Integer
                    Dim a As Integer
                    Dim cbMoji As String
                    Dim cCounter As String

                    'カウンタが入力されていなければ、ボタンを押しても何も起こらない
                    cbMoji = Clipboard.GetText()
                    If cbMoji = "" Then Exit Sub

                    n = cbMoji.Length

                    '数字以外は飛ばして読み込む
                    For a = 1 To n
                        If Strings.Mid(cbMoji, a, 1) = "0" Or Strings.Mid(cbMoji, a, 1) = "1" Or Strings.Mid(cbMoji, a, 1) = "2" Or Strings.Mid(cbMoji, a, 1) = "3" Or Strings.Mid(cbMoji, a, 1) = "4" Or Strings.Mid(cbMoji, a, 1) = "5" Or Strings.Mid(cbMoji, a, 1) = "6" Or Strings.Mid(cbMoji, a, 1) = "7" Or Strings.Mid(cbMoji, a, 1) = "8" Or Strings.Mid(cbMoji, a, 1) = "9" Then
                            cCounter = cCounter & Strings.Mid(cbMoji, a, 1)
                        End If
                    Next a

                    '7桁以上の数字となった場合、エラーメッセージを出す
                    If cCounter.Length > 6 Or cCounter < 0 Then
                        MsgBox("数字が含まれていないか、7桁以上の数字が入力されています", vbOKOnly)
                        TextBox2.Clear()
                        Exit Sub
                    End If

                    '5桁以下の場合、6桁へ強制的に変更
                    Select Case cCounter.Length
                        Case 1
                            cCounter = "00000" & cCounter
                        Case 2
                            cCounter = "0000" & cCounter
                        Case 3
                            cCounter = "000" & cCounter
                        Case 4
                            cCounter = "00" & cCounter
                        Case 5
                            cCounter = "0" & cCounter
                    End Select

                    If (Integer.Parse(Strings.Mid(cCounter, 1, 2)) * 3600) + (Integer.Parse(Strings.Mid(cCounter, 3, 2)) * 60) + (Integer.Parse(Strings.Mid(cCounter, 5, 2))) > AxWindowsMediaPlayer1.currentMedia.duration Then
                        MsgBox("入力されたカウンタがファイルの長さを超えています")
                        TextBox2.Clear()
                        Exit Sub
                    End If

                    On Error Resume Next

                    TrackBar1.Value = (Integer.Parse(Strings.Mid(cCounter, 1, 2)) * 3600) + (Integer.Parse(Strings.Mid(cCounter, 3, 2)) * 60) + (Integer.Parse(Strings.Mid(cCounter, 5, 2)))
                    AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    'Button200.Text = "一時停止"
                    Button200.Image = My.Resources.Pause_16x
                    Timer1.Enabled = True
                    TextBox2.Clear()

                Case hotkeyID_Q
                Case hotkeyID_R
                Case hotkeyID_S
                Case hotkeyID_T
                Case hotkeyID_U
                Case hotkeyID_V
                Case hotkeyID_W
                Case hotkeyID_X
                Case hotkeyID_Y
                Case hotkeyID_Z

                'Jump1
                Case hotkeyID_MM1
                    AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.MM1
                'Jump2
                Case hotkeyID_MM2
                    AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.MM2
                'Jump3
                Case hotkeyID_MM3
                    AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.MM3
                'Jump4
                Case hotkeyID_HO1
                    AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.HO1
                'Jump5
                Case hotkeyID_HO2
                    AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.HO2
                'Jump6
                Case hotkeyID_HO3
                    AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.HO3


                Case hotkeyID_SC1
                    TrackBar2.Value = My.Settings.SC1 / 10
                    Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
                    AxWindowsMediaPlayer1.Ctlcontrols.pause()
                    AxWindowsMediaPlayer1.settings.rate = TrackBar2.Value * 0.1
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    'Button200.Text = "一時停止"
                    Button200.Image = My.Resources.Pause_16x
                Case hotkeyID_SC2
                    TrackBar2.Value = My.Settings.SC2 / 10
                    Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
                    AxWindowsMediaPlayer1.Ctlcontrols.pause()
                    AxWindowsMediaPlayer1.settings.rate = TrackBar2.Value * 0.1
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    'Button200.Text = "一時停止"
                    Button200.Image = My.Resources.Pause_16x
                Case hotkeyID_SC3
                    TrackBar2.Value = My.Settings.SC3 / 10
                    Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
                    AxWindowsMediaPlayer1.Ctlcontrols.pause()
                    AxWindowsMediaPlayer1.settings.rate = TrackBar2.Value * 0.1
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    'Button200.Text = "一時停止"
                    Button200.Image = My.Resources.Pause_16x
                Case hotkeyID_SC4
                    TrackBar2.Value = My.Settings.SC4 / 10
                    Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
                    AxWindowsMediaPlayer1.Ctlcontrols.pause()
                    AxWindowsMediaPlayer1.settings.rate = TrackBar2.Value * 0.1
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    'Button200.Text = "一時停止"
                    Button200.Image = My.Resources.Pause_16x
                Case hotkeyID_SC5
                    TrackBar2.Value = My.Settings.SC5 / 10
                    Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
                    AxWindowsMediaPlayer1.Ctlcontrols.pause()
                    AxWindowsMediaPlayer1.settings.rate = TrackBar2.Value * 0.1
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    'Button200.Text = "一時停止"
                    Button200.Image = My.Resources.Pause_16x
                Case hotkeyID_SC6
                    TrackBar2.Value = My.Settings.SC6 / 10
                    Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
                    AxWindowsMediaPlayer1.Ctlcontrols.pause()
                    AxWindowsMediaPlayer1.settings.rate = TrackBar2.Value * 0.1
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    'Button200.Text = "一時停止"
                    Button200.Image = My.Resources.Pause_16x
                Case hotkeyID_SC7
                    TrackBar2.Value = My.Settings.SC7 / 10
                    Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
                    AxWindowsMediaPlayer1.Ctlcontrols.pause()
                    AxWindowsMediaPlayer1.settings.rate = TrackBar2.Value * 0.1
                    AxWindowsMediaPlayer1.Ctlcontrols.play()
                    'Button200.Text = "一時停止"
                    Button200.Image = My.Resources.Pause_16x

            End Select

        End If
    End Sub
    'タイマーで再生時間の表示とシークバーの位置をコントロールする
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If Label1.Text = "00:00:00" Then
            Exit Sub
        End If
        'Kidou = False
        On Error Resume Next
        Label1.Text = TimeSpan.FromSeconds(AxWindowsMediaPlayer1.Ctlcontrols.currentPosition).ToString("hh\:mm\:ss") & " / " & TimeSpan.FromSeconds(AxWindowsMediaPlayer1.currentMedia.duration).ToString("hh\:mm\:ss")
        Me.Text = Label1.Text

        TrackBar1.Value = AxWindowsMediaPlayer1.Ctlcontrols.currentPosition

    End Sub
    '再生のシークバーを操作しているときはTimer1を止める
    Private Sub TrackBar1_MouseDown(sender As Object, e As MouseEventArgs) Handles TrackBar1.MouseDown
        Timer1.Enabled = False
    End Sub
    '再生のシークバーの操作をやめたときにTimer1を再開
    Private Sub TrackBar1_MouseUp(sender As Object, e As MouseEventArgs) Handles TrackBar1.MouseUp
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value
        Timer1.Enabled = True
    End Sub
    'Form1にメディアファイルがドラッグ＆ドロップされた場合の処理
    Private Sub Form1_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        Me.AllowDrop = True

        If (e.Data.GetDataPresent(
           DataFormats.FileDrop)) Then
            Dim files() As String
            '---get all the file names---
            files = e.Data.GetData(
               DataFormats.FileDrop)
            If files.Length > 0 Then
                '---load only the first file---
                files(0) = UCase(files(0))
                'If files(0).EndsWith(".mp4") Then
                '---get the media player to play the
                ' first file---
                AxWindowsMediaPlayer1.URL = files(0)
                My.Settings.LastOpenedFile = files(0)

                TrackBar2.Value = AxWindowsMediaPlayer1.settings.rate * 10
                Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")

                'End If
            End If
        End If
    End Sub

    Private Sub AxWindowsMediaPlayer1_MediaChange(sender As Object, e As _WMPOCXEvents_MediaChangeEvent) Handles AxWindowsMediaPlayer1.MediaChange

        If Kidou = True Then
            AxWindowsMediaPlayer1.Ctlcontrols.pause()
            'Button200.Text = "再生"
            Button200.Image = My.Resources.Run_16x
            Kidou = False
        End If

        Label1.Text = "00:00:00 / " & TimeSpan.FromSeconds(AxWindowsMediaPlayer1.currentMedia.duration).ToString("hh\:mm\:ss")
        TrackBar1.Maximum = AxWindowsMediaPlayer1.currentMedia.duration + TrackBar1.LargeChange
        TextBox1.Text = AxWindowsMediaPlayer1.currentMedia.name



    End Sub
    'Form1にメディアファイルがドラッグ＆ドロップされた場合の処理
    Private Sub Form1_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
        '---if the data to be dropped is
        ' an filedrop format---
        If (e.Data.GetDataPresent(
           DataFormats.FileDrop)) Then
            '---determine if this is a copy or move---
            If (e.KeyState And CtrlMask) = CtrlMask Then
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.Move
            End If
        End If
    End Sub

    '速度1
    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        TrackBar2.Value = My.Settings.SC1 / 10
        Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
    End Sub
    '速度2
    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        TrackBar2.Value = My.Settings.SC2 / 10
        Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
    End Sub
    '速度3
    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        TrackBar2.Value = My.Settings.SC3 / 10
        Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
    End Sub
    '速度4
    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        TrackBar2.Value = My.Settings.SC4 / 10
        Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
    End Sub
    '速度5
    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        TrackBar2.Value = My.Settings.SC5 / 10
        Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
    End Sub
    '速度6
    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        TrackBar2.Value = My.Settings.SC6 / 10
        Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
    End Sub
    '速度7
    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        TrackBar2.Value = My.Settings.SC7 / 10
        Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
    End Sub


    Private Sub TrackBar2_Scroll(sender As Object, e As EventArgs) Handles TrackBar2.Scroll
        Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
        AxWindowsMediaPlayer1.Ctlcontrols.pause()
        AxWindowsMediaPlayer1.settings.rate = TrackBar2.Value * 0.1
        AxWindowsMediaPlayer1.Ctlcontrols.play()
        'Button200.Text = "一時停止"
        Button200.Image = My.Resources.Pause_16x
    End Sub


    Private Sub Button400_Click(sender As Object, e As EventArgs) Handles Button400.Click
        AxWindowsMediaPlayer1.Ctlcontrols.stop()
    End Sub


    Private Sub Button200_Click(sender As Object, e As EventArgs) Handles Button200.Click
        Select Case (AxWindowsMediaPlayer1.playState) 'Stop:1 Pause:2 Play:3
            Case 1
                AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = AxWindowsMediaPlayer1.Ctlcontrols.currentPosition - (AutoB / 10)
                AxWindowsMediaPlayer1.Ctlcontrols.play()
                'Button200.Text = "一時停止"
                Button200.Image = My.Resources.Pause_16x
            Case 2
                AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = AxWindowsMediaPlayer1.Ctlcontrols.currentPosition - (AutoB / 10)
                AxWindowsMediaPlayer1.Ctlcontrols.play()
                'Button200.Text = "一時停止"
                Button200.Image = My.Resources.Pause_16x
            Case 3
                AxWindowsMediaPlayer1.Ctlcontrols.pause()
                'Button200.Text = "再生"
                Button200.Image = My.Resources.Run_16x
        End Select

        'axWindowsMediaPlayer1.Ctlcontrols.play()
        'AxWindowsMediaPlayer1.settings.autoStart = True
    End Sub

    Private Sub TrackBar2_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar2.ValueChanged
        On Error Resume Next
        'ファイルの長さがないということは、何もファイルを読み込んでいないと判断　※あとでまともなコードにする
        If Strings.Right(Label1.Text, 8) = "00:00:00" Then
            Exit Sub
        End If

        Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")
        AxWindowsMediaPlayer1.Ctlcontrols.pause()
        AxWindowsMediaPlayer1.settings.rate = TrackBar2.Value * 0.1
        AxWindowsMediaPlayer1.Ctlcontrols.play()
        'Button200.Text = "一時停止"
        Button200.Image = My.Resources.Pause_16x

    End Sub

    '+1Sec
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK1
    End Sub
    '-1Sec
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK11
    End Sub
    '+3Sec
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK2
    End Sub
    '-3Sec
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK12
    End Sub
    '+5Sec
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK3
    End Sub
    '-5Sec
    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK13
    End Sub
    '+10Sec
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK4
    End Sub
    '-10Sec
    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK14
    End Sub
    '+15Sec
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK5
    End Sub
    '-15Sec
    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK15
    End Sub
    '+30Sec
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK6
    End Sub
    '-30Sec
    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK16
    End Sub
    '+1Min
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK7
    End Sub
    '-1Min
    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK17
    End Sub
    '+3Min
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK8
    End Sub
    '-3Min
    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK18
    End Sub
    '+5Min
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK9
    End Sub
    '-5Min
    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK19
    End Sub
    '+10Min
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK10
    End Sub
    '-15Min
    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value + My.Settings.SK20
    End Sub

    Private Sub TrackBar3_Scroll(sender As Object, e As EventArgs) Handles TrackBar6.Scroll
        AxWindowsMediaPlayer1.settings.volume = TrackBar6.Value
        My.Settings.Onryou = TrackBar6.Value
        Label5.Text = TrackBar6.Value & "%"
    End Sub

    Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated

        Dim PuraMai As String


        Select Case My.Settings.SKAA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_A, 0, My.Settings.SKA)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_A, MOD_CONTROL, My.Settings.SKA)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_A, MOD_ALT, My.Settings.SKA)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_A, MOD_SHIFT, My.Settings.SKA)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_A, MOD_CONTROL Or MOD_ALT, My.Settings.SKA)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_A, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKA)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_A, MOD_ALT Or MOD_SHIFT, My.Settings.SKA)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_A, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKA)



        End Select

        Select Case My.Settings.SKBA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_B, 0, My.Settings.SKB)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_B, MOD_CONTROL, My.Settings.SKB)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_B, MOD_ALT, My.Settings.SKB)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_B, MOD_SHIFT, My.Settings.SKB)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_B, MOD_CONTROL Or MOD_ALT, My.Settings.SKB)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_B, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKB)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_B, MOD_ALT Or MOD_SHIFT, My.Settings.SKB)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_B, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKB)



        End Select


        Select Case My.Settings.SKCA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_C, 0, My.Settings.SKC)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_C, MOD_CONTROL, My.Settings.SKC)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_C, MOD_ALT, My.Settings.SKC)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_C, MOD_SHIFT, My.Settings.SKC)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_C, MOD_CONTROL Or MOD_ALT, My.Settings.SKC)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_C, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKC)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_C, MOD_ALT Or MOD_SHIFT, My.Settings.SKC)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_C, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKC)

        End Select

        Select Case My.Settings.SKDA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_D, 0, My.Settings.SKD)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_D, MOD_CONTROL, My.Settings.SKD)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_D, MOD_ALT, My.Settings.SKD)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_D, MOD_SHIFT, My.Settings.SKD)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_D, MOD_CONTROL Or MOD_ALT, My.Settings.SKD)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_D, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKD)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_D, MOD_ALT Or MOD_SHIFT, My.Settings.SKD)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_D, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKD)

        End Select

        Select Case My.Settings.SKEA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_E, 0, My.Settings.SKE)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_E, MOD_CONTROL, My.Settings.SKE)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_E, MOD_ALT, My.Settings.SKE)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_E, MOD_SHIFT, My.Settings.SKE)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_E, MOD_CONTROL Or MOD_ALT, My.Settings.SKE)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_E, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKE)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_E, MOD_ALT Or MOD_SHIFT, My.Settings.SKE)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_E, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKE)

        End Select

        Select Case My.Settings.SKFA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_F, 0, My.Settings.SKF)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_F, MOD_CONTROL, My.Settings.SKF)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_F, MOD_ALT, My.Settings.SKF)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_F, MOD_SHIFT, My.Settings.SKF)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_F, MOD_CONTROL Or MOD_ALT, My.Settings.SKF)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_F, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKF)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_F, MOD_ALT Or MOD_SHIFT, My.Settings.SKF)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_F, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKF)

        End Select

        Select Case My.Settings.SKGA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_G, 0, My.Settings.SKG)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_G, MOD_CONTROL, My.Settings.SKG)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_G, MOD_ALT, My.Settings.SKG)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_G, MOD_SHIFT, My.Settings.SKG)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_G, MOD_CONTROL Or MOD_ALT, My.Settings.SKG)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_G, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKG)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_G, MOD_ALT Or MOD_SHIFT, My.Settings.SKG)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_G, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKG)

        End Select

        Select Case My.Settings.SKHA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_H, 0, My.Settings.SKH)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_H, MOD_CONTROL, My.Settings.SKH)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_H, MOD_ALT, My.Settings.SKH)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_H, MOD_SHIFT, My.Settings.SKH)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_H, MOD_CONTROL Or MOD_ALT, My.Settings.SKH)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_H, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKH)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_H, MOD_ALT Or MOD_SHIFT, My.Settings.SKH)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_H, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKH)

        End Select

        Select Case My.Settings.SKIA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_I, 0, My.Settings.SKI)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_I, MOD_CONTROL, My.Settings.SKI)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_I, MOD_ALT, My.Settings.SKI)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_I, MOD_SHIFT, My.Settings.SKI)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_I, MOD_CONTROL Or MOD_ALT, My.Settings.SKI)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_I, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKI)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_I, MOD_ALT Or MOD_SHIFT, My.Settings.SKI)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_I, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKI)

        End Select

        Select Case My.Settings.SKJA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_J, 0, My.Settings.SKJ)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_J, MOD_CONTROL, My.Settings.SKJ)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_J, MOD_ALT, My.Settings.SKJ)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_J, MOD_SHIFT, My.Settings.SKJ)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_J, MOD_CONTROL Or MOD_ALT, My.Settings.SKJ)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_J, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKJ)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_J, MOD_ALT Or MOD_SHIFT, My.Settings.SKJ)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_J, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKJ)

        End Select

        Select Case My.Settings.SKKA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_K, 0, My.Settings.SKK)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_K, MOD_CONTROL, My.Settings.SKK)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_K, MOD_ALT, My.Settings.SKK)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_K, MOD_SHIFT, My.Settings.SKK)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_K, MOD_CONTROL Or MOD_ALT, My.Settings.SKK)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_K, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKK)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_K, MOD_ALT Or MOD_SHIFT, My.Settings.SKK)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_K, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKK)

        End Select

        Select Case My.Settings.SKLA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_L, 0, My.Settings.SKL)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_L, MOD_CONTROL, My.Settings.SKL)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_L, MOD_ALT, My.Settings.SKL)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_L, MOD_SHIFT, My.Settings.SKL)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_L, MOD_CONTROL Or MOD_ALT, My.Settings.SKL)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_L, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKL)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_L, MOD_ALT Or MOD_SHIFT, My.Settings.SKL)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_L, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKL)

        End Select

        Select Case My.Settings.SKMA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_M, 0, My.Settings.SKM)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_M, MOD_CONTROL, My.Settings.SKM)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_M, MOD_ALT, My.Settings.SKM)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_M, MOD_SHIFT, My.Settings.SKM)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_M, MOD_CONTROL Or MOD_ALT, My.Settings.SKM)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_M, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKM)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_M, MOD_ALT Or MOD_SHIFT, My.Settings.SKM)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_M, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKM)

        End Select

        Select Case My.Settings.SKNA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_N, 0, My.Settings.SKN)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_N, MOD_CONTROL, My.Settings.SKN)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_N, MOD_ALT, My.Settings.SKN)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_N, MOD_SHIFT, My.Settings.SKN)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_N, MOD_CONTROL Or MOD_ALT, My.Settings.SKN)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_N, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKN)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_N, MOD_ALT Or MOD_SHIFT, My.Settings.SKN)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_N, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKN)

        End Select

        Select Case My.Settings.SKOA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_O, 0, My.Settings.SKO)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_O, MOD_CONTROL, My.Settings.SKO)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_O, MOD_ALT, My.Settings.SKO)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_O, MOD_SHIFT, My.Settings.SKO)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_O, MOD_CONTROL Or MOD_ALT, My.Settings.SKO)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_O, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKO)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_O, MOD_ALT Or MOD_SHIFT, My.Settings.SKO)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_O, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKO)

        End Select

        Select Case My.Settings.SKPA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_P, 0, My.Settings.SKP)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_P, MOD_CONTROL, My.Settings.SKP)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_P, MOD_ALT, My.Settings.SKP)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_P, MOD_SHIFT, My.Settings.SKP)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_P, MOD_CONTROL Or MOD_ALT, My.Settings.SKP)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_P, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKP)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_P, MOD_ALT Or MOD_SHIFT, My.Settings.SKP)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_P, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKP)

        End Select

        Select Case My.Settings.SKQA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_Q, 0, My.Settings.SKQ)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_Q, MOD_CONTROL, My.Settings.SKQ)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_Q, MOD_ALT, My.Settings.SKQ)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_Q, MOD_SHIFT, My.Settings.SKQ)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_Q, MOD_CONTROL Or MOD_ALT, My.Settings.SKQ)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_Q, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKQ)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_Q, MOD_ALT Or MOD_SHIFT, My.Settings.SKQ)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_Q, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKQ)

        End Select

        Select Case My.Settings.SKRA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_R, 0, My.Settings.SKR)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_R, MOD_CONTROL, My.Settings.SKR)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_R, MOD_ALT, My.Settings.SKR)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_R, MOD_SHIFT, My.Settings.SKR)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_R, MOD_CONTROL Or MOD_ALT, My.Settings.SKR)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_R, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKR)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_R, MOD_ALT Or MOD_SHIFT, My.Settings.SKR)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_R, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKR)

        End Select

        Select Case My.Settings.SKSA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_S, 0, My.Settings.SKS)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_S, MOD_CONTROL, My.Settings.SKS)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_S, MOD_ALT, My.Settings.SKS)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_S, MOD_SHIFT, My.Settings.SKS)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_S, MOD_CONTROL Or MOD_ALT, My.Settings.SKS)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_S, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKS)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_S, MOD_ALT Or MOD_SHIFT, My.Settings.SKS)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_S, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKS)

        End Select

        Select Case My.Settings.SKTA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_T, 0, My.Settings.SKT)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_T, MOD_CONTROL, My.Settings.SKT)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_T, MOD_ALT, My.Settings.SKT)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_T, MOD_SHIFT, My.Settings.SKT)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_T, MOD_CONTROL Or MOD_ALT, My.Settings.SKT)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_T, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKT)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_T, MOD_ALT Or MOD_SHIFT, My.Settings.SKT)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_T, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKT)

        End Select

        Select Case My.Settings.SKUA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_U, 0, My.Settings.SKU)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_U, MOD_CONTROL, My.Settings.SKU)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_U, MOD_ALT, My.Settings.SKU)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_U, MOD_SHIFT, My.Settings.SKU)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_U, MOD_CONTROL Or MOD_ALT, My.Settings.SKU)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_U, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKU)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_U, MOD_ALT Or MOD_SHIFT, My.Settings.SKU)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_U, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKU)

        End Select

        '未使用
        Select Case My.Settings.SKVA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_V, 0, My.Settings.SKV)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_V, MOD_CONTROL, My.Settings.SKV)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_V, MOD_ALT, My.Settings.SKV)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_V, MOD_SHIFT, My.Settings.SKV)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_V, MOD_CONTROL Or MOD_ALT, My.Settings.SKV)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_V, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKV)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_V, MOD_ALT Or MOD_SHIFT, My.Settings.SKV)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_V, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKV)

        End Select

        '未使用
        Select Case My.Settings.SKWA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_W, 0, My.Settings.SKW)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_W, MOD_CONTROL, My.Settings.SKW)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_W, MOD_ALT, My.Settings.SKW)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_W, MOD_SHIFT, My.Settings.SKW)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_W, MOD_CONTROL Or MOD_ALT, My.Settings.SKW)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_W, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKW)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_W, MOD_ALT Or MOD_SHIFT, My.Settings.SKW)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_W, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKW)

        End Select

        '未使用
        Select Case My.Settings.SKXA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_X, 0, My.Settings.SKX)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_X, MOD_CONTROL, My.Settings.SKX)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_X, MOD_ALT, My.Settings.SKX)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_X, MOD_SHIFT, My.Settings.SKX)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_X, MOD_CONTROL Or MOD_ALT, My.Settings.SKX)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_X, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKX)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_X, MOD_ALT Or MOD_SHIFT, My.Settings.SKX)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_X, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKX)

        End Select

        '未使用
        Select Case My.Settings.SKYA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_Y, 0, My.Settings.SKY)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_Y, MOD_CONTROL, My.Settings.SKY)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_Y, MOD_ALT, My.Settings.SKY)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_Y, MOD_SHIFT, My.Settings.SKY)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_Y, MOD_CONTROL Or MOD_ALT, My.Settings.SKY)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_Y, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKY)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_Y, MOD_ALT Or MOD_SHIFT, My.Settings.SKY)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_Y, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKY)

        End Select

        '未使用
        Select Case My.Settings.SKZA

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_Z, 0, My.Settings.SKZ)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_Z, MOD_CONTROL, My.Settings.SKZ)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_Z, MOD_ALT, My.Settings.SKZ)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_Z, MOD_SHIFT, My.Settings.SKZ)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_Z, MOD_CONTROL Or MOD_ALT, My.Settings.SKZ)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_Z, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKZ)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_Z, MOD_ALT Or MOD_SHIFT, My.Settings.SKZ)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_Z, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKZ)

        End Select

        'ジャンプボタン1の修飾キーの判断＆設定ファイルに保存
        Select Case My.Settings.SKM1A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_MM1, 0, My.Settings.SKM1)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_MM1, MOD_CONTROL, My.Settings.SKM1)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_MM1, MOD_ALT, My.Settings.SKM1)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_MM1, MOD_SHIFT, My.Settings.SKM1)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_MM1, MOD_CONTROL Or MOD_ALT, My.Settings.SKM1)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_MM1, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKM1)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_MM1, MOD_ALT Or MOD_SHIFT, My.Settings.SKM1)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_MM1, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKM1)

        End Select

        'ジャンプボタン2の修飾キーの判断＆設定ファイルに保存
        Select Case My.Settings.SKM2A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_MM2, 0, My.Settings.SKM2)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_MM2, MOD_CONTROL, My.Settings.SKM2)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_MM2, MOD_ALT, My.Settings.SKM2)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_MM2, MOD_SHIFT, My.Settings.SKM2)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_MM2, MOD_CONTROL Or MOD_ALT, My.Settings.SKM2)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_MM2, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKM2)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_MM2, MOD_ALT Or MOD_SHIFT, My.Settings.SKM2)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_MM2, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKM2)

        End Select

        'ジャンプボタン3の修飾キーの判断＆設定ファイルに保存
        Select Case My.Settings.SKM3A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_MM3, 0, My.Settings.SKM3)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_MM3, MOD_CONTROL, My.Settings.SKM3)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_MM3, MOD_ALT, My.Settings.SKM3)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_MM3, MOD_SHIFT, My.Settings.SKM3)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_MM3, MOD_CONTROL Or MOD_ALT, My.Settings.SKM3)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_MM3, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKM3)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_MM3, MOD_ALT Or MOD_SHIFT, My.Settings.SKM3)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_MM3, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKM3)

        End Select

        'ジャンプボタン4の修飾キーの判断＆設定ファイルに保存
        Select Case My.Settings.SKH1A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_HO1, 0, My.Settings.SKH1)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_HO1, MOD_CONTROL, My.Settings.SKH1)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_HO1, MOD_ALT, My.Settings.SKH1)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_HO1, MOD_SHIFT, My.Settings.SKH1)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_HO1, MOD_CONTROL Or MOD_ALT, My.Settings.SKH1)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_HO1, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKH1)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_HO1, MOD_ALT Or MOD_SHIFT, My.Settings.SKH1)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_HO1, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKH1)

        End Select

        'ジャンプボタン5の修飾キーの判断＆設定ファイルに保存
        Select Case My.Settings.SKH2A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_HO2, 0, My.Settings.SKH2)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_HO2, MOD_CONTROL, My.Settings.SKH2)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_HO2, MOD_ALT, My.Settings.SKH2)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_HO2, MOD_SHIFT, My.Settings.SKH2)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_HO2, MOD_CONTROL Or MOD_ALT, My.Settings.SKH2)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_HO2, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKH2)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_HO2, MOD_ALT Or MOD_SHIFT, My.Settings.SKH2)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_HO2, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKH2)

        End Select


        'ジャンプボタン6の修飾キーの判断＆設定ファイルに保存
        Select Case My.Settings.SKH3A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_HO3, 0, My.Settings.SKH3)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_HO3, MOD_CONTROL, My.Settings.SKH3)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_HO3, MOD_ALT, My.Settings.SKH3)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_HO3, MOD_SHIFT, My.Settings.SKH3)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_HO3, MOD_CONTROL Or MOD_ALT, My.Settings.SKH3)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_HO3, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKH3)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_HO3, MOD_ALT Or MOD_SHIFT, My.Settings.SKH3)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_HO3, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKH3)

        End Select

        Select Case My.Settings.SC1A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_SC1, 0, My.Settings.SKSC1)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_SC1, MOD_CONTROL, My.Settings.SKSC1)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_SC1, MOD_ALT, My.Settings.SKSC1)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_SC1, MOD_SHIFT, My.Settings.SKSC1)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_SC1, MOD_CONTROL Or MOD_ALT, My.Settings.SKSC1)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_SC1, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKSC1)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_SC1, MOD_ALT Or MOD_SHIFT, My.Settings.SKSC1)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_SC1, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKSC1)

        End Select

        Select Case My.Settings.SC2A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_SC2, 0, My.Settings.SKSC2)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_SC2, MOD_CONTROL, My.Settings.SKSC2)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_SC2, MOD_ALT, My.Settings.SKSC2)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_SC2, MOD_SHIFT, My.Settings.SKSC2)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_SC2, MOD_CONTROL Or MOD_ALT, My.Settings.SKSC2)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_SC2, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKSC2)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_SC2, MOD_ALT Or MOD_SHIFT, My.Settings.SKSC2)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_SC2, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKSC2)

        End Select

        Select Case My.Settings.SC3A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_SC3, 0, My.Settings.SKSC3)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_SC3, MOD_CONTROL, My.Settings.SKSC3)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_SC3, MOD_ALT, My.Settings.SKSC3)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_SC3, MOD_SHIFT, My.Settings.SKSC3)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_SC3, MOD_CONTROL Or MOD_ALT, My.Settings.SKSC3)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_SC3, MOD_CONTROL Or MOD_SHIFT, My.Settings.SC3)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_SC3, MOD_ALT Or MOD_SHIFT, My.Settings.SKSC3)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_SC3, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKSC3)

        End Select

        Select Case My.Settings.SC4A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_SC4, 0, My.Settings.SKSC4)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_SC4, MOD_CONTROL, My.Settings.SKSC4)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_SC4, MOD_ALT, My.Settings.SKSC4)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_SC4, MOD_SHIFT, My.Settings.SKSC4)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_SC4, MOD_CONTROL Or MOD_ALT, My.Settings.SKSC4)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_SC4, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKSC4)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_SC4, MOD_ALT Or MOD_SHIFT, My.Settings.SKSC4)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_SC4, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKSC4)

        End Select

        Select Case My.Settings.SC5A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_SC5, 0, My.Settings.SKSC5)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_SC5, MOD_CONTROL, My.Settings.SKSC5)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_SC5, MOD_ALT, My.Settings.SKSC5)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_SC5, MOD_SHIFT, My.Settings.SKSC5)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_SC5, MOD_CONTROL Or MOD_ALT, My.Settings.SKSC5)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_SC5, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKSC5)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_SC5, MOD_ALT Or MOD_SHIFT, My.Settings.SKSC5)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_SC5, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKSC5)

        End Select

        Select Case My.Settings.SC6A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_SC6, 0, My.Settings.SKSC6)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_SC6, MOD_CONTROL, My.Settings.SKSC6)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_SC6, MOD_ALT, My.Settings.SKSC6)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_SC6, MOD_SHIFT, My.Settings.SKSC6)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_SC6, MOD_CONTROL Or MOD_ALT, My.Settings.SKSC6)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_SC6, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKSC6)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_SC6, MOD_ALT Or MOD_SHIFT, My.Settings.SKSC6)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_SC6, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKSC6)

        End Select

        Select Case My.Settings.SC7A

            'none
            Case 0
                RegisterHotKey(Me.Handle, hotkeyID_SC7, 0, My.Settings.SKSC7)
            'Ctrl
            Case 3
                RegisterHotKey(Me.Handle, hotkeyID_SC7, MOD_CONTROL, My.Settings.SKSC7)
            'Alt
            Case 4
                RegisterHotKey(Me.Handle, hotkeyID_SC7, MOD_ALT, My.Settings.SKSC7)
            'Shift
            Case 5
                RegisterHotKey(Me.Handle, hotkeyID_SC7, MOD_SHIFT, My.Settings.SKSC7)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Me.Handle, hotkeyID_SC7, MOD_CONTROL Or MOD_ALT, My.Settings.SKSC7)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Me.Handle, hotkeyID_SC7, MOD_CONTROL Or MOD_SHIFT, My.Settings.SKSC7)
            'Alt + Shift
            Case 9
                RegisterHotKey(Me.Handle, hotkeyID_SC7, MOD_ALT Or MOD_SHIFT, My.Settings.SKSC7)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Me.Handle, hotkeyID_SC7, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, My.Settings.SKSC7)

        End Select


        'Player本体のコントロールをすべて無効
        AxWindowsMediaPlayer1.uiMode = "none"

        'ジャンプボタン表示用
        PuraMai = ""
        If System.Math.Abs(My.Settings.SK1) >= 60 Then
            If My.Settings.SK1 > 0 Then
                PuraMai = "+"
            End If
            Button1.Text = PuraMai & My.Settings.SK1 / 60 & "m"
        Else
            If My.Settings.SK1 > 0 Then
                PuraMai = "+"
            End If
            Button1.Text = PuraMai & My.Settings.SK1 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK11) >= 60 Then

            If My.Settings.SK11 > 0 Then
                PuraMai = "+"
            End If
            Button11.Text = PuraMai & My.Settings.SK11 / 60 & "m"
        Else
            If My.Settings.SK11 > 0 Then
                PuraMai = "+"
            End If
            Button11.Text = PuraMai & My.Settings.SK11 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK2) >= 60 Then
            If My.Settings.SK2 > 0 Then
                PuraMai = "+"
            End If
            Button2.Text = PuraMai & My.Settings.SK2 / 60 & "m"
        Else
            If My.Settings.SK2 > 0 Then
                PuraMai = "+"
            End If
            Button2.Text = PuraMai & My.Settings.SK2 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK12) >= 60 Then
            If My.Settings.SK12 > 0 Then
                PuraMai = "+"
            End If
            Button12.Text = PuraMai & My.Settings.SK12 / 60 & "m"
        Else
            If My.Settings.SK12 > 0 Then
                PuraMai = "+"
            End If
            Button12.Text = PuraMai & My.Settings.SK12 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK3) >= 60 Then
            If My.Settings.SK3 > 0 Then
                PuraMai = "+"
            End If
            Button3.Text = PuraMai & My.Settings.SK3 / 60 & "m"
        Else
            If My.Settings.SK3 > 0 Then
                PuraMai = "+"
            End If
            Button3.Text = PuraMai & My.Settings.SK3 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK13) >= 60 Then
            If My.Settings.SK13 > 0 Then
                PuraMai = "+"
            End If
            Button13.Text = PuraMai & My.Settings.SK13 / 60 & "m"
        Else
            If My.Settings.SK13 > 0 Then
                PuraMai = "+"
            End If
            Button13.Text = PuraMai & My.Settings.SK13 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK4) >= 60 Then
            If My.Settings.SK4 > 0 Then
                PuraMai = "+"
            End If
            Button4.Text = PuraMai & My.Settings.SK4 / 60 & "m"
        Else
            If My.Settings.SK4 > 0 Then
                PuraMai = "+"
            End If
            Button4.Text = PuraMai & My.Settings.SK4 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK14) >= 60 Then
            If My.Settings.SK14 > 0 Then
                PuraMai = "+"
            End If
            Button14.Text = PuraMai & My.Settings.SK14 / 60 & "m"
        Else
            If My.Settings.SK14 > 0 Then
                PuraMai = "+"
            End If
            Button14.Text = PuraMai & My.Settings.SK14 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK5) >= 60 Then
            If My.Settings.SK5 > 0 Then
                PuraMai = "+"
            End If
            Button5.Text = PuraMai & My.Settings.SK5 / 60 & "m"
        Else
            If My.Settings.SK5 > 0 Then
                PuraMai = "+"
            End If
            Button5.Text = PuraMai & My.Settings.SK5 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK15) >= 60 Then
            If My.Settings.SK15 > 0 Then
                PuraMai = "+"
            End If
            Button15.Text = PuraMai & My.Settings.SK15 / 60 & "m"
        Else
            If My.Settings.SK15 > 0 Then
                PuraMai = "+"
            End If
            Button15.Text = PuraMai & My.Settings.SK15 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK6) >= 60 Then
            If My.Settings.SK6 > 0 Then
                PuraMai = "+"
            End If
            Button6.Text = PuraMai & My.Settings.SK6 / 60 & "m"
        Else
            If My.Settings.SK6 > 0 Then
                PuraMai = "+"
            End If
            Button6.Text = PuraMai & My.Settings.SK6 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK16) >= 60 Then
            If My.Settings.SK16 > 0 Then
                PuraMai = "+"
            End If
            Button16.Text = PuraMai & My.Settings.SK16 / 60 & "m"
        Else
            If My.Settings.SK16 > 0 Then
                PuraMai = "+"
            End If
            Button16.Text = PuraMai & My.Settings.SK16 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK7) >= 60 Then
            If My.Settings.SK7 > 0 Then
                PuraMai = "+"
            End If
            Button7.Text = PuraMai & My.Settings.SK7 / 60 & "m"
        Else
            If My.Settings.SK7 > 0 Then
                PuraMai = "+"
            End If
            Button7.Text = PuraMai & My.Settings.SK7 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK17) >= 60 Then
            If My.Settings.SK17 > 0 Then
                PuraMai = "+"
            End If
            Button17.Text = PuraMai & My.Settings.SK17 / 60 & "m"
        Else
            If My.Settings.SK17 > 0 Then
                PuraMai = "+"
            End If
            Button17.Text = PuraMai & My.Settings.SK17 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK8) >= 60 Then
            If My.Settings.SK8 > 0 Then
                PuraMai = "+"
            End If
            Button8.Text = PuraMai & My.Settings.SK8 / 60 & "m"
        Else
            If My.Settings.SK8 > 0 Then
                PuraMai = "+"
            End If
            Button8.Text = PuraMai & My.Settings.SK8 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK18) >= 60 Then
            If My.Settings.SK18 > 0 Then
                PuraMai = "+"
            End If
            Button18.Text = PuraMai & My.Settings.SK18 / 60 & "m"
        Else
            If My.Settings.SK18 > 0 Then
                PuraMai = "+"
            End If
            Button18.Text = PuraMai & My.Settings.SK18 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK9) >= 60 Then
            If My.Settings.SK9 > 0 Then
                PuraMai = "+"
            End If
            Button9.Text = PuraMai & My.Settings.SK9 / 60 & "m"
        Else
            If My.Settings.SK9 > 0 Then
                PuraMai = "+"
            End If
            Button9.Text = PuraMai & My.Settings.SK9 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK19) >= 60 Then
            If My.Settings.SK19 > 0 Then
                PuraMai = "+"
            End If
            Button19.Text = PuraMai & My.Settings.SK19 / 60 & "m"
        Else
            If My.Settings.SK19 > 0 Then
                PuraMai = "+"
            End If
            Button19.Text = PuraMai & My.Settings.SK19 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK10) >= 60 Then
            If My.Settings.SK10 > 0 Then
                PuraMai = "+"
            End If
            Button10.Text = PuraMai & My.Settings.SK10 / 60 & "m"
        Else
            If My.Settings.SK10 > 0 Then
                PuraMai = "+"
            End If
            Button10.Text = PuraMai & My.Settings.SK10 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK20) >= 60 Then
            If My.Settings.SK20 > 0 Then
                PuraMai = "+"
            End If
            Button20.Text = PuraMai & My.Settings.SK20 / 60 & "m"
        Else
            If My.Settings.SK20 > 0 Then
                PuraMai = "+"
            End If
            Button20.Text = PuraMai & My.Settings.SK20 & "s"
        End If

        Button21.Text = (My.Settings.SC1 / 100).ToString("0.0")
        Button22.Text = (My.Settings.SC2 / 100).ToString("0.0")
        Button23.Text = (My.Settings.SC3 / 100).ToString("0.0")
        Button24.Text = (My.Settings.SC4 / 100).ToString("0.0")
        Button25.Text = (My.Settings.SC5 / 100).ToString("0.0")
        Button26.Text = (My.Settings.SC6 / 100).ToString("0.0")
        Button27.Text = (My.Settings.SC7 / 100).ToString("0.0")

        Label5.Text = TrackBar6.Value & "%"


    End Sub

    '要修正
    Private Sub SplitContainer1_Resize(sender As Object, e As EventArgs) Handles SplitContainer1.Resize
        My.Settings.p11_height = SplitContainer1.Panel1.Height
        My.Settings.p11_width = SplitContainer1.Panel2.Width
        My.Settings.p12_height = SplitContainer1.Panel2.Height
        My.Settings.p12_width = SplitContainer1.Panel2.Width
        My.Settings.p21_height = SplitContainer2.Panel1.Height
        My.Settings.p21_width = SplitContainer2.Panel1.Width
        My.Settings.p22_height = SplitContainer2.Panel2.Height
        My.Settings.p22_width = SplitContainer2.Panel2.Width
        My.Settings.Save()

    End Sub

    Private Sub SplitContainer2_Panel2_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer2.Panel2.Paint

    End Sub


    '要修正
    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        Dim f_b_width As Integer = Me.Width

        If SplitContainer1.Panel2Collapsed = True Then

            Me.Width += My.Settings.p22_width
            SplitContainer1.Panel2Collapsed = False
            SplitContainer1.SplitterDistance = f_b_width


            'Button30.Text = "＜＜しおり"

            My.Settings.p11_height = SplitContainer1.Panel1.Height
            My.Settings.p11_width = SplitContainer1.Panel2.Width
            My.Settings.p12_height = SplitContainer1.Panel2.Height
            My.Settings.p12_width = SplitContainer1.Panel2.Width
            My.Settings.p21_height = SplitContainer2.Panel1.Height
            My.Settings.p21_width = SplitContainer2.Panel1.Width
            My.Settings.p22_height = SplitContainer2.Panel2.Height
            My.Settings.p22_width = SplitContainer2.Panel2.Width
            My.Settings.Save()

        Else
            Me.Width -= SplitContainer2.Panel2.Width
            SplitContainer1.Panel2Collapsed = True
            'Me.Width = 520

            'Button30.Text = "しおり＞＞"

            My.Settings.p11_height = SplitContainer1.Panel1.Height
            My.Settings.p11_width = SplitContainer1.Panel2.Width
            My.Settings.p12_height = SplitContainer1.Panel2.Height
            My.Settings.p12_width = SplitContainer1.Panel2.Width
            My.Settings.p21_height = SplitContainer2.Panel1.Height
            My.Settings.p21_width = SplitContainer2.Panel1.Width
            My.Settings.p22_height = SplitContainer2.Panel2.Height
            My.Settings.p22_width = SplitContainer2.Panel2.Width
            My.Settings.Save()
        End If

    End Sub

    '要修正
    Private Sub Form1_ResizeEnd(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd


        'MsgBox("form width : " & Me.Width & vbCrLf & "form height : " & vbCrLf & Me.Height & vbCrLf & "SP1-1 width : " & SplitContainer1.Panel1.Width & vbCrLf & "SP1-2 width : " & SplitContainer1.Panel2.Width & vbCrLf & "SP2-1 height : " & SplitContainer2.Panel1.Height & vbCrLf & "SP2-2 height : " & SplitContainer1.Panel2.Height, vbOKOnly)

    End Sub


    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        DataGridView1.Rows.Add()
        Dim i As Integer
        i = DataGridView1.Rows.Count - 1

        DataGridView1.Rows(i).Cells(0).Value = Strings.Left(Label1.Text, 8)
        DataGridView1.Rows(i).Cells(2).Value = TrackBar1.Value

        DataGridView1.CurrentCell = DataGridView1(0, i)
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim colIndex As Integer = e.ColumnIndex
        Dim rowIndex As Integer = e.RowIndex
        Dim i As Integer

        'ボタン以外のセルの場合処理終了
        If colIndex <> 0 And colIndex <> 3 Then
            Exit Sub
        End If

        On Error Resume Next
        Select Case colIndex
            'ジャンプ
            Case 0
                i = DataGridView1.SelectedCells(0).RowIndex
                Timer1.Enabled = False
                TrackBar1.Value = DataGridView1.Rows(i).Cells(2).Value
                AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value
                AxWindowsMediaPlayer1.Ctlcontrols.play()
                'Button200.Text = "一時停止"
                Button200.Image = My.Resources.Pause_16x
                Timer1.Enabled = True
            '削除
            Case 3
                i = DataGridView1.SelectedCells(0).RowIndex
                DataGridView1.Rows.RemoveAt(i)
        End Select
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        Dim i As Integer
        If DataGridView1.RowCount = 0 Then Exit Sub
        i = DataGridView1.SelectedCells(0).RowIndex
        DataGridView1.Rows.RemoveAt(i)
    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        DataGridView1.Rows.Add()
        Dim i As Integer
        Dim n As Integer
        Dim a As Integer
        Dim cCounter As String

        'カウンタが入力されていなければ、ボタンを押しても何も起こらない
        If TextBox3.Text = "" Then Exit Sub

        i = DataGridView1.Rows.Count - 1
        n = TextBox3.Text.Length

        '数字以外は飛ばして読み込む
        For a = 1 To n
            If Strings.Mid(TextBox3.Text, a, 1) = "0" Or Strings.Mid(TextBox3.Text, a, 1) = "1" Or Strings.Mid(TextBox3.Text, a, 1) = "2" Or Strings.Mid(TextBox3.Text, a, 1) = "3" Or Strings.Mid(TextBox3.Text, a, 1) = "4" Or Strings.Mid(TextBox3.Text, a, 1) = "5" Or Strings.Mid(TextBox3.Text, a, 1) = "6" Or Strings.Mid(TextBox3.Text, a, 1) = "7" Or Strings.Mid(TextBox3.Text, a, 1) = "8" Or Strings.Mid(TextBox3.Text, a, 1) = "9" Then
                cCounter = cCounter & Strings.Mid(TextBox3.Text, a, 1)
            End If
        Next a

        '7桁以上の数字となった場合、エラーメッセージを出す
        If cCounter.Length > 6 Or cCounter < 0 Then
            MsgBox("数字が含まれていないか、7桁以上の数字が入力されています", vbOKOnly)
            TextBox2.Clear()
            Exit Sub
        End If

        '5桁以下の場合、6桁へ強制的に変更
        Select Case cCounter.Length
            Case 1
                cCounter = "00000" & cCounter
            Case 2
                cCounter = "0000" & cCounter
            Case 3
                cCounter = "000" & cCounter
            Case 4
                cCounter = "00" & cCounter
            Case 5
                cCounter = "0" & cCounter
        End Select

        If (Integer.Parse(Strings.Mid(cCounter, 1, 2)) * 3600) + (Integer.Parse(Strings.Mid(cCounter, 3, 2)) * 60) + (Integer.Parse(Strings.Mid(cCounter, 5, 2))) > AxWindowsMediaPlayer1.currentMedia.duration Then
            MsgBox("入力されたカウンタがファイルの長さを超えています")
            TextBox2.Clear()
            Exit Sub
        End If

        DataGridView1.Rows(i).Cells(0).Value = Strings.Mid(cCounter, 1, 2) & ":" & Strings.Mid(cCounter, 3, 2) & ":" & Strings.Mid(cCounter, 5, 2)
        DataGridView1.Rows(i).Cells(2).Value = (Integer.Parse(Strings.Mid(cCounter, 1, 2)) * 3600) + (Integer.Parse(Strings.Mid(cCounter, 3, 2)) * 60) + (Integer.Parse(Strings.Mid(cCounter, 5, 2)))

        DataGridView1.CurrentCell = DataGridView1(0, i)
    End Sub


    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click

        Dim n As Integer
        Dim a As Integer
        Dim cCounter As String

        'カウンタが入力されていなければ、ボタンを押しても何も起こらない
        If TextBox2.Text = "" Then Exit Sub

        n = TextBox2.Text.Length

        '数字以外は飛ばして読み込む
        For a = 1 To n
            If Strings.Mid(TextBox2.Text, a, 1) = "0" Or Strings.Mid(TextBox2.Text, a, 1) = "1" Or Strings.Mid(TextBox2.Text, a, 1) = "2" Or Strings.Mid(TextBox2.Text, a, 1) = "3" Or Strings.Mid(TextBox2.Text, a, 1) = "4" Or Strings.Mid(TextBox2.Text, a, 1) = "5" Or Strings.Mid(TextBox2.Text, a, 1) = "6" Or Strings.Mid(TextBox2.Text, a, 1) = "7" Or Strings.Mid(TextBox2.Text, a, 1) = "8" Or Strings.Mid(TextBox2.Text, a, 1) = "9" Then
                cCounter = cCounter & Strings.Mid(TextBox2.Text, a, 1)
            End If
        Next a

        '7桁以上の数字となった場合、エラーメッセージを出す
        If cCounter.Length > 6 Or cCounter < 0 Then
            MsgBox("数字が含まれていないか、7桁以上の数字が入力されています", vbOKOnly)
            TextBox2.Clear()
            Exit Sub
        End If

        '5桁以下の場合、6桁へ強制的に変更
        Select Case cCounter.Length
            Case 1
                cCounter = "00000" & cCounter
            Case 2
                cCounter = "0000" & cCounter
            Case 3
                cCounter = "000" & cCounter
            Case 4
                cCounter = "00" & cCounter
            Case 5
                cCounter = "0" & cCounter
        End Select

        If (Integer.Parse(Strings.Mid(cCounter, 1, 2)) * 3600) + (Integer.Parse(Strings.Mid(cCounter, 3, 2)) * 60) + (Integer.Parse(Strings.Mid(cCounter, 5, 2))) > AxWindowsMediaPlayer1.currentMedia.duration Then
            MsgBox("入力されたカウンタがファイルの長さを超えています")
            TextBox2.Clear()
            Exit Sub
        End If

        TrackBar1.Value = (Integer.Parse(Strings.Mid(cCounter, 1, 2)) * 3600) + (Integer.Parse(Strings.Mid(cCounter, 3, 2)) * 60) + (Integer.Parse(Strings.Mid(cCounter, 5, 2)))
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value
        AxWindowsMediaPlayer1.Ctlcontrols.play()
        'Button200.Text = "一時停止"
        Button200.Image = My.Resources.Pause_16x
        Timer1.Enabled = True
        TextBox2.Clear()

    End Sub



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' DataGridViewからCSVファイルへの書込処理
    ''' （WriteCsv:CSVファイルの書込処理の利用による）
    ''' </summary>
    ''' <param name="dgv">DataGridView</param>
    ''' <param name="astrFileName">ファイル名</param>
    ''' -----------------------------------------------------------------------------
    Private Function WriteCsvFromDGV(ByVal dgv As DataGridView,
                                     ByVal astrFileName As String) As Boolean
        WriteCsvFromDGV = False

        If (SaveFileDialog1.ShowDialog = DialogResult.OK) Then
            astrFileName = SaveFileDialog1.FileName & ".csv"
        Else
            Exit Function
        End If

        Try
            'DataGridView全体のデータ領域
            Dim arrData()() As String = Nothing

            '1行分を読込
            Dim arrHead As String() = Nothing
            '列ヘッダの名称取得
            For col As Integer = 0 To Me.DataGridView1.Columns.Count - 1
                '1列分の領域拡張
                ReDim Preserve arrHead(col)
                '列のヘッダデータ退避
                arrHead(col) = CStr(Me.DataGridView1.Columns(col).HeaderCell.Value)
            Next
            '1行分の領域拡張
            ReDim Preserve arrData(0)
            '列ヘッダ名退避
            arrData(0) = arrHead

            'データ行数分の処理
            For row As Integer = 0 To Me.DataGridView1.Rows.Count - 1
                '新規行は処理しない
                If Me.DataGridView1.Rows(row).IsNewRow Then
                    Continue For
                End If

                '1行分を読込
                Dim arrLine As String() = Nothing
                '列数分の処理
                For col As Integer = 0 To Me.DataGridView1.Columns.Count - 1
                    '1列分の領域拡張
                    ReDim Preserve arrLine(col)
                    '列データ退避
                    arrLine(col) = CStr(Me.DataGridView1.Rows(row).Cells(col).Value)
                Next

                '1行分の領域拡張
                ReDim Preserve arrData(row + 1)
                '1行データ退避
                arrData(row + 1) = arrLine
            Next

            '実際のCSVファイルの書込み
            Return WriteCsv(astrFileName, arrData)

        Catch ex As Exception
            'エラー
            MsgBox(ex.Message)
        End Try
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' CSVファイルの書込処理
    ''' </summary>
    ''' <param name="astrFileName">ファイル名</param>
    ''' <param name="aarrData">書込データ文字列の2次元配列</param>
    ''' <returns>True:結果OK, False:NG</returns>
    ''' <remarks>カラム名をファイルに出力したい場合は、書込データの先頭に設定すること</remarks>
    ''' -----------------------------------------------------------------------------
    Private Function WriteCsv(ByVal astrFileName As String,
                              ByVal aarrData As String()()) As Boolean
        WriteCsv = False
        'ファイルStreamWriter
        Dim sw As System.IO.StreamWriter = Nothing

        Try
            'CSVファイルに書込に使うEncoding
            Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
            '書き込むファイルを開く
            sw = New System.IO.StreamWriter(astrFileName, False, enc)

            For Each arrLine() As String In aarrData
                Dim blnFirst As Boolean = True
                Dim strLIne As String = ""
                For Each str As String In arrLine
                    If blnFirst = False Then
                        '「,」(カンマ)の書込
                        sw.Write(",")
                    End If
                    blnFirst = False
                    '1カラムデータの書込
                    str = """" & str & """"
                    sw.Write(str)
                Next
                '改行の書込
                sw.Write(vbCrLf)
            Next

            '正常終了
            Return True

        Catch ex As Exception
            'エラー
            MsgBox(ex.Message)
        Finally
            '閉じる
            If sw IsNot Nothing Then
                sw.Close()
            End If
        End Try
    End Function

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        Try
            'DataGridViewからCSVファイルへの書込処理の呼出
            If WriteCsvFromDGV(Me.DataGridView1, TextBox1.Text) = True Then
                MsgBox("書出完了")
            Else
                MsgBox("書出失敗")
            End If

        Catch ex As Exception
            'エラー
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        Dim nFile As String = Nothing
        Dim strMemo As String
        Dim n As Integer
        Dim iLine As String
        Dim cCounter As String


        If (OpenFileDialog1.ShowDialog = DialogResult.OK) Then
            nFile = OpenFileDialog1.FileName
            Select Case Path.GetExtension(nFile)
                Case ".csv"
                    CsvReader()

                Case ".doc", ".docx"
                    Dim objWord As Object
                    Dim objDoc As Object
                    Dim objNewDoc As Object
                    Dim txtNakami As String


                    objWord = CreateObject("Word.Application")
                    objWord.Visible = False
                    objDoc = objWord.Documents.Open(OpenFileDialog1.FileName)
                    'objNewDoc = objWord.Documents.Add

                    objDoc.Range.Copy()
                    'Clipboard.GetText(TextBox3.Text)
                    'Clipboard.GetText(txtNakami)
                    'MsgBox(Clipboard.GetText, vbOKOnly)
                    txtNakami = Clipboard.GetText
                    For n = 0 To txtNakami.Length - 1
                        Select Case txtNakami.Substring(n, 1)
                            Case My.Settings.Fuka
                                cCounter = txtNakami.Substring(n + 1, 10)
                                cCounter = (Integer.Parse(cCounter.Substring(1, 2)) * 3600) + (Integer.Parse(cCounter.Substring(4, 2)) * 60) + (Integer.Parse(cCounter.Substring(7, 2)))
                                iLine = txtNakami.Substring(n + 2, 8) & "," & "聞き取り不可" & "," & cCounter & "," & "削除"
                                Dim RowPlus() As String = iLine.Split(",")
                                DataGridView1.Rows.Add(RowPlus) 'DataGrid行を追加
                                Clipboard.SetText(iLine)

                            Case My.Settings.Fumei
                                '次のゲタを探す
                                For i = n + 1 To txtNakami.Length - 1
                                    If txtNakami.Substring(i, 1) = My.Settings.Fumei Then
                                        strMemo = txtNakami.Substring(n + 1, i - n - 1)
                                        cCounter = txtNakami.Substring(i + 1, 10)
                                        cCounter = (Integer.Parse(cCounter.Substring(1, 2)) * 3600) + (Integer.Parse(cCounter.Substring(4, 2)) * 60) + (Integer.Parse(cCounter.Substring(7, 2)))
                                        iLine = txtNakami.Substring(i + 2, 8) & "," & strMemo & "？," & cCounter & "," & "削除"
                                        Dim RowPlus() As String = iLine.Split(",")
                                        DataGridView1.Rows.Add(RowPlus) 'DataGrid行を追加
                                        Exit For
                                    End If
                                Next i

                            Case My.Settings.Sonota
                                '次のカッコ（タイムコードの頭）を探す
                                For i = n + 1 To txtNakami.Length - 1
                                    If txtNakami.Substring(i, 1) = "(" Or txtNakami.Substring(i, 1) = "（" Then
                                        strMemo = txtNakami.Substring(n, i - n)
                                        cCounter = txtNakami.Substring(i, 10)
                                        cCounter = (Integer.Parse(cCounter.Substring(1, 2)) * 3600) + (Integer.Parse(cCounter.Substring(4, 2)) * 60) + (Integer.Parse(cCounter.Substring(7, 2)))
                                        iLine = txtNakami.Substring(i + 1, 8) & "," & strMemo & "," & cCounter & "," & "削除"
                                        Dim RowPlus() As String = iLine.Split(",")
                                        DataGridView1.Rows.Add(RowPlus) 'DataGrid行を追加
                                        Exit For
                                    End If
                                Next i

                        End Select
                    Next n


                    objDoc = Nothing
                    objNewDoc = Nothing
                    objWord = Nothing



                Case ".txt"
                    Using reader = New StreamReader(nFile, Encoding.GetEncoding("Shift_JIS"))
                        Dim txtNakami As String = reader.ReadToEnd()
                        'Clipboard.SetText(txtNakami)
                        For n = 0 To txtNakami.Length - 1
                            Select Case txtNakami.Substring(n, 1)
                                Case My.Settings.Fuka
                                    cCounter = txtNakami.Substring(n + 1, 10)
                                    cCounter = (Integer.Parse(cCounter.Substring(1, 2)) * 3600) + (Integer.Parse(cCounter.Substring(4, 2)) * 60) + (Integer.Parse(cCounter.Substring(7, 2)))
                                    iLine = txtNakami.Substring(n + 2, 8) & "," & "聞き取り不可" & "," & cCounter & "," & "削除"
                                    Dim RowPlus() As String = iLine.Split(",")
                                    DataGridView1.Rows.Add(RowPlus) 'DataGrid行を追加
                                    Clipboard.SetText(iLine)

                                Case My.Settings.Fumei
                                    '次のゲタを探す
                                    For i = n + 1 To txtNakami.Length - 1
                                        If txtNakami.Substring(i, 1) = My.Settings.Fumei Then
                                            strMemo = txtNakami.Substring(n + 1, i - n - 1)
                                            cCounter = txtNakami.Substring(i + 1, 10)
                                            cCounter = (Integer.Parse(cCounter.Substring(1, 2)) * 3600) + (Integer.Parse(cCounter.Substring(4, 2)) * 60) + (Integer.Parse(cCounter.Substring(7, 2)))
                                            iLine = txtNakami.Substring(i + 2, 8) & "," & strMemo & "？," & cCounter & "," & "削除"
                                            Dim RowPlus() As String = iLine.Split(",")
                                            DataGridView1.Rows.Add(RowPlus) 'DataGrid行を追加
                                            Exit For
                                        End If
                                    Next i

                                Case My.Settings.Sonota
                                    '次のカッコ（タイムコードの頭）を探す
                                    For i = n + 1 To txtNakami.Length - 1
                                        If txtNakami.Substring(i, 1) = "(" Or txtNakami.Substring(i, 1) = "（" Then
                                            strMemo = txtNakami.Substring(n, i - n)
                                            cCounter = txtNakami.Substring(i, 10)
                                            cCounter = (Integer.Parse(cCounter.Substring(1, 2)) * 3600) + (Integer.Parse(cCounter.Substring(4, 2)) * 60) + (Integer.Parse(cCounter.Substring(7, 2)))
                                            iLine = txtNakami.Substring(i + 1, 8) & "," & strMemo & "," & cCounter & "," & "削除"
                                            Dim RowPlus() As String = iLine.Split(",")
                                            DataGridView1.Rows.Add(RowPlus) 'DataGrid行を追加
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
    Sub CsvReader()

        Dim csvFile As String

        csvFile = OpenFileDialog1.FileName

        DataGridView1.Rows.Clear()

        Dim SR As New StreamReader(csvFile, System.Text.Encoding.GetEncoding("shift_jis")) 'StreamReader文字化け防止
        Dim con_str As String 'データ保持用

        'DataGridViewの初期化
        'DataGridView1.Columns.Clear()

        'csvファイルの1行目を読み込む
        con_str = SR.ReadLine()

        'データが無ければ終了
        If con_str = Nothing Then Exit Sub

        '列の生成
        'Dim ColPlus() As String = con_str.Split(""",""")

        'For Each da In ColPlus
        'DataGridView1.Columns.Add(da, da)
        'Next
        'DataGridView1.Rows.Add(ColPlus)

        Do
            con_str = SR.ReadLine() 'csvファイルの2行目以降を読み込む

            If con_str = Nothing Then Exit Do 'データが無ければループ終了
            con_str = Replace(con_str, """", "")

            Dim RowPlus() As String = con_str.Split(",")

            DataGridView1.Rows.Add(RowPlus) 'DataGrid行を追加

        Loop

        SR.Close()

        'ヘッダーと全てのセルの内容に合わせて、列の幅を自動調整する
        'DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        'ヘッダーと全てのセルの内容に合わせて、行の高さを自動調整する
        'DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells


    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim n As Integer
            Dim a As Integer
            Dim cCounter As String

            'カウンタが入力されていなければ、ボタンを押しても何も起こらない
            If TextBox2.Text = "" Then Exit Sub

            n = TextBox2.Text.Length

            '数字以外は飛ばして読み込む
            For a = 1 To n
                If Strings.Mid(TextBox2.Text, a, 1) = "0" Or Strings.Mid(TextBox2.Text, a, 1) = "1" Or Strings.Mid(TextBox2.Text, a, 1) = "2" Or Strings.Mid(TextBox2.Text, a, 1) = "3" Or Strings.Mid(TextBox2.Text, a, 1) = "4" Or Strings.Mid(TextBox2.Text, a, 1) = "5" Or Strings.Mid(TextBox2.Text, a, 1) = "6" Or Strings.Mid(TextBox2.Text, a, 1) = "7" Or Strings.Mid(TextBox2.Text, a, 1) = "8" Or Strings.Mid(TextBox2.Text, a, 1) = "9" Then
                    cCounter = cCounter & Strings.Mid(TextBox2.Text, a, 1)
                End If
            Next a

            '7桁以上の数字となった場合、エラーメッセージを出す
            If cCounter.Length > 6 Or cCounter < 0 Then
                MsgBox("数字が含まれていないか、7桁以上の数字が入力されています", vbOKOnly)
                TextBox2.Clear()
                Exit Sub
            End If

            '5桁以下の場合、6桁へ強制的に変更
            Select Case cCounter.Length
                Case 1
                    cCounter = "00000" & cCounter
                Case 2
                    cCounter = "0000" & cCounter
                Case 3
                    cCounter = "000" & cCounter
                Case 4
                    cCounter = "00" & cCounter
                Case 5
                    cCounter = "0" & cCounter
            End Select

            If (Integer.Parse(Strings.Mid(cCounter, 1, 2)) * 3600) + (Integer.Parse(Strings.Mid(cCounter, 3, 2)) * 60) + (Integer.Parse(Strings.Mid(cCounter, 5, 2))) > AxWindowsMediaPlayer1.currentMedia.duration Then
                MsgBox("入力されたカウンタがファイルの長さを超えています")
                TextBox2.Clear()
                Exit Sub
            End If

            TrackBar1.Value = (Integer.Parse(Strings.Mid(cCounter, 1, 2)) * 3600) + (Integer.Parse(Strings.Mid(cCounter, 3, 2)) * 60) + (Integer.Parse(Strings.Mid(cCounter, 5, 2)))
                AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value
                AxWindowsMediaPlayer1.Ctlcontrols.play()
                'Button200.Text = "一時停止"
                Button200.Image = My.Resources.Pause_16x
                Timer1.Enabled = True
                TextBox2.Clear()
            End If
    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        SendKeys.Send("%{PRTSC}")
        'クリップボードにあるデータの取得
        Dim d As IDataObject = Clipboard.GetDataObject()
        'ビットマップデータ形式に関連付けられているデータを取得
        Dim img As Image = CType(d.GetData(DataFormats.Bitmap), Image)
        'クリップボードにデータがあれば
        If Not (img Is Nothing) Then
            Dim g As Graphics = Form3.PictureBox1.CreateGraphics()
            'サムネイルを１２０×９０ピクセルの大きさにする
            Dim destinationRect As New RectangleF(0, 0, AxWindowsMediaPlayer1.Width, AxWindowsMediaPlayer1.Height)
            'クリップボードの画像の(80,90)-(300,200)の範囲をトリミング
            Dim sourceRect As New RectangleF(0, SystemInformation.CaptionHeight, AxWindowsMediaPlayer1.Width, AxWindowsMediaPlayer1.Height)
            'Dim sourceRect As New RectangleF(0, 0, 0, 0)
            g.DrawImage(img, destinationRect, sourceRect, GraphicsUnit.Pixel)
            g.Dispose()
        End If
        Form3.Show()
    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        Dim f2 As New Form2()
        AddHandler f2.FormClosed, AddressOf Form2_FormClosed

        If Me.TopMost = True Then
            Me.TopMost = False
            f2.ShowDialog()
        Else
            f2.ShowDialog()
        End If
    End Sub

    Private Sub SplitContainer1_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer1.SplitterMoved
        My.Settings.p11_height = SplitContainer1.Panel1.Height
        My.Settings.p11_width = SplitContainer1.Panel2.Width
        My.Settings.p12_height = SplitContainer1.Panel2.Height
        My.Settings.p12_width = SplitContainer1.Panel2.Width
        My.Settings.p21_height = SplitContainer2.Panel1.Height
        My.Settings.p21_width = SplitContainer2.Panel1.Width
        My.Settings.p22_height = SplitContainer2.Panel2.Height
        My.Settings.p22_width = SplitContainer2.Panel2.Width
    End Sub

    Private Sub SplitContainer2_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer2.SplitterMoved



        My.Settings.p11_height = SplitContainer1.Panel1.Height
        My.Settings.p11_width = SplitContainer1.Panel2.Width
        My.Settings.p12_height = SplitContainer1.Panel2.Height
        My.Settings.p12_width = SplitContainer1.Panel2.Width
        My.Settings.p21_height = SplitContainer2.Panel1.Height
        My.Settings.p21_width = SplitContainer2.Panel1.Width
        My.Settings.p22_height = SplitContainer2.Panel2.Height
        My.Settings.p22_width = SplitContainer2.Panel2.Width

    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        ToolTip1.SetToolTip(TrackBar1, TimeSpan.FromSeconds(TrackBar1.Value).ToString("hh\:mm\:ss"))
        Label1.Text = TimeSpan.FromSeconds(TrackBar1.Value).ToString("hh\:mm\:ss") & " / " & TimeSpan.FromSeconds(AxWindowsMediaPlayer1.currentMedia.duration).ToString("hh\:mm\:ss")
    End Sub


    Private Sub TableLayoutPanel1_DragDrop(sender As Object, e As DragEventArgs)
        Me.AllowDrop = True

        If (e.Data.GetDataPresent(
           DataFormats.FileDrop)) Then
            Dim files() As String
            '---get all the file names---
            files = e.Data.GetData(
               DataFormats.FileDrop)
            If files.Length > 0 Then
                '---load only the first file---
                files(0) = UCase(files(0))
                'If files(0).EndsWith(".mp4") Then
                '---get the media player to play the
                ' first file---
                AxWindowsMediaPlayer1.URL = files(0)
                My.Settings.LastOpenedFile = files(0)

                TrackBar2.Value = AxWindowsMediaPlayer1.settings.rate * 10
                Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")

                'End If
            End If
        End If
    End Sub

    Private Sub CheckBox2_Click(sender As Object, e As EventArgs) Handles CheckBox2.Click
        Dim motoSize As Integer = Me.Height
        If SplitContainer2.Panel1Collapsed = True Then
            Me.Height += My.Settings.p22_height
            SplitContainer2.Panel1Collapsed = False
            SplitContainer2.SplitterDistance = Me.Height - 240

            My.Settings.p11_height = SplitContainer1.Panel1.Height
            My.Settings.p11_width = SplitContainer1.Panel2.Width
            My.Settings.p12_height = SplitContainer1.Panel2.Height
            My.Settings.p12_width = SplitContainer1.Panel2.Width
            My.Settings.p21_height = SplitContainer2.Panel1.Height
            My.Settings.p21_width = SplitContainer2.Panel1.Width
            My.Settings.p22_height = SplitContainer2.Panel2.Height
            My.Settings.p22_width = SplitContainer2.Panel2.Width
            My.Settings.Save()

            CheckBox2.Checked = True
        Else
            SplitContainer2.Panel1Collapsed = True
            Me.Height = 240
            'me.width = 
            CheckBox2.Checked = False

            My.Settings.p11_height = SplitContainer1.Panel1.Height
            My.Settings.p11_width = SplitContainer1.Panel2.Width
            My.Settings.p12_height = SplitContainer1.Panel2.Height
            My.Settings.p12_width = SplitContainer1.Panel2.Width
            My.Settings.p21_height = SplitContainer2.Panel1.Height
            My.Settings.p21_width = SplitContainer2.Panel1.Width
            My.Settings.p22_height = SplitContainer2.Panel2.Height
            My.Settings.p22_width = SplitContainer2.Panel2.Width
            My.Settings.Save()


        End If
    End Sub

    Private Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click
        If (OpenFileDialog1.ShowDialog = DialogResult.OK) Then
            AxWindowsMediaPlayer1.URL = OpenFileDialog1.FileName
            My.Settings.LastOpenedFile = OpenFileDialog1.FileName

            TrackBar2.Value = AxWindowsMediaPlayer1.settings.rate * 10
            Label4.Text = "x" & (TrackBar2.Value * 0.1).ToString("0.0")

        End If
    End Sub


    'TableLayoutPanel1の一部に着色
    Private Sub TableLayoutPanel1_CellPaint(sender As Object, e As TableLayoutCellPaintEventArgs) Handles TableLayoutPanel1.CellPaint

        '一番上
        Dim KuroppoiColor2 As New SolidBrush(Color.FromArgb(50, 50, 50))
        If e.Column = 0 And e.Row = 0 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 1 And e.Row = 0 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 2 And e.Row = 0 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 3 And e.Row = 0 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 4 And e.Row = 0 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 5 And e.Row = 0 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 6 And e.Row = 0 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 7 And e.Row = 0 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 8 And e.Row = 0 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 9 And e.Row = 0 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 10 And e.Row = 0 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 11 And e.Row = 0 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 12 And e.Row = 0 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 13 And e.Row = 0 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 14 And e.Row = 0 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 15 And e.Row = 0 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 16 And e.Row = 0 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If


        If e.Column = 0 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 1 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 2 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 3 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 4 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 5 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 6 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 7 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 8 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 9 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 10 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 11 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 12 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 13 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 14 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 15 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 16 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 17 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 18 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 19 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 20 And e.Row = 1 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If


        '音量調整部分の着色
        Dim KuroppoiColor As New SolidBrush(Color.FromArgb(64, 64, 64))
        If e.Column = 20 And e.Row = 3 Then
            e.Graphics.FillRectangle(KuroppoiColor, e.CellBounds)
        End If
        If e.Column = 20 And e.Row = 4 Then
            e.Graphics.FillRectangle(KuroppoiColor, e.CellBounds)
        End If
        If e.Column = 20 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor, e.CellBounds)
        End If

        '再生・停止部分の着色
        If e.Column = 0 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor, e.CellBounds)
        End If
        If e.Column = 1 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor, e.CellBounds)
        End If
        If e.Column = 2 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor, e.CellBounds)
        End If
        If e.Column = 3 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor, e.CellBounds)
        End If
        If e.Column = 4 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor, e.CellBounds)
        End If
        If e.Column = 5 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor, e.CellBounds)
        End If
        If e.Column = 0 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor, e.CellBounds)
        End If
        If e.Column = 1 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor, e.CellBounds)
        End If
        If e.Column = 2 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor, e.CellBounds)
        End If
        If e.Column = 3 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor, e.CellBounds)
        End If
        If e.Column = 4 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor, e.CellBounds)
        End If
        If e.Column = 5 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor, e.CellBounds)
        End If

        '速度調整部分の着色

        If e.Column = 6 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 8 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 9 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 10 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 11 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 12 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 13 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 14 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 15 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 16 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 17 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 18 And e.Row = 5 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 6 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 7 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 8 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 9 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 10 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 11 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 12 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 13 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 14 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 15 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 16 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 17 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 18 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If
        If e.Column = 19 And e.Row = 6 Then
            e.Graphics.FillRectangle(KuroppoiColor2, e.CellBounds)
        End If

    End Sub

    Private Sub Form2_FormClosed(sender As Object, e As FormClosedEventArgs)
        Dim f2 As Form2 = DirectCast(sender, Form2)
        If Saizenmen = True Then
            Me.TopMost = True
        End If
    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        Me.TopMost = Not Me.TopMost
        If Me.TopMost = True Then
            Saizenmen = True
            Button35.Image = My.Resources.AlwaysVisible_16x
        Else
            Saizenmen = False
            Button35.Image = My.Resources.PinnedItem_16x
        End If
    End Sub

    Private Sub Form1_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus

        Dim PuraMai As String
        'Player本体のコントロールをすべて無効
        AxWindowsMediaPlayer1.uiMode = "none"

        'ジャンプボタン表示用
        PuraMai = ""
        If System.Math.Abs(My.Settings.SK1) >= 60 Then
            If My.Settings.SK1 > 0 Then
                PuraMai = "+"
            End If
            Button1.Text = PuraMai & My.Settings.SK1 / 60 & "m"
        Else
            If My.Settings.SK1 > 0 Then
                PuraMai = "+"
            End If
            Button1.Text = PuraMai & My.Settings.SK1 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK11) >= 60 Then

            If My.Settings.SK11 > 0 Then
                PuraMai = "+"
            End If
            Button11.Text = PuraMai & My.Settings.SK11 / 60 & "m"
        Else
            If My.Settings.SK11 > 0 Then
                PuraMai = "+"
            End If
            Button11.Text = PuraMai & My.Settings.SK11 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK2) >= 60 Then
            If My.Settings.SK2 > 0 Then
                PuraMai = "+"
            End If
            Button2.Text = PuraMai & My.Settings.SK2 / 60 & "m"
        Else
            If My.Settings.SK2 > 0 Then
                PuraMai = "+"
            End If
            Button2.Text = PuraMai & My.Settings.SK2 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK12) >= 60 Then
            If My.Settings.SK12 > 0 Then
                PuraMai = "+"
            End If
            Button12.Text = PuraMai & My.Settings.SK12 / 60 & "m"
        Else
            If My.Settings.SK12 > 0 Then
                PuraMai = "+"
            End If
            Button12.Text = PuraMai & My.Settings.SK12 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK3) >= 60 Then
            If My.Settings.SK3 > 0 Then
                PuraMai = "+"
            End If
            Button3.Text = PuraMai & My.Settings.SK3 / 60 & "m"
        Else
            If My.Settings.SK3 > 0 Then
                PuraMai = "+"
            End If
            Button3.Text = PuraMai & My.Settings.SK3 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK13) >= 60 Then
            If My.Settings.SK13 > 0 Then
                PuraMai = "+"
            End If
            Button13.Text = PuraMai & My.Settings.SK13 / 60 & "m"
        Else
            If My.Settings.SK13 > 0 Then
                PuraMai = "+"
            End If
            Button13.Text = PuraMai & My.Settings.SK13 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK4) >= 60 Then
            If My.Settings.SK4 > 0 Then
                PuraMai = "+"
            End If
            Button4.Text = PuraMai & My.Settings.SK4 / 60 & "m"
        Else
            If My.Settings.SK4 > 0 Then
                PuraMai = "+"
            End If
            Button4.Text = PuraMai & My.Settings.SK4 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK14) >= 60 Then
            If My.Settings.SK14 > 0 Then
                PuraMai = "+"
            End If
            Button14.Text = PuraMai & My.Settings.SK14 / 60 & "m"
        Else
            If My.Settings.SK14 > 0 Then
                PuraMai = "+"
            End If
            Button14.Text = PuraMai & My.Settings.SK14 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK5) >= 60 Then
            If My.Settings.SK5 > 0 Then
                PuraMai = "+"
            End If
            Button5.Text = PuraMai & My.Settings.SK5 / 60 & "m"
        Else
            If My.Settings.SK5 > 0 Then
                PuraMai = "+"
            End If
            Button5.Text = PuraMai & My.Settings.SK5 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK15) >= 60 Then
            If My.Settings.SK15 > 0 Then
                PuraMai = "+"
            End If
            Button15.Text = PuraMai & My.Settings.SK15 / 60 & "m"
        Else
            If My.Settings.SK15 > 0 Then
                PuraMai = "+"
            End If
            Button15.Text = PuraMai & My.Settings.SK15 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK6) >= 60 Then
            If My.Settings.SK6 > 0 Then
                PuraMai = "+"
            End If
            Button6.Text = PuraMai & My.Settings.SK6 / 60 & "m"
        Else
            If My.Settings.SK6 > 0 Then
                PuraMai = "+"
            End If
            Button6.Text = PuraMai & My.Settings.SK6 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK16) >= 60 Then
            If My.Settings.SK16 > 0 Then
                PuraMai = "+"
            End If
            Button16.Text = PuraMai & My.Settings.SK16 / 60 & "m"
        Else
            If My.Settings.SK16 > 0 Then
                PuraMai = "+"
            End If
            Button16.Text = PuraMai & My.Settings.SK16 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK7) >= 60 Then
            If My.Settings.SK7 > 0 Then
                PuraMai = "+"
            End If
            Button7.Text = PuraMai & My.Settings.SK7 / 60 & "m"
        Else
            If My.Settings.SK7 > 0 Then
                PuraMai = "+"
            End If
            Button7.Text = PuraMai & My.Settings.SK7 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK17) >= 60 Then
            If My.Settings.SK17 > 0 Then
                PuraMai = "+"
            End If
            Button17.Text = PuraMai & My.Settings.SK17 / 60 & "m"
        Else
            If My.Settings.SK17 > 0 Then
                PuraMai = "+"
            End If
            Button17.Text = PuraMai & My.Settings.SK17 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK8) >= 60 Then
            If My.Settings.SK8 > 0 Then
                PuraMai = "+"
            End If
            Button8.Text = PuraMai & My.Settings.SK8 / 60 & "m"
        Else
            If My.Settings.SK8 > 0 Then
                PuraMai = "+"
            End If
            Button8.Text = PuraMai & My.Settings.SK8 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK18) >= 60 Then
            If My.Settings.SK18 > 0 Then
                PuraMai = "+"
            End If
            Button18.Text = PuraMai & My.Settings.SK18 / 60 & "m"
        Else
            If My.Settings.SK18 > 0 Then
                PuraMai = "+"
            End If
            Button18.Text = PuraMai & My.Settings.SK18 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK9) >= 60 Then
            If My.Settings.SK9 > 0 Then
                PuraMai = "+"
            End If
            Button9.Text = PuraMai & My.Settings.SK9 / 60 & "m"
        Else
            If My.Settings.SK9 > 0 Then
                PuraMai = "+"
            End If
            Button9.Text = PuraMai & My.Settings.SK9 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK19) >= 60 Then
            If My.Settings.SK19 > 0 Then
                PuraMai = "+"
            End If
            Button19.Text = PuraMai & My.Settings.SK19 / 60 & "m"
        Else
            If My.Settings.SK19 > 0 Then
                PuraMai = "+"
            End If
            Button19.Text = PuraMai & My.Settings.SK19 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK10) >= 60 Then
            If My.Settings.SK10 > 0 Then
                PuraMai = "+"
            End If
            Button10.Text = PuraMai & My.Settings.SK10 / 60 & "m"
        Else
            If My.Settings.SK10 > 0 Then
                PuraMai = "+"
            End If
            Button10.Text = PuraMai & My.Settings.SK10 & "s"
        End If

        PuraMai = ""
        If System.Math.Abs(My.Settings.SK20) >= 60 Then
            If My.Settings.SK20 > 0 Then
                PuraMai = "+"
            End If
            Button20.Text = PuraMai & My.Settings.SK20 / 60 & "m"
        Else
            If My.Settings.SK20 > 0 Then
                PuraMai = "+"
            End If
            Button20.Text = PuraMai & My.Settings.SK20 & "s"
        End If

        Button21.Text = (My.Settings.SC1 / 100).ToString("0.0")
        Button22.Text = (My.Settings.SC2 / 100).ToString("0.0")
        Button23.Text = (My.Settings.SC3 / 100).ToString("0.0")
        Button24.Text = (My.Settings.SC4 / 100).ToString("0.0")
        Button25.Text = (My.Settings.SC5 / 100).ToString("0.0")
        Button26.Text = (My.Settings.SC6 / 100).ToString("0.0")
        Button27.Text = (My.Settings.SC7 / 100).ToString("0.0")
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

    End Sub

    Private Sub Form1_MinimumSizeChanged(sender As Object, e As EventArgs) Handles Me.MinimumSizeChanged

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class





