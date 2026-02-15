Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Windows.Forms

<TestClass>
Public Class CsvWriterTests

#Region "WriteCsvFromDgv - 正常ケース"

    <TestMethod>
    Public Sub WriteCsvFromDgv_ValidData_Succeeds()
        Using form = CreateFormWithTestData()
            Dim fileName = IO.Path.GetTempFileName()
            Try
                Dim result = WriteCsvFromDgv(fileName)
                Assert.IsTrue(result)
                Assert.IsTrue(IO.File.Exists(fileName))
            Finally
                If IO.File.Exists(fileName) Then IO.File.Delete(fileName)
            End Try
        End Using
    End Sub

#End Region

#Region "WriteCsvFromDgv - 異常ケース"

    <TestMethod>
    Public Sub WriteCsvFromDgv_EmptyFileName_ReturnsFalse()
        Using form = CreateFormWithTestData()
            Dim result = WriteCsvFromDgv(String.Empty)
            Assert.IsFalse(result)
        End Using
    End Sub

    <TestMethod>
    Public Sub WriteCsvFromDgv_NullFileName_ReturnsFalse()
        Using form = CreateFormWithTestData()
            Dim result = WriteCsvFromDgv(Nothing)
            Assert.IsFalse(result)
        End Using
    End Sub

#End Region

#Region "WriteCsv - 正常ケース"

    <TestMethod>
    Public Sub WriteCsv_ValidData_Succeeds()
        Dim tempFile = IO.Path.GetTempFileName()
        Try
            Dim data = New String()() {
                New String() {"Header1", "Header2", "Header3"},
                New String() {"Data1", "Data2", "Data3"}
            }

            Dim result = WriteCsv(tempFile, data)
            Assert.IsTrue(result)
            Assert.IsTrue(IO.File.Exists(tempFile))
        Finally
            If IO.File.Exists(tempFile) Then IO.File.Delete(tempFile)
        End Try
    End Sub

#End Region

#Region "WriteCsv - 異常ケース"

    <TestMethod>
    Public Sub WriteCsv_EmptyData_Succeeds()
        Dim tempFile = IO.Path.GetTempFileName()
        Try
            Dim data = New String()() {}
            Dim result = WriteCsv(tempFile, data)
            Assert.IsTrue(result)
        Finally
            If IO.File.Exists(tempFile) Then IO.File.Delete(tempFile)
        End Try
    End Sub

    <TestMethod>
    Public Sub WriteCsv_InvalidPath_ThrowsException()
        Dim data = New String()() {New String() {"A", "B"}}
        Assert.Throws(Of Exception)(Sub() WriteCsv("!!!invalid|||path", data))
    End Sub

#End Region

#Region "WriteCsv - 境界ケース"

    <TestMethod>
    Public Sub WriteCsv_SpecialCharactersInData_EscapesCorrectly()
        Dim tempFile = IO.Path.GetTempFileName()
        Try
            Dim data = New String()() {
                New String() {"Test,Comma", "Test""Quote", "Test"""",,,Comma"}
            }

            WriteCsv(tempFile, data)

            Dim content = IO.File.ReadAllText(tempFile, System.Text.Encoding.GetEncoding("Shift_JIS"))
            Assert.IsTrue(content.Contains("""Test,Comma"""))
            Assert.IsTrue(content.Contains("""Test""""Quote"""))
        Finally
            If IO.File.Exists(tempFile) Then IO.File.Delete(tempFile)
        End Try
    End Sub

    <TestMethod>
    Public Sub WriteCsv_EmptyCells_HandlesCorrectly()
        Dim tempFile = IO.Path.GetTempFileName()
        Try
            Dim data = New String()() {
                New String() {"A", "", "C"},
                New String() {"", "B", ""}
            }

            WriteCsv(tempFile, data)

            Dim lines = IO.File.ReadAllLines(tempFile, System.Text.Encoding.GetEncoding("Shift_JIS"))
            Assert.AreEqual(2, lines.Length)
        Finally
            If IO.File.Exists(tempFile) Then IO.File.Delete(tempFile)
        End Try
    End Sub

#End Region

#Region "ヘルパーメソッド"

    Private Function CreateFormWithTestData() As MainPlayerForm
        Dim form As New MainPlayerForm()
        form.CreateHandle()

        ' DataGridViewにテストデータを追加
        form.DataGridView1.Columns.Add("Col1", "Header1")
        form.DataGridView1.Columns.Add("Col2", "Header2")
        form.DataGridView1.Columns.Add("Col3", "Header3")
        form.DataGridView1.Rows.Add("A1", "B1", "C1")
        form.DataGridView1.Rows.Add("A2", "B2", "C2")

        Return form
    End Function

    ' MainPlayerFormのPrivateメソッドを呼び出すためのリフレクションヘルパー
    Private Function WriteCsvFromDgv(fileName As String) As Boolean
        Dim method = GetType(MainPlayerForm).GetMethod("WriteCsvFromDgv",
            System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Instance)
        If method IsNot Nothing Then
            Return DirectCast(method.Invoke(Nothing, New Object() {fileName}), Boolean)
        End If
        Return False
    End Function

    Private Function WriteCsv(path As String, data As String()()) As Boolean
        Dim method = GetType(MainPlayerForm).GetMethod("WriteCsv",
            System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Static)
        If method IsNot Nothing Then
            Return DirectCast(method.Invoke(Nothing, New Object() {path, data}), Boolean)
        End If
        Return False
    End Function

#End Region

End Class
