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

            Dim newImage As Image = Clipboard.GetImage()

            If newImage Is Nothing Then
                ShowNoImageMessage()
                Return
            End If

            ' 古い画像を解放してから新しい画像を設定
            DisposeClipboardImage()
            _clipboardImage = newImage
            DisplayImage(_clipboardImage)

        Catch ex As Exception
            ShowErrorMessage(String.Format(My.Resources.ImageLoadFailed, ex.Message))
        End Try
    End Sub

    ''' <summary>
    ''' クリップボード画像を解放
    ''' </summary>
    Private Sub DisposeClipboardImage()
        If _clipboardImage IsNot Nothing Then
            _clipboardImage.Dispose()
            _clipboardImage = Nothing
        End If
    End Sub

    ''' <summary>
    ''' 画像を表示
    ''' </summary>
    Public Sub DisplayImage(image As Image)
        PictureBox1.Image = image
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom

        ' ウィンドウタイトルに画像情報を表示
        Text = String.Format(My.Resources.ClipboardImageTitle, image.Width, image.Height)
    End Sub

    ''' <summary>
    ''' 画像がない場合のメッセージ表示
    ''' </summary>
    Private Sub ShowNoImageMessage()
        Text = My.Resources.NoClipboardImage
        PictureBox1.Image = Nothing
    End Sub

    ''' <summary>
    ''' エラーメッセージの表示
    ''' </summary>
    Private Sub ShowErrorMessage(message As String)
        MessageBox.Show(message, My.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        Close()
    End Sub

#End Region

#Region "画像保存"

    ''' <summary>
    ''' 画像を保存
    ''' </summary>
    Private Sub SaveImage()
        If _clipboardImage Is Nothing Then
            MessageBox.Show(My.Resources.NoImageToSave, My.Resources.Confirm, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using saveDialog As New SaveFileDialog()
            saveDialog.Filter = My.Resources.ImageSaveFilter
            saveDialog.Title = My.Resources.SaveImage
            saveDialog.FileName = $"ClipboardImage_{DateTime.Now:yyyyMMdd_HHmmss}.png"

            If saveDialog.ShowDialog() = DialogResult.OK Then
                Try
                    Dim format As Imaging.ImageFormat = GetImageFormat(saveDialog.FileName)
                    _clipboardImage.Save(saveDialog.FileName, format)
                    MessageBox.Show(My.Resources.ImageSaved, My.Resources.Confirm, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    ShowErrorMessage(String.Format(My.Resources.SaveFailed, ex.Message))
                End Try
            End If
        End Using
    End Sub

    ''' <summary>
    ''' ファイル拡張子から画像フォーマットを取得
    ''' </summary>
    Private Function GetImageFormat(fileName As String) As Imaging.ImageFormat
        Dim extension As String = IO.Path.GetExtension(fileName).ToLower()

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

#Region "テスト用ヘルパー"

    ''' <summary>
    ''' テスト用にクリップボード画像を設定
    ''' </summary>
    Friend Sub SetClipboardImageForTest(image As Image)
        _clipboardImage = image
    End Sub

    ''' <summary>
    ''' テスト用にクリップボード画像をクリア
    ''' </summary>
    Friend Sub ClearClipboardImageForTest()
        DisposeClipboardImage()
    End Sub

    ''' <summary>
    ''' テスト用に画像フォーマットを取得
    ''' </summary>
    Friend Function GetImageFormatForTest(fileName As String) As Imaging.ImageFormat
        Return GetImageFormat(fileName)
    End Function

#End Region

#Region "リソース解放"

    ''' <summary>
    ''' フォーム終了時のリソース解放
    ''' </summary>
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        DisposeClipboardImage()
        MyBase.OnFormClosing(e)
    End Sub

#End Region

End Class
