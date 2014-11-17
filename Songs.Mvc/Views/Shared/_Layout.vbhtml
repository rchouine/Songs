@Imports Songs.Model

<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8" />
        <title>Chants intégrés du Carrefour Chrétien de la Capitale</title>

        <link rel="Shortcut Icon" href="~/favicon.ico" type="image/x-icon" />
        <meta name="viewport" content="width=device-width" />

        @Styles.Render("~/Content/css")
        @Styles.Render("~/Content/themes/cupertino/jquery-ui.min.css")

        @Scripts.Render("~/bundles/jquery")
        @Scripts.Render("~/bundles/jqueryui")
        @Scripts.Render("~/bundles/jqueryval")
        @Scripts.Render("~/bundles/modernizr")

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