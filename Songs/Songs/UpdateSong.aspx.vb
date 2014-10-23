Imports System.Data.SqlClient
Imports Songs.Model
Imports Songs.Controller

Partial Class Songs_UpdateSong
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If CStr(Session("USER_ID")) = "" Then
            Response.Redirect("../Login/Login.aspx")
        End If
        If Not Page.IsPostBack Then
            If Session("USER_LEVEL") > UserLevel.PowerUser Then
                lblSongTitle.Enabled = False
                lblCode.Enabled = False
                lblLyrics.Enabled = False
                lblAuthor.Enabled = False
                lblTrans.Enabled = False
                txtTitle.Enabled = False
                txtCode.Enabled = False
                txtLyrics.Enabled = False
                txtAuthor.Enabled = False
                txtTrans.Enabled = False
            End If

            If Request("SONG_ID") <> "0" Then
                Dim strSql As String
                Dim conn As SqlConnection
                Dim cmd As SqlCommand
                Dim dr As SqlDataReader
                Dim anItem As ListItem

                conn = New SqlConnection(GetConnectionString)
                strSql = "exec sSongCat " & Request("SONG_ID") & ", " & Session("USER_ID")
                cmd = New SqlCommand(strSql, conn)

                conn.Open()

                'Themes
                Dim catCtrl As New CategoryController
                Dim catListe = catCtrl.GetList
                chkCategoryList.DataSource = catListe
                chkCategoryList.DataBind()

                dr = cmd.ExecuteReader()
                'If song exists
                If dr.Read() Then
                    txtCode.Text = dr.GetValue(1).ToString
                    txtTitle.Text = dr.GetValue(2).ToString
                    txtAuthor.Text = dr.GetValue(3).ToString
                    txtTrans.Text = dr.GetValue(4).ToString
                    txtLyrics.Text = dr.GetValue(5).ToString
                    txtTone.Text = dr.Item("USERSONG_TONE").ToString
                    txtChords.Text = dr.Item("SONG_CHORDS").ToString

                    If Not dr.Item("CAT_ID") Is DBNull.Value Then
                        For Each anItem In chkCategoryList.Items
                            If anItem.Value = dr.Item("CAT_ID") Then
                                anItem.Selected = True
                            End If
                        Next
                    End If
                End If
                dr.Close()

                conn.Close()
                conn.Dispose()
            Else
                lblTitle.Text = "Ajout d'un chant"
                lblTone.Visible = False
                txtTone.Visible = False
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Dim conn As SqlConnection
        Dim cmd As SqlCommand

        conn = New SqlConnection(GetConnectionString)
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        conn.Open()

        'Save song
        If txtTitle.Enabled Then
            cmd.CommandText = "uSong"
            'Add the parameters
            If Request.Params("SONG_ID") <> "" And Request.Params("SONG_ID") <> "0" Then
                cmd.Parameters.Add(CreateParamInteger("@song_id", Request.Params("SONG_ID")))
            Else
                cmd.Parameters.Add(CreateParamInteger("@song_id", "0"))
            End If
            cmd.Parameters.Add(CreateParamVarchar("@song_code", txtCode.Text))
            cmd.Parameters.Add(CreateParamVarchar("@song_title", txtTitle.Text))
            cmd.Parameters.Add(CreateParamVarchar("@song_author", txtAuthor.Text))
            cmd.Parameters.Add(CreateParamVarchar("@song_trans", txtTrans.Text))
            cmd.Parameters.Add(CreateParamVarchar("@song_lyrics", txtLyrics.Text))
            cmd.Parameters.Add(CreateParamVarchar("@song_chords", txtChords.Text))
            cmd.ExecuteNonQuery()
        End If

        'Save the tone
        If Request.Params("SONG_ID") <> "0" Then
            cmd.CommandText = "uUserSong"
            cmd.Parameters.Clear()
            cmd.Parameters.Add(CreateParamInteger("@user_id", CStr(Session("USER_ID"))))
            cmd.Parameters.Add(CreateParamInteger("@song_id", Request.Params("SONG_ID")))
            cmd.Parameters.Add(CreateParamVarchar("@usersong_tone", txtTone.Text))
            cmd.ExecuteNonQuery()
        End If

        'Delete categories
        cmd.Parameters.Clear()
        cmd.CommandText = "dSongCat"
        cmd.Parameters.Add(CreateParamInteger("@song_id", Request.Params("SONG_ID")))
        cmd.ExecuteNonQuery()

        'Insert new categories
        Dim anItem As ListItem
        For Each anItem In chkCategoryList.Items
            If anItem.Selected Then
                cmd.CommandText = "iSongCat"
                cmd.Parameters.Clear()
                cmd.Parameters.Add(CreateParamInteger("@song_id", Request.Params("SONG_ID")))
                cmd.Parameters.Add(CreateParamInteger("@cat_id", anItem.Value))
                cmd.ExecuteNonQuery()
            End If
        Next

        cmd.Dispose()
        conn.Close()
        conn.Dispose()

        'Close this form
        Response.Write("<script>")
        Response.Write("window.top.returnValue = 1;")
        Response.Write("window.top.close();")
        Response.Write("</script>")


    End Sub

    Protected Sub btnScript_Click(sender As Object, e As EventArgs) Handles btnScript.Click
        Dim conn As New SqlConnection(GetConnectionString)
        Dim cmd As New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        conn.Open()

        'cmd.CommandText = "alter table songs add Accords varchar(max)"
        'cmd.CommandText = "EXEC sp_rename 'songs.[Accords]', 'song_chords', 'COLUMN'"
        'cmd.ExecuteNonQuery()

        cmd.Dispose()
        conn.Close()
        conn.Dispose()

    End Sub
End Class
