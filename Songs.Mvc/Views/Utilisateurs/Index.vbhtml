@ModelType List(Of Songs.Model.User)

@imports GridMvc.Html


<link href="~/Content/Gridmvc.css" rel="stylesheet" />
<script src="~/Scripts/jquery-1.8.2.js"></script>
<script src="~/Scripts/gridmvc.js"></script>

<style type="text/css">
    a.btnEdit {
        color: transparent !important;
        background-image: url('../../Images/pictosBoutons/modifier.gif');
        background-repeat: no-repeat;
        background-position: center;
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
</style>

<h2>Gestion des utilisateurs</h2>
<div id="divGrille" class="childTab" style="float: left;">
    @Html.Grid(Model).Named("UserGrid").Columns(Sub(col)
                                                    col.Add(Function(o) o.Id, True).Titled("Id")
                                                    col.Add(Function(o) o.Code).SetWidth(150).Titled("Code")
                                                    col.Add(Function(o) o.FirstName).SetWidth(150).Titled("Prénom")
                                                    col.Add(Function(o) o.Name).SetWidth(150).Titled("Nom")
                                                    col.Add(Function(o) o.DateLastAcces).SetWidth(50).Titled("").RenderValueAs(Function(o) Html.ActionLink("XXXXX", "About", "Home", New With {.Id = o.Id}, New With {.class = "btnEdit"})).Encoded(False).Sanitized(False)
                                                End Sub).Selectable(True).Sortable()
</div>
<div id="divDetail" class="childTab" style="float: left;"></div>

<script type="text/javascript">
    function ResizeGrid() {
        var h = $(window).height();
        var w = $(window).width();
        if (w > 1024) w = 1024;

        $(".grid-wrap").height(h - 260);
    }

    $(document).ready(function () {

        //Styliser les boutons
        $("button, input:submit, input:button").button();

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

    });

</script>