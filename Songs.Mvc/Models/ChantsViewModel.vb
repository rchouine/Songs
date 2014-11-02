Imports Songs.Model

Public Class CriteresRecherche
    Property Type As String
    Property Text As String
    Property CatId As Integer
    Property TabIndex As Integer
End Class

Public Class TypeRechercheViewModel
    Property Name As String
    Property Selected As Boolean
End Class

Public Class ChantsViewModel
    Private _typesRecherche As List(Of TypeRechercheViewModel)

    Property TabIndex As Integer
    Property Chants As List(Of UserSong)
    Property Categories As List(Of Category)
    Property CriteresRecherche As New CriteresRecherche

    Property TexteRecherche As String
        Get
            Return CriteresRecherche.Text
        End Get
        Set(value As String)
            CriteresRecherche.Text = value
        End Set
    End Property
    ReadOnly Property TypesRecherche As List(Of TypeRechercheViewModel)
        Get
            If _typesRecherche Is Nothing Then
                _typesRecherche = New List(Of TypeRechercheViewModel)
                _typesRecherche.Add(New TypeRechercheViewModel With {.Name = "Code", .Selected = False})
                _typesRecherche.Add(New TypeRechercheViewModel With {.Name = "Titre", .Selected = True})
                _typesRecherche.Add(New TypeRechercheViewModel With {.Name = "Paroles", .Selected = False})
            End If
            Return _typesRecherche
        End Get
    End Property

    Sub UpdateTypeRecherche(type As String)
        CriteresRecherche.Type = type
        For Each item In TypesRecherche
            item.Selected = CBool(item.Name = type)
        Next
    End Sub
End Class
