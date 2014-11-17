Imports Songs.Model
Imports Songs.Controller

Namespace Controllers
    Public Class SelectionsController
        Inherits Web.Mvc.Controller

        Function GetSelections(selDate As String) As JsonResult
            Dim newDate = Helpers.Json.Decode(Of DateTime)(selDate)
            Dim selCtrl As New SelectionController
            Dim liste = selCtrl.GetList(CInt(Session("USER_ID")), newDate)
            Return Json(liste, JsonRequestBehavior.AllowGet)
        End Function

        Function SaveSelection(selDate As String, section1SongId As String, section2SongId As String, section3SongId As String) As JsonResult
            Dim newDate = Helpers.Json.Decode(Of DateTime)(selDate)
            Dim sng1 = Helpers.Json.Decode(Of List(Of Integer))(section1SongId)
            Dim sng2 = Helpers.Json.Decode(Of List(Of Integer))(section2SongId)
            Dim sng3 = Helpers.Json.Decode(Of List(Of Integer))(section3SongId)

            Dim liste As New List(Of SelectedSong)
            For i = 0 To sng1.Count - 1
                liste.Add(New SelectedSong With {.Id = sng1.Item(i), .Section = 1, .Index = i})
            Next
            For i = 0 To sng2.Count - 1
                liste.Add(New SelectedSong With {.Id = sng2.Item(i), .Section = 2, .Index = i})
            Next
            For i = 0 To sng3.Count - 1
                liste.Add(New SelectedSong With {.Id = sng3.Item(i), .Section = 3, .Index = i})
            Next

            Dim selCtrl As New SelectionController
            Using scope As New Transactions.CommittableTransaction
                selCtrl.Delete(CInt(Session("USER_ID")), newDate)
                selCtrl.Save(CInt(Session("USER_ID")), newDate, liste)
            End Using
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace