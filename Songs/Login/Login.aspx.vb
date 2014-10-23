Imports Songs.Controller

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtPassword.Attributes.Add("onkeyPress", "txtPassword_onkeydown();")
            txtName.Focus()
        End If
    End Sub

    Protected Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click

        Dim userCtrl As New UserController
        Dim user = userCtrl.GetByCode(txtName.Text)

        If user IsNot Nothing Then
            'Validate desactivated
            If user.Level = Model.UserLevel.Deactivate Then
                lblMessage.Text = "Ce compte a été désactivé.<br>Veuillez contacter le responsable du système."
            Else
                'If password match
                If user.Password = txtPassword.Text Then
                    Session("USER_ID") = user.Id
                    Session("USER_NAME") = user.Name
                    Session("USER_FNAME") = user.FirstName
                    Session("USER_LEVEL") = user.Level

                    'Update log
                    userCtrl.UpdateLoginStastitics(user.Id)

                    'Validate password expires
                    If user.DatePasswordExpires < DateAdd(DateInterval.Day, -365, Now) Then
                        Response.Redirect("NewPwd.aspx")
                    Else
                        Response.Redirect("../Songs/Chants.aspx")
                    End If
                Else
                    lblMessage.Text = "Mot de passe incorrect.<br>Attention aux majuscules."
                    nbTry.Text = CStr(CInt(nbTry.Text) + 1)
                    If CInt(nbTry.Text) < 5 Then
                        lblMessage.Text = "Mot de passe incorrect.<br>Attention aux majuscules."
                    Else
                        'userCtrl.Desactivate(user.Id)
                        lblMessage.Text = "Veuillez contacter le responsable du système<br />pour réinitialiser votre mot de passe."
                    End If
                    txtPassword.Focus()
                End If
            End If
        Else
            lblMessage.Text = "Nom d'utilisateur incorrect."
            txtName.Focus()
        End If
    End Sub

End Class
