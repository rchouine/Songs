@Imports Songs.Mvc.Models
@ModelType TypeRechercheViewModel

<div style="width: 80px">
    @Html.HiddenFor(Function(x) x.Name)
    @Html.RadioButtonFor(Function(x) x.Selected, Model.Name, If(Model.Selected, New With {.Name = "typeRecherche", .checked = "checked"}, New With {.Name = "typeRecherche"}))
    @Html.LabelFor(Function(x) x.Selected, Model.Name)
</div>