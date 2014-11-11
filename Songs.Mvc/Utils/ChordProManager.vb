Imports System.IO

Namespace Utils

    Public Class ChordProManager

        Private Enum TypeLigne
            Titre = 1
            SousTitre = 2
            Parole = 3
            Commentaire = 5
            NonAffichable = 10
        End Enum

        Private _tons As String(,)

        Public Sub New()
            InitilaiseTons()
        End Sub

        Private Sub InitilaiseTons()
            ReDim _tons(11, 1)

            _tons(0, 0) = "A"
            _tons(1, 0) = "A#"
            _tons(2, 0) = "B"
            _tons(3, 0) = "C"
            _tons(4, 0) = "C#"
            _tons(5, 0) = "D"
            _tons(6, 0) = "D#"
            _tons(7, 0) = "E"
            _tons(8, 0) = "F"
            _tons(9, 0) = "F#"
            _tons(10, 0) = "G"
            _tons(11, 0) = "G#"

            _tons(0, 1) = "A"
            _tons(1, 1) = "Bb"
            _tons(2, 1) = "B"
            _tons(3, 1) = "C"
            _tons(4, 1) = "Db"
            _tons(5, 1) = "D"
            _tons(6, 1) = "Eb"
            _tons(7, 1) = "E"
            _tons(8, 1) = "F"
            _tons(9, 1) = "Gb"
            _tons(10, 1) = "G"
            _tons(11, 1) = "Ab"
        End Sub

        Public Function Shift(ByVal data As String, ByVal nb As Integer, ByVal displaySharp As Boolean) As String
            Dim resultat As String = String.Empty
            Dim pos As Integer
            Dim accordEnCours As Boolean = False

            Dim i As Integer
            For i = 0 To data.Length - 1
                Select Case data.Substring(i, 1)
                    Case "["
                        accordEnCours = True
                        resultat &= data.Substring(i, 1)

                    Case "]"
                        accordEnCours = False
                        resultat &= data.Substring(i, 1)

                    Case Else
                        If accordEnCours Then
                            If data.Substring(i + 1, 1) = "#" OrElse data.Substring(i + 1, 1) = "b" Then
                                pos = RechercheTon(data.Substring(i, 2))
                                If pos > -1 Then
                                    resultat &= _tons((pos + nb + 12) Mod 12, If(displaySharp, 0, 1))
                                Else
                                    resultat &= data.Substring(i, 1)
                                End If
                                i = i + 1
                            Else
                                pos = RechercheTon(data.Substring(i, 1))
                                If pos > -1 Then
                                    resultat &= _tons((pos + nb + 12) Mod 12, If(displaySharp, 0, 1))
                                Else
                                    resultat &= data.Substring(i, 1)
                                End If
                            End If
                        Else
                            resultat &= data.Substring(i, 1)
                        End If
                End Select
            Next
            Return resultat
        End Function

        Public Function GetChordsHtml(ByVal data As String) As String
            Using newDiv As New HtmlElement
                ShowChords(data, newDiv)
                Dim sb As New StringBuilder()
                Dim sw As New StringWriter(sb)
                Dim htmlTw As New HtmlTextWriter(sw)

                newDiv.RenderControl(htmlTw)
                Return sb.ToString()
            End Using
        End Function

        Public Sub ShowChords(ByVal data As String, ByRef mainContainer As Control)
            Dim i As Integer
            Dim chorusEnCours As Boolean = False
            Dim oldChorusEnCours As Boolean = False
            Dim accordEnCours As Boolean = False
            Dim baliseNonGereEnCours As Boolean = False
            Dim type As TypeLigne
            Dim tmpData As String(,)
            Dim iCell As Integer
            Dim currentContainer As Control = mainContainer

            currentContainer.Controls.Clear()

            'Séparer les lignes
            data = data.Trim
            Dim lignes As String() = data.Split(CChar(vbCr))

            For Each ligne As String In lignes
                type = TypeLigne.Parole
                iCell = 0
                ReDim tmpData(2, iCell)

                'Parcourir la ligne
                For i = 0 To ligne.Length - 1
                    Select Case ligne.Substring(i, 1)
                        Case "{"
                            If ligne.Length > 7 AndAlso ligne.Substring(i, 7).ToLower = "{title:" Then
                                type = TypeLigne.Titre
                                i = i + 6
                                tmpData(2, iCell) = "title"

                            ElseIf ligne.Length > 10 AndAlso ligne.Substring(i, 10).ToLower = "{subtitle:" Then
                                type = TypeLigne.SousTitre
                                i = i + 9
                                tmpData(2, iCell) = "subtitle"

                            ElseIf ligne.Length > 3 AndAlso ligne.Substring(i, 3).ToLower = "{c:" Then
                                type = TypeLigne.Commentaire
                                i = i + 2
                                tmpData(2, iCell) = "c"

                            ElseIf ligne.Substring(i, 5).ToLower = "{soc}" Then
                                chorusEnCours = True
                                i = i + 4
                                type = TypeLigne.NonAffichable

                            ElseIf ligne.Substring(i, 5).ToLower = "{eoc}" Then
                                chorusEnCours = False
                                i = i + 4
                                type = TypeLigne.NonAffichable

                            Else
                                baliseNonGereEnCours = True
                            End If

                        Case "}"
                            baliseNonGereEnCours = False
                            'Si on a un commentaire terminé et que la ligne ne l'est pas
                            If tmpData(2, iCell) = "c" AndAlso i < ligne.Length Then
                                iCell = iCell + 1
                                ReDim Preserve tmpData(2, iCell)
                            End If

                        Case "["
                            accordEnCours = True
                            If tmpData(1, iCell) <> String.Empty Then
                                iCell = iCell + 1
                                ReDim Preserve tmpData(2, iCell)
                            End If

                        Case "]"
                            accordEnCours = False
                            tmpData(0, iCell) &= "&nbsp;"

                        Case Else
                            If Not baliseNonGereEnCours Then
                                If accordEnCours Then
                                    If ligne.Substring(i, 1) = " " Then
                                        tmpData(0, iCell) &= "&nbsp;"
                                    ElseIf ligne.Substring(i, 1) <> vbLf Then
                                        tmpData(0, iCell) &= ligne.Substring(i, 1)
                                    End If
                                Else
                                    If ligne.Substring(i, 1) = " " Then
                                        tmpData(1, iCell) &= "&nbsp;"
                                    ElseIf ligne.Substring(i, 1) <> vbLf Then
                                        tmpData(1, iCell) &= ligne.Substring(i, 1)
                                    End If
                                End If
                            End If
                    End Select
                Next

                'Mode refrain
                If chorusEnCours <> oldChorusEnCours Then
                    If chorusEnCours Then
                        Using newChorus As New Panel
                            newChorus.CssClass = "chorus"
                            mainContainer.Controls.Add(newChorus)
                            currentContainer = newChorus
                        End Using
                    Else
                        currentContainer = mainContainer
                    End If
                    oldChorusEnCours = chorusEnCours
                End If

                If type <> TypeLigne.NonAffichable Then
                    'ligne(vide)
                    If tmpData(0, 0) = String.Empty AndAlso tmpData(1, 0) = String.Empty Then
                        Using newLine As New Panel
                            Using newText As New Literal
                                newText.Text = "&nbsp;"
                                newLine.Controls.Add(newText)
                            End Using
                            currentContainer.Controls.Add(newLine)
                        End Using
                    Else
                        Using newTable As New Table
                            If tmpData(2, 0) = "title" OrElse tmpData(2, 0) = "subtitle" Then
                                newTable.CssClass = "titleTable"
                            Else
                                newTable.CssClass = "songTable"
                            End If
                            Using newRowChord As New TableRow
                                newRowChord.CssClass = "chords"
                                For i = 0 To iCell
                                    Using newCellChord As New TableCell
                                        newCellChord.Text = tmpData(0, i)
                                        newRowChord.Cells.Add(newCellChord)
                                    End Using
                                Next
                                newTable.Rows.Add(newRowChord)
                            End Using

                            Using newRowLyric As New TableRow
                                If type = TypeLigne.Parole Then
                                    newRowLyric.CssClass = "lyrics"
                                End If
                                For i = 0 To iCell
                                    Using newCellLyric As New TableCell
                                        If tmpData(2, i) = "title" Then
                                            newCellLyric.CssClass = "titre"
                                        ElseIf tmpData(2, i) = "subtitle" Then
                                            newCellLyric.CssClass = "sousTitre"
                                        ElseIf tmpData(2, i) = "c" Then
                                            newCellLyric.CssClass = "commentaire"
                                        End If
                                        newCellLyric.Text = tmpData(1, i)
                                        newRowLyric.Cells.Add(newCellLyric)
                                    End Using
                                Next
                                newTable.Rows.Add(newRowLyric)
                            End Using
                            currentContainer.Controls.Add(newTable)
                        End Using
                    End If
                End If
            Next
        End Sub

        Private Function RechercheTon(ton As String) As Integer
            Dim i As Integer
            For i = 0 To UBound(_tons)
                If ton = _tons(i, 0) OrElse ton = _tons(i, 1) Then
                    Return i
                End If
            Next
            Return -1

        End Function
    End Class
End Namespace