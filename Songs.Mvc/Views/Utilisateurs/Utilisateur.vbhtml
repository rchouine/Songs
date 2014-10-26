@ModelType Songs.Mvc.UtilisateurModel

@Using Html.BeginForm()

    @Html.ValidationSummary()

    @<fieldset>
        <legend>Données de l'utilisateur</legend>
        <ol>
            <li>
                @Html.LabelFor(Function(m) m.Code)
                @Html.EditorFor(Function(m) m.Code)
            </li>
            <li>
                @Html.LabelFor(Function(m) m.FirstName)
                @Html.EditorFor(Function(m) m.FirstName)
            </li>
            <li>
                @Html.LabelFor(Function(m) m.Name)
                @Html.EditorFor(Function(m) m.Name)
            </li>
            <li>
                @Html.LabelFor(Function(m) m.Level)
                @Html.DropDownListFor(Function(m) m.Level, Model.LevelList)
            </li>
       </ol>
        <input type="submit" value="Enregistrer" />
    </fieldset>
End Using

<script type="text/javascript">

    $(document).ready(function () {
        $("#Level").val(@CInt(Model.Level));
    });
</script>

