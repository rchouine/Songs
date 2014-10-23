@If String.IsNullOrEmpty(Session("USER_ID")) Then
    @<ul>
        @*<li>@Html.ActionLink("Register", "Register", "Account", routeValues:=Nothing, htmlAttributes:=New With {.id = "registerLink"})</li>*@
        <li>@Html.ActionLink("Connexion", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {.id = "loginLink"})</li>
    </ul>
Else
@<text>
    Bonjour @Html.ActionLink(Session("USER_FNAME").ToString, "Manage", "Account", routeValues:=Nothing, htmlAttributes:=New With {.class = "username", .title = "Manage"})!
    @Using Html.BeginForm("LogOff", "Account", FormMethod.Post, New With {.id = "logoutForm"})
        @Html.AntiForgeryToken()
        @<a href="javascript:document.getElementById('logoutForm').submit()">Se déconnecter</a>
    End Using
</text>
End If
