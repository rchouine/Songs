Imports Songs.Controller
Imports Songs.Model

Public Class ChantsController
    Inherits System.Web.Mvc.Controller

    Private Function GetCategoryList(addTous As Boolean) As List(Of Category)
        Dim ctrl As New CategoryController
        Dim retour = ctrl.GetList
        If addTous Then
            retour.Insert(0, New Category With {.Id = 0, .Name = "[Tous]"})
        End If
        Return retour
    End Function

    Function Index() As ActionResult
        If String.IsNullOrEmpty(Session("USER_ID")) Then
            Return RedirectToAction("Enregistrement", "Utilisateurs")
        Else
            Dim songCtrl As New SongController
            Dim liste = songCtrl.GetList(1, Model.SearchType.Title, "", 1)

            Dim vm As New ChantsViewModel
            vm.Chants = liste
            vm.TabIndex = 0
            vm.Categories = GetCategoryList(True)
            Return View("Recherche", vm)
        End If
    End Function

    Function Rechercher(typeRecherche As String, texteRecherche As String, categorie As Integer, TabIndex As Integer) As ActionResult
        Dim leType As Model.SearchType
        Select Case typeRecherche
            Case "code" : leType = Model.SearchType.Code
            Case "paroles" : leType = Model.SearchType.Lyrics
            Case Else : leType = Model.SearchType.Title
        End Select

        Dim songCtrl As New SongController
        Dim liste = songCtrl.GetList(Session("USER_ID"), leType, texteRecherche, categorie)

        Dim vm As New ChantsViewModel
        vm.Chants = liste
        vm.TabIndex = TabIndex
        vm.Categories = GetCategoryList(True)
        Return View("Recherche", vm)
    End Function

    Function Chant(id As Integer, shift As Integer, sharp As Integer) As JsonResult
        Dim songCtrl As New SongController
        Dim chantTrouve = songCtrl.GetById(id)

        Dim ctrl As New ChordProManager

        Dim retour(4) As String
        retour(0) = id
        retour(1) = (shift + 12) Mod 12
        retour(2) = chantTrouve.Title
        retour(3) = FormatterParoles(chantTrouve.Lyrics)
        retour(4) = ctrl.GetChordsHtml(ctrl.Shift(chantTrouve.ChordPro, shift, sharp = 1))
        Return Json(retour, JsonRequestBehavior.AllowGet)
    End Function

    Function ChordPro(id As Integer, shift As Integer, sharp As Integer) As ActionResult
        Dim songCtrl As New SongController
        Dim chantTrouve = songCtrl.GetById(id)

        Dim ctrl As New ChordProManager
        Dim cp As New ChordProModel

        cp.Id = id
        cp.Text = chantTrouve.ChordPro
        cp.Html = ctrl.GetChordsHtml(ctrl.Shift(chantTrouve.ChordPro, shift, sharp = 1))
        Return View(cp)
    End Function

    Public Function FormatterParoles(ByVal text As Object) As String
        If Not text Is DBNull.Value Then
            'Grossir le titre
            Dim titre As String
            Dim reste As String
            Dim resultat As String
            titre = Left(text, InStr(text, vbCrLf) - 1)
            reste = Mid(text, titre.Length + 3)

            ''Highlighter le texte recherché
            'If rbType.SelectedIndex = 2 And Trim(txtRecherche.Text) <> "" Then
            '    Dim position = InStr(reste, txtRecherche.Text, CompareMethod.Text)
            '    If position > 0 Then
            '        reste = Left(reste, position - 1) & "<font style='background-color:Yellow'>" & _
            '                Mid(reste, position, Len(txtRecherche.Text)) & "</font>" & _
            '                Mid(reste, position + Len(txtRecherche.Text))
            '    End If
            'End If

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
End Class