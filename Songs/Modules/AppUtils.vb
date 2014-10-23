
Public Module AppUtils

    Public Function DefaultPassword() As String
        Return ConfigurationManager.AppSettings.Item("DefaultPassword")
    End Function

    Public Function GetConnectionString() As String
        Dim cs As ConnectionStringSettings
        cs = ConfigurationManager.ConnectionStrings("strcnx")
        Return cs.ConnectionString
    End Function

End Module
