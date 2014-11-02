Imports AppUtils
Imports Songs.Model

Public Class CategoryController

    Sub Save(aCat As Category)
        Using cnx As New ConnectionSql
            cnx.AddParameter("@cat_id", SqlDbType.Int, aCat.Id, False)
            cnx.AddParameter("@cat_name", SqlDbType.VarChar, aCat.Name, False)

            cnx.ExecuteSql("uCategory")
        End Using
    End Sub

    Function GetById(catId As Integer) As Category
        Using cnx As New ConnectionSql
            cnx.AddParameter("@cat_id", SqlDbType.Int, catId, False)
            cnx.OpenReader("sCategory")
            If cnx.Reader.Read Then
                Return New Category With {.Id = CInt(cnx.Reader("CAT_ID")), .Name = cnx.Reader("CAT_NAME").ToString}
            End If
            cnx.Reader.Close()
        End Using
        Return Nothing
    End Function

    Function GetList() As List(Of Category)
        Dim retour As New List(Of Category)

        Using cnx As New ConnectionSql
            cnx.OpenReader("sCategories")
            While cnx.Reader.Read
                retour.Add(New Category With {.Id = CInt(cnx.Reader("CAT_ID")), .Name = cnx.Reader("CAT_NAME").ToString})
            End While
            cnx.Reader.Close()
        End Using
        Return retour
    End Function

    Function GetListId(songId As Integer) As List(Of Integer)
        Dim retour As New List(Of Integer)
        Using cnx As New ConnectionSql
            cnx.AddParameter("@song_id", SqlDbType.Int, songId, False)
            cnx.OpenReader("sSongCategory")
            While cnx.Reader.Read
                retour.Add(CInt(cnx.Reader("CAT_ID")))
            End While
            cnx.Reader.Close()
        End Using
        Return retour
    End Function

    Sub SaveSongCategories(songId As Integer, catIds As IEnumerable(Of Integer))
        Using cnx As New ConnectionSql
            cnx.AddParameter("@song_id", SqlDbType.Int, songId, False)
            cnx.ExecuteSql("dSongCat")
            For Each catId In catIds
                cnx.ClearParameters()
                cnx.AddParameter("@song_id", SqlDbType.Int, songId, False)
                cnx.AddParameter("@cat_id", SqlDbType.Int, catId, False)
                cnx.ExecuteSql("iSongCat")
            Next
        End Using
    End Sub

End Class
