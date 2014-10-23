Imports Songs.Model
Imports Ploeh.AutoFixture
Imports FluentAssertions

<TestClass()> Public Class UserControlerTest

    Private ReadOnly _fixture As New Fixture

    <TestMethod()>
    Public Sub ImplementClassUser()
        Dim newUser = _fixture.Create(Of User)()
        newUser.Name = "ABC"

        newUser.Name.Should.Be("ABC")
    End Sub

    <TestMethod()>
    Public Sub GetUserById_ShouldReturnRightUser()
        Dim userCtrl As New UserController
        Dim newUser = userCtrl.GetById(1)

        newUser.Id.Should.Be(1)
    End Sub

    <TestMethod()>
    Public Sub GetUserByCode_ShouldReturnRightUser()
        Dim userCtrl As New UserController
        Dim newUser = userCtrl.GetByCode("Raphael")

        newUser.Name.Should.Be("Chouinard")
    End Sub

    <TestMethod()>
    Public Sub GetUserList_ShouldReturnListOfUser()
        Dim userC As New UserController
        Dim userList = userC.GetList()

        userList.Should.NotBeNull()
        userList.Any.Should.BeTrue()
        userList.Count.Should.BeGreaterThan(1)
    End Sub

End Class