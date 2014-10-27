@ModelType Songs.Mvc.LoginModel

@Code
    ViewData("Title") = "Enregistrement"
End Code

<hgroup class="title">
    <h1>@ViewData("Title").</h1>
</hgroup>

<section id="loginForm">
@Using Html.BeginForm(New With { .ReturnUrl = ViewData("ReturnUrl") })
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(true)

    @<fieldset>
        <legend></legend>
        <ol>
            <li>
                @Html.LabelFor(Function(m) m.UserName)
                @Html.TextBoxFor(Function(m) m.UserName)
                @Html.ValidationMessageFor(Function(m) m.UserName)
            </li>
            <li>
                @Html.LabelFor(Function(m) m.Password)
                @Html.PasswordFor(Function(m) m.Password)
                @Html.ValidationMessageFor(Function(m) m.Password)
            </li>
        </ol>
        <input type="submit" value="Se connecter" />
    </fieldset>
End Using
</section>

