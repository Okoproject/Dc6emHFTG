Imports System.ComponentModel

''' <summary>
'''     設定フォーム
''' </summary>
Public Class SettingsForm

#Region "定数"

    ' 修飾キーの値
    Private Const ModifierCtrl As Integer = 3
    Private Const ModifierAlt As Integer = 4
    Private Const ModifierShift As Integer = 5

#End Region

#Region "辞書"

    ''' <summary>
    '''     キー表示名と仮想キーコードの対応
    ''' </summary>
    Private ReadOnly _keyCodeMapping As New Dictionary(Of String, Integer) From {
        {"F1", 112}, {"F2", 113}, {"F3", 114}, {"F4", 115},
        {"F5", 116}, {"F6", 117}, {"F7", 118}, {"F8", 119},
        {"F9", 120}, {"F10", 121}, {"F11", 122}, {"F12", 123},
        {"F13", 124}, {"F14", 125}, {"F15", 126}, {"F16", 127},
        {"↑", 38}, {"↓", 40}, {"←", 37}, {"→", 39},
        {"0(テンキー)", 96}, {"1(テンキー)", 97}, {"2(テンキー)", 98}, {"3(テンキー)", 99},
        {"4(テンキー)", 100}, {"5(テンキー)", 101}, {"6(テンキー)", 102}, {"7(テンキー)", 103},
        {"8(テンキー)", 104}, {"9(テンキー)", 105},
        {"次のトラックキー", 176}, {"再生/一時停止キー", 179},
        {"前のトラックキー", 177}, {"停止キー", 178}
        }

    ''' <summary>
    '''     キーコードから表示名への逆引き辞書
    ''' </summary>
    Private ReadOnly _keyNameMapping As Dictionary(Of Integer, String)

    ''' <summary>
    '''     ホットキー種別とコンボボックスインデックスの対応
    ''' </summary>
    Private ReadOnly _hotKeyTypeComboIndexMapping As New Dictionary(Of Integer, HotKeyType) From {
        {0, HotKeyType.PlayPause},
        {1, HotKeyType.PlayPauseWithCounterCopy1},
        {2, HotKeyType.PlayPauseWithCounterCopy2},
        {3, HotKeyType.PlayPauseWithCounterCopy3},
        {4, HotKeyType.StopPlayback},
        {5, HotKeyType.CounterCopy1},
        {6, HotKeyType.CounterCopy2},
        {7, HotKeyType.CounterCopy3},
        {8, HotKeyType.AddBookmark},
        {9, HotKeyType.PlayPauseWithBookmark},
        {10, HotKeyType.JumpHotkey1},
        {11, HotKeyType.JumpHotkey2},
        {12, HotKeyType.JumpHotkey3},
        {13, HotKeyType.JumpHotkey4},
        {14, HotKeyType.JumpHotkey5},
        {15, HotKeyType.JumpHotkey6},
        {16, HotKeyType.SpeedUp},
        {17, HotKeyType.SpeedDown},
        {18, HotKeyType.SpeedResetTo1X},
        {19, HotKeyType.SpeedSetToHalf},
        {20, HotKeyType.SpeedSetToDouble},
        {21, HotKeyType.BringWindowToFront},
        {22, HotKeyType.SpeedControlButton1},
        {23, HotKeyType.SpeedControlButton2},
        {24, HotKeyType.SpeedControlButton3},
        {25, HotKeyType.SpeedControlButton4},
        {26, HotKeyType.SpeedControlButton5},
        {27, HotKeyType.SpeedControlButton6},
        {28, HotKeyType.SpeedControlButton7},
        {29, HotKeyType.ClipboardJump}
        }

#End Region

#Region "コンストラクタ"

    Public Sub New()
        InitializeComponent()

        ' 逆引き辞書の構築
        _keyNameMapping = New Dictionary(Of Integer, String)
        For Each kvp As KeyValuePair(Of String, Integer) In _keyCodeMapping
            _keyNameMapping(kvp.Value) = kvp.Key
        Next
    End Sub

#End Region

#Region "フォームイベント"

    Private Sub SettingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAllSettings()
        PositionRelativeToMainForm()
    End Sub

    ''' <summary>
    '''     メインフォームの右側に配置
    ''' </summary>
    Private Sub PositionRelativeToMainForm()
        If MainPlayerForm.Instance IsNot Nothing Then
            Dim mainForm = MainPlayerForm.Instance
            ' メインフォームの右側に配置
            Left = mainForm.Right + 10
            Top = mainForm.Top

            ' 画面外に出る場合は左側に配置
            If Right > Screen.PrimaryScreen.WorkingArea.Right Then
                Left = Math.Max(10, mainForm.Left - Width - 10)
            End If

            ' 画面外に出る場合は上に調整
            If Bottom > Screen.PrimaryScreen.WorkingArea.Bottom Then
                Top = Math.Max(10, Screen.PrimaryScreen.WorkingArea.Bottom - Height - 10)
            End If
        End If
    End Sub

    ''' <summary>
    '''     アクティブ時にメインフォームの下に行かないようにする
    ''' </summary>
    Private Sub SettingsForm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If MainPlayerForm.Instance IsNot Nothing Then
            ' メインフォームより手前に表示
            BringToFront()
        End If
    End Sub

    ''' <summary>
    '''     メインフォームがアクティブになった場合も設定フォームを手前に
    ''' </summary>
    Private Sub SettingsForm_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
        ' 設定フォームからフォーカスが外れた場合でも、メインフォームがアクティブなら
        ' 設定フォームを手前に保持（モーダル表示中はこの処理は不要ですが、安全策として）
    End Sub

#End Region

#Region "設定読み込み"

    ''' <summary>
    '''     全設定を読み込み
    ''' </summary>
    Private Sub LoadAllSettings()
        LoadTimeCodeSettings()
        LoadAutoBackupSettings()
        LoadTimeCodeModifierSettings()
        LoadJumpButtonSettings()
        LoadJumpHotkeySettings()
        LoadSpeedControlSettings()
        LoadAutoPlaySettings()
    End Sub

    ''' <summary>
    '''     タイムコード形式設定の読み込み
    ''' </summary>
    Private Sub LoadTimeCodeSettings()
        Select Case My.Settings.TimeCode
            Case 0 : RadioButton1.Checked = True
            Case 1 : RadioButton2.Checked = True
            Case 2 : RadioButton3.Checked = True
        End Select
    End Sub

    ''' <summary>
    '''     自動巻戻し設定とカウンタコピーとメモのCSVの保存設定の読み込み
    ''' </summary>
    Private Sub LoadAutoBackupSettings()

        NumericUpDown1.Value = My.Settings.AutoBack / 100

        CheckBox1.Checked = My.Settings.autoBM
        TextBox10.Text = My.Settings.autoBMDir
    End Sub

    ''' <summary>
    '''     タイムコード修飾設定の読み込み
    ''' </summary>
    Private Sub LoadTimeCodeModifierSettings()
        TextBox1.Text = My.Settings.Atama
        TextBox2.Text = My.Settings.Oshiri
        TextBox4.Text = My.Settings.Atama2
        TextBox3.Text = My.Settings.Oshiri2
        TextBox6.Text = My.Settings.Atama3
        TextBox5.Text = My.Settings.Oshiri3

        RadioButton5.Checked = My.Settings.shiori_PS
    End Sub

    ''' <summary>
    '''     ジャンプボタン設定の読み込み
    ''' </summary>
    Private Sub LoadJumpButtonSettings()
        NumericUpDown2.Value = My.Settings.SK1
        NumericUpDown3.Value = My.Settings.SK2
        NumericUpDown4.Value = My.Settings.SK3
        NumericUpDown5.Value = My.Settings.SK4
        NumericUpDown6.Value = My.Settings.SK5
        NumericUpDown7.Value = My.Settings.SK6
        NumericUpDown8.Value = My.Settings.SK7
        NumericUpDown9.Value = My.Settings.SK8
        NumericUpDown10.Value = My.Settings.SK9
        NumericUpDown11.Value = My.Settings.SK10
        NumericUpDown12.Value = My.Settings.SK11
        NumericUpDown13.Value = My.Settings.SK12
        NumericUpDown14.Value = My.Settings.SK13
        NumericUpDown15.Value = My.Settings.SK14
        NumericUpDown16.Value = My.Settings.SK15
        NumericUpDown17.Value = My.Settings.SK16
        NumericUpDown18.Value = My.Settings.SK17
        NumericUpDown19.Value = My.Settings.SK18
        NumericUpDown20.Value = My.Settings.SK19
        NumericUpDown21.Value = My.Settings.SK20
    End Sub

    ''' <summary>
    '''     ジャンプホットキー設定の読み込み
    ''' </summary>
    Private Sub LoadJumpHotkeySettings()
        NumericUpDown22.Value = My.Settings.MM1
        NumericUpDown23.Value = My.Settings.MM2
        NumericUpDown24.Value = My.Settings.MM3
        NumericUpDown25.Value = My.Settings.HO1
        NumericUpDown26.Value = My.Settings.HO2
        NumericUpDown27.Value = My.Settings.HO3

        UpdateJumpHotkeyLabels()
    End Sub

    ''' <summary>
    '''     速度コントロール設定の読み込み
    ''' </summary>
    Private Sub LoadSpeedControlSettings()
        Dim ctrls As NumericUpDown() = {NumericUpDown28, NumericUpDown29, NumericUpDown30,
                                        NumericUpDown31, NumericUpDown32, NumericUpDown33, NumericUpDown34}
        Dim props As String() = {"SC1", "SC2", "SC3", "SC4", "SC5", "SC6", "SC7"}

        For i As Integer = 0 To ctrls.Length - 1
            Dim v As Decimal = CDec(CallByName(My.Settings, props(i), CallType.Get) / 10.0)
            ' 旧バージョンで誤保存された値（0 等）がコントロール可動範囲外になるのを防ぐ。
            ' clamp 後の値は正しい SC 保存処理（×10）で再保存され、設定が self-heal する。
            If v < ctrls(i).Minimum Then v = ctrls(i).Minimum
            If v > ctrls(i).Maximum Then v = ctrls(i).Maximum
            ctrls(i).Value = v
        Next

        UpdateSpeedControlLabels()
    End Sub

    ''' <summary>
    '''     ファイル読込時に自動再生するかどうか
    ''' </summary>
    Private Sub LoadAutoPlaySettings()
        If My.Settings.AutoPlay = True Then
            RadioButton6.Checked = True
        Else
            RadioButton7.Checked = True
        End If
        UpdateSpeedControlLabels()
    End Sub

#End Region

#Region "ホットキー設定"

    ''' <summary>
    '''     設定ボタンクリック
    ''' </summary>
    Friend Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If String.IsNullOrEmpty(ComboBox2.Text) Then
            MessageBox.Show(My.Resources.EnterShortcutKey, My.Resources.Error, MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim hotkeyType As HotKeyType = GetSelectedHotKeyType()
        Dim modifierValue As Integer = CalculateModifierValue()
        Dim keyCode As Integer = GetSelectedKeyCode()

        ' ホットキーを登録
        RegisterHotKeySetting(hotkeyType, modifierValue, keyCode)

        ' 設定を保存
        SaveHotKeySetting(hotkeyType, modifierValue, keyCode)

        ' 表示を更新
        UpdateHotKeyDisplay(modifierValue, keyCode)

        MessageBox.Show(My.Resources.SettingsComplete, My.Resources.Confirm, MessageBoxButtons.OK,
                        MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    '''     解除ボタンクリック
    ''' </summary>
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim hotkeyType As HotKeyType = GetSelectedHotKeyType()

        ' ホットキーを解除
        UnregisterHotKeySetting(hotkeyType)

        ' 設定をクリア
        ClearHotKeySetting(hotkeyType)

        Label3.Text = String.Empty
        MessageBox.Show(My.Resources.KeySettingsDeleted, My.Resources.Confirm, MessageBoxButtons.OK,
                        MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    '''     選択されたホットキー種別を取得
    ''' </summary>
    Friend Function GetSelectedHotKeyType() As HotKeyType
        If _hotKeyTypeComboIndexMapping.ContainsKey(ComboBox1.SelectedIndex) Then
            Return _hotKeyTypeComboIndexMapping(ComboBox1.SelectedIndex)
        End If
        Return HotKeyType.PlayPause
    End Function

    ''' <summary>
    '''     修飾キーの値を計算
    ''' </summary>
    Friend Function CalculateModifierValue() As Integer
        Dim modifier = 0
        If CheckBox2.Checked Then modifier += ModifierCtrl
        If CheckBox3.Checked Then modifier += ModifierAlt
        If CheckBox4.Checked Then modifier += ModifierShift
        Return modifier
    End Function

    ''' <summary>
    '''     選択されたキーコードを取得
    ''' </summary>
    Friend Function GetSelectedKeyCode() As Integer
        Dim selectedText As String = ComboBox2.Text

        ' 定義済みキーを検索
        If _keyCodeMapping.ContainsKey(selectedText) Then
            Return _keyCodeMapping(selectedText)
        End If

        ' 単一文字の場合はASCIIコード
        If selectedText.Length = 1 Then
            Return Asc(selectedText)
        End If

        Return 0
    End Function

    ''' <summary>
    '''     ホットキーを登録
    ''' </summary>
    Private Sub RegisterHotKeySetting(hotkeyType As HotKeyType, modifier As Integer, keyCode As Integer)
        Dim atomId As Short = HotKeyAtoms(hotkeyType)
        Dim win32Modifier As Integer = GetModifierValue(modifier)

        If MainPlayerForm.Instance IsNot Nothing Then
            UnregisterHotKey(MainPlayerForm.Instance.Handle, atomId)
            RegisterHotKey(MainPlayerForm.Instance.Handle, atomId, win32Modifier, CType(keyCode, Keys))
        End If
    End Sub

    ''' <summary>
    '''     ホットキー設定を保存
    ''' </summary>
    Private Sub SaveHotKeySetting(hotkeyType As HotKeyType, modifier As Integer, keyCode As Integer)
        Dim modifierProp As String = GetSettingModifierProperty(hotkeyType)
        Dim keyProp As String = GetSettingKeyProperty(hotkeyType)

        If Not String.IsNullOrEmpty(modifierProp) Then
            CallByName(My.Settings, modifierProp, CallType.Set, modifier)
        End If
        If Not String.IsNullOrEmpty(keyProp) Then
            CallByName(My.Settings, keyProp, CallType.Set, keyCode)
        End If

        My.Settings.Save()
    End Sub

    ''' <summary>
    '''     ホットキーを解除
    ''' </summary>
    Private Sub UnregisterHotKeySetting(hotkeyType As HotKeyType)
        If MainPlayerForm.Instance Is Nothing Then
            Return
        End If
        Dim atomId As Short = HotKeyAtoms(hotkeyType)
        UnregisterHotKey(MainPlayerForm.Instance.Handle, atomId)
    End Sub

    ''' <summary>
    '''     ホットキー設定をクリア
    ''' </summary>
    Private Sub ClearHotKeySetting(hotkeyType As HotKeyType)
        Dim modifierProp As String = GetSettingModifierProperty(hotkeyType)
        Dim keyProp As String = GetSettingKeyProperty(hotkeyType)

        If Not String.IsNullOrEmpty(modifierProp) Then
            CallByName(My.Settings, modifierProp, CallType.Set, -1)
        End If
        If Not String.IsNullOrEmpty(keyProp) Then
            CallByName(My.Settings, keyProp, CallType.Set, -1)
        End If

        My.Settings.Save()
    End Sub

    ''' <summary>
    '''     ホットキー表示を更新
    ''' </summary>
    Friend Sub UpdateHotKeyDisplay(modifier As Integer, keyCode As Integer)
        Label3.Text = GetHotKeyDisplayText(modifier, keyCode)
    End Sub

    ''' <summary>
    '''     キーコードから表示テキストを取得
    ''' </summary>
    Friend Function GetKeyDisplayText(keyCode As Integer) As String
        ' 定義済みキーを検索
        If _keyNameMapping.ContainsKey(keyCode) Then
            Return _keyNameMapping(keyCode)
        End If

        ' 範囲チェック
        If keyCode >= 65 AndAlso keyCode <= 90 Then
            Return Chr(keyCode) ' A-Z
        End If
        If keyCode >= 48 AndAlso keyCode <= 57 Then
            Return Chr(keyCode) ' 0-9
        End If

        Return "Key(" & keyCode & ")"
    End Function

#End Region

#Region "ラベル更新"

    ''' <summary>
    '''     ジャンプホットキーラベルを更新
    ''' </summary>
    Private Sub UpdateJumpHotkeyLabels()
        UpdateJumpLabel(Label14, My.Settings.SKM1A, My.Settings.SKM1)
        UpdateJumpLabel(Label16, My.Settings.SKM2A, My.Settings.SKM2)
        UpdateJumpLabel(Label18, My.Settings.SKM3A, My.Settings.SKM3)
        UpdateJumpLabel(Label15, My.Settings.SKH1A, My.Settings.SKH1)
        UpdateJumpLabel(Label17, My.Settings.SKH2A, My.Settings.SKH2)
        UpdateJumpLabel(Label19, My.Settings.SKH3A, My.Settings.SKH3)
    End Sub

    ''' <summary>
    '''     修飾キーとキー入力から表示文字列を生成する（未設定の場合は「未設定」）
    ''' </summary>
    Friend Function GetHotKeyDisplayText(modifier As Integer, keyCode As Integer) As String
        If keyCode <= 0 OrElse modifier < 0 Then
            Return "未設定"
        End If

        Dim modifierText As String = GetModifierDisplayText(modifier)
        Dim keyText As String = GetKeyDisplayText(keyCode)

        If String.IsNullOrEmpty(modifierText) Then
            Return keyText
        End If
        Return modifierText & " + " & keyText
    End Function

    ''' <summary>
    '''     ジャンプホットキーラベルを更新
    ''' </summary>
    Private Sub UpdateJumpLabel(label As Label, modifier As Integer, keyCode As Integer)
        label.Text = "秒(" & GetHotKeyDisplayText(modifier, keyCode) & ")"
    End Sub

    ''' <summary>
    '''     速度コントロールラベルを更新
    ''' </summary>
    Private Sub UpdateSpeedControlLabels()
        UpdateSpeedLabel(Label25, My.Settings.SC1A, My.Settings.SKSC1)
        UpdateSpeedLabel(Label26, My.Settings.SC2A, My.Settings.SKSC2)
        UpdateSpeedLabel(Label28, My.Settings.SC3A, My.Settings.SKSC3)
        UpdateSpeedLabel(Label30, My.Settings.SC4A, My.Settings.SKSC4)
        UpdateSpeedLabel(Label32, My.Settings.SC5A, My.Settings.SKSC5)
        UpdateSpeedLabel(Label34, My.Settings.SC6A, My.Settings.SKSC6)
        UpdateSpeedLabel(Label36, My.Settings.SC7A, My.Settings.SKSC7)
    End Sub

    ''' <summary>
    '''     単一の速度ラベルを更新
    ''' </summary>
    Private Sub UpdateSpeedLabel(label As Label, modifier As Integer, keyCode As Integer)
        label.Text = "(" & GetHotKeyDisplayText(modifier, keyCode) & ")"
    End Sub

#End Region

#Region "コンボボックス変更"

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim hotkeyType As HotKeyType = GetSelectedHotKeyType()
        Dim modifierProp As String = GetSettingModifierProperty(hotkeyType)
        Dim keyProp As String = GetSettingKeyProperty(hotkeyType)

        If String.IsNullOrEmpty(modifierProp) OrElse String.IsNullOrEmpty(keyProp) Then
            Return
        End If

        Dim modifier = CInt(CallByName(My.Settings, modifierProp, CallType.Get))
        Dim keyCode = CInt(CallByName(My.Settings, keyProp, CallType.Get))

        UpdateHotKeyDisplay(modifier, keyCode)
    End Sub

#End Region

#Region "ボタンイベント"

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' リセット処理（いつの間にかなくなってる・・・）
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' 現在のホットキー設定を一覧表示
        Dim lines As New System.Collections.Generic.List(Of String)
        lines.Add("現在のホットキー設定一覧")
        lines.Add("")

        For Each hotkeyType As HotKeyType In [Enum].GetValues(GetType(HotKeyType))
            Dim label As String = ComboBox1.Items(CInt(hotkeyType)).ToString()
            Dim modifier As Integer = CInt(CallByName(My.Settings, GetSettingModifierProperty(hotkeyType), CallType.Get))
            Dim key As Integer = CInt(CallByName(My.Settings, GetSettingKeyProperty(hotkeyType), CallType.Get))

            lines.Add(label & " : " & GetHotKeyDisplayText(modifier, key))
        Next

        MessageBox.Show(String.Join(Environment.NewLine, lines), "ホットキー設定一覧",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

#End Region

#Region "テスト用ヘルパー"

    Friend Sub SetCheckBoxStatesForTest(ctrl As Boolean, alt As Boolean, shift As Boolean)
        CheckBox2.Checked = ctrl
        CheckBox3.Checked = alt
        CheckBox4.Checked = shift
    End Sub

    Friend Sub SetComboBox2TextForTest(text As String)
        ComboBox2.Text = text
    End Sub

    Friend Sub SetComboBox1SelectedIndexForTest(index As Integer)
        ComboBox1.SelectedIndex = index
    End Sub

    Friend Function CalculateModifierValueForTest() As Integer
        Return CalculateModifierValue()
    End Function

    Friend Function GetSelectedKeyCodeForTest() As Integer
        Return GetSelectedKeyCode()
    End Function

    Friend Function GetKeyDisplayTextForTest(keyCode As Integer) As String
        Return GetKeyDisplayText(keyCode)
    End Function

    Friend Function GetSelectedHotKeyTypeForTest() As HotKeyType
        Return GetSelectedHotKeyType()
    End Function

    Friend Sub UpdateHotKeyDisplayForTest(modifier As Integer, keyCode As Integer)
        UpdateHotKeyDisplay(modifier, keyCode)
    End Sub

    Friend Sub Button1_ClickForTest()
        Button1_Click(Nothing, EventArgs.Empty)
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        '設定画面を閉じるときに保存するからいらないかも
        If RadioButton6.Checked = True Then
            My.Settings.AutoPlay = True
        Else
            My.Settings.AutoPlay = False
        End If
    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        '設定画面を閉じるときに保存するからいらないかも
        If RadioButton7.Checked = True Then
            My.Settings.AutoPlay = False
        Else
            My.Settings.AutoPlay = True
        End If
    End Sub

    Private Sub SettingsForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        My.Settings.AutoBack = NumericUpDown1.Value * 100

        ' コントローラーのジャンプボタン設定（NumericUpDown2～21）を SK1～SK20 へ保存
        For i As Integer = 2 To 21
            Dim ctl = Me.Controls.Find("NumericUpDown" & i, True).FirstOrDefault()
            If ctl IsNot Nothing Then
                CallByName(My.Settings, "SK" & (i - 1), CallType.Set, CInt(DirectCast(ctl, NumericUpDown).Value))
            End If
        Next

        ' コントローラーのジャンプボタン設定（NumericUpDown22～24）を MM1～MM3 へ保存
        For i As Integer = 1 To 3
            Dim ctl = Me.Controls.Find("NumericUpDown" & i + 21, True).FirstOrDefault()
            If ctl IsNot Nothing Then
                CallByName(My.Settings, "MM" & (i), CallType.Set, CInt(DirectCast(ctl, NumericUpDown).Value))
            End If
        Next
        ' コントローラーのジャンプボタン設定（NumericUpDown25～27）を HO1～HO3 へ保存
        For i As Integer = 1 To 3
            Dim ctl = Me.Controls.Find("NumericUpDown" & i + 24, True).FirstOrDefault()
            If ctl IsNot Nothing Then
                CallByName(My.Settings, "HO" & (i), CallType.Set, CInt(DirectCast(ctl, NumericUpDown).Value))
            End If
        Next

        ' コントローラーのスピードコントローラーボタン設定（NumericUpDown28～34）を SC1～SC7 へ保存
        For i As Integer = 1 To 7
            Dim ctl = Me.Controls.Find("NumericUpDown" & i + 27, True).FirstOrDefault()
            If ctl IsNot Nothing Then
                CallByName(My.Settings, "SC" & (i), CallType.Set, CInt(DirectCast(ctl, NumericUpDown).Value * 10))
            End If
        Next

        'タイムコード修飾11の設定を保存
        My.Settings.Atama = TextBox1.Text
        My.Settings.Oshiri = TextBox2.Text
        'タイムコード修飾2の設定を保存
        My.Settings.Atama2 = TextBox4.Text
        My.Settings.Oshiri2 = TextBox3.Text
        'タイムコード修飾3の設定を保存
        My.Settings.Atama3 = TextBox6.Text
        My.Settings.Oshiri3 = TextBox5.Text

        'Wordファイル読み込み時用の聞き取り付加部分のマークの設定を保存
        My.Settings.Fuka = TextBox7.Text
        'Wordファイル読み込み時用のその他部分のマークの設定を保存
        My.Settings.Sonota = TextBox9.Text
        'Wordファイル読み込み時用の不明部分（頭）のマークの設定を保存
        My.Settings.Fumei = TextBox8.Text
        'Wordファイル読み込み時用の不明部分（末尾）のマークの設定を保存
        My.Settings.Fumei2 = TextBox10.Text

        'ファイル読み込み時に一時停止するかそのまま再生するかどうかの設定を保存
        If RadioButton4.Checked = False Then
            My.Settings.ShioriPlay = False
        End If

        'しおり選択時に一時停止するかそのまま再生するかどうかの設定を保存
        If RadioButton6.Checked = False Then
            My.Settings.AutoPlay = False
        End If

        ' 設定を反映
        My.Settings.Save()
        MsgBox("AutoBack = " & My.Settings.AutoBack)
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged

    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged

    End Sub

#End Region
End Class

