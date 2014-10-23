<%@ Page Language="VB" AutoEventWireup="false" Inherits="Songs.Login_NewPwd" Codebehind="NewPwd.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Nouveau mot de passe</title>
    <link rel="SHORTCUT ICON" href="../favicon.ico"/>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function txtPassword_onkeydown()
        {
            if (window.event.keyCode == 13)
                form1.btnOK.click();
        }
        
        function window_resize()
        {
            tblMain.style.height = document.documentElement.clientHeight - 100;
        }
        
        function window_onload()
        {
            window_resize();
            window.onresize = window_resize;
        }
        
    </script>
</head>
<body onload="window_onload();">
    <form id="form1" runat="server">
        <table id="tblMain" style="width: 100%">
            <tr style="height: 10%">
                <td>
                    <table>
                        <tr>
                            <td valign="top">
                                <a href="http://www.carrefourcc.org/">
                                    <img alt="" src="../Images/Logo100.gif" style="border: none"/>
                                </a>
                            </td>
                            <td valign="bottom"><h1 style="color:Navy; font-style:italic;">
                                Chants intégrés du Carrefour Chrétien de la Capitale</h1>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 90%">
                <td align="center" valign="middle">
                    <h3 style="color:Navy;">
                    <asp:Label ID="lblSalut" runat="server"></asp:Label>
                    <asp:Label ID="lblName" runat="server"></asp:Label>,
                    <br />
                    votre mot de passe est expiré, 
                    <br />
                    veuillez en saisir un nouveau
                    </h3>
                    <table style="border: solid 1px navy">
                        <tr>
                            <td colspan="2" align="center" class="Entete"  style="font-size: 14pt">
                                Nouveau mot de passe
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td align="left" style="color: navy; font-weight: bold;">Mot de passe:</td>
                            <td><asp:TextBox ID="txtPassword1" runat="server" TextMode="Password" Width="150px" MaxLength="16"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="height: 10px">
                            </td>
                            <td style="height: 10px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword1"
                                    Display="Dynamic" ErrorMessage="* Champ obligatoire" Font-Bold="True" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="left" style="color: navy; font-weight: bold;">Confirmez:</td>
                            <td><asp:TextBox ID="txtPassword2" runat="server" TextMode="Password" Width="150px" MaxLength="16"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-weight: bold; color: navy">
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword2"
                                    Display="Dynamic" ErrorMessage="* Champ obligatoire" Font-Bold="True" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="font-weight: bold; color: navy">
                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <hr style="color: navy" />
                                <asp:Button ID="btnOK" CssClass="button" runat="server" Text="Poursuivre" />
                           </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
