<%@ Page Language="VB" AutoEventWireup="false" Inherits="Songs.Users_GestionUsers" Codebehind="GestionUsers.aspx.vb" %>

<%@ Import Namespace="Songs.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Gestion des utilisateurs</title>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        function UpdateUser(user_id)
        {
            window.open("UpdateUser.aspx?USER_ID=" + user_id, null, "width=350, height=350, status=no,toolbar=no,menubar=no,location=no").focus();
        }
    
        function DeleteUser(user_id)
        {
            if (confirm("Êtes vous sertain de vouloir supprimer cet utilisateur?\n\nIl ne sera plus accessible et seulement le gestionnnaire de base de données pourra le réactiver."))
            {
                form1.txtUserToDelete.value = user_id;
                form1.btnDelete.click();
            }
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td><h1 style="color:Navy; font-style:italic;">Gestion des utilisateurs</h1></td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    <%If Session("USER_LEVEL") <= UserLevel.Admin Then%>
                        <input type="button" class="button" id="btnAdd" name="btnAdd" value="Ajouter un utilisateur" onclick="UpdateUser(0);"/>
                    <%End If%>
                    
                   <div style="height:350px; width: 480px; overflow-y: auto">
                        <asp:Repeater ID="repUsers" runat="server">
                            <HeaderTemplate>
                                <table style="border: solid 1px navy; width: 100%;">
                                    <tr class="Entete">
                                        <td>Code</td>
                                        <td>Nom</td>
                                        <td>Prénom</td>
                                        <td>Niveau</td>
                                        <td>Mod.</td>
                                        <%If CInt(Session("USER_LEVEL")) <= UserLevel.Admin Then%>
                                        <td>Supp.</td>
                                        <%End If%>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                    <tr class="GridItem">
                                        <td><%#CType(Container.DataItem, User).Code%></td>
                                        <td><%#CType(Container.DataItem, User).Name%></td>
                                        <td><%#CType(Container.DataItem, User).FirstName%></td>
                                        <td><%#GetUserLevel(CType(Container.DataItem, User).Level)%></td>
                                        <td align="center"><%#GetUserModif(CType(Container.DataItem, User).Id, CType(Container.DataItem, User).Level)%></td>
                                        <%If CInt(Session("USER_LEVEL")) <= UserLevel.Admin Then%>
                                            <td align="center"><%#GetUserDelete(CType(Container.DataItem, User).Id, CType(Container.DataItem, User).Level)%></td>
                                        <%End If%>
                                    </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                    <tr class="AltGridItem">
                                        <td><%#CType(Container.DataItem, User).Code%></td>
                                        <td><%#CType(Container.DataItem, User).Name%></td>
                                        <td><%#CType(Container.DataItem, User).FirstName%></td>
                                        <td><%#GetUserLevel(CType(Container.DataItem, User).Level)%></td>
                                        <td align="center"><%#GetUserModif(CType(Container.DataItem, User).Id, CType(Container.DataItem, User).Level)%></td>
                                        <%If CInt(Session("USER_LEVEL")) <= UserLevel.Admin Then%>
                                            <td align="center"><%#GetUserDelete(CType(Container.DataItem, User).Id, CType(Container.DataItem, User).Level)%></td>
                                        <%End If%>
                                   </tr>
                             </AlternatingItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>   
                    <div style="display: none">
                        <input type="text" id="txtUserToDelete" name="txtUserToDelete" />
                        <asp:Button id="btnDelete" runat="server"/>
                    </div>
               </td>
            </tr>
        </table>
    </form>
</body>
</html>
