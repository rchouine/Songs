@Imports Songs.Mvc.Models
@ModelType LocalPasswordModel

<p>
    Vous devez saisir un nouveau mot de passe.
</p>

@Using Html.BeginForm("MettreJourMotPasse", "Utilisateurs")
    @Html.AntiForgeryToken()
    @Html.ValidationSummary()

    @Html.HiddenFor(Function(m) m.Id)
    @Html.HiddenFor(Function(m) m.OldPassword)
    @<fieldset class="ui-widget ui-widget-content" style="width: 480px;">
    <table>
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
            <td><input type="submit" value="Enregistrer" /></td>
        </tr>
    </table>
</fieldset>
End Using

