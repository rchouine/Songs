Imports Songs.Model
Imports AppUtils

Public Class UserController

    Sub Desactivate(userId As Integer)
        Using cnx As New ConnectionSql
            cnx.AddParameter("@user_id", SqlDbType.Int, userId, False)
            cnx.ExecuteSql("uUserDeactivate")
        End Using
    End Sub

    Sub Delete(userId As Integer)
        Using cnx As New ConnectionSql
            cnx.AddParameter("@user_id", SqlDbType.Int, userId, False)
            cnx.ExecuteSql("dUser")
        End Using
    End Sub

    Sub ResetPassword(userId As Integer, password As String, mustChange As Boolean)
        Using cnx As New ConnectionSql
            cnx.AddParameter("@user_id", SqlDbType.Int, userId, False)
            cnx.AddParameter("@password", SqlDbType.VarChar, password, False)
            cnx.AddParameter("@create_old", SqlDbType.Bit, mustChange, False)

            cnx.ExecuteSql("uUserPassword")
        End Using
    End Sub

    Function ValidateIfCodeExists(userId As Integer, code As String) As Boolean
        Using cnx As New ConnectionSql
            cnx.AddParameter("@user_id", SqlDbType.Int, userId, False)
            cnx.AddParameter("@user_code", SqlDbType.VarChar, code, False)

            cnx.OpenReader("sUserValidateCode")
            If cnx.Reader.Read Then
                Return cnx.Reader.GetInt32(0) = 1
            End If
            cnx.Reader.Close()
        End Using
        Return False
    End Function

    Sub Save(aUser As User)
        Using cnx As New ConnectionSql
            cnx.AddParameter("@user_id", SqlDbType.Int, aUser.Id, False)
            cnx.AddParameter("@user_code", SqlDbType.VarChar, aUser.Code, False)
            cnx.AddParameter("@user_name", SqlDbType.VarChar, aUser.Name, False)
            cnx.AddParameter("@user_fname", SqlDbType.VarChar, aUser.FirstName, True)
            cnx.AddParameter("@user_password", SqlDbType.VarChar, aUser.Password, True)
            cnx.AddParameter("@user_level", SqlDbType.Int, aUser.Level, True)

            cnx.ExecuteSql("uUser")
        End Using
    End Sub

    Sub UpdateLoginStastitics(userId As Integer)
        Using cnx As New ConnectionSql
            cnx.AddParameter("@user_id", SqlDbType.Int, userId, False)
            cnx.ExecuteSql("uUserLogin")
        End Using
    End Sub

    Function GetById(userId As Integer) As User
        Using cnx As New ConnectionSql
            cnx.AddParameter("@user_id", SqlDbType.Int, userId, False)
            cnx.OpenReader("sUser")
            If cnx.Reader.Read Then
                Return FillUser(cnx.Reader)
            End If
            cnx.Reader.Close()
        End Using
        Return Nothing
    End Function

    Function GetByCode(code As String) As User
        Using cnx As New ConnectionSql
            cnx.AddParameter("@user_code", SqlDbType.VarChar, code, False)
            cnx.OpenReader("sUserByCode")
            If cnx.Reader.Read Then
                Return FillUser(cnx.Reader)
            End If
            cnx.Reader.Close()
        End Using
        Return Nothing
    End Function

    Function GetList() As List(Of User)
        Dim retour As New List(Of User)

        Using cnx As New ConnectionSql
            cnx.OpenReader("sUsers")
            While cnx.Reader.Read
                retour.Add(FillUser(cnx.Reader))
            End While
            cnx.Reader.Close()
        End Using
        Return retour
    End Function

    Private Function FillUser(reader As IDataReader) As User
        Dim retour As New User
        retour.Code = reader("USER_CODE").ToString

        If IsDBNull(reader("USER_DATE_CRE")) Then
            retour.DateCreate = Nothing
        Else
            retour.DateCreate = CDate(reader("USER_DATE_CRE"))
        End If

        If IsDBNull(reader("USER_DATE_LAST_ACCESS")) Then
            retour.DateLastAcces = Nothing
        Else
            retour.DateLastAcces = CDate(reader("USER_DATE_LAST_ACCESS"))
        End If

        If IsDBNull(reader("USER_DATE_PASS")) Then
            retour.DatePasswordExpires = Nothing
        Else
            retour.DatePasswordExpires = CDate(reader("USER_DATE_PASS"))
        End If

        retour.FirstName = reader("USER_FNAME").ToString
        retour.Id = CInt(reader("USER_ID"))
        retour.Level = CType(reader("USER_LEVEL"), UserLevel)
        retour.Name = reader("USER_NAME").ToString

        If IsDBNull(reader("USER_NB_LOGIN")) Then
            retour.NbLogin = 0
        Else
            retour.NbLogin = CInt(reader("USER_NB_LOGIN"))
        End If

        retour.Password = reader("USER_PASSWORD").ToString

        Return retour
    End Function
End Class
