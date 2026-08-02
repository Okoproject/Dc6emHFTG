<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EqualizerForm
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label125 = New System.Windows.Forms.Label()
        Me.Label250 = New System.Windows.Forms.Label()
        Me.Label500 = New System.Windows.Forms.Label()
        Me.Label1k = New System.Windows.Forms.Label()
        Me.Label2k = New System.Windows.Forms.Label()
        Me.Label4k = New System.Windows.Forms.Label()
        Me.Label8k = New System.Windows.Forms.Label()
        Me.Label16k = New System.Windows.Forms.Label()
        Me.TrackBar31 = New System.Windows.Forms.TrackBar()
        Me.TrackBar62 = New System.Windows.Forms.TrackBar()
        Me.TrackBar125 = New System.Windows.Forms.TrackBar()
        Me.TrackBar250 = New System.Windows.Forms.TrackBar()
        Me.TrackBar500 = New System.Windows.Forms.TrackBar()
        Me.TrackBar1k = New System.Windows.Forms.TrackBar()
        Me.TrackBar2k = New System.Windows.Forms.TrackBar()
        Me.TrackBar4k = New System.Windows.Forms.TrackBar()
        Me.TrackBar8k = New System.Windows.Forms.TrackBar()
        Me.TrackBar16k = New System.Windows.Forms.TrackBar()
        Me.LabelGain31 = New System.Windows.Forms.Label()
        Me.LabelGain62 = New System.Windows.Forms.Label()
        Me.LabelGain125 = New System.Windows.Forms.Label()
        Me.LabelGain250 = New System.Windows.Forms.Label()
        Me.LabelGain500 = New System.Windows.Forms.Label()
        Me.LabelGain1k = New System.Windows.Forms.Label()
        Me.LabelGain2k = New System.Windows.Forms.Label()
        Me.LabelGain4k = New System.Windows.Forms.Label()
        Me.LabelGain8k = New System.Windows.Forms.Label()
        Me.LabelGain16k = New System.Windows.Forms.Label()
        Me.ButtonReset = New System.Windows.Forms.Button()
        Me.ButtonNC = New System.Windows.Forms.Button()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.LabelCH = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.ButtonMono = New System.Windows.Forms.Button()
        Me.PanelSpectrum = New System.Windows.Forms.Panel()
        Me.TimerSpectrum = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.TrackBar31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar62, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar125, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar250, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar500, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar1k, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar2k, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar4k, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar8k, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar16k, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 12
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label31, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label62, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label125, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label250, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label500, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1k, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2k, 7, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4k, 8, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label8k, 9, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label16k, 10, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TrackBar31, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TrackBar62, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TrackBar125, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TrackBar250, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TrackBar500, 5, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TrackBar1k, 6, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TrackBar2k, 7, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TrackBar4k, 8, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TrackBar8k, 9, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TrackBar16k, 10, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelGain31, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelGain62, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelGain125, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelGain250, 4, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelGain500, 5, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelGain1k, 6, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelGain2k, 7, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelGain4k, 8, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelGain8k, 9, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelGain16k, 10, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonReset, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonNC, 5, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonClose, 11, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelCH, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.CheckBox1, 2, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.CheckBox2, 4, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.CheckBox3, 6, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.CheckBox4, 8, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonMono, 10, 4)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 350.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(864, 485)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label31
        '
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label31.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.Label31.Location = New System.Drawing.Point(83, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(62, 30)
        Me.Label31.TabIndex = 0
        Me.Label31.Text = "31"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label62
        '
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label62.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.Label62.Location = New System.Drawing.Point(151, 0)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(62, 30)
        Me.Label62.TabIndex = 1
        Me.Label62.Text = "62"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label125
        '
        Me.Label125.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label125.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.Label125.Location = New System.Drawing.Point(219, 0)
        Me.Label125.Name = "Label125"
        Me.Label125.Size = New System.Drawing.Size(62, 30)
        Me.Label125.TabIndex = 2
        Me.Label125.Text = "125"
        Me.Label125.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label250
        '
        Me.Label250.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label250.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.Label250.Location = New System.Drawing.Point(287, 0)
        Me.Label250.Name = "Label250"
        Me.Label250.Size = New System.Drawing.Size(62, 30)
        Me.Label250.TabIndex = 3
        Me.Label250.Text = "250"
        Me.Label250.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label500
        '
        Me.Label500.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label500.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.Label500.Location = New System.Drawing.Point(355, 0)
        Me.Label500.Name = "Label500"
        Me.Label500.Size = New System.Drawing.Size(62, 30)
        Me.Label500.TabIndex = 4
        Me.Label500.Text = "500"
        Me.Label500.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label1k
        '
        Me.Label1k.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1k.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.Label1k.Location = New System.Drawing.Point(423, 0)
        Me.Label1k.Name = "Label1k"
        Me.Label1k.Size = New System.Drawing.Size(62, 30)
        Me.Label1k.TabIndex = 5
        Me.Label1k.Text = "1k"
        Me.Label1k.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label2k
        '
        Me.Label2k.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2k.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.Label2k.Location = New System.Drawing.Point(491, 0)
        Me.Label2k.Name = "Label2k"
        Me.Label2k.Size = New System.Drawing.Size(62, 30)
        Me.Label2k.TabIndex = 6
        Me.Label2k.Text = "2k"
        Me.Label2k.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label4k
        '
        Me.Label4k.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4k.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.Label4k.Location = New System.Drawing.Point(559, 0)
        Me.Label4k.Name = "Label4k"
        Me.Label4k.Size = New System.Drawing.Size(62, 30)
        Me.Label4k.TabIndex = 7
        Me.Label4k.Text = "4k"
        Me.Label4k.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label8k
        '
        Me.Label8k.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8k.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.Label8k.Location = New System.Drawing.Point(627, 0)
        Me.Label8k.Name = "Label8k"
        Me.Label8k.Size = New System.Drawing.Size(62, 30)
        Me.Label8k.TabIndex = 8
        Me.Label8k.Text = "8k"
        Me.Label8k.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label16k
        '
        Me.Label16k.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label16k.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.Label16k.Location = New System.Drawing.Point(695, 0)
        Me.Label16k.Name = "Label16k"
        Me.Label16k.Size = New System.Drawing.Size(62, 30)
        Me.Label16k.TabIndex = 9
        Me.Label16k.Text = "16k"
        Me.Label16k.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'TrackBar31
        '
        Me.TrackBar31.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TrackBar31.LargeChange = 10
        Me.TrackBar31.Location = New System.Drawing.Point(83, 32)
        Me.TrackBar31.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TrackBar31.Maximum = 120
        Me.TrackBar31.Minimum = -120
        Me.TrackBar31.Name = "TrackBar31"
        Me.TrackBar31.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBar31.Size = New System.Drawing.Size(62, 346)
        Me.TrackBar31.SmallChange = 10
        Me.TrackBar31.TabIndex = 10
        Me.TrackBar31.TickFrequency = 10
        '
        'TrackBar62
        '
        Me.TrackBar62.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TrackBar62.LargeChange = 10
        Me.TrackBar62.Location = New System.Drawing.Point(151, 32)
        Me.TrackBar62.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TrackBar62.Maximum = 120
        Me.TrackBar62.Minimum = -120
        Me.TrackBar62.Name = "TrackBar62"
        Me.TrackBar62.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBar62.Size = New System.Drawing.Size(62, 346)
        Me.TrackBar62.SmallChange = 10
        Me.TrackBar62.TabIndex = 11
        Me.TrackBar62.TickFrequency = 10
        '
        'TrackBar125
        '
        Me.TrackBar125.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TrackBar125.LargeChange = 10
        Me.TrackBar125.Location = New System.Drawing.Point(219, 32)
        Me.TrackBar125.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TrackBar125.Maximum = 120
        Me.TrackBar125.Minimum = -120
        Me.TrackBar125.Name = "TrackBar125"
        Me.TrackBar125.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBar125.Size = New System.Drawing.Size(62, 346)
        Me.TrackBar125.SmallChange = 10
        Me.TrackBar125.TabIndex = 12
        Me.TrackBar125.TickFrequency = 10
        '
        'TrackBar250
        '
        Me.TrackBar250.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TrackBar250.LargeChange = 10
        Me.TrackBar250.Location = New System.Drawing.Point(287, 32)
        Me.TrackBar250.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TrackBar250.Maximum = 120
        Me.TrackBar250.Minimum = -120
        Me.TrackBar250.Name = "TrackBar250"
        Me.TrackBar250.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBar250.Size = New System.Drawing.Size(62, 346)
        Me.TrackBar250.SmallChange = 10
        Me.TrackBar250.TabIndex = 13
        Me.TrackBar250.TickFrequency = 10
        '
        'TrackBar500
        '
        Me.TrackBar500.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TrackBar500.LargeChange = 10
        Me.TrackBar500.Location = New System.Drawing.Point(355, 32)
        Me.TrackBar500.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TrackBar500.Maximum = 120
        Me.TrackBar500.Minimum = -120
        Me.TrackBar500.Name = "TrackBar500"
        Me.TrackBar500.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBar500.Size = New System.Drawing.Size(62, 346)
        Me.TrackBar500.SmallChange = 10
        Me.TrackBar500.TabIndex = 14
        Me.TrackBar500.TickFrequency = 10
        '
        'TrackBar1k
        '
        Me.TrackBar1k.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TrackBar1k.LargeChange = 10
        Me.TrackBar1k.Location = New System.Drawing.Point(423, 32)
        Me.TrackBar1k.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TrackBar1k.Maximum = 120
        Me.TrackBar1k.Minimum = -120
        Me.TrackBar1k.Name = "TrackBar1k"
        Me.TrackBar1k.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBar1k.Size = New System.Drawing.Size(62, 346)
        Me.TrackBar1k.SmallChange = 10
        Me.TrackBar1k.TabIndex = 15
        Me.TrackBar1k.TickFrequency = 10
        '
        'TrackBar2k
        '
        Me.TrackBar2k.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TrackBar2k.LargeChange = 10
        Me.TrackBar2k.Location = New System.Drawing.Point(491, 32)
        Me.TrackBar2k.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TrackBar2k.Maximum = 120
        Me.TrackBar2k.Minimum = -120
        Me.TrackBar2k.Name = "TrackBar2k"
        Me.TrackBar2k.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBar2k.Size = New System.Drawing.Size(62, 346)
        Me.TrackBar2k.SmallChange = 10
        Me.TrackBar2k.TabIndex = 16
        Me.TrackBar2k.TickFrequency = 10
        '
        'TrackBar4k
        '
        Me.TrackBar4k.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TrackBar4k.LargeChange = 10
        Me.TrackBar4k.Location = New System.Drawing.Point(559, 32)
        Me.TrackBar4k.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TrackBar4k.Maximum = 120
        Me.TrackBar4k.Minimum = -120
        Me.TrackBar4k.Name = "TrackBar4k"
        Me.TrackBar4k.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBar4k.Size = New System.Drawing.Size(62, 346)
        Me.TrackBar4k.SmallChange = 10
        Me.TrackBar4k.TabIndex = 17
        Me.TrackBar4k.TickFrequency = 10
        '
        'TrackBar8k
        '
        Me.TrackBar8k.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TrackBar8k.LargeChange = 10
        Me.TrackBar8k.Location = New System.Drawing.Point(627, 32)
        Me.TrackBar8k.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TrackBar8k.Maximum = 120
        Me.TrackBar8k.Minimum = -120
        Me.TrackBar8k.Name = "TrackBar8k"
        Me.TrackBar8k.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBar8k.Size = New System.Drawing.Size(62, 346)
        Me.TrackBar8k.SmallChange = 10
        Me.TrackBar8k.TabIndex = 18
        Me.TrackBar8k.TickFrequency = 10
        '
        'TrackBar16k
        '
        Me.TrackBar16k.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TrackBar16k.LargeChange = 10
        Me.TrackBar16k.Location = New System.Drawing.Point(695, 32)
        Me.TrackBar16k.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TrackBar16k.Maximum = 120
        Me.TrackBar16k.Minimum = -120
        Me.TrackBar16k.Name = "TrackBar16k"
        Me.TrackBar16k.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBar16k.Size = New System.Drawing.Size(62, 346)
        Me.TrackBar16k.SmallChange = 10
        Me.TrackBar16k.TabIndex = 19
        Me.TrackBar16k.TickFrequency = 10
        '
        'LabelGain31
        '
        Me.LabelGain31.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelGain31.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.LabelGain31.Location = New System.Drawing.Point(83, 380)
        Me.LabelGain31.Name = "LabelGain31"
        Me.LabelGain31.Size = New System.Drawing.Size(62, 30)
        Me.LabelGain31.TabIndex = 20
        Me.LabelGain31.Text = "0"
        Me.LabelGain31.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LabelGain62
        '
        Me.LabelGain62.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelGain62.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.LabelGain62.Location = New System.Drawing.Point(151, 380)
        Me.LabelGain62.Name = "LabelGain62"
        Me.LabelGain62.Size = New System.Drawing.Size(62, 30)
        Me.LabelGain62.TabIndex = 21
        Me.LabelGain62.Text = "0"
        Me.LabelGain62.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LabelGain125
        '
        Me.LabelGain125.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelGain125.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.LabelGain125.Location = New System.Drawing.Point(219, 380)
        Me.LabelGain125.Name = "LabelGain125"
        Me.LabelGain125.Size = New System.Drawing.Size(62, 30)
        Me.LabelGain125.TabIndex = 22
        Me.LabelGain125.Text = "0"
        Me.LabelGain125.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LabelGain250
        '
        Me.LabelGain250.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelGain250.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.LabelGain250.Location = New System.Drawing.Point(287, 380)
        Me.LabelGain250.Name = "LabelGain250"
        Me.LabelGain250.Size = New System.Drawing.Size(62, 30)
        Me.LabelGain250.TabIndex = 23
        Me.LabelGain250.Text = "0"
        Me.LabelGain250.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LabelGain500
        '
        Me.LabelGain500.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelGain500.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.LabelGain500.Location = New System.Drawing.Point(355, 380)
        Me.LabelGain500.Name = "LabelGain500"
        Me.LabelGain500.Size = New System.Drawing.Size(62, 30)
        Me.LabelGain500.TabIndex = 24
        Me.LabelGain500.Text = "0"
        Me.LabelGain500.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LabelGain1k
        '
        Me.LabelGain1k.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelGain1k.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.LabelGain1k.Location = New System.Drawing.Point(423, 380)
        Me.LabelGain1k.Name = "LabelGain1k"
        Me.LabelGain1k.Size = New System.Drawing.Size(62, 30)
        Me.LabelGain1k.TabIndex = 25
        Me.LabelGain1k.Text = "0"
        Me.LabelGain1k.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LabelGain2k
        '
        Me.LabelGain2k.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelGain2k.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.LabelGain2k.Location = New System.Drawing.Point(491, 380)
        Me.LabelGain2k.Name = "LabelGain2k"
        Me.LabelGain2k.Size = New System.Drawing.Size(62, 30)
        Me.LabelGain2k.TabIndex = 26
        Me.LabelGain2k.Text = "0"
        Me.LabelGain2k.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LabelGain4k
        '
        Me.LabelGain4k.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelGain4k.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.LabelGain4k.Location = New System.Drawing.Point(559, 380)
        Me.LabelGain4k.Name = "LabelGain4k"
        Me.LabelGain4k.Size = New System.Drawing.Size(62, 30)
        Me.LabelGain4k.TabIndex = 27
        Me.LabelGain4k.Text = "0"
        Me.LabelGain4k.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LabelGain8k
        '
        Me.LabelGain8k.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelGain8k.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.LabelGain8k.Location = New System.Drawing.Point(627, 380)
        Me.LabelGain8k.Name = "LabelGain8k"
        Me.LabelGain8k.Size = New System.Drawing.Size(62, 30)
        Me.LabelGain8k.TabIndex = 28
        Me.LabelGain8k.Text = "0"
        Me.LabelGain8k.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LabelGain16k
        '
        Me.LabelGain16k.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelGain16k.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.LabelGain16k.Location = New System.Drawing.Point(695, 380)
        Me.LabelGain16k.Name = "LabelGain16k"
        Me.LabelGain16k.Size = New System.Drawing.Size(62, 30)
        Me.LabelGain16k.TabIndex = 29
        Me.LabelGain16k.Text = "0"
        Me.LabelGain16k.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'ButtonReset
        '
        Me.ButtonReset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonReset.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonReset.Location = New System.Drawing.Point(3, 412)
        Me.ButtonReset.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonReset.Name = "ButtonReset"
        Me.ButtonReset.Size = New System.Drawing.Size(74, 41)
        Me.ButtonReset.TabIndex = 30
        Me.ButtonReset.Text = "リセット"
        Me.ButtonReset.UseVisualStyleBackColor = True
        '
        'ButtonNC
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.ButtonNC, 2)
        Me.ButtonNC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonNC.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonNC.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonNC.Location = New System.Drawing.Point(355, 412)
        Me.ButtonNC.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonNC.Name = "ButtonNC"
        Me.ButtonNC.Size = New System.Drawing.Size(130, 41)
        Me.ButtonNC.TabIndex = 32
        Me.ButtonNC.Text = "ノイキャン"
        Me.ButtonNC.UseVisualStyleBackColor = True
        '
        'ButtonClose
        '
        Me.ButtonClose.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonClose.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonClose.Location = New System.Drawing.Point(763, 412)
        Me.ButtonClose.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(98, 41)
        Me.ButtonClose.TabIndex = 31
        Me.ButtonClose.Text = "閉じる"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'LabelCH
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.LabelCH, 2)
        Me.LabelCH.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelCH.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.LabelCH.Location = New System.Drawing.Point(3, 455)
        Me.LabelCH.Name = "LabelCH"
        Me.LabelCH.Size = New System.Drawing.Size(142, 30)
        Me.LabelCH.TabIndex = 33
        Me.LabelCH.Text = "CHルーティング"
        Me.LabelCH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.CheckBox1, 2)
        Me.CheckBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBox1.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.CheckBox1.Location = New System.Drawing.Point(151, 458)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(130, 24)
        Me.CheckBox1.TabIndex = 34
        Me.CheckBox1.Text = "L→L"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.CheckBox2, 2)
        Me.CheckBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBox2.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.CheckBox2.Location = New System.Drawing.Point(287, 458)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(130, 24)
        Me.CheckBox2.TabIndex = 35
        Me.CheckBox2.Text = "L→R"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.CheckBox3, 2)
        Me.CheckBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBox3.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.CheckBox3.Location = New System.Drawing.Point(423, 458)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(130, 24)
        Me.CheckBox3.TabIndex = 36
        Me.CheckBox3.Text = "R→R"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.CheckBox4, 2)
        Me.CheckBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBox4.Font = New System.Drawing.Font("MS UI Gothic", 8.0!)
        Me.CheckBox4.Location = New System.Drawing.Point(559, 458)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(130, 24)
        Me.CheckBox4.TabIndex = 37
        Me.CheckBox4.Text = "R→L"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'ButtonMono
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.ButtonMono, 2)
        Me.ButtonMono.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonMono.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonMono.Font = New System.Drawing.Font("MS UI Gothic", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonMono.Location = New System.Drawing.Point(695, 458)
        Me.ButtonMono.Name = "ButtonMono"
        Me.ButtonMono.Size = New System.Drawing.Size(166, 24)
        Me.ButtonMono.TabIndex = 38
        Me.ButtonMono.Text = "疑似モノラル"
        Me.ButtonMono.UseVisualStyleBackColor = True
        '
        'PanelSpectrum
        '
        Me.PanelSpectrum.BackColor = System.Drawing.Color.Black
        Me.PanelSpectrum.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelSpectrum.Location = New System.Drawing.Point(0, 485)
        Me.PanelSpectrum.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelSpectrum.Name = "PanelSpectrum"
        Me.PanelSpectrum.Size = New System.Drawing.Size(864, 199)
        Me.PanelSpectrum.TabIndex = 1
        '
        'TimerSpectrum
        '
        Me.TimerSpectrum.Interval = 33
        '
        'EqualizerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(864, 684)
        Me.Controls.Add(Me.PanelSpectrum)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "EqualizerForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "イコライザー"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.TrackBar31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar62, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar125, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar250, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar500, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar1k, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar2k, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar4k, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar8k, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar16k, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Label31 As Label
    Friend WithEvents Label62 As Label
    Friend WithEvents Label125 As Label
    Friend WithEvents Label250 As Label
    Friend WithEvents Label500 As Label
    Friend WithEvents Label1k As Label
    Friend WithEvents Label2k As Label
    Friend WithEvents Label4k As Label
    Friend WithEvents Label8k As Label
    Friend WithEvents Label16k As Label
    Friend WithEvents TrackBar31 As TrackBar
    Friend WithEvents TrackBar62 As TrackBar
    Friend WithEvents TrackBar125 As TrackBar
    Friend WithEvents TrackBar250 As TrackBar
    Friend WithEvents TrackBar500 As TrackBar
    Friend WithEvents TrackBar1k As TrackBar
    Friend WithEvents TrackBar2k As TrackBar
    Friend WithEvents TrackBar4k As TrackBar
    Friend WithEvents TrackBar8k As TrackBar
    Friend WithEvents TrackBar16k As TrackBar
    Friend WithEvents LabelGain31 As Label
    Friend WithEvents LabelGain62 As Label
    Friend WithEvents LabelGain125 As Label
    Friend WithEvents LabelGain250 As Label
    Friend WithEvents LabelGain500 As Label
    Friend WithEvents LabelGain1k As Label
    Friend WithEvents LabelGain2k As Label
    Friend WithEvents LabelGain4k As Label
    Friend WithEvents LabelGain8k As Label
    Friend WithEvents LabelGain16k As Label
    Friend WithEvents ButtonReset As Button
    Friend WithEvents ButtonNC As Button
    Friend WithEvents ButtonClose As Button
    Friend WithEvents PanelSpectrum As Panel
    Friend WithEvents TimerSpectrum As Timer
    Friend WithEvents LabelCH As Label
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents ButtonMono As Button
End Class
