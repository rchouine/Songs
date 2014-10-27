@ModelType Songs.Mvc.LocalPasswordModel
@Code
    ViewData("Title") = "Changement de mot de passe"
End Code

<hgroup class="title">
    <h1>@ViewData("Title").</h1>
</hgroup>

<p class="message-success">@ViewData("StatusMessage")</p>

@Html.Partial("_ChangePasswordPartial")

