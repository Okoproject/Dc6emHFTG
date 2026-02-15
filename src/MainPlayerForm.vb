Imports System.ComponentModel
Imports System.IO
Imports System.Text

''' <summary>
''' メイン動画プレイヤーフォーム
''' </summary>
Public Class MainPlayerForm

#Region "メンバー変数"

    Private _mediaPlayer As MpvPlayerWrapper
    Private _currentPlaybackSpeed As Double = 1.0

    ''' <summary>
    ''' メインフォームのインスタンス（シングルトン）
    ''' </summary>
    Public Shared Instance As MainPlayerForm

#End Region

#Region "フォームイベント"

    Private Sub MainPlayerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Instance = Me
        InitializeWindowPosition()
        InitializeMediaPlayer()
        InitializeHotKeys()
        LoadDefaultSettings()
        ApplyPanelHeights()
        ApplyUiSettings()
    End Sub

    Private Sub MainPlayerForm_Closing(sender As Object, e As CancelEventArgs) Handles MyBase.Closing
        SaveCurrentSettings()
        DisposeHotKeys()
        DisposeMediaPlayer()
    End Sub

#End Region

#Region "初期化処理"

    ''' <summary>
    ''' ウィンドウ位置の初期化
    ''' </summary>
    Private Sub InitializeWindowPosition()
        If Left < 50 Then
            Left = (Screen.PrimaryScreen.Bounds.Width - Width) \ 2
        End If
        If Top < 50 Then
            Top = (Screen.PrimaryScreen.Bounds.Height - Height) \ 2
        End If
    End Sub

    ''' <summary>
    ''' メディアプレイヤーの初期化
    ''' </summary>
    Private Sub InitializeMediaPlayer()
        AllowDrop = True

        _mediaPlayer = New MpvPlayerWrapper(MpvPanel)
        AddHandler _mediaPlayer.MediaChanged, AddressOf OnMediaChanged

        _mediaPlayer.Volume = My.Settings.Onryou
        TrackBar6.Value = _mediaPlayer.Volume
        Label5.Text = String.Format(My.Resources.VolumeFormat, _mediaPlayer.Volume)
    End Sub

    ''' <summary>
    ''' メディア変更時の処理
    ''' </summary>
    Private Sub OnMediaChanged()
        ' TrackBar1の最大値をメディアの長さに設定
        Dim dur As Double = _mediaPlayer.Duration
        If dur > 0 Then
            TrackBar1.Maximum = CInt(dur)
            Label1.Text = String.Format(My.Resources.TimeFormat, TimeSpan.FromSeconds(dur).ToString("hh\:mm\:ss"))
        End If
        TextBox1.Text = _mediaPlayer.FileName
        TrackBar2.Value = CInt(_mediaPlayer.Speed * 10)
        Label4.Text = String.Format(My.Resources.SpeedFormat, (TrackBar2.Value * 0.1).ToString("0.0"))
    End Sub

    ''' <summary>
    ''' ホットキーの初期化
    ''' </summary>
    Private Sub InitializeHotKeys()
        CreateHotKeyAtoms(Me.Handle)

        ' 各種ホットキーを登録
        RegisterAllHotKeys()
    End Sub

    ''' <summary>
    ''' 全ホットキーを登録
    ''' </summary>
    Private Sub RegisterAllHotKeys()
        For Each hotkeyType As HotKeyType In [Enum].GetValues(GetType(HotKeyType))
            Dim modifierProp As String = GetSettingModifierProperty(hotkeyType)
            Dim keyProp As String = GetSettingKeyProperty(hotkeyType)

            If String.IsNullOrEmpty(modifierProp) OrElse String.IsNullOrEmpty(keyProp) Then
                Continue For
            End If

            Dim modifierValue As Integer = CInt(CallByName(My.Settings, modifierProp, CallType.Get))
            Dim keyValue As Keys = CType(CallByName(My.Settings, keyProp, CallType.Get), Keys)

            RegisterSingleHotKey(hotkeyType, modifierValue, keyValue)
        Next
    End Sub

    ''' <summary>
    ''' 単一のホットキーを登録
    ''' </summary>
    Private Sub RegisterSingleHotKey(hotkeyType As HotKeyType, modifierSetting As Integer, key As Keys)
        Dim modifier As Integer = GetModifierValue(modifierSetting)
        Dim atomId As Short = HotKeyAtoms(hotkeyType)

        RegisterHotKey(Me.Handle, atomId, modifier, key)
    End Sub

    ''' <summary>
    ''' デフォルト設定の読み込み
    ''' </summary>
    Private Sub LoadDefaultSettings()
        If My.Settings.shokai = 1 Then
            Return
        End If

        ' ジャンプボタンの初期値設定
        InitializeJumpButtonSettings()

        ' 速度コントロールボタンの初期値設定
        InitializeSpeedButtonSettings()

        My.Settings.shokai = 1
    End Sub

    ''' <summary>
    ''' ジャンプボタン設定の初期化
    ''' </summary>
    Private Sub InitializeJumpButtonSettings()
        Dim jumpValues As Integer() = {1, 3, 5, 10, 15, 30, 60, 180, 300, 600, -1, -3, -5, -10, -15, -30, -60, -180, -300, -600}

        For i As Integer = 0 To jumpValues.Length - 1
            CallByName(My.Settings, $"SK{i + 1}", CallType.Set, jumpValues(i))
        Next
    End Sub

    ''' <summary>
    ''' 速度コントロールボタン設定の初期化
    ''' </summary>
    Private Sub InitializeSpeedButtonSettings()
        Dim speedValues As Double() = {5, 8, 10, 12, 13, 14, 15, 20}

        For i As Integer = 0 To speedValues.Length - 1
            CallByName(My.Settings, $"SC{i + 1}", CallType.Set, speedValues(i))
        Next
    End Sub

    ''' <summary>
    ''' パネル高さの適用
    ''' </summary>
    Private Sub ApplyPanelHeights()
        MpvPanel.Height = My.Settings.p21_height
    End Sub

    ''' <summary>
    ''' UI設定の復元
    ''' </summary>
    Private Sub ApplyUiSettings()
        ' フォームサイズの復元
        If My.Settings.MyClientSize.Width > 0 Then
            Me.ClientSize = My.Settings.MyClientSize
        End If

        ' 動画表示画面の復元
        ' gamen = False のとき動画画面を表示（CheckBox2.Checked = True）
        If My.Settings.gamen = False Then
            CheckBox2.Checked = True
        Else
            SplitContainer3.SplitterDistance = My.Settings.SC3_Distance
            CheckBox2.Checked = False
        End If

        ' しおりパネルの復元
        ' shiori = True のときしおりパネルを表示
        If My.Settings.shiori = True Then
            SplitContainer1.Panel2Collapsed = False
            ' SC1_Distanceが0の場合、デフォルト値を使用
            If My.Settings.SC1_Distance > 0 Then
                SplitContainer1.SplitterDistance = My.Settings.SC1_Distance
            Else
                SplitContainer1.SplitterDistance = SplitContainer1.Width - 125
            End If
        Else
            SplitContainer1.Panel2Collapsed = True
        End If

        ' プレイリストパネルの復元
        ' PL = True のときプレイリストを表示（CheckBox1.Checked = True）
        ' PL = False のときプレイリストを非表示（CheckBox1.Checked = False）
        If My.Settings.PL = True Then
            CheckBox1.Checked = True
            SplitContainer2.Panel1Collapsed = False
            If My.Settings.SC2_Distance > 0 Then
                SplitContainer2.SplitterDistance = My.Settings.SC2_Distance
            Else
                SplitContainer2.SplitterDistance = 149
            End If
        Else
            CheckBox1.Checked = False
            SplitContainer2.Panel1Collapsed = True
        End If
    End Sub

#End Region

#Region "終了処理"

    ''' <summary>
    ''' 現在の設定を保存
    ''' </summary>
    Private Sub SaveCurrentSettings()
        ' 再生情報の保存
        My.Settings.LastOpenedFile = _mediaPlayer.FilePath
        My.Settings.LastIchi = _mediaPlayer.Position

        ' UI状態の保存
        My.Settings.gamen = Not CheckBox2.Checked
        My.Settings.SC3_Distance = SplitContainer3.SplitterDistance
        My.Settings.shiori = Not SplitContainer1.Panel2Collapsed
        ' しおりが表示されている場合のみSplitterDistanceを保存
        If Not SplitContainer1.Panel2Collapsed Then
            My.Settings.SC1_Distance = SplitContainer1.SplitterDistance
        End If
        My.Settings.PL = CheckBox1.Checked
        ' プレイリストが表示されている場合のみSplitterDistanceを保存
        If Not SplitContainer2.Panel1Collapsed Then
            My.Settings.SC2_Distance = SplitContainer2.SplitterDistance
        End If
        My.Settings.MyClientSize = ClientSize
    End Sub

    ''' <summary>
    ''' ホットキーの解放
    ''' </summary>
    Private Sub DisposeHotKeys()
        HotKeyManager.DisposeHotKeys(Me.Handle)
    End Sub

    ''' <summary>
    ''' メディアプレイヤーの解放
    ''' </summary>
    Private Sub DisposeMediaPlayer()
        If _mediaPlayer IsNot Nothing Then
            _mediaPlayer.Dispose()
            _mediaPlayer = Nothing
        End If
    End Sub

#End Region

#Region "イベントハンドラ"

    ''' <summary>
    ''' ウィンドウプロシージャ（ホットキー処理用）
    ''' </summary>
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WmHotkey Then
            HandleHotKey(m.WParam.ToInt32())
        End If
        MyBase.WndProc(m)
    End Sub

    ''' <summary>
    ''' ホットキー処理
    ''' </summary>
    Private Sub HandleHotKey(hotkeyId As Integer)
        ' ホットキーIDに対応する処理を実行
        For Each kvp As KeyValuePair(Of HotKeyType, Short) In HotKeyAtoms
            If kvp.Value = hotkeyId Then
                ExecuteHotKeyAction(kvp.Key)
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    ''' ホットキーアクションの実行
    ''' </summary>
    Private Sub ExecuteHotKeyAction(hotkeyType As HotKeyType)
        Select Case hotkeyType
            Case HotKeyType.PlayPause
                TogglePlayPause()
            Case HotKeyType.PlayPauseWithCounterCopy1
                TogglePlayPauseWithCounterCopy(1)
            Case HotKeyType.PlayPauseWithCounterCopy2
                TogglePlayPauseWithCounterCopy(2)
            Case HotKeyType.PlayPauseWithCounterCopy3
                TogglePlayPauseWithCounterCopy(3)
            Case HotKeyType.StopPlayback
                StopPlayback()
            Case HotKeyType.CounterCopy1
                CopyCounterToClipboard(1)
            Case HotKeyType.CounterCopy2
                CopyCounterToClipboard(2)
            Case HotKeyType.CounterCopy3
                CopyCounterToClipboard(3)
            Case HotKeyType.AddBookmark
                AddBookmark()
            Case HotKeyType.PlayPauseWithBookmark
                TogglePlayPauseWithBookmark()
            Case HotKeyType.SpeedUp
                AdjustPlaybackSpeed(0.1)
            Case HotKeyType.SpeedDown
                AdjustPlaybackSpeed(-0.1)
            Case HotKeyType.SpeedResetTo1X
                SetPlaybackSpeed(1.0)
            Case HotKeyType.SpeedSetToHalf
                SetPlaybackSpeed(0.5)
            Case HotKeyType.SpeedSetToDouble
                SetPlaybackSpeed(2.0)
            Case HotKeyType.BringWindowToFront
                BringWindowToFront()
            Case HotKeyType.JumpHotkey1 To HotKeyType.JumpHotkey6
                ExecuteJumpHotkey(hotkeyType)
            Case HotKeyType.SpeedControlButton1 To HotKeyType.SpeedControlButton7
                ExecuteSpeedControlHotkey(hotkeyType)
            Case HotKeyType.ClipboardJump
                JumpToClipboardPosition()
        End Select
    End Sub

#End Region

#Region "再生制御"

    ''' <summary>
    ''' 再生/一時停止の切り替え
    ''' </summary>
    Private Sub TogglePlayPause()
        If _mediaPlayer.IsPlaying Then
            _mediaPlayer.Pause()
        Else
            _mediaPlayer.Play()
        End If
    End Sub

    ''' <summary>
    ''' 再生/一時停止とカウンタコピー
    ''' </summary>
    Private Sub TogglePlayPauseWithCounterCopy(counterIndex As Integer)
        CopyCounterToClipboard(counterIndex)
        TogglePlayPause()
    End Sub

    ''' <summary>
    ''' 停止
    ''' </summary>
    Private Sub StopPlayback()
        _mediaPlayer.Stop()
    End Sub

    ''' <summary>
    ''' カウンタをクリップボードにコピー
    ''' </summary>
    Private Sub CopyCounterToClipboard(counterIndex As Integer)
        ' カウンタコピー処理
        Dim timeCode As String = GetCurrentTimeCode()
        Dim prefix As String = GetTimeCodePrefix(counterIndex)
        Dim suffix As String = GetTimeCodeSuffix(counterIndex)

        Clipboard.SetText($"{prefix}{timeCode}{suffix}")
    End Sub

    ''' <summary>
    ''' 現在のタイムコードを取得
    ''' </summary>
    Private Function GetCurrentTimeCode() As String
        Dim position As Double = _mediaPlayer.Position
        Dim hours As Integer = CInt(position) \ 3600
        Dim minutes As Integer = (CInt(position) Mod 3600) \ 60
        Dim seconds As Integer = CInt(position) Mod 60
        Dim frames As Integer = CInt((position - Math.Floor(position)) * 30)

        Return $"{hours:D2}:{minutes:D2}:{seconds:D2}.{frames:D2}"
    End Function

    ''' <summary>
    ''' タイムコード修飾（頭）を取得
    ''' </summary>
    Private Function GetTimeCodePrefix(index As Integer) As String
        Select Case index
            Case 1 : Return My.Settings.Atama
            Case 2 : Return My.Settings.Atama2
            Case 3 : Return My.Settings.Atama3
            Case Else : Return String.Empty
        End Select
    End Function

    ''' <summary>
    ''' タイムコード修飾（末尾）を取得
    ''' </summary>
    Private Function GetTimeCodeSuffix(index As Integer) As String
        Select Case index
            Case 1 : Return My.Settings.Oshiri
            Case 2 : Return My.Settings.Oshiri2
            Case 3 : Return My.Settings.Oshiri3
            Case Else : Return String.Empty
        End Select
    End Function

    ''' <summary>
    ''' しおりに追加
    ''' </summary>
    Private Sub AddBookmark()
        ' しおり追加処理
    End Sub

    ''' <summary>
    ''' 再生/一時停止としおり追加
    ''' </summary>
    Private Sub TogglePlayPauseWithBookmark()
        AddBookmark()
        TogglePlayPause()
    End Sub

    ''' <summary>
    ''' 再生速度の調整
    ''' </summary>
    Private Sub AdjustPlaybackSpeed(delta As Double)
        _currentPlaybackSpeed = _mediaPlayer.Speed + delta
        SetPlaybackSpeed(_currentPlaybackSpeed)
    End Sub

    ''' <summary>
    ''' 再生速度を設定
    ''' </summary>
    Private Sub SetPlaybackSpeed(speed As Double)
        _currentPlaybackSpeed = speed
        _mediaPlayer.Speed = speed
        Label4.Text = $"x{speed:F1}"
    End Sub

    ''' <summary>
    ''' ウィンドウを最前面に
    ''' </summary>
    Private Sub BringWindowToFront()
        TopMost = Not TopMost
    End Sub

    ''' <summary>
    ''' ジャンプホットキーの実行
    ''' </summary>
    Private Sub ExecuteJumpHotkey(hotkeyType As HotKeyType)
        Dim jumpSeconds As Integer = 0

        Select Case hotkeyType
            Case HotKeyType.JumpHotkey1 : jumpSeconds = My.Settings.MM1
            Case HotKeyType.JumpHotkey2 : jumpSeconds = My.Settings.MM2
            Case HotKeyType.JumpHotkey3 : jumpSeconds = My.Settings.MM3
            Case HotKeyType.JumpHotkey4 : jumpSeconds = My.Settings.HO1
            Case HotKeyType.JumpHotkey5 : jumpSeconds = My.Settings.HO2
            Case HotKeyType.JumpHotkey6 : jumpSeconds = My.Settings.HO3
        End Select

        If jumpSeconds <> 0 Then
            JumpToPosition(_mediaPlayer.Position + jumpSeconds)
        End If
    End Sub

    ''' <summary>
    ''' 速度コントロールホットキーの実行
    ''' </summary>
    Private Sub ExecuteSpeedControlHotkey(hotkeyType As HotKeyType)
        Dim speed As Double = 1.0

        Select Case hotkeyType
            Case HotKeyType.SpeedControlButton1 : speed = My.Settings.SC1 / 10.0
            Case HotKeyType.SpeedControlButton2 : speed = My.Settings.SC2 / 10.0
            Case HotKeyType.SpeedControlButton3 : speed = My.Settings.SC3 / 10.0
            Case HotKeyType.SpeedControlButton4 : speed = My.Settings.SC4 / 10.0
            Case HotKeyType.SpeedControlButton5 : speed = My.Settings.SC5 / 10.0
            Case HotKeyType.SpeedControlButton6 : speed = My.Settings.SC6 / 10.0
            Case HotKeyType.SpeedControlButton7 : speed = My.Settings.SC7 / 10.0
        End Select

        SetPlaybackSpeed(speed)
    End Sub

    ''' <summary>
    ''' 指定位置にジャンプ
    ''' </summary>
    Private Sub JumpToPosition(position As Double)
        _mediaPlayer.Position = Math.Max(0, position)
    End Sub

    ''' <summary>
    ''' クリップボードの位置にジャンプ
    ''' </summary>
    Private Sub JumpToClipboardPosition()
        ' クリップボードから位置を読み込んでジャンプ
    End Sub

#End Region

#Region "UI制御"

    ''' <summary>
    ''' 動画表示画面の表示/非表示切り替え
    ''' </summary>
    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        SplitContainer3.Panel1Collapsed = Not CheckBox2.Checked
    End Sub

    ''' <summary>
    ''' プレイリストの表示/非表示切り替え
    ''' </summary>
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        SplitContainer2.Panel1Collapsed = Not CheckBox1.Checked
    End Sub

#End Region

#Region "ボタンイベントハンドラ"

    ' 速度ボタン
    Private Sub SpeedButtons_Click(sender As Object, e As EventArgs) Handles Button21.Click, Button22.Click, Button23.Click, Button24.Click, Button25.Click, Button26.Click, Button27.Click
        Dim btn = TryCast(sender, Button)
        If btn Is Nothing Then Return

        ' ボタン名から番号を取得（例: "Button21" -> "1"）
        ' Button21 は SC1, Button22 は SC2 ... なので 20 を引く
        Dim buttonIndex As Integer
        If Integer.TryParse(btn.Name.Replace("Button", ""), buttonIndex) Then
            Dim scIndex = buttonIndex - 20
            Dim settingName = "SC" & scIndex

            Try
                TrackBar2.Value = CInt(My.Settings(settingName)) \ 10
                UpdateSpeedFromTrackBar()
            Catch ex As Exception
                ' 設定が見つからない場合などは何もしない
            End Try
        End If
    End Sub

    Private Sub UpdateSpeedFromTrackBar()
        Label4.Text = String.Format(My.Resources.SpeedFormat, (TrackBar2.Value * 0.1).ToString("0.0"))
        _mediaPlayer.Speed = TrackBar2.Value * 0.1
    End Sub

    Private Sub TrackBar2_Scroll(sender As Object, e As EventArgs) Handles TrackBar2.Scroll
        UpdateSpeedFromTrackBar()
    End Sub

    Private Sub Button400_Click(sender As Object, e As EventArgs) Handles Button400.Click
        _mediaPlayer.Pause()
        _mediaPlayer.Position = 0
    End Sub

    Private Sub Button200_Click(sender As Object, e As EventArgs) Handles Button200.Click
        If _mediaPlayer.IsPlaying Then
            _mediaPlayer.Pause()
        Else
            _mediaPlayer.Play()
        End If
    End Sub

    ' ジャンプボタン
    Private Sub JumpButtons_Click(sender As Object, e As EventArgs) Handles Button1.Click, Button11.Click, Button2.Click, Button12.Click, Button3.Click, Button13.Click, Button4.Click, Button14.Click, Button5.Click, Button15.Click, Button6.Click, Button16.Click, Button7.Click, Button17.Click, Button8.Click, Button18.Click, Button9.Click, Button19.Click, Button10.Click, Button20.Click
        Dim btn = TryCast(sender, Button)
        If btn Is Nothing Then Return

        ' ボタン名から番号を取得（例: "Button1" -> "1"）
        Dim buttonNumber = btn.Name.Replace("Button", "")
        ' 対応する設定名（例: "SK1"）
        Dim settingName = "SK" & buttonNumber

        ' 設定値を取得して移動
        Try
            Dim jumpValue = CInt(My.Settings(settingName))
            _mediaPlayer.Position = TrackBar1.Value + jumpValue
        Catch ex As Exception
            ' 設定が見つからない場合などは何もしない
        End Try
    End Sub

    Private Sub TrackBar6_Scroll(sender As Object, e As EventArgs) Handles TrackBar6.Scroll
        _mediaPlayer.Volume = TrackBar6.Value
        My.Settings.Onryou = TrackBar6.Value
        Label5.Text = String.Format(My.Resources.VolumeFormat, TrackBar6.Value)
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        DataGridView1.Rows.Add()
        Dim i As Integer = DataGridView1.Rows.Count - 1
        Dim appPath As String = System.Reflection.Assembly.GetExecutingAssembly().Location

        If String.IsNullOrEmpty(My.Settings.autoBMDir) Then
            My.Settings.autoBMDir = IO.Path.GetDirectoryName(appPath)
        End If

        DataGridView1.Rows(i).Cells(0).Value = Strings.Left(Label1.Text, 8)
        DataGridView1.Rows(i).Cells(2).Value = TrackBar1.Value
        DataGridView1.CurrentCell = DataGridView1(0, i)

        If My.Settings.autoBM Then
            WriteCsvFromDgv(TextBox1.Text)
        Else
            WriteCsvFromDgv("om_tmp")
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim colIndex As Integer = e.ColumnIndex

        ' 行インデックスが有効か確認
        If e.RowIndex < 0 OrElse e.RowIndex >= DataGridView1.Rows.Count Then
            Exit Sub
        End If

        If colIndex <> 0 And colIndex <> 3 Then
            Exit Sub
        End If

        Select Case colIndex
            Case 0 ' ジャンプ
                Dim i As Integer = e.RowIndex
                TrackBar1.Value = DataGridView1.Rows(i).Cells(2).Value
                _mediaPlayer.Position = TrackBar1.Value
                If My.Settings.shiori_PS Then
                    _mediaPlayer.Play()
                Else
                    _mediaPlayer.Play()
                    _mediaPlayer.Pause()
                End If
            Case 3 ' 削除
                Dim i As Integer = e.RowIndex
                DataGridView1.Rows.RemoveAt(i)
        End Select
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        If DataGridView1.RowCount = 0 OrElse DataGridView1.SelectedCells.Count = 0 Then Exit Sub
        Dim i As Integer = DataGridView1.SelectedCells(0).RowIndex
        DataGridView1.Rows.RemoveAt(i)
    End Sub

    Private Function ParseCounterToSeconds(inputText As String, ByRef resultSeconds As Integer, ByRef formattedCounter As String) As Boolean
        Dim cCounter As String = String.Empty

        ' 数字のみ抽出
        For Each ch As Char In inputText
            If Char.IsDigit(ch) Then
                cCounter &= ch
            End If
        Next

        If cCounter.Length > 6 Then
            MsgBox(My.Resources.DigitsExceeded, vbOKOnly)
            Return False
        End If

        ' 6桁にパディング
        cCounter = cCounter.PadLeft(6, "0"c)

        Dim hours = Integer.Parse(cCounter.Substring(0, 2))
        Dim minutes = Integer.Parse(cCounter.Substring(2, 2))
        Dim seconds = Integer.Parse(cCounter.Substring(4, 2))

        resultSeconds = (hours * 3600) + (minutes * 60) + seconds
        formattedCounter = String.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds)

        If resultSeconds > _mediaPlayer.Duration Then
            MsgBox(My.Resources.CounterExceedsDuration)
            Return False
        End If

        Return True
    End Function

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        If String.IsNullOrEmpty(TextBox3.Text) Then Exit Sub

        Dim seconds As Integer
        Dim formattedCounter As String = String.Empty

        If ParseCounterToSeconds(TextBox3.Text, seconds, formattedCounter) Then
            DataGridView1.Rows.Add()
            Dim i As Integer = DataGridView1.Rows.Count - 1
            DataGridView1.Rows(i).Cells(0).Value = formattedCounter
            DataGridView1.Rows(i).Cells(2).Value = seconds
            DataGridView1.CurrentCell = DataGridView1(0, i)
        Else
            TextBox3.Clear()
        End If
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        If String.IsNullOrEmpty(TextBox2.Text) Then Exit Sub

        Dim seconds As Integer
        Dim formattedCounter As String = String.Empty

        If ParseCounterToSeconds(TextBox2.Text, seconds, formattedCounter) Then
            TrackBar1.Value = seconds
            _mediaPlayer.Position = TrackBar1.Value
            _mediaPlayer.Play()
            TextBox2.Clear()
        Else
            TextBox2.Clear()
        End If
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        Try
            If WriteCsvFromDgv(TextBox1.Text) = True Then
                MsgBox(My.Resources.ExportComplete, vbOKOnly)
            Else
                MsgBox(My.Resources.ExportFailed, vbOKOnly)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbOKOnly)
        End Try
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        Dim nFile As String = Nothing
        Dim strMemo As String
        Dim n As Integer
        Dim iLine As String
        Dim cCounter As String

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            nFile = OpenFileDialog1.FileName
            Select Case Path.GetExtension(nFile).ToLower()
                Case ".csv"
                    CsvReader()
                Case ".doc", ".docx"
                    Dim objWord As Object
                    Dim objDoc As Object
                    Dim txtNakami As String

                    Try
                        objWord = CreateObject("Word.Application")
                        objWord.Visible = False
                        objDoc = objWord.Documents.Open(OpenFileDialog1.FileName)

                        objDoc.Range.Copy()
                        txtNakami = Clipboard.GetText()
                        For n = 0 To txtNakami.Length - 1
                            Select Case txtNakami.Substring(n, 1)
                                Case My.Settings.Fuka
                                    cCounter = txtNakami.Substring(n + 1, 10)
                                    cCounter = (Integer.Parse(cCounter.Substring(1, 2)) * 3600) + (Integer.Parse(cCounter.Substring(4, 2)) * 60) + (Integer.Parse(cCounter.Substring(7, 2)))
                                    iLine = txtNakami.Substring(n + 2, 8) & "," & "聞き取り不可" & "," & cCounter & "," & "削除"
                                    Dim rowPlus1() As String = iLine.Split(",")
                                    DataGridView1.Rows.Add(rowPlus1)
                                Case My.Settings.Fumei
                                    For i = n + 1 To txtNakami.Length - 1
                                        If txtNakami.Substring(i, 1) = My.Settings.Fumei2 Then
                                            strMemo = txtNakami.Substring(n + 1, i - n - 1)
                                            cCounter = txtNakami.Substring(i + 1, 10)
                                            cCounter = (Integer.Parse(cCounter.Substring(1, 2)) * 3600) + (Integer.Parse(cCounter.Substring(4, 2)) * 60) + (Integer.Parse(cCounter.Substring(7, 2)))
                                            iLine = txtNakami.Substring(i + 2, 8) & "," & strMemo & "？," & cCounter & "," & "削除"
                                            Dim rowPlus2() As String = iLine.Split(",")
                                            DataGridView1.Rows.Add(rowPlus2)
                                            Exit For
                                        End If
                                    Next i
                                Case My.Settings.Sonota
                                    For i = n + 1 To txtNakami.Length - 1
                                        If txtNakami.Substring(i, 1) = "(" OrElse txtNakami.Substring(i, 1) = "（" Then
                                            strMemo = txtNakami.Substring(n, i - n)
                                            cCounter = txtNakami.Substring(i, 10)
                                            cCounter = (Integer.Parse(cCounter.Substring(1, 2)) * 3600) + (Integer.Parse(cCounter.Substring(4, 2)) * 60) + (Integer.Parse(cCounter.Substring(7, 2)))
                                            iLine = txtNakami.Substring(i + 1, 8) & "," & strMemo & "," & cCounter & "," & "削除"
                                            Dim rowPlus3() As String = iLine.Split(",")
                                            DataGridView1.Rows.Add(rowPlus3)
                                            Exit For
                                        End If
                                    Next i
                            End Select
                        Next n

                        objDoc = Nothing
                        objWord = Nothing
                    Catch ex As Exception
                        MsgBox(String.Format(My.Resources.WordFileLoadFailed, ex.Message), vbOKOnly)
                    End Try
                Case ".txt"
                    Using reader = New StreamReader(nFile, Encoding.GetEncoding("Shift_JIS"))
                        Dim txtNakami As String = reader.ReadToEnd()
                        For n = 0 To txtNakami.Length - 1
                            Select Case txtNakami.Substring(n, 1)
                                Case My.Settings.Fuka
                                    cCounter = txtNakami.Substring(n + 1, 10)
                                    cCounter = (Integer.Parse(cCounter.Substring(1, 2)) * 3600) + (Integer.Parse(cCounter.Substring(4, 2)) * 60) + (Integer.Parse(cCounter.Substring(7, 2)))
                                    iLine = txtNakami.Substring(n + 2, 8) & "," & "聞き取り不可" & "," & cCounter & "," & "削除"
                                    Dim rowPlus4() As String = iLine.Split(",")
                                    DataGridView1.Rows.Add(rowPlus4)
                                Case My.Settings.Fumei
                                    For i = n + 1 To txtNakami.Length - 1
                                        If txtNakami.Substring(i, 1) = My.Settings.Fumei2 Then
                                            strMemo = txtNakami.Substring(n + 1, i - n - 1)
                                            cCounter = txtNakami.Substring(i + 1, 10)
                                            cCounter = (Integer.Parse(cCounter.Substring(1, 2)) * 3600) + (Integer.Parse(cCounter.Substring(4, 2)) * 60) + (Integer.Parse(cCounter.Substring(7, 2)))
                                            iLine = txtNakami.Substring(i + 2, 8) & "," & strMemo & "？," & cCounter & "," & "削除"
                                            Dim rowPlus5() As String = iLine.Split(",")
                                            DataGridView1.Rows.Add(rowPlus5)
                                            Exit For
                                        End If
                                    Next i
                                Case My.Settings.Sonota
                                    For i = n + 1 To txtNakami.Length - 1
                                        If txtNakami.Substring(i, 1) = "(" OrElse txtNakami.Substring(i, 1) = "（" Then
                                            strMemo = txtNakami.Substring(n, i - n)
                                            cCounter = txtNakami.Substring(i, 10)
                                            cCounter = (Integer.Parse(cCounter.Substring(1, 2)) * 3600) + (Integer.Parse(cCounter.Substring(4, 2)) * 60) + (Integer.Parse(cCounter.Substring(7, 2)))
                                            iLine = txtNakami.Substring(i + 1, 8) & "," & strMemo & "," & cCounter & "," & "削除"
                                            Dim rowPlus6() As String = iLine.Split(",")
                                            DataGridView1.Rows.Add(rowPlus6)
                                            Exit For
                                        End If
                                    Next i
                            End Select
                        Next n
                    End Using
                Case Else
                    MsgBox(My.Resources.FileFormatNotSupported, vbOKOnly)
                    Exit Sub
            End Select
        End If
    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        MsgBox(My.Resources.ScreenCaptureNotImplemented, vbOKOnly)
    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        Dim f2 As New SettingsForm()
        f2.ShowDialog()
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        ToolTip1.SetToolTip(TrackBar1, TimeSpan.FromSeconds(TrackBar1.Value).ToString("hh\:mm\:ss"))
        Label1.Text = TimeSpan.FromSeconds(TrackBar1.Value).ToString("hh\:mm\:ss") & My.Resources.TimeSeparator & TimeSpan.FromSeconds(_mediaPlayer.Duration).ToString("hh\:mm\:ss")
        _mediaPlayer.Position = TrackBar1.Value
    End Sub

    Private Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            _mediaPlayer.LoadFile(OpenFileDialog1.FileName)
            My.Settings.LastOpenedFile = OpenFileDialog1.FileName
            DataGridView1.Rows.Clear()
        End If
    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        TopMost = Not TopMost
        If TopMost Then
            Button35.Image = My.Resources.AlwaysVisible_16x
        Else
            Button35.Image = My.Resources.PinnedItem_16x
        End If
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        If SplitContainer1.Panel2Collapsed Then
            SplitContainer1.Panel2Collapsed = False
        Else
            SplitContainer1.Panel2Collapsed = True
        End If
    End Sub

#End Region

#Region "Timer"

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If _mediaPlayer.IsPlaying Then
            Dim pos As Integer = CInt(_mediaPlayer.Position)
            If pos > TrackBar1.Maximum Then
                pos = TrackBar1.Maximum
            End If
            TrackBar1.Value = pos
            Label1.Text = TimeSpan.FromSeconds(_mediaPlayer.Position).ToString("hh\:mm\:ss") & My.Resources.TimeSeparator & TimeSpan.FromSeconds(_mediaPlayer.Duration).ToString("hh\:mm\:ss")
        End If
    End Sub

#End Region

#Region "ドラッグ＆ドロップ"

    Private Const CtrlMask As Integer = 8

    Private Sub MainPlayerForm_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            If files.Length > 0 Then
                _mediaPlayer.LoadFile(files(0))
                My.Settings.LastOpenedFile = files(0)
            End If
        End If
    End Sub

    Private Sub MainPlayerForm_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            If (e.KeyState And CtrlMask) = CtrlMask Then
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.Move
            End If
        End If
    End Sub

    Private Sub TableLayoutPanel1_DragDrop(sender As Object, e As DragEventArgs) Handles TableLayoutPanel1.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            If files.Length > 0 Then
                _mediaPlayer.LoadFile(files(0))
                My.Settings.LastOpenedFile = files(0)
            End If
        End If
    End Sub

    Private Sub TableLayoutPanel1_DragEnter(sender As Object, e As DragEventArgs) Handles TableLayoutPanel1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            If (e.KeyState And CtrlMask) = CtrlMask Then
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.Move
            End If
        End If
    End Sub

    Private Sub TrackBar1_DragDrop(sender As Object, e As DragEventArgs) Handles TrackBar1.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            If files.Length > 0 Then
                _mediaPlayer.LoadFile(files(0))
                My.Settings.LastOpenedFile = files(0)
            End If
        End If
    End Sub

    Private Sub TrackBar1_DragEnter(sender As Object, e As DragEventArgs) Handles TrackBar1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            If (e.KeyState And CtrlMask) = CtrlMask Then
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.Move
            End If
        End If
    End Sub

    Private Sub SplitContainer2_DragDrop(sender As Object, e As DragEventArgs) Handles SplitContainer2.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            If files.Length > 0 Then
_mediaPlayer.LoadFile(files(0))

                My.Settings.LastOpenedFile = files(0)
            End If
        End If
    End Sub

    Private Sub SplitContainer2_DragEnter(sender As Object, e As DragEventArgs) Handles SplitContainer2.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            If (e.KeyState And CtrlMask) = CtrlMask Then
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.Move
            End If
        End If
    End Sub

#End Region

#Region "CSV入出力"

    ''' <summary>
    ''' CSVファイル読み込み
    ''' </summary>
    Private Sub CsvReader()
        Dim csvFile As String = OpenFileDialog1.FileName

        DataGridView1.Rows.Clear()

        Dim sr As New StreamReader(csvFile, System.Text.Encoding.GetEncoding("shift_jis"))
        Dim conStr As String

        conStr = sr.ReadLine()
        If conStr Is Nothing Then Exit Sub

        Do
            conStr = sr.ReadLine()
            If conStr Is Nothing Then Exit Do
            conStr = Replace(conStr, """", "")
            Dim rowPlus() As String = conStr.Split(",")
            DataGridView1.Rows.Add(rowPlus)
        Loop

        sr.Close()
    End Sub

    ''' <summary>
    ''' DataGridViewからCSVファイルへの書込処理
    ''' </summary>
    Private Function WriteCsvFromDgv(ByVal fileName As String) As Boolean
        Try
            Dim arrData()() As String = Nothing
            Dim arrHead As String() = Nothing
            Dim filePath As String

            ' ファイルパスの構築
            If fileName.Contains("\") OrElse fileName.Contains("/") Then
                ' フルパスまたは相対パスが指定された場合
                filePath = fileName
            Else
                ' ファイル名のみの場合、autoBMDirを使用
                If String.IsNullOrEmpty(My.Settings.autoBMDir) Then
                    filePath = Application.StartupPath & "\" & fileName & ".csv"
                Else
                    filePath = My.Settings.autoBMDir & "\" & fileName & ".csv"
                End If
            End If

            For col As Integer = 0 To DataGridView1.Columns.Count - 1
                ReDim Preserve arrHead(col)
                arrHead(col) = CStr(DataGridView1.Columns(col).HeaderCell.Value)
            Next
            ReDim Preserve arrData(0)
            arrData(0) = arrHead

            For row As Integer = 0 To DataGridView1.Rows.Count - 1
                If DataGridView1.Rows(row).IsNewRow Then
                    Continue For
                End If

                Dim arrLine As String() = Nothing
                For col As Integer = 0 To DataGridView1.Columns.Count - 1
                    ReDim Preserve arrLine(col)
                    arrLine(col) = CStr(DataGridView1.Rows(row).Cells(col).Value)
                Next

                ReDim Preserve arrData(row + 1)
                arrData(row + 1) = arrLine
            Next

            Return WriteCsv(filePath, arrData)

        Catch ex As Exception
            MsgBox(ex.Message, vbOKOnly)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' CSVファイルの書込処理
    ''' </summary>
    Private Function WriteCsv(ByVal csvPath As String, ByVal csvData As String()()) As Boolean
        Dim sw As System.IO.StreamWriter = Nothing

        Try
            Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
            sw = New System.IO.StreamWriter(csvPath, False, enc)

            For Each arrLine() As String In csvData
                Dim isFirst As Boolean = True
                For Each str As String In arrLine
                    If Not isFirst Then
                        sw.Write(",")
                    End If
                    isFirst = False
                    sw.Write("""" & str & """")
                Next
                sw.Write(vbCrLf)
            Next

            Return True

        Catch ex As Exception
            MsgBox(ex.Message, vbOKOnly)
            Return False
        Finally
            If sw IsNot Nothing Then
                sw.Close()
            End If
        End Try
    End Function

#End Region

#Region "その他イベントハンドラ"

    ''' <summary>
    ''' TextBox2でEnterキーが押されたときにジャンプ
    ''' </summary>
    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim n As Integer
            Dim a As Integer
            Dim cCounter As String = String.Empty

            If String.IsNullOrEmpty(TextBox2.Text) Then Exit Sub

            n = TextBox2.Text.Length

            For a = 1 To n
                Dim ch As String = Strings.Mid(TextBox2.Text, a, 1)
                If ch = "0" OrElse ch = "1" OrElse ch = "2" OrElse ch = "3" OrElse ch = "4" OrElse ch = "5" OrElse ch = "6" OrElse ch = "7" OrElse ch = "8" OrElse ch = "9" Then
                    cCounter &= ch
                End If
            Next a

            If cCounter.Length > 6 OrElse cCounter.Length < 0 Then
                MsgBox(My.Resources.InvalidDigits, vbOKOnly)
                TextBox2.Clear()
                Exit Sub
            End If

            Select Case cCounter.Length
                Case 1 : cCounter = "00000" & cCounter
                Case 2 : cCounter = "0000" & cCounter
                Case 3 : cCounter = "000" & cCounter
                Case 4 : cCounter = "00" & cCounter
                Case 5 : cCounter = "0" & cCounter
            End Select

            If (Integer.Parse(Strings.Mid(cCounter, 1, 2)) * 3600) + (Integer.Parse(Strings.Mid(cCounter, 3, 2)) * 60) + (Integer.Parse(Strings.Mid(cCounter, 5, 2))) > _mediaPlayer.Duration Then
                MsgBox(My.Resources.CounterExceedsDuration, vbOKOnly)
                TextBox2.Clear()
                Exit Sub
            End If

            TrackBar1.Value = (Integer.Parse(Strings.Mid(cCounter, 1, 2)) * 3600) + (Integer.Parse(Strings.Mid(cCounter, 3, 2)) * 60) + (Integer.Parse(Strings.Mid(cCounter, 5, 2)))
            _mediaPlayer.Position = TrackBar1.Value
            _mediaPlayer.Play()
            TextBox2.Clear()
        End If
    End Sub

    ''' <summary>
    ''' SplitContainer1のスプリッター移動時
    ''' </summary>
    Private Sub SplitContainer1_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer1.SplitterMoved
        My.Settings.p11_height = SplitContainer1.Panel1.Height
        My.Settings.p11_width = SplitContainer1.Panel2.Width
        My.Settings.p12_height = SplitContainer1.Panel2.Height
        My.Settings.p12_width = SplitContainer1.Panel2.Width
        My.Settings.p21_height = SplitContainer3.Panel1.Height
        My.Settings.p21_width = SplitContainer3.Panel1.Width
        My.Settings.p22_height = SplitContainer3.Panel2.Height
        My.Settings.p22_width = SplitContainer3.Panel2.Width
    End Sub

    ''' <summary>
    ''' SplitContainer3のスプリッター移動時
    ''' </summary>
    Private Sub SplitContainer3_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer3.SplitterMoved
        My.Settings.p11_height = SplitContainer1.Panel1.Height
        My.Settings.p11_width = SplitContainer1.Panel2.Width
        My.Settings.p12_height = SplitContainer1.Panel2.Height
        My.Settings.p12_width = SplitContainer1.Panel2.Width
        My.Settings.p21_height = SplitContainer3.Panel1.Height
        My.Settings.p21_width = SplitContainer3.Panel1.Width
        My.Settings.p22_height = SplitContainer3.Panel2.Height
        My.Settings.p22_width = SplitContainer3.Panel2.Width
    End Sub

    ''' <summary>
    ''' TableLayoutPanel1のセル描画時（着色）
    ''' </summary>
    Private Sub TableLayoutPanel1_CellPaint(sender As Object, e As TableLayoutCellPaintEventArgs) Handles TableLayoutPanel1.CellPaint
        Dim darkBrush2 As New SolidBrush(Color.FromArgb(50, 50, 50))

        ' Row 0
        For col As Integer = 0 To 16
            If e.Column = col AndAlso e.Row = 0 Then
                e.Graphics.FillRectangle(darkBrush2, e.CellBounds)
            End If
        Next

        ' Row 1
        For col As Integer = 0 To 20
            If e.Column = col AndAlso e.Row = 1 Then
                e.Graphics.FillRectangle(darkBrush2, e.CellBounds)
            End If
        Next

        ' 音量調整部分
        Dim darkBrush As New SolidBrush(Color.FromArgb(64, 64, 64))
        If e.Column = 20 AndAlso (e.Row = 3 OrElse e.Row = 4 OrElse e.Row = 5) Then
            e.Graphics.FillRectangle(darkBrush, e.CellBounds)
        End If

        ' 再生・停止部分
        For col As Integer = 0 To 5
            If e.Column = col AndAlso (e.Row = 5 OrElse e.Row = 6) Then
                e.Graphics.FillRectangle(darkBrush, e.CellBounds)
            End If
        Next

        ' 速度調整部分
        If (e.Column = 6 OrElse e.Column >= 8 AndAlso e.Column <= 18) AndAlso e.Row = 5 Then
            e.Graphics.FillRectangle(darkBrush2, e.CellBounds)
        End If
    End Sub

#End Region

#Region "テスト用ヘルパー"

    ''' <summary>
    ''' テスト用にメディアプレイヤーを設定
    ''' </summary>
    Friend Sub SetMediaPlayerForTest(player As MpvPlayerWrapper)
        _mediaPlayer = player
    End Sub

    ''' <summary>
    ''' テスト用にメディアプレイヤーを取得
    ''' </summary>
    Friend Function GetMediaPlayerForTest() As MpvPlayerWrapper
        Return _mediaPlayer
    End Function

#End Region

End Class
