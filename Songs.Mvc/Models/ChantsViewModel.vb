Imports Songs.Model

Namespace Models

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
        Property Categories As List(Of Category)
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
    End Class
End Namespace