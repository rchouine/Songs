<%@ Page Language="VB" AutoEventWireup="false" Inherits="Songs.Menu" Codebehind="Menu.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ul class="glossymenu">
            <li><a href="#">Recherche</a></li>
            <li><a href="#">Utilisateurs</a></li>
            <li><a href="#">Catégories</a></li>
        </ul>
        
        <table cellspacing=0>
            <tr>
                <td class="glossybtn" onmouseover="this.className='glossybtnOver';" onmouseleave="this.className='glossybtn';">qwe</td>
            </tr>
            <tr>
                <td class="glossybtn" onmouseover="this.className='glossybtnOver';" onmouseleave="this.className='glossybtn';">qwe</td>
            </tr>
            <tr>
                <td class="glossybtn" onmouseover="this.className='glossybtnOver';" onmouseleave="this.className='glossybtn';">qwe</td>
            </tr>
            <tr>
                <td class="glossybtn" onmouseover="this.className='glossybtnOver';" onmouseleave="this.className='glossybtn';">qwe</td>
            </tr>
        </table>
    </div>
        <asp:Button ID="Button1" runat="server" Text="Button" CssClass="glossybtn" />
        <asp:Button ID="Button2" runat="server" Text="Button" CssClass="button" />
        <input id="Button6" type="button" class="glossybtn" value="button" />
        <input id="Button7" type="button" class="button" value="button" />
    </form>
</body>
</html>
