<%@ Page Language="VB" AutoEventWireup="false" Inherits="Songs.Login" Codebehind="Login.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Connexion</title>
    <link rel="SHORTCUT ICON" href="../favicon.ico" />
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        html,body, form {
            height: 100%;
        }
    </style>
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
<body onload="window_onload();" style="margin: 0;">
    <form id="form1" runat="server">
        <table id="tblMain" style="width: 100%; height: 95%;">
            <tr style="height: 10%">
                <td>
                    <a href="http://www.carrefourcc.org/">
                        <img alt="" src="../Images/logo_ccc_100.png" style="border: none"/>
                    </a>
                </td>
            </tr>
            <tr style="height: 20%">
                <td style="text-align: center; vertical-align: bottom;">
                    <h1 style="color:Navy; font-style:italic;">
                        Chants intégrés
                    </h1>
                </td>
            </tr>
            <tr style="height: 60%">
              <td style="text-align: center; vertical-align: top;">
                  <center>
                    <table style="border: solid 1px navy">
                        <tr>
                            <td colspan="2" align="center" class="Entete" style="font-size: 14pt">
                                Connexion
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td align="left" style="color: navy; font-weight: bold;">Nom d'utilisateur:</td>
                            <td><asp:TextBox ID="txtName" runat="server" Width="150px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                            <td style="height: 10px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Champ obligatoire" ControlToValidate="txtName" Display="Dynamic" Font-Bold="True" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="color: navy; font-weight: bold;">Mot de passe:</td>
                            <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                            <td style="height: 10px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Champ obligatoire" ControlToValidate="txtPassword" Display="Dynamic" Font-Bold="True" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="font-weight: bold; color: navy">
                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="nbTry" runat="server" Text="0" Visible="False"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <hr style="color: navy" />
                                <asp:Button ID="btnOK" CssClass="button" runat="server" Text="Poursuivre" />
                            </td>
                        </tr>
                    </table>
                  </center>
              </td>
            </tr>
        </table>
    </form>
</body>
</html>
