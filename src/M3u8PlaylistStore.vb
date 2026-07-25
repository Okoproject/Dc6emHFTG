Imports System.IO
Imports System.Text

''' <summary>
'''     M3U8形式のプレイリストを読み書きする
''' </summary>
Friend NotInheritable Class M3u8PlaylistStore

    Private Sub New()
    End Sub

    ''' <summary>
    '''     プレイリスト項目をUTF-8のM3U8ファイルとして保存する
    ''' </summary>
    ''' <param name="filePath">保存するM3U8ファイルのパス</param>
    ''' <param name="items">保存するプレイリスト項目</param>
    Friend Shared Sub Save(filePath As String, items As IEnumerable(Of PlaylistItem))
        Using writer As New StreamWriter(filePath, False, Encoding.UTF8)
            writer.WriteLine("#EXTM3U")
            For Each item As PlaylistItem In items
                writer.WriteLine("#EXTINF:" & item.Duration & "," & item.FileName)
                If Not String.IsNullOrEmpty(item.Memo) Then
                    writer.WriteLine("#OKM-MEMO:" & item.Memo)
                End If
                If item.Position > 0 Then
                    writer.WriteLine("#OKM-POS:" & item.Position)
                End If
                writer.WriteLine(item.FilePath)
            Next
        End Using
    End Sub

    ''' <summary>
    '''     UTF-8のM3U8ファイルからプレイリスト項目を読み込む
    ''' </summary>
    ''' <param name="filePath">読み込むM3U8ファイルのパス</param>
    ''' <returns>読み込んだプレイリスト項目</returns>
    Friend Shared Function Load(filePath As String) As List(Of PlaylistItem)
        Dim items As New List(Of PlaylistItem)

        Using reader As New StreamReader(filePath, Encoding.UTF8)
            Dim currentItem As PlaylistItem = Nothing
            Do
                Dim line As String = reader.ReadLine()
                If line Is Nothing Then Exit Do
                If String.IsNullOrWhiteSpace(line) Then Continue Do
                If line.StartsWith("#EXTM3U") Then Continue Do
                If line.StartsWith("#EXTINF:") Then
                    currentItem = New PlaylistItem()
                    Dim dataPart As String = line.Substring(8)
                    Dim commaPosition As Integer = dataPart.IndexOf(","c)
                    If commaPosition > 0 Then
                        Double.TryParse(dataPart.Substring(0, commaPosition), currentItem.Duration)
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

                If currentItem IsNot Nothing Then
                    currentItem.FilePath = line
                Else
                    currentItem = New PlaylistItem(line)
                End If
                items.Add(currentItem)
                currentItem = Nothing
            Loop
        End Using

        Return items
    End Function

End Class
