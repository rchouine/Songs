
Public Enum SearchType
    Code
    Title
    Lyrics
End Enum

Public Interface ISong
    Property Id As Integer
    Property Code As String
    Property Title As String
End Interface

Public Class Song
    Implements iSong

    Property Id As Integer Implements iSong.Id
    Property Code As String Implements iSong.Code
    Property Title As String Implements iSong.Title
    Property Author As String
    Property Translator As String
    Property Lyrics As String
    Property ChordPro As String
End Class

Public Class UserSong
    Implements iSong

    Property Id As Integer Implements iSong.Id
    Property Code As String Implements iSong.Code
    Property Title As String Implements iSong.Title
    Property Tone As String
End Class

Public Class SelectedSong
    Inherits UserSong

    Public Property SelectedDate As Date
    Public Property Section As Integer
    Public Property Index As Integer
End Class