@ModelType  List(Of Songs.Model.UserSong)
@imports GridMvc.Html
@Code
    ViewData("Title") = "Songs"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@Scripts.Render("~/Scripts/jquery-ui-1.11.2/external/jquery/jquery.js")
@Scripts.Render("~/Scripts/jquery-ui-1.11.2/jquery-ui.js")
@Scripts.Render("~/Scripts/gridmvc.js")


<style type="text/css">
    a.btnEdit {
        color: transparent !important;
        background-image: url('../../Images/pictosBoutons/modifier.gif');
        background-repeat: no-repeat;
        background-position: center;
    }

    .grid-wrap {
        height: 200px;
        width: 380px;
        overflow-x: hidden;
        overflow-y: auto;
    }

    #divParoles {
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
            </form>

            @Html.Grid(Model).Named("SongGrid").Columns(Sub(col)
                                                            col.Add(Function(o) o.Id, True).Titled("Id")
                                                            col.Add(Function(o) o.Code).SetWidth(60).Titled("Code")
                                                            col.Add(Function(o) o.Title).SetWidth(300).Titled("Titre")
                                                            col.Add(Function(o) o.Tone).SetWidth(50).Titled("Ton")
                                                            col.Add().SetWidth(50).Titled("").RenderValueAs(Function(o) Html.ActionLink("XXXXX", "About", "Home", New With {.Id = o.Id}, New With {.class = "btnEdit"})).Encoded(False).Sanitized(False)
                                                        End Sub).Selectable(True).Sortable().EmptyText("Aucun chant trouvé.")
        </td>
        <td valign="top">
            <button class="ui-button" id='button 1'>hello world</button>
            <button id="createUser" class="ui-button-text-icon-primary">Test dialogue</button>
            <div id="dialog1" title="Dialogue de test" style="display: none;">
                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>
            </div>

            <div id="tabs">
                <ul>
                    <li><a href="#tabs-1">Paroles</a></li>
                    <li><a href="#tabs-2">Accords</a></li>
                    <li><a href="#tabs-3">Sélection</a></li>
                </ul>
                <div id="tabs-1">1</div>
                <div id="tabs-2">2</div>
                <div id="tabs-3">3</div>
            </div>

            @Html.TextBox("HFCurrTabIndex", 1)

            <div id="divParoles"></div>
        </td>
    </tr>
</table>

<script type="text/javascript">

    function ResizeGrid() {
        var w = $(window).width();
        if (w > 1024) w = 1024;

        $(".grid-wrap").height($(window).height() - 350);
        $("#divParoles").height($(window).height() - 220);
        $("#divParoles").width(w - 415);
    }

    $(document).ready(function () {

        ResizeGrid();
        $(window).resize(function () {
            ResizeGrid();
        });

        pageGrids.SongGrid.onRowSelect(function (e) {
            $.post("/Chants/Chant?id=" + e.row.Id, function (data) {
                if (data.Status <= 0) {
                    alert(data.Message);
                    return;
                }
                $('#divParoles').html(data);
                $("#tabs").tabs();
            });
        });

        $("#tabs").tabs();
        $('#tabs').click('tabsselect', function (event, ui) {
            $("#HFCurrTabIndex").val($("#tabs").tabs('option', 'active'));
        });
        $("#tabs").tabs("option", "active", $("#HFCurrTabIndex").val());



        $("button, input:submit, input:button").button();

        // Link to open the dialog
        $("#createUser").click(function (event) {
            $("#dialog1").dialog({
                autoOpen: false,
                height: 300,
                width: 350,
                modal: true,
                buttons: [
                    {
                        text: "Ok",
                        click: function () {
                            $(this).dialog("close");
                        }
                    },
                    {
                        text: "Cancel",
                        click: function () {
                            $(this).dialog("close");
                        }
                    }
                ]
            });
            $("#dialog1").dialog("open");
        });
    });
</script>

