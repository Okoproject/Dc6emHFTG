Imports System.ComponentModel

Public Class Form2
    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim Handan2 As Short = 0
        Dim KeyBangou As Integer
        Dim HotKeyAA As String

        Dim KeyShushoku As Integer
        Dim Zenhan As String = ""
        Dim Kouhan As String = ""

        Dim CS1 As Integer

        If ComboBox2.Text = "" Then
            MsgBox("Please enter shortcut key.", vbOKOnly, "Error")
            Exit Sub
        End If




        '3:Ctrl 4:Alt 5:Shift 7:Ctrl+Alt 8:Ctrl+Shift 9:Alt+Shift 12:Ctrl+Alt+Shift
        If CheckBox2.Checked Then
            Handan2 += 3
        End If
        If CheckBox3.Checked = True Then
            Handan2 += 4
        End If
        If CheckBox4.Checked = True Then
            Handan2 += 5
        End If





        Select Case ComboBox2.SelectedIndex
            Case 0
                KeyBangou = 112
            Case 1
                KeyBangou = 113
            Case 2
                KeyBangou = 114
            Case 3
                KeyBangou = 115
            Case 4
                KeyBangou = 116
            Case 5
                KeyBangou = 117
            Case 6
                KeyBangou = 118
            Case 7
                KeyBangou = 119
            Case 8
                KeyBangou = 120
            Case 9
                KeyBangou = 121
            Case 10
                KeyBangou = 122
            Case 11
                KeyBangou = 123
            Case 12
                KeyBangou = 124
            Case 13
                KeyBangou = 125
            Case 14
                KeyBangou = 126
            Case 15
                KeyBangou = 127
            Case 16
                KeyBangou = 38
            Case 17
                KeyBangou = 40
            Case 18
                KeyBangou = 37
            Case 19
                KeyBangou = 39
            '0(10key)
            Case 56
                KeyBangou = 96
            '1(10key)
            Case 57
                KeyBangou = 97
            '2(10key)
            Case 58
                KeyBangou = 98
            '3(10key)
            Case 59
                KeyBangou = 99
            '4(10key)
            Case 60
                KeyBangou = 100
            '5(10key)
            Case 61
                KeyBangou = 101
            '6(10key)
            Case 62
                KeyBangou = 102
            '7(10key)
            Case 63
                KeyBangou = 103
            '8(10key)
            Case 64
                KeyBangou = 104
            '9(10key)
            Case 65
                KeyBangou = 105

            'Next(Media)
            Case 66
                KeyBangou = 176
            'PlayPause(Media)
            Case 67
                KeyBangou = 179
            'Previous(Media)
            Case 68
                KeyBangou = 177
            'Stop(Media)
            Case 69
                KeyBangou = 178


            Case Else
                KeyBangou = Asc(ComboBox2.Text)
        End Select


        Select Case ComboBox1.SelectedIndex
            'Play / Pause
            Case 0
                HotKeyAA = hotkeyID_A
            'Play / Pause & Copying counter1
            Case 1
                HotKeyAA = hotkeyID_B
            'Play / Pause & Copying counter2
            Case 2
                HotKeyAA = hotkeyID_C
            'Stop
            Case 4
                HotKeyAA = hotkeyID_D
            'Play / Pause & Copying counter3
            Case 3
                HotKeyAA = hotkeyID_E
            'Copying counter1
            Case 5
                HotKeyAA = hotkeyID_T
            'Copying counter2
            Case 6
                HotKeyAA = hotkeyID_F
            'Copying counter3
            Case 7
                HotKeyAA = hotkeyID_G
            'Bookmark
            Case 8
                HotKeyAA = hotkeyID_H
            'Play & Pause & Bookmark
            Case 9
                HotKeyAA = hotkeyID_I
            'Jump1
            Case 10
                HotKeyAA = hotkeyID_MM1
            'Jump2
            Case 11
                HotKeyAA = hotkeyID_MM2
            'Jump3
            Case 12
                HotKeyAA = hotkeyID_MM3
            'Jump4
            Case 13
                HotKeyAA = hotkeyID_HO1
            'Jump5
            Case 14
                HotKeyAA = hotkeyID_HO2
            'Jump6
            Case 15
                HotKeyAA = hotkeyID_HO3
            'Speed +0.1
            Case 16
                HotKeyAA = hotkeyID_J
            'Speed -0.1
            Case 17
                HotKeyAA = hotkeyID_K
            'Speed x1
            Case 18
                HotKeyAA = hotkeyID_L
            'Speed x0.5
            Case 19
                HotKeyAA = hotkeyID_M
            'Speed x2.0
            Case 20
                HotKeyAA = hotkeyID_N
            'mosttop
            Case 21
                HotKeyAA = hotkeyID_O
            'Speed Control Button1
            Case 22
                HotKeyAA = hotkeyID_SC1
            'Speed Control Button2
            Case 23
                HotKeyAA = hotkeyID_SC2
            'Speed Control Button3
            Case 24
                HotKeyAA = hotkeyID_SC3
            'Speed Control Button4
            Case 25
                HotKeyAA = hotkeyID_SC4
            'Speed Control Button5
            Case 26
                HotKeyAA = hotkeyID_SC5
            'Speed Control Button6
            Case 27
                HotKeyAA = hotkeyID_SC6
            'Speed Control Button7
            Case 28
                HotKeyAA = hotkeyID_SC7
            Case 29
                HotKeyAA = hotkeyID_P
            Case Else
                Exit Select

                'Case Else
                'MsgBox("項目をドロップダウンボックスから選択してください", vbOKOnly)
                'Exit Sub
        End Select








        '対象のホットキーを解除
        UnregisterHotKey(Form1.Handle, HotKeyAA)
        'GlobalDeleteAtom(HotKeyAA)








        Select Case Handan2

            'none
            Case 0
                If HotKeyAA = "" Then
                    RegisterHotKey(Form1.Handle, 0, 0, KeyBangou)
                Else
                    RegisterHotKey(Form1.Handle, 0, HotKeyAA, KeyBangou)
                    MsgBox("設定完了", vbOKOnly)

                End If
            'Ctrl
            Case 3
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_CONTROL, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            'Alt
            Case 4
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_ALT, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            'Shift
            Case 5
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_SHIFT, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_CONTROL Or MOD_ALT, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_CONTROL Or MOD_SHIFT, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            'Alt + Shift
            Case 9
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_ALT Or MOD_SHIFT, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, KeyBangou)
                MsgBox("設定完了", vbOKOnly)


        End Select


        'ホットキーを設定ファイルに記憶
        Select Case HotKeyAA
            Case hotkeyID_A
                My.Settings.SKAA = Handan2
                My.Settings.SKA = KeyBangou
            Case hotkeyID_B
                My.Settings.SKBA = Handan2
                My.Settings.SKB = KeyBangou
            Case hotkeyID_C
                My.Settings.SKCA = Handan2
                My.Settings.SKC = KeyBangou
            Case hotkeyID_D
                My.Settings.SKDA = Handan2
                My.Settings.SKD = KeyBangou
            Case hotkeyID_E
                My.Settings.SKEA = Handan2
                My.Settings.SKE = KeyBangou
            Case hotkeyID_F
                My.Settings.SKFA = Handan2
                My.Settings.SKF = KeyBangou
            Case hotkeyID_G
                My.Settings.SKGA = Handan2
                My.Settings.SKG = KeyBangou
            Case hotkeyID_H
                My.Settings.SKHA = Handan2
                My.Settings.SKH = KeyBangou
            Case hotkeyID_I
                My.Settings.SKIA = Handan2
                My.Settings.SKI = KeyBangou
            Case hotkeyID_J
                My.Settings.SKJA = Handan2
                My.Settings.SKJ = KeyBangou
            Case hotkeyID_K
                My.Settings.SKKA = Handan2
                My.Settings.SKK = KeyBangou
            Case hotkeyID_L
                My.Settings.SKLA = Handan2
                My.Settings.SKL = KeyBangou
            Case hotkeyID_M
                My.Settings.SKMA = Handan2
                My.Settings.SKM = KeyBangou
            Case hotkeyID_N
                My.Settings.SKNA = Handan2
                My.Settings.SKN = KeyBangou
            Case hotkeyID_O
                My.Settings.SKOA = Handan2
                My.Settings.SKO = KeyBangou
            Case hotkeyID_P
                My.Settings.SKPA = Handan2
                My.Settings.SKP = KeyBangou
            Case hotkeyID_Q
                My.Settings.SKQA = Handan2
                My.Settings.SKQ = KeyBangou
            Case hotkeyID_R
                My.Settings.SKRA = Handan2
                My.Settings.SKR = KeyBangou
            Case hotkeyID_S
                My.Settings.SKSA = Handan2
                My.Settings.SKS = KeyBangou
            Case hotkeyID_T
                My.Settings.SKTA = Handan2
                My.Settings.SKT = KeyBangou
            Case hotkeyID_U
                My.Settings.SKUA = Handan2
                My.Settings.SKU = KeyBangou
            Case hotkeyID_V
                My.Settings.SKVA = Handan2
                My.Settings.SKV = KeyBangou
            Case hotkeyID_W
                My.Settings.SKWA = Handan2
                My.Settings.SKW = KeyBangou
            Case hotkeyID_X
                My.Settings.SKXA = Handan2
                My.Settings.SKX = KeyBangou
            Case hotkeyID_Y
                My.Settings.SKYA = Handan2
                My.Settings.SKY = KeyBangou
            Case hotkeyID_Z
                My.Settings.SKZA = Handan2
                My.Settings.SKZ = KeyBangou

            Case hotkeyID_MM1
                My.Settings.SKM1A = Handan2
                My.Settings.SKM1 = KeyBangou
            Case hotkeyID_MM2
                My.Settings.SKM2A = Handan2
                My.Settings.SKM2 = KeyBangou
            Case hotkeyID_MM3
                My.Settings.SKM3A = Handan2
                My.Settings.SKM3 = KeyBangou
            Case hotkeyID_HO1
                My.Settings.SKH1A = Handan2
                My.Settings.SKH1 = KeyBangou
            Case hotkeyID_HO2
                My.Settings.SKH2A = Handan2
                My.Settings.SKH2 = KeyBangou
            Case hotkeyID_HO3
                My.Settings.SKH3A = Handan2
                My.Settings.SKH3 = KeyBangou

            Case hotkeyID_SC1
                My.Settings.SC1A = Handan2
                My.Settings.SKSC1 = KeyBangou
            Case hotkeyID_SC2
                My.Settings.SC2A = Handan2
                My.Settings.SKSC2 = KeyBangou
            Case hotkeyID_SC3
                My.Settings.SC3A = Handan2
                My.Settings.SKSC3 = KeyBangou
            Case hotkeyID_SC4
                My.Settings.SC4A = Handan2
                My.Settings.SKSC4 = KeyBangou
            Case hotkeyID_SC5
                My.Settings.SC5A = Handan2
                My.Settings.SKSC5 = KeyBangou
            Case hotkeyID_SC6
                My.Settings.SC6A = Handan2
                My.Settings.SKSC6 = KeyBangou
            Case hotkeyID_SC7
                My.Settings.SC7A = Handan2
                My.Settings.SKSC7 = KeyBangou

        End Select




    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim HotKeyAA As Short

        Select Case ComboBox1.SelectedIndex
            'Play / Pause
            Case 0
                HotKeyAA = hotkeyID_A
            'Play / Pause & Copying counter1
            Case 1
                HotKeyAA = hotkeyID_B
            'Play / Pause & Copying counter2
            Case 2
                HotKeyAA = hotkeyID_C
            'Stop
            Case 3
                HotKeyAA = hotkeyID_D
            'Play / Pause & Copying counter3
            Case 4
                HotKeyAA = hotkeyID_E
            'Copying counter1
            Case 5
                HotKeyAA = hotkeyID_T
            'Copying counter2
            Case 6
                HotKeyAA = hotkeyID_F
            'Copying counter3
            Case 7
                HotKeyAA = hotkeyID_G
            Case 8
                HotKeyAA = hotkeyID_H
            Case 9
                HotKeyAA = hotkeyID_I


            Case 10
                HotKeyAA = hotkeyID_MM1
            Case 11
                HotKeyAA = hotkeyID_MM2
            Case 12
                HotKeyAA = hotkeyID_MM3
            Case 13
                HotKeyAA = hotkeyID_HO1
            Case 14
                HotKeyAA = hotkeyID_HO2
            Case 15
                HotKeyAA = hotkeyID_HO3
            Case 16
                HotKeyAA = hotkeyID_J
            Case 17
                HotKeyAA = hotkeyID_K
            Case 18
                HotKeyAA = hotkeyID_L
            Case 19
                HotKeyAA = hotkeyID_M
            Case 20
                HotKeyAA = hotkeyID_N
            Case 21
                HotKeyAA = hotkeyID_O

            Case 22
                HotKeyAA = hotkeyID_SC1
            Case 23
                HotKeyAA = hotkeyID_SC2
            Case 24
                HotKeyAA = hotkeyID_SC3
            Case 25
                HotKeyAA = hotkeyID_SC4
            Case 26
                HotKeyAA = hotkeyID_SC5
            Case 27
                HotKeyAA = hotkeyID_SC6
            Case 28
                HotKeyAA = hotkeyID_SC7

            Case 29
                HotKeyAA = hotkeyID_P

        End Select


        '対象のホットキーを解除
        UnregisterHotKey(Form1.Handle, HotKeyAA)
        'GlobalDeleteAtom(HotKeyAA)

        'ホットキーを設定ファイルから削除
        Select Case HotKeyAA
            Case hotkeyID_A
                My.Settings.SKAA = -1
                My.Settings.SKA = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_B
                My.Settings.SKBA = -1
                My.Settings.SKB = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_C
                My.Settings.SKCA = -1
                My.Settings.SKC = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_D
                My.Settings.SKDA = -1
                My.Settings.SKD = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_E
                My.Settings.SKEA = -1
                My.Settings.SKE = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_F
                My.Settings.SKFA = -1
                My.Settings.SKF = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_G
                My.Settings.SKGA = -1
                My.Settings.SKG = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_H
                My.Settings.SKHA = -1
                My.Settings.SKH = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_I
                My.Settings.SKIA = -1
                My.Settings.SKI = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_J
                My.Settings.SKJA = -1
                My.Settings.SKJ = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_K
                My.Settings.SKKA = -1
                My.Settings.SKK = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_L
                My.Settings.SKLA = -1
                My.Settings.SKL = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_M
                My.Settings.SKMA = -1
                My.Settings.SKM = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_N
                My.Settings.SKNA = -1
                My.Settings.SKN = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_O
                My.Settings.SKOA = -1
                My.Settings.SKO = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_P
                My.Settings.SKPA = -1
                My.Settings.SKP = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_Q
                My.Settings.SKQA = -1
                My.Settings.SKQ = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_R
                My.Settings.SKRA = -1
                My.Settings.SKR = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_S
                My.Settings.SKSA = -1
                My.Settings.SKS = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_T
                My.Settings.SKTA = -1
                My.Settings.SKT = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_U
                My.Settings.SKUA = -1
                My.Settings.SKU = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_V
                My.Settings.SKVA = -1
                My.Settings.SKV = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_W
                My.Settings.SKWA = -1
                My.Settings.SKW = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_X
                My.Settings.SKXA = -1
                My.Settings.SKX = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_Y
                My.Settings.SKYA = -1
                My.Settings.SKY = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_Z
                My.Settings.SKZA = -1
                My.Settings.SKZ = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"

            Case hotkeyID_MM1
                My.Settings.SKM1A = -1
                My.Settings.SKM1 = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_MM2
                My.Settings.SKM2A = -1
                My.Settings.SKM2 = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_MM3
                My.Settings.SKM3A = -1
                My.Settings.SKM3 = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_HO1
                My.Settings.SKH1A = -1
                My.Settings.SKH1 = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_HO2
                My.Settings.SKH2A = -1
                My.Settings.SKH2 = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"
            Case hotkeyID_HO3
                My.Settings.SKH3A = -1
                My.Settings.SKH3 = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = "　"


            Case hotkeyID_SC1
                My.Settings.SC1A = -1
                My.Settings.SKSC1 = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = " "
            Case hotkeyID_SC2
                My.Settings.SC2A = -1
                My.Settings.SKSC2 = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = " "
            Case hotkeyID_SC3
                My.Settings.SC3A = -1
                My.Settings.SKSC3 = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = " "
            Case hotkeyID_SC4
                My.Settings.SC4A = -1
                My.Settings.SKSC4 = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = " "
            Case hotkeyID_SC5
                My.Settings.SC5A = -1
                My.Settings.SKSC5 = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = " "
            Case hotkeyID_SC6
                My.Settings.SC6A = -1
                My.Settings.SKSC6 = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = " "
            Case hotkeyID_SC7
                My.Settings.SC7A = -1
                My.Settings.SKSC7 = -1
                My.Settings.Save()
                MsgBox("キー設定を削除しました", vbOKOnly)
                Label3.Text = " "

            Case Else
                MsgBox("エラー", vbOKOnly)

        End Select

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        Dim KeyBangou As Integer
        Dim KeyShushoku As Integer
        Dim Zenhan As String = ""
        Dim Kouhan As String = ""



        Select Case ComboBox1.SelectedIndex
            'Play / Pause
            Case 0
                KeyShushoku = My.Settings.SKAA
                KeyBangou = My.Settings.SKA
            'Play / Pause & Copying counter1
            Case 1
                KeyShushoku = My.Settings.SKBA
                KeyBangou = My.Settings.SKB
            'Play / Pause & Copying counter2
            Case 2
                KeyShushoku = My.Settings.SKCA
                KeyBangou = My.Settings.SKC
            'Stop
            Case 4
                KeyShushoku = My.Settings.SKDA
                KeyBangou = My.Settings.SKD
            'Play & Pause & Copying counter3
            Case 3
                KeyShushoku = My.Settings.SKEA
                KeyBangou = My.Settings.SKE
            'Copying Counter1
            Case 5
                KeyShushoku = My.Settings.SKTA
                KeyBangou = My.Settings.SKT
            'Copying Counter2
            Case 6
                KeyShushoku = My.Settings.SKFA
                KeyBangou = My.Settings.SKF
            'Copying Counter3
            Case 7
                KeyShushoku = My.Settings.SKGA
                KeyBangou = My.Settings.SKG
            'Bookmark
            Case 8
                KeyShushoku = My.Settings.SKHA
                KeyBangou = My.Settings.SKH
            'Play / Pause & Bookmark
            Case 9
                KeyShushoku = My.Settings.SKIA
                KeyBangou = My.Settings.SKI
            Case 10
                KeyShushoku = My.Settings.SKM1A
                KeyBangou = My.Settings.SKM1
            Case 11
                KeyShushoku = My.Settings.SKM2A
                KeyBangou = My.Settings.SKM2
            Case 12
                KeyShushoku = My.Settings.SKM3A
                KeyBangou = My.Settings.SKM3
            Case 13
                KeyShushoku = My.Settings.SKH1A
                KeyBangou = My.Settings.SKH1
            Case 14
                KeyShushoku = My.Settings.SKH2A
                KeyBangou = My.Settings.SKH2
            Case 15
                KeyShushoku = My.Settings.SKH3A
                KeyBangou = My.Settings.SKH3

            Case 16
                KeyShushoku = My.Settings.SKJA
                KeyBangou = My.Settings.SKJ
            Case 17
                KeyShushoku = My.Settings.SKKA
                KeyBangou = My.Settings.SKK
            Case 18
                KeyShushoku = My.Settings.SKLA
                KeyBangou = My.Settings.SKL
            Case 19
                KeyShushoku = My.Settings.SKMA
                KeyBangou = My.Settings.SKM
            Case 20
                KeyShushoku = My.Settings.SKNA
                KeyBangou = My.Settings.SKN
            Case 21
                KeyShushoku = My.Settings.SKOA
                KeyBangou = My.Settings.SKO

            Case 22
                KeyShushoku = My.Settings.SC1A
                KeyBangou = My.Settings.SKSC1
            Case 23
                KeyShushoku = My.Settings.SC2A
                KeyBangou = My.Settings.SKSC2
            Case 24
                KeyShushoku = My.Settings.SC3A
                KeyBangou = My.Settings.SKSC3
            Case 25
                KeyShushoku = My.Settings.SC4A
                KeyBangou = My.Settings.SKSC4
            Case 26
                KeyShushoku = My.Settings.SC5A
                KeyBangou = My.Settings.SKSC5
            Case 27
                KeyShushoku = My.Settings.SC6A
                KeyBangou = My.Settings.SKSC6
            Case 28
                KeyShushoku = My.Settings.SC7A
                KeyBangou = My.Settings.SKSC7

            Case 29
                KeyShushoku = My.Settings.SKPA
                KeyBangou = My.Settings.SKP


        End Select

        Zenhan = ShuHan(KeyShushoku)


        'GoTo 10 ←なんだっけ？
        '3:Ctrl 4:Alt 5:Shift 7:Ctrl+Alt 8:Ctrl+Shift 9:Alt+Shift 12:Ctrl+Alt+Shift

        Select Case KeyShushoku

            'none
            Case 0
                Zenhan = ""
            'Ctrl
            Case 3
                Zenhan = "Ctrl"
            'Alt
            Case 4
                Zenhan = "Alt"

            'Shift
            Case 5
                Zenhan = "Shift"
            'Ctrl + Alt
            Case 7
                Zenhan = "Ctrl + Alt"
            'Ctrl + Shift
            Case 8
                Zenhan = "Ctrl + Shift"
            'Alt + Shift
            Case 9
                Zenhan = "Alt + Shift"
            'Ctrl + Alt + Shift
            Case 12
                Zenhan = "Ctrl + Alt + Shift"


        End Select

10:

        Select Case KeyBangou
            Case 112
                Kouhan = " + F1"
            Case 113
                Kouhan = " + F2"
            Case 114
                Kouhan = " + F3"
            Case 115
                Kouhan = " + F4"
            Case 116
                Kouhan = " + F5"
            Case 117
                Kouhan = " + F6"
            Case 118
                Kouhan = " + F7"
            Case 119
                Kouhan = " + F8"
            Case 120
                Kouhan = " + F9"
            Case 121
                Kouhan = " + F10"
            Case 122
                Kouhan = " + F11"
            Case 123
                Kouhan = " + F12"
            Case 124
                Kouhan = " + F13"
            Case 125
                Kouhan = " + F14"
            Case 126
                Kouhan = " + F15"
            Case 127
                Kouhan = " + F16"
            Case 38
                Kouhan = " + ↑"
            Case 40
                Kouhan = " + ↓"
            Case 37
                Kouhan = " + ←"
            Case 39
                Kouhan = " + →"
            Case 65
                Kouhan = " + A"
            Case 66
                Kouhan = " + B"
            Case 67
                Kouhan = " + C"
            Case 68
                Kouhan = " + D"
            Case 69
                Kouhan = " + E"
            Case 70
                Kouhan = " + F"
            Case 71
                Kouhan = " + G"
            Case 72
                Kouhan = " + H"
            Case 73
                Kouhan = " + I"
            Case 74
                Kouhan = " + J"
            Case 75
                Kouhan = " + K"
            Case 76
                Kouhan = " + L"
            Case 77
                Kouhan = " + M"
            Case 78
                Kouhan = " + N"
            Case 79
                Kouhan = " + O"
            Case 80
                Kouhan = " + P"
            Case 81
                Kouhan = " + Q"
            Case 82
                Kouhan = " + R"
            Case 83
                Kouhan = " + S"
            Case 84
                Kouhan = " + T"
            Case 85
                Kouhan = " + U"
            Case 86
                Kouhan = " + V"
            Case 87
                Kouhan = " + W"
            Case 88
                Kouhan = " + X"
            Case 89
                Kouhan = " + Y"
            Case 90
                Kouhan = " + Z"
            Case 48
                Kouhan = " + 0"
            Case 49
                Kouhan = " + 1"
            Case 50
                Kouhan = " + 2"
            Case 51
                Kouhan = " + 3"
            Case 52
                Kouhan = " + 4"
            Case 53
                Kouhan = " + 5"
            Case 54
                Kouhan = " + 6"
            Case 55
                Kouhan = " + 7"
            Case 56
                Kouhan = " + 8"
            Case 57
                Kouhan = " + 9"
            Case 58
                Kouhan = ""
            Case 96
                Kouhan = " + 0(テンキー）"
            Case 97
                Kouhan = " + 1(テンキー）"
            Case 98
                Kouhan = " + 2(テンキー）"
            Case 99
                Kouhan = " + 3(テンキー）"
            Case 100
                Kouhan = " + 4(テンキー）"
            Case 101
                Kouhan = " + 5(テンキー）"
            Case 102
                Kouhan = " + 6(テンキー）"
            Case 103
                Kouhan = " + 7(テンキー）"
            Case 104
                Kouhan = " + 8(テンキー）"
            Case 105
                Kouhan = " + 9(テンキー）"
            Case 176
                Kouhan = " + 次のトラックキー"
            Case 179
                Kouhan = " + 再生/一時停止キー"
            Case 177
                Kouhan = " + 前のトラックキー"
            Case 178
                Kouhan = " + 停止キー"

        End Select

        If Zenhan = "" Then
            Label3.Text = Kouhan.Replace(" + ", "")
        Else
            Label3.Text = Zenhan & Kouhan

        End If
        If Label3.Text = "Ctrl + Ctrl" Then
            Label3.Text = "Ctrl"
        End If


    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        On Error Resume Next
        '自分自身のバージョン情報を取得する
        Dim ver As System.Diagnostics.FileVersionInfo =
    System.Diagnostics.FileVersionInfo.GetVersionInfo(
    System.Reflection.Assembly.GetExecutingAssembly().Location)
        '結果を表示
        'Label13.Text = ver.ProductName & " Ver." & ver.FileVersion & " (C) 2021 Teruhisa Yoshioka"

        Select Case My.Settings.TimeCode
            Case 0
                RadioButton1.Checked = True
            Case 1
                RadioButton2.Checked = True
            Case 2
                RadioButton3.Checked = True
        End Select


        NumericUpDown1.Value = My.Settings.AutoBack / 10
        TextBox1.Text = My.Settings.Atama
        TextBox2.Text = My.Settings.Oshiri
        TextBox4.Text = My.Settings.Atama2
        TextBox3.Text = My.Settings.Oshiri2
        TextBox6.Text = My.Settings.Atama3
        TextBox5.Text = My.Settings.Oshiri3

        NumericUpDown2.Value = My.Settings.SK1
        NumericUpDown12.Value = My.Settings.SK11
        NumericUpDown3.Value = My.Settings.SK2
        NumericUpDown13.Value = My.Settings.SK12
        NumericUpDown4.Value = My.Settings.SK3
        NumericUpDown14.Value = My.Settings.SK13
        NumericUpDown5.Value = My.Settings.SK4
        NumericUpDown15.Value = My.Settings.SK14
        NumericUpDown6.Value = My.Settings.SK5
        NumericUpDown16.Value = My.Settings.SK15
        NumericUpDown7.Value = My.Settings.SK6
        NumericUpDown17.Value = My.Settings.SK16
        NumericUpDown8.Value = My.Settings.SK7
        NumericUpDown18.Value = My.Settings.SK17
        NumericUpDown9.Value = My.Settings.SK8
        NumericUpDown19.Value = My.Settings.SK18
        NumericUpDown10.Value = My.Settings.SK9
        NumericUpDown20.Value = My.Settings.SK19
        NumericUpDown11.Value = My.Settings.SK10
        NumericUpDown21.Value = My.Settings.SK20

        NumericUpDown22.Value = My.Settings.MM1
        NumericUpDown23.Value = My.Settings.MM2
        NumericUpDown24.Value = My.Settings.MM3
        NumericUpDown25.Value = My.Settings.HO1
        NumericUpDown26.Value = My.Settings.HO2
        NumericUpDown27.Value = My.Settings.HO3



        Label14.Text = "秒 (" & ShuHan(My.Settings.SKM1A) & KeyHan(My.Settings.SKM1) & ")"
        If Strings.Left(Label14.Text, 3) = "( +" Then
            Label14.Text = Label14.Text.Replace(" + ", "")
        End If
        Label16.Text = "秒 (" & ShuHan(My.Settings.SKM2A) & KeyHan(My.Settings.SKM2) & ")"
        If Strings.Left(Label16.Text, 3) = "( +" Then
            Label16.Text = Label16.Text.Replace(" + ", "")
        End If
        Label18.Text = "秒 (" & ShuHan(My.Settings.SKM3A) & KeyHan(My.Settings.SKM3) & ")"
        If Strings.Left(Label18.Text, 3) = "( +" Then
            Label18.Text = Label18.Text.Replace(" + ", "")
        End If
        Label15.Text = "秒 (" & ShuHan(My.Settings.SKH1A) & KeyHan(My.Settings.SKH1) & ")"
        If Strings.Left(Label15.Text, 3) = "( +" Then
            Label15.Text = Label15.Text.Replace(" + ", "")
        End If
        Label17.Text = "秒 (" & ShuHan(My.Settings.SKH2A) & KeyHan(My.Settings.SKH2) & ")"
        If Strings.Left(Label17.Text, 3) = "( +" Then
            Label17.Text = Label17.Text.Replace(" + ", "")
        End If
        Label19.Text = "秒 (" & ShuHan(My.Settings.SKH3A) & KeyHan(My.Settings.SKH3) & ")"
        If Strings.Left(Label19.Text, 3) = "( +" Then
            Label19.Text = Label19.Text.Replace(" + ", "")
        End If

        NumericUpDown28.Value = My.Settings.SC1 / 100
        NumericUpDown29.Value = My.Settings.SC2 / 100
        NumericUpDown30.Value = My.Settings.SC3 / 100
        NumericUpDown31.Value = My.Settings.SC4 / 100
        NumericUpDown32.Value = My.Settings.SC5 / 100
        NumericUpDown33.Value = My.Settings.SC6 / 100
        NumericUpDown34.Value = My.Settings.SC7 / 100

        Label25.Text = "(" & ShuHan(My.Settings.SC1A) & KeyHan(My.Settings.SKSC1) & ")"
        If Strings.Left(Label25.Text, 3) = "( +" Then
            Label25.Text = Label25.Text.Replace(" + ", "")
        End If
        Label26.Text = "(" & ShuHan(My.Settings.SC2A) & KeyHan(My.Settings.SKSC2) & ")"
        If Strings.Left(Label26.Text, 3) = "( +" Then
            Label26.Text = Label26.Text.Replace(" + ", "")
        End If
        Label28.Text = "(" & ShuHan(My.Settings.SC3A) & KeyHan(My.Settings.SKSC3) & ")"
        If Strings.Left(Label28.Text, 3) = "( +" Then
            Label28.Text = Label28.Text.Replace(" + ", "")
        End If
        Label30.Text = "(" & ShuHan(My.Settings.SC4A) & KeyHan(My.Settings.SKSC4) & ")"
        If Strings.Left(Label30.Text, 3) = "( +" Then
            Label30.Text = Label30.Text.Replace(" + ", "")
        End If
        Label32.Text = "(" & ShuHan(My.Settings.SC5A) & KeyHan(My.Settings.SKSC5) & ")"
        If Strings.Left(Label32.Text, 3) = "( +" Then
            Label32.Text = Label32.Text.Replace(" + ", "")
        End If
        Label34.Text = "(" & ShuHan(My.Settings.SC6A) & KeyHan(My.Settings.SKSC6) & ")"
        If Strings.Left(Label34.Text, 3) = "( +" Then
            Label34.Text = Label34.Text.Replace(" + ", "")
        End If
        Label36.Text = "(" & ShuHan(My.Settings.SC7A) & KeyHan(My.Settings.SKSC7) & ")"
        If Strings.Left(Label36.Text, 3) = "( +" Then
            Label36.Text = Label36.Text.Replace(" + ", "")
        End If



    End Sub

    Private Sub Form2_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing


        If RadioButton1.Checked = True Then
            My.Settings.TimeCode = 0
        End If
        If RadioButton2.Checked = True Then
            My.Settings.TimeCode = 1
        End If
        If RadioButton3.Checked = True Then
            My.Settings.TimeCode = 2
        End If


        My.Settings.AutoBack = NumericUpDown1.Value * 10
        Form1.AutoB = My.Settings.AutoBack
        My.Settings.Atama = TextBox1.Text
        My.Settings.Oshiri = TextBox2.Text
        My.Settings.Atama2 = TextBox4.Text
        My.Settings.Oshiri2 = TextBox3.Text
        My.Settings.Atama3 = TextBox6.Text
        My.Settings.Oshiri3 = TextBox5.Text

        My.Settings.SK1 = NumericUpDown2.Value
        My.Settings.SK11 = NumericUpDown12.Value
        My.Settings.SK2 = NumericUpDown3.Value
        My.Settings.SK12 = NumericUpDown13.Value
        My.Settings.SK3 = NumericUpDown4.Value
        My.Settings.SK13 = NumericUpDown14.Value
        My.Settings.SK4 = NumericUpDown5.Value
        My.Settings.SK14 = NumericUpDown15.Value
        My.Settings.SK5 = NumericUpDown6.Value
        My.Settings.SK15 = NumericUpDown16.Value
        My.Settings.SK6 = NumericUpDown7.Value
        My.Settings.SK16 = NumericUpDown17.Value
        My.Settings.SK7 = NumericUpDown8.Value
        My.Settings.SK17 = NumericUpDown18.Value
        My.Settings.SK8 = NumericUpDown9.Value
        My.Settings.SK18 = NumericUpDown19.Value
        My.Settings.SK9 = NumericUpDown10.Value
        My.Settings.SK19 = NumericUpDown20.Value
        My.Settings.SK10 = NumericUpDown11.Value
        My.Settings.SK20 = NumericUpDown21.Value

        My.Settings.MM1 = NumericUpDown22.Value
        My.Settings.MM2 = NumericUpDown23.Value
        My.Settings.MM3 = NumericUpDown24.Value
        My.Settings.HO1 = NumericUpDown25.Value
        My.Settings.HO2 = NumericUpDown26.Value
        My.Settings.HO3 = NumericUpDown27.Value

        My.Settings.SC1 = NumericUpDown28.Value * 100
        My.Settings.SC2 = NumericUpDown29.Value * 100
        My.Settings.SC3 = NumericUpDown30.Value * 100
        My.Settings.SC4 = NumericUpDown31.Value * 100
        My.Settings.SC5 = NumericUpDown32.Value * 100
        My.Settings.SC6 = NumericUpDown33.Value * 100
        My.Settings.SC7 = NumericUpDown34.Value * 100

        My.Settings.Fuka = TextBox7.Text
        My.Settings.Fumei = TextBox8.Text
        My.Settings.Sonota = TextBox9.Text

    End Sub

    Private Sub NumericUpDown22_ValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        My.Settings.SKAA = -1
        My.Settings.SKA = -1
        My.Settings.SKDA = -1
        My.Settings.SKD = -1
        My.Settings.SKEA = -1
        My.Settings.SKE = -1
        My.Settings.SKCA = -1
        My.Settings.SKC = -1
        My.Settings.SKBA = -1
        My.Settings.SKB = -1
        My.Settings.SKFA = -1
        My.Settings.SKF = -1

        My.Settings.SKG = -1
        My.Settings.SKGA = -1
        My.Settings.SKH = -1
        My.Settings.SKHA = -1
        My.Settings.SKI = -1
        My.Settings.SKIA = -1
        My.Settings.SKJ = -1
        My.Settings.SKJA = -1
        My.Settings.SKK = -1
        My.Settings.SKKA = -1
        My.Settings.SKLA = -1
        My.Settings.SKL = -1
        My.Settings.SKM = -1
        My.Settings.SKMA = -1
        My.Settings.SKN = -1
        My.Settings.SKNA = -1
        My.Settings.SKO = -1
        My.Settings.SKOA = -1
        My.Settings.SKP = -1
        My.Settings.SKPA = -1
        My.Settings.SKQ = -1
        My.Settings.SKQA = -1
        My.Settings.SKR = -1
        My.Settings.SKRA = -1
        My.Settings.SKS = -1
        My.Settings.SKSA = -1
        My.Settings.SKT = -1
        My.Settings.SKTA = -1
        My.Settings.SKU = -1
        My.Settings.SKUA = -1
        My.Settings.SKV = -1
        My.Settings.SKVA = -1
        My.Settings.SKW = -1
        My.Settings.SKWA = -1
        My.Settings.SKX = -1
        My.Settings.SKXA = -1
        My.Settings.SKY = -1
        My.Settings.SKYA = -1
        My.Settings.SKZ = -1
        My.Settings.SKZA = -1

        My.Settings.SKM1A = -1
        My.Settings.SKM1 = -1
        My.Settings.SKM2A = -1
        My.Settings.SKM2 = -1
        My.Settings.SKM3A = -1
        My.Settings.SKM3 = -1
        My.Settings.SKH1A = -1
        My.Settings.SKH1 = -1
        My.Settings.SKH2A = -1
        My.Settings.SKH2 = -1
        My.Settings.SKH3A = -1
        My.Settings.SKH3 = -1

        My.Settings.SC1A = -1
        My.Settings.SKSC1 = -1
        My.Settings.SC2A = -1
        My.Settings.SKSC2 = -1
        My.Settings.SC3A = -1
        My.Settings.SKSC3 = -1
        My.Settings.SC4A = -1
        My.Settings.SKSC4 = -1
        My.Settings.SC5A = -1
        My.Settings.SKSC5 = -1
        My.Settings.SC6A = -1
        My.Settings.SKSC6 = -1
        My.Settings.SC7A = -1
        My.Settings.SKSC7 = -1

        UnregisterHotKey(Form1.Handle, hotkeyID_A)
        UnregisterHotKey(Form1.Handle, hotkeyID_B)
        UnregisterHotKey(Form1.Handle, hotkeyID_C)
        UnregisterHotKey(Form1.Handle, hotkeyID_D)
        UnregisterHotKey(Form1.Handle, hotkeyID_E)
        UnregisterHotKey(Form1.Handle, hotkeyID_F)
        UnregisterHotKey(Form1.Handle, hotkeyID_G)
        UnregisterHotKey(Form1.Handle, hotkeyID_H)
        UnregisterHotKey(Form1.Handle, hotkeyID_I)
        UnregisterHotKey(Form1.Handle, hotkeyID_J)
        UnregisterHotKey(Form1.Handle, hotkeyID_K)
        UnregisterHotKey(Form1.Handle, hotkeyID_L)
        UnregisterHotKey(Form1.Handle, hotkeyID_M)
        UnregisterHotKey(Form1.Handle, hotkeyID_N)
        UnregisterHotKey(Form1.Handle, hotkeyID_O)
        UnregisterHotKey(Form1.Handle, hotkeyID_P)
        UnregisterHotKey(Form1.Handle, hotkeyID_Q)
        UnregisterHotKey(Form1.Handle, hotkeyID_R)
        UnregisterHotKey(Form1.Handle, hotkeyID_S)
        UnregisterHotKey(Form1.Handle, hotkeyID_T)
        UnregisterHotKey(Form1.Handle, hotkeyID_U)
        UnregisterHotKey(Form1.Handle, hotkeyID_V)
        UnregisterHotKey(Form1.Handle, hotkeyID_W)
        UnregisterHotKey(Form1.Handle, hotkeyID_X)
        UnregisterHotKey(Form1.Handle, hotkeyID_Y)
        UnregisterHotKey(Form1.Handle, hotkeyID_Z)
        UnregisterHotKey(Form1.Handle, hotkeyID_MM1)
        UnregisterHotKey(Form1.Handle, hotkeyID_MM2)
        UnregisterHotKey(Form1.Handle, hotkeyID_MM3)
        UnregisterHotKey(Form1.Handle, hotkeyID_HO1)
        UnregisterHotKey(Form1.Handle, hotkeyID_HO2)
        UnregisterHotKey(Form1.Handle, hotkeyID_HO3)

        UnregisterHotKey(Form1.Handle, hotkeyID_SC1)
        UnregisterHotKey(Form1.Handle, hotkeyID_SC2)
        UnregisterHotKey(Form1.Handle, hotkeyID_SC3)
        UnregisterHotKey(Form1.Handle, hotkeyID_SC4)
        UnregisterHotKey(Form1.Handle, hotkeyID_SC5)
        UnregisterHotKey(Form1.Handle, hotkeyID_SC6)
        UnregisterHotKey(Form1.Handle, hotkeyID_SC7)


    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            My.Settings.TimeCode = 0
        End If
        If RadioButton2.Checked = True Then
            My.Settings.TimeCode = 1
        End If
        If RadioButton3.Checked = True Then
            My.Settings.TimeCode = 2
        End If
    End Sub

    Private Sub GroupBox4_Enter(sender As Object, e As EventArgs) Handles GroupBox4.Enter

    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton1.Checked = True Then
            My.Settings.TimeCode = 0
        End If
        If RadioButton2.Checked = True Then
            My.Settings.TimeCode = 1
        End If
        If RadioButton3.Checked = True Then
            My.Settings.TimeCode = 2
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton1.Checked = True Then
            My.Settings.TimeCode = 0
        End If
        If RadioButton2.Checked = True Then
            My.Settings.TimeCode = 1
        End If
        If RadioButton3.Checked = True Then
            My.Settings.TimeCode = 2
        End If
    End Sub

    Private Sub GroupBox7_Enter(sender As Object, e As EventArgs) Handles GroupBox7.Enter

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) 

        Dim Handan2 As Short = 0
        Dim KeyBangou As Integer
        Dim HotKeyAA As String

        Dim KeyShushoku As Integer
        Dim Zenhan As String = ""
        Dim Kouhan As String = ""

        Dim CS1 As Integer

        If ComboBox2.Text = "" Then
            MsgBox("Please enter shortcut key.", vbOKOnly, "Error")
            Exit Sub
        End If




        '3:Ctrl 4:Alt 5:Shift 7:Ctrl+Alt 8:Ctrl+Shift 9:Alt+Shift 12:Ctrl+Alt+Shift
        If CheckBox2.Checked = True Then
            Handan2 += 3
        End If
        If CheckBox3.Checked = True Then
            Handan2 += 4
        End If
        If CheckBox4.Checked = True Then
            Handan2 += 5
        End If





        Select Case ComboBox2.SelectedIndex
            Case 0
                KeyBangou = 112
            Case 1
                KeyBangou = 113
            Case 2
                KeyBangou = 114
            Case 3
                KeyBangou = 115
            Case 4
                KeyBangou = 116
            Case 5
                KeyBangou = 117
            Case 6
                KeyBangou = 118
            Case 7
                KeyBangou = 119
            Case 8
                KeyBangou = 120
            Case 9
                KeyBangou = 121
            Case 10
                KeyBangou = 122
            Case 11
                KeyBangou = 123
            Case 12
                KeyBangou = 124
            Case 13
                KeyBangou = 125
            Case 14
                KeyBangou = 126
            Case 15
                KeyBangou = 127
            Case 16
                KeyBangou = 38
            Case 17
                KeyBangou = 40
            Case 18
                KeyBangou = 37
            Case 19
                KeyBangou = 39
            '0(10key)
            Case 56
                KeyBangou = 96
            '1(10key)
            Case 57
                KeyBangou = 97
            '2(10key)
            Case 58
                KeyBangou = 98
            '3(10key)
            Case 59
                KeyBangou = 99
            '4(10key)
            Case 60
                KeyBangou = 100
            '5(10key)
            Case 61
                KeyBangou = 101
            '6(10key)
            Case 62
                KeyBangou = 102
            '7(10key)
            Case 63
                KeyBangou = 103
            '8(10key)
            Case 64
                KeyBangou = 104
            '9(10key)
            Case 65
                KeyBangou = 105

            'Next(Media)
            Case 66
                KeyBangou = 176
            'PlayPause(Media)
            Case 67
                KeyBangou = 179
            'Previous(Media)
            Case 68
                KeyBangou = 177
            'Stop(Media)
            Case 69
                KeyBangou = 178


            Case Else
                KeyBangou = Asc(ComboBox2.Text)
        End Select




        MsgBox(ComboBox1.SelectedIndex)
        CS1 = ComboBox1.SelectedIndex


        Select Case CS1
            'Play / Pause
            Case 0
                HotKeyAA = hotkeyID_A
            'Play / Pause & Copying counter1
            Case 1
                HotKeyAA = hotkeyID_B
            'Play / Pause & Copying counter2
            Case 2
                HotKeyAA = hotkeyID_C
            'Stop
            Case 4
                HotKeyAA = hotkeyID_D
            'Play / Pause & Copying counter3
            Case 3
                HotKeyAA = hotkeyID_E
            'Copying counter1
            Case 5
                HotKeyAA = hotkeyID_T
            'Copying counter2
            Case 6
                HotKeyAA = hotkeyID_F
            'Copying counter3
            Case 7
                HotKeyAA = hotkeyID_G
            'Bookmark
            Case 8
                HotKeyAA = hotkeyID_H
            'Play & Pause & Bookmark
            Case 9
                HotKeyAA = hotkeyID_I
            'Jump1
            Case 10
                HotKeyAA = hotkeyID_MM1
            'Jump2
            Case 11
                HotKeyAA = hotkeyID_MM2
            'Jump3
            Case 12
                HotKeyAA = hotkeyID_MM3
            'Jump4
            Case 13
                HotKeyAA = hotkeyID_HO1
            'Jump5
            Case 14
                HotKeyAA = hotkeyID_HO2
            'Jump6
            Case 15
                HotKeyAA = hotkeyID_HO3
            'Speed +0.1
            Case 16
                HotKeyAA = hotkeyID_J
            'Speed -0.1
            Case 17
                HotKeyAA = hotkeyID_K
            'Speed x1
            Case 18
                HotKeyAA = hotkeyID_L
            'Speed x0.5
            Case 19
                HotKeyAA = hotkeyID_M
            'Speed x2.0
            Case 20
                HotKeyAA = hotkeyID_N
            'mosttop
            Case 21
                HotKeyAA = hotkeyID_O
            'Speed Control Button1
            Case 22
                HotKeyAA = hotkeyID_SC1
            'Speed Control Button2
            Case 23
                HotKeyAA = hotkeyID_SC2
            'Speed Control Button3
            Case 24
                HotKeyAA = hotkeyID_SC3
            'Speed Control Button4
            Case 25
                HotKeyAA = hotkeyID_SC4
            'Speed Control Button5
            Case 26
                HotKeyAA = hotkeyID_SC5
            'Speed Control Button6
            Case 27
                HotKeyAA = hotkeyID_SC6
            'Speed Control Button7
            Case 28
                HotKeyAA = hotkeyID_SC7
            Case 29
                HotKeyAA = hotkeyID_P

            Case Else
                MsgBox("項目をドロップダウンボックスから選択してください", vbOKOnly)
                'Exit Sub
        End Select




        '対象のホットキーを解除
        UnregisterHotKey(Form1.Handle, HotKeyAA)
        'GlobalDeleteAtom(HotKeyAA)






        'キーの登録
        Select Case Handan2

            'none
            Case 0
                If HotKeyAA = "" Then
                    RegisterHotKey(Form1.Handle, 0, 0, KeyBangou)
                Else
                    RegisterHotKey(Form1.Handle, 0, HotKeyAA, KeyBangou)
                    MsgBox("設定完了", vbOKOnly)

                End If
            'Ctrl
            Case 3
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_CONTROL, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            'Alt
            Case 4
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_ALT, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            'Shift
            Case 5
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_SHIFT, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_CONTROL Or MOD_ALT, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_CONTROL Or MOD_SHIFT, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            'Alt + Shift
            Case 9
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_ALT Or MOD_SHIFT, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, KeyBangou)
                MsgBox("設定完了", vbOKOnly)


        End Select


        'ホットキーを設定ファイルに記憶
        Select Case HotKeyAA
            Case hotkeyID_A
                My.Settings.SKAA = Handan2
                My.Settings.SKA = KeyBangou
            Case hotkeyID_B
                My.Settings.SKBA = Handan2
                My.Settings.SKB = KeyBangou
            Case hotkeyID_C
                My.Settings.SKCA = Handan2
                My.Settings.SKC = KeyBangou
            Case hotkeyID_D
                My.Settings.SKDA = Handan2
                My.Settings.SKD = KeyBangou
            Case hotkeyID_E
                My.Settings.SKEA = Handan2
                My.Settings.SKE = KeyBangou
            Case hotkeyID_F
                My.Settings.SKFA = Handan2
                My.Settings.SKF = KeyBangou
            Case hotkeyID_G
                My.Settings.SKGA = Handan2
                My.Settings.SKG = KeyBangou
            Case hotkeyID_H
                My.Settings.SKHA = Handan2
                My.Settings.SKH = KeyBangou
            Case hotkeyID_I
                My.Settings.SKIA = Handan2
                My.Settings.SKI = KeyBangou
            Case hotkeyID_J
                My.Settings.SKJA = Handan2
                My.Settings.SKJ = KeyBangou
            Case hotkeyID_K
                My.Settings.SKKA = Handan2
                My.Settings.SKK = KeyBangou
            Case hotkeyID_L
                My.Settings.SKLA = Handan2
                My.Settings.SKL = KeyBangou
            Case hotkeyID_M
                My.Settings.SKMA = Handan2
                My.Settings.SKM = KeyBangou
            Case hotkeyID_N
                My.Settings.SKNA = Handan2
                My.Settings.SKN = KeyBangou
            Case hotkeyID_O
                My.Settings.SKOA = Handan2
                My.Settings.SKO = KeyBangou
            Case hotkeyID_P
                My.Settings.SKPA = Handan2
                My.Settings.SKP = KeyBangou
            Case hotkeyID_Q
                My.Settings.SKQA = Handan2
                My.Settings.SKQ = KeyBangou
            Case hotkeyID_R
                My.Settings.SKRA = Handan2
                My.Settings.SKR = KeyBangou
            Case hotkeyID_S
                My.Settings.SKSA = Handan2
                My.Settings.SKS = KeyBangou
            Case hotkeyID_T
                My.Settings.SKTA = Handan2
                My.Settings.SKT = KeyBangou
            Case hotkeyID_U
                My.Settings.SKUA = Handan2
                My.Settings.SKU = KeyBangou
            Case hotkeyID_V
                My.Settings.SKVA = Handan2
                My.Settings.SKV = KeyBangou
            Case hotkeyID_W
                My.Settings.SKWA = Handan2
                My.Settings.SKW = KeyBangou
            Case hotkeyID_X
                My.Settings.SKXA = Handan2
                My.Settings.SKX = KeyBangou
            Case hotkeyID_Y
                My.Settings.SKYA = Handan2
                My.Settings.SKY = KeyBangou
            Case hotkeyID_Z
                My.Settings.SKZA = Handan2
                My.Settings.SKZ = KeyBangou

            Case hotkeyID_MM1
                My.Settings.SKM1A = Handan2
                My.Settings.SKM1 = KeyBangou
            Case hotkeyID_MM2
                My.Settings.SKM2A = Handan2
                My.Settings.SKM2 = KeyBangou
            Case hotkeyID_MM3
                My.Settings.SKM3A = Handan2
                My.Settings.SKM3 = KeyBangou
            Case hotkeyID_HO1
                My.Settings.SKH1A = Handan2
                My.Settings.SKH1 = KeyBangou
            Case hotkeyID_HO2
                My.Settings.SKH2A = Handan2
                My.Settings.SKH2 = KeyBangou
            Case hotkeyID_HO3
                My.Settings.SKH3A = Handan2
                My.Settings.SKH3 = KeyBangou

            Case hotkeyID_SC1
                My.Settings.SC1A = Handan2
                My.Settings.SKSC1 = KeyBangou
            Case hotkeyID_SC2
                My.Settings.SC2A = Handan2
                My.Settings.SKSC2 = KeyBangou
            Case hotkeyID_SC3
                My.Settings.SC3A = Handan2
                My.Settings.SKSC3 = KeyBangou
            Case hotkeyID_SC4
                My.Settings.SC4A = Handan2
                My.Settings.SKSC4 = KeyBangou
            Case hotkeyID_SC5
                My.Settings.SC5A = Handan2
                My.Settings.SKSC5 = KeyBangou
            Case hotkeyID_SC6
                My.Settings.SC6A = Handan2
                My.Settings.SKSC6 = KeyBangou
            Case hotkeyID_SC7
                My.Settings.SC7A = Handan2
                My.Settings.SKSC7 = KeyBangou

        End Select


        My.Settings.Save()


        Select Case ComboBox1.SelectedIndex
            'Play / Pause
            Case 0
                KeyShushoku = My.Settings.SKAA
                KeyBangou = My.Settings.SKA
            'Play / Pause & Copying counter1
            Case 1
                KeyShushoku = My.Settings.SKBA
                KeyBangou = My.Settings.SKB
            'Play / Pause & Copying counter2
            Case 2
                KeyShushoku = My.Settings.SKCA
                KeyBangou = My.Settings.SKC
            'Stop
            Case 4
                KeyShushoku = My.Settings.SKDA
                KeyBangou = My.Settings.SKD
            'Play & Pause & Copying counter3
            Case 3
                KeyShushoku = My.Settings.SKEA
                KeyBangou = My.Settings.SKE
            'Copying Counter1
            Case 5
                KeyShushoku = My.Settings.SKTA
                KeyBangou = My.Settings.SKT
            'Copying Counter2
            Case 6
                KeyShushoku = My.Settings.SKFA
                KeyBangou = My.Settings.SKF
            'Copying Counter3
            Case 7
                KeyShushoku = My.Settings.SKGA
                KeyBangou = My.Settings.SKG
            'Bookmark
            Case 8
                KeyShushoku = My.Settings.SKHA
                KeyBangou = My.Settings.SKH
            'Play / Pause & Bookmark
            Case 9
                KeyShushoku = My.Settings.SKIA
                KeyBangou = My.Settings.SKI

            'Jump1
            Case 10
                KeyShushoku = My.Settings.SKM1A
                KeyBangou = My.Settings.SKM1
            'Jump2
            Case 11
                KeyShushoku = My.Settings.SKM2A
                KeyBangou = My.Settings.SKM2
            'Jump3
            Case 12
                KeyShushoku = My.Settings.SKM3A
                KeyBangou = My.Settings.SKM3
            'Jump4
            Case 13
                KeyShushoku = My.Settings.SKH1A
                KeyBangou = My.Settings.SKH1
            'Jump5
            Case 14
                KeyShushoku = My.Settings.SKH2A
                KeyBangou = My.Settings.SKH2
            'Jump6
            Case 15
                KeyShushoku = My.Settings.SKH3A
                KeyBangou = My.Settings.SKH3

            'Speed up +0.1
            Case 16
                KeyShushoku = My.Settings.SKJA
                KeyBangou = My.Settings.SKJ
            'Speed down -0.1
            Case 17
                KeyShushoku = My.Settings.SKKA
                KeyBangou = My.Settings.SKK
            'Speed x1.0
            Case 18
                KeyShushoku = My.Settings.SKLA
                KeyBangou = My.Settings.SKL
            'Speed x0.5
            Case 19
                KeyShushoku = My.Settings.SKMA
                KeyBangou = My.Settings.SKM
            'Speed x2.20
            Case 20
                KeyShushoku = My.Settings.SKNA
                KeyBangou = My.Settings.SKN
            'Mosttop
            Case 21
                KeyShushoku = My.Settings.SKOA
                KeyBangou = My.Settings.SKO

            'Speed control button1
            Case 22
                KeyShushoku = My.Settings.SC1A
                KeyBangou = My.Settings.SKSC1
            'Speed control button2
            Case 23
                KeyShushoku = My.Settings.SC2A
                KeyBangou = My.Settings.SKSC2
            'Speed control button3
            Case 24
                KeyShushoku = My.Settings.SC3A
                KeyBangou = My.Settings.SKSC3
            'Speed control button4
            Case 25
                KeyShushoku = My.Settings.SC4A
                KeyBangou = My.Settings.SKSC4
            'Speed control button5
            Case 26
                KeyShushoku = My.Settings.SC5A
                KeyBangou = My.Settings.SKSC5
            'Speed control button6
            Case 27
                KeyShushoku = My.Settings.SC6A
                KeyBangou = My.Settings.SKSC6
            'Speed control button7
            Case 28
                KeyShushoku = My.Settings.SC7A
                KeyBangou = My.Settings.SKSC7

            Case 29
                KeyShushoku = My.Settings.SKPA
                KeyBangou = My.Settings.SKP



        End Select

        Zenhan = ShuHan(KeyShushoku)


        'GoTo 10 ←なんだっけ？

        '3:Ctrl 4:Alt 5:Shift 7:Ctrl+Alt 8:Ctrl+Shift 9:Alt+Shift 12:Ctrl+Alt+Shift
        Select Case KeyShushoku
            'none
            Case 0
                Zenhan = ""
            'Ctrl
            Case 3
                Zenhan = "Ctrl"
            'Alt
            Case 4
                Zenhan = "Alt"

            'Shift
            Case 5
                Zenhan = "Shift"
            'Ctrl + Alt
            Case 7
                Zenhan = "Ctrl + Alt"
            'Ctrl + Shift
            Case 8
                Zenhan = "Ctrl + Shift"
            'Alt + Shift
            Case 9
                Zenhan = "Alt + Shift"
            'Ctrl + Alt + Shift
            Case 12
                Zenhan = "Ctrl + Alt + Shift"
        End Select

10:

        Select Case KeyBangou
            Case 112
                Kouhan = " + F1"
            Case 113
                Kouhan = " + F2"
            Case 114
                Kouhan = " + F3"
            Case 115
                Kouhan = " + F4"
            Case 116
                Kouhan = " + F5"
            Case 117
                Kouhan = " + F6"
            Case 118
                Kouhan = " + F7"
            Case 119
                Kouhan = " + F8"
            Case 120
                Kouhan = " + F9"
            Case 121
                Kouhan = " + F10"
            Case 122
                Kouhan = " + F11"
            Case 123
                Kouhan = " + F12"
            Case 124
                Kouhan = " + F13"
            Case 125
                Kouhan = " + F14"
            Case 126
                Kouhan = " + F15"
            Case 127
                Kouhan = " + F16"
            Case 38
                Kouhan = " + ↑"
            Case 40
                Kouhan = " + ↓"
            Case 37
                Kouhan = " + ←"
            Case 39
                Kouhan = " + →"
            Case 65
                Kouhan = " + A"
            Case 66
                Kouhan = " + B"
            Case 67
                Kouhan = " + C"
            Case 68
                Kouhan = " + D"
            Case 69
                Kouhan = " + E"
            Case 70
                Kouhan = " + F"
            Case 71
                Kouhan = " + G"
            Case 72
                Kouhan = " + H"
            Case 73
                Kouhan = " + I"
            Case 74
                Kouhan = " + J"
            Case 75
                Kouhan = " + K"
            Case 76
                Kouhan = " + L"
            Case 77
                Kouhan = " + M"
            Case 78
                Kouhan = " + N"
            Case 79
                Kouhan = " + O"
            Case 80
                Kouhan = " + P"
            Case 81
                Kouhan = " + Q"
            Case 82
                Kouhan = " + R"
            Case 83
                Kouhan = " + S"
            Case 84
                Kouhan = " + T"
            Case 85
                Kouhan = " + U"
            Case 86
                Kouhan = " + V"
            Case 87
                Kouhan = " + W"
            Case 88
                Kouhan = " + X"
            Case 89
                Kouhan = " + Y"
            Case 90
                Kouhan = " + Z"
            Case 48
                Kouhan = " + 0"
            Case 49
                Kouhan = " + 1"
            Case 50
                Kouhan = " + 2"
            Case 51
                Kouhan = " + 3"
            Case 52
                Kouhan = " + 4"
            Case 53
                Kouhan = " + 5"
            Case 54
                Kouhan = " + 6"
            Case 55
                Kouhan = " + 7"
            Case 56
                Kouhan = " + 8"
            Case 57
                Kouhan = " + 9"
            Case 58
                Kouhan = ""
            Case 96
                Kouhan = " + 0(テンキー）"
            Case 97
                Kouhan = " + 1(テンキー）"
            Case 98
                Kouhan = " + 2(テンキー）"
            Case 99
                Kouhan = " + 3(テンキー）"
            Case 100
                Kouhan = " + 4(テンキー）"
            Case 101
                Kouhan = " + 5(テンキー）"
            Case 102
                Kouhan = " + 6(テンキー）"
            Case 103
                Kouhan = " + 7(テンキー）"
            Case 104
                Kouhan = " + 8(テンキー）"
            Case 105
                Kouhan = " + 9(テンキー）"
            Case 176
                Kouhan = " + 次のトラックキー"
            Case 179
                Kouhan = " + 再生/一時停止キー"
            Case 177
                Kouhan = " + 前のトラックキー"
            Case 178
                Kouhan = " + 停止キー"


        End Select

        If Zenhan = "" Then
            Label3.Text = Kouhan.Replace(" + ", "")
        Else
            Label3.Text = Zenhan & Kouhan
        End If

        Label14.Text = "秒 (" & ShuHan(My.Settings.SKM1A) & KeyHan(My.Settings.SKM1) & ")"
        If Strings.Left(Label14.Text, 5) = "秒 ( + " Then
            Label14.Text = Label14.Text.Replace("秒 ( + ", "")
        End If
        Label16.Text = "秒 (" & ShuHan(My.Settings.SKM2A) & KeyHan(My.Settings.SKM2) & ")"
        If Strings.Left(Label16.Text, 5) = "秒 ( + " Then
            Label16.Text = Label16.Text.Replace("秒 ( + ", "")
        End If
        Label18.Text = "秒 (" & ShuHan(My.Settings.SKM3A) & KeyHan(My.Settings.SKM3) & ")"
        If Strings.Left(Label18.Text, 5) = "秒 ( + " Then
            Label18.Text = Label18.Text.Replace("秒 ( + ", "")
        End If
        Label15.Text = "秒 (" & ShuHan(My.Settings.SKH1A) & KeyHan(My.Settings.SKH1) & ")"
        If Strings.Left(Label15.Text, 5) = "秒 ( + " Then
            Label15.Text = Label15.Text.Replace("秒 ( + ", "")
        End If
        Label17.Text = "秒 (" & ShuHan(My.Settings.SKH2A) & KeyHan(My.Settings.SKH2) & ")"
        If Strings.Left(Label17.Text, 5) = "秒 ( + " Then
            Label17.Text = Label17.Text.Replace("秒 ( + ", "")
        End If
        Label19.Text = "秒 (" & ShuHan(My.Settings.SKH3A) & KeyHan(My.Settings.SKH3) & ")"
        If Strings.Left(Label19.Text, 5) = "秒 ( + " Then
            Label19.Text = Label19.Text.Replace("秒 ( + ", "")
        End If


        Label25.Text = "(" & ShuHan(My.Settings.SC1A) & KeyHan(My.Settings.SKSC1) & ")"
        If Strings.Left(Label25.Text, 5) = "秒 ( + " Then
            Label25.Text = Label25.Text.Replace("秒 ( + ", "")
        End If
        Label26.Text = "(" & ShuHan(My.Settings.SC2A) & KeyHan(My.Settings.SKSC2) & ")"
        If Strings.Left(Label26.Text, 5) = "秒 ( + " Then
            Label26.Text = Label26.Text.Replace("秒 ( + ", "")
        End If
        Label28.Text = "(" & ShuHan(My.Settings.SC3A) & KeyHan(My.Settings.SKSC3) & ")"
        If Strings.Left(Label28.Text, 5) = "秒 ( + " Then
            Label28.Text = Label28.Text.Replace("秒 ( + ", "")
        End If
        Label30.Text = "(" & ShuHan(My.Settings.SC4A) & KeyHan(My.Settings.SKSC4) & ")"
        If Strings.Left(Label30.Text, 5) = "秒 ( + " Then
            Label30.Text = Label30.Text.Replace("秒 ( + ", "")
        End If
        Label32.Text = "(" & ShuHan(My.Settings.SC5A) & KeyHan(My.Settings.SKSC5) & ")"
        If Strings.Left(Label32.Text, 5) = "秒 ( + " Then
            Label32.Text = Label32.Text.Replace("秒 ( + ", "")
        End If
        Label34.Text = "(" & ShuHan(My.Settings.SC6A) & KeyHan(My.Settings.SKSC6) & ")"
        If Strings.Left(Label34.Text, 5) = "秒 ( + " Then
            Label34.Text = Label34.Text.Replace("秒 ( + ", "")
        End If
        Label36.Text = "(" & ShuHan(My.Settings.SC7A) & KeyHan(My.Settings.SKSC7) & ")"
        If Strings.Left(Label36.Text, 5) = "秒 ( + " Then
            Label36.Text = Label36.Text.Replace("秒 ( + ", "")
        End If
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
        Dim Handan2 As Short = 0
        Dim KeyBangou As Integer = 0
        Dim HotKeyAA As String = ""

        Dim KeyShushoku As Integer
        Dim Zenhan As String = ""
        Dim Kouhan As String = ""

        Dim CS1 As Long = 0

        If ComboBox2.Text = "" Then
            MsgBox("Please enter shortcut key.", vbOKOnly, "Error")
            Exit Sub
        End If




        '3:Ctrl 4:Alt 5:Shift 7:Ctrl+Alt 8:Ctrl+Shift 9:Alt+Shift 12:Ctrl+Alt+Shift
        If CheckBox2.Checked Then
            Handan2 += 3
        End If
        If CheckBox3.Checked = True Then
            Handan2 += 4
        End If
        If CheckBox4.Checked = True Then
            Handan2 += 5
        End If

        CS1 = ComboBox1.SelectedIndex

        Select Case CS1
            'Play / Pause
            Case 0
                HotKeyAA = hotkeyID_A
            'Play / Pause & Copying counter1
            Case 1
                HotKeyAA = hotkeyID_B
            'Play / Pause & Copying counter2
            Case 2
                HotKeyAA = hotkeyID_C
            'Stop
            Case 4
                HotKeyAA = hotkeyID_D
            'Play / Pause & Copying counter3
            Case 3
                HotKeyAA = hotkeyID_E
            'Copying counter1
            Case 5
                HotKeyAA = hotkeyID_T
            'Copying counter2
            Case 6
                HotKeyAA = hotkeyID_F
            'Copying counter3
            Case 7
                HotKeyAA = hotkeyID_G
            'Bookmark
            Case 8
                HotKeyAA = hotkeyID_H
            'Play & Pause & Bookmark
            Case 9
                HotKeyAA = hotkeyID_I
            'Jump1
            Case 10
                HotKeyAA = hotkeyID_MM1
            'Jump2
            Case 11
                HotKeyAA = hotkeyID_MM2
            'Jump3
            Case 12
                HotKeyAA = hotkeyID_MM3
            'Jump4
            Case 13
                HotKeyAA = hotkeyID_HO1
            'Jump5
            Case 14
                HotKeyAA = hotkeyID_HO2
            'Jump6
            Case 15
                HotKeyAA = hotkeyID_HO3
            'Speed +0.1
            Case 16
                HotKeyAA = hotkeyID_J
            'Speed -0.1
            Case 17
                HotKeyAA = hotkeyID_K
            'Speed x1
            Case 18
                HotKeyAA = hotkeyID_L
            'Speed x0.5
            Case 19
                HotKeyAA = hotkeyID_M
            'Speed x2.0
            Case 20
                HotKeyAA = hotkeyID_N
            'mosttop
            Case 21
                HotKeyAA = hotkeyID_O
            'Speed Control Button1
            Case 22
                HotKeyAA = hotkeyID_SC1
            'Speed Control Button2
            Case 23
                HotKeyAA = hotkeyID_SC2
            'Speed Control Button3
            Case 24
                HotKeyAA = hotkeyID_SC3
            'Speed Control Button4
            Case 25
                HotKeyAA = hotkeyID_SC4
            'Speed Control Button5
            Case 26
                HotKeyAA = hotkeyID_SC5
            'Speed Control Button6
            Case 27
                HotKeyAA = hotkeyID_SC6
            'Speed Control Button7
            Case 28
                HotKeyAA = hotkeyID_SC7
            Case 29
                HotKeyAA = hotkeyID_P
            Case Else
                Exit Select

                'Case Else
                'MsgBox("項目をドロップダウンボックスから選択してください", vbOKOnly)
                'Exit Sub
        End Select

        UnregisterHotKey(Form1.Handle, HotKeyAA)



        'MsgBox(ComboBox2.SelectedIndex, vbOKOnly)
        Select Case ComboBox2.SelectedIndex
            Case 0
                KeyBangou = 112
            Case 1
                KeyBangou = 113
            Case 2
                KeyBangou = 114
            Case 3
                KeyBangou = 115
            Case 4
                KeyBangou = 116
            Case 5
                KeyBangou = 117
            Case 6
                KeyBangou = 118
            Case 7
                KeyBangou = 119
            Case 8
                KeyBangou = 120
            Case 9
                KeyBangou = 121
            Case 10
                KeyBangou = 122
            Case 11
                KeyBangou = 123
            Case 12
                KeyBangou = 124
            Case 13
                KeyBangou = 125
            Case 14
                KeyBangou = 126
            Case 15
                KeyBangou = 127
            Case 16
                KeyBangou = 38
            Case 17
                KeyBangou = 40
            Case 18
                KeyBangou = 37
            Case 19
                KeyBangou = 39
            Case 56
                KeyBangou = 96
            Case 57
                KeyBangou = 97
            Case 58
                KeyBangou = 98
            Case 59
                KeyBangou = 99
            Case 60
                KeyBangou = 100
            Case 61
                KeyBangou = 101
            Case 62
                KeyBangou = 102
            Case 63
                KeyBangou = 103
            Case 64
                KeyBangou = 104
            Case 65
                KeyBangou = 105
            Case 66
                KeyBangou = 176
            Case 67
                KeyBangou = 179
            Case 68
                KeyBangou = 177
            Case 69
                KeyBangou = 178
            Case Else
                KeyBangou = Asc(ComboBox2.Text)

        End Select

        MsgBox(KeyBangou, vbOKOnly)

        'キーの登録
        Select Case Handan2

            'none
            Case 0
                If HotKeyAA = "" Then
                    RegisterHotKey(Form1.Handle, 0, 0, KeyBangou)
                Else
                    RegisterHotKey(Form1.Handle, 0, HotKeyAA, KeyBangou)
                    MsgBox("設定完了", vbOKOnly)

                End If
            'Ctrl
            Case 3
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_CONTROL, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            'Alt
            Case 4
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_ALT, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            'Shift
            Case 5
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_SHIFT, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            'Ctrl + Alt
            Case 7
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_CONTROL Or MOD_ALT, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            'Ctrl + Shift
            Case 8
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_CONTROL Or MOD_SHIFT, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            'Alt + Shift
            Case 9
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_ALT Or MOD_SHIFT, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            'Ctrl + Alt + Shift
            Case 12
                RegisterHotKey(Form1.Handle, HotKeyAA, MOD_CONTROL Or MOD_SHIFT Or MOD_ALT, KeyBangou)
                MsgBox("設定完了", vbOKOnly)
            Case Else
                Exit Select


        End Select


        'ホットキーを設定ファイルに記憶
        Select Case HotKeyAA
            Case hotkeyID_A
                My.Settings.SKAA = Handan2
                My.Settings.SKA = KeyBangou
            Case hotkeyID_B
                My.Settings.SKBA = Handan2
                My.Settings.SKB = KeyBangou
            Case hotkeyID_C
                My.Settings.SKCA = Handan2
                My.Settings.SKC = KeyBangou
            Case hotkeyID_D
                My.Settings.SKDA = Handan2
                My.Settings.SKD = KeyBangou
            Case hotkeyID_E
                My.Settings.SKEA = Handan2
                My.Settings.SKE = KeyBangou
            Case hotkeyID_F
                My.Settings.SKFA = Handan2
                My.Settings.SKF = KeyBangou
            Case hotkeyID_G
                My.Settings.SKGA = Handan2
                My.Settings.SKG = KeyBangou
            Case hotkeyID_H
                My.Settings.SKHA = Handan2
                My.Settings.SKH = KeyBangou
            Case hotkeyID_I
                My.Settings.SKIA = Handan2
                My.Settings.SKI = KeyBangou
            Case hotkeyID_J
                My.Settings.SKJA = Handan2
                My.Settings.SKJ = KeyBangou
            Case hotkeyID_K
                My.Settings.SKKA = Handan2
                My.Settings.SKK = KeyBangou
            Case hotkeyID_L
                My.Settings.SKLA = Handan2
                My.Settings.SKL = KeyBangou
            Case hotkeyID_M
                My.Settings.SKMA = Handan2
                My.Settings.SKM = KeyBangou
            Case hotkeyID_N
                My.Settings.SKNA = Handan2
                My.Settings.SKN = KeyBangou
            Case hotkeyID_O
                My.Settings.SKOA = Handan2
                My.Settings.SKO = KeyBangou
            Case hotkeyID_P
                My.Settings.SKPA = Handan2
                My.Settings.SKP = KeyBangou
            Case hotkeyID_Q
                My.Settings.SKQA = Handan2
                My.Settings.SKQ = KeyBangou
            Case hotkeyID_R
                My.Settings.SKRA = Handan2
                My.Settings.SKR = KeyBangou
            Case hotkeyID_S
                My.Settings.SKSA = Handan2
                My.Settings.SKS = KeyBangou
            Case hotkeyID_T
                My.Settings.SKTA = Handan2
                My.Settings.SKT = KeyBangou
            Case hotkeyID_U
                My.Settings.SKUA = Handan2
                My.Settings.SKU = KeyBangou
            Case hotkeyID_V
                My.Settings.SKVA = Handan2
                My.Settings.SKV = KeyBangou
            Case hotkeyID_W
                My.Settings.SKWA = Handan2
                My.Settings.SKW = KeyBangou
            Case hotkeyID_X
                My.Settings.SKXA = Handan2
                My.Settings.SKX = KeyBangou
            Case hotkeyID_Y
                My.Settings.SKYA = Handan2
                My.Settings.SKY = KeyBangou
            Case hotkeyID_Z
                My.Settings.SKZA = Handan2
                My.Settings.SKZ = KeyBangou

            Case hotkeyID_MM1
                My.Settings.SKM1A = Handan2
                My.Settings.SKM1 = KeyBangou
            Case hotkeyID_MM2
                My.Settings.SKM2A = Handan2
                My.Settings.SKM2 = KeyBangou
            Case hotkeyID_MM3
                My.Settings.SKM3A = Handan2
                My.Settings.SKM3 = KeyBangou
            Case hotkeyID_HO1
                My.Settings.SKH1A = Handan2
                My.Settings.SKH1 = KeyBangou
            Case hotkeyID_HO2
                My.Settings.SKH2A = Handan2
                My.Settings.SKH2 = KeyBangou
            Case hotkeyID_HO3
                My.Settings.SKH3A = Handan2
                My.Settings.SKH3 = KeyBangou

            Case hotkeyID_SC1
                My.Settings.SC1A = Handan2
                My.Settings.SKSC1 = KeyBangou
            Case hotkeyID_SC2
                My.Settings.SC2A = Handan2
                My.Settings.SKSC2 = KeyBangou
            Case hotkeyID_SC3
                My.Settings.SC3A = Handan2
                My.Settings.SKSC3 = KeyBangou
            Case hotkeyID_SC4
                My.Settings.SC4A = Handan2
                My.Settings.SKSC4 = KeyBangou
            Case hotkeyID_SC5
                My.Settings.SC5A = Handan2
                My.Settings.SKSC5 = KeyBangou
            Case hotkeyID_SC6
                My.Settings.SC6A = Handan2
                My.Settings.SKSC6 = KeyBangou
            Case hotkeyID_SC7
                My.Settings.SC7A = Handan2
                My.Settings.SKSC7 = KeyBangou
            Case Else
                Exit Select

        End Select

















    End Sub
End Class