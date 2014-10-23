<%@ Page Language="VB" AutoEventWireup="false" Inherits="Songs.Songs_Top10" Codebehind="Top10.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
    <title>Top 10</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="X-Large"
            ForeColor="Navy" Text="Top 10"></asp:Label></div>
    <div id="divSelection" runat="server" style="border: 1px solid teal">
    </div>
    </form>
</body>
</html>
