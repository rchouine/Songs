@ModelType Songs.Mvc.LocalPasswordModel

<p>
    Vous devez saisir un nouveau mot de passe.
</p>

@Using Html.BeginForm("MettreJourMotPasse", "Utilisateurs")
    @Html.AntiForgeryToken()
    @Html.ValidationSummary()

    @Html.HiddenFor(Function(m) m.Id)
    @Html.HiddenFor(Function(m) m.OldPassword)
    @<fieldset>
        <ol>
            <li>
                @Html.LabelFor(Function(m) m.NewPassword)
                @Html.PasswordFor(Function(m) m.NewPassword)
            </li>
            <li>
                @Html.LabelFor(Function(m) m.ConfirmPassword)
                @Html.PasswordFor(Function(m) m.ConfirmPassword)
            </li>
        </ol>
        <input type="submit" value="Enregistrer" />
    </fieldset>
End Using

