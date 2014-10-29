Imports Songs.Controller
Imports Songs.Model

Public Class UtilisateursController
    Inherits System.Web.Mvc.Controller

    '
    ' GET: /Utilisateurs

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

    Function Index(Message As String) As ActionResult
        ViewData("StatusMessage") = Message

        Dim userCtrl As New UserController
        Dim liste = userCtrl.GetList
        If Session("USER_LEVEL") IsNot Nothing AndAlso Session("USER_LEVEL") = UserLevel.MeMyself Then
            Return View(liste)
        Else
            Dim listeFiltre = (From x In liste Where x.Level <> UserLevel.MeMyself And x.Level <> UserLevel.Suppressed)
            Return View(listeFiltre)
        End If
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