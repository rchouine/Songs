
<style>

    #divSelection {
        height: 100%;
        background-color: white;
    }
    .divSelectionSection {
        min-height: 25px;
        background-color: white;
        border: 1px solid lightgray;
        margin: 0 3px 3px 3px;
        padding: 3px 0;
    }
    #divSortable {
        margin: 0 3px;
        padding: 0.2em;
        font-size: 1.0em;
        height: 18px;
    }
    #inputdatepicker {
        margin: 0;
        border: none;
    }
</style>
    
    <table>
        <tr>
            <td><label style="padding-left: 4px;">Date:</label> </td>
            <td><div id='datepicker'></div></td>
            <td><input type="button" id="btnWord" value="Télécharger" /></td>
        </tr>
    </table>
    
    <div id="divSortable" class="ui-widget-header">Avant la réunion</div>
    <div id="SelectionAvant" class="divSelectionSection"></div>
    <div id="divSortable" class="ui-widget-header">Pendant la réunion</div>
    <div id="SelectionPendant" class="divSelectionSection"></div>
    <div id="divSortable" class="ui-widget-header"> Offrande - Communion - Autre</div>
    <div id="SelectionApres" class="divSelectionSection">
        @*<div id="divSortable" class="sortableItem ui-widget-content">Test</div>*@
    </div>
 
    <script>

    function CreateSelectedSong(parentContainerName, id, code, title, tone) {
        var html = "<div id='divSortable' class='sortableItem ui-widget-content'>" +
        "<div style='display: none'>" + id + "</div>" +
        "<div style='float: left; width: 50px;'>" + code + "</div>" +
        "<div style='float: left; padding-left: 10px;'>" + title + "</div>" +
        "<div style='float: right;' id='cboSelTone" + id + "' class='cboSelTone'></div>" +
        "</div>";

        $("#" + parentContainerName).html($("#" + parentContainerName).html() + html);
        CreateToneCboForSelection(id, tone);
    }

    function CreateToneCboForSelection(songId, selectedValue) {

        InitializeDropDown(songId, selectedValue);

        //Valider si on perd la fonctionalité du dropdown lors du prochain click
        $(".cboSelTone").click(function () {
            if ($("#" + this.id).jqxDropDownList('getItems') == null) {
                //Si on a perdu les items, il faut tout reconfigurer
                InitializeDropDown($(this).attr("songId"), this.innerText);
                $("#" + this.id).jqxDropDownList('open');
            }
        });
    }

    $("#btnWord").click(function () {
        window.open("/DocumentWord.aspx?selDate=" + JSON.stringify($("#datepicker").jqxDateTimeInput('value')), "DocumentWord")
        @*$.ajax({
                url: '@Url.Action("CreerDocumentWord", "Selections")',
                type: 'GET',
                dataType: 'json',
                cache: false,
                data: { selDate: JSON.stringify($("#datepicker").jqxDateTimeInput('value')) },
            });*@
    });


    function InitializeDropDown(songId, selectedValue) {
        $("#cboSelTone" + songId).jqxDropDownList({ source: tonalites, width: '50', height: '16' });
        $("#cboSelTone" + songId).val(selectedValue);
        $("#cboSelTone" + songId).attr("songId", songId);
        $("#cboSelTone" + songId).on('change', function (event) {
            var args = event.args;
            if (args) {
                var value = args.item.value;
                $.ajax({
                    url: '@Url.Action("ChangerTonalite", "Chants")',
                    type: 'GET',
                    dataType: 'json',
                    cache: false,
                    data: { songId: $(this).attr("songId"), newTone: args.item.value },
                });

            }
        });
    }

    $("#datepicker").jqxDateTimeInput({
        width: 100,
        height: 20,
        culture: 'fr-FR',
        formatString: 'yyyy-MM-dd',
    });
    $('#datepicker').on('change', function (event) {
        ChargerListe();
    });

    function ChargerListe() {
        $("#SelectionAvant").empty();
        $("#SelectionPendant").empty();
        $("#SelectionApres").empty();

        $.ajax({
            url: '@Url.Action("GetSelections", "Selections")',
            type: 'GET',
            dataType: 'json',
            cache: false,
            data: { selDate: JSON.stringify($("#datepicker").jqxDateTimeInput('value')) },
        }).done(function (data) {
            if (data.length > 0)
                for (var i = 0; i < data.length; i++) {
                    CreateSelectedSong(GetParentContainerName(data[i].Section), data[i].Id, data[i].Code, data[i].Title, data[i].Tone);
                }
        });
    }

    function SaveNewOrder() {
        var sng1 = GetNewOrder("SelectionAvant");
        var sng2 = GetNewOrder("SelectionPendant");
        var sng3 = GetNewOrder("SelectionApres");

        $.ajax({
            url: '@Url.Action("SaveSelection", "Selections")',
            type: 'GET',
            dataType: 'json',
            cache: false,
            data: { selDate: JSON.stringify($("#datepicker").jqxDateTimeInput('value')), section1SongId: JSON.stringify(sng1), section2SongId: JSON.stringify(sng2), section3SongId: JSON.stringify(sng3) },
        });
    }

    function GetParentContainerName(section) {
        if (section == 1)
            return "SelectionAvant";
        else if (section == 2)
            return "SelectionPendant";
        else
            return "SelectionApres";
    }

    function GetNewOrder(sectionName) {
        var ids = [];
        $("#" + sectionName).children(".sortableItem").each(function (index, value) {
            var id = $(value).children()[0].innerText;
            ids.push(id);
        });
        return ids;
    }

    $(function () {
        $(".divSelectionSection").sortable({
            //Supprimer l'item si il est sorti de la zone de drop
            receive: function (event, ui) {
                sortableIn = 1;
            },
            over: function (e, ui) { sortableIn = 1; },
            out: function (e, ui) { sortableIn = 0; },
            beforeStop: function (e, ui) {
                if (sortableIn == 0) {
                    ui.item.remove();
                }
            },
            connectWith: '.divSelectionSection',
            opacity: 0.6,
            cursor: 'move',
            update: function () {
                SaveNewOrder();
            }
        });
    });

    //Initialiser laa sélection du jour
    ChargerListe();

    </script>
