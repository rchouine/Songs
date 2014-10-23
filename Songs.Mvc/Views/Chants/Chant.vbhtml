@ModelType Songs.Model.Song

@Styles.Render("~/Scripts/jquery-ui-1.11.2/themes/cupertino/jquery-ui.css")
@Scripts.Render("~/Scripts/jquery-ui-1.11.2/external/jquery/jquery.js")
@Scripts.Render("~/Scripts/jquery-ui-1.11.2/jquery-ui.js")



@Html.Raw(Model.Lyrics)

<script type="text/javascript">
    $(document).ready(function () {

    });
</script>

