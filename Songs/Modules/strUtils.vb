Module StrUtils
    'utility for doubling quotes in a string
    Public Function DblQuotes(ByVal txt As String) As String
        DblQuotes = Replace(txt, "'", "''")
    End Function

    'utility for converting a string to be interpreted in a jscript command in a string
    Public Function JavaString(ByVal txt As String) As String
        JavaString = Replace(Replace(txt, "\", "\\"), "'", "\'")
    End Function

    'Convert line feed for <BR>
    Function StrToHtml(ByVal text As String) As String
        StrToHtml = Replace(text, vbLf, "<BR>")
    End Function

    'Calls the correct function (may change)
    Function ValidateDecimal(ByVal text As String) As String
        ValidateDecimal = CommaToDot(text)
    End Function

    'Convert dot for a comma
    Function DotToComma(ByVal text As String) As String
        DotToComma = Replace(text, ".", ",")
    End Function

    'Convert dot for a comma
    Function CommaToDot(ByVal text As String) As String
        CommaToDot = Replace(text, ",", ".")
    End Function
    
End Module
