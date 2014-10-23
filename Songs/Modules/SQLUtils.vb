Imports System.Data
Imports System.Data.SqlClient


Public Module SqlUtils

    'Creates parameters for stored procs
    Function CreateParamVarchar(ByVal paramName As String, ByVal text As String) As SqlParameter
        text = Trim(text)
        Dim aParam As SqlParameter
        aParam = New SqlParameter(paramName, SqlDbType.VarChar)
        aParam.Direction = ParameterDirection.Input
        If text = "" Then
            aParam.Value = DBNull.Value
        Else
            aParam.Value = text
        End If
        CreateParamVarchar = aParam
    End Function

    Function CreateParamInteger(ByVal paramName As String, ByVal value As String) As SqlParameter
        value = Trim(value)
        Dim aParam As SqlParameter
        aParam = New SqlParameter(paramName, SqlDbType.Int)
        aParam.Direction = ParameterDirection.Input
        If value = "" Then
            aParam.Value = DBNull.Value
        Else
            aParam.Value = CInt(value)
        End If
        CreateParamInteger = aParam
    End Function

    Function CreateParamFloat(ByVal paramName As String, ByVal value As String) As SqlParameter
        value = Trim(value)
        Dim aParam As SqlParameter
        aParam = New SqlParameter(paramName, SqlDbType.Float)
        aParam.Direction = ParameterDirection.Input
        If value = "" Then
            aParam.Value = DBNull.Value
        Else
            aParam.Value = CDbl(ValidateDecimal(value))
        End If
        CreateParamFloat = aParam
    End Function

    Function CreateParamDateTime(ByVal paramName As String, ByVal value As String) As SqlParameter
        value = Trim(value)
        Dim aParam As SqlParameter
        aParam = New SqlParameter(paramName, SqlDbType.DateTime)
        aParam.Direction = ParameterDirection.Input
        If value = "" Then
            aParam.Value = DBNull.Value
        Else
            aParam.Value = CDate(value)
        End If
        CreateParamDateTime = aParam
    End Function

    Function CreateParamVarcharOutput(ByVal paramName As String) As SqlParameter
        Dim aParam As SqlParameter
        aParam = New SqlParameter(paramName, SqlDbType.VarChar)
        aParam.Direction = ParameterDirection.Output
        CreateParamVarcharOutput = aParam
    End Function

    Function CreateParamIntegerOutput(ByVal paramName As String) As SqlParameter
        Dim aParam As SqlParameter
        aParam = New SqlParameter(paramName, SqlDbType.Int)
        aParam.Direction = ParameterDirection.Output
        CreateParamIntegerOutput = aParam
    End Function

End Module
