Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Globalization
Imports System.Data.Entity

Public Class UsersContext
    Inherits DbContext

    Public Sub New()
        MyBase.New("DefaultConnection")
    End Sub

    Public Property UserProfiles As DbSet(Of UserProfile)
End Class

<Table("UserProfile")> _
Public Class UserProfile
    <Key()> _
    <DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)> _
    Public Property UserId As Integer

    Public Property UserName As String
End Class

Public Class RegisterExternalLoginModel
    <Required(ErrorMessage:="Le nom d'utilisateur est requis")> _
    <Display(Name:="Nom d'utilisateur")> _
    Public Property UserName As String

    Public Property ExternalLoginData As String
End Class

Public Class LocalPasswordModel
    <Required(ErrorMessage:="Erreur: L'identifiant de l'utilisateur est inconnu.")> _
    Public Property Id As Integer

    <Required(ErrorMessage:="L'ancien mot de passe est requis")> _
    <DataType(DataType.Password)> _
    <Display(Name:="Ancien mot de passe")> _
    Public Property OldPassword As String

    <Required(ErrorMessage:="Le nouveau mot de passe est requis")> _
    <StringLength(100, ErrorMessage:="Le mot de passe doit avoit au moins {2} caractères.", MinimumLength:=4)> _
    <DataType(DataType.Password)> _
    <Display(Name:="Nouveau mot de passe")> _
    Public Property NewPassword As String

    <DataType(DataType.Password)> _
    <Display(Name:="Confirmer nouveau mot de passe")> _
    <Compare("NewPassword", ErrorMessage:="Vos mots de passe ne correspondent pas.")> _
    Public Property ConfirmPassword As String
End Class

Public Class LoginModel
    <Required(ErrorMessage:="Le nom d'utilisateur est requis")> _
    <Display(Name:="Nom d'utilisateur")> _
    Public Property UserName As String

    <Required(ErrorMessage:="Le mot de passe est requis")> _
    <DataType(DataType.Password)> _
    <Display(Name:="Mot de passe")> _
    Public Property Password As String
End Class

Public Class RegisterModel
    <Required(ErrorMessage:="Le nom d'utilisateur est requis")> _
    <Display(Name:="Nom d'utilisateur")> _
    Public Property UserName As String

    <Required(ErrorMessage:="Le mot de passe est requis")> _
    <StringLength(100, ErrorMessage:="Le mot de passe doit avoit au moins {2} caractères.", MinimumLength:=4)> _
    <DataType(DataType.Password)> _
    <Display(Name:="Mot de passe")> _
    Public Property Password As String

    <DataType(DataType.Password)> _
    <Display(Name:="Confirmer nouveau mot de passe")> _
    <Compare("Password", ErrorMessage:="Vos mots de passe ne correspondent pas.")> _
    Public Property ConfirmPassword As String
End Class

Public Class ExternalLogin
    Public Property Provider As String
    Public Property ProviderDisplayName As String
    Public Property ProviderUserId As String
End Class
