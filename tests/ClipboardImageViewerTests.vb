Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Drawing
Imports System.Windows.Forms

<TestClass>
Public Class ClipboardImageViewerTests

#Region "DisplayImage - 通常ケース"

    <TestMethod>
    Public Sub DisplayImage_ValidImage_SetsImageAndSizeMode()
        Using form As New ClipboardImageViewer()
            Dim testImage As New Bitmap(100, 50)

            form.SetClipboardImageForTest(testImage)
            form.DisplayImage(testImage)

            Assert.IsNotNull(form.PictureBox1.Image)
            Assert.AreEqual(PictureBoxSizeMode.Zoom, form.PictureBox1.SizeMode)
        End Using
    End Sub

#End Region

#Region "DisplayImage - 異常ケース"

    <TestMethod>
    Public Sub DisplayImage_NullImage_ThrowsArgumentNullException()
        Using form As New ClipboardImageViewer()
            Assert.Throws(Of ArgumentNullException)(Sub() form.DisplayImage(Nothing))
        End Using
    End Sub

#End Region

#Region "GetImageFormat - 正常ケース"

    <TestMethod>
    Public Sub GetImageFormat_JpgExtension_ReturnsJpeg()
        Using form As New ClipboardImageViewer()
            Assert.AreEqual(Imaging.ImageFormat.Jpeg, form.GetImageFormatForTest("test.jpg"))
            Assert.AreEqual(Imaging.ImageFormat.Jpeg, form.GetImageFormatForTest("test.jpeg"))
        End Using
    End Sub

    <TestMethod>
    Public Sub GetImageFormat_PngExtension_ReturnsPng()
        Using form As New ClipboardImageViewer()
            Assert.AreEqual(Imaging.ImageFormat.Png, form.GetImageFormatForTest("test.png"))
        End Using
    End Sub

    <TestMethod>
    Public Sub GetImageFormat_BmpExtension_ReturnsBmp()
        Using form As New ClipboardImageViewer()
            Assert.AreEqual(Imaging.ImageFormat.Bmp, form.GetImageFormatForTest("test.bmp"))
        End Using
    End Sub

    <TestMethod>
    Public Sub GetImageFormat_GifExtension_ReturnsGif()
        Using form As New ClipboardImageViewer()
            Assert.AreEqual(Imaging.ImageFormat.Gif, form.GetImageFormatForTest("test.gif"))
        End Using
    End Sub

#End Region

#Region "GetImageFormat - 境界ケース"

    <TestMethod>
    Public Sub GetImageFormat_UnknownExtension_ReturnsPng()
        Using form As New ClipboardImageViewer()
            Assert.AreEqual(Imaging.ImageFormat.Png, form.GetImageFormatForTest("test.unknown"))
        End Using
    End Sub

    <TestMethod>
    Public Sub GetImageFormat_EmptyExtension_ReturnsPng()
        Using form As New ClipboardImageViewer()
            Assert.AreEqual(Imaging.ImageFormat.Png, form.GetImageFormatForTest("test"))
        End Using
    End Sub

    <TestMethod>
    Public Sub GetImageFormat_CaseInsensitive_ReturnsCorrectFormat()
        Using form As New ClipboardImageViewer()
            Assert.AreEqual(Imaging.ImageFormat.Png, form.GetImageFormatForTest("test.PNG"))
            Assert.AreEqual(Imaging.ImageFormat.Jpeg, form.GetImageFormatForTest("test.JPG"))
        End Using
    End Sub

#End Region

#Region "SetClipboardImageForTest - 正常ケース"

    <TestMethod>
    Public Sub SetClipboardImageForTest_ValidImage_SetsClipboardImage()
        Using form As New ClipboardImageViewer()
            Dim testImage As New Bitmap(50, 50)

            form.SetClipboardImageForTest(testImage)

            Assert.IsNotNull(form.PictureBox1.Image)
        End Using
    End Sub

#End Region

#Region "ClearClipboardImageForTest - 正常ケース"

    <TestMethod>
    Public Sub ClearClipboardImageForTest_ImageSet_ClearsImage()
        Using form As New ClipboardImageViewer()
            Dim testImage As New Bitmap(50, 50)
            form.SetClipboardImageForTest(testImage)

            form.ClearClipboardImageForTest()

            Assert.IsNull(form.PictureBox1.Image)
        End Using
    End Sub

    <TestMethod>
    Public Sub ClearClipboardImageForTest_NoImage_DoesNotThrow()
        Using form As New ClipboardImageViewer()
            form.ClearClipboardImageForTest()
        End Using
    End Sub

#End Region

#Region "OnFormClosing - リソース解放"

    <TestMethod>
    Public Sub OnFormClosing_ImageSet_DisposesImage()
        Using form As New ClipboardImageViewer()
            Dim testImage As New Bitmap(50, 50)
            form.SetClipboardImageForTest(testImage)

            Dim e = New FormClosingEventArgs(CloseReason.UserClosing)
            form.OnFormClosing(e)

            Assert.IsNull(form.PictureBox1.Image)
        End Using
    End Sub

#End Region

#Region "New - 正常ケース"

    <TestMethod>
    Public Sub New_CreatesValidForm()
        Using form As New ClipboardImageViewer()
            Assert.IsNotNull(form)
            Assert.IsNotNull(form.PictureBox1)
        End Using
    End Sub

#End Region

End Class
