Imports System.IO
Imports System.Text

''' <summary>
'''     しおりCSVの読み書きを行う
''' </summary>
Friend NotInheritable Class BookmarkCsvStore

    Private Sub New()
    End Sub

    ''' <summary>
    '''     Shift_JISのしおりCSVを読み込む
    ''' </summary>
    ''' <param name="filePath">読み込むCSVファイルのパス</param>
    ''' <returns>ヘッダーを除いた各行のセル配列</returns>
    Friend Shared Function Load(filePath As String) As List(Of String())
        Dim rows As New List(Of String())

        Using reader As New StreamReader(filePath, Encoding.GetEncoding("shift_jis"))
            If reader.ReadLine() Is Nothing Then Return rows

            Do
                Dim line As String = reader.ReadLine()
                If line Is Nothing Then Exit Do
                line = line.Replace("""", "")
                rows.Add(line.Split(","c))
            Loop
        End Using

        Return rows
    End Function

    ''' <summary>
    '''     しおり行をShift_JISのCSVとして保存する
    ''' </summary>
    ''' <param name="filePath">保存するCSVファイルのパス</param>
    ''' <param name="rows">ヘッダーを含む行データ</param>
    Friend Shared Sub Save(filePath As String, rows As IEnumerable(Of String()))
        Using writer As New StreamWriter(filePath, False, Encoding.GetEncoding("Shift_JIS"))
            For Each row As String() In rows
                Dim isFirst As Boolean = True
                For Each value As String In row
                    If Not isFirst Then
                        writer.Write(",")
                    End If
                    isFirst = False
                    writer.Write("""" & value & """")
                Next
                writer.Write(vbCrLf)
            Next
        End Using
    End Sub

End Class
