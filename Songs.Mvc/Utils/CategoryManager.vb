Imports Songs.Controller

Public Class CategoryManager

    Function GetList(songId As Integer) As List(Of SongCategotyViewModel)
        Dim retour As New List(Of SongCategotyViewModel)
        Dim catCtrl As New CategoryController
        Dim liste = catCtrl.GetList()
        Dim selection = catCtrl.GetListId(songId)

        For Each item In liste
            retour.Add(New SongCategotyViewModel With {.id = item.Id, .Name = item.Name, .Selected = selection.Contains(item.Id)})
        Next
        Return retour
    End Function
End Class
