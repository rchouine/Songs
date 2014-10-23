<%@ Page Language="VB" AutoEventWireup="false" Inherits="Songs.Users_UpdateUser" Codebehind="UpdateUser.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Gestion des utilisateurs</title>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <center>
    <form id="form1" runat="server">
        <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="16pt"
            ForeColor="Navy" Text="Modification d'un utilisateur"></asp:Label>
        <br />
        <br />
    <table>
        <tr>
            <td align="left"><b>Code: </b></td>
            <td><asp:TextBox ID="txtCode" runat="server" Width="150px" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCode"
                    Display="Dynamic" ErrorMessage="* Le code est obligatoire" Font-Bold="True" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td align="left"><b>Nom: </b></td>
            <td><asp:TextBox ID="txtName" runat="server" Width="150px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName"
                    Display="Dynamic" ErrorMessage="* Le nom est obligatoire" Font-Bold="True" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td align="left"><b>Prénom: </b></td>
            <td><asp:TextBox ID="txtFName" runat="server" Width="150px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFName"
                    Display="Dynamic" ErrorMessage="* Le prénom est obligatoire" Font-Bold="True" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td align="left"><b>Niveau: </b></td>
            <td>
                <asp:DropDownList ID="cboLevel" runat="server" Width="155px">
                    <asp:ListItem Value="0">Gestionnaire</asp:ListItem>
                    <asp:ListItem Value="10">Administrateur</asp:ListItem>
                    <asp:ListItem Value="20">Ajout / Modification</asp:ListItem>
                    <asp:ListItem Value="30">Utilisateur de base</asp:ListItem>
                    <asp:ListItem Value="100">Désactivé</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" class="button" id="btnCancel" value="Annuler" onclick="window.top.close();"/>
            </td>
            <td align="right" style="height: 25px">
                <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Enregistrer" />
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnResetPassword" CssClass="button" runat="server" Text="Réinitialiser le mot de passe" width="100%"/>
            </td>
        </tr>
    </table>
    </form>
    </center>
</body>
</html>
