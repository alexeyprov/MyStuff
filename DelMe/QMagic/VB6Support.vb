Module VB6Support
    Public Null = System.DBNull.Value
    Public Empty = Nothing

    Function IsNull(ByVal arg As Object) As Boolean
        IsNull = IsDBNull(arg)
    End Function

    Function IsEmpty(ByVal arg As Object) As Boolean
        IsEmpty = IsNothing(arg)
    End Function

    Function IsObject(ByVal arg As Object) As Boolean
        IsObject = IsReference(arg)
    End Function

    Function Array(ByVal ParamArray args() As Object) As Object()
        Array = args
    End Function

End Module
