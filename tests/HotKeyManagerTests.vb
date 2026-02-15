Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class HotKeyManagerTests

#Region "GetModifierValue テスト"

    <TestMethod>
    Public Sub GetModifierValue_None_ReturnsZero()
        Assert.AreEqual(0, GetModifierValue(0))
    End Sub

    <TestMethod>
    Public Sub GetModifierValue_Ctrl_ReturnsModControl()
        Assert.AreEqual(ModControl, GetModifierValue(3))
    End Sub

    <TestMethod>
    Public Sub GetModifierValue_Alt_ReturnsModAlt()
        Assert.AreEqual(ModAlt, GetModifierValue(4))
    End Sub

    <TestMethod>
    Public Sub GetModifierValue_Shift_ReturnsModShift()
        Assert.AreEqual(ModShift, GetModifierValue(5))
    End Sub

    <TestMethod>
    Public Sub GetModifierValue_CtrlAlt_ReturnsCombined()
        Dim expected = ModControl Or ModAlt
        Assert.AreEqual(expected, GetModifierValue(7))
    End Sub

    <TestMethod>
    Public Sub GetModifierValue_CtrlShift_ReturnsCombined()
        Dim expected = ModControl Or ModShift
        Assert.AreEqual(expected, GetModifierValue(8))
    End Sub

    <TestMethod>
    Public Sub GetModifierValue_AltShift_ReturnsCombined()
        Dim expected = ModAlt Or ModShift
        Assert.AreEqual(expected, GetModifierValue(9))
    End Sub

    <TestMethod>
    Public Sub GetModifierValue_CtrlAltShift_ReturnsCombined()
        Dim expected = ModControl Or ModShift Or ModAlt
        Assert.AreEqual(expected, GetModifierValue(12))
    End Sub

    <TestMethod>
    Public Sub GetModifierValue_UnknownValue_ReturnsNone()
        Assert.AreEqual(0, GetModifierValue(999))
    End Sub

#End Region

#Region "GetModifierDisplayText テスト"

    <TestMethod>
    Public Sub GetModifierDisplayText_None_ReturnsEmpty()
        Assert.AreEqual("", GetModifierDisplayText(0))
    End Sub

    <TestMethod>
    Public Sub GetModifierDisplayText_Ctrl_ReturnsCtrl()
        Assert.AreEqual("Ctrl", GetModifierDisplayText(3))
    End Sub

    <TestMethod>
    Public Sub GetModifierDisplayText_Alt_ReturnsAlt()
        Assert.AreEqual("Alt", GetModifierDisplayText(4))
    End Sub

    <TestMethod>
    Public Sub GetModifierDisplayText_Shift_ReturnsShift()
        Assert.AreEqual("Shift", GetModifierDisplayText(5))
    End Sub

    <TestMethod>
    Public Sub GetModifierDisplayText_CtrlAlt_ReturnsCombined()
        Assert.AreEqual("Ctrl + Alt", GetModifierDisplayText(7))
    End Sub

    <TestMethod>
    Public Sub GetModifierDisplayText_CtrlShift_ReturnsCombined()
        Assert.AreEqual("Ctrl + Shift", GetModifierDisplayText(8))
    End Sub

    <TestMethod>
    Public Sub GetModifierDisplayText_AltShift_ReturnsCombined()
        Assert.AreEqual("Alt + Shift", GetModifierDisplayText(9))
    End Sub

    <TestMethod>
    Public Sub GetModifierDisplayText_CtrlAltShift_ReturnsCombined()
        Assert.AreEqual("Ctrl + Alt + Shift", GetModifierDisplayText(12))
    End Sub

    <TestMethod>
    Public Sub GetModifierDisplayText_UnknownValue_ReturnsEmpty()
        Assert.AreEqual("", GetModifierDisplayText(999))
    End Sub

#End Region

#Region "GetSettingModifierProperty テスト"

    <TestMethod>
    Public Sub GetSettingModifierProperty_PlayPause_ReturnsSKAA()
        Assert.AreEqual("SKAA", GetSettingModifierProperty(HotKeyType.PlayPause))
    End Sub

    <TestMethod>
    Public Sub GetSettingModifierProperty_StopPlayback_ReturnsSKDA()
        Assert.AreEqual("SKDA", GetSettingModifierProperty(HotKeyType.StopPlayback))
    End Sub

    <TestMethod>
    Public Sub GetSettingModifierProperty_SpeedUp_ReturnsSKJA()
        Assert.AreEqual("SKJA", GetSettingModifierProperty(HotKeyType.SpeedUp))
    End Sub

    <TestMethod>
    Public Sub GetSettingModifierProperty_ClipboardJump_ReturnsSKPA()
        Assert.AreEqual("SKPA", GetSettingModifierProperty(HotKeyType.ClipboardJump))
    End Sub

#End Region

#Region "GetSettingKeyProperty テスト"

    <TestMethod>
    Public Sub GetSettingKeyProperty_PlayPause_ReturnsSKA()
        Assert.AreEqual("SKA", GetSettingKeyProperty(HotKeyType.PlayPause))
    End Sub

    <TestMethod>
    Public Sub GetSettingKeyProperty_StopPlayback_ReturnsSKD()
        Assert.AreEqual("SKD", GetSettingKeyProperty(HotKeyType.StopPlayback))
    End Sub

    <TestMethod>
    Public Sub GetSettingKeyProperty_SpeedUp_ReturnsSKJ()
        Assert.AreEqual("SKJ", GetSettingKeyProperty(HotKeyType.SpeedUp))
    End Sub

    <TestMethod>
    Public Sub GetSettingKeyProperty_ClipboardJump_ReturnsSKP()
        Assert.AreEqual("SKP", GetSettingKeyProperty(HotKeyType.ClipboardJump))
    End Sub

#End Region

#Region "GetSettingModifierProperty / GetSettingKeyProperty 全列挙値の非空テスト"

    <TestMethod>
    Public Sub GetSettingModifierProperty_AllHotKeyTypes_ReturnNonEmpty()
        For Each hotkeyType As HotKeyType In [Enum].GetValues(GetType(HotKeyType))
            Dim result = GetSettingModifierProperty(hotkeyType)
            Assert.IsTrue(result <> "", $"GetSettingModifierProperty returned empty for {hotkeyType}")
        Next
    End Sub

    <TestMethod>
    Public Sub GetSettingKeyProperty_AllHotKeyTypes_ReturnNonEmpty()
        For Each hotkeyType As HotKeyType In [Enum].GetValues(GetType(HotKeyType))
            Dim result = GetSettingKeyProperty(hotkeyType)
            Assert.IsTrue(result <> "", $"GetSettingKeyProperty returned empty for {hotkeyType}")
        Next
    End Sub

#End Region

End Class
