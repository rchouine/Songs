Imports System.Data.SqlClient
Imports System.Configuration


Public Class ConnectionSql
    Implements System.IDisposable

    Public Property Reader As SqlDataReader
    Private ReadOnly _conn As SqlConnection
    ReadOnly _cmd As SqlCommand

    Sub New()
        Dim cs = ConfigurationManager.ConnectionStrings("strcnx")
        _conn = New SqlConnection(cs.ConnectionString)
        _cmd = New SqlCommand()
        _cmd.CommandType = System.Data.CommandType.StoredProcedure
        _cmd.Connection = _conn
        _conn.Open()
    End Sub

    Sub AddParameter(name As String, type As System.Data.SqlDbType, value As Object, setNull As Boolean)
        If setNull Then
            If (type = System.Data.SqlDbType.VarChar AndAlso value.ToString = String.Empty) _
            OrElse (type = System.Data.SqlDbType.Int AndAlso CInt(value) = 0) Then
                value = System.DBNull.Value
            End If
        End If

        Dim param As New SqlParameter()
        param.SqlDbType = type
        param.Direction = System.Data.ParameterDirection.Input
        param.ParameterName = name
        param.Value = value

        _cmd.Parameters.Add(param)
    End Sub

    Sub ClearParameters()
        _cmd.Parameters.Clear()
    End Sub

    Sub OpenReader(query As String)
        _cmd.CommandText = query
        Reader = _cmd.ExecuteReader()
    End Sub

    Sub ExecuteSql(query As String)
        _cmd.CommandText = query
        _cmd.ExecuteNonQuery()
    End Sub

    Sub Dispose() Implements System.IDisposable.Dispose
        If _cmd IsNot Nothing Then
            _cmd.Parameters.Clear()
            _cmd.Dispose()
        End If

        If _conn IsNot Nothing Then
            If _conn.State <> System.Data.ConnectionState.Closed Then
                _conn.Close()
            End If
            _conn.Dispose()
        End If
    End Sub
End Class
