Imports System.Data.SqlClient

Partial Class Songs_Top10
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Request("USER_ID") = "" Then
            lblTitle.Text = "Top 10"
        Else
            lblTitle.Text = "Mon top 10"
        End If
        GetSelection()
    End Sub

    Private Function GetTblSelectionHeader() As String
        Dim strTbl As String = ""

        strTbl += "<table id=""tblSelection"" width=""100%"">"
        strTbl += "   <tr class=""Entete"">"
        strTbl += "      <td style=""display:none""></td>"
        strTbl += "      <td style=""width: 15%"">Code</td>"
        strTbl += "      <td style=""width: 70%"">Titre</td>"
        strTbl += "      <td style=""width: 15%"" align='center'>nb fois.</td>"
        strTbl += "   </tr>"

        Return strTbl
    End Function

    Private Sub GetSelection()
        Dim strSql As String
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        Dim dr As SqlDataReader
        Dim strTblSel As String = ""

        conn = New SqlConnection(GetConnectionString)
        strSql = "execute sTop10 " & Request("USER_ID")
        cmd = New SqlCommand(strSql, conn)
        conn.Open()
        dr = cmd.ExecuteReader()

        strTblSel += GetTblSelectionHeader()
        While dr.Read()
            strTblSel += "   <tr style=""cursor: hand; color:navy; background-color:White"" onclick=""OnSelectTR(this)"">"
            strTblSel += "      <td style='display: none'>" & dr.GetValue(0).ToString & "</td>"
            strTblSel += "      <td>" & dr.GetValue(1).ToString & "</td>"
            strTblSel += "      <td>" & dr.GetValue(2).ToString & "</td>"
            strTblSel += "      <td align='center'>" & dr.GetValue(3).ToString & "</td>"
            strTblSel += "   </tr>"
        End While
        strTblSel += "</table>"

        dr.Close()
        conn.Close()
        conn.Dispose()

        divSelection.InnerHtml = strTblSel

    End Sub
End Class

