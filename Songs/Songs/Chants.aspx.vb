Imports System.Data.SqlClient
Imports Songs.Modules
Imports Songs.Controller
Imports Songs.Model

Partial Class Chants
    Inherits Page

    Protected i As Integer = 0

    Public Function JavaStr(ByVal text As Object) As String
        Return Replace(text, "'", "\'")
    End Function

    Public Function FormatterParoles(ByVal text As Object) As String
        If Not text Is DBNull.Value Then
            'Grossir le titre
            Dim titre As String
            Dim reste As String
            Dim resultat As String
            Dim position As Long
            titre = Left(text, InStr(text, vbCrLf) - 1)
            reste = Mid(text, titre.Length + 3)

            'Highlighter le texte recherché
            If rbType.SelectedIndex = 2 And Trim(txtRecherche.Text) <> "" Then
                position = InStr(reste, txtRecherche.Text, CompareMethod.Text)
                If position > 0 Then
                    reste = Left(reste, position - 1) & "<font style='background-color:Yellow'>" & _
                            Mid(reste, position, Len(txtRecherche.Text)) & "</font>" & _
                            Mid(reste, position + Len(txtRecherche.Text))
                End If
            End If

            resultat = "<h2>" & titre & "</h2>" & reste
            resultat = Replace(resultat, vbCrLf, "<br>")
            resultat = Replace(resultat, vbCr, "")
            resultat = Replace(resultat, vbLf, "")
            resultat = Replace(resultat, """", "\""")
            Return resultat
        Else
            Return ""
        End If
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If CStr(Session("USER_ID")) = "" Then
            Response.Redirect("../Login/Login.aspx")
        End If

        txtDateSel.Text = Request("txtDateSel")

        If Not Page.IsPostBack Then
            'Themes
            Dim catCtrl As New CategoryController
            Dim liste = catCtrl.GetList

            cboTheme.DataSource = liste
            cboTheme.DataBind()

            Dim objTous As New ListItem
            objTous.Value = 0
            objTous.Text = "[Tous]"
            cboTheme.Items.Insert(0, objTous)

            GetUserDates()

            'Initialiser le tableau des listes
            Dim strTblSel As String = String.Empty
            strTblSel += GetTblSelectionHeader(1) & "</table>"
            strTblSel += GetTblSelectionHeader(2) & "</table>"
            strTblSel += GetTblSelectionHeader(3) & "</table>"
            divSelection.InnerHtml = strTblSel
        End If

        lblMessage.Text = ""
        txtRecherche.Focus()
    End Sub

    Private Sub GetUserDates()

        Dim selDate = cboDates.SelectedValue

        Dim aSql As New SqlDataSource
        aSql.ConnectionString = GetConnectionString()

        aSql.SelectCommand = "execute sSelectionDates " & CStr(Session("USER_ID"))
        aSql.DataBind()
        cboDates.DataSource = aSql
        'cboDates.DataBind()

        aSql.Dispose()

        Dim objVide As New ListItem
        objVide.Value = ""
        objVide.Text = ""
        cboDates.Items.Insert(0, objVide)

        If selDate <> String.Empty Then
            cboDates.SelectedValue = selDate
        End If
        upDates.Update()
    End Sub
    Private Function GetTblSelectionHeader(ByVal section As Integer) As String
        Dim strTbl As String
        Select Case section
            Case 1
                strTbl = "Avant la réunion"
            Case 2
                strTbl = "<br>Pendant la réunion"
            Case Else
                strTbl = "<br>Offrande, commmunion, appel"
        End Select


        strTbl += "<table id=""tblSelection" & CStr(section) & """>"
        'strTbl += "   <tr style=""font-weight:bold; color:White; background-color:#5D7B9D"">"
        strTbl += "   <tr class='Entete'>"
        strTbl += "      <td style=""display:none""></td>"
        strTbl += "      <td style=""width: 20%"">Code</td>"
        strTbl += "      <td style=""width: 70%"">Titre</td>"
        strTbl += "      <td style=""width: 10%"">Ton.</td>"
        strTbl += "      <td style=""width: 10px"">&nbsp;&nbsp;&nbsp;</td>"
        strTbl += "      <td style=""width: 10px"">&nbsp;&nbsp;&nbsp;</td>"
        strTbl += "      <td style=""width: 10px"">&nbsp;&nbsp;&nbsp;</td>"
        strTbl += "   </tr>"

        Return strTbl
    End Function
    Protected Sub GetSelection(ByVal strDate As String)
        Dim strSql As String
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        Dim dr As SqlDataReader
        Dim strTblSel As String = ""
        Dim iSection As Integer = 0
        Dim bSection1 As Boolean = False
        Dim bSection2 As Boolean = False
        Dim bSection3 As Boolean = False


        conn = New SqlConnection(GetConnectionString)
        strSql = "execute sSelection " & CStr(Session("USER_ID")) & ", '" & strDate & "'"
        cmd = New SqlCommand(strSql, conn)
        conn.Open()
        dr = cmd.ExecuteReader()
        While dr.Read()
            txtDateSel.Text = Format(dr.GetValue(0), "dd/MM/yyyy")

            If dr.GetValue(1) > iSection Then

                If strTblSel <> "" Then
                    strTblSel += "</table>"
                End If
                iSection = dr.GetValue(1)

                'Valider si on a les sections précédentes
                If iSection > 1 And Not bSection1 Then
                    strTblSel += GetTblSelectionHeader(1)
                    strTblSel += "</table>"
                    bSection1 = True
                End If

                If iSection > 2 And Not bSection2 Then
                    strTblSel += GetTblSelectionHeader(2)
                    strTblSel += "</table>"
                    bSection2 = True
                End If

                strTblSel += GetTblSelectionHeader(iSection)
                Select Case iSection
                    Case 1
                        bSection1 = True
                    Case 2
                        bSection2 = True
                    Case 3
                        bSection3 = True
                End Select
            End If
            strTblSel += "   <tr style=""cursor: hand; color:navy; background-color:White"" onclick=""OnSelectTR(this)"">"
            strTblSel += "      <td style='display: none'>" & dr.GetValue(3).ToString & "</td>"
            strTblSel += "      <td>" & dr.GetValue(4).ToString & "</td>"
            strTblSel += "      <td>" & dr.GetValue(5).ToString & "</td>"
            strTblSel += "      <td>" & dr.GetValue(6).ToString & "</td>"
            strTblSel += "      <td style=""display: none"">" & FormatterParoles(dr.GetValue(7).ToString) & "</td>"

            strTblSel += "      <td title=""Déplacer vers le haut"" onclick=""PrepareMoveUp();""><img src='../Images/FlecheHaut.gif' /></td>"
            strTblSel += "      <td title=""Déplacer vers le bas"" onclick=""PrepareMoveDown();""><img src='../Images/FlecheBas.gif' /></td>"
            strTblSel += "      <td title=""Retirer de la liste"" onclick=""RemoveFromListe();""><img src='../Images/pictosBoutons/fermer.gif' /></td>"
            strTblSel += "   </tr>"
        End While
        dr.Close()
        conn.Close()
        conn.Dispose()

        'Valider si on a toutes les sections
        If Not bSection1 Then
            strTblSel += GetTblSelectionHeader(1)
        End If
        If Not bSection2 Then
            strTblSel += "</table>"
            strTblSel += GetTblSelectionHeader(2)
        End If
        If Not bSection3 Then
            strTblSel += "</table>"
            strTblSel += GetTblSelectionHeader(3)
        End If

        strTblSel += "</table>"

        divSelection.InnerHtml = strTblSel

    End Sub

    Protected Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        If cboTheme.SelectedValue = 0 And Trim(txtRecherche.Text) = "" Then
            lblMessage.Text = "<script>alert('Veuillez sélectionner un thème et / ou\nentrer un text à rechercher.')</script>"
        Else
            Dim leType As SearchType
            Select Case rbType.SelectedIndex
                Case 0 : leType = SearchType.Code
                Case 1 : leType = SearchType.Title
                Case 2 : leType = SearchType.Lyrics
                Case Else : leType = SearchType.Title
            End Select

            Dim songCtrl As New SongController
            Dim liste = songCtrl.GetList(CInt(Session("USER_ID")), leType, txtRecherche.Text, cboTheme.SelectedValue)

            repRecherche.DataSource = liste
            repRecherche.DataBind()

            If liste.Count > 1 Then
                lblSearchResult.Text = CStr(liste.Count) & " chants trouvés"
            Else
                lblSearchResult.Text = CStr(liste.Count) & " chant trouvé"
            End If
            upResultat.Update()
        End If
    End Sub

    Protected Sub btnSaveList_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveList.Click
        If txtDateSel.Text <> "" Then
            Dim conn As SqlConnection
            Dim cmd As SqlCommand
            Dim aSection As Array
            Dim aSel As Array

            conn = New SqlConnection(GetConnectionString)
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            conn.Open()

            'Delete existing selections
            cmd.CommandText = "dSelection"
            cmd.Parameters.Clear()
            cmd.Parameters.Add(CreateParamInteger("@user_id", CStr(Session("USER_ID"))))
            cmd.Parameters.Add(CreateParamVarchar("@sel_date", txtDateSel.Text))
            cmd.ExecuteNonQuery()

            'Insert new selections
            aSection = txtListCache.Text.Split("§")
            aSel = aSection(0).Split("¦")
            SaveSection(cmd, aSel, 1)

            aSel = aSection(1).Split("¦")
            SaveSection(cmd, aSel, 2)

            aSel = aSection(2).Split("¦")
            SaveSection(cmd, aSel, 3)

            cmd.Dispose()
            conn.Close()
            conn.Dispose()

            GetUserDates()
        End If
    End Sub

    Private Sub SaveSection(ByVal cmd As SqlCommand, ByVal data As Array, ByVal section As Integer)
        Dim aSong As Array

        For compteur As Integer = 0 To UBound(data)
            aSong = data(compteur).ToString.Split("¤")
            If aSong.Length > 1 AndAlso Trim(aSong(0)) <> String.Empty Then
                cmd.CommandText = "iSelection"
                cmd.Parameters.Clear()

                cmd.Parameters.Add(CreateParamInteger("@user_id", CStr(Session("USER_ID"))))
                cmd.Parameters.Add(CreateParamVarchar("@sel_date", txtDateSel.Text))
                cmd.Parameters.Add(CreateParamInteger("@sel_section", section))
                cmd.Parameters.Add(CreateParamInteger("@sel_index", CStr(compteur)))
                cmd.Parameters.Add(CreateParamInteger("@song_id", aSong(0).ToString))

                cmd.ExecuteNonQuery()
            End If
        Next
    End Sub

    Protected Sub btnLoadList_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoadList.Click
        If cboDates.SelectedIndex > -1 Then
            GetSelection(cboDates.SelectedValue)
        End If
    End Sub

    Protected Sub btnUpdateSong_Click(sender As Object, e As EventArgs) Handles btnUpdateSong.Click
        If txtSondId.Text <> "" Then

            Dim songCtrl As New SongController
            Dim aSong = songCtrl.GetById(CInt(txtSondId.Text))

            If aSong IsNot Nothing Then
                lblParoles.Text = FormatterParoles(aSong.Lyrics)
                txtChords.Text = aSong.ChordPro
                Dim cu As New ChordUtils
                cu.ShowChords(cu.Shift(aSong.ChordPro, 0, rbSharp.Checked), divAccords)
            End If
        End If
        upAccords.Update()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "resize", "OnResize();", True)
    End Sub

    Protected Sub btnShift_Click(sender As Object, e As EventArgs) Handles btnShift.Click
        Dim cu As New ChordUtils
        txtChords.Text = cu.Shift(txtChords.Text, 1, rbSharp.Checked)
        cu.ShowChords(txtChords.Text, divAccords)
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "resize", "OnResize();", True)
    End Sub

    Protected Sub btnBemol_Click(sender As Object, e As EventArgs) Handles btnBemol.Click
        Dim cu As New ChordUtils
        txtChords.Text = cu.Shift(txtChords.Text, 12 - 1, rbSharp.Checked)
        cu.ShowChords(txtChords.Text, divAccords)
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "resize", "OnResize();", True)
    End Sub

    Protected Sub rbBemol_CheckedChanged(sender As Object, e As EventArgs) Handles rbBemol.CheckedChanged
        Dim cu As New ChordUtils
        cu.ShowChords(cu.Shift(txtChords.Text, 0, rbSharp.Checked), divAccords)
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "resize", "OnResize();", True)
    End Sub

    Protected Sub rbSharp_CheckedChanged(sender As Object, e As EventArgs) Handles rbSharp.CheckedChanged
        Dim cu As New ChordUtils
        cu.ShowChords(cu.Shift(txtChords.Text, 0, rbSharp.Checked), divAccords)
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "resize", "OnResize();", True)
    End Sub
End Class
