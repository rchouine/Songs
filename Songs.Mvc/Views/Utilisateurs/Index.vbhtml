@ModelType IEnumerable(Of Songs.Model.User)

@imports GridMvc.Html

@Styles.Render("~/Content/Gridmvc.css")
@Scripts.Render("~/Scripts/gridmvc.min.js")

<style type="text/css">
    .btnEdit {
        padding: 0px 8px;
        cursor: pointer;
    }

    a.btnEdit:hover {
        background-color: #c7d1d6;
    }

    .grid-wrap {
        height: 300px;
        width: 380px;
        overflow-x: hidden;
        overflow-y: auto;
    }

    label {
        font-size: inherit;
    }
</style>

<h2>Gestion des utilisateurs</h2>

<div id="divGrille" class="childTab" style="float: left; margin-right: 20px;">
    @Html.Grid(Model).Named("UserGrid").Columns(Sub(col)
                                                    col.Add(Function(o) o.Id, True).Titled("Id")
                                                    col.Add(Function(o) o.Code).SetWidth(150).Titled("Code")
                                                    col.Add(Function(o) o.FirstName).SetWidth(150).Titled("Prénom")
                                                    col.Add(Function(o) o.Name).SetWidth(150).Titled("Nom")
                                                    col.Add().SetWidth(20).Titled("").RenderValueAs(Function(o) Html.Raw("<img id='" & o.Id & "' class='btnEdit' src='../../Images/pictosBoutons/supprimer.gif' title='Supprimer'/>")).Encoded(False).Sanitized(False)
                                                End Sub).Selectable(True).Sortable()
</div>
<div>
    <button id="btnAddUser">Ajouter un utilisateur</button>
</div>
<div id="divDetail" class="childTab" style="float: left;"></div>
<div id="dlgConfirm" title="Confirmation" style="display: none;">
    <div id="dlgContent"></div>
</div>


<script type="text/javascript">
    function ResizeGrid() {
        var h = $(window).height();
        var w = $(window).width();
        if (w > 1024) w = 1024;

        $(".grid-wrap").height(h - 260);
    }

    $(document).ready(function () {

        ResizeGrid();
        $(window).resize(function () {
            ResizeGrid();
        });

        pageGrids.UserGrid.onRowSelect(function (e) {
            var url = "/Utilisateurs/Utilisateur?id=" + e.row.Id;
            $.post(url, function (data) {
                $('#divDetail').html(data);
            });
        });

        $("#btnAddUser").click(function () {
            var url = "/Utilisateurs/Utilisateur?id=0";
            $.post(url, function (data) {
                $('#divDetail').html(data);
            });
        });

        $(".btnEdit").click(function () {
            var userId = this.id;
            var leDlg = $("#dlgConfirm");
            $('#dlgContent').html("Êtes vous certain de vouloir supprimer cet utilisateur?");
            leDlg.attr("title", "Confirmation de suppression");
            leDlg.dialog({
                autoOpen: false,
                height: 180,
                width: 300,
                modal: true,
                closeText: "Annuler",
                buttons: [
                    {
                        text: "Oui",
                        click: function () {
                            window.location = "@Url.Action("Supprimer")/" + userId;
                            leDlg.dialog("close");
                        }
                    },
                    {
                        text: "Non",
                        click: function () {
                            leDlg.dialog("close");
                        }
                    }
                ]
            });
            leDlg.dialog("open");
        });
    });
</script>