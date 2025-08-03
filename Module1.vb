Module Module1

    Public Sub SetCurrentEffectPreset(ByVal value As Integer)

    End Sub

    Public Function ShuHan(ByVal KeyShushoku As Integer) As String
        Select Case KeyShushoku

            'none
            Case 0
                ShuHan = ""
            'Ctrl
            Case 3
                ShuHan = "Ctrl"
            'Alt
            Case 4
                ShuHan = "Alt"

            'Shift
            Case 5
                ShuHan = "Shift"
            'Ctrl + Alt
            Case 7
                ShuHan = "Ctrl + Alt"
            'Ctrl + Shift
            Case 8
                ShuHan = "Ctrl + Shift"
            'Alt + Shift
            Case 9
                ShuHan = "Alt + Shift"
            'Ctrl + Alt + Shift
            Case 12
                ShuHan = "Ctrl + Alt + Shift"


        End Select
    End Function

    Public Function KeyHan(ByVal KeyBangou As Integer) As String
        Select Case KeyBangou
            Case 112
                KeyHan = " + F1"
            Case 113
                KeyHan = " + F2"
            Case 114
                KeyHan = " + F3"
            Case 115
                KeyHan = " + F4"
            Case 116
                KeyHan = " + F5"
            Case 117
                KeyHan = " + F6"
            Case 118
                KeyHan = " + F7"
            Case 119
                KeyHan = " + F8"
            Case 120
                KeyHan = " + F9"
            Case 121
                KeyHan = " + F10"
            Case 122
                KeyHan = " + F11"
            Case 123
                KeyHan = " + F12"
            Case 124
                KeyHan = " + F13"
            Case 125
                KeyHan = " + F14"
            Case 126
                KeyHan = " + F15"
            Case 127
                KeyHan = " + F16"
            Case 38
                KeyHan = " + ↑"
            Case 40
                KeyHan = " + ↓"
            Case 37
                KeyHan = " + ←"
            Case 39
                KeyHan = " + →"
            Case 65
                KeyHan = " + A"
            Case 66
                KeyHan = " + B"
            Case 67
                KeyHan = " + C"
            Case 68
                KeyHan = " + D"
            Case 69
                KeyHan = " + E"
            Case 70
                KeyHan = " + F"
            Case 71
                KeyHan = " + G"
            Case 72
                KeyHan = " + H"
            Case 73
                KeyHan = " + I"
            Case 74
                KeyHan = " + J"
            Case 75
                KeyHan = " + K"
            Case 76
                KeyHan = " + L"
            Case 77
                KeyHan = " + M"
            Case 78
                KeyHan = " + N"
            Case 79
                KeyHan = " + O"
            Case 80
                KeyHan = " + P"
            Case 81
                KeyHan = " + Q"
            Case 82
                KeyHan = " + R"
            Case 83
                KeyHan = " + S"
            Case 84
                KeyHan = " + T"
            Case 85
                KeyHan = " + U"
            Case 86
                KeyHan = " + V"
            Case 87
                KeyHan = " + W"
            Case 88
                KeyHan = " + X"
            Case 89
                KeyHan = " + Y"
            Case 90
                KeyHan = " + Z"
            Case 48
                KeyHan = " + 0"
            Case 49
                KeyHan = " + 1"
            Case 50
                KeyHan = " + 2"
            Case 51
                KeyHan = " + 3"
            Case 52
                KeyHan = " + 4"
            Case 53
                KeyHan = " + 5"
            Case 54
                KeyHan = " + 6"
            Case 55
                KeyHan = " + 7"
            Case 56
                KeyHan = " + 8"
            Case 57
                KeyHan = " + 9"
            Case 58
                KeyHan = ""
            Case 96
                KeyHan = " + 0(テンキー）"
            Case 97
                KeyHan = " + 1(テンキー）"
            Case 98
                KeyHan = " + 2(テンキー）"
            Case 99
                KeyHan = " + 3(テンキー）"
            Case 100
                KeyHan = " + 4(テンキー）"
            Case 101
                KeyHan = " + 5(テンキー）"
            Case 102
                KeyHan = " + 6(テンキー）"
            Case 103
                KeyHan = " + 7(テンキー）"
            Case 104
                KeyHan = " + 8(テンキー）"
            Case 105
                KeyHan = " + 9(テンキー）"
            Case 176
                KeyHan = " + 次のトラックキー"
            Case 179
                KeyHan = " + 再生/一時停止キー"
            Case 177
                KeyHan = " + 前のトラックキー"
            Case 178
                KeyHan = " + 停止キー"

        End Select
    End Function

    Private objWordApp(9) As Object     'Word.Application ' ワードＡＰ
    Private objDocument(9) As Object    'Word.Document    ' ドキュメント

    ' 関数名    : RdWordValue
    ' 返り値    : 成功時(True)
    ' 引き数    : strPath (i) : エクセルファイルPATH
    '           : strDt   (o) : 収集したデータ
    ' 機能説明  : ワードデータ収集
    ' 備考      : 処理後ワード終了
    Public Function RdWordValue(ByVal strPath As String,
                                ByRef strDt As String) As Boolean
        On Error GoTo ErrHandler
        Const FUNCNAME = "RdWordValue"
        RdWordValue = False
        strDt = ""

        ' ワード起動
        Dim xp As Integer
        xp = GetWordObject()
        If xp < 0 Then
            Exit Function
        End If

        ' ワード非表示
        objWordApp(xp).Application.Visible = False
        objWordApp(xp).Application.DisplayAlerts = False
        ' ブックを開く
        objDocument(xp) = objWordApp(xp).Documents.Open(strPath, , True)

        ' テキスト選択
        objDocument(xp).Select()
        ' Clipboardコピー
        objDocument(xp).Range.Copy()
        ' Clipboardペースト
        strDt = Clipboard.GetText()

        ' 成功
        RdWordValue = True

        ' ワードの強制終了
        TerminateWord(xp)

        Exit Function
ErrHandler:
        MsgBox("エラー:" & FUNCNAME & vbCrLf & Err.Description)
        ' ワードの強制終了
        TerminateWord(xp)
        Exit Function
    End Function

    ' 関数名    : GetWordObject
    ' 返り値    : ワードＯＢＪの配列番号
    ' 引き数    : 無し
    ' 機能説明  : ワードＯＢＪ取得
    ' 備考      :
    Public Function GetWordObject() As Integer
        On Error GoTo ErrHandler
        GetWordObject = -1

        ' ワード起動
        Dim i As Long
        For i = 0 To UBound(objWordApp)
            If objWordApp(i) Is Nothing Then
                ' ワード起動
                objWordApp(i) = CreateObject("Word.Application")
                If objWordApp(i) Is Nothing = False Then
                    ' ワードＯＢＪの配列番号
                    GetWordObject = i
                    Exit For
                End If
            End If
        Next i

        If GetWordObject < 0 Then
            ' ワード起動失敗
            Err.Raise(9999, "", "原因不明のエラー")
        End If

        Exit Function
ErrHandler:
        Err.Raise(Err.Number, Err.Source,
            "Wordの起動に失敗しました" & vbCrLf & Err.Description)
        Exit Function
    End Function

    ' 関数名    : TerminateWord
    ' 返り値    : 無し
    ' 引き数    : 無し
    ' 機能説明  : ワードの強制終了
    ' 備考      :
    Public Sub TerminateWord(ByVal xp As Integer)
        On Error Resume Next

        Dim i As Integer
        For i = 0 To UBound(objWordApp)
            If i = xp Then
                If objDocument(i) Is Nothing = False Then
                    objDocument(i).Colse(0)
                End If
                If objWordApp(i) Is Nothing = False Then
                    objWordApp(i).Quit(0)
                End If

                objDocument(i) = Nothing
                objWordApp(i) = Nothing
                Exit For
            End If
        Next i

    End Sub

    ' 関数名    : TerminateWordAll
    ' 返り値    : 無し
    ' 引き数    : 無し
    ' 機能説明  : 全てのワードの強制終了
    ' 備考      :
    Public Sub TerminateWordAll()
        On Error Resume Next

        Dim i As Integer
        For i = 0 To UBound(objWordApp)
            If objDocument(i) Is Nothing = False Then
                objDocument(i).Colse(0)
            End If
            If objWordApp(i) Is Nothing = False Then
                objWordApp(i).Quit(0)
            End If

            objDocument(i) = Nothing
            objWordApp(i) = Nothing
        Next i
    End Sub

End Module
