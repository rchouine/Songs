Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Songs.Mvc.Utils
Imports FluentAssertions

Namespace Utils
    <TestClass()> Public Class ChordProManagerTest

        <TestMethod()> Public Sub ShiftSongOverAnOctave()
            Dim oldData = "aaa[A]...[C]xxxx iii[Ab]xxxx ccc[A#]fffff[D]gggg xxxx g[C#]zzz[G]ooo"
            Dim newData = "aaa[B]...[D]xxxx iii[A#]xxxx ccc[C]fffff[E]gggg xxxx g[D#]zzz[A]ooo"
            Dim cpMan As New ChordProManager
            Dim result = cpMan.Shift(oldData, 14, True)
            result.Should.Be(newData)
        End Sub

        <TestMethod()> Public Sub ShiftSong_withFlat()
            Dim oldData = "aaa[A]...[C]xxxx iii[Ab]xxxx ccc[A#]fffff[D]gggg xxxx g[C#]zzz[G]ooo"
            Dim newData = "aaa[B]...[D]xxxx iii[Bb]xxxx ccc[C]fffff[E]gggg xxxx g[Eb]zzz[A]ooo"
            Dim cpMan As New ChordProManager
            Dim result = cpMan.Shift(oldData, 14, False)
            result.Should.Be(newData)
        End Sub

        <TestMethod()> Public Sub PurifyTone_straight()
            Dim cpMan As New ChordProManager
            Dim tone = cpMan.PurifyTone("A")

            tone.Should.Be("A")
        End Sub

        <TestMethod()> Public Sub PurifyTone_minor()
            Dim cpMan As New ChordProManager
            Dim tone = cpMan.PurifyTone("Am")

            tone.Should.Be("A")
        End Sub

        <TestMethod()> Public Sub PurifyTone_unHandled()
            Dim cpMan As New ChordProManager
            Dim tone = cpMan.PurifyTone("zzz")

            tone.Should.BeNullOrEmpty()
        End Sub


        <TestMethod()> Public Sub PurifyTone_fromFrench()
            Dim cpMan As New ChordProManager
            Dim tone = cpMan.PurifyTone("Sol")

            tone.Should.Be("G")
        End Sub

        <TestMethod()> Public Sub PurifyTone_withSharp()
            Dim cpMan As New ChordProManager
            Dim tone = cpMan.PurifyTone("Sol#")

            tone.Should.Be("G#")
        End Sub

        <TestMethod()> Public Sub PurifyTone_withFlat()
            Dim cpMan As New ChordProManager
            Dim tone = cpMan.PurifyTone("Solb")

            tone.Should.Be("Gb")
        End Sub

        <TestMethod()> Public Sub PurifyTone_withCrap()
            Dim cpMan As New ChordProManager
            Dim tone = cpMan.PurifyTone("Solsus4")

            tone.Should.Be("G")
        End Sub

        <TestMethod()> Public Sub ExtractSongTone()
            Dim cpMan As New ChordProManager
            Dim tone As String = cpMan.ExtractSongTone("ChordPro Song blablabla [Bbm] and so on [F] and again")

            tone.Should.Be("Bbm")
        End Sub

        <TestMethod()> Public Sub FindShiftValue()
            Dim cpMan As New ChordProManager
            Dim shift As Integer = cpMan.FindShiftValue("G", "Bb")
            shift.Should.Be(3)
        End Sub

        <TestMethod()> Public Sub FindShiftValue2()
            Dim cpMan As New ChordProManager
            Dim shift As Integer = cpMan.FindShiftValue("Bb", "G")
            shift.Should.Be(9)
        End Sub

    End Class
End Namespace
