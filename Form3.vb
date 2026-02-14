Public Class Form3
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Clipboard.ContainsImage() Then
            'クリップボードにあるデータの取得
            Dim img As Image = Clipboard.GetImage()
            If img IsNot Nothing Then
                'データが取得できたときは表示する
                PictureBox1.Image = img
            End If
        End If
    End Sub
End Class