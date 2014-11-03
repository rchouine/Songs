@ModelType Songs.Mvc.ChantViewModel

@Using Html.BeginForm("Enregistrer", "Chants", FormMethod.Post, New With {.id = "frmEdition"})
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    @Html.HiddenFor(Function(x) x.Id)

    @<table class="songTable">
        <tr>
            <td style="vertical-align: top;">
                <fieldset class="ui-widget ui-widget-content">
                    <div>
                        @Html.LabelFor(Function(x) x.Code, htmlAttributes:=New With {.class = "control-label col-md-2"})
                        @Html.ValidationMessageFor(Function(x) x.Code, "", New With {.class = "text-danger"})
                        <div>
                            @Html.EditorFor(Function(x) x.Code, New With {.htmlAttributes = New With {.class = "form-control"}})
                        </div>
                    </div>

                    <div class="form-group">
                        @Html.LabelFor(Function(x) x.Title, htmlAttributes:=New With {.class = "control-label col-md-2"})
                        @Html.ValidationMessageFor(Function(x) x.Title, "", New With {.class = "text-danger"})
                        <div class="col-md-10">
                            @Html.EditorFor(Function(x) x.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
                        </div>
                    </div>

                    <div class="form-group">
                        @Html.LabelFor(Function(x) x.Author, htmlAttributes:=New With {.class = "control-label col-md-2"})
                        @Html.ValidationMessageFor(Function(x) x.Author, "", New With {.class = "text-danger"})
                        <div class="col-md-10">
                            @Html.EditorFor(Function(x) x.Author, New With {.htmlAttributes = New With {.class = "form-control"}})
                        </div>
                    </div>

                    <div class="form-group">
                        @Html.LabelFor(Function(x) x.Translator, htmlAttributes:=New With {.class = "control-label col-md-2"})
                        @Html.ValidationMessageFor(Function(x) x.Translator, "", New With {.class = "text-danger"})
                        <div class="col-md-10">
                            @Html.EditorFor(Function(x) x.Translator, New With {.htmlAttributes = New With {.class = "form-control"}})
                        </div>
                    </div>
                    <div class="form-group">
                        @Html.LabelFor(Function(x) x.Tone, htmlAttributes:=New With {.class = "control-label col-md-2"})
                        @Html.ValidationMessageFor(Function(x) x.Tone, "", New With {.class = "text-danger"})
                        <div class="col-md-10">
                            @Html.DropDownListFor(Function(x) x.Tone, New SelectList(Model.ToneList, Model.Tone), New With {.htmlAttributes = New With {.class = "form-control"}})
                        </div>
                    </div>
                </fieldset>
            </td>
            <td style="padding-left: 8px; vertical-align: top;">
                <div id="tabsEditSong">
                    <ul>
                        <li><a href="#tabs-1">Paroles</a></li>
                        <li><a href="#tabs-2">Accords</a></li>
                        <li><a href="#tabs-3">Thèmes</a></li>
                    </ul>
                    <div id="tabs-1" style="padding: 0px;">
                        @Html.TextAreaFor(Function(x) x.Lyrics, New With {.style = "width: 350px; height: 250px;"})
                    </div>
                    <div id="tabs-2" style="padding: 0px;">
                        @Html.TextAreaFor(Function(x) x.ChordPro, New With {.style = "width: 350px; height: 250px;"})
                    </div>
                    <div id="tabs-3" style="padding: 20px; width: 314px; height: 216px;">
                        @Html.EditorFor(Function(x) x.Categories)
                    </div>
                </div>
            </td>
        </tr>
    </table>
End Using

<script type="text/javascript">

    $(document).ready(function () {
       //Gestion des onglets
        $("#tabsEditSong").tabs();
    });
</script>
