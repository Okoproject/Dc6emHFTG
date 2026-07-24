''' <summary>
'''     マーカーを含むテキストからしおりを抽出する
''' </summary>
Friend NotInheritable Class BookmarkTextParser

    Private Const TimestampLength As Integer = 10
    Private Const TimeDisplayLength As Integer = 8

    Private Sub New()
    End Sub

    ''' <summary>
    '''     設定されたマーカーを使ってテキストからしおりを抽出する
    ''' </summary>
    ''' <param name="content">解析するテキスト</param>
    ''' <param name="fukaMarker">聞き取り不可マーカー</param>
    ''' <param name="fumeiMarker">不明箇所の開始マーカー</param>
    ''' <param name="fumeiEndMarker">不明箇所の終了マーカー</param>
    ''' <param name="sonotaMarker">その他メモの開始マーカー</param>
    ''' <returns>テキスト内の順序で抽出したしおり</returns>
    Friend Shared Function Parse(content As String, fukaMarker As String, fumeiMarker As String,
                                 fumeiEndMarker As String, sonotaMarker As String) As List(Of BookmarkEntry)
        Dim entries As New List(Of BookmarkEntry)

        For n As Integer = 0 To content.Length - TimestampLength
            Dim currentChar As String = content.Substring(n, 1)

            If currentChar = fukaMarker Then
                ParseFukaPattern(content, n, entries)
            ElseIf currentChar = fumeiMarker Then
                ParseFumeiPattern(content, n, fumeiEndMarker, entries)
            ElseIf currentChar = sonotaMarker Then
                ParseSonotaPattern(content, n, entries)
            End If
        Next

        Return entries
    End Function

    Private Shared Function ParseTimestampToSeconds(timestamp As String) As Integer
        Return (Integer.Parse(timestamp.Substring(1, 2)) * 3600) +
               (Integer.Parse(timestamp.Substring(4, 2)) * 60) +
               Integer.Parse(timestamp.Substring(7, 2))
    End Function

    Private Shared Sub ParseFukaPattern(content As String, startIndex As Integer,
                                        entries As ICollection(Of BookmarkEntry))
        If startIndex + TimestampLength + 1 > content.Length Then Return

        Dim timestamp As String = content.Substring(startIndex + 1, TimestampLength)
        Dim seconds As Integer = ParseTimestampToSeconds(timestamp)
        Dim timeDisplay As String = content.Substring(startIndex + 2, TimeDisplayLength)
        entries.Add(New BookmarkEntry(timeDisplay, "聞き取り不可", seconds))
    End Sub

    Private Shared Sub ParseFumeiPattern(content As String, startIndex As Integer, endMarker As String,
                                         entries As ICollection(Of BookmarkEntry))
        For i As Integer = startIndex + 1 To content.Length - TimestampLength - 1
            If content.Substring(i, 1) = endMarker Then
                Dim memo As String = content.Substring(startIndex + 1, i - startIndex - 1)
                Dim timestamp As String = content.Substring(i + 1, TimestampLength)
                Dim seconds As Integer = ParseTimestampToSeconds(timestamp)
                Dim timeDisplay As String = content.Substring(i + 2, TimeDisplayLength)
                entries.Add(New BookmarkEntry(timeDisplay, memo & "？", seconds))
                Exit For
            End If
        Next
    End Sub

    Private Shared Sub ParseSonotaPattern(content As String, startIndex As Integer,
                                          entries As ICollection(Of BookmarkEntry))
        For i As Integer = startIndex + 1 To content.Length - TimestampLength - 1
            Dim checkChar As String = content.Substring(i, 1)
            If checkChar = "(" OrElse checkChar = "（" Then
                Dim memo As String = content.Substring(startIndex, i - startIndex)
                Dim timestamp As String = content.Substring(i, TimestampLength)
                Dim seconds As Integer = ParseTimestampToSeconds(timestamp)
                Dim timeDisplay As String = content.Substring(i + 1, TimeDisplayLength)
                entries.Add(New BookmarkEntry(timeDisplay, memo, seconds))
                Exit For
            End If
        Next
    End Sub

End Class
