@ModelType IEnumerable(Of Songs.Model.User)

@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<link href="~/Content/JQWidgets/jqx.base.css" rel="stylesheet" />
<link href="~/Content/JQWidgets/jqx.web.css" rel="stylesheet" />

<script type="text/javascript" src="~/Scripts/JQWidgets/jqxcore.js"></script>
<script type="text/javascript" src="~/Scripts/JQWidgets/jqxdata.js"></script>
<script type="text/javascript" src="~/Scripts/JQWidgets/jqxgrid.js"></script>
<script type="text/javascript" src="~/Scripts/JQWidgets/jqxgrid.selection.js"></script>
<script type="text/javascript" src="~/Scripts/JQWidgets/jqxgrid.columnsresize.js"></script>
<script type="text/javascript" src="~/Scripts/JQWidgets/jqxgrid.sort.js"></script>
<script type="text/javascript" src="~/Scripts/JQWidgets/jqxscrollbar.js"></script>
<script type="text/javascript" src="~/Scripts/JQWidgets/jqxbuttons.js"></script>
<script type="text/javascript" src="~/Scripts/JQWidgets/jqxmenu.js"></script>

<style type="text/css">
    .btnDelete {
        padding: 6px;
        cursor: pointer;
    }

        .btnDelete:hover {
            background-color: #c7d1d6;
        }

    .ui-widget-overlay {
        z-index: 300;
    }

    .ui-dialog {
        z-index: 301;
    }
</style>

<h2>Gestion des utilisateurs</h2>

<div id="divGrille" class="childTab" style="float: left; margin-right: 20px;">
    <div id="jqxgrid"></div>
</div>
@Html.Hidden("userId")

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

        $("#divGrille").height(h - 260);
    }

    $(document).ready(function () {

        ResizeGrid();
        $(window).resize(function () {
            ResizeGrid();
        });

        // prepare the data
        var source = {
            datatype: "json",
            datafields: [
                { name: 'Id', type: 'integer' },
                { name: 'Code', type: 'string' },
                { name: 'FirstName', type: 'string' },
                { name: 'Name', type: 'string' }
            ],
            url: "Utilisateurs/Liste"
        };
        var dataAdapter = new $.jqx.dataAdapter(source);
        $("#jqxgrid").jqxGrid(
        {
            theme: "web",
            width: '350px',
            height: '100%',
            source: dataAdapter,
            columnsresize: true,
            sortable: true,
            editable: false,
            columns: [
                { text: 'Id', datafield: 'Id', hidden: true },
                { text: 'Code', datafield: 'Code', width: '30%' },
                { text: 'Prénom', datafield: 'FirstName', width: '32%' },
                { text: 'Nom', datafield: 'Name', width: '30%' },
                {
                    text: '', width: '8%', sortable: false, cellsrenderer: function () {
                        return '<div><img src="../../Images/pictosBoutons/supprimer.gif" class="btnDelete" /></div>';
                    }
                },
            ],
            ready: function () {

                var localizationobj = {};
                localizationobj.sortascendingstring = "Tri ascendant";
                localizationobj.sortdescendingstring = "Tri descendant";
                localizationobj.sortremovestring = "Enlever tri";
                // apply localization.
                $("#jqxgrid").jqxGrid('localizestrings', localizationobj);

                //Set editButton function
                SetDeleteButton();
            },
        });
        $("#jqxgrid").on('rowselect', function (event) {
            var value = $("#jqxgrid").jqxGrid('getcellvalue', event.args.rowindex, 'Id');
            $("#userId").val(value);

            var url = "/Utilisateurs/Utilisateur?id=" + value;
            $.post(url, function (data) {
                $('#divDetail').html(data);
            });
        });
        $("#jqxgrid").on("sort", function (event) {
            //Reactiver delete button
            SetDeleteButton();
        });

        $("#btnAddUser").click(function () {
            var url = "/Utilisateurs/Utilisateur?id=0";
            $.post(url, function (data) {
                $('#divDetail').html(data);
            });
        });
    });

    function SetDeleteButton() {
        $(".btnDelete").click(function () {

            var userId = $("#userId").val();
            var leDlg = $("#dlgConfirm");
            $('#dlgContent').html("Êtes vous certain de vouloir supprimer cet utilisateur?");
            leDlg.attr("title", "Confirmation de suppression");
            leDlg.dialog({
                autoOpen: false,
                height: 180,
                width: 300,
                modal: true,
                zIndex: 300,
                buttons: [
                    {
                        text: "Oui",
                        click: function () {
                            window.location = "/Utilisateurs/Supprimer/" + userId;
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
    }
</script>
