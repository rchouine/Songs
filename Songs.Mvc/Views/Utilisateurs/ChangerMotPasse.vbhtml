@ModelType Songs.Mvc.LocalPasswordModel
@Code
    ViewData("Title") = "Changement de mot de passe"
End Code
<hgroup class="title">
    <h1>@ViewData("Title").</h1>
</hgroup>

@Scripts.Render("~/bundles/jquery")
@Scripts.Render("~/bundles/jqueryval")

<p class="message-success">@ViewData("StatusMessage")</p>

@Using Html.BeginForm("MettreJourMotPasse", "Utilisateurs")
    @Html.AntiForgeryToken()
    @Html.ValidationSummary()

    @Html.HiddenFor(Function(m) m.Id)
    
    @<fieldset class="ui-widget ui-widget-content" style="width: 480px;">
    <table>
        <tr>
            <td>@Html.LabelFor(Function(m) m.OldPassword)</td>
            <td>@Html.PasswordFor(Function(m) m.OldPassword)</td>
        </tr>
        <tr>
            <td>@Html.LabelFor(Function(m) m.NewPassword)</td>
            <td>@Html.PasswordFor(Function(m) m.NewPassword)</td>
        </tr>
        <tr>
            <td>@Html.LabelFor(Function(m) m.ConfirmPassword)</td>
            <td>@Html.PasswordFor(Function(m) m.ConfirmPassword)</td>
        </tr>
        <tr>
            <td></td>
            <td><input type="submit" value="Changer mot de passe" /></td>
        </tr>
    </table>

</fieldset>
End Using

