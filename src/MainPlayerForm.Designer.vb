<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainPlayerForm
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If _mediaPlayer IsNot Nothing Then
                    _mediaPlayer.Dispose()
                End If
                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainPlayerForm))
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.File_Namae = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.File_Length = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.File_Memo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.File_Delete = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.File_Position = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.File_Progress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button41 = New System.Windows.Forms.Button()
        Me.Button42 = New System.Windows.Forms.Button()
        Me.Button43 = New System.Windows.Forms.Button()
        Me.Button44 = New System.Windows.Forms.Button()
        Me.Button45 = New System.Windows.Forms.Button()
        Me.Button46 = New System.Windows.Forms.Button()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.MpvPanel = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.CheckBoxMpvPamel = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button33 = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.Button13 = New System.Windows.Forms.Button()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.Button15 = New System.Windows.Forms.Button()
        Me.Button16 = New System.Windows.Forms.Button()
        Me.Button17 = New System.Windows.Forms.Button()
        Me.Button18 = New System.Windows.Forms.Button()
        Me.Button19 = New System.Windows.Forms.Button()
        Me.Button20 = New System.Windows.Forms.Button()
        Me.TrackBar2 = New System.Windows.Forms.TrackBar()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button21 = New System.Windows.Forms.Button()
        Me.Button22 = New System.Windows.Forms.Button()
        Me.Button23 = New System.Windows.Forms.Button()
        Me.Button24 = New System.Windows.Forms.Button()
        Me.Button25 = New System.Windows.Forms.Button()
        Me.Button26 = New System.Windows.Forms.Button()
        Me.Button27 = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button39 = New System.Windows.Forms.Button()
        Me.Button37 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TrackBar6 = New System.Windows.Forms.TrackBar()
        Me.Button36 = New System.Windows.Forms.Button()
        Me.Button200 = New System.Windows.Forms.Button()
        Me.Button400 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ButtonShiori = New System.Windows.Forms.Button()
        Me.Button35 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.Button30 = New System.Windows.Forms.Button()
        Me.Button38 = New System.Windows.Forms.Button()
        Me.Button40 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.colCounter = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.colMemo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPosition = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDelete = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Button28 = New System.Windows.Forms.Button()
        Me.Button29 = New System.Windows.Forms.Button()
        Me.Button32 = New System.Windows.Forms.Button()
        Me.Button31 = New System.Windows.Forms.Button()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Button34 = New System.Windows.Forms.Button()
        Me.CustomTitleBar = New System.Windows.Forms.Panel()
        Me.BtnMinimize = New System.Windows.Forms.Button()
        Me.BtnMaximize = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.LblTitle = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CustomTitleBar.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 56)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(1)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1MinSize = 0
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.SplitContainer1.Panel2.Controls.Add(Me.TableLayoutPanel2)
        Me.SplitContainer1.Panel2MinSize = 0
        Me.SplitContainer1.Size = New System.Drawing.Size(1599, 779)
        Me.SplitContainer1.SplitterDistance = 1279
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(1)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.TableLayoutPanel3)
        Me.SplitContainer2.Panel1MinSize = 0
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Panel2MinSize = 0
        Me.SplitContainer2.Size = New System.Drawing.Size(1279, 779)
        Me.SplitContainer2.SplitterDistance = 276
        Me.SplitContainer2.SplitterWidth = 5
        Me.SplitContainer2.TabIndex = 0
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 6
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.67!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.67!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.67!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.67!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.67!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.65!))
        Me.TableLayoutPanel3.Controls.Add(Me.DataGridView2, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Button41, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Button42, 1, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Button43, 2, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Button44, 3, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Button45, 4, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Button46, 5, 2)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 3
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(276, 779)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowDrop = True
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToOrderColumns = True
        Me.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.File_Namae, Me.File_Length, Me.File_Memo, Me.File_Delete, Me.File_Position, Me.File_Progress})
        Me.TableLayoutPanel3.SetColumnSpan(Me.DataGridView2, 6)
        Me.DataGridView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView2.Location = New System.Drawing.Point(2, 34)
        Me.DataGridView2.Margin = New System.Windows.Forms.Padding(2)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowHeadersWidth = 51
        Me.DataGridView2.RowTemplate.Height = 24
        Me.DataGridView2.Size = New System.Drawing.Size(272, 703)
        Me.DataGridView2.TabIndex = 6
        '
        'File_Namae
        '
        Me.File_Namae.HeaderText = "ファイル名"
        Me.File_Namae.MinimumWidth = 50
        Me.File_Namae.Name = "File_Namae"
        '
        'File_Length
        '
        Me.File_Length.HeaderText = "長さ"
        Me.File_Length.MinimumWidth = 50
        Me.File_Length.Name = "File_Length"
        '
        'File_Memo
        '
        Me.File_Memo.HeaderText = "メモ"
        Me.File_Memo.MinimumWidth = 50
        Me.File_Memo.Name = "File_Memo"
        '
        'File_Delete
        '
        Me.File_Delete.HeaderText = "削除"
        Me.File_Delete.MinimumWidth = 40
        Me.File_Delete.Name = "File_Delete"
        '
        'File_Position
        '
        Me.File_Position.HeaderText = "Position"
        Me.File_Position.MinimumWidth = 50
        Me.File_Position.Name = "File_Position"
        '
        'File_Progress
        '
        Me.File_Progress.HeaderText = "進捗"
        Me.File_Progress.MinimumWidth = 50
        Me.File_Progress.Name = "File_Progress"
        '
        'Button41
        '
        Me.Button41.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button41.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button41.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button41.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button41.Image = Global.OkoshiMAX.My.Resources.Resources.FolderClosed_16x
        Me.Button41.Location = New System.Drawing.Point(1, 740)
        Me.Button41.Margin = New System.Windows.Forms.Padding(1)
        Me.Button41.Name = "Button41"
        Me.Button41.Size = New System.Drawing.Size(44, 38)
        Me.Button41.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.Button41, "フォルダを開く")
        Me.Button41.UseVisualStyleBackColor = False
        '
        'Button42
        '
        Me.Button42.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button42.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button42.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button42.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button42.Image = Global.OkoshiMAX.My.Resources.Resources.GlyphRight_16x
        Me.Button42.Location = New System.Drawing.Point(47, 740)
        Me.Button42.Margin = New System.Windows.Forms.Padding(1)
        Me.Button42.Name = "Button42"
        Me.Button42.Size = New System.Drawing.Size(44, 38)
        Me.Button42.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.Button42, "次へ")
        Me.Button42.UseVisualStyleBackColor = False
        '
        'Button43
        '
        Me.Button43.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button43.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button43.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button43.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button43.Image = Global.OkoshiMAX.My.Resources.Resources.Cancel_16x
        Me.Button43.Location = New System.Drawing.Point(93, 740)
        Me.Button43.Margin = New System.Windows.Forms.Padding(1)
        Me.Button43.Name = "Button43"
        Me.Button43.Size = New System.Drawing.Size(44, 38)
        Me.Button43.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.Button43, "一覧から削除")
        Me.Button43.UseVisualStyleBackColor = False
        '
        'Button44
        '
        Me.Button44.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button44.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button44.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button44.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button44.Image = Global.OkoshiMAX.My.Resources.Resources.Add_16x
        Me.Button44.Location = New System.Drawing.Point(139, 740)
        Me.Button44.Margin = New System.Windows.Forms.Padding(1)
        Me.Button44.Name = "Button44"
        Me.Button44.Size = New System.Drawing.Size(44, 38)
        Me.Button44.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.Button44, "ファイルを追加")
        Me.Button44.UseVisualStyleBackColor = False
        '
        'Button45
        '
        Me.Button45.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button45.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button45.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button45.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button45.Image = Global.OkoshiMAX.My.Resources.Resources.DownloadDocument_16x
        Me.Button45.Location = New System.Drawing.Point(185, 740)
        Me.Button45.Margin = New System.Windows.Forms.Padding(1)
        Me.Button45.Name = "Button45"
        Me.Button45.Size = New System.Drawing.Size(44, 38)
        Me.Button45.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.Button45, "プレイリスト保存")
        Me.Button45.UseVisualStyleBackColor = False
        '
        'Button46
        '
        Me.Button46.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button46.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button46.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button46.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button46.Image = Global.OkoshiMAX.My.Resources.Resources.OpenFolder_16x
        Me.Button46.Location = New System.Drawing.Point(231, 740)
        Me.Button46.Margin = New System.Windows.Forms.Padding(1)
        Me.Button46.Name = "Button46"
        Me.Button46.Size = New System.Drawing.Size(44, 38)
        Me.Button46.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.Button46, "プレイリスト読込")
        Me.Button46.UseVisualStyleBackColor = False
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.MpvPanel)
        Me.SplitContainer3.Panel1MinSize = 0
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.TableLayoutPanel1)
        Me.SplitContainer3.Panel2MinSize = 0
        Me.SplitContainer3.Size = New System.Drawing.Size(998, 779)
        Me.SplitContainer3.SplitterDistance = 292
        Me.SplitContainer3.SplitterWidth = 5
        Me.SplitContainer3.TabIndex = 1
        '
        'MpvPanel
        '
        Me.MpvPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.MpvPanel.BackColor = System.Drawing.Color.Black
        Me.MpvPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MpvPanel.Location = New System.Drawing.Point(0, 0)
        Me.MpvPanel.Margin = New System.Windows.Forms.Padding(1)
        Me.MpvPanel.Name = "MpvPanel"
        Me.MpvPanel.Size = New System.Drawing.Size(998, 292)
        Me.MpvPanel.TabIndex = 193
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.TableLayoutPanel1.ColumnCount = 21
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.70151!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.969807!))
        Me.TableLayoutPanel1.Controls.Add(Me.CheckBoxMpvPamel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 13, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Button33, 10, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBox2, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBox1, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Button3, 4, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Button4, 6, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Button5, 8, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Button6, 10, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Button7, 12, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Button8, 14, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Button9, 16, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Button10, 18, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.TrackBar1, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Button11, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Button13, 4, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Button14, 6, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Button15, 8, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Button16, 10, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Button17, 12, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Button18, 14, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Button19, 16, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Button20, 18, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.TrackBar2, 8, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 17, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Button21, 6, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.Button22, 8, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.Button23, 10, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.Button24, 12, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.Button25, 14, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.Button26, 16, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.Button27, 18, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 6, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Button39, 13, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Button37, 19, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 20, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.TrackBar6, 20, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Button36, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Button200, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Button400, 3, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 20, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonShiori, 17, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Button35, 15, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Button1, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Button2, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Button12, 2, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Button30, 12, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Button38, 12, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Button40, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 5)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.Padding = New System.Windows.Forms.Padding(1, 0, 1, 1)
        Me.TableLayoutPanel1.RowCount = 8
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(998, 482)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'CheckBoxMpvPamel
        '
        Me.CheckBoxMpvPamel.Appearance = System.Windows.Forms.Appearance.Button
        Me.CheckBoxMpvPamel.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CheckBoxMpvPamel.Checked = True
        Me.CheckBoxMpvPamel.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TableLayoutPanel1.SetColumnSpan(Me.CheckBoxMpvPamel, 2)
        Me.CheckBoxMpvPamel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBoxMpvPamel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CheckBoxMpvPamel.Image = Global.OkoshiMAX.My.Resources.Resources.Monitor_16x
        Me.CheckBoxMpvPamel.Location = New System.Drawing.Point(2, 1)
        Me.CheckBoxMpvPamel.Margin = New System.Windows.Forms.Padding(1)
        Me.CheckBoxMpvPamel.Name = "CheckBoxMpvPamel"
        Me.CheckBoxMpvPamel.Size = New System.Drawing.Size(90, 66)
        Me.CheckBoxMpvPamel.TabIndex = 919
        Me.ToolTip1.SetToolTip(Me.CheckBoxMpvPamel, "動画再生画面の表示・非表示")
        Me.CheckBoxMpvPamel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoEllipsis = True
        Me.Label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label1, 8)
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Lime
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Label1.Location = New System.Drawing.Point(599, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(398, 68)
        Me.Label1.TabIndex = 920
        Me.Label1.Text = "00:00:00"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button33
        '
        Me.Button33.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button33, 2)
        Me.Button33.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button33.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button33.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button33.Location = New System.Drawing.Point(462, 1)
        Me.Button33.Margin = New System.Windows.Forms.Padding(1)
        Me.Button33.Name = "Button33"
        Me.Button33.Size = New System.Drawing.Size(90, 66)
        Me.Button33.TabIndex = 921
        Me.Button33.Text = ">>"
        Me.ToolTip1.SetToolTip(Me.Button33, "指定位置にジャンプ")
        Me.Button33.UseVisualStyleBackColor = False
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TableLayoutPanel1.SetColumnSpan(Me.TextBox2, 6)
        Me.TextBox2.Location = New System.Drawing.Point(186, 23)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(274, 22)
        Me.TextBox2.TabIndex = 922
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.TextBox2, "カウンタ入力欄")
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TableLayoutPanel1.SetColumnSpan(Me.TextBox1, 10)
        Me.TextBox1.Location = New System.Drawing.Point(94, 91)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(458, 22)
        Me.TextBox1.TabIndex = 923
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button3, 2)
        Me.Button3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button3.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button3.Location = New System.Drawing.Point(186, 137)
        Me.Button3.Margin = New System.Windows.Forms.Padding(1)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(90, 66)
        Me.Button3.TabIndex = 930
        Me.Button3.Text = "+5S"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button4, 2)
        Me.Button4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button4.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button4.Location = New System.Drawing.Point(278, 137)
        Me.Button4.Margin = New System.Windows.Forms.Padding(1)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(90, 66)
        Me.Button4.TabIndex = 931
        Me.Button4.Text = "+10S"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button5, 2)
        Me.Button5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button5.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button5.Location = New System.Drawing.Point(370, 137)
        Me.Button5.Margin = New System.Windows.Forms.Padding(1)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(90, 66)
        Me.Button5.TabIndex = 932
        Me.Button5.Text = "+15S"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button6, 2)
        Me.Button6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button6.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button6.Location = New System.Drawing.Point(462, 137)
        Me.Button6.Margin = New System.Windows.Forms.Padding(1)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(90, 66)
        Me.Button6.TabIndex = 933
        Me.Button6.Text = "+30S"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button7, 2)
        Me.Button7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button7.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button7.Location = New System.Drawing.Point(554, 137)
        Me.Button7.Margin = New System.Windows.Forms.Padding(1)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(90, 66)
        Me.Button7.TabIndex = 934
        Me.Button7.Text = "+1M"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Button8
        '
        Me.Button8.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button8, 2)
        Me.Button8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button8.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button8.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button8.Location = New System.Drawing.Point(646, 137)
        Me.Button8.Margin = New System.Windows.Forms.Padding(1)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(90, 66)
        Me.Button8.TabIndex = 935
        Me.Button8.Text = "+3M"
        Me.Button8.UseVisualStyleBackColor = False
        '
        'Button9
        '
        Me.Button9.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button9, 2)
        Me.Button9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button9.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button9.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button9.Location = New System.Drawing.Point(738, 137)
        Me.Button9.Margin = New System.Windows.Forms.Padding(1)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(90, 66)
        Me.Button9.TabIndex = 936
        Me.Button9.Text = "+5M"
        Me.Button9.UseVisualStyleBackColor = False
        '
        'Button10
        '
        Me.Button10.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button10, 2)
        Me.Button10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button10.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button10.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button10.Location = New System.Drawing.Point(830, 137)
        Me.Button10.Margin = New System.Windows.Forms.Padding(1)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(90, 66)
        Me.Button10.TabIndex = 937
        Me.Button10.Text = "+10M"
        Me.Button10.UseVisualStyleBackColor = False
        '
        'TrackBar1
        '
        Me.TrackBar1.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.TableLayoutPanel1.SetColumnSpan(Me.TrackBar1, 20)
        Me.TrackBar1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TrackBar1.Location = New System.Drawing.Point(2, 205)
        Me.TrackBar1.Margin = New System.Windows.Forms.Padding(1)
        Me.TrackBar1.Maximum = 10000
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(918, 66)
        Me.TrackBar1.TabIndex = 938
        Me.TrackBar1.TickFrequency = 100
        Me.TrackBar1.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'Button11
        '
        Me.Button11.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button11, 2)
        Me.Button11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button11.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button11.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button11.Location = New System.Drawing.Point(2, 273)
        Me.Button11.Margin = New System.Windows.Forms.Padding(1)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(90, 66)
        Me.Button11.TabIndex = 939
        Me.Button11.Text = "-1S"
        Me.Button11.UseVisualStyleBackColor = False
        '
        'Button13
        '
        Me.Button13.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button13, 2)
        Me.Button13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button13.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button13.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button13.Location = New System.Drawing.Point(186, 273)
        Me.Button13.Margin = New System.Windows.Forms.Padding(1)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(90, 66)
        Me.Button13.TabIndex = 941
        Me.Button13.Text = "-5S"
        Me.Button13.UseVisualStyleBackColor = False
        '
        'Button14
        '
        Me.Button14.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button14, 2)
        Me.Button14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button14.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button14.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button14.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button14.Location = New System.Drawing.Point(278, 273)
        Me.Button14.Margin = New System.Windows.Forms.Padding(1)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(90, 66)
        Me.Button14.TabIndex = 942
        Me.Button14.Text = "-10S"
        Me.Button14.UseVisualStyleBackColor = False
        '
        'Button15
        '
        Me.Button15.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button15, 2)
        Me.Button15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button15.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button15.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button15.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button15.Location = New System.Drawing.Point(370, 273)
        Me.Button15.Margin = New System.Windows.Forms.Padding(1)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(90, 66)
        Me.Button15.TabIndex = 943
        Me.Button15.Text = "-15S"
        Me.Button15.UseVisualStyleBackColor = False
        '
        'Button16
        '
        Me.Button16.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button16, 2)
        Me.Button16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button16.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button16.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button16.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button16.Location = New System.Drawing.Point(462, 273)
        Me.Button16.Margin = New System.Windows.Forms.Padding(1)
        Me.Button16.Name = "Button16"
        Me.Button16.Size = New System.Drawing.Size(90, 66)
        Me.Button16.TabIndex = 944
        Me.Button16.Text = "-30S"
        Me.Button16.UseVisualStyleBackColor = False
        '
        'Button17
        '
        Me.Button17.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button17, 2)
        Me.Button17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button17.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button17.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button17.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button17.Location = New System.Drawing.Point(554, 273)
        Me.Button17.Margin = New System.Windows.Forms.Padding(1)
        Me.Button17.Name = "Button17"
        Me.Button17.Size = New System.Drawing.Size(90, 66)
        Me.Button17.TabIndex = 945
        Me.Button17.Text = "-1M"
        Me.Button17.UseVisualStyleBackColor = False
        '
        'Button18
        '
        Me.Button18.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button18, 2)
        Me.Button18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button18.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button18.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button18.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button18.Location = New System.Drawing.Point(646, 273)
        Me.Button18.Margin = New System.Windows.Forms.Padding(1)
        Me.Button18.Name = "Button18"
        Me.Button18.Size = New System.Drawing.Size(90, 66)
        Me.Button18.TabIndex = 946
        Me.Button18.Text = "-3M"
        Me.Button18.UseVisualStyleBackColor = False
        '
        'Button19
        '
        Me.Button19.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button19, 2)
        Me.Button19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button19.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button19.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button19.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button19.Location = New System.Drawing.Point(738, 273)
        Me.Button19.Margin = New System.Windows.Forms.Padding(1)
        Me.Button19.Name = "Button19"
        Me.Button19.Size = New System.Drawing.Size(90, 66)
        Me.Button19.TabIndex = 947
        Me.Button19.Text = "-5M"
        Me.Button19.UseVisualStyleBackColor = False
        '
        'Button20
        '
        Me.Button20.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button20, 2)
        Me.Button20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button20.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button20.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button20.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button20.Location = New System.Drawing.Point(830, 273)
        Me.Button20.Margin = New System.Windows.Forms.Padding(1)
        Me.Button20.Name = "Button20"
        Me.Button20.Size = New System.Drawing.Size(90, 66)
        Me.Button20.TabIndex = 948
        Me.Button20.Text = "-10M"
        Me.Button20.UseVisualStyleBackColor = False
        '
        'TrackBar2
        '
        Me.TrackBar2.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.TrackBar2, 9)
        Me.TrackBar2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TrackBar2.LargeChange = 1
        Me.TrackBar2.Location = New System.Drawing.Point(370, 341)
        Me.TrackBar2.Margin = New System.Windows.Forms.Padding(1)
        Me.TrackBar2.Maximum = 40
        Me.TrackBar2.Minimum = 5
        Me.TrackBar2.Name = "TrackBar2"
        Me.TrackBar2.Size = New System.Drawing.Size(412, 66)
        Me.TrackBar2.TabIndex = 952
        Me.ToolTip1.SetToolTip(Me.TrackBar2, "再生速度")
        Me.TrackBar2.Value = 10
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label4, 3)
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label4.Location = New System.Drawing.Point(783, 340)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(138, 68)
        Me.Label4.TabIndex = 954
        Me.Label4.Text = "x1.0"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button21
        '
        Me.Button21.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button21, 2)
        Me.Button21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button21.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button21.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button21.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button21.Location = New System.Drawing.Point(278, 409)
        Me.Button21.Margin = New System.Windows.Forms.Padding(1)
        Me.Button21.Name = "Button21"
        Me.Button21.Size = New System.Drawing.Size(90, 71)
        Me.Button21.TabIndex = 956
        Me.Button21.Text = "0.5"
        Me.Button21.UseVisualStyleBackColor = False
        '
        'Button22
        '
        Me.Button22.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button22, 2)
        Me.Button22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button22.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button22.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button22.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button22.Location = New System.Drawing.Point(370, 409)
        Me.Button22.Margin = New System.Windows.Forms.Padding(1)
        Me.Button22.Name = "Button22"
        Me.Button22.Size = New System.Drawing.Size(90, 71)
        Me.Button22.TabIndex = 957
        Me.Button22.Text = "1.0"
        Me.Button22.UseVisualStyleBackColor = False
        '
        'Button23
        '
        Me.Button23.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button23, 2)
        Me.Button23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button23.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button23.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button23.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button23.Location = New System.Drawing.Point(462, 409)
        Me.Button23.Margin = New System.Windows.Forms.Padding(1)
        Me.Button23.Name = "Button23"
        Me.Button23.Size = New System.Drawing.Size(90, 71)
        Me.Button23.TabIndex = 958
        Me.Button23.Text = "1.1"
        Me.Button23.UseVisualStyleBackColor = False
        '
        'Button24
        '
        Me.Button24.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button24, 2)
        Me.Button24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button24.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button24.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button24.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button24.Location = New System.Drawing.Point(554, 409)
        Me.Button24.Margin = New System.Windows.Forms.Padding(1)
        Me.Button24.Name = "Button24"
        Me.Button24.Size = New System.Drawing.Size(90, 71)
        Me.Button24.TabIndex = 959
        Me.Button24.Text = "1.2"
        Me.Button24.UseVisualStyleBackColor = False
        '
        'Button25
        '
        Me.Button25.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button25, 2)
        Me.Button25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button25.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button25.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button25.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button25.Location = New System.Drawing.Point(646, 409)
        Me.Button25.Margin = New System.Windows.Forms.Padding(1)
        Me.Button25.Name = "Button25"
        Me.Button25.Size = New System.Drawing.Size(90, 71)
        Me.Button25.TabIndex = 960
        Me.Button25.Text = "1.3"
        Me.Button25.UseVisualStyleBackColor = False
        '
        'Button26
        '
        Me.Button26.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button26, 2)
        Me.Button26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button26.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button26.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button26.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button26.Location = New System.Drawing.Point(738, 409)
        Me.Button26.Margin = New System.Windows.Forms.Padding(1)
        Me.Button26.Name = "Button26"
        Me.Button26.Size = New System.Drawing.Size(90, 71)
        Me.Button26.TabIndex = 961
        Me.Button26.Text = "1.4"
        Me.Button26.UseVisualStyleBackColor = False
        '
        'Button27
        '
        Me.Button27.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button27, 2)
        Me.Button27.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button27.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button27.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button27.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button27.Location = New System.Drawing.Point(830, 409)
        Me.Button27.Margin = New System.Windows.Forms.Padding(1)
        Me.Button27.Name = "Button27"
        Me.Button27.Size = New System.Drawing.Size(90, 71)
        Me.Button27.TabIndex = 962
        Me.Button27.Text = "1.5"
        Me.Button27.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label6, 2)
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label6.Location = New System.Drawing.Point(277, 340)
        Me.Label6.Margin = New System.Windows.Forms.Padding(0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(92, 68)
        Me.Label6.TabIndex = 951
        Me.Label6.Text = "速度"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button39
        '
        Me.Button39.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button39, 2)
        Me.Button39.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button39.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button39.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button39.Image = Global.OkoshiMAX.My.Resources.Resources.OpenFolder_16x
        Me.Button39.Location = New System.Drawing.Point(600, 69)
        Me.Button39.Margin = New System.Windows.Forms.Padding(1)
        Me.Button39.Name = "Button39"
        Me.Button39.Size = New System.Drawing.Size(90, 66)
        Me.Button39.TabIndex = 927
        Me.ToolTip1.SetToolTip(Me.Button39, "ファイルを開く")
        Me.Button39.UseVisualStyleBackColor = False
        '
        'Button37
        '
        Me.Button37.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button37, 2)
        Me.Button37.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button37.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button37.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button37.Image = Global.OkoshiMAX.My.Resources.Resources.SettingsOutline_16x
        Me.Button37.Location = New System.Drawing.Point(876, 69)
        Me.Button37.Margin = New System.Windows.Forms.Padding(1)
        Me.Button37.Name = "Button37"
        Me.Button37.Size = New System.Drawing.Size(120, 66)
        Me.Button37.TabIndex = 924
        Me.ToolTip1.SetToolTip(Me.Button37, "設定")
        Me.Button37.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Image = Global.OkoshiMAX.My.Resources.Resources.Volume_16x
        Me.Label2.Location = New System.Drawing.Point(921, 136)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 68)
        Me.Label2.TabIndex = 969
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TrackBar6
        '
        Me.TrackBar6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.TrackBar6.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TrackBar6.LargeChange = 1
        Me.TrackBar6.Location = New System.Drawing.Point(931, 204)
        Me.TrackBar6.Margin = New System.Windows.Forms.Padding(0)
        Me.TrackBar6.Maximum = 100
        Me.TrackBar6.Name = "TrackBar6"
        Me.TrackBar6.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TableLayoutPanel1.SetRowSpan(Me.TrackBar6, 3)
        Me.TrackBar6.Size = New System.Drawing.Size(56, 204)
        Me.TrackBar6.TabIndex = 950
        Me.TrackBar6.TickFrequency = 10
        Me.TrackBar6.TickStyle = System.Windows.Forms.TickStyle.Both
        Me.ToolTip1.SetToolTip(Me.TrackBar6, "音量")
        Me.TrackBar6.Value = 60
        '
        'Button36
        '
        Me.Button36.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button36, 2)
        Me.Button36.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button36.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button36.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button36.Image = Global.OkoshiMAX.My.Resources.Resources.CaptureFrame_16x
        Me.Button36.Location = New System.Drawing.Point(94, 1)
        Me.Button36.Margin = New System.Windows.Forms.Padding(1)
        Me.Button36.Name = "Button36"
        Me.Button36.Size = New System.Drawing.Size(90, 66)
        Me.Button36.TabIndex = 925
        Me.ToolTip1.SetToolTip(Me.Button36, "画面キャプチャ")
        Me.Button36.UseVisualStyleBackColor = False
        '
        'Button200
        '
        Me.Button200.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button200.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button200, 3)
        Me.Button200.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button200.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button200.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button200.Image = Global.OkoshiMAX.My.Resources.Resources.Run_16x
        Me.Button200.Location = New System.Drawing.Point(2, 341)
        Me.Button200.Margin = New System.Windows.Forms.Padding(1)
        Me.Button200.Name = "Button200"
        Me.TableLayoutPanel1.SetRowSpan(Me.Button200, 2)
        Me.Button200.Size = New System.Drawing.Size(136, 139)
        Me.Button200.TabIndex = 963
        Me.ToolTip1.SetToolTip(Me.Button200, "再生・一時停止")
        Me.Button200.UseVisualStyleBackColor = False
        '
        'Button400
        '
        Me.Button400.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button400.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button400, 3)
        Me.Button400.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button400.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button400.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button400.Image = Global.OkoshiMAX.My.Resources.Resources.Stop_grey_16x
        Me.Button400.Location = New System.Drawing.Point(140, 341)
        Me.Button400.Margin = New System.Windows.Forms.Padding(1)
        Me.Button400.Name = "Button400"
        Me.TableLayoutPanel1.SetRowSpan(Me.Button400, 2)
        Me.Button400.Size = New System.Drawing.Size(136, 139)
        Me.Button400.TabIndex = 964
        Me.ToolTip1.SetToolTip(Me.Button400, "停止")
        Me.Button400.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label5.Location = New System.Drawing.Point(921, 408)
        Me.Label5.Margin = New System.Windows.Forms.Padding(0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 73)
        Me.Label5.TabIndex = 955
        Me.Label5.Text = "100"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ButtonShiori
        '
        Me.ButtonShiori.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.ButtonShiori, 2)
        Me.ButtonShiori.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonShiori.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonShiori.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonShiori.Image = Global.OkoshiMAX.My.Resources.Resources.Bookmark_16x
        Me.ButtonShiori.Location = New System.Drawing.Point(784, 69)
        Me.ButtonShiori.Margin = New System.Windows.Forms.Padding(1)
        Me.ButtonShiori.Name = "ButtonShiori"
        Me.ButtonShiori.Size = New System.Drawing.Size(90, 66)
        Me.ButtonShiori.TabIndex = 967
        Me.ToolTip1.SetToolTip(Me.ButtonShiori, "しおり一覧")
        Me.ButtonShiori.UseVisualStyleBackColor = False
        '
        'Button35
        '
        Me.Button35.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button35, 2)
        Me.Button35.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button35.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button35.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button35.Image = Global.OkoshiMAX.My.Resources.Resources.PinnedItem_16x
        Me.Button35.Location = New System.Drawing.Point(692, 69)
        Me.Button35.Margin = New System.Windows.Forms.Padding(1)
        Me.Button35.Name = "Button35"
        Me.Button35.Size = New System.Drawing.Size(90, 66)
        Me.Button35.TabIndex = 971
        Me.ToolTip1.SetToolTip(Me.Button35, "最前面に表示")
        Me.Button35.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button1, 2)
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button1.Location = New System.Drawing.Point(2, 137)
        Me.Button1.Margin = New System.Windows.Forms.Padding(1)
        Me.Button1.MinimumSize = New System.Drawing.Size(0, 25)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(90, 66)
        Me.Button1.TabIndex = 940
        Me.Button1.Text = "+1S"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button2, 2)
        Me.Button2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button2.Location = New System.Drawing.Point(94, 137)
        Me.Button2.Margin = New System.Windows.Forms.Padding(1)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(90, 66)
        Me.Button2.TabIndex = 928
        Me.Button2.Text = "+3S"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button12
        '
        Me.Button12.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button12, 2)
        Me.Button12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button12.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button12.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button12.Location = New System.Drawing.Point(94, 273)
        Me.Button12.Margin = New System.Windows.Forms.Padding(1)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(90, 66)
        Me.Button12.TabIndex = 929
        Me.Button12.Text = "-3S"
        Me.Button12.UseVisualStyleBackColor = False
        '
        'Button30
        '
        Me.Button30.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button30.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button30.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button30.Location = New System.Drawing.Point(555, 2)
        Me.Button30.Margin = New System.Windows.Forms.Padding(2)
        Me.Button30.Name = "Button30"
        Me.Button30.Size = New System.Drawing.Size(42, 64)
        Me.Button30.TabIndex = 973
        Me.Button30.Text = "Button30"
        Me.Button30.UseVisualStyleBackColor = True
        '
        'Button38
        '
        Me.Button38.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button38.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button38.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button38.Location = New System.Drawing.Point(555, 70)
        Me.Button38.Margin = New System.Windows.Forms.Padding(2)
        Me.Button38.Name = "Button38"
        Me.Button38.Size = New System.Drawing.Size(42, 64)
        Me.Button38.TabIndex = 974
        Me.Button38.Text = "Button38"
        Me.Button38.UseVisualStyleBackColor = True
        '
        'Button40
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button40, 2)
        Me.Button40.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button40.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button40.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button40.Location = New System.Drawing.Point(3, 70)
        Me.Button40.Margin = New System.Windows.Forms.Padding(2)
        Me.Button40.Name = "Button40"
        Me.Button40.Size = New System.Drawing.Size(88, 64)
        Me.Button40.TabIndex = 975
        Me.Button40.Text = "Button40"
        Me.ToolTip1.SetToolTip(Me.Button40, "プレイリストの表示・非表示")
        Me.Button40.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.TableLayoutPanel2.ColumnCount = 4
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.DataGridView1, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Button28, 3, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Button29, 2, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Button32, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Button31, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.TextBox3, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Button34, 0, 2)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(315, 779)
        Me.TableLayoutPanel2.TabIndex = 158
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCounter, Me.colMemo, Me.colPosition, Me.colDelete})
        Me.TableLayoutPanel2.SetColumnSpan(Me.DataGridView1, 4)
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(1, 33)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(1)
        Me.DataGridView1.MinimumSize = New System.Drawing.Size(200, 100)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 25
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(313, 665)
        Me.DataGridView1.TabIndex = 149
        '
        'colCounter
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        Me.colCounter.DefaultCellStyle = DataGridViewCellStyle2
        Me.colCounter.FillWeight = 80.0!
        Me.colCounter.HeaderText = "カウンタ"
        Me.colCounter.MinimumWidth = 6
        Me.colCounter.Name = "colCounter"
        Me.colCounter.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colCounter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colCounter.Text = "カウンタ"
        Me.colCounter.ToolTipText = "カウンタ"
        '
        'colMemo
        '
        Me.colMemo.FillWeight = 150.0!
        Me.colMemo.HeaderText = "メモ"
        Me.colMemo.MinimumWidth = 50
        Me.colMemo.Name = "colMemo"
        '
        'colPosition
        '
        Me.colPosition.FillWeight = 80.0!
        Me.colPosition.HeaderText = "Position"
        Me.colPosition.MinimumWidth = 50
        Me.colPosition.Name = "colPosition"
        Me.colPosition.Visible = False
        '
        'colDelete
        '
        Me.colDelete.FillWeight = 50.0!
        Me.colDelete.HeaderText = "削除"
        Me.colDelete.MinimumWidth = 40
        Me.colDelete.Name = "colDelete"
        Me.colDelete.Text = "削除"
        Me.colDelete.ToolTipText = "削除"
        Me.colDelete.UseColumnTextForButtonValue = True
        '
        'Button28
        '
        Me.Button28.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button28.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button28.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button28.Image = Global.OkoshiMAX.My.Resources.Resources.Cancel_16x
        Me.Button28.Location = New System.Drawing.Point(235, 740)
        Me.Button28.Margin = New System.Windows.Forms.Padding(1)
        Me.Button28.Name = "Button28"
        Me.Button28.Size = New System.Drawing.Size(79, 38)
        Me.Button28.TabIndex = 150
        Me.ToolTip1.SetToolTip(Me.Button28, "一覧から削除")
        Me.Button28.UseVisualStyleBackColor = False
        '
        'Button29
        '
        Me.Button29.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button29.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button29.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button29.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button29.Image = Global.OkoshiMAX.My.Resources.Resources.Add_16x
        Me.Button29.Location = New System.Drawing.Point(157, 740)
        Me.Button29.Margin = New System.Windows.Forms.Padding(1)
        Me.Button29.Name = "Button29"
        Me.Button29.Size = New System.Drawing.Size(76, 38)
        Me.Button29.TabIndex = 151
        Me.ToolTip1.SetToolTip(Me.Button29, "しおりを追加")
        Me.Button29.UseVisualStyleBackColor = False
        '
        'Button32
        '
        Me.Button32.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button32.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button32.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button32.Image = Global.OkoshiMAX.My.Resources.Resources.DestinationAssistant_16x
        Me.Button32.Location = New System.Drawing.Point(79, 740)
        Me.Button32.Margin = New System.Windows.Forms.Padding(1)
        Me.Button32.Name = "Button32"
        Me.Button32.Size = New System.Drawing.Size(76, 38)
        Me.Button32.TabIndex = 152
        Me.ToolTip1.SetToolTip(Me.Button32, "CSV・Wordファイル読み込み")
        Me.Button32.UseVisualStyleBackColor = False
        '
        'Button31
        '
        Me.Button31.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button31.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button31.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button31.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button31.Image = Global.OkoshiMAX.My.Resources.Resources.DownloadDocument_16x
        Me.Button31.Location = New System.Drawing.Point(1, 740)
        Me.Button31.Margin = New System.Windows.Forms.Padding(1)
        Me.Button31.Name = "Button31"
        Me.Button31.Size = New System.Drawing.Size(76, 38)
        Me.Button31.TabIndex = 153
        Me.ToolTip1.SetToolTip(Me.Button31, "CSVファイル書き出し")
        Me.Button31.UseVisualStyleBackColor = False
        '
        'TextBox3
        '
        Me.TextBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.SetColumnSpan(Me.TextBox3, 3)
        Me.TextBox3.Location = New System.Drawing.Point(79, 700)
        Me.TextBox3.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(235, 22)
        Me.TextBox3.TabIndex = 156
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button34
        '
        Me.Button34.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button34.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button34.Location = New System.Drawing.Point(1, 700)
        Me.Button34.Margin = New System.Windows.Forms.Padding(1)
        Me.Button34.Name = "Button34"
        Me.Button34.Size = New System.Drawing.Size(76, 38)
        Me.Button34.TabIndex = 157
        Me.Button34.Text = ">>"
        Me.ToolTip1.SetToolTip(Me.Button34, "カウンタを手動入力して追加")
        Me.Button34.UseVisualStyleBackColor = False
        '
        'CustomTitleBar
        '
        Me.CustomTitleBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.CustomTitleBar.Controls.Add(Me.BtnMinimize)
        Me.CustomTitleBar.Controls.Add(Me.BtnMaximize)
        Me.CustomTitleBar.Controls.Add(Me.BtnClose)
        Me.CustomTitleBar.Controls.Add(Me.LblTitle)
        Me.CustomTitleBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.CustomTitleBar.Location = New System.Drawing.Point(3, 28)
        Me.CustomTitleBar.Name = "CustomTitleBar"
        Me.CustomTitleBar.Size = New System.Drawing.Size(1599, 28)
        Me.CustomTitleBar.TabIndex = 0
        '
        'BtnMinimize
        '
        Me.BtnMinimize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnMinimize.BackColor = System.Drawing.Color.Transparent
        Me.BtnMinimize.FlatAppearance.BorderSize = 0
        Me.BtnMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.BtnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.BtnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnMinimize.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnMinimize.ForeColor = System.Drawing.Color.White
        Me.BtnMinimize.Location = New System.Drawing.Point(2884, 0)
        Me.BtnMinimize.Name = "BtnMinimize"
        Me.BtnMinimize.Size = New System.Drawing.Size(40, 32)
        Me.BtnMinimize.TabIndex = 0
        Me.BtnMinimize.Text = "🗕"
        Me.BtnMinimize.UseVisualStyleBackColor = True
        '
        'BtnMaximize
        '
        Me.BtnMaximize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnMaximize.BackColor = System.Drawing.Color.Transparent
        Me.BtnMaximize.FlatAppearance.BorderSize = 0
        Me.BtnMaximize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.BtnMaximize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.BtnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnMaximize.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnMaximize.ForeColor = System.Drawing.Color.White
        Me.BtnMaximize.Location = New System.Drawing.Point(2924, 0)
        Me.BtnMaximize.Name = "BtnMaximize"
        Me.BtnMaximize.Size = New System.Drawing.Size(40, 32)
        Me.BtnMaximize.TabIndex = 1
        Me.BtnMaximize.Text = "🗖"
        Me.BtnMaximize.UseVisualStyleBackColor = True
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.BackColor = System.Drawing.Color.Transparent
        Me.BtnClose.FlatAppearance.BorderSize = 0
        Me.BtnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.BtnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnClose.ForeColor = System.Drawing.Color.White
        Me.BtnClose.Location = New System.Drawing.Point(2964, 0)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(40, 32)
        Me.BtnClose.TabIndex = 2
        Me.BtnClose.Text = "✕"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'LblTitle
        '
        Me.LblTitle.AutoSize = True
        Me.LblTitle.BackColor = System.Drawing.Color.Transparent
        Me.LblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblTitle.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTitle.ForeColor = System.Drawing.Color.White
        Me.LblTitle.Location = New System.Drawing.Point(0, 0)
        Me.LblTitle.Name = "LblTitle"
        Me.LblTitle.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.LblTitle.Size = New System.Drawing.Size(99, 19)
        Me.LblTitle.TabIndex = 3
        Me.LblTitle.Text = "OkoshiMAX"
        Me.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MainPlayerForm
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(1605, 838)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.CustomTitleBar)
        Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.OkoshiMAX.My.MySettings.Default, "MyLocation", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = Global.OkoshiMAX.My.MySettings.Default.MyLocation
        Me.Margin = New System.Windows.Forms.Padding(1)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(638, 233)
        Me.Name = "MainPlayerForm"
        Me.Padding = New System.Windows.Forms.Padding(2)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "OkoshiMAX"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CustomTitleBar.ResumeLayout(False)
        Me.CustomTitleBar.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Button34 As System.Windows.Forms.Button
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Button32 As System.Windows.Forms.Button
    Friend WithEvents Button31 As System.Windows.Forms.Button
    Friend WithEvents Button28 As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Button29 As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CheckBoxMpvPamel As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button33 As System.Windows.Forms.Button
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button37 As System.Windows.Forms.Button
    Friend WithEvents Button36 As System.Windows.Forms.Button
    Friend WithEvents Button39 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button13 As System.Windows.Forms.Button
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents Button15 As System.Windows.Forms.Button
    Friend WithEvents Button16 As System.Windows.Forms.Button
    Friend WithEvents Button17 As System.Windows.Forms.Button
    Friend WithEvents Button18 As System.Windows.Forms.Button
    Friend WithEvents Button19 As System.Windows.Forms.Button
    Friend WithEvents Button20 As System.Windows.Forms.Button
    Friend WithEvents TrackBar6 As System.Windows.Forms.TrackBar
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TrackBar2 As System.Windows.Forms.TrackBar
    Friend WithEvents CustomTitleBar As System.Windows.Forms.Panel
    Friend WithEvents BtnMinimize As System.Windows.Forms.Button
    Friend WithEvents BtnMaximize As System.Windows.Forms.Button
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents LblTitle As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button21 As System.Windows.Forms.Button
    Friend WithEvents Button22 As System.Windows.Forms.Button
    Friend WithEvents Button23 As System.Windows.Forms.Button
    Friend WithEvents Button24 As System.Windows.Forms.Button
    Friend WithEvents Button25 As System.Windows.Forms.Button
    Friend WithEvents Button26 As System.Windows.Forms.Button
    Friend WithEvents Button27 As System.Windows.Forms.Button
    Friend WithEvents Button200 As System.Windows.Forms.Button
    Friend WithEvents Button400 As System.Windows.Forms.Button
    Friend WithEvents ButtonShiori As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button35 As System.Windows.Forms.Button
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents Button30 As System.Windows.Forms.Button
    Friend WithEvents Button38 As System.Windows.Forms.Button
    Friend WithEvents Button40 As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Button41 As System.Windows.Forms.Button
    Friend WithEvents Button42 As System.Windows.Forms.Button
    Friend WithEvents Button43 As System.Windows.Forms.Button
    Friend WithEvents Button44 As System.Windows.Forms.Button
    Friend WithEvents Button45 As System.Windows.Forms.Button
    Friend WithEvents Button46 As System.Windows.Forms.Button
    Friend WithEvents colCounter As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents colMemo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPosition As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDelete As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents File_Namae As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents File_Length As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents File_Memo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents File_Delete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents File_Position As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents File_Progress As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MpvPanel As System.Windows.Forms.Panel
End Class
