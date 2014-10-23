<%@ Page Language="VB" AutoEventWireup="false" Inherits="Songs.Ref_UpdateTheme" Codebehind="UpdateTheme.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="X-Large"
            ForeColor="Navy" Text="Modification d'un thème"></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="txtCatName" runat="server" Width="250px"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCatName"
            Display="Dynamic" ErrorMessage="* Champ obligatoire" Font-Bold="True" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
        <table>
            <tr>
                <td>
                    <input id="btnCancel" class="button" onclick="window.top.close();" type="button"
                        value="Annuler" />
                </td>
                <td>&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                </td>
                <td align="right" style="height: 25px">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Enregistrer" />
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
