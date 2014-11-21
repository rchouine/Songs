Imports System.IO

Namespace Utils

    Public Class ChordProManager

        Private SharpTones As New List(Of String) From {"A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#"}
        Private FlatTones As New List(Of String) From {"A", "Bb", "B", "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab"}

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
                                    resultat &= GetShiftedTone(pos + nb, displaySharp)
                                Else
                                    resultat &= data.Substring(i, 1)
                                End If
                                i = i + 1
                            Else
                                pos = RechercheTon(data.Substring(i, 1))
                                If pos > -1 Then
                                    resultat &= GetShiftedTone(pos + nb, displaySharp)
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

        Private Function GetShiftedTone(position As Integer, displaySharp As Boolean) As String
            If displaySharp Then
                Return SharpTones.Item((position + 12) Mod 12)
            Else
                Return FlatTones.Item((position + 12) Mod 12)
            End If
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
            Dim affichable As Boolean
            Dim tmpData As String(,)
            Dim iCell As Integer
            Dim currentContainer As Control = mainContainer

            currentContainer.Controls.Clear()

            'Séparer les lignes
            data = data.Trim
            Dim lignes As String() = data.Split(CChar(vbCr))

            For Each ligne As String In lignes
                affichable = True
                iCell = 0
                ReDim tmpData(2, iCell)

                'Parcourir la ligne
                For i = 0 To ligne.Length - 1
                    Select Case ligne.Substring(i, 1)
                        Case "{"
                            If ligne.Length > 7 AndAlso ligne.Substring(i, 7).ToLower = "{title:" Then
                                i = i + 6
                                tmpData(2, iCell) = "title"

                            ElseIf ligne.Length > 10 AndAlso ligne.Substring(i, 10).ToLower = "{subtitle:" Then
                                i = i + 9
                                tmpData(2, iCell) = "subtitle"

                            ElseIf ligne.Length > 3 AndAlso ligne.Substring(i, 3).ToLower = "{c:" Then
                                i = i + 2
                                tmpData(2, iCell) = "c"

                            ElseIf ligne.Substring(i, 5).ToLower = "{soc}" Then
                                chorusEnCours = True
                                i = i + 4
                                affichable = False

                            ElseIf ligne.Substring(i, 5).ToLower = "{eoc}" Then
                                chorusEnCours = False
                                i = i + 4
                                affichable = False

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

                If affichable Then
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
                                newRowLyric.CssClass = "lyrics"

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
            Dim retour = SharpTones.IndexOf(ton)
            If retour = -1 Then
                retour = FlatTones.IndexOf(ton)
            End If
            Return retour
        End Function

        Function PurifyTone(tone As String) As String
            Dim pos = tone.IndexOf("/")
            If pos > 0 Then
                tone = Left(tone, pos)
            End If
            tone = tone.ToUpper
            tone = tone.Replace("É", "E")
            tone = tone.Replace("SOL", "SO")
            Dim retour As String
            Select Case Left(tone, 2)
                Case "DO" : retour = "C"
                Case "RE" : retour = "D"
                Case "MI" : retour = "E"
                Case "FA" : retour = "F"
                Case "SO" : retour = "G"
                Case "LA" : retour = "A"
                Case "SI" : retour = "B"
                Case Else
                    If SharpTones.Contains(Left(tone, 1)) Then
                        retour = Left(tone, 1)
                    Else
                        Return String.Empty
                    End If
            End Select

            Return retour & GetSharpOrFlat(tone)
        End Function

        Private Function GetSharpOrFlat(tone As String) As String
            If tone.Length > 2 Then
                If tone.Substring(2, 1) = "#" Then
                    Return "#"
                ElseIf tone.Substring(2, 1) = "B" Then
                    Return "b"
                End If
            ElseIf tone.Length > 1 Then
                If tone.Substring(1, 1) = "#" Then
                    Return "#"
                ElseIf tone.Substring(1, 1) = "B" Then
                    Return "b"
                End If
            End If
            Return String.Empty
        End Function

        Function FindShiftValue(oldTone As String, newTone As String) As Integer
            Dim oldIndex = RechercheTon(oldTone)
            Dim newIndex = RechercheTon(newTone)
            Return (newIndex - oldIndex + 12) Mod 12
        End Function

        Function ExtractSongTone(chordProSong As String) As String
            Dim posDebut = chordProSong.IndexOf("["c)
            Dim posFin = chordProSong.IndexOf("]"c)
            If posDebut > 0 AndAlso posFin > 0 Then
                Return chordProSong.Substring(posDebut + 1, posFin - posDebut - 1)
            Else
                Return String.Empty
            End If
        End Function

    End Class
End Namespace