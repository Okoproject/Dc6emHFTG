''' <summary>
'''     しおりの1項目を表すデータクラス
''' </summary>
Friend NotInheritable Class BookmarkEntry

    ''' <summary>表示時刻</summary>
    Public Property TimeDisplay As String

    ''' <summary>メモ</summary>
    Public Property Memo As String

    ''' <summary>再生位置（秒）</summary>
    Public Property PositionSeconds As Integer

    ''' <summary>
    '''     しおりの1項目を初期化する
    ''' </summary>
    ''' <param name="timeDisplay">表示時刻</param>
    ''' <param name="memo">メモ</param>
    ''' <param name="positionSeconds">再生位置（秒）</param>
    Public Sub New(timeDisplay As String, memo As String, positionSeconds As Integer)
        Me.TimeDisplay = timeDisplay
        Me.Memo = memo
        Me.PositionSeconds = positionSeconds
    End Sub

End Class
