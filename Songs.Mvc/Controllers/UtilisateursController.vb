Imports Songs.Controller
Imports Songs.Model

Public Class UtilisateursController
    Inherits System.Web.Mvc.Controller

    '
    ' GET: /Utilisateurs

    Function ConvertUserToUtilisateur(unUser As User) As UtilisateurModel
        Dim newUser As New UtilisateurModel
        newUser.Id = unUser.Id
        newUser.Code = unUser.Code
        newUser.Name = unUser.Name
        newUser.FirstName = unUser.FirstName
        newUser.Password = unUser.Password
        newUser.Level = unUser.Level
        newUser.DateCreate = unUser.DateCreate
        newUser.DatePasswordExpires = unUser.DatePasswordExpires
        newUser.DateLastAcces = unUser.DateLastAcces
        newUser.NbLogin = unUser.NbLogin
        Return newUser
    End Function

    Function Index() As ActionResult
        Dim userCtrl As New UserController
        Dim liste = userCtrl.GetList

        Return View(liste)
    End Function

    Function Utilisateur(id As Integer) As PartialViewResult
        Dim userCtrl As New UserController
        Dim unUser = userCtrl.GetById(id)
        Dim unUserModel = ConvertUserToUtilisateur(userCtrl.GetById(id))
        Return PartialView(unUserModel)
    End Function
End Class