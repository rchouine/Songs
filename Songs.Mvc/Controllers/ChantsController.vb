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
        Return Rechercher("titre", "", 1, 0)
    End Function

    Function Rechercher(typeRecherche As String, texteRecherche As String, categorie As Integer, TabIndex As Integer) As ActionResult
        If String.IsNullOrEmpty(Session("USER_ID")) Then
            Return RedirectToAction("Enregistrement", "Utilisateurs")
        Else
            Dim leType As Model.SearchType
            Select Case typeRecherche
                Case "code" : leType = Songs.Model.SearchType.Code
                Case "paroles" : leType = Songs.Model.SearchType.Lyrics
                Case Else : leType = Songs.Model.SearchType.Title
            End Select

            Dim songCtrl As New SongController
            Dim liste = songCtrl.GetList(Session("USER_ID"), leType, texteRecherche, categorie)

            Dim model As New ChantsViewModel
            model.Chants = liste
            model.TabIndex = TabIndex
            model.Categories = GetCategoryList(True)
            Return View("Recherche", model)
        End If
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

    Function Modifier(id As Integer) As ActionResult
        Dim model As ChantViewModel
        If id > 0 Then
            Dim songCtrl As New SongController
            Dim chantTrouve = songCtrl.GetById(id)

            model = ConvertChantToModel(chantTrouve)
        Else
            model = New ChantViewModel
        End If

        Return View("Chant", model)
    End Function

    Function Enregistrer(model As ChantViewModel) As JsonResult
        Return Json(True, JsonRequestBehavior.AllowGet)
    End Function

    Function ConvertChantToModel(aSong As Song) As ChantViewModel
        Dim newSong As New ChantViewModel
        newSong.Id = aSong.Id
        newSong.Code = aSong.Code
        newSong.Title = aSong.Title
        newSong.Author = aSong.Author
        newSong.Translator = aSong.Translator
        newSong.Lyrics = aSong.Lyrics
        newSong.ChordPro = aSong.ChordPro
        Return newSong
    End Function
    Function ConvertModelToChant(aSong As ChantViewModel) As Song
        Dim newSong As New Song
        newSong.Id = aSong.Id
        newSong.Code = aSong.Code
        newSong.Title = aSong.Title
        newSong.Author = aSong.Author
        newSong.Translator = aSong.Translator
        newSong.Lyrics = aSong.Lyrics
        newSong.ChordPro = aSong.ChordPro
        Return newSong
    End Function

End Class