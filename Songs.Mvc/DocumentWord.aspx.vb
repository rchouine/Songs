Public Class DocumentWord
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim selectedDate = System.Web.Helpers.Json.Decode(Of DateTime)(Request.QueryString.Item("selDate"))

        Dim selCtrl As New Songs.Controller.SelectionController
        Dim listeChants = selCtrl.GetList(CInt(Session("USER_ID")), selectedDate)

        HttpContext.Current.Response.ContentType = "application/msword"
        HttpContext.Current.Response.ContentEncoding = System.Text.UnicodeEncoding.UTF8
        HttpContext.Current.Response.Charset = "UTF-8"

        Response.Write("<html>")
        Response.Write("<head>")
        Response.Write("<META HTTP-EQUIV=Content-Type CONTENT=text/html; charset=UTF-8>")
        Response.Write("<meta name=ProgId content=Word.Document>")
        Response.Write("<meta name=Generator content=Microsoft Word 9>")
        Response.Write("<meta name=Originator content=Microsoft Word 9>")
        Response.Write("<style>")
        Response.Write("@page Section1 {size:841.7pt 595.45pt;mso-page-orientation:landscape;margin:1.0in 1.0in 1.0in 1.0in;mso-header-margin:.5in;mso-footer-margin:.5in;mso-paper-source:0;}")
        Response.Write("div.Section1 {page:Section1;}")
        Response.Write(".section, .section tr td { border-spacing: 0; border-collapse: collapse;border: 1px solid gray; text-align: center; }")
        Response.Write("</style>")
        Response.Write("</head>")
        Response.Write("<body><div class=""Section1"">")

        Response.Write("<h1 style=""text-align: center"">Liste de chants</h1>")
        Response.Write("<table style=""width: 100%;""><tr><td><h3>Animateur: " & Session("USER_FNAME") & " " & Session("USER_NAME") & "</h3></td>" & _
                          "<td style=""text-align: right""><h3>Date: " & Format(selectedDate, "yyyy-MM-dd") & "</h3></td></tr></table>")

        Response.Write("<br /><b>Chants avant la réunion</b>")
        Response.Write("<table width=""100%"" class=""section"">")
        For Each chant In (From x In listeChants Where x.Section = 1)
            Response.Write("<tr>")
            Response.Write("<td style=""width: 150px"">" & chant.Code & "</td>")
            Response.Write("<td style=""text-align: left;"">" & chant.Title & "</td>")
            Response.Write("<td style=""width: 150px"">" & chant.Tone & "</td>")
            Response.Write("</tr>")
        Next
        Response.Write("</table>")

        Response.Write("<br /><b>Chants pendant la réunion</b>")
        Response.Write("<table width=""100%"" class=""section"">")
        For Each chant In (From x In listeChants Where x.Section = 2)
            Response.Write("<tr>")
            Response.Write("<td style=""width: 150px"">" & chant.Code & "</td>")
            Response.Write("<td style=""text-align: left;"">" & chant.Title & "</td>")
            Response.Write("<td style=""width: 150px"">" & chant.Tone & "</td>")
            Response.Write("</tr>")
        Next
        Response.Write("</table>")

        Response.Write("<br /><b>Chants après la réunion</b>")
        Response.Write("<table width=""100%"" class=""section"">")
        For Each chant In (From x In listeChants Where x.Section = 3)
            Response.Write("<tr>")
            Response.Write("<td style=""width: 150px"">" & chant.Code & "</td>")
            Response.Write("<td style=""text-align: left;"">" & chant.Title & "</td>")
            Response.Write("<td style=""width: 150px"">" & chant.Tone & "</td>")
            Response.Write("</tr>")
        Next
        Response.Write("</table>")

        Response.Write("</div></body>")
        Response.Write("</html>")
        HttpContext.Current.Response.Flush()
    End Sub

End Class