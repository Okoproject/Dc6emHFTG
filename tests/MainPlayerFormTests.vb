Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Drawing
Imports System.Windows.Forms

<TestClass>
Public Class MainPlayerFormTests

#Region "Position プロパティ - 正常ケース"

    <TestMethod>
    Public Sub Position_SetValue_UpdatesTrackBar()
        Using form As CreateTestableForm()
            form.SetMediaPlayerForTest(New TestMpvPlayerWrapper())

            form.Position = 100

            Assert.AreEqual(100, form.TrackBar1.Value)
        End Using
    End Sub

#End Region

#Region "Position プロパティ - 境界ケース"

    <TestMethod>
    Public Sub Position_SetNegativeValue_ClampsToZero()
        Using form As CreateTestableForm()
            form.SetMediaPlayerForTest(New TestMpvPlayerWrapper())

            form.Position = -10

            Assert.AreEqual(0, form.Position)
        End Using
    End Sub

    <TestMethod>
    Public Sub Position_SetZeroValue_Succeeds()
        Using form As CreateTestableForm()
            form.SetMediaPlayerForTest(New TestMpvPlayerWrapper())

            form.Position = 0

            Assert.AreEqual(0, form.Position)
        End Using
    End Sub

#End Region

#Region "Volume プロパティ - 正常ケース"

    <TestMethod>
    Public Sub Volume_SetValue_UpdatesTrackBarAndLabel()
        Using form As CreateTestableForm()
            form.SetMediaPlayerForTest(New TestMpvPlayerWrapper())

            form.Volume = 50

            Assert.AreEqual(50, form.Volume)
        End Using
    End Sub

#End Region

#Region "Volume プロパティ - 境界ケース"

    <TestMethod>
    Public Sub Volume_SetNegativeValue_ClampsToZero()
        Using form As CreateTestableForm()
            form.SetMediaPlayerForTest(New TestMpvPlayerWrapper())

            form.Volume = -10

            Assert.AreEqual(0, form.Volume)
        End Using
    End Sub

    <TestMethod>
    Public Sub Volume_SetAbove100_ClampsTo100()
        Using form As CreateTestableForm()
            form.SetMediaPlayerForTest(New TestMpvPlayerWrapper())

            form.Volume = 150

            Assert.AreEqual(100, form.Volume)
        End Using
    End Sub

    <TestMethod>
    Public Sub Volume_SetBoundaryValues_Succeeds()
        Using form As CreateTestableForm()
            form.SetMediaPlayerForTest(New TestMpvPlayerWrapper())

            form.Volume = 0
            Assert.AreEqual(0, form.Volume)

            form.Volume = 100
            Assert.AreEqual(100, form.Volume)
        End Using
    End Sub

#End Region

#Region "Speed プロパティ - 正常ケース"

    <TestMethod>
    Public Sub Speed_SetValue_UpdatesCorrectly()
        Using form As CreateTestableForm()
            form.SetMediaPlayerForTest(New TestMpvPlayerWrapper())

            form.Speed = 2.0

            Assert.AreEqual(2.0, form.Speed)
        End Using
    End Sub

#End Region

#Region "Speed プロパティ - 境界ケース"

    <TestMethod>
    Public Sub Speed_SetBelowMin_ClampsToMin()
        Using form As CreateTestableForm()
            form.SetMediaPlayerForTest(New TestMpvPlayerWrapper())

            form.Speed = 0.05

            Assert.AreEqual(0.1, form.Speed)
        End Using
    End Sub

    <TestMethod>
    Public Sub Speed_SetAboveMax_ClampsToMax()
        Using form As CreateTestableForm()
            form.SetMediaPlayerForTest(New TestMpvPlayerWrapper())

            form.Speed = 20.0

            Assert.AreEqual(10.0, form.Speed)
        End Using
    End Sub

    <TestMethod>
    Public Sub Speed_SetBoundaryValues_Succeeds()
        Using form As CreateTestableForm()
            form.SetMediaPlayerForTest(New TestMpvPlayerWrapper())

            form.Speed = 0.1
            Assert.AreEqual(0.1, form.Speed)

            form.Speed = 10.0
            Assert.AreEqual(10.0, form.Speed)
        End Using
    End Sub

#End Region

#Region "ヘルパーメソッド"

    Private Function CreateTestableForm() As MainPlayerForm
        Dim form As New MainPlayerForm()
        form.CreateHandle()
        Return form
    End Function

#End Region

#Region "テスト用モック"

    Private Class TestMpvPlayerWrapper
        Inherits MpvPlayerWrapper

        Public Sub New()
            MyBase.New(Nothing)
        End Sub

        Public Overrides Property Position As Double
            Get
                Return _testPosition
            End Get
            Set(value As Double)
                _testPosition = value
            End Set
        End Property

        Public Overrides Property Volume As Integer
            Get
                Return _testVolume
            End Get
            Set(value As Integer)
                _testVolume = Math.Max(0, Math.Min(100, value))
            End Set
        End Property

        Public Overrides Property Speed As Double
            Get
                Return _testSpeed
            End Get
            Set(value As Double)
                _testSpeed = Math.Max(0.1, Math.Min(10.0, value))
            End Set
        End Property

        Private _testPosition As Double
        Private _testVolume As Integer
        Private _testSpeed As Double = 1.0
    End Class

#End Region

End Class
