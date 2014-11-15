Imports System.Web.Helpers
Imports Songs.Controller
Imports Songs.Model

Public Class DocumentWord
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Dim selectedDate = Json.Decode(Of DateTime)(Request.QueryString.Item("selDate"))

        Dim selCtrl As New SelectionController
        Dim listeChants = selCtrl.GetList(CInt(Session("USER_ID")), selectedDate)

        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.ContentType = "application/msword"
        HttpContext.Current.Response.ContentEncoding = UnicodeEncoding.UTF8
        HttpContext.Current.Response.Charset = "UTF-8"

        Dim strDoc As New System.Text.StringBuilder("")
        strDoc.Append("<html " & _
                "xmlns:o='urn:schemas-microsoft-com:office:office' " & _
                "xmlns:w='urn:schemas-microsoft-com:office:word'" & _
                "xmlns='http://www.w3.org/TR/REC-html40'>" & _
                "<head><title>Time</title>")

        'The setting specifies document's view after it is downloaded as Print instead of the default Web Layout
        strDoc.Append("<!--[if gte mso 9]><xml><w:WordDocument><w:View>Print</w:View><w:Zoom>90</w:Zoom><w:DoNotOptimizeForBrowser/></w:WordDocument></xml><![endif]-->")
        strDoc.Append("<META HTTP-EQUIV=Content-Type CONTENT=text/html; charset=UTF-8>")

        strDoc.Append("<style>")
        strDoc.Append("@page Section1 {size:841.7pt 595.45pt;mso-page-orientation:landscape;margin:1.0in 1.0in 1.0in 1.0in;mso-header-margin:.5in;mso-footer-margin:.5in;mso-paper-source:0;}")
        strDoc.Append("div.Section1 {page:Section1;}")
        strDoc.Append(".section, .section tr td { border-spacing: 0; border-collapse: collapse;border: 1px solid gray; text-align: center; }")
        strDoc.Append("</style></head>")

        strDoc.Append("<body><div class=""Section1"">")
        strDoc.Append("<h1 style=""text-align: center"">Liste de chants</h1>")
        strDoc.Append("<table style=""width: 100%;""><tr>")
        strDoc.Append(String.Format("<td><h2>Animateur: {0} {1}</h2></td>", Session("USER_FNAME"), Session("USER_NAME")))
        strDoc.Append(String.Format("<td style=""text-align: right""><h2>Date: {0}</h2></td>", Format(selectedDate, "yyyy-MM-dd")))
        strDoc.Append("</tr></table>")

        strDoc.Append("<br /><b>Chants avant la réunion</b>")
        strDoc.Append(AddSection(listeChants, 1))

        strDoc.Append("<br /><b>Chants pendant la réunion</b>")
        strDoc.Append(AddSection(listeChants, 2))

        strDoc.Append("<br /><b>Offrande - Communion - Autre</b>")
        strDoc.Append(AddSection(listeChants, 3))

        strDoc.Append("</div></body>")
        strDoc.Append("</html>")

        'Force this content to be downloaded as a Word document with the name of your choice
        Response.AppendHeader("Content-Type", "application/msword")
        Response.AppendHeader("Content-disposition", "attachment; filename=ListeChants.doc")
        Response.Write(strDoc)
        Response.Flush()
    End Sub

    Private Function AddSection(liste As IEnumerable(Of SelectedSong), sectionId As Integer) As String
        Dim retour As String
        retour = "<table width=""100%"" class=""section"">"
        For Each chant In (From x In liste Where x.Section = sectionId)
            retour &= AddLine(chant.Code, chant.Title, chant.Tone)
        Next
        retour &= AddLine("&nbsp;", "", "")
        retour &= "</table>"
        Return retour
    End Function

    Private Function AddLine(code As String, title As String, tone As String) As String
        Dim retour As String
        retour = "<tr>"
        retour &= String.Format("<td style=""width: 150px"">{0}</td>", code)
        retour &= String.Format("<td style=""text-align: left;"">{0}</td>", title)
        retour &= String.Format("<td style=""width: 150px"">{0}</td>", tone)
        retour &= "</tr>"
        Return retour
    End Function
End Class