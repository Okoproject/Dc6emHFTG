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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainPlayerForm))
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Button41 = New System.Windows.Forms.Button()
        Me.Button42 = New System.Windows.Forms.Button()
        Me.Button43 = New System.Windows.Forms.Button()
        Me.Button44 = New System.Windows.Forms.Button()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.File_Namae = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.File_Length = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.File_Memo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.File_Delete = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.File_Position = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.File_Progress = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.TableLayoutPanel2)
        Me.SplitContainer1.Panel2MinSize = 0
        Me.SplitContainer1.Size = New System.Drawing.Size(1284, 670)
        Me.SplitContainer1.SplitterDistance = 1028
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.DataGridView2)
        Me.SplitContainer2.Panel1MinSize = 0
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Panel2MinSize = 0
        Me.SplitContainer2.Size = New System.Drawing.Size(1028, 670)
        Me.SplitContainer2.SplitterDistance = 223
        Me.SplitContainer2.TabIndex = 0
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 4
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Button41, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Button42, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Button43, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Button44, 3, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 639)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(223, 31)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'Button41
        '
        Me.Button41.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button41.Image = Global.OkoshiMAX.My.Resources.Resources.FolderClosed_16x
        Me.Button41.Location = New System.Drawing.Point(1, 1)
        Me.Button41.Margin = New System.Windows.Forms.Padding(1)
        Me.Button41.Name = "Button41"
        Me.Button41.Size = New System.Drawing.Size(53, 29)
        Me.Button41.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.Button41, "フォルダを開く")
        Me.Button41.UseVisualStyleBackColor = True
        '
        'Button42
        '
        Me.Button42.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button42.Location = New System.Drawing.Point(56, 1)
        Me.Button42.Margin = New System.Windows.Forms.Padding(1)
        Me.Button42.Name = "Button42"
        Me.Button42.Size = New System.Drawing.Size(53, 29)
        Me.Button42.TabIndex = 1
        Me.Button42.Text = "Button42"
        Me.Button42.UseVisualStyleBackColor = True
        '
        'Button43
        '
        Me.Button43.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button43.Image = Global.OkoshiMAX.My.Resources.Resources.Cancel_16x
        Me.Button43.Location = New System.Drawing.Point(111, 1)
        Me.Button43.Margin = New System.Windows.Forms.Padding(1)
        Me.Button43.Name = "Button43"
        Me.Button43.Size = New System.Drawing.Size(53, 29)
        Me.Button43.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.Button43, "一覧から削除")
        Me.Button43.UseVisualStyleBackColor = True
        '
        'Button44
        '
        Me.Button44.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button44.Image = Global.OkoshiMAX.My.Resources.Resources.Add_16x
        Me.Button44.Location = New System.Drawing.Point(166, 1)
        Me.Button44.Margin = New System.Windows.Forms.Padding(1)
        Me.Button44.Name = "Button44"
        Me.Button44.Size = New System.Drawing.Size(56, 29)
        Me.Button44.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.Button44, "ファイルを追加")
        Me.Button44.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.File_Namae, Me.File_Length, Me.File_Memo, Me.File_Delete, Me.File_Position, Me.File_Progress})
        Me.DataGridView2.Dock = System.Windows.Forms.DockStyle.Top
        Me.DataGridView2.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView2.Margin = New System.Windows.Forms.Padding(2)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowHeadersWidth = 51
        Me.DataGridView2.RowTemplate.Height = 24
        Me.DataGridView2.Size = New System.Drawing.Size(223, 637)
        Me.DataGridView2.TabIndex = 0
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
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
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
        Me.SplitContainer3.Panel2MinSize = 208
        Me.SplitContainer3.Size = New System.Drawing.Size(801, 670)
        Me.SplitContainer3.SplitterDistance = 255
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
        Me.MpvPanel.Size = New System.Drawing.Size(801, 255)
        Me.MpvPanel.TabIndex = 193
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
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
        Me.TableLayoutPanel1.Controls.Add(Me.CheckBoxMpvPamel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 13, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Button33, 10, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBox2, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBox1, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Button3, 4, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Button4, 6, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Button5, 8, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Button6, 10, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Button7, 12, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Button8, 14, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Button9, 16, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Button10, 18, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TrackBar1, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Button11, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Button13, 4, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Button14, 6, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Button15, 8, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Button16, 10, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Button17, 12, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Button18, 14, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Button19, 16, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Button20, 18, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.TrackBar2, 8, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 17, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Button21, 6, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Button22, 8, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Button23, 10, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Button24, 12, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Button25, 14, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Button26, 16, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Button27, 18, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 6, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Button39, 13, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Button37, 19, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 20, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TrackBar6, 20, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Button36, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Button200, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Button400, 3, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 20, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonShiori, 17, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Button35, 15, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Button1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Button2, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Button12, 2, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Button30, 12, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Button38, 12, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Button40, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(1, 1, 1, 8)
        Me.TableLayoutPanel1.MinimumSize = New System.Drawing.Size(413, 166)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 7
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(801, 411)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'CheckBoxMpvPamel
        '
        Me.CheckBoxMpvPamel.Appearance = System.Windows.Forms.Appearance.Button
        Me.CheckBoxMpvPamel.AutoSize = True
        Me.CheckBoxMpvPamel.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.CheckBoxMpvPamel.Checked = True
        Me.CheckBoxMpvPamel.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TableLayoutPanel1.SetColumnSpan(Me.CheckBoxMpvPamel, 2)
        Me.CheckBoxMpvPamel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBoxMpvPamel.Image = Global.OkoshiMAX.My.Resources.Resources.Monitor_16x
        Me.CheckBoxMpvPamel.Location = New System.Drawing.Point(1, 1)
        Me.CheckBoxMpvPamel.Margin = New System.Windows.Forms.Padding(1)
        Me.CheckBoxMpvPamel.Name = "CheckBoxMpvPamel"
        Me.CheckBoxMpvPamel.Size = New System.Drawing.Size(72, 56)
        Me.CheckBoxMpvPamel.TabIndex = 919
        Me.ToolTip1.SetToolTip(Me.CheckBoxMpvPamel, "動画再生画面の表示・非表示")
        Me.CheckBoxMpvPamel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoEllipsis = True
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label1, 8)
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Lime
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Label1.Location = New System.Drawing.Point(481, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(320, 58)
        Me.Label1.TabIndex = 920
        Me.Label1.Text = "00:00:00"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button33
        '
        Me.Button33.AutoSize = True
        Me.Button33.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button33.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button33, 2)
        Me.Button33.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button33.Location = New System.Drawing.Point(371, 1)
        Me.Button33.Margin = New System.Windows.Forms.Padding(1)
        Me.Button33.Name = "Button33"
        Me.Button33.Size = New System.Drawing.Size(72, 56)
        Me.Button33.TabIndex = 921
        Me.Button33.Text = ">>"
        Me.ToolTip1.SetToolTip(Me.Button33, "指定位置にジャンプ")
        Me.Button33.UseVisualStyleBackColor = False
        '
        'TextBox2
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.TextBox2, 6)
        Me.TextBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox2.Location = New System.Drawing.Point(149, 1)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(220, 19)
        Me.TextBox2.TabIndex = 922
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.TextBox2, "カウンタ入力欄")
        '
        'TextBox1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.TextBox1, 10)
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(75, 59)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(368, 19)
        Me.TextBox1.TabIndex = 923
        '
        'Button3
        '
        Me.Button3.AutoSize = True
        Me.Button3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button3.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button3, 2)
        Me.Button3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button3.Location = New System.Drawing.Point(149, 117)
        Me.Button3.Margin = New System.Windows.Forms.Padding(1)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(72, 56)
        Me.Button3.TabIndex = 930
        Me.Button3.Text = "+5S"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.AutoSize = True
        Me.Button4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button4.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button4, 2)
        Me.Button4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button4.Location = New System.Drawing.Point(223, 117)
        Me.Button4.Margin = New System.Windows.Forms.Padding(1)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(72, 56)
        Me.Button4.TabIndex = 931
        Me.Button4.Text = "+10S"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button5
        '
        Me.Button5.AutoSize = True
        Me.Button5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button5.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button5, 2)
        Me.Button5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button5.Location = New System.Drawing.Point(297, 117)
        Me.Button5.Margin = New System.Windows.Forms.Padding(1)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(72, 56)
        Me.Button5.TabIndex = 932
        Me.Button5.Text = "+15S"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.AutoSize = True
        Me.Button6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button6.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button6, 2)
        Me.Button6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button6.Location = New System.Drawing.Point(371, 117)
        Me.Button6.Margin = New System.Windows.Forms.Padding(1)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(72, 56)
        Me.Button6.TabIndex = 933
        Me.Button6.Text = "+30S"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Button7
        '
        Me.Button7.AutoSize = True
        Me.Button7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button7.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button7, 2)
        Me.Button7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button7.Location = New System.Drawing.Point(445, 117)
        Me.Button7.Margin = New System.Windows.Forms.Padding(1)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(72, 56)
        Me.Button7.TabIndex = 934
        Me.Button7.Text = "+1M"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Button8
        '
        Me.Button8.AutoSize = True
        Me.Button8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button8.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button8, 2)
        Me.Button8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button8.Location = New System.Drawing.Point(519, 117)
        Me.Button8.Margin = New System.Windows.Forms.Padding(1)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(72, 56)
        Me.Button8.TabIndex = 935
        Me.Button8.Text = "+3M"
        Me.Button8.UseVisualStyleBackColor = False
        '
        'Button9
        '
        Me.Button9.AutoSize = True
        Me.Button9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button9.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button9, 2)
        Me.Button9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button9.Location = New System.Drawing.Point(593, 117)
        Me.Button9.Margin = New System.Windows.Forms.Padding(1)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(72, 56)
        Me.Button9.TabIndex = 936
        Me.Button9.Text = "+5M"
        Me.Button9.UseVisualStyleBackColor = False
        '
        'Button10
        '
        Me.Button10.AutoSize = True
        Me.Button10.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button10.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button10, 2)
        Me.Button10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button10.Location = New System.Drawing.Point(667, 117)
        Me.Button10.Margin = New System.Windows.Forms.Padding(1)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(72, 56)
        Me.Button10.TabIndex = 937
        Me.Button10.Text = "+10M"
        Me.Button10.UseVisualStyleBackColor = False
        '
        'TrackBar1
        '
        Me.TrackBar1.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.TableLayoutPanel1.SetColumnSpan(Me.TrackBar1, 20)
        Me.TrackBar1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TrackBar1.Location = New System.Drawing.Point(1, 175)
        Me.TrackBar1.Margin = New System.Windows.Forms.Padding(1)
        Me.TrackBar1.Maximum = 10000
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(738, 56)
        Me.TrackBar1.TabIndex = 938
        Me.TrackBar1.TickFrequency = 100
        Me.TrackBar1.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'Button11
        '
        Me.Button11.AutoSize = True
        Me.Button11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button11.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button11, 2)
        Me.Button11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button11.Location = New System.Drawing.Point(1, 233)
        Me.Button11.Margin = New System.Windows.Forms.Padding(1)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(72, 56)
        Me.Button11.TabIndex = 939
        Me.Button11.Text = "-1S"
        Me.Button11.UseVisualStyleBackColor = False
        '
        'Button13
        '
        Me.Button13.AutoSize = True
        Me.Button13.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button13.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button13, 2)
        Me.Button13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button13.Location = New System.Drawing.Point(149, 233)
        Me.Button13.Margin = New System.Windows.Forms.Padding(1)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(72, 56)
        Me.Button13.TabIndex = 941
        Me.Button13.Text = "-5S"
        Me.Button13.UseVisualStyleBackColor = False
        '
        'Button14
        '
        Me.Button14.AutoSize = True
        Me.Button14.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button14.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button14, 2)
        Me.Button14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button14.Location = New System.Drawing.Point(223, 233)
        Me.Button14.Margin = New System.Windows.Forms.Padding(1)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(72, 56)
        Me.Button14.TabIndex = 942
        Me.Button14.Text = "-10S"
        Me.Button14.UseVisualStyleBackColor = False
        '
        'Button15
        '
        Me.Button15.AutoSize = True
        Me.Button15.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button15.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button15, 2)
        Me.Button15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button15.Location = New System.Drawing.Point(297, 233)
        Me.Button15.Margin = New System.Windows.Forms.Padding(1)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(72, 56)
        Me.Button15.TabIndex = 943
        Me.Button15.Text = "-15S"
        Me.Button15.UseVisualStyleBackColor = False
        '
        'Button16
        '
        Me.Button16.AutoSize = True
        Me.Button16.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button16.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button16, 2)
        Me.Button16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button16.Location = New System.Drawing.Point(371, 233)
        Me.Button16.Margin = New System.Windows.Forms.Padding(1)
        Me.Button16.Name = "Button16"
        Me.Button16.Size = New System.Drawing.Size(72, 56)
        Me.Button16.TabIndex = 944
        Me.Button16.Text = "-30S"
        Me.Button16.UseVisualStyleBackColor = False
        '
        'Button17
        '
        Me.Button17.AutoSize = True
        Me.Button17.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button17.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button17, 2)
        Me.Button17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button17.Location = New System.Drawing.Point(445, 233)
        Me.Button17.Margin = New System.Windows.Forms.Padding(1)
        Me.Button17.Name = "Button17"
        Me.Button17.Size = New System.Drawing.Size(72, 56)
        Me.Button17.TabIndex = 945
        Me.Button17.Text = "-1M"
        Me.Button17.UseVisualStyleBackColor = False
        '
        'Button18
        '
        Me.Button18.AutoSize = True
        Me.Button18.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button18.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button18, 2)
        Me.Button18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button18.Location = New System.Drawing.Point(519, 233)
        Me.Button18.Margin = New System.Windows.Forms.Padding(1)
        Me.Button18.Name = "Button18"
        Me.Button18.Size = New System.Drawing.Size(72, 56)
        Me.Button18.TabIndex = 946
        Me.Button18.Text = "-3M"
        Me.Button18.UseVisualStyleBackColor = False
        '
        'Button19
        '
        Me.Button19.AutoSize = True
        Me.Button19.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button19.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button19, 2)
        Me.Button19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button19.Location = New System.Drawing.Point(593, 233)
        Me.Button19.Margin = New System.Windows.Forms.Padding(1)
        Me.Button19.Name = "Button19"
        Me.Button19.Size = New System.Drawing.Size(72, 56)
        Me.Button19.TabIndex = 947
        Me.Button19.Text = "-5M"
        Me.Button19.UseVisualStyleBackColor = False
        '
        'Button20
        '
        Me.Button20.AutoSize = True
        Me.Button20.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button20.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button20, 2)
        Me.Button20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button20.Location = New System.Drawing.Point(667, 233)
        Me.Button20.Margin = New System.Windows.Forms.Padding(1)
        Me.Button20.Name = "Button20"
        Me.Button20.Size = New System.Drawing.Size(72, 56)
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
        Me.TrackBar2.Location = New System.Drawing.Point(297, 291)
        Me.TrackBar2.Margin = New System.Windows.Forms.Padding(1)
        Me.TrackBar2.Maximum = 40
        Me.TrackBar2.Minimum = 5
        Me.TrackBar2.Name = "TrackBar2"
        Me.TrackBar2.Size = New System.Drawing.Size(331, 56)
        Me.TrackBar2.TabIndex = 952
        Me.ToolTip1.SetToolTip(Me.TrackBar2, "再生速度")
        Me.TrackBar2.Value = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label4, 3)
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label4.Location = New System.Drawing.Point(629, 290)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(111, 58)
        Me.Label4.TabIndex = 954
        Me.Label4.Text = "x1.0"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button21
        '
        Me.Button21.AutoSize = True
        Me.Button21.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button21.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button21, 2)
        Me.Button21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button21.Location = New System.Drawing.Point(223, 349)
        Me.Button21.Margin = New System.Windows.Forms.Padding(1)
        Me.Button21.Name = "Button21"
        Me.Button21.Size = New System.Drawing.Size(72, 61)
        Me.Button21.TabIndex = 956
        Me.Button21.Text = "0.5"
        Me.Button21.UseVisualStyleBackColor = False
        '
        'Button22
        '
        Me.Button22.AutoSize = True
        Me.Button22.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button22.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button22, 2)
        Me.Button22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button22.Location = New System.Drawing.Point(297, 349)
        Me.Button22.Margin = New System.Windows.Forms.Padding(1)
        Me.Button22.Name = "Button22"
        Me.Button22.Size = New System.Drawing.Size(72, 61)
        Me.Button22.TabIndex = 957
        Me.Button22.Text = "1.0"
        Me.Button22.UseVisualStyleBackColor = False
        '
        'Button23
        '
        Me.Button23.AutoSize = True
        Me.Button23.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button23.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button23, 2)
        Me.Button23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button23.Location = New System.Drawing.Point(371, 349)
        Me.Button23.Margin = New System.Windows.Forms.Padding(1)
        Me.Button23.Name = "Button23"
        Me.Button23.Size = New System.Drawing.Size(72, 61)
        Me.Button23.TabIndex = 958
        Me.Button23.Text = "1.1"
        Me.Button23.UseVisualStyleBackColor = False
        '
        'Button24
        '
        Me.Button24.AutoSize = True
        Me.Button24.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button24.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button24, 2)
        Me.Button24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button24.Location = New System.Drawing.Point(445, 349)
        Me.Button24.Margin = New System.Windows.Forms.Padding(1)
        Me.Button24.Name = "Button24"
        Me.Button24.Size = New System.Drawing.Size(72, 61)
        Me.Button24.TabIndex = 959
        Me.Button24.Text = "1.2"
        Me.Button24.UseVisualStyleBackColor = False
        '
        'Button25
        '
        Me.Button25.AutoSize = True
        Me.Button25.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button25.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button25, 2)
        Me.Button25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button25.Location = New System.Drawing.Point(519, 349)
        Me.Button25.Margin = New System.Windows.Forms.Padding(1)
        Me.Button25.Name = "Button25"
        Me.Button25.Size = New System.Drawing.Size(72, 61)
        Me.Button25.TabIndex = 960
        Me.Button25.Text = "1.3"
        Me.Button25.UseVisualStyleBackColor = False
        '
        'Button26
        '
        Me.Button26.AutoSize = True
        Me.Button26.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button26.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button26, 2)
        Me.Button26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button26.Location = New System.Drawing.Point(593, 349)
        Me.Button26.Margin = New System.Windows.Forms.Padding(1)
        Me.Button26.Name = "Button26"
        Me.Button26.Size = New System.Drawing.Size(72, 61)
        Me.Button26.TabIndex = 961
        Me.Button26.Text = "1.4"
        Me.Button26.UseVisualStyleBackColor = False
        '
        'Button27
        '
        Me.Button27.AutoSize = True
        Me.Button27.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button27.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button27, 2)
        Me.Button27.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button27.Location = New System.Drawing.Point(667, 349)
        Me.Button27.Margin = New System.Windows.Forms.Padding(1)
        Me.Button27.Name = "Button27"
        Me.Button27.Size = New System.Drawing.Size(72, 61)
        Me.Button27.TabIndex = 962
        Me.Button27.Text = "1.5"
        Me.Button27.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label6, 2)
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label6.Location = New System.Drawing.Point(222, 290)
        Me.Label6.Margin = New System.Windows.Forms.Padding(0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 58)
        Me.Label6.TabIndex = 951
        Me.Label6.Text = "速度"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button39
        '
        Me.Button39.AutoSize = True
        Me.Button39.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button39.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button39, 2)
        Me.Button39.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button39.Image = Global.OkoshiMAX.My.Resources.Resources.OpenFolder_16x
        Me.Button39.Location = New System.Drawing.Point(482, 59)
        Me.Button39.Margin = New System.Windows.Forms.Padding(1)
        Me.Button39.Name = "Button39"
        Me.Button39.Size = New System.Drawing.Size(72, 56)
        Me.Button39.TabIndex = 927
        Me.ToolTip1.SetToolTip(Me.Button39, "ファイルを開く")
        Me.Button39.UseVisualStyleBackColor = False
        '
        'Button37
        '
        Me.Button37.AutoSize = True
        Me.Button37.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button37.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button37, 2)
        Me.Button37.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button37.Image = Global.OkoshiMAX.My.Resources.Resources.SettingsOutline_16x
        Me.Button37.Location = New System.Drawing.Point(704, 59)
        Me.Button37.Margin = New System.Windows.Forms.Padding(1)
        Me.Button37.Name = "Button37"
        Me.Button37.Size = New System.Drawing.Size(96, 56)
        Me.Button37.TabIndex = 924
        Me.ToolTip1.SetToolTip(Me.Button37, "設定")
        Me.Button37.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Image = Global.OkoshiMAX.My.Resources.Resources.Volume_16x
        Me.Label2.Location = New System.Drawing.Point(740, 116)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 58)
        Me.Label2.TabIndex = 969
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TrackBar6
        '
        Me.TrackBar6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.TrackBar6.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TrackBar6.LargeChange = 1
        Me.TrackBar6.Location = New System.Drawing.Point(748, 174)
        Me.TrackBar6.Margin = New System.Windows.Forms.Padding(0)
        Me.TrackBar6.Maximum = 100
        Me.TrackBar6.Name = "TrackBar6"
        Me.TrackBar6.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TableLayoutPanel1.SetRowSpan(Me.TrackBar6, 3)
        Me.TrackBar6.Size = New System.Drawing.Size(45, 174)
        Me.TrackBar6.TabIndex = 950
        Me.TrackBar6.TickFrequency = 10
        Me.TrackBar6.TickStyle = System.Windows.Forms.TickStyle.Both
        Me.ToolTip1.SetToolTip(Me.TrackBar6, "音量")
        Me.TrackBar6.Value = 60
        '
        'Button36
        '
        Me.Button36.AutoSize = True
        Me.Button36.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button36.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button36, 2)
        Me.Button36.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button36.Enabled = False
        Me.Button36.Image = Global.OkoshiMAX.My.Resources.Resources.CaptureFrame_16x
        Me.Button36.Location = New System.Drawing.Point(75, 1)
        Me.Button36.Margin = New System.Windows.Forms.Padding(1)
        Me.Button36.Name = "Button36"
        Me.Button36.Size = New System.Drawing.Size(72, 56)
        Me.Button36.TabIndex = 925
        Me.ToolTip1.SetToolTip(Me.Button36, "画面キャプチャ")
        Me.Button36.UseVisualStyleBackColor = False
        '
        'Button200
        '
        Me.Button200.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button200.AutoSize = True
        Me.Button200.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button200.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button200, 3)
        Me.Button200.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button200.Image = Global.OkoshiMAX.My.Resources.Resources.Run_16x
        Me.Button200.Location = New System.Drawing.Point(1, 291)
        Me.Button200.Margin = New System.Windows.Forms.Padding(1)
        Me.Button200.Name = "Button200"
        Me.TableLayoutPanel1.SetRowSpan(Me.Button200, 2)
        Me.Button200.Size = New System.Drawing.Size(109, 119)
        Me.Button200.TabIndex = 963
        Me.ToolTip1.SetToolTip(Me.Button200, "再生・一時停止")
        Me.Button200.UseVisualStyleBackColor = False
        '
        'Button400
        '
        Me.Button400.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button400.AutoSize = True
        Me.Button400.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button400.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button400, 3)
        Me.Button400.Image = Global.OkoshiMAX.My.Resources.Resources.Stop_grey_16x
        Me.Button400.Location = New System.Drawing.Point(112, 291)
        Me.Button400.Margin = New System.Windows.Forms.Padding(1)
        Me.Button400.Name = "Button400"
        Me.TableLayoutPanel1.SetRowSpan(Me.Button400, 2)
        Me.Button400.Size = New System.Drawing.Size(109, 119)
        Me.Button400.TabIndex = 964
        Me.ToolTip1.SetToolTip(Me.Button400, "停止")
        Me.Button400.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label5.Location = New System.Drawing.Point(740, 348)
        Me.Label5.Margin = New System.Windows.Forms.Padding(0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 63)
        Me.Label5.TabIndex = 955
        Me.Label5.Text = "100"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ButtonShiori
        '
        Me.ButtonShiori.AutoSize = True
        Me.ButtonShiori.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ButtonShiori.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.ButtonShiori, 2)
        Me.ButtonShiori.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonShiori.Image = Global.OkoshiMAX.My.Resources.Resources.Bookmark_16x
        Me.ButtonShiori.Location = New System.Drawing.Point(630, 59)
        Me.ButtonShiori.Margin = New System.Windows.Forms.Padding(1)
        Me.ButtonShiori.Name = "ButtonShiori"
        Me.ButtonShiori.Size = New System.Drawing.Size(72, 56)
        Me.ButtonShiori.TabIndex = 967
        Me.ToolTip1.SetToolTip(Me.ButtonShiori, "しおり一覧")
        Me.ButtonShiori.UseVisualStyleBackColor = False
        '
        'Button35
        '
        Me.Button35.AutoSize = True
        Me.Button35.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button35.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button35, 2)
        Me.Button35.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button35.Image = Global.OkoshiMAX.My.Resources.Resources.PinnedItem_16x
        Me.Button35.Location = New System.Drawing.Point(556, 59)
        Me.Button35.Margin = New System.Windows.Forms.Padding(1)
        Me.Button35.Name = "Button35"
        Me.Button35.Size = New System.Drawing.Size(72, 56)
        Me.Button35.TabIndex = 971
        Me.ToolTip1.SetToolTip(Me.Button35, "最前面に表示")
        Me.Button35.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.AutoSize = True
        Me.Button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button1, 2)
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button1.Location = New System.Drawing.Point(1, 117)
        Me.Button1.Margin = New System.Windows.Forms.Padding(1)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 56)
        Me.Button1.TabIndex = 940
        Me.Button1.Text = "+1S"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.AutoSize = True
        Me.Button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button2, 2)
        Me.Button2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button2.Location = New System.Drawing.Point(75, 117)
        Me.Button2.Margin = New System.Windows.Forms.Padding(1)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(72, 56)
        Me.Button2.TabIndex = 928
        Me.Button2.Text = "+3S"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button12
        '
        Me.Button12.AutoSize = True
        Me.Button12.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button12.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button12, 2)
        Me.Button12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button12.Location = New System.Drawing.Point(75, 233)
        Me.Button12.Margin = New System.Windows.Forms.Padding(1)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(72, 56)
        Me.Button12.TabIndex = 929
        Me.Button12.Text = "-3S"
        Me.Button12.UseVisualStyleBackColor = False
        '
        'Button30
        '
        Me.Button30.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button30.Location = New System.Drawing.Point(446, 2)
        Me.Button30.Margin = New System.Windows.Forms.Padding(2)
        Me.Button30.Name = "Button30"
        Me.Button30.Size = New System.Drawing.Size(33, 54)
        Me.Button30.TabIndex = 973
        Me.Button30.Text = "Button30"
        Me.Button30.UseVisualStyleBackColor = True
        '
        'Button38
        '
        Me.Button38.Location = New System.Drawing.Point(446, 60)
        Me.Button38.Margin = New System.Windows.Forms.Padding(2)
        Me.Button38.Name = "Button38"
        Me.Button38.Size = New System.Drawing.Size(33, 18)
        Me.Button38.TabIndex = 974
        Me.Button38.Text = "Button38"
        Me.Button38.UseVisualStyleBackColor = True
        '
        'Button40
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.Button40, 2)
        Me.Button40.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button40.Location = New System.Drawing.Point(2, 60)
        Me.Button40.Margin = New System.Windows.Forms.Padding(2)
        Me.Button40.Name = "Button40"
        Me.Button40.Size = New System.Drawing.Size(70, 54)
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
        Me.TableLayoutPanel2.Controls.Add(Me.DataGridView1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Button28, 3, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Button29, 2, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Button32, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Button31, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.TextBox3, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Button34, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(252, 670)
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
        Me.DataGridView1.Location = New System.Drawing.Point(1, 1)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(1)
        Me.DataGridView1.MinimumSize = New System.Drawing.Size(160, 80)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 25
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(250, 604)
        Me.DataGridView1.TabIndex = 149
        '
        'colCounter
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.colCounter.DefaultCellStyle = DataGridViewCellStyle1
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
        Me.Button28.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button28.Image = Global.OkoshiMAX.My.Resources.Resources.Cancel_16x
        Me.Button28.Location = New System.Drawing.Point(190, 639)
        Me.Button28.Margin = New System.Windows.Forms.Padding(1)
        Me.Button28.Name = "Button28"
        Me.Button28.Size = New System.Drawing.Size(61, 30)
        Me.Button28.TabIndex = 150
        Me.ToolTip1.SetToolTip(Me.Button28, "一覧から削除")
        Me.Button28.UseVisualStyleBackColor = False
        '
        'Button29
        '
        Me.Button29.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button29.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button29.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button29.Image = Global.OkoshiMAX.My.Resources.Resources.Add_16x
        Me.Button29.Location = New System.Drawing.Point(127, 639)
        Me.Button29.Margin = New System.Windows.Forms.Padding(1)
        Me.Button29.Name = "Button29"
        Me.Button29.Size = New System.Drawing.Size(61, 30)
        Me.Button29.TabIndex = 151
        Me.ToolTip1.SetToolTip(Me.Button29, "しおりを追加")
        Me.Button29.UseVisualStyleBackColor = False
        '
        'Button32
        '
        Me.Button32.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button32.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button32.Image = Global.OkoshiMAX.My.Resources.Resources.DestinationAssistant_16x
        Me.Button32.Location = New System.Drawing.Point(64, 639)
        Me.Button32.Margin = New System.Windows.Forms.Padding(1)
        Me.Button32.Name = "Button32"
        Me.Button32.Size = New System.Drawing.Size(61, 30)
        Me.Button32.TabIndex = 152
        Me.ToolTip1.SetToolTip(Me.Button32, "CSV・Wordファイル読み込み")
        Me.Button32.UseVisualStyleBackColor = False
        '
        'Button31
        '
        Me.Button31.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button31.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button31.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button31.Image = Global.OkoshiMAX.My.Resources.Resources.DownloadDocument_16x
        Me.Button31.Location = New System.Drawing.Point(1, 639)
        Me.Button31.Margin = New System.Windows.Forms.Padding(1)
        Me.Button31.Name = "Button31"
        Me.Button31.Size = New System.Drawing.Size(61, 30)
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
        Me.TextBox3.Location = New System.Drawing.Point(64, 607)
        Me.TextBox3.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(187, 19)
        Me.TextBox3.TabIndex = 156
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button34
        '
        Me.Button34.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button34.Location = New System.Drawing.Point(1, 607)
        Me.Button34.Margin = New System.Windows.Forms.Padding(1)
        Me.Button34.Name = "Button34"
        Me.Button34.Size = New System.Drawing.Size(61, 30)
        Me.Button34.TabIndex = 157
        Me.Button34.Text = ">>"
        Me.ToolTip1.SetToolTip(Me.Button34, "カウンタを手動入力して追加")
        Me.Button34.UseVisualStyleBackColor = False
        '
        'MainPlayerForm
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1284, 670)
        Me.Controls.Add(Me.SplitContainer1)
        Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.OkoshiMAX.My.MySettings.Default, "MyLocation", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = Global.OkoshiMAX.My.MySettings.Default.MyLocation
        Me.Margin = New System.Windows.Forms.Padding(1)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(514, 196)
        Me.Name = "MainPlayerForm"
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
        Me.SplitContainer3.Panel2.PerformLayout()
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
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PageSetupDialog1 As PageSetupDialog
    Friend WithEvents Timer1 As Timer
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents MpvPanel As Panel
    Friend WithEvents Button34 As Button
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Button32 As Button
    Friend WithEvents Button31 As Button
    Friend WithEvents Button28 As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents Button29 As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents CheckBoxMpvPamel As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button33 As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button37 As Button
    Friend WithEvents Button36 As Button
    Friend WithEvents Button39 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button12 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents Button9 As Button
    Friend WithEvents Button10 As Button
    Friend WithEvents TrackBar1 As TrackBar
    Friend WithEvents Button11 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Button13 As Button
    Friend WithEvents Button14 As Button
    Friend WithEvents Button15 As Button
    Friend WithEvents Button16 As Button
    Friend WithEvents Button17 As Button
    Friend WithEvents Button18 As Button
    Friend WithEvents Button19 As Button
    Friend WithEvents Button20 As Button
    Friend WithEvents TrackBar6 As TrackBar
    Friend WithEvents Label6 As Label
    Friend WithEvents TrackBar2 As TrackBar
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Button21 As Button
    Friend WithEvents Button22 As Button
    Friend WithEvents Button23 As Button
    Friend WithEvents Button24 As Button
    Friend WithEvents Button25 As Button
    Friend WithEvents Button26 As Button
    Friend WithEvents Button27 As Button
    Friend WithEvents Button200 As Button
    Friend WithEvents Button400 As Button
    Friend WithEvents ButtonShiori As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Button35 As Button
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents File_Namae As DataGridViewTextBoxColumn
    Friend WithEvents File_Length As DataGridViewTextBoxColumn
    Friend WithEvents File_Memo As DataGridViewTextBoxColumn
    Friend WithEvents File_Delete As DataGridViewTextBoxColumn
    Friend WithEvents File_Position As DataGridViewTextBoxColumn
    Friend WithEvents File_Progress As DataGridViewTextBoxColumn
    Friend WithEvents Button30 As Button
    Friend WithEvents Button38 As Button
    Friend WithEvents Button40 As Button
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Button41 As Button
    Friend WithEvents Button42 As Button
    Friend WithEvents Button43 As Button
    Friend WithEvents Button44 As Button
    Friend WithEvents colCounter As DataGridViewButtonColumn
    Friend WithEvents colMemo As DataGridViewTextBoxColumn
    Friend WithEvents colPosition As DataGridViewTextBoxColumn
    Friend WithEvents colDelete As DataGridViewButtonColumn
End Class
