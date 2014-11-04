@ModelType Songs.Mvc.ChantViewModel

@Using Html.BeginForm("Enregistrer", "Chants", FormMethod.Post, New With {.id = "frmEdition"})
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    @Html.HiddenFor(Function(x) x.Id)

    @<table class="songTable">
        <tr>
            <td style="vertical-align: top;">
                <fieldset class="ui-widget ui-widget-content">
                    @If Session("USER_LEVEL") < Songs.Model.UserLevel.User Then
                        @<div>
                            @Html.LabelFor(Function(x) x.Code)
                            @Html.ValidationMessageFor(Function(x) x.Code, "")<br />
                            @Html.EditorFor(Function(x) x.Code)
                        </div>

                        @<div>
                            @Html.LabelFor(Function(x) x.Title)
                            @Html.ValidationMessageFor(Function(x) x.Title, "")<br />
                            @Html.EditorFor(Function(x) x.Title)
                        </div>

                        @<div class="form-group">
                            @Html.LabelFor(Function(x) x.Author)
                            @Html.ValidationMessageFor(Function(x) x.Author, "")<br />
                            @Html.EditorFor(Function(x) x.Author)
                        </div>

                        @<div class="form-group">
                            @Html.LabelFor(Function(x) x.Translator)
                            @Html.ValidationMessageFor(Function(x) x.Translator, "")<br />
                            @Html.EditorFor(Function(x) x.Translator)
                        </div>
                    Else
                        @<div>
                            @Html.LabelFor(Function(x) x.Code, htmlAttributes:=New With {.style = "color: grey;"})<br />
                            @Html.EditorFor(Function(x) x.Code, New With {.htmlAttributes = New With {.readonly = "readonly"}})
                        </div>
                        @<div>
                            @Html.LabelFor(Function(x) x.Title, htmlAttributes:=New With {.style = "color: grey;"})<br />
                            @Html.EditorFor(Function(x) x.Title, New With {.htmlAttributes = New With {.readonly = "readonly"}})
                        </div>

                        @<div class="form-group">
                            @Html.LabelFor(Function(x) x.Author, htmlAttributes:=New With {.style = "color: grey;"})<br />
                            @Html.EditorFor(Function(x) x.Author, New With {.htmlAttributes = New With {.readonly = "readonly"}})
                        </div>

                        @<div class="form-group">
                            @Html.LabelFor(Function(x) x.Translator, New With {.style = "color: grey;"})<br />
                            @Html.EditorFor(Function(x) x.Translator, New With {.htmlAttributes = New With {.readonly = "readonly"}})
                        </div>
                    End If

                    <div class="form-group">
                        @Html.LabelFor(Function(x) x.Tone)
                        @Html.ValidationMessageFor(Function(x) x.Tone, "")<br />
                        @Html.DropDownListFor(Function(x) x.Tone, New SelectList(Model.ToneList, Model.Tone))
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
                        @If Session("USER_LEVEL") < Songs.Model.UserLevel.User Then
                            @Html.TextAreaFor(Function(x) x.Lyrics, New With {.style = "width: 350px; height: 250px;"})
                        Else
                            @Html.TextAreaFor(Function(x) x.Lyrics, New With {.readonly = "readonly", .style = "width: 350px; height: 250px;"})
                        End If
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
