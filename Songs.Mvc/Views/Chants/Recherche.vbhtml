@ModelType  Songs.Mvc.ChantsViewModel
@imports GridMvc.Html
@Code
    ViewData("Title") = "Songs"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<link href="~/Content/StyleChordPro.css" rel="stylesheet" />

@Scripts.Render("~/Scripts/jquery-ui-1.11.2/external/jquery/jquery.js")
@Scripts.Render("~/Scripts/gridmvc.js")

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
        height: 200px;
        width: 380px;
        overflow-x: hidden;
        overflow-y: auto;
    }

    .childTab {
        height: 200px;
        text-align: center;
        overflow-x: auto;
        overflow-y: auto;
    }
</style>

<table>
    <tr>
        <td valign="top">
            <form method="post" action="Rechercher">
                <fieldset style="width: 370px; margin-bottom: 4px;">
                    <legend>Recherche</legend>
                    <table>
                        <tr>
                            <td>@Html.RadioButton("typeRecherche", "titre", True, New With {.id = "typeRecherche1"}) </td>
                            <td><label for="typeRecherche1">Titre</label></td>
                        </tr>
                        <tr>
                            <td>@Html.RadioButton("typeRecherche", "code", If(Request("typeRecherche") = "1", True, False), New With {.id = "typeRecherche2"}) </td>
                            <td><label for="typeRecherche2">Code</label></td>
                        </tr>
                        <tr>
                            <td>@Html.RadioButton("typeRecherche", "paroles", If(Request("typeRecherche") = "3", True, False), New With {.id = "typeRecherche3"})</td>
                            <td><label for="typeRecherche3">Paroles</label></td>
                            <td>
                                @Html.TextBox("texteRecherche")
                                <input type="submit" id="btnRecherche" value="Go" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                @Html.HiddenFor(Function(x) x.TabIndex)
                @Html.Hidden("id", "0")
                @Html.Hidden("shift", "0")
            </form>

            @Html.Grid(Model.Chants).Named("SongGrid").Columns(Sub(col)
                                                                   col.Add(Function(o) o.Id, True).Titled("Id")
                                                                   col.Add(Function(o) o.Code).SetWidth(60).Titled("Code")
                                                                   col.Add(Function(o) o.Title).SetWidth(300).Titled("Titre")
                                                                   col.Add(Function(o) o.Tone).SetWidth(50).Titled("Ton")
                                                                   col.Add().SetWidth(50).Titled("").RenderValueAs(Function(o) Html.ActionLink("XXXXX", "About", "Home", New With {.Id = o.Id}, New With {.class = "btnEdit"})).Encoded(False).Sanitized(False)
                                                               End Sub).Selectable(True).Sortable().EmptyText("Aucun chant trouvé.")
        </td>
        <td valign="top">
            <button id="ChordPro">ChordPro</button>
            <div id="dialogChordPro" title="Dialogue de test" style="display: none;">
                <div id="dialogContentChordPro"></div>
            </div>

            <div id="tabs">
                <ul>
                    <li><a href="#tabs-1">Paroles</a></li>
                    <li><a href="#tabs-2">Accords</a></li>
                    <li><a href="#tabs-3">Sélection</a></li>
                </ul>
                <div id="tabs-1"><div id="divParoles" class="childTab"></div></div>
                <div id="tabs-2">
                    <table>
                        <tr>
                            <td><input type="button" id="btnBemol" value="b" title="Déscendre d'un demi-ton" /></td>
                            <td><input type="button" id="btnShift" value="#" title="Monter d'un demi-ton" /></td>
                            <td>&nbsp;&nbsp;&nbsp;</td>
                            <td>Affichage: </td>
                            <td><input type="radio" id="rbBemol" name="rbFlat" checked="checked" title="Afficher en bémol" /></td>
                            <td><label for="rbBemol" style="font-size: inherit; font-weight: inherit;">b</label></td>
                            <td><input type="radio" id="rbSharp" name="rbFlat" title="Afficher en dièse" /></td>
                            <td><label for="rbSharp" style="font-size: inherit; font-weight: inherit;">#</label></td>
                        </tr>
                    </table>
                    <hr />
                    <div id="divAccords" class="childTab"></div>
                </div>
                <div id="tabs-3">@Html.Partial("TestChildView")</div>
            </div>
        </td>
    </tr>
</table>

<script type="text/javascript">

    function ResizeGrid() {
        var h = $(window).height();
        var w = $(window).width();
        if (w > 1024) w = 1024;

        $(".grid-wrap").height(h - 330);
        $("#tabs").height(h - 250);
        $("#tabs").width(w - 415);
        $(".childTab").height(h - 310);
        $(".childTab").width(w - 435);
        $("#divAccords").height(h - 354); //Moins d'espace pour les boutons de shift
    }

    $(document).ready(function () {

        ResizeGrid();
        $(window).resize(function () {
            ResizeGrid();
        });

        pageGrids.SongGrid.onRowSelect(function (e) {
            updateSong(e.row.Id, 0);
        });
        $("#btnBemol").click(function () {
            updateSong($('#id').val(), eval($('#shift').val()) - 1);
        });
        $("#btnShift").click(function () {
            updateSong($('#id').val(), eval($('#shift').val()) + 1);
        });
        $("#rbBemol").click(function () {
            updateSong($('#id').val(), eval($('#shift').val()));
        });
        $("#rbSharp").click(function () {
            updateSong($('#id').val(), eval($('#shift').val()));
        });

        function updateSong(id, shift) {
            var url = "/Chants/Chant?id=" + id + "&shift=" + shift + "&sharp=" + getSharp();
            $.post(url, function (data) {
                $('#id').val(data[0]);
                $('#shift').val(data[1]);
                $('#divParoles').html(data[3]);
                $('#divAccords').html(data[4]);
                $("#tabs").tabs();
            });
        }

        function getSharp() {
            if ($('#rbSharp').prop('checked'))
                return "1";
            else
                return "0";
        }

        //Gestion des onglets
        $("#tabs").tabs();
        $('#tabs').click('tabsselect', function (event, ui) {
            $("#TabIndex").val($("#tabs").tabs('option', 'active'));
        });
        $("#tabs").tabs("option", "active", $("#TabIndex").val());


        //Styliser les boutons
        $("button, input:submit, input:button").button();


        $("#ChordPro").click(function (event) {
            var h = $(window).height() - 50;
            var w = $(window).width() - 200;

            var url = "/Chants/Chant?id=" + $('#id').val() + "&shift=" + $('#shift').val() + "&sharp=" + getSharp();
            $.post(url, function (data) {
                $('#dialogContentChordPro').html(data[4]);
                $("#dialogChordPro").attr("title", data[2])
                $("#dialogChordPro").dialog({
                    autoOpen: false,
                    height: h,
                    width: w,
                    modal: true,
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
                            text: "Ok",
                            title: "Fermer cette fenêtre",
                            style: "float: right",
                            click: function () {
                                $(this).dialog("close");
                            }
                        },
                    ]
                });

                $(".ui-dialog-buttonset").width("100%");
                $("#dialogChordPro").dialog("open");
            });

            function ShiftSong() {
                var url = "/Chants/Chant?id=" + $('#id').val() + "&shift=" + $('#shift').val() + "&sharp=" + getSharp();
                $.post(url, function (data) {
                    $('#shift').val(data[1]);
                    $('#dialogContentChordPro').html(data[4]);
                });
            }

        });
    });
</script>

