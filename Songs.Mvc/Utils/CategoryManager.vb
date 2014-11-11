Imports Songs.Mvc.Models
Imports Songs.Controller

Namespace Utils

    Public Class CategoryManager

        Function GetList(songId As Integer) As IEnumerable(Of SongCategotyViewModel)
            Dim catCtrl As New CategoryController
            Dim liste = catCtrl.GetList()
            Dim selection = catCtrl.GetListId(songId)

            Return (From item In liste Select New SongCategotyViewModel With {.Id = item.Id, .Name = item.Name, .Selected = selection.Contains(item.Id)}).ToList()
        End Function
    End Class
End Namespace