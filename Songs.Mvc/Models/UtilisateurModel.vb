Imports System.ComponentModel.DataAnnotations
Imports Songs.Model

Namespace Models

    Public Class UtilisateurModel
        Property Id As Integer

        <Required(ErrorMessage:="Le code est requis")> _
        <Display(Name:="Code")> _
        Property Code As String

        <Required(ErrorMessage:="Le nom est requis")> _
        <Display(Name:="Nom")> _
        Property Name As String

        <Required(ErrorMessage:="Le prénom est requis")> _
        <Display(Name:="Prénom")> _
        Property FirstName As String

        <Display(Name:="Mot de passe")> _
        Property Password As String = ConfigurationManager.AppSettings.Item("DefaultPassword")

        <Display(Name:="Niveau")> _
        Property Level As UserLevel

        Property DateCreate As DateTime?
        Property DatePasswordExpires As DateTime?
        Property DateLastAcces As DateTime?
        Property NbLogin As Integer

        Private _levelList As SelectList
        Property LevelList As SelectList
            Get
                If _levelList Is Nothing Then
                    Dim newList As New List(Of SelectListItem)
                    If HttpContext.Current.Session("USER_LEVEL") = UserLevel.MeMyself Then
                        newList.Add(New SelectListItem With {.Text = "Concepteur", .Value = UserLevel.MeMyself})
                    End If
                    newList.Add(New SelectListItem With {.Text = "Administrateur", .Value = UserLevel.Admin})
                    newList.Add(New SelectListItem With {.Text = "Gestionnaire", .Value = UserLevel.PowerUser})
                    newList.Add(New SelectListItem With {.Text = "Utilisateur", .Value = UserLevel.User})
                    newList.Add(New SelectListItem With {.Text = "Désactivé", .Value = UserLevel.Deactivate})
                    If HttpContext.Current.Session("USER_LEVEL") = UserLevel.MeMyself Then
                        newList.Add(New SelectListItem With {.Text = "Supprimé", .Value = UserLevel.Suppressed})
                    End If
                    _levelList = New SelectList(newList, "Value", "Text")
                End If

                Return _levelList
            End Get
            Set(value As SelectList)
                _levelList = value
            End Set
        End Property
    End Class
End Namespace