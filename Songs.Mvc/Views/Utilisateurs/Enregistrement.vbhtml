@Imports Songs.Mvc.Models
@ModelType LoginModel

@Code
    ViewData("Title") = "Enregistrement"
End Code
<hgroup class="title">
    <h1>@ViewData("Title").</h1>
</hgroup>

@Using Html.BeginForm(New With { .ReturnUrl = ViewData("ReturnUrl") })
    @Html.AntiForgeryToken()
    @Html.ValidationSummary()

    @<fieldset class="ui-widget ui-widget-content" style="width: 400px;">
        <table>
            <tr>
                <td>@Html.LabelFor(Function(m) m.UserName)</td>
                <td>@Html.TextBoxFor(Function(m) m.UserName)</td>
            </tr>
            <tr>
                <td>@Html.LabelFor(Function(m) m.Password)</td>
                <td>@Html.PasswordFor(Function(m) m.Password)</td>
            </tr>
            <tr>
                <td></td>
                <td><input type="submit" value="Se connecter" /></td>
            </tr>
        </table>
    </fieldset>
End Using

