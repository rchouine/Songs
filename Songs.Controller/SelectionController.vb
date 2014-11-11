Imports AppUtils
Imports Songs.Model

Public Class SelectionController

    Function GetDates(userId As Integer) As List(Of Date)
        Dim retour As New List(Of Date)

        Using cnx As New ConnectionSql
            cnx.AddParameter("@user_id", SqlDbType.Int, userId, False)
            cnx.OpenReader("sSelectionDates")
            While cnx.Reader.Read
                retour.Add(CDate(cnx.Reader("SEL_DATE")))
            End While
            cnx.Reader.Close()
        End Using
        Return retour
    End Function

    Function GetList(userId As Integer, selDate As Date) As List(Of SelectedSong)
        Dim retour As New List(Of SelectedSong)

        Using cnx As New ConnectionSql
            cnx.AddParameter("@user_id", SqlDbType.Int, userId, False)
            cnx.AddParameter("@sel_date", SqlDbType.DateTime, selDate, False)
            cnx.OpenReader("sSelection")
            While cnx.Reader.Read
                retour.Add(FillSelectedSong(cnx.Reader))
            End While
            cnx.Reader.Close()
        End Using
        Return retour
    End Function

    Private Function FillSelectedSong(reader As IDataReader) As SelectedSong
        Dim retour As New SelectedSong
        retour.SelectedDate = CDate(reader("SEL_DATE"))
        retour.Section = CInt(reader("SEL_SECTION"))
        retour.Index = CInt(reader("SEL_INDEX"))

        retour.Id = CInt(reader("SONG_ID"))
        retour.Code = reader("SONG_CODE").ToString
        retour.Title = reader("SONG_TITLE").ToString
        retour.Tone = reader("USERSONG_TONE").ToString

        Return retour
    End Function

    Public Sub Delete(userId As Integer, selDate As Date)
        Using cnx As New ConnectionSql
            cnx.AddParameter("@user_id", SqlDbType.Int, userId, False)
            cnx.AddParameter("@sel_date", SqlDbType.DateTime, selDate, False)
            cnx.ExecuteSql("dSelection")
        End Using
    End Sub

    Private Sub Save(userId As Integer, selDate As Date, liste As List(Of SelectedSong))

        Using cnx As New ConnectionSql
            For Each chant In liste
                cnx.AddParameter("@user_id", SqlDbType.Int, userId, False)
                cnx.AddParameter("@sel_date", SqlDbType.DateTime, selDate, False)
                cnx.AddParameter("@sel_section", SqlDbType.Int, chant.Section, False)
                cnx.AddParameter("@sel_index", SqlDbType.Int, chant.Index, False)
                cnx.AddParameter("@song_id", SqlDbType.Int, chant.Id, False)

                cnx.ExecuteSql("iSelection")
            Next
        End Using
    End Sub
End Class
