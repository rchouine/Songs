Imports System.Diagnostics.CodeAnalysis
Imports System.Security.Principal
Imports System.Transactions
Imports System.Web.Routing
Imports DotNetOpenAuth.AspNet
Imports Microsoft.Web.WebPages.OAuth
Imports WebMatrix.WebData
Imports Songs.Controller
Imports Songs.Model

Public Class AccountController
    Inherits System.Web.Mvc.Controller

    Public Enum ManageMessageId
        ChangePasswordSuccess
        SetPasswordSuccess
        RemoveLoginSuccess
    End Enum

    <AllowAnonymous()> _
    Public Function Login(ByVal returnUrl As String) As ActionResult
        ViewData("ReturnUrl") = returnUrl
        Return View()
    End Function

    <HttpPost()> _
    <AllowAnonymous()> _
    <ValidateAntiForgeryToken()> _
    Public Function Login(ByVal model As LoginModel, ByVal returnUrl As String) As ActionResult
        If ModelState.IsValid Then 'AndAlso WebSecurity.Login(model.UserName, model.Password, persistCookie:=model.RememberMe) Then
            Dim userCtrl As New UserController
            Dim aUser = userCtrl.GetByCode(model.UserName)
            If aUser IsNot Nothing AndAlso aUser.Password = model.Password Then
                Session.Add("USER_PWD", aUser.Password)
                userCtrl.UpdateLoginStastitics(aUser.Id)
                If aUser.DatePassword.HasValue AndAlso aUser.DatePassword.Value < DateAdd(DateInterval.Day, -365, Now) Then
                    Dim newPwd As New LocalPasswordModel
                    newPwd.Id = aUser.Id
                    newPwd.OldPassword = aUser.Password
                    Return View("_SetPasswordPartial", newPwd)
                Else
                    SetSessionUser(aUser)
                End If

                Return RedirectToAction("Index", "Home")
            End If
        End If

        ' If we got this far, something failed, redisplay form
        ModelState.AddModelError("", "Le nom d'utilisateur ou mot de passe est incorrect.")
        Return View(model)
    End Function

    Private Sub SetSessionUser(aUser As User)
        Session.Add("USER_CODE", aUser.Code)
        Session.Add("USER_PWD", aUser.Password)
        Session.Add("USER_ID", aUser.Id)
        Session.Add("USER_LEVEL", aUser.Level)
        Session.Add("USER_NAME", aUser.Name)
        Session.Add("USER_FNAME", aUser.FirstName)
    End Sub

    <HttpPost()> _
    <AllowAnonymous()> _
    <ValidateAntiForgeryToken()> _
    Public Function LogOff() As ActionResult
        Session.Remove("USER_CODE")
        Session.Remove("USER_PWD")
        Session.Remove("USER_ID")
        Session.Remove("USER_LEVEL")
        Session.Remove("USER_NAME")
        Session.Remove("USER_FNAME")

        Return RedirectToAction("Index", "Home")
    End Function

    <AllowAnonymous()> _
    Public Function Register() As ActionResult
        Return View()
    End Function

    Public Function Manage(ByVal message As ManageMessageId?) As ActionResult
        ViewData("StatusMessage") =
            If(message = ManageMessageId.ChangePasswordSuccess, "Votre mot de passe a été modifié.", _
                If(message = ManageMessageId.SetPasswordSuccess, "Votre mot de passe a été enrégistré.", _
                    If(message = ManageMessageId.RemoveLoginSuccess, "La connexion a été retirée.", _
                        "")))

        Dim model As New LocalPasswordModel
        model.Id = Session("USER_ID")
        model.OldPassword = Session("USER_PWD")
        'ViewData("ReturnUrl") = Url.Action("Manage", )
        Return View("Manage", model)
    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Manage(ByVal model As LocalPasswordModel) As ActionResult
        ViewData("ReturnUrl") = Url.Action("Manage")
        If ModelState.IsValid Then

            ' ChangePassword will throw an exception rather than return false in certain failure scenarios.
            If model.OldPassword <> Session("USER_PWD").ToString Then
                ModelState.AddModelError("", "Votre ancien mot de passe est érroné.")
            Else
                Dim userCtrl As New UserController
                userCtrl.ResetPassword(model.Id, model.NewPassword, False)
                Session("USER_PWD") = model.NewPassword
                Return RedirectToAction("Index", "Home", New With {.Message = "Votre mot de pass a été modifié."})
            End If
        End If
        ' If we got this far, something failed, redisplay form 
        Return View(model)
    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function MettreJourMotPasse(ByVal model As LocalPasswordModel) As ActionResult
        ViewData("ReturnUrl") = Url.Action("Manage")
        If ModelState.IsValid Then

            ' ChangePassword will throw an exception rather than return false in certain failure scenarios.
            If model.OldPassword <> Session("USER_PWD").ToString Then
                ModelState.AddModelError("", "Votre ancien mot de passe est érroné.")
            Else
                Dim userCtrl As New UserController
                userCtrl.ResetPassword(model.Id, model.NewPassword, False)
                Dim currentUser = userCtrl.GetById(model.Id)
                SetSessionUser(currentUser)
                Return RedirectToAction("Index", "Home", New With {.Message = "Votre mot de pass a été modifié."})
            End If
        End If

        ' If we got this far, something failed, redisplay form 
        Return View("_SetPasswordPartial", model)
    End Function
End Class
