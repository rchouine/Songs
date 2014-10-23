<%@ Page Language="VB" AutoEventWireup="false" Inherits="Songs.Songs_DatePickerx" Codebehind="DatePicker.aspx.vb" %>

<!DOCTYPE>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
    <title>Date</title>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css" />
    <style>
        body {
            text-align: center;
        }

        #datepicker {
            font-size: 62.5%;
            width: 200px;
            margin: auto;
        }
    </style>
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.1/jquery-ui.js"></script>
    <script type="text/javascript" src="../Lib/datepicker-fr.js"></script>
    <script type="text/javascript" >
      $(function () {
          $("#datepicker").datepicker({
              changeMonth: true,
              changeYear: true,
              showOtherMonths: true,
              selectOtherMonths: true,
              showButtonPanel: false,
              onSelect: function () {
                  var dateObject = $(this).datepicker('getDate');
                  alert(dateObject);
              }
          }).datepicker.regional["fr"];
      });
  </script>

    
</head>

<body style="margin: 10px 10px 10px 10px;">
    <form id="form1" runat="server">
        <b>Veuillez sélectionner une date</b>
        <div id="datepicker"></div>
        <input id="btnToday" type="button" value="Aujourd'hui" />
    </form>
</body>
</html>

<script type="text/javascript">
    $("#btnToday").click(function () {
        $("#datepicker").datepicker("setDate", new Date());
        var dateObject = $("#datepicker").datepicker('getDate');
        alert(dateObject);
    });

</script>
