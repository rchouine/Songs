@ModelType  Songs.Mvc.ChantsViewModel
@imports GridMvc.Html
@Code
    'ViewData("Title") = "Chants"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@Styles.Render("~/Content/StyleChordPro.css")
@Styles.Render("~/Content/Gridmvc.css")
@Scripts.Render("~/Scripts/gridmvc.js")

<style type="text/css">
    .btnEdit {
        padding: 0px 8px;
        cursor: pointer;
    }

    a.btnEdit:hover {
        background-color: #c7d1d6;
    }

    .grid-wrap {
        height: 200px;
        width: 384px;
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
            @Using Html.BeginForm("Rechercher", "Chants", FormMethod.Post)
            @<fieldset class="ui-widget ui-widget-content" style="width: 360px; margin-bottom: 10px;">
                <legend class="">Recherche</legend>
                 <table class="songTable">
                     <tr>
                         <td>@Html.EditorFor(Function(x) x.TypesRecherche)</td>
                         <td>
                             <table class="songTable">
                                 <tr>
                                     <td>
                                         Filtrer par thème<br />
                                         @Html.DropDownList("categorie", New SelectList(Model.Categories, "Id", "Name", Model.CriteresRecherche.CatId))
                                     </td>
                                     <td style="text-align: right; vertical-align: bottom;"><input type="Button" value="Ajouter un chant" onclick="javascript: Modifier(0);" /></td>
                                 </tr>
                                 <tr>
                                     <td colspan="2">
                                         <table>
                                             <tr>
                                                 <td>@Html.TextBoxFor(Function(x) x.TexteRecherche, New With {.style = "width: 250px;"})</td>
                                                 <td style="padding-left: 2px;"><input type="submit" id="btnRecherche" value="Go" /></td>
                                             </tr>
                                         </table>
                                     </td>
                                 </tr>
                             </table>
                         </td>
                     </tr>
                 </table>
            </fieldset>
                @Html.HiddenFor(Function(x) x.TabIndex)
                @Html.Hidden("id", "0")
                @Html.Hidden("shift", "0")
            End Using

            @Html.Grid(Model.Chants).Named("SongGrid").Columns(Sub(col)
                                                                   col.Add(Function(o) o.Id, True).Titled("Id")
                                                                   col.Add(Function(o) o.Code).SetWidth(30).Titled("Code")
                                                                   col.Add(Function(o) o.Title).SetWidth(300).Titled("Titre")
                                                                   col.Add(Function(o) o.Tone).SetWidth(20).Titled("Ton")
                                                                   col.Add().SetWidth(20).Titled("").RenderValueAs(Function(o) Html.Raw("<img id='" & o.Id & "' class='btnEdit' src='../../Images/pictosBoutons/modifier.gif' title='Modifier'/>")).Encoded(False).Sanitized(False)
                                                               End Sub).Selectable(True).Sortable().EmptyText("Aucun chant trouvé.")
        </td>
        <td style="vertical-align: top; padding: 10px;">
            <div id="dialogChordPro" title="ChordPro" style="display: none;">
                <div id="dialogContentChordPro"></div>
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
                <div id="tabsSongs-1"><div id="divParoles" class="childTab"></div></div>
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
                            <td style="width: 80%; text-align: right;"><button id="ChordPro">Fenetre</button></td>
                        </tr>
                    </table>
                    <hr />
                    <div id="divAccords" class="childTab"></div>
                </div>
                <div id="tabsSongs-3">@Html.Partial("TestChildView")</div>
            </div>
        </td>
    </tr>
</table>

<script type="text/javascript">

    function ResizeGrid() {
        var h = $(window).height();
        var w = $(window).width();
        if (w > 1024) w = 1024;

        $(".grid-wrap").height(h - 340);
        $("#tabsSongs").height(h - 228);
        $("#tabsSongs").width(w - 415);
        $(".childTab").height(h - 284);
        $(".childTab").width(w - 435);
        $("#divAccords").height(h - 344); //Moins d'espace pour les boutons de shift
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
        ongletsChants.click('tabsselect', function (event, ui) {
            $("#TabIndex").val(ongletsChants.tabs('option', 'active'));
        });
        ongletsChants.tabs("option", "active", $("#TabIndex").val());

        $("#ChordPro").click(function (event) {
            var h = $(window).height() - 50;
            var w = $(window).width() - 200;

            var url = "/Chants/Chant?id=" + $('#id').val() + "&shift=" + $('#shift').val() + "&sharp=" + getSharp();
            $.post(url, function (data) {
                $('#dialogContentChordPro').html(data[4]);
                $("#dialogChordPro").dialog({
                    autoOpen: false,
                    height: h,
                    width: w,
                    modal: true,
                    title:  data[2],
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

    $(".btnEdit").click(function () {
        Modifier(this.id);
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
                                    window.location.href = "/Chants/Rechercher";
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
            leDlg.dialog("open");
        });
    }
</script>

