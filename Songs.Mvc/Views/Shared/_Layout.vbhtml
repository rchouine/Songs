@Imports Songs.Model

<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8" />
        <title>@ViewData("Title") - Chants intégrés du Carrefour Chrétien de la Capitale</title>

        <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
        <meta name="viewport" content="width=device-width" />

        @Scripts.Render("~/bundles/modernizr")
        @Styles.Render("~/Content/css")
        @Styles.Render("~/Scripts/jquery-ui-1.11.2/themes/smoothness/jquery-ui.css")
        @Styles.Render("~/Content/Gridmvc.css")

    </head>
    <body>
        <header>
            <div class="content-wrapper">
                <div class="float-left"><img src="~/Images/logo_ccc_100.png" />
                    <p class="site-title">Chants intégrés</p>
                </div>
                <div class="float-right">
                    <section id="login">
                        @Html.Partial("_LoginPartial")
                    </section>
                    <nav>
                        <ul id="menu">
                            <li>@Html.ActionLink("Chants", "Index", "Chants")</li>
                            @If Session("USER_LEVEL") IsNot Nothing AndAlso Session("USER_LEVEL") < UserLevel.PowerUser Then
                                @<li>@Html.ActionLink("Utilisateurs", "Index", "Utilisateurs")</li>
                            End If
                            <li>@Html.ActionLink("Home", "Index", "Home")</li>
                            <li>@Html.ActionLink("About", "About", "Home")</li>
                        </ul>
                    </nav>
                </div>
            </div>
        </header>
        <div id="body">
            @RenderSection("featured", required:=false)
            <section class="content-wrapper main-content clear-fix">
                @RenderBody()
            </section>
        </div>
        <footer>
            <div class="content-wrapper">
                <div class="float-left">
                    <p>&copy; @DateTime.Now.Year - Tous droits réservés</p>
                </div>
            </div>
        </footer>

        @Scripts.Render("~/Scripts/jquery-ui-1.11.2/external/jquery/jquery.js")
        @Scripts.Render("~/Scripts/jquery-ui-1.11.2/jquery-ui.js")
        @RenderSection("scripts", required:=False)
    </body>
</html>
