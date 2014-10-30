@ModelType Songs.Mvc.LocalPasswordModel
@Code
    ViewData("Title") = "Changement de mot de passe"
End Code
<hgroup class="title">
    <h1>@ViewData("Title").</h1>
</hgroup>

<p class="message-success">@ViewData("StatusMessage")</p>

@Using Html.BeginForm("MettreJourMotPasse", "Utilisateurs")
    @Html.AntiForgeryToken()
    @Html.ValidationSummary()

    @<fieldset>
        @Html.HiddenFor(Function(m) m.Id)
        <ol>
            <li>
                @Html.LabelFor(Function(m) m.OldPassword)
                @Html.PasswordFor(Function(m) m.OldPassword)
            </li>
            <li>
                @Html.LabelFor(Function(m) m.NewPassword)
                @Html.PasswordFor(Function(m) m.NewPassword)
            </li>
            <li>
                @Html.LabelFor(Function(m) m.ConfirmPassword)
                @Html.PasswordFor(Function(m) m.ConfirmPassword)
            </li>
        </ol>
         <input type="submit" value="Changer mot de passe" style="margin-left: 40px;" />
    </fieldset>
End Using

