
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
        $("#datepicker").jqxDateTimeInput({
            width: 100,
            height: 20,
            culture: 'fr-FR',
            formatString: 'yyyy-MM-dd',
        });
        $('#datepicker').on('change', function (event) {
            //var jsDate = event.args.date;
            alert("changer la liste");
        });

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
                    alert("Il faut sauver le nouvel ordre: " + GetNewOrder(this.id));
                }
            });
        });


    </script>
