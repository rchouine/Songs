
Public Enum UserLevel

    MeMyself = 0
    Admin = 10
    PowerUser = 20
    User = 30
    Deactivate = 100
    Suppressed = 1000
End Enum

Public Class User
    Property Id As Integer
    Property Code As String
    Property Name As String
    Property FirstName As String
    Property Password As String
    Property Level As UserLevel
    Property DateCreate As DateTime?
    Property DatePassword As DateTime?
    Property DateLastAcces As DateTime?
    Property NbLogin As Integer
End Class
