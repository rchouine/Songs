Imports Songs.Modules
Imports Songs.Controller

Partial Class Songs_ChordPro
    Inherits Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            If Request("SONG_ID") <> "" Then
                Dim songCtrl As New SongController
                Dim aSong = songCtrl.GetById(CInt(Request.Params("SONG_ID")))

                'Load the chords
                If aSong IsNot Nothing Then
                    txtData.Text = aSong.ChordPro
                End If

                Dim cu As New ChordUtils
                cu.ShowChords(cu.Shift(txtData.Text, 0, rbSharp.Checked), divMain)
            End If
        End If

    End Sub

    Protected Sub btnShift_Click(sender As Object, e As EventArgs) Handles btnShift.Click
        Dim cu As New ChordUtils
        txtData.Text = cu.Shift(txtData.Text, 1, rbSharp.Checked)
        cu.ShowChords(txtData.Text, divMain)
    End Sub

    Protected Sub btnBemol_Click(sender As Object, e As EventArgs) Handles btnBemol.Click
        Dim cu As New ChordUtils
        txtData.Text = cu.Shift(txtData.Text, 12 - 1, rbSharp.Checked)
        cu.ShowChords(txtData.Text, divMain)
    End Sub

    Protected Sub rbBemol_CheckedChanged(sender As Object, e As EventArgs) Handles rbBemol.CheckedChanged
        Dim cu As New ChordUtils
        cu.ShowChords(cu.Shift(txtData.Text, 0, rbSharp.Checked), divMain)
    End Sub

    Protected Sub rbSharp_CheckedChanged(sender As Object, e As EventArgs) Handles rbSharp.CheckedChanged
        Dim cu As New ChordUtils
        cu.ShowChords(cu.Shift(txtData.Text, 0, rbSharp.Checked), divMain)
    End Sub

End Class
