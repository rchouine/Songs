Imports Songs.Model
Imports Ploeh.AutoFixture
Imports FluentAssertions


<TestClass()> Public Class SongControllerTest

    Private ReadOnly _fixture As New Fixture

    <TestMethod()>
    Public Sub ImplementClassSong()
        Dim newSong = _fixture.Create(Of Song)()
        newSong.Title = "ABC"

        newSong.Title.Should.Be("ABC")
    End Sub

    <TestMethod()>
    Public Sub GetSongById_ShouldReturnRightSong()
        Dim SongC As New SongController
        Dim newSong = SongC.GetById(12)

        newSong.Id.Should.Be(12)
    End Sub

    <TestMethod()>
    Public Sub GetSongList_ShouldReturnListOfSong()
        Dim SongC As New SongController
        Dim songList = SongC.GetList(1, SearchType.Title, "croix", 0)

        songList.Should.NotBeNull()
        songList.Any.Should.BeTrue()
        songList.Count.Should.BeGreaterThan(1)
        songList.Item(0).Title.Should.Contain("croix")
    End Sub
End Class