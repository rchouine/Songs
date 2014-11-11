Imports AppUtils
Imports Songs.Model

Public Class SongController

    Function Save(aSong As Song) As Integer
        Using cnx As New ConnectionSql
            cnx.AddParameter("@song_id", SqlDbType.Int, aSong.Id, False)
            cnx.AddParameter("@song_code", SqlDbType.VarChar, aSong.Code, False)
            cnx.AddParameter("@song_title", SqlDbType.VarChar, aSong.Title, False)
            cnx.AddParameter("@song_author", SqlDbType.VarChar, aSong.Author, True)
            cnx.AddParameter("@song_trans", SqlDbType.VarChar, aSong.Translator, True)
            cnx.AddParameter("@song_lyrics", SqlDbType.Text, aSong.Lyrics, True)
            cnx.AddParameter("@song_chords", SqlDbType.VarChar, aSong.ChordPro, True)

            cnx.OpenReader("uSong")
            If cnx.Reader.Read Then
                Return CInt(cnx.Reader(0))
            Else
                Return aSong.Id
            End If
        End Using
    End Function

    Sub SaveUserSong(songId As Integer, userId As Integer, tone As String)
        Using cnx As New ConnectionSql
            cnx.AddParameter("@user_id", SqlDbType.VarChar, userId, False)
            cnx.AddParameter("@song_id", SqlDbType.Int, songId, False)
            cnx.AddParameter("@usersong_tone", SqlDbType.VarChar, tone, True)

            cnx.ExecuteSql("uUserSong")
        End Using
    End Sub

    Function GetTone(songId As Integer, userId As Integer) As String

        Using cnx As New ConnectionSql
            cnx.AddParameter("@user_id", SqlDbType.VarChar, userId, False)
            cnx.AddParameter("@song_id", SqlDbType.Int, songId, False)
            cnx.OpenReader("sUserSong")
            If cnx.Reader.Read Then
                Return cnx.Reader("USERSONG_TONE").ToString
            Else
                Return String.Empty
            End If
            cnx.Reader.Close()
        End Using
        Return Nothing
    End Function

    Function GetById(songId As Integer) As Song

        Using cnx As New ConnectionSql
            cnx.AddParameter("@song_id", SqlDbType.Int, songId, False)
            cnx.OpenReader("sSong")
            If cnx.Reader.Read Then
                Return FillSong(cnx.Reader)
            End If
            cnx.Reader.Close()
        End Using
        Return Nothing
    End Function

    Function GetList(userId As Integer, type As SearchType, strSearch As String, catId As Integer) As List(Of UserSong)
        Dim retour As New List(Of UserSong)
        Dim strType As String
        Select Case type
            Case SearchType.Code : strType = "SONG_CODE"
            Case SearchType.Lyrics : strType = "SONG_LYRICS"
            Case Else : strType = "SONG_TITLE"
        End Select

        Using cnx As New ConnectionSql
            cnx.AddParameter("@user_id", SqlDbType.Int, userId, False)
            cnx.AddParameter("@type", SqlDbType.VarChar, strType, False)
            cnx.AddParameter("@strSearch", SqlDbType.VarChar, strSearch, False)
            cnx.AddParameter("@cat_id", SqlDbType.Int, catId, False)
            cnx.OpenReader("sSongs")
            While cnx.Reader.Read
                retour.Add(FillUserSong(cnx.Reader))
            End While
            cnx.Reader.Close()
        End Using
        Return retour
    End Function

    Private Function FillSong(reader As IDataReader) As Song
        Dim retour As New Song
        retour.Author = reader("SONG_AUTHOR").ToString
        retour.ChordPro = reader("SONG_CHORDS").ToString
        retour.Code = reader("SONG_CODE").ToString
        retour.Id = CInt(reader("SONG_ID"))
        retour.Lyrics = reader("SONG_LYRICS").ToString
        retour.Title = reader("SONG_TITLE").ToString
        retour.Translator = reader("SONG_TRANS").ToString

        Return retour
    End Function

    Private Function FillUserSong(reader As IDataReader) As UserSong
        Dim retour As New UserSong
        retour.Code = reader("SONG_CODE").ToString
        retour.Id = CInt(reader("SONG_ID"))
        retour.Title = reader("SONG_TITLE").ToString
        retour.Tone = reader("USERSONG_TONE").ToString

        Return retour
    End Function
End Class
