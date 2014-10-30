@If String.IsNullOrEmpty(Session("USER_ID")) Then
    @<ul>
        <li>@Html.ActionLink("Connexion", "Enregistrement", "Utilisateurs", routeValues:=Nothing, htmlAttributes:=New With {.id = "loginLink"})</li>
    </ul>
Else
@<text>
    Bonjour @Html.ActionLink(Session("USER_FNAME").ToString, "ChangerMotPasse", "Utilisateurs", Nothing, New With {.class = "username", .title = "Manage"})
    @Using Html.BeginForm("Deconnexion", "Utilisateurs", FormMethod.Post, New With {.id = "logoutForm"})
        @Html.AntiForgeryToken()
        @<a href="javascript:document.getElementById('logoutForm').submit()">Se déconnecter</a>
    End Using
</text>
End If


