<%@ Page Language="VB" AutoEventWireup="false" Inherits="Songs.Songs_ChordPro" Codebehind="ChordPro.aspx.vb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ChordPro song</title>
    <link href="../StyleChordPro.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox ID="txtData" runat="server" TextMode="MultiLine" Visible="false"></asp:TextBox>
        <table>
            <tr>
                <td><asp:Button ID="btnBemol" runat="server" Text="b" /></td>
                <td><asp:Button ID="btnShift" runat="server" Text="#" /></td>
                <td>&nbsp;&nbsp;&nbsp;</td>
                <td>Affichage: </td>
                <td><asp:RadioButton ID="rbBemol" GroupName="rbFlat" runat="server" Text="b" Checked="true" AutoPostBack="true" /></td>
                <td><asp:RadioButton ID="rbSharp" GroupName="rbFlat" runat="server" Text="#" AutoPostBack="true" /></td>
            </tr>
        </table>

       <hr />
        <div runat="server" id="divMain"></div>
    </form>
</body>
</html>
