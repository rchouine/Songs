Imports Songs.Model
Imports System.ComponentModel.DataAnnotations

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
    Property Password As String

    <Display(Name:="Niveau")> _
    Property Level As UserLevel

    Property DateCreate As DateTime?
    Property DatePasswordExpires As DateTime?
    Property DateLastAcces As DateTime?
    Property NbLogin As Integer

    Private _LevelList As SelectList
    Property LevelList As SelectList
        Get
            If _LevelList Is Nothing Then

                Dim newList As New List(Of SelectListItem)
                newList.Add(New SelectListItem With {.Text = "Concepteur", .Value = UserLevel.MeMyself})
                newList.Add(New SelectListItem With {.Text = "Administrateur", .Value = UserLevel.Admin})
                newList.Add(New SelectListItem With {.Text = "Gestionnaire", .Value = UserLevel.PowerUser})
                newList.Add(New SelectListItem With {.Text = "Utilisateur", .Value = UserLevel.User})
                newList.Add(New SelectListItem With {.Text = "Désactivé", .Value = UserLevel.Deactivate})
                newList.Add(New SelectListItem With {.Text = "Supprimé", .Value = UserLevel.Suppressed})
                _LevelList = New SelectList(newList, "Value", "Text")
            End If

            Return _LevelList
        End Get
        Set(value As SelectList)
            _LevelList = value
        End Set
    End Property
End Class
