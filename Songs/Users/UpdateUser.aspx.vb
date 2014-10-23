Imports Songs.Model
Imports Songs.Controller

Partial Class Users_UpdateUser
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If CStr(Session("USER_ID")) = "" Then
                Response.Redirect("../Login/Login.aspx")
            End If

            If Session("USER_LEVEL") > UserLevel.MeMyself Then
                'Remove Gestionnaire
                cboLevel.Items.Remove(cboLevel.Items.Item(0))
            End If
            If Session("USER_LEVEL") > UserLevel.Admin Then
                'Remove Admin
                cboLevel.Items.Remove(cboLevel.Items.Item(0))
            End If
            If Session("USER_LEVEL") > UserLevel.PowerUser Then
                'Remove Ajout/modif
                cboLevel.Items.Remove(cboLevel.Items.Item(0))
            End If

            If Request("USER_ID") <> "0" Then
                Dim userCtrl As New UserController
                Dim aUser = userCtrl.GetById(CInt(Request("USER_ID")))

                If aUser IsNot Nothing Then
                    txtCode.Text = aUser.Code
                    txtName.Text = aUser.Name
                    txtFName.Text = aUser.FirstName
                    cboLevel.SelectedValue = CInt(aUser.Level)
                End If
            Else
                lblTitle.Text = "Ajout d'un utilisateur"
                'Set level "Utilisateur de base"
                cboLevel.SelectedValue = UserLevel.User
                btnResetPassword.Visible = False
            End If
        End If
    End Sub

    Protected Sub btnResetPassword_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnResetPassword.Click
        Dim userCtrl As New UserController
        userCtrl.ResetPassword(CInt(Request("USER_ID")), DefaultPassword, True)
        lblMessage.Text = "<script>alert('Le mot de passe a été réinitialisé à \'\'" + DefaultPassword() + "\'\'');</script>"
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Dim userCtrl As New UserController

        'If user exists
        If userCtrl.ValidateIfCodeExists(CInt(Request("USER_ID")), Trim(txtCode.Text)) Then
            Response.Write("<script>")
            Response.Write("alert('Sauvegarde impossible.\nCe code est utilisé par un autre utilisateur.')")
            Response.Write("</script>")
        Else
            'Enregistrer
            Dim aUser As New User

            'Add the parameters
            If Request.Params("USER_ID") <> "" And Request.Params("USER_ID") <> "0" Then
                aUser.Id = CInt(Request.Params("USER_ID"))
            Else
                aUser.Id = 0
            End If
            aUser.Code = txtCode.Text
            aUser.Name = txtName.Text
            aUser.FirstName = txtFName.Text
            aUser.Password = DefaultPassword()
            aUser.Level = cboLevel.SelectedValue

            userCtrl.Save(aUser)

            'Close this form
            Response.Write("<script>")
            Response.Write("window.top.opener.location = window.top.opener.location;")
            Response.Write("window.top.close();")
            Response.Write("</script>")
        End If

    End Sub
End Class
