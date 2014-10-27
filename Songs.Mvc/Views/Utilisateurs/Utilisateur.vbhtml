@ModelType Songs.Mvc.UtilisateurModel

@Scripts.Render("~/Scripts/jquery-ui-1.11.2/external/jquery/jquery.js")
@Scripts.Render("~/Scripts/jquery-ui-1.11.2/jquery-ui.js")

<div id="container">

    @Using Html.BeginForm("SaveUtilisateur", "Utilisateurs", FormMethod.Post, New With {.id = "formulaire"})

        @Html.AntiForgeryToken()
        @Html.ValidationSummary()

        @<fieldset>
            <legend style="font-weight: bold;">Données de l'utilisateur</legend>
             @Html.HiddenFor(Function(m) m.Id)
             @Html.HiddenFor(Function(m) m.DateCreate)
             @Html.HiddenFor(Function(m) m.DateLastAcces)
             @Html.HiddenFor(Function(m) m.DatePasswordExpires)
             @Html.HiddenFor(Function(m) m.NbLogin)
             @Html.HiddenFor(Function(m) m.Password)

            <table>
                <tr>
                    <td>@Html.LabelFor(Function(m) m.Code)</td>
                    <td>@Html.EditorFor(Function(m) m.Code)</td>
                </tr>
                <tr>
                    <td>@Html.LabelFor(Function(m) m.FirstName)</td>
                    <td>@Html.EditorFor(Function(m) m.FirstName)</td>
                </tr>
                <tr>
                    <td>@Html.LabelFor(Function(m) m.Name)</td>
                    <td>@Html.EditorFor(Function(m) m.Name)</td>
                </tr>
                <tr>
                    <td>@Html.LabelFor(Function(m) m.Level)</td>
                    <td>@Html.DropDownListFor(Function(m) m.Level, Model.LevelList)</td>
                </tr>
                <tr>
                    <td></td>
                    <td><input id="btnSubmitFormulaire" type="submit" value="Enregistrer" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td><input id="btnResetPassword" type="button" value="Réinitialiser le mot de passe" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td><input id="btnRefresh" type="button" value="Actualiser la grille" /></td>
                </tr>
            </table>
        </fieldset>
    End Using
</div>

    <script type="text/javascript">

        $(document).ready(function () {
            $("#Level").val(@CInt(Model.Level));

            //Styliser les boutons
            $("#btnSubmitFormulaire").button();
            $("#btnResetPassword").button();
            $("#btnRefresh").button();

        });

        //Prendre controle du formulaire
        $('#formulaire').submit(function (e) {
            e.preventDefault();
            $.post($(this).attr("action"), $(this).serialize(), function (r, s) {
                $("#container").html(r);
            });
        });

        $("#btnResetPassword").click(function () {
            $.post("Utilisateurs/ResetPassword", $('#formulaire').serialize(), function (r, s) {
                $("#container").html(r);
            });
        });

        $("#btnRefresh").click(function () {
            window.location.href = "/Utilisateurs";
        });
    </script>

