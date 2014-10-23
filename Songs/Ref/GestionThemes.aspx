<%@ Page Language="VB" AutoEventWireup="false" Inherits="Songs.Ref_GestionThemes" Codebehind="GestionThemes.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Gestion des thèmes</title>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />

    
    <script type="text/javascript">
        function UpdateTheme(cat_id)
        {
            window.open("UpdateTheme.aspx?CAT_ID=" + cat_id, null, "height=230,width=290,status=no,toolbar=no,menubar=no,location=no").focus();
        }
    
    </script>
</head>
<body style="height: 100%">
    <form id="form1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td><h1 style="color:Navy; font-style:italic;">Gestion des thèmes</h1></td>
            </tr>
            <tr>
                <td align="left" valign="top">
                   <input type="button" class="button" id="btnAdd" name="btnAdd" value="Ajouter un thème" onclick="UpdateTheme(0);"/>
                    
                   <div style="height:150px; overflow-y: auto">
                        <asp:Repeater ID="repCat" runat="server">
                            <HeaderTemplate>
                                <table style="border: solid 1px navy" width="100%">
                                    <tr class="Entete">
                                        <td style="width: 90%">Nom</td>
                                        <td align="center" style="width: 10%">Modifier</td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                    <tr class="GridItem">
                                        <td><%#DataBinder.Eval(Container.DataItem, "Name")%></td>
                                        <td align="center" title="Modifier" style="cursor:pointer" onclick="UpdateTheme(<%#DataBinder.Eval(Container.DataItem, "Id")%>);">
                                            <img src="../Images/pictosBoutons/modifier.gif" alt="" /></td>
                                    </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                    <tr class="AltGridItem">
                                        <td><%#DataBinder.Eval(Container.DataItem, "Name")%></td>
                                        <td align="center" title="Modifier" style="cursor:pointer" onclick="UpdateTheme(<%#DataBinder.Eval(Container.DataItem, "Id")%>);">
                                            <img src="../Images/pictosBoutons/modifier.gif" alt=""  /></td>
                                    </tr>
                             </AlternatingItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
               </td>
            </tr>
        </table>
    </form>
</body>
</html>
