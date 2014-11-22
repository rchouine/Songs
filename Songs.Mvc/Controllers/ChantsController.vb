Imports Songs.Controller
Imports Songs.Model
Imports Songs.Mvc.Models
Imports Songs.Mvc.Utils

Namespace Controllers

    Public Class ChantsController
        Inherits Web.Mvc.Controller

        Function QuoiDeNeuf() As ActionResult
            Return View()
        End Function

        Private Function GetCategoryList(addTous As Boolean) As List(Of Category)
            Dim ctrl As New CategoryController
            Dim retour = ctrl.GetList
            If addTous Then
                retour.Insert(0, New Category With {.Id = 0, .Name = "[Tous]"})
            End If
            Return retour
        End Function

        Function Index() As ActionResult
            'Session.Add("USER_CODE", "raphael")
            'Session.Add("USER_PWD", "")
            'Session.Add("USER_ID", 1)
            'Session.Add("USER_LEVEL", 0)
            'Session.Add("USER_NAME", "Schwinn")
            'Session.Add("USER_FNAME", "Ralphy")

            If String.IsNullOrEmpty(Session("USER_ID")) Then
                Return RedirectToAction("Enregistrement", "Utilisateurs")
            Else
                Dim model As New ChantsViewModel
                model.Categories = GetCategoryList(True)
                Return View("Recherche", model)
            End If
        End Function

        Function Rechercher(type As String, texte As String, categorie As Integer) As JsonResult
            Dim leType As SearchType
            Select Case type
                Case "Code" : leType = SearchType.Code
                Case "Paroles" : leType = SearchType.Lyrics
                Case Else : leType = SearchType.Title
            End Select

            Dim songCtrl As New SongController
            Dim liste = songCtrl.GetList(Session("USER_ID"), leType, texte, categorie)
            Return Json(liste, JsonRequestBehavior.AllowGet)
        End Function

        Function Chant(id As Integer, shift As Integer, sharp As Integer, Optional tone As String = "") As JsonResult
            Dim songCtrl As New SongController
            Dim chantTrouve = songCtrl.GetById(id)

            Dim ctrl As New ChordProManager
            If tone <> String.Empty Then
                Dim oldtone = ctrl.PurifyTone(ctrl.ExtractSongTone(chantTrouve.ChordPro))
                Dim newtone = ctrl.PurifyTone(tone)
                shift = ctrl.FindShiftValue(oldtone, newtone)
            End If

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

            Return PartialView("Chant", model)
        End Function

        Function Enregistrer(model As ChantViewModel) As ActionResult
            If ModelState.IsValid Then
                Using scope As New Transactions.CommittableTransaction
                    Dim songCtrl As New SongController
                    model.Id = songCtrl.Save(ConvertModelToChant(model))

                    If Session("USER_LEVEL") < Songs.Model.UserLevel.User Then
                        Dim catCtrl As New CategoryController
                        catCtrl.SaveSongCategories(model.Id, From x In model.Categories Where x.Selected Select id = x.id)
                    End If
                End Using
                Return Nothing
            End If

            ' If we got this far, something failed, redisplay form 
            Return PartialView("Chant", model)
        End Function

        Sub ChangerTonalite(songId As Integer, newTone As String)
            Dim songCtrl As New SongController
            songCtrl.SaveUserSong(songId, Session("USER_ID"), newTone)
        End Sub

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
End Namespace