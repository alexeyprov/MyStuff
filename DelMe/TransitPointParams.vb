'***************************************************************
'* VBScript -> VB.NET change info:
'*  Array(x, y, z, ...) -> New Object() {x, y, z, ...}
'*  Null -> System.DBNull.Value
'*  IsNull -> IsDbNull
'*  Empty -> Nothing
'*  IsEmpty -> IsNothing
'*  IsObject -> IsReference
'*  Class_Initialize -> Cls_Initialize; Sub New has been added
'*  Number of array dimension has been added to array definitions
'*  Set obj = value -> obj = value
'***************************************************************
' Original code begins here

' Class: TransitPointParams
' Purpose: This class aids in the creation and access of the Exit parameter array.
' Note:
' To pass this into an exit function, follow the example below:
'
'     Dim objParams, arrParams
'     Set objParams = Global.CreateTransitPointParamsObj ()
'     objParams.SetUserDefParam 1, vntOrderId
'     objParams.SetUserDefParam 2, vntAlertId
'     objParams.SetUserDefParam 3, vntOutputResult
'     objParams.SetUserDefParam 4, objOutputResult2
'     objParams.AddDefaultField "Company", "ABC Inc."
'     objParams.AddDefaultField "Ship_To", "Hello Inc."
'
'     arrParams = objParams.ConstructParams ()
'     form.DoNewFormData (arrParams)
'
'     Dim strErrorKey
'     If Not IsEmpty (vntParams.GetFirstErrorMsg (arrParams, strErrorKey)) Then 'Check for error
'         MsgBox GetFirstErrorMsg (arrParams, strErrorKey)
'     End If
'     ...
'     Set objParams = Nothing
'
'  On the receiving end of a client script:
'
'  Function NewFormData (form, vntParams)
'     Dim vrstProduct, rstProduct
'     Dim vntParams2
'     Set vrstProduct = form.DoNewFormData (vntParams)
'     Set rstProduct = vrstProduct(0)
'
'     Dim objParams, rstTemp, vntOutput
'     Set objParams = Global.CreateTransitPointParamsObj()
'     If Not objParams.HasValidParams (vntParams) Then
'        UIMaster.ShowErrorMessage "Error in Params."
'        Exit Function
'     End If
'     objParams.SetDefaultFields rstProduct, vntParams 'Set all default fields.
'     vntOutput = objParams.GetUserDefParam (3, vntParams)
'     Set rstTemp = objParams.GetUserDefParam (4, vntParams)
'     ...
'     Set objParams = nothing
'  End Function
'
'
Class TransitPointParams
    ' Name: ConstructParams
    ' Purpose: This function returns the parameter array constructed by calls to
    ' AddDefaultField (...) and SetUserDef (...).
    ' Note:
    ' It's more efficient to pass the member array, m_values, directly.  Unfortunately,
    ' there's currently a bug in VBScript 5.0 that prevents this.  Therefore, this class
    ' is written to be easily upgraded when VBScript 5.5 becomes available.
    Function ConstructParams()
        If iNumDefaultFields > 0 Then
            ReDim Preserve m_defaultFields(1, iNumDefaultFields - 1)
        End If

        Select Case m_iNumUserDef
            Case 0
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5)}
            Case 1
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6)}
            Case 2
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7)}
            Case 3
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7), m_values(8)}
            Case 4
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7), m_values(8), m_values(9)}
            Case 5
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7), m_values(8), m_values(9), m_values(10)}
            Case 6
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7), m_values(8), m_values(9), m_values(10), m_values(11)}
            Case 7
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7), m_values(8), m_values(9), m_values(10), m_values(11), m_values(12)}
            Case 8
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7), m_values(8), m_values(9), m_values(10), m_values(11), m_values(12), m_values(13)}
            Case 9
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7), m_values(8), m_values(9), m_values(10), m_values(11), m_values(12), m_values(13), m_values(14)}
            Case 10
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7), m_values(8), m_values(9), m_values(10), m_values(11), m_values(12), m_values(13), m_values(14), m_values(15)}
            Case 11
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7), m_values(8), m_values(9), m_values(10), m_values(11), m_values(12), m_values(13), m_values(14), m_values(15), m_values(16)}
            Case 12
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7), m_values(8), m_values(9), m_values(10), m_values(11), m_values(12), m_values(13), m_values(14), m_values(15), m_values(16), m_values(17)}
            Case 13
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7), m_values(8), m_values(9), m_values(10), m_values(11), m_values(12), m_values(13), m_values(14), m_values(15), m_values(16), m_values(17), m_values(18)}
            Case 14
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7), m_values(8), m_values(9), m_values(10), m_values(11), m_values(12), m_values(13), m_values(14), m_values(15), m_values(16), m_values(17), m_values(18), m_values(19)}
            Case 15
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7), m_values(8), m_values(9), m_values(10), m_values(11), m_values(12), m_values(13), m_values(14), m_values(15), m_values(16), m_values(17), m_values(18), m_values(19), m_values(20)}
            Case 16
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7), m_values(8), m_values(9), m_values(10), m_values(11), m_values(12), m_values(13), m_values(14), m_values(15), m_values(16), m_values(17), m_values(18), m_values(19), m_values(20), m_values(21)}
            Case 17
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7), m_values(8), m_values(9), m_values(10), m_values(11), m_values(12), m_values(13), m_values(14), m_values(15), m_values(16), m_values(17), m_values(18), m_values(19), m_values(20), m_values(21), m_values(22)}
            Case 18
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7), m_values(8), m_values(9), m_values(10), m_values(11), m_values(12), m_values(13), m_values(14), m_values(15), m_values(16), m_values(17), m_values(18), m_values(19), m_values(20), m_values(21), m_values(22), m_values(23)}
            Case 19
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7), m_values(8), m_values(9), m_values(10), m_values(11), m_values(12), m_values(13), m_values(14), m_values(15), m_values(16), m_values(17), m_values(18), m_values(19), m_values(20), m_values(21), m_values(22), m_values(23), m_values(24)}
            Case 20
                ConstructParams = New Object() {m_values(0), m_values(1), m_defaultFields, m_values(3), System.DBNull.Value, m_values(5), m_values(6), m_values(7), m_values(8), m_values(9), m_values(10), m_values(11), m_values(12), m_values(13), m_values(14), m_values(15), m_values(16), m_values(17), m_values(18), m_values(19), m_values(20), m_values(21), m_values(22), m_values(23), m_values(24), m_values(25)}
            Case Else
                UIMaster.ShowErrorMessage("Current size limit of parameter array is 20.  Please make changes to the global_transitParams script.")
        End Select
    End Function

    ' Name: GetFirstErrorMsg
    ' Purpose: This function checks to see if the Exit function's parameter list contains an
    ' error message.  It returns the message if the answer is yes, otherwise Empty.
    Public Function GetFirstErrorMsg(ByVal vntParams, ByVal strErrorKey)
        Dim arrMsg
        m_iErrIndex = 0

        arrMsg = vntParams(1)
        If Not IsNothing(vntParams(1)) Then
            If Not IsNothing(arrMsg(0, m_iErrIndex)) Then

                GetFirstErrorMsg = arrMsg(1, m_iErrIndex)
            End If
        Else
            GetFirstErrorMsg = Nothing
        End If
    End Function

    Public Function GetNextErrorMsg(ByVal vntParams, ByVal strErrorKey)
        Dim arrMsg
        m_iErrIndex = m_iErrIndex + 1

        arrMsg = vntParams(1)
        If Not IsNothing(vntParams(1)) Then
            If Not IsNothing(arrMsg(0, m_iErrIndex)) Then
                strErrorKey = arrMsg(0, m_iErrIndex)
                GetNextErrorMsg = arrMsg(1, m_iErrIndex)
            End If
        Else
            GetNextErrorMsg = Nothing
        End If
    End Function

    'Name: SetInfoString
    Public Sub SetInfoString(ByVal strMsg)
        m_values(0) = strMsg
    End Sub

    'Name: SetFKFieldName
    Public Sub SetFKFieldName(ByVal strFKFieldName)
        m_values(3) = strFKFieldName
    End Sub

    ' Name: SetFakeNewFormDataFlag
    ' Purpose:  This method is used to set a flag to inform the middle-tier script that the
    '           NewFormData is fake one so that the middle-tier script will bring back a new
    '           Record Id
    ' 05/15/2001  DY
    Public Sub SetFakeNewFormDataFlag(ByVal blnFlag)
        m_values(4) = New Object() {blnFlag, System.DBNull.Value}
    End Sub

    ' Name SetFakeNewFormDataFlagIntoParams
    ' Purpose:  This method is used to set a flag into the transit point parameter list
    '           to inform the middle-tier script that the NewFormData is fake one so that
    '           the middle-tier script will bring back a new
    '           Record Id
    ' 05/15/2001  DY
    Public Sub SetFakeNewFormDataFlagIntoParams(ByVal blnFlag, ByVal vntParams)
        If Not HasValidParams(vntParams) Then
            vntParams = ConstructParams()
        End If
        If Not IsArray(vntParams(4)) Then
            vntParams(4) = New Object() {blnFlag, System.DBNull.Value}
        Else
            vntParams(4)(0) = blnFlag
        End If
    End Sub

    ' -------------------------------------------------------------------------------------------
    ' Name:    SetExtendedParam
    ' Purpose: This procedure used in the server side scripts to set extended parameter into the
    '          transit point parameter list.
    ' Assumptions:
    ' Inputs:
    '   strParamName  - The parameter name to be added into the parameter list
    '   vntParamValue - The parameter value to be added into transit point parameter list
    ' Outputs:
    ' Returns:
    ' Implements Agent:
    ' Revision# Date            Author  Description
    ' --------  ----            ------  -----------
    ' 5.0       10/30/2002      DY      Initial version
    ' -------------------------------------------------------------------------------------------
    Public Sub SetExtendedParam(ByVal strParamName, ByVal vntParamValue)
        Dim vntParams
        Dim intParam

        If Not HasValidParams(m_values) Then Cls_Initialize()
        vntParams = m_values(5)
        If Not IsArray(vntParams) Then
            ReDim vntParams(0)
            intParam = 0
        Else
            intParam = UBound(vntParams)
            intParam = intParam + 1
            ReDim Preserve vntParams(intParam)
        End If
        vntParams(intParam) = New Object() {strParamName, System.DBNull.Value}
        If IsReference(vntParamValue) Then
            vntParams(intParam)(1) = vntParamValue
        Else
            vntParams(intParam)(1) = vntParamValue
        End If
        m_values(5) = vntParams

    End Sub


    ' Name: GetInfoString
    ' Purpose: This function checks to see if the Exit function's parameter list contains an
    ' informational message.  It returns the message if the answer is yes, otherwise Empty.
    Public Function GetInfoString(ByVal vntParams)
        If Not IsNothing(vntParams(0)) Then
            GetInfoString = vntParams(0)
        Else
            GetInfoString = Nothing
        End If
    End Function

    ' Name: GetFKFieldName
    ' Purpose: This function checks to see if the Exit function's parameter list contains an
    ' Foreign Key field name from the parent form.  It returns the field name, otherwise Empty.
    Public Function GetFKFieldName(ByVal vntParams)
        If Not IsNothing(vntParams(3)) Then
            GetFKFieldName = vntParams(3)
        Else
            GetFKFieldName = Nothing
        End If
    End Function

    ' Name: GetFakeNewFormDataFlag
    ' Purpose: This function is used to get NewRecordId Flag
    ' 05/15/2001  DY
    Public Function GetFakeNewFormDataFlag(ByVal vntParams)
        GetFakeNewFormDataFlag = False
        If Not HasValidParams(vntParams) Then Exit Function
        If Not IsArray(vntParams(4)) Then Exit Function
        If IsNothing(vntParams(4)(0)) Then Exit Function
        If IsDBNull(vntParams(4)(0)) Then Exit Function
        GetFakeNewFormDataFlag = vntParams(4)(0)
    End Function

    ' Name: GetNewRecordId
    ' Purpose: This function is used to get NewRecord Id
    ' 05/15/2001  DY
    Public Function GetNewRecordId(ByVal vntParams)
        GetNewRecordId = System.DBNull.Value
        If Not HasValidParams(vntParams) Then Exit Function
        If Not IsArray(vntParams(4)) Then Exit Function
        If UBound(vntParams(4)) < 1 Then Exit Function
        If IsNothing(vntParams(4)(1)) Then Exit Function
        GetNewRecordId = vntParams(4)(1)
    End Function

    ' -------------------------------------------------------------------------------------------
    ' Name:    GetExtendedParam
    ' Purpose: This procedure used in the server side scripts to get extended parameter from the
    '          transit point parameter list.
    ' Assumptions:
    ' Inputs:
    '   strParamName  - The parameter name to be added into the parameter list
    '   vntParam      - The transit point parameter list
    ' Outputs:
    '   GetExtendedParam - Transition Point Parameters array
    ' Returns:
    ' Implements Agent:
    ' Revision# Date            Author  Description
    ' --------  ----            ------  -----------
    ' 5.0       10/31/2002      DY      Initial version
    ' -------------------------------------------------------------------------------------------
    Public Function GetExtendedParam(ByVal strParamName, ByVal vntParams)
        Dim i

        GetExtendedParam = System.DBNull.Value
        If Not HasValidParams(vntParams) Then Exit Function
        If Not IsArray(vntParams(5)) Then Exit Function
        On Error Resume Next
        If IsNothing(vntParams(5)(0)) Then Exit Function
        If IsDBNull(vntParams(5)(0)) Then Exit Function
        If Not IsNothing(vntParams) And Not IsDBNull(vntParams) Then
            'Continue setting the rest of the fields even if anyone of them fails in the middle
            On Error Resume Next
            For i = 0 To UBound(vntParams(5))
                If IsArray(vntParams(5)(i)) Then
                    If Not IsDBNull(vntParams(5)(i)(0)) Then
                        If LCase(vntParams(5)(i)(0)) = LCase(strParamName) Then
                            If IsReference(vntParams(5)(i)(1)) Then
                                GetExtendedParam = vntParams(5)(i)(1)
                            Else
                                GetExtendedParam = vntParams(5)(i)(1)
                            End If
                            Exit For
                        End If
                    End If
                End If
            Next
        End If

    End Function


    ' Name: AddDefaultField
    ' Purpose: This subroutine adds a default field pair to the parameter list.
    '
    Public Sub AddDefaultField(ByVal strFieldName, ByVal vntFieldValue)
        If iNumDefaultFields >= iCurrentDefaultFieldArraySize Then
            iCurrentDefaultFieldArraySize = iCurrentDefaultFieldArraySize + 5 'increment size by 5.
            ReDim Preserve m_defaultFields(1, iCurrentDefaultFieldArraySize - 1)
        End If

        m_defaultFields(0, iNumDefaultFields) = strFieldName
        m_defaultFields(1, iNumDefaultFields) = vntFieldValue

        iNumDefaultFields = iNumDefaultFields + 1
    End Sub


    ' Name: SetUserDefParam
    ' Purpose: This subroutine is for setting user-defined parameter from
    ' array element m_values(6) onward.
    ' Inputs: iParamIndex is 1-based.
    Public Sub SetUserDefParam(ByVal iUserParamIndex, ByVal userValue)
        If iUserParamIndex < 1 Then
            On Error Resume Next
            UIMaster.ShowErrorMessage("User parameter index is 1-based.  It cannot be less zero or a negative number.")
            m_values(9999) = userValue 'force VBScript to throw an error.
            Exit Sub
        End If

        If iUserParamIndex + 5 >= iCurrentParamArraySize Then
            iCurrentParamArraySize = iCurrentParamArraySize + 5
            ReDim Preserve m_values(iCurrentParamArraySize - 1)
        End If

        If IsReference(userValue) Then
            m_values(5 + iUserParamIndex) = userValue
        Else
            m_values(5 + iUserParamIndex) = userValue
        End If

        If (iUserParamIndex > m_iNumUserDef) Then
            m_iNumUserDef = iUserParamIndex
        End If
    End Sub

    ' Name: GetUserDefParam
    Public Function GetUserDefParam(ByVal iUserParamIndex, ByVal vntParams)
        If IsNothing(vntParams) Then
            Err.Raise(13402, "GetUserDefParam", UIMaster.GetText("Errors", "Transit Point Parameters Exepcted", 13402))
        End If

        If iUserParamIndex < 1 Then
            'On Error Resume Next
            'UIMaster.ShowErrorMessage "User parameter index is 1-based.  It cannot be zero or a negative number."
            GetUserDefParam = Nothing
            Err.Raise(13402, "GetUserDefParam", UIMaster.GetText("Errors", "Transit Point Parameters Exepcted", 13402))
            Exit Function
        End If

        If iUserParamIndex > UBound(vntParams) - 5 Then
            On Error Resume Next
            GetUserDefParam = Nothing
            Err.Raise(13402, "GetUserDefParam", UIMaster.GetText("Errors", "Transit Point Parameters Exepcted", 13402))
            Exit Function
        End If

        If IsReference(vntParams(5 + iUserParamIndex)) Then
            GetUserDefParam = vntParams(5 + iUserParamIndex)
        Else
            GetUserDefParam = vntParams(5 + iUserParamIndex)
        End If
    End Function

    Public Sub Clear()
        m_iNumUserDef = 0

        Dim i
        For i = 0 To iNumDefaultFields - 1
            m_defaultFields(0, i) = Nothing
            m_defaultFields(1, i) = Nothing
        Next

        iNumDefaultFields = 0
    End Sub

    ' Class Constructor
    ' 10/31/2002        DY              Make the 6-th reserved parameters as an extended parameter array
    '                                   to meet any more request for the reserved parameters.
    Private Sub Cls_Initialize() 'former Class_Initialize
        iCurrentParamArraySize = 10
        iCurrentDefaultFieldArraySize = 10
        m_iNumUserDef = 0
        ReDim m_values(iCurrentParamArraySize - 1) 'set default size to 10
        ReDim m_defaultFields(1, iCurrentDefaultFieldArraySize - 1) 'default set to a 10 x 2 array

        m_values(0) = Nothing
        m_values(1) = Nothing
        m_values(3) = Nothing
        m_values(4) = System.DBNull.Value 'reserved
        m_values(5) = System.DBNull.Value 'reserved

        iNumDefaultFields = 0
    End Sub

    ' Constructor for VB.Net
    Sub New()
        Cls_Initialize()
    End Sub

    ' Name: HasValidParams
    ' Purpose: This function checks to see if an Exit function's parameter list is valid.
    '
    Public Function HasValidParams(ByVal vntParams)
        'Need to check the error array, too.
        '

        If IsDBNull(vntParams) = False And IsArray(vntParams) = True _
            And IsNothing(vntParams) = False Then
            HasValidParams = True
        Else
            HasValidParams = False
        End If
    End Function

    ' Member variables
    Private m_values() 'the parameter array
    Private m_defaultFields(,) 'the default field array
    Private m_iErrIndex
    Private iCurrentParamArraySize, iCurrentDefaultFieldArraySize
    Private iNumDefaultFields 'number of default field pairs.
    Private m_iNumUserDef
End Class

