Imports System.Data.sqlclient

Partial Class Login_NewPwd
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If CStr(Session("USER_ID")) = "" Then
            Response.Redirect("Login.aspx")
        End If

        If Not Page.IsPostBack Then
            txtPassword2.Attributes.Add("onkeyPress", "txtPassword_onkeydown();")
            If Hour(Now) < 18 Then
                lblSalut.Text = "Bonjour"
            Else
                lblSalut.Text = "Bonsoir"
            End If
            lblName.Text = Session("USER_FNAME") & " " & Session("USER_NAME")
        End If
        txtPassword1.Focus()
    End Sub

    Protected Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        If txtPassword1.Text <> txtPassword2.Text Then
            lblMessage.Text = "Les mots de passe<br>doivent êtres identiques"
        ElseIf txtPassword1.Text = Session("USER_PASSWORD") Then
            lblMessage.Text = "Le nouveau mot de passe doit <br>être différent mot de passe courant."
        Else
            SavePassword()
        End If
    End Sub

    Private Sub SavePassword()
        Dim cnx As New SqlConnection
        Dim sql As New SqlCommand
        cnx.ConnectionString = GetConnectionString()
        Try
            cnx.Open()
            sql.Connection = cnx
            sql.CommandText = "exec uUserPassword " & Session("USER_ID") & ", '" & txtPassword1.Text & "'"
            sql.ExecuteNonQuery()
            sql.Dispose()
        Finally
            cnx.Close()
            cnx.Dispose()
        End Try
        Response.Redirect("../Songs/Chants.aspx")
    End Sub

End Class
