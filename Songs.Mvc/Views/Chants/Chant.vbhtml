@ModelType Songs.Mvc.ChantViewModel

@Scripts.Render("~/bundles/jquery")
@Scripts.Render("~/bundles/jqueryval")

@Using Html.BeginForm("Enregistrer", "Chants", FormMethod.Post, New With {.id = "formulaire"})
    @Html.AntiForgeryToken()
    
    @<div>
        <h4>Ajout / modification de chant</h4>
        <hr />
        @Html.ValidationSummary(True, "", New With { .class = "text-danger" })
        @Html.HiddenFor(Function(x) x.Id)

         <div style="float: left;">
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
             </fieldset>

         </div>
         <div style="float: left; padding-left: 8px;">
             <div id="tabs">
                 <ul>
                     <li><a href="#tabs-1">Paroles</a></li>
                     <li><a href="#tabs-2">Accords</a></li>
                     <li><a href="#tabs-3">Thèmes</a></li>
                 </ul>
                 <div id="tabs-1" style="padding: 0px;">
                     @Html.TextAreaFor(Function(x) x.Lyrics, 15, 50, New With {.htmlAttributes = New With {.class = ""}})
                 </div>
                 <div id="tabs-2" style="padding: 0px;">
                     @Html.TextAreaFor(Function(x) x.ChordPro, 15, 50, New With {.htmlAttributes = New With {.class = ""}})
                 </div>
                 <div id="tabs-3" style="padding: 20px; width: 310px; height: 220px;">
                     @Html.EditorFor(Function(x) x.Categories)
                 </div>
             </div>
         </div>
    </div>
    @<br />
    @<input type="submit" value="Enregistrer" class="btn btn-default" />
End Using

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

<script type="text/javascript">

    $(document).ready(function () {
       //Gestion des onglets
        $("#tabs").tabs();

    });
</script>
