<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VideoSeekBar
    Inherits System.Windows.Forms.UserControl

    'UserControl1 をオーバーライドして dispose を呼び出してください。
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'コンポーネント必要な場合は、このメソッドを追加してください。
    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'VideoSeekBar
        '
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.MinimumSize = New System.Drawing.Size(200, 20)
        Me.Name = "VideoSeekBar"
        Me.Size = New System.Drawing.Size(400, 20)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

End Class
