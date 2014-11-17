Imports Songs.Controller
Imports Songs.Model
Imports Songs.Mvc.Models

Namespace Controllers

    Public Class UtilisateursController
        Inherits Web.Mvc.Controller

        Function ConvertUserToModel(unUser As User) As UtilisateurModel
            Dim newUser As New UtilisateurModel
            newUser.Id = unUser.Id
            newUser.Code = unUser.Code
            newUser.Name = unUser.Name
            newUser.FirstName = unUser.FirstName
            newUser.Password = unUser.Password
            newUser.Level = unUser.Level
            newUser.DateCreate = unUser.DateCreate
            newUser.DatePasswordExpires = unUser.DatePassword
            newUser.DateLastAcces = unUser.DateLastAcces
            newUser.NbLogin = unUser.NbLogin
            Return newUser
        End Function
        Function ConvertModelToUser(unUser As UtilisateurModel) As User
            Dim newUser As New User
            newUser.Id = unUser.Id
            newUser.Code = unUser.Code
            newUser.Name = unUser.Name
            newUser.FirstName = unUser.FirstName
            newUser.Password = unUser.Password
            newUser.Level = unUser.Level
            newUser.DateCreate = unUser.DateCreate
            newUser.DatePassword = unUser.DatePasswordExpires
            newUser.DateLastAcces = unUser.DateLastAcces
            newUser.NbLogin = unUser.NbLogin
            Return newUser
        End Function

        Function Index(message As String) As ActionResult
            ViewData("StatusMessage") = message

            If Session("USER_LEVEL") IsNot Nothing AndAlso Session("USER_LEVEL") < UserLevel.PowerUser Then
                Return View("Index")
            Else
                Return RedirectToAction("Index", "Chants")
            End If
        End Function

        Function Liste() As JsonResult

            Dim userCtrl As New UserController
            Dim uliste = userCtrl.GetList
            If Session("USER_LEVEL") = UserLevel.MeMyself Then
                Return Json(uliste, JsonRequestBehavior.AllowGet)
            Else
                Dim listeFiltre = (From x In uliste Where x.Level <> UserLevel.MeMyself And x.Level <> UserLevel.Suppressed)
                Return Json(listeFiltre, JsonRequestBehavior.AllowGet)
            End If

        End Function

        Private Sub SetSessionUser(aUser As User)
            Session.Add("USER_CODE", aUser.Code)
            Session.Add("USER_PWD", aUser.Password)
            Session.Add("USER_ID", aUser.Id)
            Session.Add("USER_LEVEL", aUser.Level)
            Session.Add("USER_NAME", aUser.Name)
            Session.Add("USER_FNAME", aUser.FirstName)
        End Sub

        <AllowAnonymous()> _
        Public Function Enregistrement() As ActionResult
            Return View()
        End Function

        <HttpPost()> _
        <AllowAnonymous()> _
        <ValidateAntiForgeryToken()> _
        Public Function Enregistrement(ByVal model As LoginModel, ByVal returnUrl As String) As ActionResult
            If ModelState.IsValid Then
                Dim userCtrl As New UserController
                Dim aUser = userCtrl.GetByCode(model.UserName)
                If aUser IsNot Nothing AndAlso aUser.Password = model.Password Then
                    Session.Add("USER_PWD", aUser.Password)
                    userCtrl.UpdateLoginStastitics(aUser.Id)
                    If aUser.DatePassword.HasValue AndAlso aUser.DatePassword.Value < DateAdd(DateInterval.Day, -365, Now) Then
                        Dim newPwd As New LocalPasswordModel
                        newPwd.Id = aUser.Id
                        newPwd.OldPassword = aUser.Password
                        Return View("NouveauMotPasse", newPwd)
                    Else
                        SetSessionUser(aUser)
                    End If

                    Return RedirectToAction("Index", "Chants")
                Else
                    ModelState.AddModelError("", "Le nom d'utilisateur ou mot de passe est incorrect.")
                End If
            End If

            ' If we got this far, something failed, redisplay form
            Return View(model)
        End Function

        Public Function ChangerMotPasse() As ActionResult
            Dim model As New LocalPasswordModel
            model.Id = Session("USER_ID")
            model.OldPassword = Session("USER_PWD")
            Return View("ChangerMotPasse", model)
        End Function

        <HttpPost()> _
        <ValidateAntiForgeryToken()> _
        Public Function MettreJourMotPasse(ByVal model As LocalPasswordModel) As ActionResult
            If ModelState.IsValid Then

                ' ChangePassword will throw an exception rather than return false in certain failure scenarios.
                If model.OldPassword <> Session("USER_PWD").ToString Then
                    ModelState.AddModelError("", "Votre ancien mot de passe est érroné.")
                Else
                    Dim userCtrl As New UserController
                    userCtrl.ResetPassword(model.Id, model.NewPassword, False)
                    Dim currentUser = userCtrl.GetById(model.Id)
                    SetSessionUser(currentUser)
                    Return RedirectToAction("Index", "Chants", New With {.Message = "Votre mot de pass a été modifié."})
                End If
            End If

            ' If we got this far, something failed, redisplay form 
            Return View("ChangerMotPasse", model)
        End Function

        <HttpPost()> _
        <AllowAnonymous()> _
        <ValidateAntiForgeryToken()> _
        Public Function Deconnexion() As ActionResult
            Session.Remove("USER_CODE")
            Session.Remove("USER_PWD")
            Session.Remove("USER_ID")
            Session.Remove("USER_LEVEL")
            Session.Remove("USER_NAME")
            Session.Remove("USER_FNAME")

            Return RedirectToAction("Index", "Chants")
        End Function

        Function Utilisateur(id As Integer) As PartialViewResult
            Dim model As UtilisateurModel
            If id = 0 Then
                model = New UtilisateurModel
            Else
                Dim userCtrl As New UserController
                model = ConvertUserToModel(userCtrl.GetById(id))
            End If
            Return PartialView(model)
        End Function

        <HttpPost()> _
        <ValidateAntiForgeryToken()> _
        Public Function Enregistrer(ByVal model As UtilisateurModel) As ActionResult
            If ModelState.IsValid Then
                ' Attempt to register the user
                Try
                    Dim userCtrl As New UserController
                    If userCtrl.ValidateIfCodeExists(model.Id, model.Code) Then
                        ModelState.AddModelError("Code", "Erreur: Ce code utilisateur est déjà utilisé.")
                    Else
                        userCtrl.Save(ConvertModelToUser(model))
                        ViewData("StatusMessage") = "Enregistrement effectué."
                        'Mettre à jour le Id
                        If model.Id = 0 Then
                            Dim newUser = userCtrl.GetByCode(model.Code)
                            model.Id = newUser.Id
                        End If
                    End If
                    Return PartialView("Utilisateur", model)
                    'Return RedirectToAction("Index", "Utilisateurs")
                Catch e As Exception
                    ModelState.AddModelError("", e.Message)
                End Try
            End If

            ' If we got this far, something failed, redisplay form
            Return PartialView("Utilisateur", model)
        End Function

        <HttpPost()> _
        <ValidateAntiForgeryToken()> _
        Public Function ReinitialiserMotPasse(ByVal model As UtilisateurModel) As ActionResult
            ' Attempt to register the user
            Try
                Dim userCtrl As New UserController
                model.Password = ConfigurationManager.AppSettings.Item("DefaultPassword")
                userCtrl.ResetPassword(model.Id, model.Password, True)
                ViewData("StatusMessage") = "Le mot de pass a été changé pour " & model.Password
                Return PartialView("Utilisateur", model)
                'Return RedirectToAction("Index", "Utilisateurs")
            Catch e As Exception
                ModelState.AddModelError("", e.Message)
            End Try

            ' If we got this far, something failed, redisplay form
            Return PartialView("Utilisateur", model)
        End Function

        Public Function Supprimer(ByVal id As Integer) As ActionResult
            If id = Session("USER_ID") Then
                Return RedirectToAction("Index", "Utilisateurs", New With {.Message = "Vous ne pouvez vous supprimer vous-même."})
            Else
                Dim userCtrl As New UserController
                userCtrl.Delete(id)
                Return RedirectToAction("Index", "Utilisateurs", New With {.Message = "Utilisateur supprimé."})
            End If
        End Function

    End Class
End Namespace