Imports System.IO

''' <summary>
'''     プレイリストの1項目を表すデータクラス
''' </summary>
Public Class PlaylistItem

    ''' <summary>メディアファイルのフルパス</summary>
    Public Property FilePath As String

    ''' <summary>メディアファイルのファイル名（FilePath から自動取得）</summary>
    Public ReadOnly Property FileName As String
        Get
            If String.IsNullOrEmpty(FilePath) Then Return ""
            Return IO.Path.GetFileName(FilePath)
        End Get
    End Property

    ''' <summary>メディアの長さ（秒）</summary>
    Public Property Duration As Double

    ''' <summary>メモ</summary>
    Public Property Memo As String

    ''' <summary>再生位置（秒）</summary>
    Public Property Position As Double

    Public Sub New(path As String)
        Me.FilePath = path
    End Sub

    Public Sub New()
    End Sub

End Class
