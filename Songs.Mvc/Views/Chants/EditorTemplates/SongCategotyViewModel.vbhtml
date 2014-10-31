﻿@ModelType Songs.Mvc.SongCategotyViewModel

<style>
    label {
        font-size: inherit;
    }

</style>

<div style="width: 200px">
    @Html.HiddenFor(Function(x) x.id)
    @Html.HiddenFor(Function(x) x.Name)
    @Html.EditorFor(Function(x) x.Selected)
    @Html.LabelFor(Function(x) x.Selected, Model.Name)
</div>
