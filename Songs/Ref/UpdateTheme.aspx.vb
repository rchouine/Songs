Imports Songs.Controller
Imports Songs.Model

Partial Class Ref_UpdateTheme
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If CStr(Session("USER_ID")) = "" Then
                Response.Redirect("../Login/Login.aspx")
            End If

            If Request("CAT_ID") <> "0" Then

                Dim catCtrl As New CategoryController
                Dim newCat = catCtrl.GetById(CInt(Request("CAT_ID")))
                If newCat IsNot Nothing Then
                    txtCatName.Text = newCat.Name
                End If
            Else
                lblTitle.Text = "Ajout d'un thème"
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim aCategory As New Category
        If Request.Params("CAT_ID") <> "" Then
            aCategory.Id = CInt(Request.Params("CAT_ID"))
        Else
            aCategory.Id = 0
        End If
        aCategory.Name = txtCatName.Text

        Dim catCtrl As New CategoryController
        catCtrl.Save(aCategory)

        'Close this form
        Response.Write("<script>")
        Response.Write("window.top.close();")
        Response.Write("</script>")
    End Sub

End Class
