Imports Songs.Controller

Partial Class Ref_GestionThemes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If CStr(Session("USER_ID")) = "" Then
            Response.Redirect("../Login/Login.aspx")
        End If

        Dim catCtrl As New CategoryController
        Dim catList = catCtrl.GetList

        repCat.DataSource = catList
        repCat.DataBind()
    End Sub
End Class
