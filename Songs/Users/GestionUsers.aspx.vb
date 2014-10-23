Imports Songs.Model
Imports Songs.Controller

Partial Class Users_GestionUsers
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If CStr(Session("USER_ID")) = "" Then
            Response.Redirect("../Login/Login.aspx")
        End If

        Dim userCtrl As New UserController
        Dim userList = userCtrl.GetList
        Dim filteredList As List(Of User)

        If CInt(Session("USER_LEVEL")) > UserLevel.Admin Then
            'Les utilisateurs non admin ne voient qu'eux-même
            filteredList = (From x In userList Where x.Id = CStr(Session("USER_ID")))
        ElseIf CInt(Session("USER_LEVEL")) > UserLevel.MeMyself Then
            'Seulement moi voie les comptes supprimés
            filteredList = (From x In userList Where x.Level < UserLevel.Suppressed)
        Else
            filteredList = userList
        End If

        repUsers.DataSource = filteredList
        repUsers.DataBind()
    End Sub

    Public Function GetUserLevel(ByVal level As Integer) As String
        Select Case level
            Case UserLevel.MeMyself : Return "Gestionnaire"
            Case UserLevel.Admin : Return "Administrateur"
            Case UserLevel.PowerUser : Return "Ajout / Modif."
            Case UserLevel.User : Return "Utilisateur"
            Case UserLevel.Deactivate : Return "<label style=""color: red"">Désactivé<label>"
            Case UserLevel.Suppressed : Return "<label style=""color: red"">Supprimé<label>"
            Case Else : Return "Indéfini"
        End Select
    End Function

    Public Function GetUserModif(ByVal id As Integer, ByVal level As Integer) As String
        If level >= CInt(Session("USER_LEVEL")) Then
            If CInt(Session("USER_LEVEL")) > 10 Then
                If CInt(Session("USER_ID")) = id Then
                    Return "<img src='../Images/pictosBoutons/modifier.gif' Title='Modifier' style='cursor: hand' onclick='UpdateUser(" & id & ");' />"
                Else
                    Return ""
                End If
            Else
                Return "<img src='../Images/pictosBoutons/modifier.gif' alt='Modifier' style='cursor: hand' onclick='UpdateUser(" & id & ");' />"
            End If
        Else
            Return ""
        End If
    End Function

    Public Function GetUserDelete(ByVal id As Integer, ByVal level As Integer) As String
        If level >= CInt(Session("USER_LEVEL")) Then
            If CInt(Session("USER_ID")) = id Then
                Return ""
            Else
                Return "<img src='../Images/pictosBoutons/supprimer.gif' alt='Supprimer' style='cursor: hand' onclick='DeleteUser(" & id & ");' />"
            End If
        Else
            Return ""
        End If
    End Function

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Dim userCtrl As New UserController
        userCtrl.Delete(CInt(Request("txtUserToDelete")))
        repUsers.DataBind()
    End Sub
End Class
