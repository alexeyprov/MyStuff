'This module along with 
'   TransitPointParams
'   CustomDialogBox
'   CMSErrorObject
'make up analog of Global Active Access Script

'***************************************************************
'* VBScript -> VB.NET change info:
'*  Err -> Err.Number when comparing with integers
'*  Number of array dimension has been added to array definitions
'*  Following variables were added due to absense of 'Option Explicit':
'*      strFormName in MapActivityTypeToFormName
'*      rstPrimary in 
'*  Full name qualifications were made to following enums:
'*      RDAUILib.ActionTypeEnum in CSSFullTextSearch
'*      RDAUILib.ModelessMenuAppearanceEnum in ShowProjectAgreement
'*  Set obj = value -> obj = value
'***************************************************************

Module Global
    Public UIMaster As RDAUILib.IRUIMaster7
    Public window As mshtml.IHTMLWindow4

    Public Function CreateTransitPointParamsObj() As TransitPointParams
        Return New TransitPointParams
    End Function

    'Name: CreateCustomDialogBox
    'Purpose: Factory function for the CustomDialogBox class.
    '
    Function CreateCustomDialogBox()
        CreateCustomDialogBox = New CustomDialogBox
    End Function

    Function CMSDialog(ByVal strHTMLText, ByVal strTitle, ByVal strBtnTexts)
        Dim rdlgParam
        Dim lngRet
        Dim strReturn

        rdlgParam = UIMaster.CreateDialogParam
        With rdlgParam
            If IsArray(strBtnTexts) = True Then
                For lngRet = 0 To UBound(strBtnTexts)
                    .AddButton(strBtnTexts(lngRet))
                Next
            Else
                .AddButton(strBtnTexts)
            End If
            '.AddTitleText strTitle
            .AddSection(" ", strHTMLText)
        End With

        strReturn = UIMaster.ShowDialogModal(rdlgParam, strTitle)

        If IsArray(strBtnTexts) = True Then
            For lngRet = 0 To UBound(strBtnTexts)
                If strReturn = strBtnTexts(lngRet) Then
                    CMSDialog = lngRet + 1
                    Exit Function
                End If
                CMSDialog = -1
            Next
        Else
            If strReturn = strBtnTexts Then
                CMSDialog = 1
            Else
                CMSDialog = -1
            End If
        End If

    End Function


    ' -------------------------------------------------------------------------------------------
    ' Name :     CMSMsgBox
    ' Purpose:  This function to show a modal msgbox that has the same UI as dialog form provided
    '            by UIMaster. However, it has almost same interface as normal Msgbox function
    '            supported by VBScript
    ' -------------------------------------------------------------------------------------------
    ' Inputs:
    '   strPrompt  : The string expression displayed as the message in the dialog box.
    '   strTitle   : String expression displayed in the title bar of the dialog box.
    '   intButtons : The integer specifying the number and type of buttons to display. They are
    '                the same VB constant used in VBScript.
    '                Now this function supports following selections:
    '                   vbOKOnly           '0 Display OK button only.
    '                   vbOKCancel         '1 Display OK and Cancel buttons.
    '                   vbAbortRetryIgnore '2 Display Abort, Retry, and Ignore buttons.
    '                   vbYesNoCancel      '3 Display Yes, No, and Cancel buttons.
    '                   vbYesNo            '4 Display Yes and No buttons.
    '                   vbRetryCancel      '5 Display Retry and Cancel buttons.
    ' Returns:
    '   CMSMsgBox  : The MsgBox function has the following return values:
    '                   vbOK
    '                   vbCancel
    '                   vbYes
    '                   vbNo
    '                   vbAbort
    '                   vbRetry
    '                   vbIgnore
    ' History:
    ' Reversion#    Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 1.0           01/12/2000  DY      Initial version
    ' ------------------------------------------------------------------------------------------
    Function CMSMsgBox(ByVal strPrompt, ByVal intButtons, ByVal strTitle)
        Const strOK = "OK"
        Const strCANCEL = "Cancel"
        Const strABORT = "Abort"
        Const strRETRY = "Retry"
        Const strIGNORE = "Ignore"
        Const strYES = "Yes"
        Const strNO = "No"

        Dim rdlgParam
        Dim rdltLangDict
        Dim strBtnAbort
        Dim strBtnCancel
        Dim strBtnIgnore
        Dim strBtnOK
        Dim strBtnNo
        Dim strBtnRetry
        Dim strBtnYes
        Dim strReturn

        On Error Resume Next
        rdltLangDict = UIMaster.RSysClient.GetLDGroup("Common")

        If Err.Number <> 0 Then
            strBtnOK = strOK
            strBtnAbort = strABORT
            strBtnCancel = strCANCEL
            strBtnIgnore = strIGNORE
            strBtnNo = strNO
            strBtnRetry = strRETRY
            strBtnYes = strYES
            Err.Clear()
        Else
            With rdltLangDict
                strBtnOK = .GetText(strOK)
                strBtnAbort = .GetText(strABORT)
                strBtnCancel = .GetText(strCANCEL)
                strBtnIgnore = .GetText(strIGNORE)
                strBtnNo = .GetText(strNO)
                strBtnRetry = .GetText(strRETRY)
                strBtnYes = .GetText(strYES)
            End With
        End If

        rdlgParam = UIMaster.CreateDialogParam
        With rdlgParam
            Select Case intButtons
                Case vbOKOnly           '0 Display OK button only.
                    .AddButton(strBtnOK)
                Case vbOKCancel         '1 Display OK and Cancel buttons.
                    .AddButton(strBtnOK)
                    .AddButton(strBtnCancel)
                Case vbAbortRetryIgnore '2 Display Abort, Retry, and Ignore buttons.
                    .AddButton(strBtnAbort)
                    .AddButton(strBtnRetry)
                    .AddButton(strBtnIgnore)
                Case vbYesNoCancel      '3 Display Yes, No, and Cancel buttons.
                    .AddButton(strBtnYes)
                    .AddButton(strBtnNo)
                    .AddButton(strBtnCancel)
                Case vbYesNo            '4 Display Yes and No buttons.
                    .AddButton(strBtnYes)
                    .AddButton(strBtnNo)
                Case vbRetryCancel      '5 Display Retry and Cancel buttons.
                    .AddButton(strBtnRetry)
                    .AddButton(strBtnCancel)

                Case Else
                    .AddButton(strBtnOK)

            End Select
            '.AddTitleText strTitle
            .AddSection("", strPrompt) '.SetHTMLText 0, strPrompt

        End With

        strReturn = UIMaster.ShowDialogModal(rdlgParam, strTitle)
        rdlgParam = Nothing

        Select Case strReturn
            Case strBtnOK
                CMSMsgBox = vbOK
            Case strBtnCancel
                CMSMsgBox = vbCancel
            Case strBtnYes
                CMSMsgBox = vbYes
            Case strBtnNo
                CMSMsgBox = vbNo
            Case strBtnAbort
                CMSMsgBox = vbAbort
            Case strBtnRetry
                CMSMsgBox = vbRetry
            Case strBtnIgnore
                CMSMsgBox = vbIgnore
            Case Else
                CMSMsgBox = vbCancel
        End Select

    End Function


    ' ------------------------------------------------------------------------------------------
    ' Name    : NewActivity
    ' Purpose : This client script opens a new activity form used selected with the link
    '           (contact, company, opportunity, lead, marketing project, etc) information filled in.
    ' ------------------------------------------------------------------------------------------
    ' Inputs:
    '   blnShowModal : The flag to indicate if show form modal or modeless. The passed value
    '                  True - show modal, false - show modeless
    '   vntContactId : The Contact record Id linking to this new activity, if you do not have
    '                  this record id , just pass Null
    '   vntLeadId    : The Lead record Id linking to this new activity, if you do not have
    '                  this record id , just pass Null
    '   vntCompanyId : The Company record Id linking to this new activity, if you do not have
    '                  this record id , just pass Null
    '   vntOpportunityId : The Opportunity record Id linking to this new activity, if you do not
    '                      have this record id , just pass Null
    '   vntMarketingProject : The Marketing Project record Id linking to this new activity, if
    '                         you do not have this record id , just pass Null
    ' Returns:
    '   NewActivity : True - Open a new Activity, False - Cancel
    ' History:
    ' Revision#     Date        Author   Note
    ' 1.0           01/12/2000  DY       Initial version
    ' ------------------------------------------------------------------------------------------
    Function NewActivity(ByVal blnShowModal, ByVal vntContactId, ByVal vntLeadId, ByVal vntCompanyId, ByVal vntOpportunityId, ByVal vntMarketingProject)
        Const strfCOMPANY = "Company"
        Const strfCONTACT = "Contact"
        Const strfLEAD_ID = "Lead_Id"
        Const strfMARKETING_PROJECT = "Marketing_Project"
        Const strfOPPORTUNITY = "Opportunity"
        Const strfACTIVITY_TYPE = "Activity_Type"

        Const strrMEETING = "Meeting"
        Const strrTO_DO = "To-Do"
        Const strrCALL = "Call"
        Const strrMESSAGE = "Message"
        Const strrLITERATURE = "Literature Fulfillment"
        Const strrNOTE = "Note"
        Const strrVISIT = "Visit Report"
        Const strrCOMPL = "To-do Complaint"

        Const lngAPP_TYPE_MEETING = 0
        Const lngAPP_TYPE_TO_DO = 1
        Const lngAPP_TYPE_CALL = 2
        Const lngAPP_TYPE_MESSAGE = 3
        Const lngAPP_TYPE_LITERATURE = 4
        Const lngAPP_TYPE_NOTE = 5
        Const lngAPP_TYPE_VISIT = 6
        Const lngAPP_TYPE_COMPL = 7
        Const lngAPP_TYPE_CANCEL = -1

        Const strdSELECT_APP_TYPE = "Select Activity Type"

        Dim intActivityType
        Dim vntRecordsets
        Dim vntParameters
        Dim strFormName
        Dim strBtnTexts
        Dim intAppTypes
        Dim strHTMLText
        Dim strTitle
        Dim objParam
        Dim vntReturns

        On Error Resume Next

        ' Get activity type from user
        '' TO DO Pop up Option Dialog to alolow the user to select activity type
        With UIMaster.RSysClient.GetLDGroup("Activity Management")
            strHTMLText = .GetText(strdSELECT_APP_TYPE)
            strTitle = .GetText("New Activity")
            strBtnTexts = Array(.GetText("Call Button"), .GetText("Literature Fulfillment Button"), _
                .GetText("Meeting Button"), .GetText("Message Button"), .GetText("Note Button"), _
                .GetText("To-Do Button"), "Visit Report", "Complaint")
            intAppTypes = Array(lngAPP_TYPE_CALL, lngAPP_TYPE_LITERATURE, lngAPP_TYPE_MEETING, _
                lngAPP_TYPE_MESSAGE, lngAPP_TYPE_NOTE, lngAPP_TYPE_TO_DO, lngAPP_TYPE_VISIT, lngAPP_TYPE_COMPL)
        End With

        ' Get user choice button
        intActivityType = CMSDialogRadio(strHTMLText, strTitle, strBtnTexts) - 1
        If intActivityType < 0 Then Exit Function
        ' Transfer to the real activity type
        intActivityType = intAppTypes(intActivityType)

        Select Case intActivityType
            Case lngAPP_TYPE_MEETING
                strFormName = strrMEETING
            Case lngAPP_TYPE_TO_DO
                strFormName = strrTO_DO
            Case lngAPP_TYPE_CALL
                strFormName = strrCALL
            Case lngAPP_TYPE_MESSAGE
                strFormName = strrMESSAGE
            Case lngAPP_TYPE_LITERATURE
                strFormName = strrLITERATURE
            Case lngAPP_TYPE_NOTE
                strFormName = strrNOTE
            Case lngAPP_TYPE_VISIT
                strFormName = strrVISIT
            Case lngAPP_TYPE_COMPL
                strFormName = strrCOMPL
            Case Else
                Exit Function
        End Select
        ' Organize transition point paramer list
        objParam = CreateTransitPointParamsObj()
        With objParam
            .AddDefaultField(strfLEAD_ID, vntLeadId)
            .AddDefaultField(strfCOMPANY, vntCompanyId)
            .AddDefaultField(strfCONTACT, vntContactId)
            .AddDefaultField(strfOPPORTUNITY, vntOpportunityId)
            .AddDefaultField(strfMARKETING_PROJECT, vntMarketingProject)
            .AddDefaultField(strfACTIVITY_TYPE, intActivityType)
            vntParameters = .ConstructParams()
        End With

        'actionAskUser
        If blnShowModal = True Then
            vntReturns = UIMaster.ShowFormModal(strFormName, Null, vntParameters)
            NewActivity = (vntReturns(0) = 1)
        Else
            UIMaster.ShowForm(2, strFormName, Null, vntParameters)
            NewActivity = True
        End If


    End Function

    ' -----------------------------------------------------------------------------------------------------------------
    ' EqualValues
    ' Purpose:  Compare two values to see if they are equal
    ' ------------------------------------------------------------------------------------------
    ' Inputs:
    '   vntValue1 : The one of the values to be compared
    '   vntValue2 : The one of the values to be compared
    ' Returns:
    '   EqualValues ; True - Equal, False - not equal
    ' Revision#     Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 1.0           01/25/2000  DY      Initial version
    ' ------------------------------------------------------------------------------------------
    Function EqualValues(ByVal vntValue1, ByVal vntValue2)
        On Error Resume Next

        If IsNull(vntValue1) And IsNull(vntValue2) Then
            EqualValues = True
        ElseIf IsNull(vntValue1) And Not IsNull(vntValue2) Then
            EqualValues = False
        ElseIf Not IsNull(vntValue1) And IsNull(vntValue2) Then
            EqualValues = False
        ElseIf IsArray(vntValue1) Then
            EqualValues = UIMaster.RSysClient.EqualIds(vntValue1, vntValue2)
        Else
            EqualValues = (vntValue1 = vntValue2)
        End If

    End Function


    ' -------------------------------------------------------------------------------------------
    ' Name :    ApplyActionPlan
    ' Purpose:  This client script sets up the necessary information and passes it to the action
    '           plan agent to create activities.  It is attachted to the Action Plan button.
    ' -------------------------------------------------------------------------------------------
    ' Inputs:
    '   strObject : The object Name
    '   vntPartnerId : The partner record Id:
    '                      "Partner_Company_Id"      for Company, contact
    '                      "Reseller_Id"             for Opportunity
    '                      Null                      for Lead
    '                      "Company_Id"              for Marketing Project
    '   vntPartnerContactId : The partner contact record Id:
    '                      "Partner_Contact_Id"      for Company, contact
    '                      "Partner_Contact_Id"      for Opportunity
    '                      Null                      for Lead
    '                      "Contact_Id"              for Marketing Project
    '   vntManagerId : The account manager Id :
    '                      "Account_Manager_Id"      for Company, contact, opportunity
    '                      "Project_Manager_Id"      for marketing Project
    '                      Null                      for Lead
    '   vntParameters : The transit point patameter list contains default fields for opening
    '                   Action Plan Links form
    ' Returns:
    '   ApplyActionPlan : True - Successful ,  False - Cancel or failed
    ' Implements Agent: Sys\Form\Company\Action Plan Stub, Sys\Form\Contact\Action Plan Stub,
    '                   Sys\Form\Lead\Action Plan Stub, Sys\Form\Opportunity\Action Plan Stub,
    '                   Sys\Form\Marketing Project\Action Plan Stub
    ' History:
    ' Reversion#    Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 1.0           02/01/2000  DY      Initial version
    ' ------------------------------------------------------------------------------------------
    Function ApplyActionPlan(ByVal strObject, ByVal vntPartnerId, ByVal vntPartnerContactId, ByVal vntManagerId, ByVal vntParameters)
        Const strfCOMPANY_ID = "Company_Id"
        Const strfCOMPANY = "Company"
        Const strfAPPT_DATE = "Appt_Date"
        Const strfACTION_PLAN_ID = "Action_Plan_Id"
        Const strfPARTNER_COMPANY_ID = "Partner_Company_Id"
        Const strfPARTNER_CONTACT_ID = "Partner_Contact_Id"
        Const strfRN_APPOINTMENTS_ID = "Rn_Appointments_Id"

        Const strrACTION_PLAN = "Action Plan"
        Const strrACTION_PLAN_LINKS = "Action Plan Links"
        Const strrMEETING = "Meeting"

        Const strmFIND_ACTION_PLANS = "FindActionPlans"
        Const strmUSE_ACTION_PLAN = "UseActionPlan"
        Const strmCHECK_ACTION_PLAN_STEP = "CheckActionPlanStep"

        Const strgACTION_PLAN = "Action Plan"
        Const strdERROR = "Error"
        Const strdNO_ACTION_PLANS_FOUND = "No Action Plans Found"

        Dim rfrmForm
        Dim rstRecordset
        Dim vntfAction_Plan_Id
        Dim vntfActivity_Id
        Dim vntArguments
        Dim rdltLangDict
        Dim vntSelectId
        Dim objParam
        Dim strMsg
        Dim vntMeetingIds
        Dim vntTableId
        Dim vntRecordsets
        Dim intParam

        ApplyActionPlan = False
        On Error Resume Next
        With UIMaster.RSysClient
            rfrmForm = .GetForm(strrACTION_PLAN)
            rdltLangDict = .GetLDGroup(strgACTION_PLAN)
        End With

        ' Create transition point paramer object
        objParam = CreateTransitPointParamsObj()

        ' Step 1 : Get relationship action plan recordset
        objParam.SetUserDefParam(1, strObject)
        vntArguments = objParam.ConstructParams()
        rfrmForm.Execute(strmFIND_ACTION_PLANS, vntArguments)
        If Err.Number <> 0 Then
            UIMaster.ShowErrorMessage(Err.Description)
            Err.Clear()
            Exit Function
        End If
        rstRecordset = objParam.GetUserDefParam(1, vntArguments)
        vntTableId = objParam.GetUserDefParam(2, vntArguments)

        ' Step 2 : Waiting user to choose
        ' Show List to wait user choose one
        If rstRecordset.RecordCount = 0 Then
            CMSMsgBox(rdltLangDict.GetText(strdNO_ACTION_PLANS_FOUND), vbOKOnly, "")
            Exit Function
        End If
        vntSelectId = UIMaster.ShowSelectionListModal(vntTableId, rstRecordset)
        If UBound(vntSelectId) < 0 Then Exit Function
        vntfAction_Plan_Id = vntSelectId(0)

        ' Step 3: Check action plan step
        If UCase(strObject) <> "LEAD" Then
            objParam.Clear()
            objParam.SetUserDefParam(1, strObject)
            objParam.SetUserDefParam(2, vntfAction_Plan_Id)
            objParam.SetUserDefParam(3, vntPartnerId)
            objParam.SetUserDefParam(4, vntPartnerContactId)
            vntArguments = objParam.ConstructParams()
            rfrmForm.Execute(strmCHECK_ACTION_PLAN_STEP, vntArguments)
            strMsg = objParam.GetUserDefParam(1, vntArguments)
            If Len(strMsg) > 0 Then
                CMSMsgBox(strMsg, vbOKOnly, rdltLangDict.GetText(strdERROR))
                Exit Function
            End If
        End If

        ' Step 4 : Open Activity link form
        objParam.Clear()
        vntRecordsets = UIMaster.ShowFormModal(strrACTION_PLAN_LINKS, Null, vntParameters)
        ' If cancel or save failed, by pass the following steps
        If vntRecordsets(0) = 0 Then Exit Function
        vntfActivity_Id = vntRecordsets(1) '.Fields(strfRN_APPOINTMENTS_ID).Value
        If IsNull(vntfActivity_Id) Or IsEmpty(vntfActivity_Id) Then Exit Function
        Err.Clear()

        ' Call Use Action Plan
        objParam.Clear()
        objParam.SetUserDefParam(1, vntfActivity_Id)
        objParam.SetUserDefParam(2, vntfAction_Plan_Id)
        objParam.SetUserDefParam(3, vntPartnerContactId)
        objParam.SetUserDefParam(4, vntManagerId)
        vntParameters = objParam.ConstructParams
        'vntParameters = Array(vntfActivity_Id, vntfAction_Plan_Id)
        rfrmForm.Execute(strmUSE_ACTION_PLAN, vntParameters)
        If Err.Number <> 0 Then
            UIMaster.ShowErrorMessage(Err.Description)
            Exit Function
            Err.Clear()
        End If

        vntMeetingIds = objParam.GetUserDefParam(1, vntParameters)
        If IsArray(vntMeetingIds) And IsEmpty(vntMeetingIds) = False Then
            For intParam = 0 To UBound(vntMeetingIds)
                vntfActivity_Id = vntMeetingIds(intParam)
                vntArguments = Array(Empty)
                UIMaster.ShowFormModal(strrMEETING, vntfActivity_Id, vntArguments)
            Next
        End If
        ApplyActionPlan = True

    End Function


    ' -------------------------------------------------------------------------------------------
    ' Name :    CMSDialogRadio
    ' Purpose : This function is a Global client script to show a modal dialog form by use of radio
    '           button
    ' -------------------------------------------------------------------------------------------
    ' Inputs:
    '   strHTMLText : The message to describe the function of the dialog form
    '   strTitle    : The title of the dialog form
    '   strBtnTexts : A string array to hold the button text to be shown on the dialog form
    '                 NOTE: If you only want to add one button, you can pass a string instead of
    '                       a string array.
    ' Returns:
    '   CMSDialogRadio : The index start from 1 user selected indicate the order of the button
    '                 If the user click the close button on the upper-right of the form, the returned
    '                 will be -1.
    ' History:
    ' Reversion#    Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 1.0           02/15/2000  DY      Initial version
    ' ------------------------------------------------------------------------------------------
    Function CMSDialogRadio(ByVal strHTMLText, ByVal strTitle, ByVal strBtnTexts)
        Dim lngRet
        Dim strReturn
        Dim rfrmRadioButton
        Dim rfrmRadioButtons
        Dim rfrmRadioButtonMain
        Dim rfrmParamMain
        Dim rfrmRadioTitle

        rfrmRadioButtonMain = UIMaster.CreateFormParam()
        rfrmRadioButtons = UIMaster.CreateFormParams()
        rfrmParamMain = UIMaster.CreateFormParams()

        With rfrmParamMain
            rfrmRadioTitle = UIMaster.CreateFormParam()
            rfrmRadioTitle.Label = strHTMLText
            rfrmRadioTitle.Value = vbEmpty
            rfrmRadioTitle.Type = vbEmpty
            rfrmRadioTitle.Skip = True
            rfrmParamMain.AddItem(rfrmRadioTitle)

            If IsArray(strBtnTexts) = True Then
                For lngRet = 0 To UBound(strBtnTexts)
                    rfrmRadioButton = UIMaster.CreateFormParam()
                    rfrmRadioButton.Label = strBtnTexts(lngRet)
                    rfrmRadioButton.Value = vbEmpty
                    rfrmRadioButton.Type = vbEmpty
                    rfrmRadioButtons.AddItem(rfrmRadioButton)
                Next
            Else
                rfrmRadioButton = UIMaster.CreateFormParam()
                rfrmRadioButton.Label = strBtnTexts
                rfrmRadioButton.Value = vbEmpty
                rfrmRadioButton.Type = vbEmpty
                rfrmRadioButtons.AddItem(rfrmRadioButton)
            End If
            With rfrmRadioButtonMain
                .Label = ""
                .Value = 0
                .Type = 1024
                .Enums = rfrmRadioButtons
            End With
            .AddItem(rfrmRadioButtonMain)
        End With

        strReturn = UIMaster.ShowDialogModal(rfrmParamMain, strTitle)

        With rfrmRadioButtonMain
            If strReturn = "1" Or strReturn = "OK" Then
                If .Value >= 0 Then CMSDialogRadio = .Value + 1
            Else
                CMSDialogRadio = -1
            End If
        End With

    End Function

    '**********************************************************
    'Name: ExpandRequiredSegments
    'Purpose: Expands all primary segments on the form which have
    ' a required field.  Allows you to skip specified segments
    'Assumptions:
    'Effects: expands segments of the form
    'Inputs: rfrmForm - the form definition
    ' vntExceptions - segment name(s) that will not be expanded.
    ' vntExceptions can take a single string or an array of strings
    ' as input
    'Outputs:
    'Returns:
    'Implements Agent: None
    '
    'Revision # Date            Author  Description
    '---------- ----            ------  -----------
    '1.0        March 13, 2000 TS     Initial version
    '**********************************************************
    Sub ExpandRequiredSegments(ByVal rfrmForm, ByVal vntExceptions)

        Dim intNumSegments
        Dim intSegmentCounter
        Dim blnSkipSegment
        Dim intExceptionCounter
        Dim intNumFieldsInSegment
        Dim intFieldCounter

        If TypeName(vntExceptions) = "String" Then
            vntExceptions = Array(vntExceptions)
        End If
        If IsArray(vntExceptions) = False Then
            vntExceptions = Array("")
        End If
        blnSkipSegment = False
        With rfrmForm
            intNumSegments = .Segments.Count
            For intSegmentCounter = 0 To intNumSegments - 1
                With .Segments(intSegmentCounter)
                    If .IsSecondary = True Then
                        blnSkipSegment = True
                    End If
                    If .IsTitleHidden = True Then
                        blnSkipSegment = True
                    End If
                    If blnSkipSegment = False Then
                        For intExceptionCounter = 0 To UBound(vntExceptions)
                            If .SegmentName = vntExceptions(intExceptionCounter) Then
                                blnSkipSegment = True
                                Exit For
                            End If
                        Next
                        If blnSkipSegment = False Then
                            intNumFieldsInSegment = .FormFields.Count
                            For intFieldCounter = 0 To intNumFieldsInSegment - 1
                                If .FormFields.Item(CInt(intFieldCounter)).Required = True Then
                                    UIMaster.RUICenter.OpenSegment(.SegmentName)
                                    Exit For
                                End If
                            Next
                        Else
                            blnSkipSegment = False
                        End If
                    Else
                        blnSkipSegment = False
                    End If
                End With
            Next
        End With

    End Sub
    '**********************************************************
    'Name: ExpandTopSegments
    'Purpose: Expands the top N segments on the form.  Normally N
    ' should not exceed 6 for performance reasons.  Secondary
    ' segments that have no data will be skipped.
    'Assumptions:
    'Effects: expands segments of the form
    'Inputs: rfrmForm - the form definition
    ' intNumSegments - the requested number of segments to expand
    ' vntRecordsets - the data in the form
    'Outputs:
    'Returns:
    'Implements Agent: None
    '
    'Revision # Date            Author  Description
    '---------- ----            ------  -----------
    '1.0        March 13, 2000 TS     Initial version
    '**********************************************************
    Sub ExpandTopSegments(ByVal rfrmForm, ByVal intNumSegments, ByVal vntRecordsets)

        Dim intMaxSegments
        Dim intTotalSegments
        Dim intSegmentCounter
        Dim rstSecondary

        intNumSegments = CInt(intNumSegments)
        intTotalSegments = rfrmForm.Segments.Count
        If intTotalSegments > intNumSegments Then
            intMaxSegments = intNumSegments
        Else
            intMaxSegments = intTotalSegments
        End If
        If intMaxSegments > 6 Then
            intMaxSegments = 6
        End If
        For intSegmentCounter = 0 To intMaxSegments - 1
            With rfrmForm.Segments(intSegmentCounter)
                If .IsTitleHidden = False Then
                    If .IsSecondary = True Then
                        rstSecondary = rfrmForm.SecondaryFromVariantArray(vntRecordsets, .SegmentName)
                        If rstSecondary.BOF = True And rstSecondary.EOF = True Then
                            'No data, so don't open the secondary segment
                        Else
                            UIMaster.RUICenter.OpenSegment(rfrmForm.Segments(intSegmentCounter).SegmentName)
                        End If
                    Else
                        UIMaster.RUICenter.OpenSegment(rfrmForm.Segments(intSegmentCounter).SegmentName)
                    End If
                End If
            End With
        Next
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''from QM_Global_Functions
    Function IsNullID(ByVal vntID)
        Dim vntTmpID

        vntTmpID = UIMaster.RSysClient.StringToId("0x0000000000000000")
        IsNullID = UIMaster.RSysClient.EqualIds(vntTmpID, vntID)
    End Function

    ' -------------------------------------------------------------------------------------------
    '     Name: ChoiceForm   
    ' Purpose: Allows user to select active form for new record
    '  
    ' 
    ' -------------------------------------------------------------------------------------------
    '     Inputs: strTableName - table name
    '   
    '  Returns: returns selected active form name or empty string if user closed dialog w/o selection
    '   
    ' Implements Agent: 
    ' History:
    ' Reversion#    Date        Author           Description
    ' --------------- -         -------          -----------------
    ' 1.0          17/11/2003   VDI\AndreyF      Initial version
    ' ------------------------------------------------------------------------------------------
    Function ChoiceForm(ByVal strTableName)
        Dim vntParams
        Dim intChoice
        Dim vntFormNames
        Dim vntFormTitles
        Dim i
        Dim intCounter

        vntParams = strTableName

        UIMaster.RUICenter.Form.Execute("GetFormsForTable", vntParams)

        If Not IsNothing(vntParams) Then

            ReDim vntFormNames(((UBound(vntParams) + 1) / 2) - 1)
            ReDim vntFormTitles(((UBound(vntParams) + 1) / 2) - 1)
            intCounter = 0

            For i = 0 To UBound(vntParams) Step 2
                vntFormNames(intCounter) = vntParams(i + 1)
                vntFormTitles(intCounter) = vntParams(i)

                intCounter = intCounter + 1
            Next

            intChoice = CMSDialogRadio("Please, select active form to add new record.", "Employee Actions", vntFormTitles)

            If intChoice = -1 Then
                ChoiceForm = ""
            Else
                ChoiceForm = vntFormNames(intChoice - 1)
            End If
        Else
            CMSMsgBox("There are not forms available.", vbOKOnly, "Employee Actions")
            ChoiceForm = ""
        End If

    End Function


    ' -------------------------------------------------------------------------------------------
    '     Name:   
    ' Purpose: 
    '  
    ' 
    ' -------------------------------------------------------------------------------------------
    '     Inputs:
    '   
    '  Returns:
    '   
    ' Implements Agent: 
    ' History:
    ' Reversion#    Date    Author  Description
    ' --------------- -   -------   ---------  -----------------
    ' 1.0                                JT          Initial version
    ' ------------------------------------------------------------------------------------------  

    Function FindValue(ByVal strTableName, ByVal strReturnField, ByVal strSearchField, ByVal vntSearchValue)
        Dim tblTable
        Dim fldReturn
        Dim fldSearch

        tblTable = UIMaster.RSysClient.GetTable(strTableName)
        fldReturn = tblTable.Fields(strReturnField)
        fldSearch = tblTable.Fields(strSearchField)

        FindValue = fldReturn.FindValue(fldSearch, vntSearchValue)
    End Function


    ' -------------------------------------------------------------------------------------------
    '     Name:   
    ' Purpose: 
    '  
    ' 
    ' -------------------------------------------------------------------------------------------
    '     Inputs:
    '   
    '  Returns:
    '   
    ' Implements Agent: 
    ' History:
    ' Reversion#    Date    Author  Description
    ' --------------- -   -------   ---------  -----------------
    ' 1.0                                JT          Initial version
    ' ------------------------------------------------------------------------------------------   

    Sub MaxDate()
        Dim vntParams

        vntParams = UIMaster.RUICenter.PrimaryRecordset

        UIMaster.RUICenter.Form.Execute("SetMaxDate", vntParams)
    End Sub


    ' -------------------------------------------------------------------------------------------
    '     Name:   
    ' Purpose: 
    '  
    ' 
    ' -------------------------------------------------------------------------------------------
    '     Inputs:
    '   
    '  Returns:
    '   
    ' Implements Agent: 
    ' History:
    ' Reversion#    Date    Author  Description
    ' --------------- -   -------   ---------  -----------------
    ' 1.0                                JT          Initial version
    ' ------------------------------------------------------------------------------------------

    Sub ShowWarningMsg(ByVal strParam1, ByVal strParam2)
        Const strMSGTITLE1 = "Let op!"

        Dim strMessage
        Dim rstPrimary

        strMessage = "De wijze van vervoer ingevuld bij de medewerker en het project komen niet overeen." + vbCrLf + vbCrLf + _
                     "De vervoerswijze bij de medewerker staat nu op: %1" + vbCrLf + _
                     "De vervoerswijze bij het project staat nu op: %2" + vbCrLf + vbCrLf + _
                     "Pivotal zal nu automatisch de vervoerswijze van de medewerker aanpassen naar: %2" + vbCrLf + vbCrLf + _
                     "LET OP: Aanpassing van de vervoerswijze kan consequenties hebben voor het tarief van de medewerker."

        rstPrimary = UIMaster.RUICenter.PrimaryRecordset

        strMessage = Replace(strMessage, "%1", strParam1)
        strMessage = Replace(strMessage, "%2", strParam2)

        Call CMSMsgBox(strMessage, vbOKOnly, strMSGTITLE1)
    End Sub


    ' -------------------------------------------------------------------------------------------
    '     Name:   
    ' Purpose: 
    '  
    ' 
    ' -------------------------------------------------------------------------------------------
    '     Inputs:
    '   
    '  Returns:
    '   
    ' Implements Agent: 
    ' History:
    ' Reversion#    Date    Author  Description
    ' --------------- -   -------   ---------  -----------------
    ' 1.0                                JT          Initial version
    ' ------------------------------------------------------------------------------------------             

    Sub ShowProjectAgreement()
        Const strfDELTAENDDATE = "delta_end_date"
        Const strfENDDATE = "End_Date_"
        Const strMESSAGE1 = "Project overeenkomst printen?"
        Const strMESSAGE2 = "Project wijziging printen?"
        Const strJA = "Ja"
        Const strNEE = "Nee"
        Const strTITLE = "Print"

        Const strLE1 = "SALES - Project Agreement NL"
        Const strLE2 = "SALES - Project Agreement Modified NL"

        Dim rstPrimary
        Dim intChoice
        Dim strLEToShow
        Dim objLEOptions
        Dim objWndDef

        rstPrimary = UIMaster.RUICenter.PrimaryRecordset

        If rstPrimary.Fields(strfDELTAENDDATE).Value <> rstPrimary.Fields(strfENDDATE).Value Then
            If CMSDialog(strMESSAGE2, strTITLE, New Object() {strJA, strNEE}) = 1 Then
                strLEToShow = strLE2
            Else
                Exit Sub
            End If
        Else
            If CMSDialog(strMESSAGE1, strTITLE, New Object() {strJA, strNEE}) = 1 Then
                strLEToShow = strLE1
            Else
                Exit Sub
            End If
        End If

        objWndDef = UIMaster.CreateModelessWindowDef
        objWndDef.Width = 600
        objWndDef.Height = 400
        objWndDef.menuAppearance = RDAUILib.ModelessMenuAppearanceEnum.modelessMenuNone

        Dim objParams
        Dim vntParams

        objParams = CreateTransitPointParamsObj()
        objParams.SetUserDefParam(1, UIMaster.RUICenter.RecordId)
        objParams.SetUserDefParam(2, strLEToShow)

        vntParams = objParams.ConstructParams()

        Call UIMaster.ShowFormModeless("QM_ProjectDetails_LE", UIMaster.RUICenter.RecordId, objWndDef, vntParams)

    End Sub

    Sub OnMainWindowOpen()
        Dim EmployeeID
        Dim AgentName
        Dim QMApplicant
        Dim rduModelessWindowDef
        EmployeeID = FindValue("Employee", "Employee_Id", "Rn_Employee_User_Id", UIMaster.RSysClient.UserId)
        If IsDBNull(EmployeeID) Then
            Exit Sub
        Else
            rduModelessWindowDef = UIMaster.CreateModelessWindowDef
            With rduModelessWindowDef
                .menuAppearance = 1     ' 1= modelessMenuNone
                .Size = 2               ' 2 = modelessSizeSpecific
                .position = 1           ' 1= modelessPositionTopCenter
                .Height = CLng(0.75 * window.Screen.availHeight)
                .Width = CLng(0.75 * window.Screen.availWidth)
            End With
            AgentName = FindValue("Employee", "Login_Script", "Employee_Id", EmployeeID)
            If AgentName = "Today's Activities" Then
                'UIMaster.ShowFormModal "Today's Activity List", EmployeeID, Null
                UIMaster.ShowFormModeless("Today's Activity List", EmployeeID, rduModelessWindowDef)
            ElseIf AgentName = "Applicants main" Then
                QMApplicant = FindValue("Employee", "QM_Applicant_main_fk", "Employee_Id", EmployeeID)
                'UIMaster.ShowFormModal "QM Applicants Main", QMApplicant, Null
                UIMaster.ShowFormModeless("QM Applicants Main", QMApplicant, rduModelessWindowDef)
            End If
        End If
    End Sub

    ' -------------------------------------------------------------------------------------------
    '     Name: QM_Employee_Visit ClientRep   
    ' Purpose: Show two additional tabs in case a comany visit report is also needed
    '           or hide them in case it's not.
    ' 
    ' -------------------------------------------------------------------------------------------
    ' History:
    ' Reversion#    Date    Author  Description
    ' --------------- -   -------   ---------  -----------------
    ' 1.0                 VDI\RuslanKu               JT          Initial version
    ' ------------------------------------------------------------------------------------------
    ' Declare vars

    Sub AddOrRemoveCompanyVisitReportToQMEmployeeVisitForm()
        ' Init
        Dim objPrimRS
        objPrimRS = UIMaster.RUICenter.PrimaryRecordset

        ' Check if we have to show an additional report (Company Visit)
        If objPrimRS.Fields("Also_Conv_With_Client").Value = True Then
            ' Show tabs
            UIMaster.RUICenter.ShowTab(1)
            UIMaster.RUICenter.ShowTab(2)
            ' Set required fields
            UIMaster.RUICenter.SetFieldRequiredEx("Note", "Notes", "App_Appt_Description", True)
            UIMaster.RUICenter.SetFieldRequiredEx("Note", "Visit Report Properties", "App_Appt_Priority", True)
            UIMaster.RUICenter.SetFieldRequiredEx("Note", "Visit Report Properties", "App_Access_Type", True)
            UIMaster.RUICenter.SetFieldRequiredEx("Note", "Notes", "App_Visit_Type", True)
            UIMaster.RUICenter.SetFieldRequiredEx("Details", "Date", "App_Appt_Date", True)
        ElseIf objPrimRS.Fields("Also_Conv_With_Client").Value = False Then
            ' Hide tabs
            UIMaster.RUICenter.HideTab(1)
            UIMaster.RUICenter.HideTab(2)
            ' Make required fields not required to prevent errors when saving
            UIMaster.RUICenter.SetFieldRequiredEx("Note", "Notes", "App_Appt_Description", False)
            UIMaster.RUICenter.SetFieldRequiredEx("Note", "Visit Report Properties", "App_Appt_Priority", False)
            UIMaster.RUICenter.SetFieldRequiredEx("Note", "Visit Report Properties", "App_Access_Type", False)
            UIMaster.RUICenter.SetFieldRequiredEx("Note", "Notes", "App_Visit_Type", False)
            UIMaster.RUICenter.SetFieldRequiredEx("Details", "Date", "App_Appt_Date", False)
        End If

    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''from QM_Global_EmployeeActions

    Const strfACTIONTYPE = "Action_Type_"
    Const strBEZOEKVERSLAG = "Bezoekverslag"
    Const strPROJECTDETAILREFERENCE = "project_detail_reference"

    Const strQMEMPLOYEEACTION = "QM_Employee_Action"
    Const strQMEMPLOYEESALARY = "QM_Employee_Salary"
    Const strQMEMPLOYEEZIEKMELDING = "QM_Employee_Ziekmelding"
    Const strQMEMPLOYEEVISIT = "QM_Employee_Visit"

    Const strAT_ZIEKMELDING = "Ziekmelding"
    Const strAT_BEZOEKVERSLAG = "Bezoekverslag"
    Const strAT_SALARISMUTATIE = "Salarismutatie"

    Const strVERPLEEGADRES = "Verpleegadres"

    Const strQMFINANCE = "QMagic finance"


    ' -----------------------------------------------------------------------------------------------------------------
    ' Name:    Disable
    ' Purpose: This sub disables field after opening the form if Action_Type = "Bezoekverslag"
    ' ------------------------------------------------------------------------------------------------------
    ' Inputs: vntParameters - input parameter for OnFormLoaded sub
    ' Returns:
    ' Implements Agent: QMagic\Employee action\on form open
    ' History:
    ' Reversion#    Date        Author                  Description
    ' ----------           ----        ------                         -----------
    ' 1.0           14/11/2003  VDI\AlexanderSl      Initial version 
    ' 1.1           17/11/2003  VDI\AndreyF          Minor update code and move to global scripts
    ' ------------------------------------------------------------------------------------------
    Sub Disable()

        Dim vntCarRsts
        Dim rstPrimaryRecordset
        Dim Segment
        Dim SegmentID

        vntCarRsts = UIMaster.RUICenter.Form

        If vntCarRsts.FormName = strQMEMPLOYEESALARY Then Exit Sub

        rstPrimaryRecordset = UIMaster.RUICenter.PrimaryRecordset
        Segment = vntCarRsts.Segments.Item(strQMEMPLOYEEACTION)
        SegmentID = Segment.SegmentID

        'Disabling field if Action_Type <> Bezoekverslag

        If Not rstPrimaryRecordset.Fields(strfACTIONTYPE).Value = strBEZOEKVERSLAG Then
            Call UIMaster.RUICenter.DisableField(SegmentID, strPROJECTDETAILREFERENCE, False)
        Else
            Call UIMaster.RUICenter.DisableField(SegmentID, strPROJECTDETAILREFERENCE, True)
        End If

    End Sub


    Function GetFormToOpen(ByVal strActionType)
        Dim strFormName
        Dim vntParams

        Select Case strActionType
            Case strAT_ZIEKMELDING
                strFormName = strQMEMPLOYEEZIEKMELDING
            Case strAT_BEZOEKVERSLAG
                strFormName = strQMEMPLOYEEVISIT
            Case strAT_SALARISMUTATIE
                vntParams = New Object() {UIMaster.RSysClient.UserId, strQMFINANCE}

                UIMaster.RUICenter.Form.Execute("UserInGroup", vntParams)

                If Not vntParams Then
                    CMSMsgBox("U heeft geen rechten om salarismutaties te wijzigen.", vbOKOnly, "Foutmelding")
                    strFormName = ""

                Else
                    strFormName = strQMEMPLOYEESALARY
                End If
            Case Else
                strFormName = strQMEMPLOYEEACTION

        End Select

        GetFormToOpen = strFormName
    End Function
End Module
