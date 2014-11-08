Imports System.ComponentModel.DataAnnotations
Imports Songs.Model

Public Class SongCategotyViewModel
    Property id As Integer
    Property Name As String
    Property Selected As Boolean
End Class

Public Class ChantViewModel
    Private _categories As List(Of SongCategotyViewModel)
    Property Id As Integer

    <Required(ErrorMessage:="* Champ requis")> _
    <Display(Name:="Code")> _
    Property Code As String

    <Required(ErrorMessage:="* Champ requis")> _
    <Display(Name:="Titre")> _
    Property Title As String

    <Display(Name:="Auteur")> _
    Property Author As String

    <Display(Name:="Traducteur")> _
    Property Translator As String

    <Display(Name:="Paroles")> _
    Property Lyrics As String

    <Display(Name:="Accords Chordpro")> _
    Property ChordPro As String

    Property Categories As List(Of SongCategotyViewModel)
        Get
            If _categories Is Nothing Then
                Dim catManager As New CategoryManager
                _categories = catManager.GetList(Id)
            End If
            Return _categories
        End Get
        Set(value As List(Of SongCategotyViewModel))
            _categories = value
        End Set
    End Property
End Class
