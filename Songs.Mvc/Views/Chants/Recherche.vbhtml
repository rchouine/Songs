@Imports Songs.Mvc.Models
@ModelType  ChantsViewModel

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

<script type="text/javascript" src="~/Scripts/JQWidgets/jqxcombobox.js"></script>
<script type="text/javascript" src="~/Scripts/JQWidgets/jqxdropdownlist.js"></script>
<script type="text/javascript" src="~/Scripts/JQWidgets/jqxlistbox.js"></script>
<script type="text/javascript" src="~/Scripts/JQWidgets/jqxgrid.edit.js"></script>
<script type="text/javascript" src="~/Scripts/JQWidgets/jqxdragdrop.js"></script>

<script type="text/javascript" src="~/Scripts/JQWidgets/jqxinput.js"></script>

<script type="text/javascript" src="~/Scripts/JQWidgets/jqxdatetimeinput.js"></script>
<script type="text/javascript" src="~/Scripts/JQWidgets/jqxcalendar.js"></script>
<script type="text/javascript" src="~/Scripts/JQWidgets/jqxtooltip.js"></script>
<script type="text/javascript" src="~/Scripts/JQWidgets/globalization/globalize.js"></script>
<script type="text/javascript" src="~/Scripts/JQWidgets/globalization/globalize.culture.fr-FR.js"></script>

@Styles.Render("~/Content/StyleChordPro.css")

<style type="text/css">
    .btnEdit {
        padding: 6px;
        cursor: pointer;
    }

    .btnEdit:hover {
        background-color: #c7d1d6;
    }

    .ui-widget-overlay {
        z-index: 300;
    }

    .ui-dialog {
        z-index: 301;
    }

    .childTab {
        height: 200px;
        overflow-x: auto;
        overflow-y: auto;
    }
</style>

<table>
    <tr>
        <td valign="top">
            <fieldset class="ui-widget ui-widget-content" style="width: 380px; margin-bottom: 10px;">
                <legend class="">Recherche</legend>
                <table class="songTable">
                    <tr>
                        <td>@Html.EditorFor(Function(x) x.TypesRecherche)</td>
                        <td>
                            <table class="songTable">
                                <tr>
                                    <td>
                                        Filtrer par thème<br />
                                        @Html.DropDownList("categorie", New SelectList(Model.Categories, "Id", "Name"))
                                    </td>
                                    @If Session("USER_LEVEL") < Songs.Model.UserLevel.User Then
                                        @<td style="text-align: right; vertical-align: bottom;"><input type="Button" id="btnAddSong" value="Ajouter un chant" /></td>
                                    End If
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table>
                                            <tr>
                                                <td>@Html.TextBox("TexteRecherche", "", New With {.style = "width: 250px;"})</td>
                                                <td style="padding-left: 2px;"><input type="button" id="btnRecherche" value="Go" /></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
            @Html.Hidden("id", "0")
            @Html.Hidden("shift", "0")
            @Html.Hidden("songId")

            <div id="divGrille" style="float: left;">
                <div id="jqxgrid" style="z-index: 1;"></div>
            </div>
        </td>
        <td style="vertical-align: top; padding: 10px;">
            <div id="dialogChordPro" title="ChordPro" style="display: none; z-index: 400;">
                <div id="dialogContentChordPro" style="z-index: 400;"></div>
            </div>
            <div id="dialogChant" title="Modification" style="display: none;">
                <div id="dialogContentChant"></div>
            </div>

            <div id="tabsSongs">
                <ul>
                    <li><a href="#tabsSongs-1">Paroles</a></li>
                    <li><a href="#tabsSongs-2">Accords</a></li>
                    <li><a href="#tabsSongs-3">Sélection</a></li>
                </ul>
                <div id="tabsSongs-1"><div id="divParoles" class="childTab" style="text-align: center;"></div></div>
                <div id="tabsSongs-2">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 24px;"><input type="button" style="width: 24px;" id="btnBemol" value="b" title="Déscendre d'un demi-ton" /></td>
                            <td style="width: 24px;"><input type="button" style="width: 24px;" id="btnShift" value="#" title="Monter d'un demi-ton" /></td>
                            <td style="width: 10px;">&nbsp;&nbsp;&nbsp;</td>
                            <td style="width: 10px;">Affichage:</td>
                            <td style="width: 10px;"><input type="radio" id="rbBemol" name="rbFlat" checked="checked" title="Afficher en bémol" /></td>
                            <td style="width: 10px;"><label for="rbBemol" style="font-size: inherit; font-weight: inherit;">b</label></td>
                            <td style="width: 10px;"><input type="radio" id="rbSharp" name="rbFlat" title="Afficher en dièse" /></td>
                            <td style="width: 10px;"><label for="rbSharp" style="font-size: inherit; font-weight: inherit;">#</label></td>
                            <td style="width: 80%; text-align: right;"></td>
                        </tr>
                    </table>
                    <hr />
                    <div id="divAccords" class="childTab"></div>
                </div>
                <div id="tabsSongs-3"><div id="divSelection" class="childTab">@Html.Partial("Selection")</div></div>
            </div>
        </td>
    </tr>
</table>

<script type="text/javascript">

    var selectionCourante;
    var dropTarget = "";

    function ResizeGrid() {
        var h = $(window).height();
        var w = $(window).width();
        if (w > 1024) w = 1024;

        $("#divGrille").height(h - 333);
        $("#tabsSongs").height(h - 228);
        $("#tabsSongs").width(w - 480);
        $(".childTab").height(h - 284);
        $(".childTab").width(w - 500);
        $("#divAccords").height(h - 344); //Moins d'espace à cause des boutons de shift
    }

    function GetDataUrl() {
        return "/Chants/Rechercher?type=" + $("input[name=typeRecherche]:checked").val() + "&texte=" + encodeURI($("#TexteRecherche").val()) + "&categorie=" + $("#categorie").val();
    }
    var source = {
        datatype: "json",
        datafields: [
            { name: 'Id', type: 'integer' },
            { name: 'Code', type: 'string' },
            { name: 'Title', type: 'string' },
            { name: 'Tone', type: 'string' }
        ],
        url: GetDataUrl()
    }

    function RefreshGrid() {
        source.url = GetDataUrl();
        $("#jqxgrid").jqxGrid("updatebounddata");
        $("#jqxgrid").jqxGrid("clearselection");
    }
    $("#btnRecherche").click(function () {
        RefreshGrid();
    });
    $("#TexteRecherche").keydown(function( event ) {
        if ( event.which == 13 ) {
            RefreshGrid();
        }
    });

    $(document).ready(function () {

        ResizeGrid();
        $(window).resize(function () {
            ResizeGrid();
        });

        // prepare the data
        var dataAdapter = new $.jqx.dataAdapter(source);

        var localizationobj = {
            sortascendingstring: "Triz ascendant",
            sortdescendingstring: "Tri descendant",
            sortremovestring: "Enlever tri",
            emptydatastring: "Aucun chant trouvé"
        };

        var columns = [
                { text: 'Id', datafield: 'Id', hidden: true },
                { text: 'Code', datafield: 'Code', width: '16%', cellbeginedit: function () { return false; }},
                @If Session("USER_LEVEL") < Songs.Model.UserLevel.User Then
                    @<Text>
                        { text: 'Titre', datafield: 'Title', width: '66%', cellbeginedit: function () { return false; } },
                    </Text>
                Else
                     @<Text>
                        { text: 'Titre', datafield: 'Title', width: '72%', cellbeginedit: function () { return false; } },
                    </Text>
                End If
                {
                    text: 'Ton', datafield: 'Tone', width: '12%',
                    // update the editor's value before saving it.
                    cellvaluechanging: function (row, column, columntype, oldvalue, newvalue) {
                        var songId = $("#jqxgrid").jqxGrid('getcellvalue', row, 'Id');
                        $.ajax({
                            url: '@Url.Action("ChangerTonalite")',
                            type: 'GET',
                            dataType: 'json',
                            cache: false,
                            data: { songId: songId, newTone: newvalue },
                        });
                    }
                },
                @If Session("USER_LEVEL") < Songs.Model.UserLevel.User Then
                    @<Text>
                    {
                        text: '', width: '5%', sortable: false, cellsrenderer: function () {
                            return '<div><img src="../../Images/pictosBoutons/modifier.gif" class="btnEdit" /></div>';
                        }
                    },
                    </Text>
                End If
            ];

        $("#jqxgrid").jqxGrid(
        {
            theme: "web",
            width: '400px',
            height: '100%',
            source: dataAdapter,
            columnsresize: true,
            sortable: true,
            editable: true,
            localization: localizationobj,
            columns: columns,
            rendered: function () {
                // select all grid cells for Drag-Drop
                var gridCells = $('#jqxgrid').find('.jqx-grid-cell');
                if ($('#jqxgrid').jqxGrid('groups').length > 0) {
                    gridCells = $('#jqxgrid').find('.jqx-grid-group-cell');
                }
                // initialize the jqxDragDrop plug-in. Set its drop target to divSelection.
                gridCells.jqxDragDrop({
                    appendTo: 'body',
                    dragZIndex: 99999,
                    dropAction: 'none',
                    initFeedback: function (feedback) {
                        feedback.height(25);
                        feedback.width(300);
                    },
                    dropTarget: '.divSelectionSection',
                    onDropTargetEnter: function (target) {
                        dropTarget = target.context.id;
                    },
                    onDropTargetLeave: function (target) {
                        dropTarget = "";
                    },
                });

                // Set the dragged object.
                gridCells.off('dragStart');
                gridCells.on('dragStart', function (event) {
                    var value = $(this).text();
                    var position = $.jqx.position(event.args);
                    var cell = $("#jqxgrid").jqxGrid('getcellatposition', position.left, position.top);
                    $(this).jqxDragDrop('data', $("#jqxgrid").jqxGrid('getrowdata', cell.row));
                    var groupslength = $('#jqxgrid').jqxGrid('groups').length;
                    // update feedback's display value.
                    var feedback = $(this).jqxDragDrop('feedback');
                    var feedbackContent = $(this).parent().clone();
                    var table = '<div id="divSortable" class="sortableItem ui-widget-content">';
                    $.each(feedbackContent.children(), function (index) {
                        if (index < groupslength)
                            return true;
                        if (index > 0 && index < 4) {
                            table += '<div style="float: left;padding-right: 10px; margin: 0;">';
                            table += $(this).text();
                            table += '</div>';
                        }
                    });
                    table += '</div>';
                    feedback.html(table);
                });

                // Set the dropped object.
                gridCells.off('dragEnd');
                gridCells.on('dragEnd', function (event) {
                    var value = $(this).jqxDragDrop('data');

                    if (dropTarget != "") {
                        CreateSelectedSong(dropTarget, value.Id, value.Code, value.Title, value.Tone);
                        dropTarget = "";
                        $(this).jqxDragDrop('feedback').html("");
                        SaveNewOrder();
                    }
                    body.style.cursor = 'default';
                });
            },
        });
        $("#jqxgrid").bind('bindingcomplete', function () {
            //Set editButton function
            SetEditButton();
        });
        $("#jqxgrid").on('rowclick', function () {
            // put the focus back to the Grid. Otherwise, the focus goes to the drag feedback element.
            $("#jqxgrid").jqxGrid('focus');
        });
        $("#jqxgrid").on('rowselect', function (event) {
            var value = $("#jqxgrid").jqxGrid('getcellvalue', event.args.rowindex, 'Id');
            var tone = $("#jqxgrid").jqxGrid('getcellvalue', event.args.rowindex, 'Tone');
            $("#songId").val(value);
            updateSong(value, 0, tone);
        });
        $("#jqxgrid").on("sort", function (event) {
            SetEditButton();
        });
        $("#jqxgrid").on('cellbeginedit', function (event) {
            setTimeout(function () { SetEditButton(); }, 1);
        });
        $("#jqxgrid").on('cellendedit', function (event) {
            setTimeout(function () { SetEditButton(); }, 1);
        });

        function SetEditButton() {
            $(".btnEdit").click(function () {
                Modifier($("#songId").val());
            });
        }

        $("#btnBemol").click(function () {
            updateSong($('#songId').val(), eval($('#shift').val()) - 1);
        });
        $("#btnShift").click(function () {
            updateSong($('#songId').val(), eval($('#shift').val()) + 1);
        });
        $("#rbBemol").click(function () {
            updateSong($('#songId').val(), eval($('#shift').val()));
        });
        $("#rbSharp").click(function () {
            updateSong($('#songId').val(), eval($('#shift').val()));
        });

        function updateSong(id, shift, tone) {
            var url = "/Chants/Chant?id=" + id + "&shift=" + shift + "&sharp=" + getSharp();
            if (tone != null)
                url += "&tone=" + escape(tone.replace("é", "e"));
            $.post(url, function (data) {
                $('#songId').val(data[0]);
                $('#shift').val(data[1]);
                $('#divParoles').html(data[3]);
                $('#divAccords').html(data[4]);
                $("#tabsSongs").tabs();
            });
        }

        function getSharp() {
            if ($('#rbSharp').prop('checked'))
                return "1";
            else
                return "0";
        }

        //Gestion des onglets
        var ongletsChants = $("#tabsSongs");
        ongletsChants.tabs();

        $("#ChordPro").click(function (event) {

            if ($("#SelectionAvant").children(".sortableItem").length > 0)
                selectionCourante = $("#SelectionAvant").children(".sortableItem").first();
            else
                selectionCourante = $("#SelectionPendant").children(".sortableItem").first();

            $('#songId').val(selectionCourante.attr("songId"));

            var h = $(window).height() - 0;
            var w = $(window).width() - 0;

            var url = "/Chants/Chant?id=" + $('#songId').val() + "&shift=" + $('#shift').val() + "&sharp=" + getSharp() + "&tone=" + escape(selectionCourante.attr("tone").replace("é", "e"));
            $.post(url, function (data) {
                $('#shift').val(data[1]);
                $('#dialogContentChordPro').html(data[4]);
                $("#dialogChordPro").dialog({
                    autoOpen: false,
                    height: h,
                    width: w,
                    modal: true,
                    title: data[2],
                    closeText: "Fermer",
                    buttons: [
                        {
                            text: "↓b",
                            title: "Déscendre d'un demi-ton",
                            style: "float: left",
                            click: function () {
                                $('#shift').val(eval($('#shift').val()) - 1);
                                ShiftSong();
                            }
                        },
                        {
                            text: "↑#",
                            title: "Monter d'un demi-ton",
                            style: "float: left",
                            click: function () {
                                $('#shift').val(eval($('#shift').val()) + 1);
                                ShiftSong();
                            }
                        },
                        {
                            text: "b",
                            title: "Afficher les demi-tons en b",
                            style: "float: left; margin-left: 10px;",
                            click: function () {
                                $('#rbBemol').prop('checked', true);
                                $('#rbSharp').prop('checked', false);
                                ShiftSong();
                            }
                        },
                        {
                            text: "#",
                            title: "Afficher les demi-tons en #",
                            style: "float: left",
                            click: function () {
                                $('#rbBemol').prop('checked', false);
                                $('#rbSharp').prop('checked', true);
                                ShiftSong();
                            }
                        },
                        {
                            text: "Préc.",
                            title: "Chant précédent",
                            style: "float: left; margin-left: 20px;",
                            click: function () {

                                var prev = selectionCourante.prev(".sortableItem");

                                if (prev.length == 0) {
                                    prev = selectionCourante.parent().prev().prev(".divSelectionSection").children(".sortableItem").last();
                                }

                                if (prev.length != 0) {
                                    selectionCourante = prev;
                                    $('#songId').val(selectionCourante.attr("songId"));
                                    ShiftSong(selectionCourante.attr("tone"));
                                }
                            }
                        },
                        {
                            text: "Suiv.",
                            title: "Chant suivant",
                            style: "float: left",
                            click: function () {

                                var next = selectionCourante.next(".sortableItem");

                                if (next.length == 0) {
                                    next = selectionCourante.parent().next().next(".divSelectionSection").children(".sortableItem").first();
                                }

                                if (next.length != 0) {
                                    selectionCourante = next;
                                    $('#songId').val(selectionCourante.attr("songId"));
                                    ShiftSong(selectionCourante.attr("tone"));
                                }
                            }
                        },
                        {
                            id: "dialogDefaultButton",
                            text: "Ok",
                            title: "Fermer cette fenêtre",
                            style: "float: right",
                            focused: true,
                            click: function () {
                                $(this).dialog("close");
                            }
                        },
                    ]
                });

                $(".ui-dialog-buttonset").width("100%");
                $(".ui-dialog").css("z-index", 9999);
                $("#dialogChordPro").dialog("open");
                $("#dialogDefaultButton").focus();
            });

            function ShiftSong(tone) {
                var url = "/Chants/Chant?id=" + $('#songId').val() + "&shift=" + $('#shift').val() + "&sharp=" + getSharp();
                if (tone != null)
                    url += "&tone=" + escape(tone.replace("é", "e"));

                $.post(url, function (data) {
                    $('#shift').val(data[1]);
                    $('#dialogChordPro').dialog('option', 'title', data[2]);
                    $('#dialogContentChordPro').html(data[4]);
                });
            }

        });
    });

    $("#btnAddSong").click(function (event) {
        Modifier(0);
    });

    function Modifier(id) {
        var url = "/Chants/Modifier?id=" + id;
        var leDlg = $("#dialogChant");
        $.post(url, function (data) {
            $('#dialogContentChant').html(data);
            var leTitre;
            if (id == 0)
                leTitre = "Ajouter un chant";
            else
                leTitre = "Modification";
            leDlg.dialog({
                autoOpen: false,
                height: 430,
                width: 610,
                modal: true,
                closeText: "Fermer",
                title: leTitre,
                buttons: [
                    {
                        text: "Enregistrer",
                        click: function () {
                            var frmEdit = $("#frmEdition");
                            $.post(frmEdit.attr("action"), frmEdit.serialize(), function (r, s) {
                                if (r == "") {
                                    RefreshGrid();
                                    leDlg.dialog("close");
                                }
                                else
                                    $('#dialogContentChant').html(r);
                            });
                        }
                    },
                    {
                        text: "Annuler",
                        click: function () {
                            leDlg.dialog("close");
                        }
                    },
                ]
            });
            $(".ui-dialog").css("z-index", 9999);
            leDlg.dialog("open");
        });
    }
</script>


