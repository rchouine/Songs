Imports Songs.Controller
Imports Songs.web

Public Class ChantsController
    Inherits System.Web.Mvc.Controller

    '
    ' GET: /Songs

    Function Chants() As ActionResult
        Dim songCtrl As New SongController
        Dim liste = songCtrl.GetList(1, Model.SearchType.Title, "", 1)

        Dim vm As New ChantsViewModel
        vm.Chants = liste
        vm.TabIndex = 0
        Return View("Recherche", vm)
    End Function

    Function Rechercher(typeRecherche As String, texteRecherche As String, TabIndex As Integer) As ActionResult
        Dim leType As Model.SearchType
        Select Case typeRecherche
            Case "code" : leType = Model.SearchType.Code
            Case "paroles" : leType = Model.SearchType.Lyrics
            Case Else : leType = Model.SearchType.Title
        End Select

        Dim songCtrl As New SongController
        Dim liste = songCtrl.GetList(1, leType, texteRecherche, 0)

        Dim vm As New ChantsViewModel
        vm.Chants = liste
        vm.TabIndex = TabIndex
        Return View("Recherche", vm)
    End Function

    Function Chant(id As Integer, shift As Integer, sharp As Integer) As JsonResult
        Dim songCtrl As New SongController
        Dim chantTrouve = songCtrl.GetById(id)

        Dim ctrl As New ChordProController

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

        Dim ctrl As New ChordProController
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