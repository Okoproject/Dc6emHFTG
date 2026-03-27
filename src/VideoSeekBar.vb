Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

''' <summary>
''' YouTube風のシークバーコントロール
''' </summary>
<DefaultEvent("ValueChanged")>
<ToolboxItem(True)>
Public Class VideoSeekBar
    Inherits UserControl

#Region "プロパティ"

    Private _value As Double = 0
    ''' <summary>
    ''' 現在の再生位置（秒）
    ''' </summary>
    <Category("動作")>
    <Description("現在の再生位置（秒）")>
    Public Property Value As Double
        Get
            Return _value
        End Get
        Set(value As Double)
            _value = Math.Max(0, Math.Min(value, Maximum))
            Invalidate()
        End Set
    End Property

    Private _maximum As Double = 100
    ''' <summary>
    ''' メディアの最大長さ（秒）
    ''' </summary>
    <Category("動作")>
    <Description("メディアの最大長さ（秒）")>
    Public Property Maximum As Double
        Get
            Return _maximum
        End Get
        Set(value As Double)
            _maximum = Math.Max(0, value)
            If _value > _maximum Then _value = _maximum
            Invalidate()
        End Set
    End Property

    Private _bufferedEnd As Double = 0
    ''' <summary>
    ''' バッファリングの終了位置（秒）
    ''' </summary>
    <Category("動作")>
    <Description("バッファリングの終了位置（秒）")>
    Public Property BufferedEnd As Double
        Get
            Return _bufferedEnd
        End Get
        Set(value As Double)
            _bufferedEnd = Math.Max(0, Math.Min(value, Maximum))
            Invalidate()
        End Set
    End Property

    Private _minimum As Double = 0
    ''' <summary>
    ''' 最小値（秒）
    ''' </summary>
    <Category("動作")>
    <Description("最小値（秒）")>
    Public Property Minimum As Double
        Get
            Return _minimum
        End Get
        Set(value As Double)
            _minimum = value
            If _value < _minimum Then _value = _minimum
            Invalidate()
        End Set
    End Property

    Private _playedColor As Color = Color.FromArgb(255, 255, 255)
    ''' <summary>
    ''' 再生済み progress の色
    ''' </summary>
    <Category("外観")>
    <Description("再生済み progress の色")>
    Public Property PlayedColor As Color
        Get
            Return _playedColor
        End Get
        Set(value As Color)
            _playedColor = value
            Invalidate()
        End Set
    End Property

    Private _bufferedColor As Color = Color.FromArgb(128, 128, 128)
    ''' <summary>
    ''' バッファリング progress の色
    ''' </summary>
    <Category("外観")>
    <Description("バッファリング progress の色")>
    Public Property BufferedColor As Color
        Get
            Return _bufferedColor
        End Get
        Set(value As Color)
            _bufferedColor = value
            Invalidate()
        End Set
    End Property

    Private _thumbColor As Color = Color.White
    ''' <summary>
    ''' スライダーの色
    ''' </summary>
    <Category("外観")>
    <Description("スライダーの色")>
    Public Property ThumbColor As Color
        Get
            Return _thumbColor
        End Get
        Set(value As Color)
            _thumbColor = value
            Invalidate()
        End Set
    End Property

    Private _thumbRadius As Integer = 6
    ''' <summary>
    ''' スライダーの半径
    ''' </summary>
    <Category("外観")>
    <Description("スライダーの半径")>
    Public Property ThumbRadius As Integer
        Get
            Return _thumbRadius
        End Get
        Set(value As Integer)
            _thumbRadius = Math.Max(3, Math.Min(value, 20))
            Invalidate()
        End Set
    End Property

    Private _barHeight As Integer = 4
    ''' <summary>
    ''' バーの高さ
    ''' </summary>
    <Category("外観")>
    <Description("バーの高さ")>
    Public Property BarHeight As Integer
        Get
            Return _barHeight
        End Get
        Set(value As Integer)
            _barHeight = Math.Max(2, Math.Min(value, 20))
            Invalidate()
        End Set
    End Property

    Private _showTooltip As Boolean = True
    ''' <summary>
    ''' ツールチップを表示するか
    ''' </summary>
    <Category("動作")>
    <Description("ツールチップを表示するか")>
    Public Property ShowTooltip As Boolean
        Get
            Return _showTooltip
        End Get
        Set(value As Boolean)
            _showTooltip = value
        End Set
    End Property

    Private _isDragging As Boolean = False

#End Region

#Region "イベント"

    ''' <summary>
    ''' 値が変更されたときに発生します。
    ''' </summary>
    <Category("動作")>
    Public Event ValueChanged As EventHandler

    ''' <summary>
    ''' シークを開始したとき（マウスダウン）に発生します。
    ''' </summary>
    <Category("動作")>
    Public Event SeekStarted As EventHandler

    ''' <summary>
    ''' シークが完了したとき（マウスアップ）に発生します。
    ''' </summary>
    <Category("動作")>
    Public Event SeekCompleted As EventHandler

#End Region

    Private ReadOnly _toolTip As New ToolTip()

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.UserPaint Or
                 ControlStyles.DoubleBuffer Or
                 ControlStyles.Selectable,
                 True)
        MinimumSize = New Size(200, 20)
        Size = New Size(400, 20)
        TabStop = True
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim trackRect As New Rectangle(0, (Height - BarHeight) \ 2, Width, BarHeight)
        Dim valueRatio As Double = If(Maximum > Minimum, (Value - Minimum) / (Maximum - Minimum), 0)
        Dim bufferedRatio As Double = If(Maximum > Minimum, (BufferedEnd - Minimum) / (Maximum - Minimum), 0)
        Dim thumbX As Integer = CInt(valueRatio * (Width - 1))

        Using bgBrush As New SolidBrush(Color.FromArgb(80, 80, 80))
            g.FillRectangle(bgBrush, trackRect)
        End Using

        If bufferedRatio > 0 Then
            Dim bufferedWidth As Integer = CInt(Math.Min(bufferedRatio, 1.0) * Width)
            Dim bufferedRect As New Rectangle(0, trackRect.Y, bufferedWidth, BarHeight)
            Using bufferedBrush As New SolidBrush(BufferedColor)
                g.FillRectangle(bufferedBrush, bufferedRect)
            End Using
        End If

        If valueRatio > 0 Then
            Dim playedWidth As Integer = CInt(Math.Min(valueRatio, 1.0) * Width)
            Dim playedRect As New Rectangle(0, trackRect.Y, playedWidth, BarHeight)
            Using playedBrush As New SolidBrush(PlayedColor)
                g.FillRectangle(playedBrush, playedRect)
            End Using
        End If

        Using thumbBrush As New SolidBrush(ThumbColor)
            g.FillEllipse(thumbBrush, thumbX - ThumbRadius, (Height - ThumbRadius * 2) \ 2, ThumbRadius * 2, ThumbRadius * 2)
        End Using
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            _isDragging = True
            Capture = True
            UpdateValueFromMouse(e.X)
            RaiseEvent SeekStarted(Me, EventArgs.Empty)
        End If
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        If _isDragging Then
            UpdateValueFromMouse(e.X)
        ElseIf ShowTooltip Then
            Dim time As Double = GetTimeFromX(e.X)
            Dim tooltipText As String = FormatTime(time)
            _toolTip.Show(tooltipText, Me, e.X, -20)
        End If
        MyBase.OnMouseMove(e)
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        If e.Button = MouseButtons.Left AndAlso _isDragging Then
            _isDragging = False
            Capture = False
            UpdateValueFromMouse(e.X)
            RaiseEvent SeekCompleted(Me, EventArgs.Empty)
        End If
        MyBase.OnMouseUp(e)
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        _toolTip.Hide(Me)
        If _isDragging Then
            _isDragging = False
            Capture = False
            RaiseEvent SeekCompleted(Me, EventArgs.Empty)
        End If
        MyBase.OnMouseLeave(e)
    End Sub

    Protected Overrides Sub OnMouseCaptureChanged(e As EventArgs)
        If Not Capture AndAlso _isDragging Then
            _isDragging = False
            RaiseEvent SeekCompleted(Me, EventArgs.Empty)
        End If
        MyBase.OnMouseCaptureChanged(e)
    End Sub

    Private Sub UpdateValueFromMouse(mouseX As Integer)
        Dim ratio As Double = Math.Max(0, Math.Min(1, mouseX / CDbl(Width - 1)))
        Dim newValue As Double = Minimum + ratio * (Maximum - Minimum)
        If Math.Abs(newValue - Value) > 0.1 Then
            Value = newValue
            RaiseEvent ValueChanged(Me, EventArgs.Empty)
        End If
    End Sub

    Private Function GetTimeFromMouse(mouseX As Integer) As Double
        Dim ratio As Double = Math.Max(0, Math.Min(1, mouseX / CDbl(Width - 1)))
        Return Minimum + ratio * (Maximum - Minimum)
    End Function

    Private Function GetTimeFromX(x As Integer) As Double
        Return GetTimeFromMouse(x)
    End Function

    Private Function FormatTime(seconds As Double) As String
        If seconds < 0 Then seconds = 0
        Dim ts As TimeSpan = TimeSpan.FromSeconds(seconds)
        If ts.TotalHours >= 1 Then
            Return ts.ToString("hh\:mm\:ss")
        Else
            Return ts.ToString("mm\:ss")
        End If
    End Function

    Protected Overrides Function IsInputKey(keyData As Keys) As Boolean
        If keyData = Keys.Left OrElse keyData = Keys.Right OrElse
           keyData = Keys.Up OrElse keyData = Keys.Down Then
            Return True
        End If
        Return MyBase.IsInputKey(keyData)
    End Function

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        Dim step As Double = (Maximum - Minimum) \ 100
        If step < 1 Then step = 1
        Select Case e.KeyCode
            Case Keys.Left, Keys.Down
                Value = Math.Max(Minimum, Value - step)
                RaiseEvent ValueChanged(Me, EventArgs.Empty)
                e.Handled = True
            Case Keys.Right, Keys.Up
                Value = Math.Min(Maximum, Value + step)
                RaiseEvent ValueChanged(Me, EventArgs.Empty)
                e.Handled = True
            Case Keys.Home
                Value = Minimum
                RaiseEvent ValueChanged(Me, EventArgs.Empty)
                e.Handled = True
            Case Keys.End
                Value = Maximum
                RaiseEvent ValueChanged(Me, EventArgs.Empty)
                e.Handled = True
        End Select
        MyBase.OnKeyDown(e)
    End Sub

    Protected Overrides Sub SetBoundsCore(x As Integer, y As Integer, width As Integer, height As Integer, specified As BoundsSpecified)
        If width < 200 Then width = 200
        If height < 20 Then height = 20
        MyBase.SetBoundsCore(x, y, width, height, specified)
    End Sub

End Class
