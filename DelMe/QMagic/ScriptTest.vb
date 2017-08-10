Option Explicit On 

Class ScriptTest
    Const strtVACANCY_HISTORY = "QM_vakantiehistorie"
    Const strfVACANCY_HISTORY_ID = "QM_vakantiehistorie_id"

    

    Function OnSecondaryAddClick()
        On Error Resume Next

        Dim objEvent
        Dim objParams
        Dim vntParams
        Dim arRet
        Dim vntEmplId

        OnSecondaryAddClick = False
        objEvent = UIMaster.RUICenter.FormEventObj
        vntEmplId = UIMaster.RUICenter.RecordId

        Select Case objEvent.SegmentName
            Case "Actions"
                objParams = Global.CreateTransitPointParamsObj() 'TODO: prepend with set in script
                Call objParams.SetUserDefParam(1, vntEmplId)
                vntParams = objParams.ConstructParams() 'TODO: prepend with set in script
                Call UIMaster.ShowForm(RDAUILib.ActionTypeEnum.actionAskUser, "Start", Null, vntParams) 'TODO: unqualify enum in script
                OnSecondaryAddClick = True
            Case "Hours history" ''modal form => primary must be saved
                If Not Global.IsNullID(vntEmplId) Then
                    objParams = Global.CreateTransitPointParamsObj()
                    Call objParams.SetUserDefParam(1, vntEmplId)
                    vntParams = objParams.ConstructParams()
                    arRet = UIMaster.ShowFormByTableModal(strtVACANCY_HISTORY, Null, vntParams)
                    If arRet(0) <> 0 Then
                        Call UIMaster.RUICenter.Reload()
                    End If
                End If
                OnSecondaryAddClick = True
        End Select

        If Err.Number <> 0 Then
            UIMaster.ShowErrorMessage(Err.Description)
        End If
    End Function

    Function OnSecondaryEditClick()
        On Error Resume Next

        Dim rdaForm
        Dim objFormObj
        Dim rs
        Dim strSegment_Name
        Dim strTab_Name
        Dim vntVac_Hist_Id
        Dim arRet

        Call Err.Clear()
        On Error Resume Next

        OnSecondaryEditClick = False

        rdaForm = UIMaster.RUICenter.Form  'TODO: prepend with set in script
        objFormObj = UIMaster.RUICenter.FormEventObj  'TODO: prepend with set in script
        strTab_Name = objFormObj.TabName
        strSegment_Name = objFormObj.SegmentName

        If "General" = strTab_Name And "Hours history" = strSegment_Name Then
            '1. apply changes
            '        Call UIMaster.RUICenter.Apply(Empty)
            '        Call Global.CMSErrorInitialize(Err)

            '        Global.CMSErrorHandling
            ' The following code is a tempporary solution
            '        If Err.Number <> 0 Then
            '            Call Global.ShowSystemErrorMessage(Err)
            '            Exit Function
            '        End If
            '        On Error Resume Next

            '2. show modal form
            rs = UIMaster.RUICenter.GetRecordsetEx(strTab_Name, strSegment_Name) 'TODO: prepend with set in script
            With rs
                Call .MoveFirst()
                Call .Move(objFormObj.rowIndex - 1)
                vntVac_Hist_Id = .Fields(strfVACANCY_HISTORY_ID).Value
            End With
            arRet = UIMaster.ShowFormByTableModal(strtVACANCY_HISTORY, vntVac_Hist_Id)
            If arRet(0) <> 0 Then
                Call UIMaster.RUICenter.Reload()
            End If
            OnSecondaryEditClick = True
        End If

        If Err.Number <> 0 Then
            Call UIMaster.ShowErrorMessage(Err.Description)
        End If
    End Function


    ' -----------------------------------------------------------------------------------------------------------------
    ' RefreshForm
    ' Purpose:  Calculates values for siblings of qm_workable_hours
    ' ------------------------------------------------------------------------------------------------------
    ' Inputs:
    '   rfrmForm      : 
    '   vntRecordsets : 
    '   vntParameters : 
    ' Returns:
    '   AddFormData   : 
    ' Implements Agent: 
    ' History:
    ' Reversion#    Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 1.0                        VDI\OlegSh      Initial version
    ' 1.1                        VDI\AlexeyP     Changed method of finding parent division
    '                                            Apply made in order to save pending secondaries
    ' ------------------------------------------------------------------------------------------
    Sub RefreshForm()
        Dim strEndDate
        Dim strStartDate
        Dim rstPForm As ADODB.Recordset 'TODO: remove in script
        Dim rstSec As ADODB.Recordset 'TODO: remove in script
        Dim rstSec2 As ADODB.Recordset 'TODO: remove in script
        Dim Parameters
        Dim rdlgParam As RDAUILib.IRDialogParam3 'TODO: remove in script
        '<alexeyp>
        Dim vntDivisionId
        Dim vntBudgDivisionId

        Stop

        On Error Resume Next
        If UIMaster.RUICenter.Dirty Then
            'If we have any pending secondaries save them to database
            Call UIMaster.RUICenter.ApplyNoReload(Array(Empty))
        End If
        '</alexeyp>

        rstPForm = UIMaster.RUICenter.PrimaryRecordset 'TODO: prepend with set in script
        rstSec = UIMaster.RUICenter.GetRecordset("Detailed_Info") 'TODO: prepend with set in script
        rstSec2 = UIMaster.RUICenter.GetRecordset("Hidden Segment") 'TODO: prepend with set in script

        strStartDate = rstPForm.fields("convert_month_date").value
        If Month(strStartDate) = 12 Then
            strEndDate = DateSerial(Year(strStartDate) + 1, 1, 1)
        Else
            strEndDate = DateSerial(Year(strStartDate), Month(strStartDate) + 1, 1)
        End If
        With rstSec2
            If Not (.BOF And .EOF) Then Call .MoveFirst() ' VDI\RuslanKu: we should start from the first record

            '<alexeyp>
            ''Recordset must be ordered by budget division ids!!
            vntBudgDivisionId = UIMaster.RSysClient.StringToId("0x0000000000000000")
            vntDivisionId = UIMaster.RSysClient.StringToId("0x0000000000000000")
            '</alexeyp>
            While Not .EOF
                '<alexeyp>
                ' change division id if budget division has changed
                If Not UIMaster.RSysClient.EqualIds(vntBudgDivisionId, .Fields("QM_Division_ref").Value) Then
                    vntBudgDivisionId = .Fields("QM_Division_ref").Value
                    vntDivisionId = Form.FindDivision(rstSec, vntBudgDivisionId)
                    If Global.IsNullID(vntDivisionId) Then
                        Call UIMaster.ShowErrorMessage("Division is not defined!")
                        Exit Sub
                    End If
                End If
                '</alexeyp>

                ' employee count
                ReDim Parameters(4)
                Parameters(0) = vntDivisionId
                Parameters(1) = rstSec2.Fields("Territory_ref").Value
                Parameters(2) = strEndDate
                Parameters(3) = strStartDate
                Call UIMaster.RUICenter.Form.Execute("CountEmployee", Parameters)
                rstSec2.Fields("staff_realization").Value = Parameters(0)
                ' employee count
                ' turnover count
                ReDim Parameters(4)
                Parameters(0) = vntDivisionId
                Parameters(1) = rstSec2.Fields("Territory_ref").Value
                Parameters(2) = strEndDate
                Parameters(3) = strStartDate
                Call UIMaster.RUICenter.Form.Execute("CountTurnover", Parameters)
                rstSec2.Fields("turnover_realization").Value = Parameters(0)
                rstSec2.Fields("turnover_worked").Value = Parameters(1)
                rstSec2.Fields("hours_worked").Value = Parameters(2)
                ' turnover count
                ' forecast count
                ReDim Parameters(4)
                Parameters(0) = strEndDate
                Parameters(1) = strStartDate
                Parameters(2) = vntDivisionId
                Parameters(3) = rstSec2.Fields("Territory_ref").Value
                Call UIMaster.RUICenter.Form.Execute("CountForecast", Parameters)
                rstSec2.Fields("Forecast_").Value = Parameters(0) ' VDI\RuslanKu: changed fields("hours_worked") to fields("Forecast_")
                ' forecast count

                Call .MoveNext()
            End While
        End With
        Call UIMaster.RUICenter.Form.Execute("ClearOrphanDetails", Empty)

        rdlgParam = UIMaster.CreateDialogParam()  'TODO: prepend with set in script
        With rdlgParam
            Call .AddButton("OK")          'Display OK button only. 
            Call .AddSection("Notification", "Finished refreshing.....")
        End With

        'UIMaster.RUICenter.Apply Empty

        Call UIMaster.ShowDialogModal(rdlgParam, "Notification")

        'EmployeeCount( Parameters )
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' Support_Contract Form
    Const strmFIND_ANOTHER_CONTRACT_WITH_REG = "FindAnotherContractWithReg"
    Const strmDROP_WEB_CONTRACT_FLAG = "DropWebContractFlag"
    Const strfWEB_CONTRACT = "Web_Contract"
    Const strfREGISTRATION_ID = "Registration_Id"

    Const strmsgCONFIRM_SAVE = "Web Contract for current Registration alread exists.  You can only have one Web Contract per Registration.  Do you want to make this the web contract?"

    Function CheckWebContractFlag() As Boolean 'TODO: remove in script
        Dim vntRegId
        Dim vntWebCtrId 'ID of existing web contract
        Dim rfrmForm As RDALib.IRForm3 'TODO: remove in script
        Dim rstPrimary As ADODB.Recordset 'TODO: remove in script
        Dim objParam As TransitPointParams 'TODO: remove in script
        Dim vntArgument

        On Error Resume Next

        CheckWebContractFlag = False
        rfrmForm = UIMaster.RUICenter.Form 'TODO: prepend with set in script
        rstPrimary = UIMaster.RUICenter.PrimaryRecordset 'TODO: prepend with set in script

        If rstPrimary.Fields(strfWEB_CONTRACT).Value <> 0 Then
            vntRegId = rstPrimary.Fields(strfREGISTRATION_ID).Value
            If Not IsNull(vntRegId) Then
                ''1. find out another web support contracts with same registration
                objParam = Global.CreateTransitPointParamsObj() 'TODO: prepend with Set in script
                Call objParam.SetUserDefParam(1, vntRegId)
                Call objParam.SetUserDefParam(2, UIMaster.RUICenter.RecordId)
                vntArgument = objParam.ConstructParams()

                Call rfrmForm.Execute(strmFIND_ANOTHER_CONTRACT_WITH_REG, vntArgument)
                If Err.Number <> 0 Then
                    Call Global.CMSMsgBox(Err.Description, vbOKOnly, "Error while search of possible duplicate")
                    Exit Function
                End If

                vntWebCtrId = objParam.GetUserDefParam(1, vntArgument)

                'do not merge these two statements in one!
                'VBScript does not support short circuit evaluation
                If IsNull(vntWebCtrId) Then
                    CheckWebContractFlag = True
                    Exit Function
                End If
                If Global.IsNullID(vntWebCtrId) Then
                    CheckWebContractFlag = True
                    Exit Function
                End If

                ''2. ask user whether to continue saving and drop 
                '' "web contract" flag in contract found
                If vbYes = Global.CMSMsgBox(strmsgCONFIRM_SAVE, vbYesNo, "Warning") Then
                    Call objParam.Clear()
                    Call objParam.SetUserDefParam(1, vntWebCtrId)
                    vntArgument = objParam.ConstructParams()

                    Call rfrmForm.Execute(strmDROP_WEB_CONTRACT_FLAG, vntArgument)
                    If Err.Number <> 0 Then
                        Call Global.CMSMsgBox(Err.Description, vbOKOnly, "Error while resetting web contract flag in existing contract")
                        Exit Function
                    End If
                End If
            End If
        End If
        CheckWebContractFlag = True
    End Function

    Function OnSave() As Boolean 'TODO: remove in script
        OnSave = CheckWebContractFlag()

        ''NB: The main part of agent has moved into ASR::SaveFormData() function
    End Function


    Sub CheckUniqueCertificate()
        Const strsQM_Employee_Certificates = "QM Certificates"
        Const strfQM_Certificates_fk = "QM_Certificates_fk"
        Const strfQM_EmployeeCertId = "QM_Employee_Certificates_Id"

        Dim rstEmpCert

        Dim vntCertificateId
        Dim vntEmployeeCertId
        Dim blnFound : blnFound = False
        Dim nRowIndex
        Dim nIndex

        rstEmpCert = UIMaster.RUICenter.GetRecordset(strsQM_Employee_Certificates)

        If rstEmpCert.EOF And rstEmpCert.BOF Then Exit Sub

        nRowIndex = UIMaster.RUICenter.FormEventObj.RowIndex

        vntCertificateId = rstEmpCert.Fields(strfQM_Certificates_fk).Value
        vntEmployeeCertId = rstEmpCert.Fields(strfQM_EmployeeCertId).Value

        nIndex = 1
        rstEmpCert.MoveFirst()
        Do While Not rstEmpCert.EOF
            If (UIMaster.RSysClient.EqualIds(vntCertificateId, rstEmpCert.Fields(strfQM_Certificates_fk).Value)) _
               And (nIndex <> nRowIndex) Then
                blnFound = True
                Exit Do
            End If
            rstEmpCert.MoveNext()
            nIndex = nIndex + 1
        Loop

        If blnFound Then
            rstEmpCert.MoveFirst()
            rstEmpCert.Move(nRowIndex - 1)
            rstEmpCert.Fields(strfQM_Certificates_fk).Value = Null

            Global.CMSMsgBox("This Certificate already exists. Please choose another Certificate.", vbOKOnly, "Error")

            UIMaster.RUICenter.FocusField(strsQM_Employee_Certificates, strfQM_Certificates_fk, nRowIndex)
        End If

    End Sub
End Class
