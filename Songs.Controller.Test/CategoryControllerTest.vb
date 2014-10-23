Imports FluentAssertions

<TestClass()> Public Class CategoryControllerTest

    <TestMethod()>
    Public Sub GetSongList_ShouldReturnListOfSong()
        Dim catCtrl As New CategoryController
        Dim catList = catCtrl.GetList

        catList.Should.NotBeNull()
        catList.Any.Should.BeTrue()
        catList.Count.Should.BeGreaterThan(1)
    End Sub

    <TestMethod()>
    Public Sub GetById_ShouldReturnRightCat()
        Dim catCtrl As New CategoryController
        Dim newCat = catCtrl.GetById(1)

        newCat.Name.Should.Be("Nouveaux")
    End Sub

End Class