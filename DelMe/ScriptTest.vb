Module ScriptTest
    'forms
    Const strfrmCONTACT = "Contact"
    'fields
    Const strfCLOCK_NAME = "Name"
    Const strfCLOCK_TYPE = "Type"
    Const strfDISCONNECTED_COMPANY = "Company"
    Const strfRN_DESCRIPTOR = "Rn_Descriptor"
    'methods
    Const strmGET_COMPANY_URL = "GetCompanyURL"
    Const strmGET_COMPANY_CONTACTS = "GetCompanyContacts"
    'searches
    Const strsrchMY_SEARCH = "My Clock Search"

    Sub OnBtnJump2WebClicked()
        Dim sFieldName
        Dim objParam As TransitPointParams 'TODO: remove in script
        Dim vntArgument
        Dim sURL

        '1. Find real name by title
        sFieldName = Global.GetDisconnectedFieldNameEx(strfDISCONNECTED_COMPANY)
        If "" = sFieldName Then
            Exit Sub
        End If

        '2. Prepare call
        objParam = Global.CreateTransitPointParamsObj() 'TODO: prepend with Set in script
        Call objParam.SetUserDefParam(1, UIMaster.RUICenter.PrimaryRecordset(sFieldName).Value)
        vntArgument = objParam.ConstructParams()
        ''vParams = "Microsoft" '...
        Call UIMaster.RUICenter.Form.Execute(strmGET_COMPANY_URL, vntArgument)

        '3. Process result
        ''        Call MsgBox(objParam.GetUserDefParam(1, vntArgument))
        sURL = objParam.GetUserDefParam(1, vntArgument)
        If sURL <> "NA" Then
            Call UIMaster.ShowUrlInWindow(objParam.GetUserDefParam(1, vntArgument))
        Else
            Call MsgBox("Company you selected has no web site.")
        End If
    End Sub

    'TODO: remove ByVal in script
    Sub ShowModalContacts(ByVal rs, ByVal tableId)
        Dim selected
        selected = UIMaster.ShowSelectionListModal(tableId, rs)
        'show selected contact
        If UBound(selected) >= 0 Then
            Call UIMaster.ShowFormModal(strfrmCONTACT, selected(0))
        End If
    End Sub

    'TODO: remove ByVal in script
    Sub ShowModelessContacts(ByVal rs, ByVal tableId)
        Dim rfrmDef As RDAUILib.IRModelessWindowDef3 'TODO: remove in script
        rfrmDef = Global.CreateModelessWindowDef() 'TODO: prepend with Set in script
        Call UIMaster.ShowSelectionListModeless(tableId, rs, rfrmDef)
    End Sub

    'TODO: remove ByVal in script
    Sub ProcessSelectedRecords(ByVal rs As ADODB.Recordset, ByVal fieldName As String)
        With rs
            Call .MoveFirst()
            While Not .EOF
                Call MsgBox(.Fields(fieldName), , fieldName)
                Call .MoveNext()
            End While 'TODO: replace with WEnd in script
        End With
    End Sub

    Sub OnBtnListContactsClicked()
        Dim sFieldName
        Dim objParam As TransitPointParams 'TODO: remove in script
        Dim vntArgument
        Dim tableId

        Dim rs
        Dim nShowWay

        '1. Find real name by title
        sFieldName = Global.GetDisconnectedFieldNameEx(strfDISCONNECTED_COMPANY)
        If "" = sFieldName Then
            Exit Sub
        End If

        '2. Prepare call
        objParam = Global.CreateTransitPointParamsObj() 'TODO: prepend with Set in script
        Call objParam.SetUserDefParam(1, UIMaster.RUICenter.PrimaryRecordset(sFieldName).Value)
        vntArgument = objParam.ConstructParams()
        ''vParams = "Microsoft" '...
        Call UIMaster.RUICenter.Form.Execute(strmGET_COMPANY_CONTACTS, vntArgument)

        '3. Process result
        ''        Call MsgBox(objParam.GetUserDefParam(1, vntArgument))
        tableId = objParam.GetUserDefParam(1, vntArgument)
        rs = objParam.GetUserDefParam(2, vntArgument) 'TODO: prepend with Set in script
        If Not (rs Is Nothing) Then
            ''4. ask the way to show results (modal/modeless)
            ''      show results as he(she) wants
            nShowWay = Global.CMSDialogRadio("Select the way to show contacts:", _
                           "Contacts found", New Object() {"Modal", "Modeless"}) 'TODO: change to Array(,) in script
            Select Case nShowWay
                Case 1 'modal
                    Call ShowModalContacts(rs, tableId)
                Case 2 'modeless
                    Call ShowModelessContacts(rs, tableId)
                Case Else 'user cancelled
            End Select
        Else
            Call MsgBox("Company is not found or has no contacts.")
        End If
    End Sub

    Sub OnBtnCoolSearchClicked()
        Dim nClkType
        Dim objFactory As RDAUILib.IRUISearchFactory2 'TODO: remove in script
        Dim objButtons As RDAUILib.IRUICustomIndexButtons 'TODO: remove in script
        Dim objBut As RDAUILib.IRUICustomIndexButton 'TODO: remove in script
        Dim arBtnIndicies(2) As Integer 'TODO: remove in script
        Dim objSelResults As RDAUILib.IRUIMultiSelectResults 'TODO: remove in script
        On Error GoTo CatchErr

        '1. Check type of selected clock
        nClkType = UIMaster.RUICenter.PrimaryRecordset(strfCLOCK_TYPE).Value
        If IsDBNull(nClkType) Then 'TODO: replace with IsNull in script
            Call Global.CMSMsgBox("You must select clock type first", vbOKOnly, "Search Failed")
            Exit Sub
        End If

        '2. Create search factory
        objFactory = UIMaster.CreateCenterReference("search")     'TODO: prepend with Set in script
        With objFactory
            .SearchType = RDAUILib.SearchTypeEnum.searchTypeRegular 'TODO: unqualify enum name in script
            .MultiSelectMode = RDAUILib.MultiSelectModeEnum.mulselBoolean 'TODO: unqualify enum name in script

            .search = UIMaster.RSysClient.GetSearch(strsrchMY_SEARCH)     'TODO: prepend with Set in script
            .Options.AutoRun = True
            .Parameters(0) = nClkType
        End With

        '3. Create buttons in search window and add it to search factory
        objButtons = UIMaster.CreateIndexButtons()     'TODO: prepend with Set in script

        objBut = UIMaster.CreateIndexButton()     'TODO: prepend with Set in script
        objBut.Label = "Show Names"
        objBut.ToolTip = "Displays names of selected clocks"
        arBtnIndicies(0) = objButtons.SetItem(objBut)

        objBut = UIMaster.CreateIndexButton()     'TODO: prepend with Set in script
        objBut.Label = "Show Descriptors"
        objBut.ToolTip = "Displays descriptors of selected clocks"
        arBtnIndicies(1) = objButtons.SetItem(objBut)

        objFactory.SearchButtons = objButtons 'TODO: prepend with Set in script

        '4. Run search in modal window and process results
        objSelResults = UIMaster.ShowMultiSelectModal(objFactory)      'TODO: prepend with Set in script

        With objSelResults
            Select Case .SelectedButton
                Case arBtnIndicies(0)
                    'show names of selected clocks
                    Call ProcessSelectedRecords(.SelectedRecords, strfCLOCK_NAME)
                Case arBtnIndicies(1)
                    Call ProcessSelectedRecords(.SelectedRecords, strfRN_DESCRIPTOR)
            End Select
        End With
        Exit Sub
CatchErr:
        Call MsgBox("Search failed")
    End Sub

    Const strfENDDATE = "End_Date"
    Const strfSTARTDATE = "Start_Date"
    Const strfPOSSIBLEDUP = "Possible_Duplicate"
    Const strfFIRSTNAME = "First_Name"
    Const strfLASTNAME = "Last_Name"
    Const strfMIDDLENAME = "Middle_Name_"
    'Const strEMAIL = "andreyf@moscow.vdiweb.com"
    Const strEMAIL = "support@qmagic.nl"

    Const strSUBJ = "%1 was added as a new employee, please add to NT security."
    Const strfDIVISIONID2 = "Division_"
    Const strfDIVISIONID = "QM_Division_Id"
    Const strfTERRITORYID = "Territory_Id"
    Const strfJOBTITLE = "QM_functie_referentie"
    Const strfJOBTITLEID = "QM_functietabel_Id"
    Const strfWORKEMAIL = "Work_Email"
    Const strfEMPNUMMER = "Number_"
    Const strfUSERNAME = "Web_login_name"
    Const strfPASSWORD = "Web_login_name"
    Const strfSTATUS = "Status"
    Const strSOLLICITANT = "Sollicitant"
    Const strPROJECTPOOL = "Projectpool"

    Const strfENDDATEDELTA = "_End_Date_Delta"
    Const strfSTARTDATEDELTA = "_Start_Date_Delta"
    Const strfJOBCONTRACT = "Job_Contract_"
    Const strfJOBCONTRACTDELTA = "_Job_Contract_Delta"
    Const strfSALARY = "Salary_"
    Const strfSALARYDELTA = "_Salary_Delta"
    Const strfCHRS = "Contract_Hours"
    Const strfCHRSDELTA = "_Contract_Hours_Delta"
    Const strfSTATUSDELTA = "_Status_Delta"
    Const strfAPPLICANTSTATUS = "QM_Applicant_status"
    Const strfAPPLICANTSTATUSDELTA = "_QM_Applicant_status_Delta"
    Const strfTRAVELSBY = "QM_Employee_travels_by_fk"
    Const strfTRAVELSBYDELTA = "_QM_Employee_travels_by_delta"
    Const strPERSONAL = "Personal"
    Const strGENERAL = "General"
    Const strCONTRACT = "Contract"
    Const strFA = "F&A"
    Const strCONTRHISTORY = "Contract History"
    Const strEMPLOYEEHRM = "Employee HRM"
    Const strADMIN = "Admin"
    Const strCONTRACTTYPE = "Contract_Type"
    Const strNIETMEETALEN = "Niet_Meetellen_Flatscreen"
    Const strADP = "Meld_ADP"
    Const strZIEKENFONDS = "Meld_Ziekenfonds"
    Const strARBO = "Meld_ARBO"
    Const strPENSIONEN = "Meld_pensioen"
    Const strWAO = "Meld_WAO"
    Const strCVINFO = "CV info"
    Const strCVSTATUS = "CV status"
    Const strBROUCHEERV = "cv_status_branche_ervaring"
    Const strKNOWLEDGE = "cv_status_knowledge"
    Const strKORTPROFIEL = "cv_status_kort_profiel"
    Const strOPLEIDINGEN = "cv_status_opleidingen"
    Const strSPELFOUTEN = "cv_status_spelfouten"
    Const strSTIJL = "cv_status_stijl"
    Const strTRAININGEN = "cv_status_trainingen"
    Const strLAATSGECONTR = "cv_status_laats_gecontroleerd"
    Const strFOTOINODRE = "cv_status_foto_in_odre"
    Const strGECONTRDOOR = "cv_status_gecontroleerd_door"
    Const strUITDIENST = "Uit dienst"
    Const strECDONE = "Exit_Call_Done"
    Const strECDESCR = "Exit_Call_Description"
    Const strTOCUSTOMER = "To customer"
    Const strTERROVRD = "Territory_override"
    Const strRATE = "Rate"
    Const strBASICINFO = "Basic Info"
    Const strCONTRHOURS = "Contract_Hours"
    Const strCOMPCOST = "ComputerCost_"
    Const strEXPCONS = "ExpenseCost_"
    Const strMARGIN = "Margin_"
    Const strMOBPHONECOSTS = "MobilePhoneCosts_"
    Const strPENSION = "Pension_"
    Const strPROFITSHARING = "ProfitSharing_"
    Const strSALAMOUNT = "QM_employee_sal_amount"
    Const strSALNR = "QM_employee_sal_nr"
    Const strSALARY = "Salary_"
    Const strTRAININGCOST = "TrainingCost_"
    Const strTRAVELCOST = "TravelCost_"
    Const strYEARLYHOURS = "YearlyHours_"
    Const strEIGENAUTO = "Eigen_bijdrage_auto_"



    ''Function NewFormData(ByVal rfrmForm, ByVal vntParameters)
    ''    Dim vntRecordsets
    ''    vntRecordsets = rfrmForm.DoNewFormData(vntParameters)

    ''    'vntParameters(0) indicates if current user is in "QMagic finance" groupe
    ''    If Not vntParameters(0) Then
    ''        UIMaster.RUICenter.DisableField("Admin", "Niet_Meetellen_Flatscreen", True)
    ''        UIMaster.RUICenter.DisableField("Employee HRM", "Status", True)
    ''        UIMaster.RUICenter.DisableField("Basic Info", "Start_Date", True)
    ''        UIMaster.RUICenter.DisableField("Basic Info", "End_Date", True)
    ''        UIMaster.RUICenter.DisableField("F&A", "Meld_ADP", True)
    ''        UIMaster.RUICenter.DisableField("F&A", "Meld_Ziekenfonds", True)
    ''        UIMaster.RUICenter.DisableField("F&A", "Meld_ARBO", True)
    ''        UIMaster.RUICenter.DisableField("F&A", "Meld_pensioen", True)
    ''        UIMaster.RUICenter.DisableField("F&A", "Meld_WAO", True)
    ''        UIMaster.RUICenter.DisableField("Contract History", "Contract_type", True)
    ''        UIMaster.RUICenter.DisableField("Contract History", "End_date", True)
    ''        UIMaster.RUICenter.DisableField("Contract History", "Start_date", True)
    ''    End If

    ''    'vntParameters(1) contains current "Employee_Number" field value of "QM_Employee_Number" table
    ''    vntRecordsets(0).Fields("Number_").Value = vntParameters(1)

    ''    If Not vntRecordsets(0).Fields("Territory_override").Value Then
    ''        UIMaster.RUICenter.DisableField("Employee HRM", "Territory_Id", True)
    ''    Else
    ''        UIMaster.RUICenter.DisableField("Employee HRM", "Territory_Id", False)
    ''    End If

    ''    NewFormData = vntRecordsets
    ''End Function



    ''Function LoadFormData(ByVal rfrmForm, ByVal vntRecordId, ByVal vntParameters)
    ''    Dim rstsEmployee
    ''    Dim rstPrimary
    ''    Dim vntUserInGroups
    ''    Dim vntParams

    ''    rstsEmployee = rfrmForm.DoLoadFormData(vntRecordId, vntUserInGroups)
    ''    rstPrimary = rstsEmployee(0)

    ''    ' Populate delta fields
    ''    rstPrimary.Fields(strfSTARTDATEDELTA).Value = rstPrimary.Fields(strfSTARTDATE).Value
    ''    rstPrimary.Fields(strfENDDATEDELTA).Value = rstPrimary.Fields(strfENDDATE).Value
    ''    rstPrimary.Fields(strfJOBCONTRACTDELTA).Value = rstPrimary.Fields(strfJOBCONTRACT).Value
    ''    rstPrimary.Fields(strfSALARYDELTA).Value = rstPrimary.Fields(strfSALARY).Value
    ''    rstPrimary.Fields(strfCHRSDELTA).Value = rstPrimary.Fields(strfCHRS).Value
    ''    rstPrimary.Fields(strfSTATUSDELTA).Value = rstPrimary.Fields(strfSTATUS).Value
    ''    rstPrimary.Fields(strfAPPLICANTSTATUSDELTA).Value = rstPrimary.Fields(strfAPPLICANTSTATUS).Value
    ''    rstPrimary.Fields(strfTRAVELSBYDELTA).Value = rstPrimary.Fields(strfTRAVELSBY).Value

    ''    ' Check vacation history and update history record
    ''    Call CheckHistory(rstPrimary)
    ''    ' Update contracts for current employee
    ''    vntParams = vntRecordId
    ''    UIMaster.RUICenter.Form.Execute("UpdateContracts", vntParams)

    ''    ' check if user is in QMagic QA management
    ''    '    if Not vntUserInGroups(0) then
    ''    '       Call UIMaster.RUICenter.DisableFieldEx(strCVINFO, strCVSTATUS, strBROUCHEERV, True)
    ''    '       Call UIMaster.RUICenter.DisableFieldEx(strCVINFO, strCVSTATUS, strKNOWLEDGE, True)
    ''    '       Call UIMaster.RUICenter.DisableFieldEx(strCVINFO, strCVSTATUS, strKORTPROFIEL, True)
    ''    '       Call UIMaster.RUICenter.DisableFieldEx(strCVINFO, strCVSTATUS, strOPLEIDINGEN, True)
    ''    '       Call UIMaster.RUICenter.DisableFieldEx(strCVINFO, strCVSTATUS, strSPELFOUTEN, True)
    ''    '       Call UIMaster.RUICenter.DisableFieldEx(strCVINFO, strCVSTATUS, strSTIJL, True)
    ''    '       Call UIMaster.RUICenter.DisableFieldEx(strCVINFO, strCVSTATUS, strTRAININGEN, True)
    ''    '       'Call UIMaster.RUICenter.DisableFieldEx(strCVINFO, strCVSTATUS, strLAATSGECONTR, True)
    ''    '       Call UIMaster.RUICenter.DisableFieldEx(strCVINFO, strCVSTATUS, strFOTOINODRE, True)
    ''    '       Call UIMaster.RUICenter.DisableFieldEx(strCVINFO, strCVSTATUS, strGECONTRDOOR, True)
    ''    '    end if  

    ''    ' check if user is in QMagic finance group 
    ''    If Not vntUserInGroups(1) Then
    ''        Call UIMaster.RUICenter.DisableFieldEx(strADMIN, strADMIN, strNIETMEETALEN, True)

    ''        Call UIMaster.RUICenter.DisableFieldEx(strGENERAL, strEMPLOYEEHRM, strfSTATUS, True)
    ''        'Call UIMaster.RUICenter.DisableFieldEx(strGENERAL, strEMPLOYEEHRM, strfSTARTDATE, True)
    ''        'Call UIMaster.RUICenter.DisableFieldEx(strGENERAL, strEMPLOYEEHRM, strfENDDATE, True)

    ''        Call UIMaster.RUICenter.DisableFieldEx(strCONTRACT, strFA, strADP, True)
    ''        Call UIMaster.RUICenter.DisableFieldEx(strCONTRACT, strFA, strZIEKENFONDS, True)
    ''        Call UIMaster.RUICenter.DisableFieldEx(strCONTRACT, strFA, strARBO, True)
    ''        Call UIMaster.RUICenter.DisableFieldEx(strCONTRACT, strFA, strPENSIONEN, True)
    ''        Call UIMaster.RUICenter.DisableFieldEx(strCONTRACT, strFA, strWAO, True)

    ''        Call UIMaster.RUICenter.DisableSecondaryFieldEx(strCONTRACT, strCONTRHISTORY, strCONTRACTTYPE, True)
    ''        Call UIMaster.RUICenter.DisableSecondaryFieldEx(strCONTRACT, strCONTRHISTORY, strfSTARTDATE, True)
    ''        Call UIMaster.RUICenter.DisableSecondaryFieldEx(strCONTRACT, strCONTRHISTORY, strfENDDATE, True)
    ''    End If

    ''    ' check employee's status
    ''    If rstPrimary.Fields(strfSTATUS).Value = strUITDIENST Then
    ''        Call UIMaster.RUICenter.DisableFieldEx(strPERSONAL, strTOCUSTOMER, strECDONE, True)
    ''        Call UIMaster.RUICenter.DisableFieldEx(strPERSONAL, strTOCUSTOMER, strECDESCR, True)
    ''    End If

    ''    ' check if user is in QMagic HR group
    ''    If Not vntUserInGroups(2) Then
    ''        Call UIMaster.RUICenter.DisableFieldEx(strGENERAL, strBASICINFO, strCONTRHOURS, True)

    ''        Call UIMaster.RUICenter.DisableFieldEx(strCONTRACT, strRATE, strCOMPCOST, True)
    ''        Call UIMaster.RUICenter.DisableFieldEx(strCONTRACT, strRATE, strEXPCONS, True)
    ''        Call UIMaster.RUICenter.DisableFieldEx(strCONTRACT, strRATE, strMARGIN, True)
    ''        Call UIMaster.RUICenter.DisableFieldEx(strCONTRACT, strRATE, strMOBPHONECOSTS, True)
    ''        Call UIMaster.RUICenter.DisableFieldEx(strCONTRACT, strRATE, strPENSION, True)
    ''        Call UIMaster.RUICenter.DisableFieldEx(strCONTRACT, strRATE, strPROFITSHARING, True)
    ''        Call UIMaster.RUICenter.DisableFieldEx(strCONTRACT, strRATE, strSALAMOUNT, True)
    ''        Call UIMaster.RUICenter.DisableFieldEx(strCONTRACT, strRATE, strSALNR, True)
    ''        Call UIMaster.RUICenter.DisableFieldEx(strCONTRACT, strRATE, strSALARY, True)
    ''        Call UIMaster.RUICenter.DisableFieldEx(strCONTRACT, strRATE, strTRAININGCOST, True)
    ''        Call UIMaster.RUICenter.DisableFieldEx(strCONTRACT, strRATE, strTRAVELCOST, True)
    ''        Call UIMaster.RUICenter.DisableFieldEx(strCONTRACT, strRATE, strYEARLYHOURS, True)
    ''        Call UIMaster.RUICenter.DisableFieldEx(strCONTRACT, strRATE, strEIGENAUTO, True)
    ''    End If

    ''    ' check if territory override flag is false
    ''    If rstPrimary.Fields(strTERROVRD).Value Then
    ''        Call UIMaster.RUICenter.DisableFieldEx(strGENERAL, strEMPLOYEEHRM, strfTERRITORYID, True)
    ''    Else
    ''        Call UIMaster.RUICenter.DisableFieldEx(strGENERAL, strEMPLOYEEHRM, strfTERRITORYID, False)
    ''    End If

    ''    LoadFormData = rstsEmployee

    ''End Function




    ''Sub SaveFormData(ByVal rfrmForm, ByVal vntRecordsets, ByVal vntParameters)
    ''    Dim robjForm : robjForm = UIMaster.RUICenter
    ''    Dim rstEmployee : rstEmployee = robjForm.PrimaryRecordset

    ''    If rstEmployee.Fields("Status").Value = "Sollicitant" Then
    ''        rfrmForm.DoSaveFormData(vntRecordsets, vntParameters)
    ''        Exit Sub
    ''    End If

    ''    Dim vntFullName
    ''    With rstEmployee
    ''        vntFullName = Replace(.Fields("First_Name").Value & " " & .Fields("Middle_Name_").Value & " " & .Fields("Last_Name").Value, "  ", " ")

    ''        If .Fields("HourlyRate_").Value < .Fields("QM_Tarief_Functietabel").Value Then
    ''            If Global.CMSMsgBox("Het tarief van " & vntFullName & _
    ''                " is lager dan het berekende tarief. Wilt u doorgaan?", vbYesNo, "Attentie!") = vbNo Then
    ''                robjForm.SaveCanceled = True
    ''                Exit Sub
    ''            End If
    ''        End If
    ''    End With

    ''    Dim vntSendMail : vntSendMail = False
    ''    Dim vntMailText, vntActionText
    ''    Dim vntNewValue, vntOldValue
    ''    Dim vntText

    ''    Dim robjEmployeeTable : robjEmployeeTable = RSysClient.GetTable("Employee")
    ''    With rstEmployee
    ''        vntMailText = "Contract gegevens van " & _
    ''            vbLf & "Name: " & vntFullName & _
    ''            vbLf & "Nummer: " & .Fields("Number_").Value & _
    ''            vbLf & vbLf & "Het betreft de volgende wijzigingen:" & vbLf

    ''        vntNewValue = .Fields("Start_Date").Value : vntOldValue = .Fields("_Start_Date_Delta").Value
    ''        If IsNull(vntNewValue) <> IsNull(vntOldValue) Or vntNewValue <> vntOldValue Then
    ''            vntText = "Wijziging contractdata:" & vbLf & "Start datum was: "
    ''            If IsNull(vntOldValue) Then vntText = vntText & "niet ingevuld" Else vntText = vntText & vntOldValue
    ''            vntText = vntText & "." & vbLf

    ''            vntText = vntText & "Is gewijzigd in: "
    ''            If IsNull(vntNewValue) Then vntText = vntText & "niet ingevuld" Else vntText = vntText & vntNewValue
    ''            vntText = vntText & "." & vbLf

    ''            vntMailText = vntMailText & vntText
    ''            vntActionText = vntActionText & vntText
    ''            vntSendMail = True
    ''        End If

    ''        vntNewValue = .Fields("End_Date").Value : vntOldValue = .Fields("_End_Date_Delta").Value
    ''        If IsNull(vntNewValue) <> IsNull(vntOldValue) Or vntNewValue <> vntOldValue Then
    ''            vntText = "Wijziging contractdata:" & vbLf & "Eind datum was: "
    ''            If IsNull(vntOldValue) Then vntText = vntText & "niet ingevuld" Else vntText = vntText & vntOldValue
    ''            vntText = vntText & "." & vbLf

    ''            vntText = vntText & "Is gewijzigd in: "
    ''            If IsNull(vntNewValue) Then vntText = vntText & "niet ingevuld" Else vntText = vntText & vntNewValue
    ''            vntText = vntText & "." & vbLf

    ''            vntMailText = vntMailText & vntText
    ''            vntActionText = vntActionText & vntText
    ''            vntSendMail = True
    ''        End If

    ''        vntNewValue = .Fields("Job_Contract_").Value : vntOldValue = .Fields("_Job_Contract_Delta").Value
    ''        If IsNull(vntNewValue) <> IsNull(vntOldValue) Or vntNewValue <> vntOldValue Then
    ''            vntText = "Wijziging contracttype:" & vbLf & "Contracttype was: "
    ''            If IsNull(vntOldValue) Then vntText = vntText & "niet ingevuld" Else vntText = vntText & vntOldValue
    ''            vntText = vntText & "." & vbLf

    ''            vntText = vntText & "Is gewijzigd in: "
    ''            If IsNull(vntNewValue) Then vntText = vntText & "niet ingevuld" Else vntText = vntText & vntNewValue
    ''            vntText = vntText & "." & vbLf

    ''            vntMailText = vntMailText & vntText
    ''            vntActionText = vntActionText & vntText
    ''            vntSendMail = True
    ''        End If

    ''        vntNewValue = .Fields("Salary_").Value : vntOldValue = .Fields("_Salary_Delta").Value
    ''        If IsNull(vntNewValue) <> IsNull(vntOldValue) Or vntNewValue <> vntOldValue Then
    ''            vntText = "Wijziging salaris:" & vbLf & "Salaris was: "
    ''            If IsNull(vntOldValue) Then vntText = vntText & "niet ingevuld" Else vntText = vntText & vntOldValue
    ''            vntText = vntText & "." & vbLf

    ''            vntText = vntText & "Is gewijzigd in: "
    ''            If IsNull(vntNewValue) Then vntText = vntText & "niet ingevuld" Else vntText = vntText & vntNewValue
    ''            vntText = vntText & "." & vbLf

    ''            vntMailText = vntMailText & vntText
    ''            vntActionText = vntActionText & vntText
    ''            vntSendMail = True
    ''        End If

    ''        vntNewValue = .Fields("Contract_Hours").Value : vntOldValue = .Fields("_Contract_Hours_Delta").Value
    ''        If IsNull(vntNewValue) <> IsNull(vntOldValue) Or vntNewValue <> vntOldValue Then
    ''            vntText = "Wijziging contract uren:" & vbLf & "Contract uren was: "
    ''            If IsNull(vntOldValue) Then vntText = vntText & "niet ingevuld" Else vntText = vntText & vntOldValue
    ''            vntText = vntText & "." & vbLf

    ''            vntText = vntText & "Is gewijzigd in: "
    ''            If IsNull(vntNewValue) Then vntText = vntText & "niet ingevuld" Else vntText = vntText & vntNewValue
    ''            vntText = vntText & "." & vbLf

    ''            vntMailText = vntMailText & vntText
    ''            vntActionText = vntActionText & vntText
    ''            vntSendMail = True
    ''        End If

    ''        vntNewValue = .Fields(robjEmployeeTable.Fields("QM_Employee_travels_by_fk").DescriptorName).Value
    ''        vntOldValue = .Fields(robjEmployeeTable.Fields("_QM_Employee_travels_by_delta").DescriptorName).Value
    ''        If IsNull(vntNewValue) <> IsNull(vntOldValue) Or vntNewValue <> vntOldValue Then
    ''            vntText = "Wijziging Travels_By:" & vbLf & "Travels_By was: "
    ''            If IsNull(vntOldValue) Then vntText = vntText & "niet ingevuld" Else vntText = vntText & vntOldValue
    ''            vntText = vntText & "." & vbLf

    ''            vntText = vntText & "Is gewijzigd in: "
    ''            If IsNull(vntNewValue) Then vntText = vntText & "niet ingevuld" Else vntText = vntText & vntNewValue
    ''            vntText = vntText & "." & vbLf

    ''            vntMailText = vntMailText & vntText
    ''            vntActionText = vntActionText & vntText
    ''            vntSendMail = True
    ''        End If
    ''    End With

    ''    Dim rstContracts : rstContracts = robjForm.GetRecordsetEx("Contract", "Contract History")
    ''    With rstContracts
    ''        If .RecordCount <> 0 Then .MoveFirst()
    ''        While Not .EOF
    ''            vntNewValue = .Fields("Start_Date").Value : vntOldValue = .Fields("_Start_Date_Delta").Value
    ''            If IsNull(vntNewValue) <> IsNull(vntOldValue) Or vntNewValue <> vntOldValue Then
    ''                vntText = "Wijziging contract historie:" & vbLf & "Contract start datum was: "
    ''                If IsNull(vntOldValue) Then vntText = vntText & "niet ingevuld" Else vntText = vntText & vntOldValue
    ''                vntText = vntText & "." & vbLf

    ''                vntText = vntText & "Is gewijzigd in: "
    ''                If IsNull(vntNewValue) Then vntText = vntText & "niet ingevuld" Else vntText = vntText & vntNewValue
    ''                vntText = vntText & "." & vbLf

    ''                vntMailText = vntMailText & vntText
    ''                vntActionText = vntActionText & vntText
    ''                vntSendMail = True
    ''            End If

    ''            vntNewValue = .Fields("End_Date").Value : vntOldValue = .Fields("_End_Date_Delta").Value
    ''            If IsNull(vntNewValue) <> IsNull(vntOldValue) Or vntNewValue <> vntOldValue Then
    ''                vntText = "Wijziging contract historie:" & vbLf & "Contract eind datum was: "
    ''                If IsNull(vntOldValue) Then vntText = vntText & "niet ingevuld" Else vntText = vntText & vntOldValue
    ''                vntText = vntText & "." & vbLf

    ''                vntText = vntText & "Is gewijzigd in: "
    ''                If IsNull(vntNewValue) Then vntText = vntText & "niet ingevuld" Else vntText = vntText & vntNewValue
    ''                vntText = vntText & "." & vbLf

    ''                vntMailText = vntMailText & vntText
    ''                vntActionText = vntActionText & vntText
    ''                vntSendMail = True
    ''            End If
    ''            .MoveNext()
    ''        End While
    ''    End With

    ''    If vntSendMail Then
    ''        Dim robjSend : robjSend = UIMaster.CreateEmail
    ''        robjSend.To = "administratie@qmagic.nl"
    ''        robjSend.Subject = "Wijzigingen voor medewerker " & rstEmployee.Fields("Number_").Value
    ''        robjSend.Body = vntMailText

    ''        On Error Resume Next
    ''        robjSend.Send()
    ''        If Err.Number <> 0 Then
    ''            Global.CMSMsgBox("Failed to sent mail with contract changes of this employee. Please contact your Pivotal administrator.", vbOKOnly, "Error")
    ''            Err.Clear()
    ''        End If
    ''        On Error GoTo 0

    ''        Dim rstActions : rstActions = robjForm.GetRecordsetEx("Actions", "Actions")
    ''        With rstActions
    ''            .AddNew()
    ''            .Fields("Action_Type_").Value = "Salarismutatie"
    ''            .Fields("Description_").Value = vntActionText
    ''            .Fields("Account_Manager_").Value = RSysClient.EmployeeId
    ''        .Fields("Due_Date_").Value = Date
    ''            .Fields("Status_").Value = "Open"
    ''        End With
    ''    End If

    ''    If Not CheckStatus() Then
    ''        UIMaster.RUICenter.SaveCanceled = True
    ''        Exit Sub
    ''    End If

    ''    rfrmForm.DoSaveFormData(vntRecordsets, vntParameters)

    ''End Sub



    ''Function AddFormData(ByVal objForm, ByVal vntRecordsets, ByVal vntParameters)


    ''    Dim vntNewRecId
    ''    Dim rstPrimary
    ''    Dim vntParams
    ''    Dim rstDuplicates
    ''    Dim strMessage

    ''    strMessage = "The employee you are trying to add may already be present. " + _
    ''                 "Please carefully examine the list of employees for possible duplicates. " + _
    ''                 "Do you still want to add the employee?"

    ''    rstPrimary = vntRecordsets(0)

    ''    If rstPrimary.Fields(strfPOSSIBLEDUP).Value Or IsNull(rstPrimary.Fields(strfPOSSIBLEDUP).Value) Then
    ''        vntParams = Array(rstPrimary.Fields(strfFIRSTNAME).Value, rstPrimary.Fields(strfLASTNAME).Value)

    ''        UIMaster.RUICenter.Form.Execute("CheckForDuplicates", vntParams)

    ''        rstDuplicates = vntParams(6)

    ''        If rstDuplicates.RecordCount > 0 Then
    ''            If Global.CMSMsgBox(strMessage, vbYesNo, "Duplicates") = vbYes Then
    ''                Call UIMaster.ShowSelectionListModal("Employee", rstDuplicates)
    ''            Else
    ''                UIMaster.RUICenter.SaveCanceled = True
    ''                Exit Function
    ''            End If
    ''        End If

    ''    End If

    ''    If Not CheckStatus() Then
    ''        UIMaster.RUICenter.SaveCanceled = True
    ''        Exit Function
    ''    End If

    ''    vntNewRecId = objForm.DoAddFormData(vntRecordsets, vntParameters)

    ''    Call SendEmailForNewEmployee(rstPrimary)

    ''    AddFormData = vntNewRecId
    ''End Function

    ''Function CheckStatus()
    ''    Dim strMessage
    ''    Dim rstPrimary

    ''    strMessage = "Save failed." & vbCrLf & "The field 'Initials' in the segment 'Employee HRM' is a required field. Please enter a valid value."
    ''    rstPrimary = UIMaster.RUICenter.PrimaryRecordset

    ''    If strSOLLICITANT = rstPrimary.Fields(strfSTATUS).Value Then
    ''        CheckStatus = True
    ''    Else
    ''        If IsNull(rstPrimary.Fields("Initials").Value) Then
    ''            Global.CMSMsgBox(strMessage, vbOKOnly, "Error")
    ''            UIMaster.RUICenter.FocusFieldEx("General", "Employee HRM", "Initials")
    ''            CheckStatus = False
    ''        Else
    ''            CheckStatus = True
    ''        End If
    ''    End If
    ''End Function

    ''Sub SendEmailForNewEmployee(ByVal rstPrimary)
    ''    Dim objEmail
    ''    Dim strBody
    ''    Dim strFullName
    ''    Dim strMiddleName
    ''    Dim strStartDate
    ''    Dim strWorkEmail
    ''    Dim strEmpNumber
    ''    Dim strLogin
    ''    Dim strPassw

    ''    On Error Resume Next

    ''    If rstPrimary.Fields(strfSTATUS).Value = strSOLLICITANT Or rstPrimary.Fields(strfSTATUS).Value = strPROJECTPOOL Then Exit Sub

    ''    strBody = "Detailed information:" + vbCrLf + _
    ''              "Full name: %1" + vbCrLf + _
    ''              "Division: %2" + vbCrLf + _
    ''              "Territory: %3" + vbCrLf + _
    ''              "Job Title: %4" + vbCrLf + _
    ''              "Date in service: %5" + vbCrLf + _
    ''              "E-Mail: %6" + vbCrLf + _
    ''              "Employee Nummer: %7" + vbCrLf + _
    ''              "Username: %8" + vbCrLf + _
    ''              "Password: %9" + vbCrLf + vbCrLf + vbCrLf + _
    ''              "This message was sent by Pivotal Relationship."


    ''    If IsNull(rstPrimary.Fields(strfMIDDLENAME).Value) Then
    ''        strMiddleName = ""
    ''    Else
    ''        strMiddleName = rstPrimary.Fields(strfMIDDLENAME).Value
    ''    End If

    ''    strFullName = rstPrimary.Fields(strfFIRSTNAME).Value + " " + strMiddleName + " " + rstPrimary.Fields(strfLASTNAME).Value

    ''    If IsNull(rstPrimary.Fields(strfSTARTDATE).Value) Then
    ''        strStartDate = ""
    ''    Else
    ''        strStartDate = rstPrimary.Fields(strfSTARTDATE).Value
    ''    End If


    ''    If IsNull(rstPrimary.Fields(strfWORKEMAIL).Value) Then
    ''        strWorkEmail = ""
    ''    Else
    ''        strWorkEmail = rstPrimary.Fields(strfWORKEMAIL).Value
    ''    End If


    ''    If IsNull(rstPrimary.Fields(strfEMPNUMMER).Value) Then
    ''        strEmpNumber = ""
    ''    Else
    ''        strEmpNumber = rstPrimary.Fields(strfEMPNUMMER).Value
    ''    End If


    ''    If IsNull(rstPrimary.Fields(strfUSERNAME).Value) Then
    ''        strLogin = ""
    ''    Else
    ''        strLogin = rstPrimary.Fields(strfUSERNAME).Value
    ''    End If


    ''    If IsNull(rstPrimary.Fields(strfPASSWORD).Value) Then
    ''        strPassw = ""
    ''    Else
    ''        strPassw = rstPrimary.Fields(strfPASSWORD).Value
    ''    End If

    ''    strBody = Replace(strBody, "%1", strFullName)
    ''    strBody = Replace(strBody, "%2", Global.FindValue("QM_Division", "Division_Text", strfDIVISIONID, rstPrimary.Fields(strfDIVISIONID2).Value))
    ''    strBody = Replace(strBody, "%3", Global.FindValue("Territory", "Territory_Name", strfTERRITORYID, rstPrimary.Fields(strfTERRITORYID).Value))
    ''    strBody = Replace(strBody, "%4", Global.FindValue("QM_functietabel", "Functienaam", strfJOBTITLEID, rstPrimary.Fields(strfJOBTITLE).Value))
    ''    strBody = Replace(strBody, "%5", strStartDate)
    ''    strBody = Replace(strBody, "%6", strWorkEmail)
    ''    strBody = Replace(strBody, "%7", strEmpNumber)
    ''    strBody = Replace(strBody, "%8", strLogin)
    ''    strBody = Replace(strBody, "%9", strPassw)


    ''    objEmail = UIMaster.RSysClient.CreateEmail
    ''    objEmail.To = strEMAIL
    ''    objEmail.Subject = Replace(strSUBJ, "%1", strFullName)
    ''    objEmail.Body = strBody

    ''    objEmail.Send()

    ''    If Err.Number <> 0 Then
    ''        Global.CMSMsgBox("Failed to sent mail with contract changes of this employee. Please contact your Pivotal administrator.", vbOKOnly, "Error")
    ''        Err.Clear()
    ''    End If

    ''End Sub



    ''' -------------------------------------------------------------------------------------------
    '''     Name: CheckHistory  
    ''' Purpose: Checks and updates vacancies history for employee
    '''  
    ''' 
    ''' -------------------------------------------------------------------------------------------
    '''     Inputs: rstPrimary - primary recordset for employeerecord
    '''   
    '''  Returns:
    '''   
    ''' Implements Agent: 
    ''' History:
    ''' Reversion#    Date      Author           Description
    ''' --------------- -       -------   ---------  -----------------
    ''' 1.0        11/20/2003  VDI\AndreyF       Initial version
    ''' ------------------------------------------------------------------------------------------      

    ''Sub CheckHistory(ByVal rstPrimary)
    ''    Dim datStartDate
    ''    Dim datEndDate
    ''    Dim intYearStart
    ''    Dim intYearEnd
    ''    Dim intYearsBetween
    ''    Dim intRestVorigJaar
    ''    Dim intTempYear
    ''    Dim datTempStart
    ''    Dim datTempEnd
    ''    Dim intStartDay
    ''    Dim intEndDay
    ''    Dim dblVacationHours
    ''    Dim vntParams

    ''    ' Start date should be filled in
    ''    If (IsNull(rstPrimary.Fields(strfSTARTDATE).Value)) And (rstPrimary.Fields(strfSTATUS).Value = "In dienst") Then
    ''        Call Global.CMSMsgBox("Please, first fill in a start date", vbOKOnly, "Error")

    ''        Exit Sub
    ''    End If

    ''    ' Check if end date isNull or not
    ''    If IsNull(rstPrimary.Fields(strfENDDATE).Value) Then
    ''   datEndDate = CDate("12/31/" + CStr(Year(Date)))
    ''    Else
    ''   if Year(rstPrimary.Fields(strfENDDATE).Value) > Year(Date) then
    ''      datEndDate = CDate("12/31/" + CStr(Year(Date)))
    ''        Else
    ''            datEndDate = rstPrimary.Fields(strfENDDATE).Value
    ''        End If
    ''    End If

    ''    datStartDate = rstPrimary.Fields(strfSTARTDATE).Value

    ''    intYearStart = Year(datStartDate)
    ''    intYearEnd = Year(datEndDate)

    ''    intYearsBetween = intYearEnd - intYearStart
    ''    intRestVorigJaar = 0

    ''    intTempYear = intYearStart
    ''    datTempStart = datStartDate
    ''    datTempEnd = datEndDate

    ''    While intTempYear <= intYearEnd
    ''        If Year(datTempStart) < intTempYear Then
    ''            intStartDay = 0
    ''            datTempStart = CDate("1/1/" + CStr(intTempYear))
    ''        Else
    ''            intStartDay = DateDiff("y", CDate("1/1/" + CStr(Year(datTempStart))), datTempStart)
    ''        End If


    ''        If Year(datTempEnd) > intTempYear Then
    ''            intEndDay = 365
    ''            datTempEnd = CDate("1/1/" + CStr(intTempYear + 1))
    ''        Else
    ''            intEndDay = DateDiff("y", CDate("1/1/" + CStr(Year(datTempEnd))), datTempEnd)
    ''        End If


    ''        dblVacationHours = Round(((intEndDay - intStartDay) / 365) * 200)

    ''        vntParams = Array(UIMaster.RUICenter.RecordId, datTempStart, datTempEnd, intTempYear, dblVacationHours, intRestVorigJaar)
    ''        UIMaster.RUICenter.Form.Execute("UpdateHoursHistory", vntParams)

    ''        intRestVorigJaar = vntParams(6)

    ''        intTempYear = intTempYear + 1
    ''        datTempStart = datStartDate
    ''        datTempEnd = datEndDate

    ''        dblVacationHours = 0
    ''    End While
    ''End Sub

    Function OnSecondaryAddClick()
        On Error Resume Next

        Dim objEvent
        Dim objParams
        Dim vntParams
        Dim rs

        Stop

        OnSecondaryAddClick = False
        objEvent = UIMaster.RUICenter.FormEventObj     'TODO: prepend with set in script

        Select Case objEvent.SegmentName
            Case "Actions"
                objParams = Global.CreateTransitPointParamsObj()
                objParams.SetUserDefParam(1, UIMaster.RUICenter.PrimaryRecordset.fields("Employee_Id").value)
                vntParams = objParams.ConstructParams()
                UIMaster.ShowForm(RDAUILib.ActionTypeEnum.actionAskUser, "Start", System.DBNull.Value, vntParams) 'TODO: DBNull->Null; unqualify enum
                OnSecondaryAddClick = True
            Case "Hours History"
                vntParams = UIMaster.ShowFormByTableModal("QM_vakantiehistorie", System.DBNull.Value) 'TODO: DBNull->Null
                rs = vntParams(0) 'TODO: prepend with set in script

                Call UIMaster.RUICenter.Form.Segments("Hours History").SetLinkValue(rs.Fields("QM_vakantiehistorie_id"), rs)
                OnSecondaryAddClick = True
        End Select

        If Err.Number <> 0 Then
            UIMaster.ShowErrorMessage(Err.Description)
        End If
    End Function

End Module
