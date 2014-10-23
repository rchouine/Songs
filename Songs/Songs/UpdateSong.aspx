<%@ Page Language="VB" AutoEventWireup="false" Inherits="Songs.Songs_UpdateSong" Codebehind="UpdateSong.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Gestion des chants</title>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript">

    function ViewChords() {
        win = window.top.open("ChordPro.aspx?SONG_ID=<%=Request("SONG_ID")%>&time=<%=Now.Ticks.ToString%>", "", "height=500,width=600,status=no,toolbar=no,menubar=no,location=no");
    }

</script>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="X-Large"
            ForeColor="Navy" Text="Modification d'un Chant"></asp:Label><br />
        <br />
        <table>
            <tr>
                <td style="text-align: left; vertical-align: top;">
                    <asp:Label ID="lblSongTitle" runat="server" Font-Bold="True" Text="Titre:"></asp:Label></td>
                <td nowrap="noWrap" style="text-align: left; vertical-align: top;">
                    <asp:TextBox ID="txtTitle" runat="server" MaxLength="255" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitle"
                        Display="Dynamic" ErrorMessage="* Le titre est obligatoire" Font-Bold="True" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                <td style="width: 12px"></td>
                <td style="text-align: left; vertical-align: top;">
                    <asp:Label ID="lblCode" runat="server" Font-Bold="True" Text="Code:"></asp:Label></td>
                <td style="text-align: left; vertical-align: top; white-space: nowrap">
                    <asp:TextBox ID="txtCode" runat="server" MaxLength="16" Width="150px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCode"
                        Display="Dynamic" ErrorMessage="* Le code est obligatoire" Font-Bold="True" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top;" rowspan="5">
                    <asp:Label ID="lblLyrics" runat="server" Font-Bold="True" Text="Paroles:"></asp:Label>
                </td>
                <td rowspan="5" style="text-align: left; vertical-align: top;">
                    <asp:TextBox ID="txtLyrics" runat="server" Height="208px" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                <td align="left" rowspan="10" style="width: 3px"></td>
                <td style="text-align: left; vertical-align: top;">
                    <asp:Label ID="lblAuthor" runat="server" Font-Bold="True" Text="Auteur:"></asp:Label></td>
                <td style="text-align: left; vertical-align: top;">
                    <asp:TextBox ID="txtAuthor" runat="server" MaxLength="50" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top;">
                    <asp:Label ID="lblTrans" runat="server" Font-Bold="True" Text="Traducteur:"></asp:Label>
                </td>
                <td style="text-align: left; vertical-align: top;">
                    <asp:TextBox ID="txtTrans" runat="server" MaxLength="50" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top;">
                    <asp:Label ID="lblTone" runat="server" Font-Bold="True" Text="Tonalité:"></asp:Label>

                </td>
                <td style="text-align: left; vertical-align: top;">
                    <asp:TextBox ID="txtTone" runat="server" MaxLength="8" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top;"><b>Thèmes:</b></td>
                <td style="text-align: left; vertical-align: top;">
                    <div style="height: 128px; overflow-y: auto">
                        <asp:CheckBoxList ID="chkCategoryList" runat="server" DataTextField="Name" DataValueField="Id"></asp:CheckBoxList>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top;">
                    <asp:Label ID="lblAccords" runat="server" Font-Bold="True">Accords<br />ChordPro:</asp:Label>
                    <br />
                    <input id="btnViewChords" class="button" style="display: none" onclick="ViewChords();" type="button" value="Afficher" />
                </td>
                <td style="text-align: left; vertical-align: top;">
                    <asp:TextBox ID="txtChords" runat="server" Height="108px" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                <td rowspan="1" style="vertical-align: bottom; text-align: left;">
                    <input id="btnCancel" class="button" onclick="window.top.close();" type="button" value="Annuler" /></td>
                <td rowspan="1" style="vertical-align: bottom; text-align: left;">
                    &nbsp;
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Enregistrer" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnScript" runat="server" CssClass="button" Text="Ne pas toucher" Visible="false" />
                </td>
            </tr>

        </table>
    </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>
