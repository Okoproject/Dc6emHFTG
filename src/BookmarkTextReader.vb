Imports System.Diagnostics
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms

''' <summary>
'''     しおり取込対象ファイルからテキストを取得する
''' </summary>
Friend NotInheritable Class BookmarkTextReader

    Private Sub New()
    End Sub

    ''' <summary>
    '''     Shift_JISのテキストファイルを読み込む
    ''' </summary>
    ''' <param name="filePath">読み込むテキストファイルのパス</param>
    ''' <returns>ファイルの全文</returns>
    Friend Shared Function ReadTextFile(filePath As String) As String
        Using reader As New StreamReader(filePath, Encoding.GetEncoding("Shift_JIS"))
            Return reader.ReadToEnd()
        End Using
    End Function

    ''' <summary>
    '''     Word文書の本文をクリップボード経由で取得する
    ''' </summary>
    ''' <param name="filePath">読み込むWord文書のパス</param>
    ''' <returns>Word文書の本文</returns>
    Friend Shared Function ReadWordDocument(filePath As String) As String
        Dim wordApplication As Object = Nothing
        Dim document As Object = Nothing
        Dim extractedText As String = String.Empty

        Try
            wordApplication = CreateObject("Word.Application")
            wordApplication.Visible = False
            document = wordApplication.Documents.Open(filePath)
            document.Range.Copy()
            extractedText = Clipboard.GetText()
        Finally
            Try
                Clipboard.Clear()
            Catch ex As Exception
                Debug.WriteLine("クリップボードのクリアに失敗しました: " & ex.Message)
            End Try

            If document IsNot Nothing Then
                Try
                    document.Close(False)
                Catch ex As Exception
                    Debug.WriteLine("Word文書を閉じられませんでした: " & ex.Message)
                End Try

                Try
                    Marshal.ReleaseComObject(document)
                Catch ex As Exception
                    Debug.WriteLine("Word文書のCOMオブジェクトを解放できませんでした: " & ex.Message)
                End Try
                document = Nothing
            End If

            If wordApplication IsNot Nothing Then
                Try
                    wordApplication.Quit()
                Catch ex As Exception
                    Debug.WriteLine("Wordを終了できませんでした: " & ex.Message)
                End Try

                Try
                    Marshal.ReleaseComObject(wordApplication)
                Catch ex As Exception
                    Debug.WriteLine("WordのCOMオブジェクトを解放できませんでした: " & ex.Message)
                End Try
                wordApplication = Nothing
            End If

            ' WordのCOMプロキシを残さないため、元の解放手順を維持する。
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try

        Return extractedText
    End Function

End Class
