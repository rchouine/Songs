@Imports Songs.Model

<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8" />
        <title>Chants intégrés du Carrefour Chrétien de la Capitale</title>

        <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
        <meta name="viewport" content="width=device-width" />

        @Styles.Render("~/Content/Site.css")
        @Styles.Render("~/Scripts/jquery-ui-1.11.2/themes/cupertino/jquery-ui.min.css")

        @Scripts.Render("~/Scripts/jquery-2.1.1.min.js")
        @Scripts.Render("~/Scripts/jquery-ui-1.11.2/jquery-ui.min.js")
        @Scripts.Render("~/Scripts/jquery.validate.min.js")
        @Scripts.Render("~/Scripts/jquery.validate.unobtrusive.min.js")
        @Scripts.Render("~/Scripts/modernizr-2.8.3.js")

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
                            @If Session("USER_ID") IsNot Nothing Then
                                @<li>@Html.ActionLink("Chants", "Index", "Chants")</li>
                            End If
                            @If Session("USER_LEVEL") IsNot Nothing AndAlso Session("USER_LEVEL") < UserLevel.PowerUser Then
                                @<li>@Html.ActionLink("Utilisateurs", "Index", "Utilisateurs")</li>
                            End If
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
    </body>
</html>


<script type="text/javascript">
    $(document).ready(function () {
        $("button, input:submit, input:button").button();
    });


</script>