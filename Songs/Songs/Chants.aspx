<%@ Page Language="VB" AutoEventWireup="false" Inherits="Songs.Chants"  validateRequest="false" Codebehind="Chants.aspx.vb" %>

<%@ Import Namespace="Songs.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Louanges</title>
    <link rel="SHORTCUT ICON" href="../favicon.ico" />
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../StyleChordPro.css" rel="stylesheet" />
    <script type="text/javascript" src="../Songs/Chant4.js"></script>
    <style type="text/css">
        html,body, form {
            height: 100%;
        }
    </style>
</head>

<body id="theBody" onload="Onload();" style="margin: 0;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
        <table id="tblMain" style="width: 100%; height: 100%;">
          <tr>
            <td colspan="2" style="height: 50px;">
                <table>
                    <tr>
                        <td>
                            <a href="http://www.carrefourcc.org/">
                                <img alt="" src="../Images/logo_ccc_100.png" style="border: none"/>
                            </a>
                        </td>
                        <td style="padding-left: 50px;">
                            <h1 style="color:Navy; font-style:italic;">
                                Chants intégrés
                            </h1>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="bottom" style="width:50%; height: 80px;">
                <fieldset>
                    <legend style="color:navy">Critères de recherche</legend>
                    <table  border="0">
                      <tr id="testTR">
                        <td style="width: 80px">
                          <asp:RadioButtonList ID="rbType" runat="server">
                            <asp:ListItem>Code</asp:ListItem>
                            <asp:ListItem Selected="True">Titre</asp:ListItem>
                            <asp:ListItem>Contenu</asp:ListItem>
                          </asp:RadioButtonList>
                        </td>
                        <td>&nbsp;&nbsp;&nbsp;</td>
                        <td>
                            <asp:UpdatePanel ID="upRecherche" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    Filtrer par thème<br />
                                    <asp:DropDownList ID="cboTheme" runat="server" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                                    <br />
                                    Texte à rechercher<br />
                                    <asp:TextBox ID="txtRecherche" runat="server" Width="243px"></asp:TextBox>
                                    <asp:Button ID="btnOK" CssClass="button" runat="server" Text="OK" /><br />
                                    <asp:Label ID="lblSearchResult" runat="server" Text=""></asp:Label></td>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                      </tr>
                    </table>
                </fieldset>
            </td>
            <td valign="bottom" style="width:50%">
                <table width="100%">
                    <tr>
                        <td>
                            <table width="100%"> 
                                <tr>
                                    <td valign="top">
                                        <input class="button" style="width:150px;" id="btnTop10Perso" type="button" value="Mon top 10" onclick="ShowTop10('<%=Session("USER_ID")%>')"/><br />
                                        <input class="button" style="width:150px;" id="btnTop10Gen" type="button" value="Top 10 général" onclick="ShowTop10('')"/>
                                    </td>
                                    <td align="right" valign="top">
                                        <input id="btnUsers" type="button" class="button" style="width:175px" value="Gestion des utilisateurs" onclick="GestionUtilisateurs();" /><br />
                                        <%If Session("USER_LEVEL") < UserLevel.User Then%>
                                        &nbsp;<input id="btnCats" type="button" class="button" style="width:175px" value="Gestion des thèmes" onclick="GestionThemes();" /><br />
                                            <input id="btnAddSong" type="button" class="button" style="width:175px" value="Ajouter un chant" onclick="UpdateSong(0);" />
                                        <%End If%>
                                    </td>
                                </tr>
                            </table>
                       </td>
                    </tr>
                </table>
                <table border="0" style="position:relative; top: 7px;">
                    <tr>
                        <td id="tdParoles" class="TabSelected" onclick="SelParoles();" >&nbsp;Paroles&nbsp;</td>
                        <td id="tdAccords" class="TabUnSelected" onclick="SelAccords();" >&nbsp;Accords&nbsp;</td>
                        <td id="tdSel" class="TabUnSelected" onclick="SelListe();">
                            <asp:UpdatePanel ID="upDateSel" runat="server" UpdateMode="always">
                                <ContentTemplate>
                                    &nbsp;Sélection&nbsp;
                                    <asp:TextBox ID="txtDateSel" ReadOnly="true" runat="server" BackColor="Transparent" BorderStyle="None" Font-Bold="True" ForeColor="White" Width="80px"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
          </tr>
          <tr> 
            <td valign="top" class="Encadre">
                <asp:UpdatePanel ID="upResultat" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                      <div id="divResultat" style="border: 4px solid blue">
                          <asp:Repeater ID="repRecherche" runat="server">
                            <HeaderTemplate>
                                <table border="1">
                                    <tr class="Entete">
                                        <td style="width: 20%">Code</td>
                                        <td style="width: 70%">Titre</td>
                                        <td style="width: 10%">Ton.</td>
                                        <td>&nbsp;&nbsp;&nbsp;</td>
                                        <td>&nbsp;&nbsp;&nbsp;</td>
                                        <td>&nbsp;&nbsp;&nbsp;</td>
                                        <td>&nbsp;&nbsp;&nbsp;</td>
                                        <%If Session("USER_LEVEL") < 0 Then%>
                                        <td>&nbsp;&nbsp;&nbsp;</td>
                                        <%End If%>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                    <tr id="TR<%=i%>" onclick="OnSelectTR(TR<%=i%>);" class="GridItem" style="cursor: pointer">
                                        <td style="display: none"><%#CType(Container.DataItem, UserSong).Id%></td>
                                        <td><%#CType(Container.DataItem, UserSong).Code%></td>
                                        <td><%#CType(Container.DataItem, UserSong).Title%></td>
                                        <td><%#CType(Container.DataItem, UserSong).Tone%></td>
                                        <td onclick="UpdateSong(<%#CType(Container.DataItem, UserSong).Id%>);">
                                            <img alt="Modifier" src="../Images/pictosBoutons/modifier.gif" /></td>
                                        <td onclick="AddToListe('TR<%=i%>', 1)">
                                            <img alt="Ajouter à la liste de chants avant la réunion" src="../Images/pictosBoutons/QUITTER.GIF" /></td>
                                        <td onclick="AddToListe('TR<%=i%>', 2)">
                                            <img alt="Ajouter à la liste de chants pendant la réunion" src="../Images/pictosBoutons/QUITTER.GIF" /></td>
                                        <td onclick="AddToListe('TR<%=i%>', 3)">
                                            <img alt="Ajouter à la liste de chants pour l'offrande, communion et appel" src="../Images/pictosBoutons/QUITTER.GIF" /></td>
                                        <%If Session("USER_LEVEL") < 0 Then%>
                                        <td onclick="PreDeleteSong(<%#CType(Container.DataItem, UserSong).Id%>)">
                                            <img alt="Supprimer ce chant" src="../Images/pictosBoutons/fermer.GIF" /></td>
                                        <%End If%>
                                        <%i = i + 1%>
                                    </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        <div style="display:none">
                          <asp:Button ID="btnDelSong" runat="server" Text="Button" />
                          <asp:TextBox ID="txtDelSong" runat="server"></asp:TextBox>
                        </div>
                      </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </td> 
            <td valign="top" class="Encadre">
              <table cellspacing="0" cellpadding="0" width="100%">
                <tr>
                  <td valign="top" id="trSel" style="display: none; white-space: nowrap;">
                      <asp:UpdatePanel ID="upDates" runat="server" UpdateMode="Conditional">
                          <ContentTemplate>
                              <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                      <asp:DropDownList ID="cboDates" runat="server" DataTextField="STR_DATE" DataValueField="STR_DATE"></asp:DropDownList>
                                      <asp:Button ID="btnLoadList" runat="server" CssClass="button" Text="Charger liste" Width="125px" />
                                    </td>
                                    <td align="right">
                                      <input id="btnNewList" type="button" class="button" value="Nouvelle liste" onclick="NouvelleListe();" style="width: 125px" />
                                    </td>
                                </tr>
                              </table>
                              <div id="divSelection" runat="server" style="height:300px; overflow:auto; border: 1px solid teal"></div>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                      
                      <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="right">
                                <asp:UpdatePanel ID="upSaveList" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                  <asp:Button ID="btnSaveList" runat="server" CssClass="button" Text="Sauvegarder liste" ToolTip="Sauvegarder la liste des chants sélectionnés pour votre prochaine session." />
                                      &nbsp;
                                      <input id="btnOpenWord" type="button" class="button" value="Ouvrir Word" onclick='OpenWord("<%=Request.ServerVariables("HTTP_HOST")%>", "<%=Session("USER_FNAME") & " " & Session("USER_NAME") %>");' />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                      </table>
                  </td>
                  <td valign="top" id="trParoles" align="center">
                      <asp:UpdatePanel ID="upParoles" runat="server" UpdateMode="Conditional">
                          <ContentTemplate>
                            <div id="divParoles" style="height:100px; overflow:auto;">
                                <div style="display: none;">
                                    <asp:Button ID="btnUpdateSong" runat="server" />
                                    <asp:TextBox ID="txtSondId" runat="server" />
                                </div>
                                <asp:Label ID="lblParoles" runat="server" Text="" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            </div>   
                          </ContentTemplate>
                      </asp:UpdatePanel>
                  </td>
                  <td valign="top" id="trAccords" style="display: none;">
                      <asp:UpdatePanel ID="upAccords" runat="server" UpdateMode="Conditional">
                          <ContentTemplate>
                            <asp:TextBox ID="txtChords" runat="server" TextMode="MultiLine" Visible="false"></asp:TextBox>
                            <table>
                                <tr>
                                    <td><asp:Button ID="btnBemol" runat="server" Text="b" CssClass="button" ToolTip="Déscendre d'un demi-ton" /></td>
                                    <td><asp:Button ID="btnShift" runat="server" Text="#" CssClass="button" ToolTip="Monter d'un demi-ton" /></td>
                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                    <td>Affichage: </td>
                                    <td><asp:RadioButton ID="rbBemol" GroupName="rbFlat" runat="server" Text="b" Checked="true" AutoPostBack="true" ToolTip="Afficher en bémol" /></td>
                                    <td><asp:RadioButton ID="rbSharp" GroupName="rbFlat" runat="server" Text="#" AutoPostBack="true" ToolTip="Afficher en dièse" /></td>
                                </tr>
                            </table>
                            <hr />
                            <div id="divAccords" runat="server" style="height:100px; overflow:auto;"></div>   
                          </ContentTemplate>
                      </asp:UpdatePanel>
                  </td>

                </tr>
              </table>
            </td>
          </tr>
        </table>
        <div style="display:none">
            <asp:Button ID="btnWord" runat="server" Text="Ouvrir Word" CssClass="button" />
            <asp:TextBox ID="txtSelectedTab" runat="server" />
            <asp:TextBox ID="txtListCache" runat="server" />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
         </div>
    </form>
</body>
</html>
