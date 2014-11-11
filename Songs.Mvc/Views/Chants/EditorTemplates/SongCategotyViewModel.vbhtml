@Imports Songs.Mvc.Models
@ModelType SongCategotyViewModel

<div style="width: 200px">
    @Html.HiddenFor(Function(x) x.id)
    @Html.HiddenFor(Function(x) x.Name)
    @If Session("USER_LEVEL") < Songs.Model.UserLevel.User Then
        @Html.EditorFor(Function(x) x.Selected)
        @Html.LabelFor(Function(x) x.Selected, Model.Name)
    Else
        @Html.EditorFor(Function(x) x.Selected, New With {.htmlAttributes = New With {.disabled = "disabled"}})
        @Html.LabelFor(Function(x) x.Selected, Model.Name, New With {.style = "color: grey;"})
    End If
</div>
