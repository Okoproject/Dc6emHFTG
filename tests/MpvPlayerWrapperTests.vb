Imports Microsoft.VisualStudio.TestTools.UnitTesting

''' <summary>
''' MpvPlayerWrapperのテスト。
''' libmpv-2.dllが必要な機能はCI環境では実行できないため、
''' DLLが存在しない場合はスキップする。
''' </summary>
<TestClass>
Public Class MpvPlayerWrapperTests

#Region "初期状態テスト（DLL不要のロジック検証）"

    ''' <summary>
    ''' DLLが存在しない環境でインスタンス作成すると
    ''' InvalidOperationExceptionが発生することを確認
    ''' </summary>
    <TestMethod>
    <ExpectedException(GetType(InvalidOperationException))>
    Public Sub New_WithoutDll_ThrowsInvalidOperationException()
        ' libmpv-2.dll がテスト実行ディレクトリにない場合、
        ' mpv_create が失敗して例外が発生する
        Dim panel As New Windows.Forms.Panel()
        Try
            Dim player As New MpvPlayerWrapper(panel)
            ' DLLが存在する環境ではここに到達する可能性がある
            ' その場合はクリーンアップして成功とする
            player.Dispose()
            Assert.Inconclusive("libmpv-2.dll が利用可能な環境のためスキップ")
        Finally
            panel.Dispose()
        End Try
    End Sub

#End Region

#Region "HotKeyType 列挙テスト"

    <TestMethod>
    Public Sub HotKeyType_PlayPause_IsZero()
        Assert.AreEqual(0, CInt(HotKeyType.PlayPause))
    End Sub

    <TestMethod>
    Public Sub HotKeyType_HasExpectedMemberCount()
        Dim count = [Enum].GetValues(GetType(HotKeyType)).Length
        ' PlayPause(0) から ClipboardJump(29) まで 30 個
        Assert.AreEqual(30, count)
    End Sub

    <TestMethod>
    Public Sub HotKeyType_AllValuesAreUnique()
        Dim values = [Enum].GetValues(GetType(HotKeyType))
        Dim intValues = values.Cast(Of HotKeyType)().Select(Function(v) CInt(v)).ToArray()
        Assert.AreEqual(intValues.Length, intValues.Distinct().Count(),
                         "HotKeyType に重複する値があります")
    End Sub

#End Region

End Class
