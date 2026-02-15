Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Windows.Forms

<TestClass>
Public Class SettingsFormTests

#Region "GetModifierValue - 正常ケース"

    <TestMethod>
    Public Sub CalculateModifierValue_NoModifiers_ReturnsZero()
        Using form As New SettingsForm()
            form.SetCheckBoxStatesForTest(False, False, False)
            Assert.AreEqual(0, form.CalculateModifierValueForTest())
        End Using
    End Sub

    <TestMethod>
    Public Sub CalculateModifierValue_CtrlOnly_Returns3()
        Using form As New SettingsForm()
            form.SetCheckBoxStatesForTest(True, False, False)
            Assert.AreEqual(3, form.CalculateModifierValueForTest())
        End Using
    End Sub

    <TestMethod>
    Public Sub CalculateModifierValue_AltOnly_Returns4()
        Using form As New SettingsForm()
            form.SetCheckBoxStatesForTest(False, True, False)
            Assert.AreEqual(4, form.CalculateModifierValueForTest())
        End Using
    End Sub

    <TestMethod>
    Public Sub CalculateModifierValue_ShiftOnly_Returns5()
        Using form As New SettingsForm()
            form.SetCheckBoxStatesForTest(False, False, True)
            Assert.AreEqual(5, form.CalculateModifierValueForTest())
        End Using
    End Sub

#End Region

#Region "GetModifierValue - 組合せケース"

    <TestMethod>
    Public Sub CalculateModifierValue_CtrlAlt_Returns7()
        Using form As New SettingsForm()
            form.SetCheckBoxStatesForTest(True, True, False)
            Assert.AreEqual(7, form.CalculateModifierValueForTest())
        End Using
    End Sub

    <TestMethod>
    Public Sub CalculateModifierValue_CtrlShift_Returns8()
        Using form As New SettingsForm()
            form.SetCheckBoxStatesForTest(True, False, True)
            Assert.AreEqual(8, form.CalculateModifierValueForTest())
        End Using
    End Sub

    <TestMethod>
    Public Sub CalculateModifierValue_AltShift_Returns9()
        Using form As New SettingsForm()
            form.SetCheckBoxStatesForTest(False, True, True)
            Assert.AreEqual(9, form.CalculateModifierValueForTest())
        End Using
    End Sub

    <TestMethod>
    Public Sub CalculateModifierValue_AllModifiers_Returns12()
        Using form As New SettingsForm()
            form.SetCheckBoxStatesForTest(True, True, True)
            Assert.AreEqual(12, form.CalculateModifierValueForTest())
        End Using
    End Sub

#End Region

#Region "GetSelectedKeyCode - 正常・異常ケース"

    <TestMethod>
    Public Sub GetSelectedKeyCode_ValidFKey_ReturnsKeyCode()
        Using form As New SettingsForm()
            form.SetComboBox2TextForTest("F1")
            Assert.AreEqual(112, form.GetSelectedKeyCodeForTest())
        End Using
    End Sub

    <TestMethod>
    Public Sub GetSelectedKeyCode_SingleLetter_ReturnsAsciiCode()
        Using form As New SettingsForm()
            form.SetComboBox2TextForTest("A")
            Assert.AreEqual(65, form.GetSelectedKeyCodeForTest())
        End Using
    End Sub

    <TestMethod>
    Public Sub GetSelectedKeyCode_SingleDigit_ReturnsAsciiCode()
        Using form As New SettingsForm()
            form.SetComboBox2TextForTest("5")
            Assert.AreEqual(53, form.GetSelectedKeyCodeForTest())
        End Using
    End Sub

    <TestMethod>
    Public Sub GetSelectedKeyCode_EmptyText_ReturnsZero()
        Using form As New SettingsForm()
            form.SetComboBox2TextForTest(String.Empty)
            Assert.AreEqual(0, form.GetSelectedKeyCodeForTest())
        End Using
    End Sub

#End Region

#Region "GetKeyDisplayText - 正常・異常ケース"

    <TestMethod>
    Public Sub GetKeyDisplayText_F1_ReturnsDisplayString()
        Using form As New SettingsForm()
            Assert.AreEqual("F1", form.GetKeyDisplayTextForTest(112))
        End Using
    End Sub

    <TestMethod>
    Public Sub GetKeyDisplayText_SingleLetter_ReturnsLetter()
        Using form As New SettingsForm()
            Assert.AreEqual("A", form.GetKeyDisplayTextForTest(65))
        End Using
    End Sub

    <TestMethod>
    Public Sub GetKeyDisplayText_SingleDigit_ReturnsDigit()
        Using form As New SettingsForm()
            Assert.AreEqual("5", form.GetKeyDisplayTextForTest(53))
        End Using
    End Sub

    <TestMethod>
    Public Sub GetKeyDisplayText_UnknownKey_ReturnsKeyWithCode()
        Using form As New SettingsForm()
            Assert.AreEqual("Key(999)", form.GetKeyDisplayTextForTest(999))
        End Using
    End Sub

#End Region

#Region "GetSelectedHotKeyType - 正常ケース"

    <TestMethod>
    Public Sub GetSelectedHotKeyType_FirstItem_ReturnsPlayPause()
        Using form As New SettingsForm()
            form.CreateHandle()
            form.SetComboBox1SelectedIndexForTest(0)
            Assert.AreEqual(HotKeyManager.HotKeyType.PlayPause, form.GetSelectedHotKeyTypeForTest())
        End Using
    End Sub

    <TestMethod>
    Public Sub GetSelectedHotKeyType_LastItem_ReturnsClipboardJump()
        Using form As New SettingsForm()
            form.CreateHandle()
            form.SetComboBox1SelectedIndexForTest(29)
            Assert.AreEqual(HotKeyManager.HotKeyType.ClipboardJump, form.GetSelectedHotKeyTypeForTest())
        End Using
    End Sub

#End Region

#Region "UpdateHotKeyDisplay - 正常ケース"

    <TestMethod>
    Public Sub UpdateHotKeyDisplay_NoModifier_ReturnsKeyOnly()
        Using form As New SettingsForm()
            form.UpdateHotKeyDisplayForTest(0, 112)
            Assert.AreEqual("F1", form.Label3.Text)
        End Using
    End Sub

    <TestMethod>
    Public Sub UpdateHotKeyDisplay_WithModifier_ReturnsCombined()
        Using form As New SettingsForm()
            form.UpdateHotKeyDisplayForTest(3, 65)
            Assert.AreEqual("Ctrl + A", form.Label3.Text)
        End Using
    End Sub

#End Region

#Region "ComboBox操作関連 - 異常ケース"

    <TestMethod>
    Public Sub Button1_Click_EmptyComboBox_ShowsError()
        Using form As New SettingsForm()
            form.CreateHandle()
            form.SetComboBox2TextForTest(String.Empty)
            form.SetComboBox1SelectedIndexForTest(0)

            Assert.Throws(Of InvalidOperationException)(Sub() form.Button1_ClickForTest())
        End Using
    End Sub

#End Region

End Class
