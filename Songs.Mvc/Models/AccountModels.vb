Imports System.ComponentModel.DataAnnotations

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



