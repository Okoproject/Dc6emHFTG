Imports System.Drawing

''' <summary>
''' クリップボード画像表示フォーム
''' </summary>
Public Class ClipboardImageViewer

#Region "メンバー変数"

    Private _clipboardImage As Image

#End Region

#Region "フォームイベント"

    ''' <summary>
    ''' フォーム読み込み時の処理
    ''' </summary>
    Private Sub ClipboardImageViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadClipboardImage()
    End Sub

#End Region

#Region "クリップボード処理"

    ''' <summary>
    ''' クリップボードから画像を読み込み
    ''' </summary>
    Private Sub LoadClipboardImage()
        Try
            If Not Clipboard.ContainsImage() Then
                ShowNoImageMessage()
                Return
            End If

            _clipboardImage = Clipboard.GetImage()

            If _clipboardImage Is Nothing Then
                ShowNoImageMessage()
                Return
            End If

            DisplayImage(_clipboardImage)

        Catch ex As Exception
            ShowErrorMessage($"画像の読み込みに失敗しました: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' 画像を表示
    ''' </summary>
    Private Sub DisplayImage(image As Image)
        PictureBox1.Image = image
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom

        ' ウィンドウタイトルに画像情報を表示
        Me.Text = $"クリップボード画像 - {image.Width}x{image.Height}"
    End Sub

    ''' <summary>
    ''' 画像がない場合のメッセージ表示
    ''' </summary>
    Private Sub ShowNoImageMessage()
        Me.Text = "クリップボードに画像がありません"
        PictureBox1.Image = Nothing
    End Sub

    ''' <summary>
    ''' エラーメッセージの表示
    ''' </summary>
    Private Sub ShowErrorMessage(message As String)
        MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

#End Region

#Region "ボタンイベント"

    ''' <summary>
    ''' 保存ボタンクリック
    ''' </summary>
    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SaveImage()
    End Sub

    ''' <summary>
    ''' 閉じるボタンクリック
    ''' </summary>
    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles Button33.Click
        Me.Close()
    End Sub

#End Region

#Region "画像保存"

    ''' <summary>
    ''' 画像を保存
    ''' </summary>
    Private Sub SaveImage()
        If _clipboardImage Is Nothing Then
            MessageBox.Show("保存する画像がありません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using saveDialog As New SaveFileDialog()
            saveDialog.Filter = "PNG画像|*.png|JPEG画像|*.jpg|Bitmap画像|*.bmp|すべてのファイル|*.*"
            saveDialog.Title = "画像を保存"
            saveDialog.FileName = $"ClipboardImage_{DateTime.Now:yyyyMMdd_HHmmss}.png"

            If saveDialog.ShowDialog() = DialogResult.OK Then
                Try
                    Dim format As Imaging.ImageFormat = GetImageFormat(saveDialog.FileName)
                    _clipboardImage.Save(saveDialog.FileName, format)
                    MessageBox.Show("画像を保存しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    ShowErrorMessage($"保存に失敗しました: {ex.Message}")
                End Try
            End If
        End Using
    End Sub

    ''' <summary>
    ''' ファイル拡張子から画像フォーマットを取得
    ''' </summary>
    Private Function GetImageFormat(fileName As String) As Imaging.ImageFormat
        Dim extension As String = System.IO.Path.GetExtension(fileName).ToLower()

        Select Case extension
            Case ".jpg", ".jpeg"
                Return Imaging.ImageFormat.Jpeg
            Case ".png"
                Return Imaging.ImageFormat.Png
            Case ".bmp"
                Return Imaging.ImageFormat.Bmp
            Case ".gif"
                Return Imaging.ImageFormat.Gif
            Case Else
                Return Imaging.ImageFormat.Png
        End Select
    End Function

#End Region

#Region "リソース解放"

    ''' <summary>
    ''' フォーム終了時のリソース解放
    ''' </summary>
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If _clipboardImage IsNot Nothing Then
            _clipboardImage.Dispose()
            _clipboardImage = Nothing
        End If
        MyBase.OnFormClosing(e)
    End Sub

#End Region

End Class
