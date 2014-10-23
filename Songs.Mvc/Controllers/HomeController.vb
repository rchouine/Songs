Imports Songs.Controller

Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult
        If String.IsNullOrEmpty(Session("USER_ID")) Then
            ViewData("Message") = "T'es pas logué"
            Return RedirectToAction("Login", "Account")
        Else
            ViewData("Message") = "T'es logué"
            'ViewData("Message") = "Modify this template to jump-start your ASP.NET MVC application."
        End If

        Dim userCtrl As New UserController
        Dim liste = userCtrl.GetList

        Return View(liste)
    End Function

    Function About() As ActionResult
        ViewData("Message") = "Chants intégrés du Carrefour Chrétien de la Capitale"

        Return View()
    End Function

End Class
