'This module along with 
'   TransitPointParams
'   CustomDialogBox
'   CMSErrorObject
'make up analog of Global Active Access Script

'***************************************************************
'* VBScript -> VB.NET change info:
'*  Array(x, y, z, ...) -> New Object() {x, y, z, ...}
'*  Null -> System.DBNull.Value
'*  IsNull -> IsDbNull
'*  Empty -> Nothing
'*  IsEmpty -> IsNothing
'*  IsObject -> IsReference
'*  Err -> Err.Number when comparing with integers
'*  Number of array dimension has been added to array definitions
'*  Following variables were added due to absense of 'Option Explicit':
'*      strFormName in MapActivityTypeToFormName
'*  Full name qualifications were made to following enums:
'*      RDAUILib.ActionTypeEnum in CSSFullTextSearch
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

    ' -------------------------------------------------------------------------------------------
    ' Name :    CMSDialogRadioEx
    ' Purpose : This function is a Global client script to show a modal dialog form by use of radio
    '           button
    ' -------------------------------------------------------------------------------------------
    ' Inputs:
    '   strHTMLText : The message to describe the function of the dialog form
    '   strTitle    : The title of the dialog form
    '   strBtnTexts : A string array to hold the button text to be shown on the dialog form
    '                 NOTE: If you only want to add one button, you can pass a string instead of
    '                       a string array.
    '   iDefaultBtn - index of the button text to be used as default.
    '
    ' Returns:
    '   CMSDialogRadioEx : The index start from 1 user selected indicate the order of the button
    '                 If the user click the close button on the upper-right of the form, the returned
    '                 will be -1.
    ' History:
    ' Reversion#    Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 1.0           02/15/2000  DY      Initial version
    '                               02/21/2000  JH          Added default parameter.
    ' ------------------------------------------------------------------------------------------
    Function CMSDialogRadioEx(ByVal strHTMLText, ByVal strTitle, ByVal strBtnTexts, ByVal iDefaultBtn)
        Dim lngRet
        Dim strReturn
        Dim rfrmRadioButton
        Dim rfrmRadioButtons
        Dim rfrmRadioButtonMain
        Dim rfrmParamMain

        rfrmRadioButtonMain = UIMaster.CreateFormParam()
        rfrmRadioButtons = UIMaster.CreateFormParams()
        rfrmParamMain = UIMaster.CreateFormParams()
        With rfrmParamMain
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
                .Label = strHTMLText
                .Value = iDefaultBtn
                .Type = 1024
                .Enums = rfrmRadioButtons
            End With
            .AddItem(rfrmRadioButtonMain)
        End With

        strReturn = UIMaster.ShowDialogModal(rfrmParamMain, strTitle)

        With rfrmRadioButtonMain
            If strReturn = "1" Then
                If .Value >= 0 Then CMSDialogRadioEx = .Value + 1
            Else
                CMSDialogRadioEx = -1
            End If
        End With

    End Function
    'Option Explicit

    ' 08/07/2001  DY    Remove script PromptRefresh because we include the functionality in Global_ErrorHandler

    ' -------------------------------------------------------------------------------------------
    ' Name :    CMSDialog
    ' Purpose :  This function is a Global client script to show a modal dialog form
    ' -------------------------------------------------------------------------------------------
    ' Inputs:
    '   strHTMLText : The message to describe the function of the dialog form
    '   strTitle    : The title of the dialog form
    '   strBtnTexts : A string array to hold the button text to be shown on the dialog form
    '                 NOTE: If you only want to add one button, you can pass a string instead of
    '                       a string array.
    ' Returns:
    '   CMSDialog   : The index start from 1 user selected indicate the order of the button
    '                 If the user click the close button on the upper-right of the form, the returned
    '                 will be -1.
    ' History:
    ' Reversion#    Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 1.0           01/12/2000  DY      Initial version
    ' ------------------------------------------------------------------------------------------
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
        Const strdOK = "OK"
        Const strdCANCEL = "Cancel"
        Const strdABORT = "Abort"
        Const strdRETRY = "Retry"
        Const strdIGNORE = "Ignore"
        Const strdYES = "Yes"
        Const strdNO = "No"

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
            strBtnOK = strdOK
            strBtnAbort = strdABORT
            strBtnCancel = strdCANCEL
            strBtnIgnore = strdIGNORE
            strBtnNo = strdNO
            strBtnRetry = strdRETRY
            strBtnYes = strdYES
            Err.Clear()
        Else
            With rdltLangDict
                strBtnOK = .GetText(strdOK)
                strBtnAbort = .GetText(strdABORT)
                strBtnCancel = .GetText(strdCANCEL)
                strBtnIgnore = .GetText(strdIGNORE)
                strBtnNo = .GetText(strdNO)
                strBtnRetry = .GetText(strdRETRY)
                strBtnYes = .GetText(strdYES)
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
                Select Case intButtons
                    Case vbOKOnly, vbOKCancel, vbYesNoCancel, vbRetryCancel
                        CMSMsgBox = vbCancel
                    Case vbAbortRetryIgnore
                        CMSMsgBox = vbIgnore
                    Case vbYesNo
                        CMSMsgBox = vbNo
                    Case Else
                        CMSMsgBox = vbCancel
                End Select
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

        Const lngAPP_TYPE_MEETING = 0
        Const lngAPP_TYPE_TO_DO = 1
        Const lngAPP_TYPE_CALL = 2
        Const lngAPP_TYPE_MESSAGE = 3
        Const lngAPP_TYPE_LITERATURE = 4
        Const lngAPP_TYPE_NOTE = 5
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
            strBtnTexts = New Object() {.GetText("Call Button"), .GetText("Literature Fulfillment Button"), _
                .GetText("Meeting Button"), .GetText("Message Button"), .GetText("Note Button"), _
                .GetText("To-Do Button")}
            intAppTypes = New Object() {lngAPP_TYPE_CALL, lngAPP_TYPE_LITERATURE, lngAPP_TYPE_MEETING, _
                lngAPP_TYPE_MESSAGE, lngAPP_TYPE_NOTE, lngAPP_TYPE_TO_DO}
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
            Case Else
                Exit Function
        End Select

        ' Organize transition point paramer list
        objParam = CreateTransitPointParamsObj()
        With objParam
            If Not IsDBNull(vntCompanyId) Then .AddDefaultField(strfCOMPANY, vntCompanyId)
            If Not IsDBNull(vntContactId) Then .AddDefaultField(strfCONTACT, vntContactId)
            If Not IsDBNull(vntLeadId) Then .AddDefaultField(strfLEAD_ID, vntLeadId)
            If Not IsDBNull(vntOpportunityId) Then .AddDefaultField(strfOPPORTUNITY, vntOpportunityId)
            If Not IsDBNull(vntMarketingProject) Then .AddDefaultField(strfMARKETING_PROJECT, vntMarketingProject)
            .AddDefaultField(strfACTIVITY_TYPE, intActivityType)
            vntParameters = .ConstructParams()
        End With

        'actionAskUser
        If blnShowModal = True Then
            vntReturns = UIMaster.ShowFormModal(strFormName, System.DBNull.Value, vntParameters)
            NewActivity = (vntReturns(0) = 1)
        Else
            UIMaster.ShowForm(2, strFormName, System.DBNull.Value, vntParameters)
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

        If IsDBNull(vntValue1) And IsDBNull(vntValue2) Then
            EqualValues = True
        ElseIf IsDBNull(vntValue1) And Not IsDBNull(vntValue2) Then
            EqualValues = False
        ElseIf Not IsDBNull(vntValue1) And IsDBNull(vntValue2) Then
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
        vntRecordsets = UIMaster.ShowFormModal(strrACTION_PLAN_LINKS, System.DBNull.Value, vntParameters)
        ' If cancel or save failed, by pass the following steps
        If vntRecordsets(0) = 0 Then Exit Function
        vntfActivity_Id = vntRecordsets(1) '.Fields(strfRN_APPOINTMENTS_ID).Value
        If IsDBNull(vntfActivity_Id) Or IsNothing(vntfActivity_Id) Then Exit Function
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
        If IsArray(vntMeetingIds) And IsNothing(vntMeetingIds) = False Then
            For intParam = 0 To UBound(vntMeetingIds)
                vntfActivity_Id = vntMeetingIds(intParam)
                vntArguments = New Object() {Nothing}
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
            vntExceptions = New Object() {vntExceptions}
        End If
        If IsArray(vntExceptions) = False Then
            vntExceptions = New Object() {""}
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


    '**********************************************************
    'Name: PrePendEmailBody
    'Purpose: Takes the body of an email as a string and prepend each
    ' line by the '>' character.
    'Assumptions:
    'Effects:
    'Inputs: strBody - email body as a string
    'Outputs:
    'Returns: email body with each line prepended with a '>' character
    'Implements Agent: None
    '
    'Revision # Date            Author  Description
    '---------- ----            ------  -----------
    '1.0        July 17, 2001   TS     Initial version
    '**********************************************************
    Function PrePendEmailBody(ByVal strBody)

        Const strPREPEND_CHARACTER = ">"

        Dim intStartPos
        Dim intNextPos
        Dim strTempBody


        intStartPos = 1
        intNextPos = InStr(intStartPos, strBody, vbCrLf, vbTextCompare)
        If intNextPos = 0 Then intNextPos = Len(strBody)
        strTempBody = strPREPEND_CHARACTER & Mid(strBody, intStartPos, intNextPos - intStartPos + 2)
        intStartPos = intNextPos + 2
        intNextPos = InStr(intStartPos, strBody, vbCrLf, vbTextCompare)
        While intNextPos <> 0
            strTempBody = strTempBody & strPREPEND_CHARACTER & Mid(strBody, intStartPos, intNextPos - intStartPos + 2)
            intStartPos = intNextPos + 2
            intNextPos = InStr(intStartPos, strBody, vbCrLf, vbTextCompare)
        End While
        If intNextPos = 0 Then
            intNextPos = Len(strBody)
            strTempBody = strTempBody & strPREPEND_CHARACTER & Mid(strBody, intStartPos, intNextPos - intStartPos + 2)
        End If

        PrePendEmailBody = strTempBody
    End Function





    'Option Explicit

    Dim lngTempActivityLoad
    Dim lngTempActivityNew

    Dim gvntMilestone_Template_Id
    Dim gstrPipeline_Stage
    Dim gintDays

    Dim arrUIStack

    Dim objUIMasterFullTextSearchModelessWindow
    Dim objUIMasterModelessWindowFromSearch
    Dim blnDisplayRecordFromListInModelessWindow

    Dim blnRefreshRequired
    Dim arrDirtyWebTabs()
    Dim arrItemsInUse(,)
    Dim arrSolutionsInUse(,)



    ' Initilaze the dirty flag arrays
    ' iWebTabs - number of tabs
    ' rstItems - recordset of Quote_Details
    ' rstSolutions - recordset of Product_Solutions
    ' iType -  0 - Quote
    '          1 - Order
    Sub InitDirtyFlag(ByVal iWebTabs, ByVal rstItems, ByVal rstSolutions, ByVal strType)

        On Error Resume Next

        Dim i
        Dim iItems
        Dim iSolutions

        If iWebTabs < 0 Then iWebTabs = 0
        ReDim arrDirtyWebTabs(iWebTabs)
        For i = 0 To iWebTabs
            arrDirtyWebTabs(i) = False
        Next

        If strType = 0 Then

            ' Configured Quote
            iItems = rstItems.RecordCount - 1
            iSolutions = rstSolutions.RecordCount - 1

            If iItems < 0 Then iItems = 0
            If iSolutions < 0 Then iSolutions = 0

            ReDim arrItemsInUse(iItems, 1)
            ReDim arrSolutionsInUse(iSolutions, 1)

            With rstItems
                If .RecordCount = 0 Then
                    arrItemsInUse(0, 0) = UIMaster.RSysClient.StringToId("0x0000000000000000")
                    arrItemsInUse(0, 1) = False
                Else
                    .MoveFirst()
                    i = 0
                    Do Until .EOF
                        arrItemsInUse(i, 0) = .Fields("Quote_Details_Id").Value
                        arrItemsInUse(i, 1) = False
                        i = i + 1
                        .MoveNext()
                    Loop
                End If
            End With

            With rstSolutions
                If .RecordCount = 0 Then
                    arrSolutionsInUse(i, 0) = UIMaster.RSysClient.StringToId("0x0000000000000000")
                    arrSolutionsInUse(i, 1) = False
                Else
                    .MoveFirst()
                    i = 0
                    Do Until .EOF
                        arrSolutionsInUse(i, 0) = .Fields("Product_Solution_Id").Value
                        arrSolutionsInUse(i, 1) = False
                        i = i + 1
                        .MoveNext()
                    Loop
                End If
            End With

        Else
            ' eSelling Order
            iItems = rstItems.RecordCount - 1

            If iItems < 0 Then iItems = 0

            ReDim arrItemsInUse(iItems, 1)

            With rstItems
                If .RecordCount = 0 Then
                    arrItemsInUse(0, 0) = UIMaster.RSysClient.StringToId("0x0000000000000000")
                    arrItemsInUse(0, 1) = False
                Else
                    .MoveFirst()
                    i = 0
                    Do Until .EOF
                        arrItemsInUse(i, 0) = .Fields("Order_Detail_Id").Value
                        arrItemsInUse(i, 1) = False
                        i = i + 1
                        .MoveNext()
                    Loop
                End If
            End With
        End If
    End Sub


    Sub UpdateDirtyFlag(ByVal rstItems, ByVal rstSolutions, ByVal iType)

        On Error Resume Next

        Dim i
        Dim iOldItems
        Dim iOldSolutions
        Dim iItems
        Dim iSolutions

        Dim oldItems
        Dim oldSolutions

        If iType = 0 Then
            ' Quote
            oldItems = arrItemsInUse
            oldSolutions = arrSolutionsInUse

            iItems = rstItems.RecordCount
            iSolutions = rstSolutions.RecordCount

            If iItems < 0 Then iItems = 0
            If iSolutions < 0 Then iSolutions = 0

            ReDim arrItemsInUse(iItems - 1, 1)
            ReDim arrSolutionsInUse(iSolutions - 1, 1)

            With rstItems
                If .RecordCount = 0 Then
                    arrItemsInUse(0, 0) = UIMaster.RSysClient.StringToId("0x0000000000000000")
                    arrItemsInUse(0, 1) = False
                Else
                    .MoveFirst()
                    i = 0
                    Do Until .EOF
                        arrItemsInUse(i, 0) = .Fields("Quote_Details_Id").Value
                        arrItemsInUse(i, 1) = GetItemFlag(arrItemsInUse(i, 0), oldItems)
                        i = i + 1
                        .MoveNext()
                    Loop
                End If
            End With

            With rstSolutions
                If .RecordCount = 0 Then
                    arrSolutionsInUse(0, 0) = UIMaster.RSysClient.StringToId("0x0000000000000000")
                    arrSolutionsInUse(0, 1) = False
                Else
                    .MoveFirst()
                    i = 0
                    Do Until .EOF
                        arrSolutionsInUse(i, 0) = .Fields("Product_Solution_Id").Value
                        arrSolutionsInUse(i, 1) = GetItemFlag(arrSolutionsInUse(i, 0), oldSolutions)
                        i = i + 1
                        .MoveNext()
                    Loop
                End If
            End With

        Else
            ' Order
            oldItems = arrItemsInUse

            iItems = rstItems.RecordCount

            If iItems < 0 Then iItems = 0

            ReDim arrItemsInUse(iItems, 1)

            With rstItems
                If .RecordCount = 0 Then
                    arrItemsInUse(0, 0) = UIMaster.RSysClient.StringToId("0x0000000000000000")
                    arrItemsInUse(0, 1) = False
                Else
                    .MoveFirst()
                    i = 0
                    Do Until .EOF
                        arrItemsInUse(i, 0) = .Fields("Order_Detail_Id").Value
                        arrItemsInUse(i, 1) = GetItemFlag(arrItemsInUse(i, 0), oldItems)
                        i = i + 1
                        .MoveNext()
                    Loop
                End If
            End With
        End If
    End Sub


    Function GetItemFlag(ByVal vntId, ByVal arrItems)

        On Error Resume Next

        Dim i

        GetItemFlag = False
        For i = 0 To UBound(arrItems, 1)
            If EqualValues(vntId, arrItems(i, 0)) Then
                GetItemFlag = arrItems(i, 1)
                Exit For
            End If
        Next

    End Function

    ' OkToChangeTab - checks whether the to-be-browsed tab is dirty and ask for user input.
    'Parameters:
    ' strTabName: 	tab name eg. "Configurator" or "Guilded Selling"
    ' strType: 	type of record eg. "Quote" or "Order"
    'Return value:    True - browse to the designated tab. That means either the to-be-browsed tab is not dirty or it's dirty but the user is ok to tab away.
    '                       False - stay on the same tab. That means the to-be-browsed tab is dirty and the user does not want to browse away.
    Function OkToChangeTab(ByVal strTabName, ByVal strType)

        On Error Resume Next

        Dim strMessage
        Dim strTitle
        Dim strBtnTexts
        Dim intChoice
        Dim iTabConfigurator
        Dim iTabGuidedSelling
        Dim iTab

        OkToChangeTab = False 'default - stay on the same tab
        iTabConfigurator = 0
        iTabGuidedSelling = 0
        iTab = 0

        If strType = "Quote" Then
            iTabConfigurator = 2
            iTabGuidedSelling = 3
            If strTabName = "Configurator" Then
                iTab = 2
            ElseIf strTabName = "Guided Selling" Then
                iTab = 3
            End If
        ElseIf strType = "Order" Then
            iTabConfigurator = 5
            iTabGuidedSelling = 6
            If strTabName = "Configurator" Then
                iTab = 5
            ElseIf strTabName = "Guided Selling" Then
                iTab = 6
            End If
        End If


        If arrDirtyWebTabs(iTab) = True Then
            With UIMaster.RSysClient.GetLDGroup("Quote")
                If iTab = iTabConfigurator Then
                    strMessage = .GetText("Unfinished Configuration AS")
                    strTitle = .GetText("Unfinished Configuration")
                ElseIf iTab = iTabGuidedSelling Then
                    strMessage = .GetText("Unfinished Guided Selling Session AS")
                    strTitle = .GetText("Unfinished Guided Selling Session")
                Else
                    Exit Function
                End If
                strBtnTexts = New Object() {.GetText("Yes"), .GetText("No")}
            End With

            intChoice = CMSDialog(strMessage, strTitle, strBtnTexts)
            If intChoice = 1 Then
                OkToChangeTab = True 'browse to designated tab
            End If
        Else
            OkToChangeTab = True
        End If

    End Function


    'global functions

    ' ------------------------------------------------------------------------------------------
    ' Name:     CalculateTimeDiff
    ' Purpose:  This function calculates the time difference between the Relationship
    '           fields starting date/time and the ending date/time in terms of the interval provided.
    '           The DateDiff function would round down to the lower unit. As for minute and for
    '           the requirement of time tracking, we would round up to the nearest minute.
    ' ------------------------------------------------------------------------------------------
    ' Inputs:   Interval - the interval of time you use to calculate the difference
    '                       between StartDate, StartTime and EndDate, EndTime
    '                   'yyyy'  - Year
    '                   'q'     - Quarter
    '                   'm'     - Month
    '                   'y'     - Day of Year
    '                   'd'     - Day
    '                   'w'     - Weekday
    '                   'ww'    - Week
    '                   'h'     - Hour
    '                   'n'     - Minute
    '                   's'     - Second
    '           StartDate - Relationship Date field for the starting date
    '           StartTime - Relationship Time field for the starting time
    '           EndDate   - Relationship Date field for the ending date
    '           EndTime   - Relationship Time field for the ending time
    ' Outputs:
    ' Returns:  time difference in terms of the interval provided
    ' History:
    ' Revision#     Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 4.0           04/05/2001  CT      Initial version
    ' ------------------------------------------------------------------------------------------
    Function CalculateTimeDiff(ByVal Interval, ByVal StartDate, ByVal StartTime, _
    ByVal EndDate, ByVal EndTime)

        Dim StartDateTime
        Dim EndDateTime
        On Error Resume Next

        StartDateTime = CStr(DateValue(StartDate)) & " " & CStr(TimeValue(StartTime))
        EndDateTime = CStr(DateValue(EndDate)) & " " & CStr(TimeValue(EndTime))

        If Interval = "n" Then 'rounding up for minute interval
            CalculateTimeDiff = CLng(Int((DateDiff("s", StartDateTime, EndDateTime) / 60) + 1))
        Else
            CalculateTimeDiff = DateDiff(Interval, StartDateTime, EndDateTime)
        End If

    End Function


    ' ------------------------------------------------------------------------------------------
    ' Name:     PushUIStack
    ' Purpose:  This function adds the input UIMaster to the end of the global array of UIMaster's
    ' ------------------------------------------------------------------------------------------
    ' Inputs:    objUIMaster - UIMaster object to push to the array stack
    ' Outputs:  None
    ' Returns:  None
    ' History:
    ' Revision#     Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 4.0           05/05/2001  CT      Initial version
    ' ------------------------------------------------------------------------------------------
    Sub PushUIStack(ByVal objUIMaster)
        Dim intUB
        On Error Resume Next
        If IsNothing(arrUIStack) = False Then
            intUB = UBound(arrUIStack)
            ReDim Preserve arrUIStack(intUB + 1)
            arrUIStack(intUB + 1) = objUIMaster
        Else
            arrUIStack = New Object() {objUIMaster}
        End If

    End Sub

    ' ------------------------------------------------------------------------------------------
    ' Name:     PopUIStack
    ' Purpose:  This function removes the last UIMaster from the global array of UIMaster's
    ' ------------------------------------------------------------------------------------------
    ' Inputs:   None
    ' Outputs:  None
    ' Returns:  None
    ' History:
    ' Revision#     Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 4.0           05/05/2001  CT      Initial version
    ' ------------------------------------------------------------------------------------------
    Sub PopUIStack()
        Dim intUB
        On Error Resume Next
        If IsNothing(arrUIStack) = False Then
            intUB = UBound(arrUIStack)
        Else
            intUB = -1
        End If
        If intUB > 0 Then
            ReDim Preserve arrUIStack(intUB - 1)
        Else
            arrUIStack = Nothing
        End If
    End Sub

    ' ------------------------------------------------------------------------------------------
    ' Name:     GetCurUI
    ' Purpose:  This function returns the last UIMaster on the global array of UIMaster's
    ' ------------------------------------------------------------------------------------------
    ' Inputs:   None
    ' Outputs:  None
    ' Returns:  the last UIMaster object on the global array of UIMaster's
    ' History:
    ' Revision#     Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 4.0           05/05/2001  CT      Initial version
    ' ------------------------------------------------------------------------------------------
    Function GetCurUI()
        Dim intUB
        On Error Resume Next
        If IsNothing(arrUIStack) = False Then
            intUB = UBound(arrUIStack)
        Else
            intUB = -1
        End If
        If intUB >= 0 Then
            GetCurUI = arrUIStack(intUB)
        Else
            GetCurUI = Nothing
        End If
    End Function

    ' ------------------------------------------------------------------------------------------
    ' Name:     ShowFKForm
    ' Purpose:  This function saves the current UIMaster to the global array of UIMaster and
    '           shows the form the foreign key link and then removes the UIMaster from the array
    '           after showing the form
    ' ------------------------------------------------------------------------------------------
    ' Inputs:   UIMaster - the UIMaster to save
    '           strFormName - the foreign key form to show
    '           strFieldName - the foreign key field name for the UIMaster to set value to
    ' Outputs:  None
    ' Returns:
    ' History:
    ' Revision#     Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 4.0           05/05/2001  CT      Initial version
    ' ------------------------------------------------------------------------------------------
    Sub ShowFKForm(ByVal UIMaster, ByVal vntFormId, ByVal vntParameters)

        On Error Resume Next
        PushUIStack(UIMaster)
        UIMaster.ShowFormModal(vntFormId, System.DBNull.Value, vntParameters)
        PopUIStack()
        If Err.Number <> 0 Then
            Exit Sub
        End If

    End Sub

    ' -------------------------------------------------------------------------------------------
    ' Name:     CheckCompanyLeadContactFK
    ' Purpose:  This function checks if the current field is a foreign key field to either
    '           one of Company, Contact, or Lead tables. It then saves the UIMaster before showing
    '           the form. Such that, when a record is added, the new record id will be returned
    '           to the foreign key field on the UIMaster saved for the last form.
    ' -------------------------------------------------------------------------------------------
    ' Inputs:
    ' Returns:  True - override default platform behavior
    '           False - use default platform behavior
    '   N/A
    ' History:
    ' Reversion#    Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 4.0           05/22/2001  CT      Initial version
    ' ------------------------------------------------------------------------------------------
    Function CheckCompanyLeadContactFK(ByVal UIMaster, ByVal vntExistParameters)

        Dim obj
        Dim tableId
        Dim formId
        Dim recordId
        Dim security
        Dim permissions
        Dim eventObj
        Dim sysClnt
        Dim tab
        Dim seg
        Dim fld
        Dim strTableName
        Dim strMsg
        Dim rForm
        Dim vntParameters
        Dim objParam

        On Error Resume Next

        eventObj = UIMaster.RUICenter.FormEventObj
        sysClnt = UIMaster.RSysClient

        tab = UIMaster.RUICenter.Form.Tabs.Item(eventObj.TabIndex)
        seg = tab.Segments.Item(eventObj.SegmentIndex)
        fld = seg.FormFields.Item(eventObj.FieldIndex)
        tableId = fld.ForeignField.Table.tableId

        'MsgBox (sysClnt.IdToString(tableId))

        security = sysClnt.GetUserSecurity

        permissions = security.TablePermissions(tableId)
        ' 1 is read permission
        If (permissions & 1) Then
            formId = security.DefaultFormId(tableId)
            'MsgBox (sysClnt.IdToString(formId))
        End If

        recordId = UIMaster.RUICenter.PrimaryRecordset.Fields.Item(eventObj.FieldName).Value

        'MsgBox (sysClnt.IdToString(recordId))

        strTableName = sysClnt.GetTable(tableId).TableName

        'Check for Lead, Contact, Company
        If (strTableName = "Lead_" Or _
            strTableName = "Contact" Or _
            strTableName = "Company") And IsDBNull(recordId) Then
            If strTableName = "Lead_" Then
                rForm = sysClnt.GetForm("Lead")
                formId = rForm.formId
            End If
            'set strFieldName to vntParameter(3)
            If IsDBNull(vntExistParameters) = True Then
                objParam = CreateTransitPointParamsObj()
                objParam.SetFKFieldName(eventObj.FieldName)
                vntParameters = objParam.ConstructParams
            Else
                vntParameters = vntExistParameters
            End If
            ShowFKForm(UIMaster, formId, vntParameters)
        Else 'other FK Id fields
            CheckCompanyLeadContactFK = False
            Exit Function
        End If

        If Err.Number <> 0 Then
            CheckCompanyLeadContactFK = False
        Else
            CheckCompanyLeadContactFK = True
        End If
        Err.Clear()

    End Function


    ' ------------------------------------------------------------------------------------------
    ' Name:     SaveNewRecord
    ' Purpose:  This function shows the list of duplicate and gets user response to determine to
    '           save the new record
    ' ------------------------------------------------------------------------------------------
    ' Inputs:   vntTableId - table id of the list to show
    '           rstDuplicates - recordset of duplicates to show
    '           strFormName - form name of the duplicates
    '           strFieldName - the foreign key field name for the UIMaster to set value to
    ' Outputs:  None
    ' Returns:
    ' History:
    ' Revision#     Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 4.0           05/05/2001  CT      Initial version
    '               06/13/2001  DY      Fix Issue 106496-2432 Able to select a deleted company
    ' ------------------------------------------------------------------------------------------
    Function SaveNewRecord(ByVal vntTableId, ByVal rstDuplicates, ByVal strFormName, ByVal strFKFieldName)
        Const strfrmCOMPANY = "Company"
        Const strfrmCONTACT = "Contact"
        Const strfrmLEAD = "Lead"

        Const strgCOMPANY = "Company"
        Const strgCONTACT = "Contact"
        Const strgLEAD = "Lead"

        Const strldstrWANT_TO_ADD_COMPANY = "WantToAddCompany"
        Const strldstrIS_THIS_COMPANY = "IsThisCompany"
        Const strldstrEXAM_DUP_COMPANY = "Examine Duplicate Companies"

        Const strldstrWANT_TO_ADD_CONTACT = "WantToAddContact"
        Const strldstrIS_THIS_CONTACT = "IsThisContact"
        Const strldstrEXAM_DUP_CONTACT = "Examine Duplicate Contacts"

        Const strldstrWANT_TO_ADD_LEAD = "WantToAddLead"
        Const strldstrIS_THIS_LEAD = "IsThisLead"
        Const strldstrEXAM_DUP_LEAD = "Examine Duplicate Leads"

        Const strldstrMESSAGE = "Message"

        Dim blnSelectAnotherOne
        Dim arrRecord_Id
        Dim vntSelected_Id
        Dim strMsgDoYou, strTitle, strMsgIsThis, strMsgExamDup
        Dim vntParams
        Dim rldtLangDict
        Dim objCurUI

        On Error Resume Next
        blnSelectAnotherOne = True
        'get strMsgDoYou, strTitle, strIsThis
        Select Case strFormName
            Case strfrmCOMPANY
                rldtLangDict = UIMaster.RSysClient.GetLDGroup(strgCOMPANY)
                strMsgDoYou = rldtLangDict.GetText(strldstrWANT_TO_ADD_COMPANY)
                strMsgIsThis = rldtLangDict.GetText(strldstrIS_THIS_COMPANY)
                strMsgExamDup = rldtLangDict.GetText(strldstrEXAM_DUP_COMPANY)
            Case strfrmCONTACT
                rldtLangDict = UIMaster.RSysClient.GetLDGroup(strgCONTACT)
                strMsgDoYou = rldtLangDict.GetText(strldstrWANT_TO_ADD_CONTACT)
                strMsgIsThis = rldtLangDict.GetText(strldstrIS_THIS_CONTACT)
                strMsgExamDup = rldtLangDict.GetText(strldstrEXAM_DUP_CONTACT)
            Case strfrmLEAD
                rldtLangDict = UIMaster.RSysClient.GetLDGroup(strgLEAD)
                strMsgDoYou = rldtLangDict.GetText(strldstrWANT_TO_ADD_LEAD)
                strMsgIsThis = rldtLangDict.GetText(strldstrIS_THIS_LEAD)
                strMsgExamDup = rldtLangDict.GetText(strldstrEXAM_DUP_LEAD)
            Case Else
                strMsgDoYou = ""
                strMsgIsThis = ""
                strMsgExamDup = ""
        End Select

        strTitle = rldtLangDict.GetText(strldstrMESSAGE)

        CMSMsgBox(strMsgExamDup, vbOKOnly, strTitle)
        While blnSelectAnotherOne = True
            arrRecord_Id = UIMaster.ShowSelectionListModal(vntTableId, rstDuplicates)
            If UBound(arrRecord_Id) < 0 Then
                blnSelectAnotherOne = False
                If CMSMsgBox(strMsgDoYou, vbYesNo, strTitle) = vbYes Then
                    SaveNewRecord = True
                Else
                    SaveNewRecord = False
                End If
            ElseIf Not IsDBNull(arrRecord_Id(0)) Then
                vntSelected_Id = arrRecord_Id(0)
                arrRecord_Id = UIMaster.ShowFormModal(strFormName, vntSelected_Id, vntParams)
                If arrRecord_Id(0) <> 2 Then
                    If CMSMsgBox(strMsgIsThis, vbYesNo, strTitle) = vbYes Then
                        If IsNothing(arrUIStack) = False And IsNothing(strFKFieldName) = False Then
                            objCurUI = GetCurUI()
                            objCurUI.RUICenter.PrimaryRecordset.Fields(strFKFieldName).Value = vntSelected_Id
                        End If
                        SaveNewRecord = False
                        Exit Function
                    Else
                        blnSelectAnotherOne = True
                    End If
                Else
                    SaveNewRecord = False
                    Exit Function
                End If
            End If
        End While
    End Function

    ' ------------------------------------------------------------------------------------------
    ' Name:     SetNewFKId
    ' Purpose:  This function sets the new record id to the Foreign Key field of the parent form
    ' ------------------------------------------------------------------------------------------
    ' Inputs:   strFieldName - the foreign key field name for the UIMaster to set value to
    '           vntNewRecId - new record id just added
    ' Outputs:  None
    ' Returns:
    ' History:
    ' Revision#     Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 4.0           05/05/2001  CT      Initial version
    ' ------------------------------------------------------------------------------------------
    Sub SetNewFKId(ByVal strFKFieldName, ByVal vntNewRecId)
        On Error Resume Next
        Dim objCurUI
        If IsNothing(arrUIStack) = False And IsNothing(strFKFieldName) = False Then
            objCurUI = GetCurUI()
            objCurUI.RUICenter.PrimaryRecordset.Fields(strFKFieldName).Value = vntNewRecId
        End If
    End Sub


    ' ------------------------------------------------------------------------------------------
    ' Name:     SetRequiredFields
    ' Purpose:  This function sets the required fields to the current form
    ' ------------------------------------------------------------------------------------------
    ' Inputs:   vntRequired - variant array holds required fields information.
    ' Outputs:  None
    ' Returns:
    ' History:
    ' Revision#     Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 4.0           08/20/2001  JC      Initial version
    ' ------------------------------------------------------------------------------------------
    Sub SetRequiredFields(ByVal vntRequiredFields)

        On Error Resume Next

        Dim i
        Dim vntRequiredField

        If IsNothing(vntRequiredFields) = True Then Exit Sub

        For i = 0 To UBound(vntRequiredFields)
            vntRequiredField = vntRequiredFields(i)
            UIMaster.RUICenter.SetFieldRequiredEx(vntRequiredField(0), vntRequiredField(1), vntRequiredField(2), True)
        Next

    End Sub


    '--------------------------------------------------------------------------------
    '5.0      02/06/2003  DY   Fix Issue 28119: Error logs are generated if Employee 
    '			   record for Active Access user created in WA. 
    '--------------------------------------------------------------------------------
    Sub OnPortalLoaded(ByVal ParameterList)
        Dim rfrmForm
        Dim vntRecordsets

        If IsDBNull(UIMaster.RSysClient.EmployeeId) Then Exit Sub

        rfrmForm = UIMaster.RSysClient.GetForm("Employee")

        Dim objParams
        Dim vntParams
        Dim blnRet
        objParams = CreateTransitPointParamsObj()
        objParams.SetUserDefParam(1, "Duty Manager")
        objParams.SetUserDefParam(2, blnRet)
        vntParams = objParams.ConstructParams()
        rfrmForm.Execute("IsCurrentUserInGroup", vntParams)
        blnRet = objParams.GetUserDefParam(2, vntParams)
        If Not blnRet Then
            Exit Sub
        End If

        vntRecordsets = rfrmForm.doLoadFormData(UIMaster.RSysClient.EmployeeId, System.DBNull.Value)

        Dim objButton
        Dim objLangGroup
        Dim strText
        objLangGroup = UIMaster.RSysClient.GetLDGroup("Duty Manager")
        If vntRecordsets(0).Fields("Duty_Manager").Value = True Then
            strText = objLangGroup.GetText("Sign Off")
        Else
            strText = objLangGroup.GetText("Sign On")
        End If
        objButton = UIMaster.RUITop.AddButton(0, strText, 9999)

        UIMaster.RUITop.AddEventHookScript(strText, "DutyManager_SignOn_Click", "onclick")
    End Sub


    'Purpose: Exit handler for modeless windows.
    'Revisions:
    '5/12/2001   JH		Created.
    '6/28/2001	 TS		Added code to clean up global variables when modeless windows for
    '					for an in-context search and resulting selected items (separate window)
    '					are closed.
    Sub OnModelessWindowClose()

        If IsReference(UIMaster.Master.Global.objUIMasterFullTextSearchModelessWindow) Then
            If UIMaster.Master.Global.objUIMasterFullTextSearchModelessWindow Is UIMaster Then
                UIMaster.Master.Global.objUIMasterFullTextSearchModelessWindow = Nothing
                UIMaster.Master.Global.blnDisplayRecordFromListInModelessWindow = False
            End If
        End If

        If IsReference(UIMaster.Master.Global.objUIMasterModelessWindowFromSearch) Then
            If UIMaster.Master.Global.objUIMasterModelessWindowFromSearch Is UIMaster Then
                UIMaster.Master.Global.objUIMasterModelessWindowFromSearch = Nothing
                UIMaster.Master.Global.objUIMasterModelessWindowFromSearch = Nothing
            End If
        End If

        IsLastWindow()
    End Sub


    'Purpose: Exit handler for the Main window.
    'Revisions:
    '5/12/2001  JH   Created.
    '
    Sub OnMainWindowClose()
        IsLastWindow()
    End Sub

    'Purpose: Check to see if the last window (modeless or main) is exiting.  If so,
    'automatically log off the Duty Manager.
    '--------------------------------------------------------------------------------
    'Revision#   	Date    AUthor  Description
    '---------	----	------	-----------
    '         5/11/2001   JH   Created.
    '5.0      02/06/2003  DY   Fix Issue 28119: Error logs are generated if Employee 
    '			   record for Active Access user created in WA. 
    '--------------------------------------------------------------------------------
    Sub IsLastWindow()
        On Error Resume Next
        If IsDBNull(UIMaster.RSysClient.EmployeeId) Then Exit Sub
        If UIMaster.RSysClient.IdToString(UIMaster.RSysClient.EmployeeId) = "0x0000000000000000" Then Exit Sub

        If UBound(UIMaster.AllMasterChildren) < 0 And UIMaster.Master.Window Is Nothing Then
            'All windows have exit, so automatically log off the Duty Manager.
            Dim rfrmForm
            Dim vntRecordsets
            rfrmForm = UIMaster.RSysClient.GetForm("Employee")
            vntRecordsets = rfrmForm.doLoadFormData(UIMaster.RSysClient.EmployeeId, System.DBNull.Value)

            If vntRecordsets(0).Fields("Duty_Manager").Value = True Then
                vntRecordsets(0).Fields("Duty_Manager").Value = False
                Call rfrmForm.doSaveFormData(vntRecordsets, System.DBNull.Value)
            End If
        End If
    End Sub


    'Option Explicit

    '**********************************************************
    'Name: SearchResultsEmailLinkClickHandler
    'Purpose: When an email link on a search results list is clicked, the client's
    ' system is examined to determine whether they have Outlook 2000 installed.  If they do
    ' have Outlook 2000, then this function returns False, which indicates to the calling
    ' function, OnSearchResultsItemClick, that the default behaviour of starting Outlook 2000
    ' (with the Log & Send button) is to be performed. If Outlook 2000 is not installed, then a custom
    ' form is displayed, which has the ability to log outgoing emails to the Comm Log.  (Currently,
    ' no other email clients have the ability to log outgoing emails to the Comm Log, hence the need for
    ' the custom form).
    'Assumptions:
    'Effects:
    'Inputs:
    'Outputs:
    'Returns: True if the email click event has been handled (by bringing up the custom form) or False if
    ' the event has not been handled and the default behaviour should be performed.
    'Implements Agent: None
    '
    'Revision # Date            Author  Description
    '---------- ----            ------  -----------
    '1.0        June 28,2000    TS     Initial version
    '           11/1/2001       DY     Fix bug: objParams and vntParameters does not declare
    '           6/14/2002       JC     Fix Bug: Send email in Active Access does not create Comm log
    '                                           entry when using Lotus Notes.
    ' 5.0           12/06/2002  DY     Fix Issue 27430: Send email does not work for Lotus Note
    '**********************************************************
    Function SearchResultsEmailLinkClickHandler()
        Dim objParams
        Dim vntParameters
        Dim strTableName
        Dim vntId
        Dim objSearch

        If UIMaster.MailtoIsOutlook2000 Then
            SearchResultsEmailLinkClickHandler = False
        ElseIf UIMaster.MailtoIsLotus5 Then
            SearchResultsEmailLinkClickHandler = False
        Else
            SearchResultsEmailLinkClickHandler = True
            objParams = CreateTransitPointParamsObj()
            objParams.AddDefaultField("Email_To", UIMaster.RUICenter.SearchEventObj.Value)

            objSearch = UIMaster.RUICenter
            vntId = objSearch.List.Current
            strTableName = objSearch.List.Table.TableName
            objParams.SetUserDefParam(1, strTableName)
            objParams.SetUserDefParam(2, vntId)

            vntParameters = objParams.ConstructParams()
            UIMaster.ShowFormModal("Outbound Email", System.DBNull.Value, vntParameters)
        End If

    End Function

    '**********************************************************
    'Name: CreateModelessWindowDef
    'Purpose: Creates the window definition for a modeless window.  The window definition
    ' uses custom sizing and positioning.  The size is 85% the height of the available screen
    ' resolution (excluding the Windows Start menu bar) and 95% the width of the available screen
    ' resolution.  The position is the top/center area of the screen.
    'Assumptions:
    'Effects:
    'Inputs:
    'Outputs:
    'Returns: The modeless window definition object
    'Implements Agent: None
    '
    'Revision # Date            Author  Description
    '---------- ----            ------  -----------
    '1.0        June 28,2000        TS     Initial version
    '**********************************************************
    Function CreateModelessWindowDef()

        Dim ruiModelessWindowDef

        ruiModelessWindowDef = UIMaster.CreateModelessWindowDef
        ruiModelessWindowDef.menuAppearance = 1 ' 1= modelessMenuNone
        ruiModelessWindowDef.Size = 2 '2 = modelessSizeSpecific
        ruiModelessWindowDef.position = 1 ' 1= modelessPositionTopCenter
        ruiModelessWindowDef.Height = CLng(0.84999999999999998 * window.Screen.availHeight)
        ruiModelessWindowDef.Width = CLng(0.94999999999999996 * window.Screen.availWidth)
        CreateModelessWindowDef = ruiModelessWindowDef

    End Function

    '*******************************************************************************************
    'Name: OnSearchResultsItemClick
    'Purpose: This routine gets called by Active Access when a user selects a clickable
    ' item in a search results list.  This implementation has two primary roles: behaviour is defined
    ' for when an in-context (full text) search is displayed in a modeless window and a global variable is set
    ' and all other cases where an email type link is selected in the search results list.  In the first case,
    ' all clickable items result in new modeless windows being opened, rather than replacing the search
    ' results list window.  If multiple (consecutive) items are selected from the search results list window,
    ' then the second modeless window will be reused, if possible.  In the second case, if an email type link is
    ' clicked, then SearchResultsEmailLinkClickHandler is called.
    'Assumptions:
    'Effects:
    'Inputs:
    'Outputs:
    'Returns:
    '   False if the default behaviour (which typically replaces the window content) is to be performed,
    '   True if it is handled in some other way (usually opening another modeless window).
    'Implements Agent: None
    '
    'Revision # Date            Author  Description
    '---------- ----            ------  -----------
    '1.0        June 28,2000    TS      Initial version
    '           10/31/2001      DY      Reoganize and add code to make this script can open
    '                                   modal or modeless windows
    '           6/17/2002       JC      Changed the testing ordre for email link click.
    '*******************************************************************************************
    Function OnSearchResultsItemClick()

        With UIMaster
            If .RUICenter.SearchEventObj.Field.DataDictionary.DataAttribute = 3 Then
                ' metaTextEmail
                OnSearchResultsItemClick = SearchResultsEmailLinkClickHandler()
            ElseIf Not .RUIMenu Is Nothing Then
                OnSearchResultsItemClick = False
            ElseIf .IsModal And .RUICenter.SearchType = 5 Then
                OnSearchResultsItemClick = OpenModalFullTextSearchWindow()
            ElseIf .RUICenter.SearchType = 5 Then
                OnSearchResultsItemClick = OpenModelessFullTextSearchWindow()
            Else
                OnSearchResultsItemClick = False
            End If
        End With

    End Function

    '-------------------------------------------------------------------------------------------
    ' Name:     OpenModelessFullTextSearchWindow
    ' Purpose:  This global script will called by OnSearchResultsItemClick to open a modeless
    '           full text search window
    ' Assumptions:
    ' Effects:
    ' Inputs:
    ' Outputs:
    ' Returns:
    '    OpenModalFullTextSearchWindow - The return for the OnSearchResultsItemClick event
    ' Implements Agent: None
    '
    ' Revision # Date            Author  Description
    ' ---------- ----            ------  -----------
    ' 1.0        10/30/2001      DY      Initial version
    '                                    Move the original code to open modeless search window
    '                                    from OpenModelessFullTextSearchWindow into this function
    '                                    because OpenModelessFullTextSearchWindow will open modal or
    '                                    modeless windows
    '-------------------------------------------------------------------------------------------
    Function OpenModelessFullTextSearchWindow()
        Const ERR_RPC_E_SERVER_DIED_DNE = -2147418094
        Dim blnUseOpenRecordLogic
        Dim vntRecordId
        Dim vntTableId
        Dim ruiModelessWindowDef

        OpenModelessFullTextSearchWindow = True
        With UIMaster.RUICenter.SearchEventObj
            .searchSource.List.Recordset.Move(.rowIndex - 1, 1) ' 1= adBookMarkFirst
            If .ColumnName = "" Then
                blnUseOpenRecordLogic = True
            ElseIf .Field.DataDictionary.Type = 3 Then 'metaId
                blnUseOpenRecordLogic = True
            End If
            If blnUseOpenRecordLogic = True Then 'A record Id (either PK or FK) was clicked
                vntRecordId = .Value
                vntTableId = .TargetTable.TableId
                If IsNothing(UIMaster.Master.Global.objUIMasterModelessWindowFromSearch) Or UBound(UIMaster.AllMasterChildren) = -1 Then
                    ruiModelessWindowDef = CreateModelessWindowDef()
                    UIMaster.Master.Global.objUIMasterModelessWindowFromSearch = UIMaster.ShowFormByTableModeless(vntTableId, vntRecordId, ruiModelessWindowDef, System.DBNull.Value)
                Else
                    On Error Resume Next
                    UIMaster.Master.Global.objUIMasterModelessWindowFromSearch.ShowFormByTable(2, vntTableId, vntRecordId, System.DBNull.Value)
                    If Err.Number = ERR_RPC_E_SERVER_DIED_DNE Then
                        Err.Clear()
                        On Error GoTo 0
                        ruiModelessWindowDef = CreateModelessWindowDef()
                        UIMaster.Master.Global.objUIMasterModelessWindowFromSearch = UIMaster.ShowFormByTableModeless(vntTableId, vntRecordId, ruiModelessWindowDef, System.DBNull.Value)
                    ElseIf Err.Number <> 0 Then
                        UIMaster.ShowErrorMessage(Err.Description)
                    End If
                End If
            Else 'An email, fax, URL or unknown type link was selected
                If .Field.DataDictionary.DataAttribute = 3 Then 'metaTextEmail
                    OpenModelessFullTextSearchWindow = SearchResultsEmailLinkClickHandler()
                ElseIf .Field.DataDictionary.DataAttribute = 2 Then 'metaTextFaxPhone
                    OpenModelessFullTextSearchWindow = False
                ElseIf .Field.DataDictionary.DataAttribute = 4 Then 'metaTextWebSite
                    OpenModelessFullTextSearchWindow = False
                Else
                    OpenModelessFullTextSearchWindow = False
                End If
            End If
        End With

    End Function

    '-------------------------------------------------------------------------------------------
    ' Name:     OpenModalFullTextSearchWindow
    ' Purpose:  This global script will called by OnSearchResultsItemClick to open a modal
    '           full text search window
    ' Assumptions:
    ' Effects:
    ' Inputs:
    ' Outputs:
    ' Returns:
    '    OpenModalFullTextSearchWindow - The return for the OnSearchResultsItemClick event
    ' Implements Agent: None
    '
    ' Revision # Date            Author  Description
    ' ---------- ----            ------  -----------
    ' 1.0        10/30/2001      DY      Initial version
    '-------------------------------------------------------------------------------------------
    Function OpenModalFullTextSearchWindow()
        Dim blnUseOpenRecordLogic
        Dim vntRecordId
        Dim vntTableId
        Dim rduSecurity
        Dim vntFormId

        OpenModalFullTextSearchWindow = True
        blnUseOpenRecordLogic = False
        With UIMaster.RUICenter.SearchEventObj
            .searchSource.List.Recordset.Move(.rowIndex - 1, 1) ' 1= adBookMarkFirst
            If .ColumnName = "" Then
                blnUseOpenRecordLogic = True
            ElseIf .Field.DataDictionary.Type = 3 Then 'metaId
                blnUseOpenRecordLogic = True
            End If
            If blnUseOpenRecordLogic = True Then 'A record Id (either PK or FK) was clicked
                vntRecordId = .Value
                vntTableId = .TargetTable.TableId
                rduSecurity = UIMaster.RSysClient.GetUserSecurity
                vntFormId = rduSecurity.DefaultFormId(vntTableId)
                ' This method does not works, so that we have to use ShowFormModal
                'UIMaster.ShowFormByTable actionAskUser, vntTableId, vntRecordId, System.DBNull.Value
                UIMaster.ShowFormModal(vntFormId, vntRecordId, System.DBNull.Value)
            Else 'An email, fax, URL or unknown type link was selected
                If .Field.DataDictionary.DataAttribute = 3 Then 'metaTextEmail
                    OpenModalFullTextSearchWindow = SearchResultsEmailLinkClickHandler()
                ElseIf .Field.DataDictionary.DataAttribute = 2 Then 'metaTextFaxPhone
                    OpenModalFullTextSearchWindow = False
                ElseIf .Field.DataDictionary.DataAttribute = 4 Then 'metaTextWebSite
                    OpenModalFullTextSearchWindow = False
                Else
                    OpenModalFullTextSearchWindow = False
                End If
            End If
        End With

    End Function


    '-------------------------------------------------------------------------------------------
    ' Name:     CSSFullTextSearch
    ' Purpose:  Prepares a full text (in-context) search of the support incident,
    '           issues, and knowledge base search sources.  This global script will be called
    '           by any modal or modeless form where the search text will passed in to this
    '           script as defaulted text for which to search.  The search is displayed in
    '           a modeless or modal window dependent on the parent form.
    ' Assumptions:
    ' Effects:
    ' Inputs:
    '    strSearchText - The text willbe used as default search text
    ' Outputs:
    ' Returns:
    ' Implements Agent: None
    '
    ' Revision # Date            Author  Description
    ' ---------- ----            ------  -----------
    ' 1.0        10/30/2001      DY      Initial version
    '                                    move the search functionality to this global script
    '                                    for the better maitainance and fix the issue
    '                                    Faled for opening full text search modeless window from
    '                                    modal form.
    '-------------------------------------------------------------------------------------------
    Sub CSSFullTextSearch(ByVal strSearchText)
        Dim objSearchFactory
        Dim ruiModelessWindowDef
        Dim vntParameters
        Dim objParam

        With UIMaster

            If .RUICenter.IsModal Then
                ' We can not use ShowCenterReference to open a modal full text search window
                ' because this method will replace the modal form's center where the search
                ' button clicked. We have to open another modal form then replcase the RUIcenter
                ' with the full text search object objSearchFactory
                '.ShowCenterReference actionAskUser, objSearchFactory, System.DBNull.Value
                objParam = .Master.Global.CreateTransitPointParamsObj()
                objParam.SetUserDefParam(1, strSearchText)
                vntParameters = objParam.ConstructParams
                .ShowFormModal("Temp Incident", System.DBNull.Value, vntParameters)
            Else
                objSearchFactory = UIMaster.CreateCenterReference("search")
                With objSearchFactory
                    .SearchType = 5 'searchTypeInContext
                    If Not IsDBNull(strSearchText) Then .SearchText = strSearchText
                    .Options.FTMatchMethod = 0 ' FTMatchSmart
                    .Options.UseSearchSource("Support Incidents")
                    .Options.UseSearchSource("Issues")
                    .Options.UseSearchSource("Knowledge Base")
                    If Not (IsDBNull(strSearchText)) Then
                        .Options.AutoRun = True
                    Else
                        .Options.AutoRun = False
                    End If
                End With
                ruiModelessWindowDef = UIMaster.CreateModelessWindowDef
                With ruiModelessWindowDef
                    .menuAppearance = 1     ' 1= modelessMenuNone
                    .Size = 2               ' 2 = modelessSizeSpecific
                    .position = 1           ' 1= modelessPositionTopCenter
                    .Height = CLng(0.84999999999999998 * window.Screen.availHeight)
                    .Width = CLng(0.94999999999999996 * window.Screen.availWidth)
                End With
                .ShowCenterReferenceModeless(RDAUILib.ActionTypeEnum.actionAskUser, objSearchFactory, ruiModelessWindowDef, System.DBNull.Value)
            End If
        End With

    End Sub



    Dim gobjCMSError

    ' ------------------------------------------------------------------------------------------
    ' Name:    CMSErrorInitialize
    ' Purpose: This global script initialize the CMS error object to strore the error info about
    '          the error object passed in.
    ' ------------------------------------------------------------------------------------------
    ' Inputs:
    '   objErr      - The VBScript Error object
    ' Returns:
    ' History:
    ' Revision#     Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 4.0           08/01/2001  DY      Initial version
    ' ------------------------------------------------------------------------------------------
    Sub CMSErrorInitialize(ByVal objErr)
        If objErr.Number = 0 Then
            gobjCMSError = Nothing
        Else
            gobjCMSError = New CMSErrorObject
            gobjCMSError.Initialize(objErr)
        End If
    End Sub

    ' ------------------------------------------------------------------------------------------
    ' Name:    CMSErrorHandling
    ' Purpose: This global script is used for error handling in the transpoint methods.
    ' ------------------------------------------------------------------------------------------
    ' Inputs:
    ' Returns:
    ' History:
    ' Revision#     Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 4.0           08/01/2001  DY      Initial version
    ' ------------------------------------------------------------------------------------------
    Sub CMSErrorHandling()
        Const lngERR_APPDEV_START_NUMBER = 10000
        Const lngERR_APPDEV_END_NUMBER = 29999
        Const lngERR_CMS_EXPAN_START_NUMBER = 40000
        Const lngERR_CMS_EXPAN_END_NUMBER = 49151
        Const lngERR_SERVER_START_NUMBER = 49152
        Const lngERR_SERVER_END_NUMBER = 53247

        Const E_RDA_REQUIRED_REFRESH = -2147172309
        Const E_RAD_SPECIAL_ERROR_NUMBER = -2147205071   ' vbObjectError + 16433
        Dim strWarning
        Dim strRefresh
        Dim lngError

        On Error GoTo 0
        If gobjCMSError Is Nothing Then Exit Sub
        lngError = gobjCMSError.Number
        If lngError = 0 Then
            If UIMaster.RUICenter.SaveCanceled Or UIMaster.RUICenter.DeleteCanceled Then
                lngError = E_RAD_SPECIAL_ERROR_NUMBER
            Else
                Exit Sub
            End If
        ElseIf lngError = E_RDA_REQUIRED_REFRESH Then
            UIMaster.RUICenter.SaveCanceled = True
            On Error Resume Next
            With UIMaster.RSysClient.GetLDGroup("Common")
                strWarning = .GetText("Warning")
                strRefresh = .GetText("Refresh")
                If IsNothing(strWarning) Then strWarning = "Warning"
                If IsNothing(strRefresh) Then strWarning = "Do you want to refresh?"
            End With
            If CMSMsgBox(gobjCMSError.Description & vbCrLf & strRefresh, vbOKCancel, strWarning) = vbOK Then
                UIMaster.RUICenter.Reload()
                Exit Sub
            End If
            On Error GoTo 0
            lngError = E_RAD_SPECIAL_ERROR_NUMBER
        ElseIf lngError >= vbObjectError + lngERR_SERVER_START_NUMBER And lngError <= vbObjectError + lngERR_SERVER_END_NUMBER Then
            lngError = gobjCMSError.Number
        ElseIf lngError >= vbObjectError + lngERR_APPDEV_START_NUMBER And lngError < vbObjectError + lngERR_APPDEV_END_NUMBER _
            Or lngError >= vbObjectError + lngERR_CMS_EXPAN_START_NUMBER And lngError <= vbObjectError + lngERR_CMS_EXPAN_END_NUMBER Then
            If lngError <> E_RAD_SPECIAL_ERROR_NUMBER Then
                CMSMsgBox(gobjCMSError.Description, vbOKOnly, "")
                lngError = E_RAD_SPECIAL_ERROR_NUMBER
            End If
        Else
            lngError = gobjCMSError.Number
        End If
        UIMaster.RUICenter.SaveCanceled = True
        UIMaster.RUICenter.DeleteCanceled = True
        Err.Raise(lngError, gobjCMSError.Source, gobjCMSError.Description, gobjCMSError.HelpFile, gobjCMSError.HelpContext)

    End Sub

    ' ------------------------------------------------------------------------------------------
    ' Name:    ShowSystemErrorMessage
    ' Purpose: This script will show any system error
    ' ------------------------------------------------------------------------------------------
    ' Inputs:
    ' Returns:
    ' History:
    ' Revision#     Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 4.0           08/31/2001  DY      Initial version
    ' ------------------------------------------------------------------------------------------
    Sub ShowSystemErrorMessage(ByVal objErr)
        Const lngERR_APPDEV_START_NUMBER = 10000
        Const lngERR_APPDEV_END_NUMBER = 29999
        Const lngERR_CMS_EXPAN_START_NUMBER = 40000
        Const lngERR_CMS_EXPAN_END_NUMBER = 49151
        Const lngERR_SERVER_START_NUMBER = 49152
        Const lngERR_SERVER_END_NUMBER = 53247

        Const E_RAD_SPECIAL_ERROR_NUMBER = -2147205071   ' vbObjectError + 16433
        Dim lngError
        Dim strMsg
        If objErr Is Nothing Then Exit Sub
        lngError = objErr.Number
        strMsg = objErr.Description
        On Error Resume Next
        If lngError >= vbObjectError + lngERR_SERVER_START_NUMBER And lngError <= vbObjectError + lngERR_SERVER_END_NUMBER Then
            If lngError <> E_RAD_SPECIAL_ERROR_NUMBER Then
                CMSMsgBox(strMsg, vbOKOnly, "")
            End If
        ElseIf lngError >= vbObjectError + lngERR_APPDEV_START_NUMBER And lngError < vbObjectError + lngERR_APPDEV_END_NUMBER _
            Or lngError >= vbObjectError + lngERR_CMS_EXPAN_START_NUMBER And lngError <= vbObjectError + lngERR_CMS_EXPAN_END_NUMBER Then
        Else
            CMSMsgBox(strMsg, vbOKOnly, "")
        End If

    End Sub


    Sub OnMenuExpanded(ByVal index, ByVal connectionTitle)

        Const strgSUPPORT_INCIDENT = "Support Incident"
        Const strdSUPPORT_INCIDENT_CONNECTION_TITLE = "Support Incidents"
        Const strNEW_INCIDENT_E_TAB_CLICK_SCRIPT = "Incident_New_E-tab_Click"

        Select Case connectionTitle
            Case UIMaster.RSysClient.GetLDGroup(strgSUPPORT_INCIDENT).GetText(strdSUPPORT_INCIDENT_CONNECTION_TITLE)
                UIMaster.RUIMenu.AttachEventHookScriptToMenuItem(strNEW_INCIDENT_E_TAB_CLICK_SCRIPT, "onclick", UIMaster.RUIMenu.Connections(CInt(index)).ConnectionId, 3)
        End Select

    End Sub




    ' ------------------------------------------------------------------------------------------
    ' Name :    GetForeignFieldText
    ' Purpose:  This global function returns a forign filed text value
    ' ------------------------------------------------------------------------------------------
    ' Inputs:
    '   strTab     - The tab name where the forign field locates
    '   strSegment - The segement name where the forign field locates
    '   strField   - The field name
    ' Returns:
    '   GetForeignFieldText - The text value of the specified Forign field
    ' History:
    ' Reversion#    Date        Author  Description
    ' ----------    ----        ------  -----------
    '               07/29/2002  DY      Initial version
    ' ------------------------------------------------------------------------------------------
    Public Function GetForeignFieldText(ByVal strTab, ByVal strSegment, ByVal strField)
        Dim rduRecord

        On Error Resume Next

        If IsDBNull(strTab) Then strTab = ""
        If Len(strTab) = 0 Then
            rduRecord = UIMaster.RUICenter.GetForeignField(strSegment, strField)
        Else
            rduRecord = UIMaster.RUICenter.GetForeignFieldEx(strTab, strSegment, strField)
        End If

        If Err.Number <> 0 Then
            Err.Clear()
            GetForeignFieldText = ""
        Else
            GetForeignFieldText = rduRecord.TextValue
        End If

    End Function


    ' ------------------------------------------------------------------------------------------
    ' Name :    GetForeignFieldTextEx
    ' Purpose:  This global function returns a forign filed text value.  However, the caller only
    '           need to pass the field name.
    ' ------------------------------------------------------------------------------------------
    ' Inputs:
    '   strField   - The field name
    ' Returns:
    '   GetForeignFieldTextEx - The text value of the specified Forign field
    ' History:
    ' Reversion#    Date        Author  Description
    ' ----------    ----        ------  -----------
    '               07/29/2002  DY      Initial version
    ' ------------------------------------------------------------------------------------------
    Public Function GetForeignFieldTextEx(ByVal strField)
        Dim rduRecord
        Dim rdaTab
        Dim rdaSegment
        Dim rdaFormField
        Dim rdaForm
        Dim strTab
        Dim strSegment

        On Error Resume Next

        rdaForm = UIMaster.RUICenter.Form

        GetForeignFieldTextEx = ""
        If rdaForm.Tabs.Count = 0 Then
            For Each rdaSegment In rdaForm.Segments
                For Each rdaFormField In rdaSegment.FormFields
                    If rdaFormField.FieldName = strField Then
                        strSegment = rdaSegment.SegmentName
                        rduRecord = UIMaster.RUICenter.GetForeignField(strSegment, strField)
                        GetForeignFieldTextEx = rduRecord.TextValue
                        If Err.Number <> 0 Then Err.Clear()
                        Exit Function
                    End If
                Next
            Next
        Else
            For Each rdaTab In rdaForm.Tabs
                For Each rdaSegment In rdaTab.Segments
                    For Each rdaFormField In rdaSegment.FormFields
                        If rdaFormField.FieldName = strField Then
                            strTab = rdaTab.TabName
                            strSegment = rdaSegment.SegmentName
                            rduRecord = UIMaster.RUICenter.GetForeignFieldEx(strTab, strSegment, strField)
                            GetForeignFieldTextEx = rduRecord.TextValue
                            If Err.Number <> 0 Then Err.Clear()
                            Exit Function
                        End If
                    Next
                Next
            Next
        End If
        If Err.Number <> 0 Then Err.Clear()

    End Function

    ' ------------------------------------------------------------------------------------------
    ' Name :    GetDisconnectedFieldNameEx
    ' Purpose:  This global function returns a forign filed text value.  However, the caller only
    '           need to pass the field name.
    ' ------------------------------------------------------------------------------------------
    ' Inputs:
    '   strField   - The field name
    ' Returns:
    '   GetForeignFieldTextEx - The text value of the specified Forign field
    ' History:
    ' Reversion#    Date        Author  Description
    ' ----------    ----        ------  -----------
    '               07/29/2002  DY      Initial version
    ' ------------------------------------------------------------------------------------------
    Public Function GetDisconnectedFieldNameEx(ByVal strFieldOrTitle)
        Dim rduRecord
        Dim rdaTab
        Dim rdaSegment
        Dim rdaFormField
        Dim rdaForm

        On Error Resume Next

        rdaForm = UIMaster.RUICenter.Form

        GetDisconnectedFieldNameEx = ""
        If rdaForm.Tabs.Count = 0 Then
            For Each rdaSegment In rdaForm.Segments
                For Each rdaFormField In rdaSegment.FormFields
                    If rdaFormField.FieldName = strFieldOrTitle Or rdaFormField.FormFieldTitle = strFieldOrTitle Then
                        GetDisconnectedFieldNameEx = rdaFormField.DisconnectedName
                        If Err.Number <> 0 Then Err.Clear()
                        Exit Function
                    End If
                Next
            Next
        Else
            For Each rdaTab In rdaForm.Tabs
                For Each rdaSegment In rdaTab.Segments
                    For Each rdaFormField In rdaSegment.FormFields
                        If rdaFormField.FieldName = strFieldOrTitle Or rdaFormField.FormFieldTitle = strFieldOrTitle Then
                            GetDisconnectedFieldNameEx = rdaFormField.DisconnectedName
                            If Err.Number <> 0 Then Err.Clear()
                            Exit Function
                        End If
                    Next
                Next
            Next
        End If
        If Err.Number <> 0 Then Err.Clear()

    End Function


    ' ------------------------------------------------------------------------------------------
    ' Name :    GetComboChoiceText
    ' Purpose:  This global function returns a combo choice text based on the base string
    ' ------------------------------------------------------------------------------------------
    ' Inputs:
    '   strBaseString - The Base Strin of the choice in the combo
    '   strFieldName  - The field name
    '   strTableName  - The tbale Name, if it is empty, the current form's table name will be used.
    ' Returns:
    '   GetComboChoiceText- The text value of the specified Combo field
    ' History:
    ' Reversion#    Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 5.0           02/21/2003  DY      Initial version
    ' ------------------------------------------------------------------------------------------
    Function GetComboChoiceText(ByVal strBaseString, ByVal strFieldName, ByVal strTableName)
        Dim strTable
        On Error Resume Next
        GetComboChoiceText = ""
        If IsDBNull(strTableName) Then strTableName = ""
        If Len(strTableName) = 0 Then strTableName = UIMaster.RUICenter.Form.Table.TableName
        With UIMaster.RSysClient.GetTable(strTableName).Fields(strFieldName)
            GetComboChoiceText = .Choices(.Choices.GetIndexForBaseString(strBaseString))
        End With
        If Err.Number <> 0 Then
            Err.Clear()
            GetComboChoiceText = ""
        End If
    End Function

    '**********************************************************
    'Name: OnEditActivity
    'Purpose: Called by the Calendar, OnEditActivity displays an existing activity
    ' record in the appropriate form in a modal window. 
    'Assumptions: 
    'Effects: 
    'Inputs: vntActivityId - a Variant byte array storing the eRelationship Id of the Rn_Appointments record to display
    'Outputs:
    'Returns: 
    '
    'Revision # Date                   Author  Description
    '---------- ----                         ------     -----------
    '1.0        September 5, 2001 TS         Initial version
    '**********************************************************
    Sub OnEditActivity(ByVal vntActivityId)

        Const strrTEMP_ACTIVITY = "Temp Activity"

        UIMaster.ShowFormModal(strrTEMP_ACTIVITY, vntActivityId, System.DBNull.Value)
    End Sub

    '**********************************************************
    'Name: OnNewAppointment
    'Purpose: Called by the Calendar, OnNewAppointment sets default values for the Appt_Date, 
    ' Start_Time, Activity_Completed_Date, End_Time and Rn_Employee_Id fields in the parameter 
    ' list.  A new activity form, based on the provided activity type, is displayed in a modal window. 
    ' OnNewAppointment returns the result of the user's action with the form - either a record Id, 
    ' if the record was saved or null, if the user canceled - to the calling Calendar function.
    'Assumptions: 
    'Effects: 
    'Inputs: intActivityType - an integer representing the activity type.  
    '            dtmStartDateTime - a VBScript Date object used in defaulting the activity start date (Appt_Date) and time (Start_Time).
    '            dtmEndDateTime - a VBScript Date object used in defaulting the activity end date (Activity_Completed_Date) and time (End_Time).
    '            vntAssignedToEmployeeId - a Variant byte array storing the eRelationship Id of the employee record to 
    '              default as the Rn_Employee_Id (i.e. assigned to employee).  If there is no employee record associated with a user, 
    '              then the employeeId passed is null.
    '            vntAssignedByEmployeeId - a Variant byte array storing the eRelationship Id of the employee record to 
    '              default as the Assigned_By (i.e. assigned by employee).  If there is no employee record associated with a user, 
    '              then the employeeId passed is null.
    'Outputs:
    'Returns: a Byte array containing a record Id if a new record was added or null if a record was not added.
    '
    'Revision # Date                   Author  Description
    '---------- ----                         ------     -----------
    '1.0        September 27, 2001 TS         Initial version
    '**********************************************************
    Function OnNewAppointment(ByVal intActivityType, ByVal dtmStartDateTime, ByVal dtmEndDateTime, ByVal vntAssignedToEmployeeId, ByVal vntAssignedByEmployeeId)

        Const strfAPPT_DATE = "Appt_Date"
        Const strfSTART_TIME = "Start_Time"
        Const strfACTIVITY_COMPLETED_DATE = "Activity_Completed_Date"
        Const strfEND_TIME = "End_Time"
        Const strfRN_EMPLOYEE_ID = "Rn_Employee_Id"
        Const strfASSIGNED_BY = "Assigned_By"

        Dim strFormName
        Dim objParams, vntParameters
        Dim vntReturns

        objParams = CreateTransitPointParamsObj()
        If Not (IsDBNull(dtmStartDateTime)) Then
            If IsDate(dtmStartDateTime) Then
                objParams.AddDefaultField(strfAPPT_DATE, DateValue(dtmStartDateTime))
                objParams.AddDefaultField(strfSTART_TIME, TimeValue(dtmStartDateTime))
            End If
        End If
        If Not (IsDBNull(dtmEndDateTime)) Then
            If IsDate(dtmEndDateTime) Then
                objParams.AddDefaultField(strfEND_TIME, TimeValue(dtmEndDateTime))
            End If
        End If
        objParams.AddDefaultField(strfRN_EMPLOYEE_ID, vntAssignedToEmployeeId)
        objParams.AddDefaultField(strfASSIGNED_BY, vntAssignedByEmployeeId)
        vntParameters = objParams.ConstructParams()

        strFormName = MapActivityTypeToFormName(intActivityType)
        If strFormName <> "" Then
            vntReturns = UIMaster.ShowFormModal(strFormName, System.DBNull.Value, vntParameters)
            If (vntReturns(0) = 1) Then
                OnNewAppointment = vntReturns(1)
            Else
                OnNewAppointment = System.DBNull.Value
            End If
        Else
            OnNewAppointment = System.DBNull.Value
        End If

    End Function

    '**********************************************************
    'Name: OnNewTask
    'Purpose: Called by the Calendar, OnNewTask sets default values for the Appt_Date &
    ' Rn_Employee_Id fields in the parameter list.  A new activity form, based on the provided 
    ' activity type, is displayed in a modal window. OnNewTask returns the 
    ' result of the user's action with the form - either a record Id, if the record was saved
    ' or System.DBNull.Value, if the user canceled - to the calling Calendar function.
    'Assumptions: 
    'Effects: 
    'Inputs: intActivityType - an integer representing the activity type.  
    '             dtmStartDate - a VBScript Date object used in defaulting the activity start  date (Appt_Date).
    '            vntAssignedToEmployeeId - a Variant byte array storing the eRelationship Id of the employee record to 
    '              default as the Rn_Employee_Id (i.e. assigned to employee).  If there is no employee record associated with a user, 
    '              then the employeeId passed is null.
    '            vntAssignedByEmployeeId - a Variant byte array storing the eRelationship Id of the employee record to 
    '              default as the Assigned_By (i.e. assigned by employee).  If there is no employee record associated with a user, 
    '              then the employeeId passed is null.
    'Outputs:
    'Returns: a Byte array containing a record Id if a new record was added or null if a record was not added.
    '
    'Revision # Date                   Author  Description
    '---------- ----                         ------     -----------
    '1.0        September 27, 2001 TS         Initial version
    '**********************************************************
    Function OnNewTask(ByVal intActivityType, ByVal dtmStartDate, ByVal vntAssignedToEmployeeId, ByVal vntAssignedByEmployeeId)

        Const strfAPPT_DATE = "Appt_Date"
        Const strfRN_EMPLOYEE_ID = "Rn_Employee_Id"
        Const strfASSIGNED_BY = "Assigned_By"

        Dim strFormName
        Dim objParams, vntParameters
        Dim vntReturns

        objParams = CreateTransitPointParamsObj()
        objParams.AddDefaultField(strfAPPT_DATE, dtmStartDate)
        objParams.AddDefaultField(strfRN_EMPLOYEE_ID, vntAssignedToEmployeeId)
        objParams.AddDefaultField(strfASSIGNED_BY, vntAssignedByEmployeeId)
        vntParameters = objParams.ConstructParams()

        strFormName = MapActivityTypeToFormName(intActivityType)
        If strFormName <> "" Then
            vntReturns = UIMaster.ShowFormModal(strFormName, System.DBNull.Value, vntParameters)
            If (vntReturns(0) = 1) Then
                OnNewTask = vntReturns(1)
            Else
                OnNewTask = System.DBNull.Value
            End If
        Else
            OnNewTask = System.DBNull.Value
        End If
    End Function

    '**********************************************************
    'Name: OnNewActivity
    'Purpose: Called by the Calendar, OnNewActivity sets default values for the Appt_Date &
    ' Rn_Employee_Id fields in the parameter list.  NewActivity2 is called to allow the user to choose 
    ' an activity type (using a dialog box) and create a new record. OnNewActivity returns the 
    ' result of NewActivity2 (either a record Id) or null to the calling Calendar function.
    'Assumptions: 
    'Effects: 
    'Inputs: dtmStartDate - a VBScript Date object used in defaulting the activity start  date (Appt_Date).
    '            vntAssignedToEmployeeId - a Variant byte array storing the eRelationship Id of the employee record to 
    '              default as the Rn_Employee_Id (i.e. assigned to employee).  If there is no employee record associated with a user, 
    '              then the employeeId passed is null.
    '            vntAssignedByEmployeeId - a Variant byte array storing the eRelationship Id of the employee record to 
    '              default as the Assigned_By (i.e. assigned by employee).  If there is no employee record associated with a user, 
    '              then the employeeId passed is null.
    'Outputs:
    'Returns: a Byte array containing a record Id if a new record was added or null if a record was not added.
    '
    'Revision # Date                   Author  Description
    '---------- ----                         ------     -----------
    '1.0        September 27, 2001 TS         Initial version
    '**********************************************************
    Function OnNewActivity(ByVal dtmStartDate, ByVal vntAssignedToEmployeeId, ByVal vntAssignedByEmployeeId)

        Const strfAPPT_DATE = "Appt_Date"
        Const strfRN_EMPLOYEE_ID = "Rn_Employee_Id"
        Const strfASSIGNED_BY = "Assigned_By"

        Dim objParams, vntParameters

        objParams = CreateTransitPointParamsObj()
        objParams.AddDefaultField(strfAPPT_DATE, dtmStartDate)
        objParams.AddDefaultField(strfRN_EMPLOYEE_ID, vntAssignedToEmployeeId)
        objParams.AddDefaultField(strfASSIGNED_BY, vntAssignedByEmployeeId)
        vntParameters = objParams.ConstructParams()
        OnNewActivity = NewActivity2(True, vntParameters)
    End Function

    '**********************************************************
    'Name: NewActivity2
    'Purpose: Create a dialog box with which the user will select an activity type.  
    ' After an activity type is selected, a new form is displayed for the selected activity type.
    'Assumptions: 
    'Effects: 
    'Inputs: blnShowModal - a boolean indicating whether the new form should be displayed in a modal window
    '             vntParameters - CMS standard transit point parameters list.  Useful for defaulting field values in the new form
    'Outputs:
    'Returns: a Byte array containing a record Id if a new record was added or null if a record was not added.
    '
    'Revision # Date                   Author  Description
    '---------- ----                         ------     -----------
    '1.0        September 5, 2001 TS         Initial version
    '**********************************************************
    Function NewActivity2(ByVal blnShowModal, ByVal vntParameters)

        Const intAPP_TYPE_MEETING = 0
        Const intAPP_TYPE_TO_DO = 1
        Const intAPP_TYPE_CALL = 2
        Const intAPP_TYPE_MESSAGE = 3
        Const intAPP_TYPE_LITERATURE = 4
        Const intAPP_TYPE_NOTE = 5

        Dim intActivityType
        Dim strFormName
        Dim strBtnTexts
        Dim intAppTypes
        Dim strHTMLText
        Dim strTitle
        Dim vntReturns

        ' Get activity type from user
        With UIMaster.RSysClient.GetLDGroup("Activity Management")
            strHTMLText = .GetText("Select Activity Type")
            strTitle = .GetText("New Activity")
            strBtnTexts = New Object() {.GetText("Call Button"), .GetText("Literature Fulfillment Button"), _
                .GetText("Meeting Button"), .GetText("Message Button"), .GetText("Note Button"), _
                .GetText("To-Do Button")}
            intAppTypes = New Object() {intAPP_TYPE_CALL, intAPP_TYPE_LITERATURE, intAPP_TYPE_MEETING, _
                intAPP_TYPE_MESSAGE, intAPP_TYPE_NOTE, intAPP_TYPE_TO_DO}
        End With

        ' Get user choice button
        intActivityType = CMSDialogRadio(strHTMLText, strTitle, strBtnTexts) - 1
        If intActivityType < 0 Then
            NewActivity2 = System.DBNull.Value
            Exit Function
        End If
        ' Transfer to the real activity type
        intActivityType = intAppTypes(intActivityType)

        strFormName = MapActivityTypeToFormName(intActivityType)

        If blnShowModal = True Then
            vntReturns = UIMaster.ShowFormModal(strFormName, System.DBNull.Value, vntParameters)
            If (vntReturns(0) = 1) Then
                NewActivity2 = vntReturns(1)
            Else
                NewActivity2 = System.DBNull.Value
            End If
        Else
            UIMaster.ShowForm(2, strFormName, System.DBNull.Value, vntParameters)
            NewActivity2 = System.DBNull.Value
        End If

    End Function

    '**********************************************************
    'Name: MapActivityTypeToFormName
    'Purpose: Given an activity type, returns the name of the associated Activity form.
    'Assumptions: 
    'Effects: 
    'Inputs: intActivityType - an integer representing the activity type
    'Outputs:
    'Returns: a string representing the name of the associated form
    '
    'Revision # Date                   Author  Description
    '---------- ----                         ------     -----------
    '1.0        September 5, 2001 TS         Initial version
    '**********************************************************
    Function MapActivityTypeToFormName(ByVal intActivityType)

        Const intAPP_TYPE_MEETING = 0
        Const intAPP_TYPE_TO_DO = 1
        Const intAPP_TYPE_CALL = 2
        Const intAPP_TYPE_MESSAGE = 3
        Const intAPP_TYPE_LITERATURE = 4
        Const intAPP_TYPE_NOTE = 5

        Const strrMEETING = "Meeting"
        Const strrTO_DO = "To-Do"
        Const strrCALL = "Call"
        Const strrMESSAGE = "Message"
        Const strrLITERATURE = "Literature Fulfillment"
        Const strrNOTE = "Note"

        Dim strFormName

        Select Case intActivityType
            Case intAPP_TYPE_MEETING
                strFormName = strrMEETING
            Case intAPP_TYPE_TO_DO
                strFormName = strrTO_DO
            Case intAPP_TYPE_CALL
                strFormName = strrCALL
            Case intAPP_TYPE_MESSAGE
                strFormName = strrMESSAGE
            Case intAPP_TYPE_LITERATURE
                strFormName = strrLITERATURE
            Case intAPP_TYPE_NOTE
                strFormName = strrNOTE
            Case Else
                strFormName = ""
        End Select
        MapActivityTypeToFormName = strFormName
    End Function

    '**********************************************************
    'Name: URLEncodeParameter
    'Purpose: Given an intended URL (query string) parameter, encodes it so 
    ' that it's safe to put in an URL
    'Assumptions: 
    'Effects: 
    'Inputs: strURLParameter - a string value that is to be placed in a URL
    'Outputs:
    'Returns: a URL safe string
    '
    'Revision # Date                   Author  Description
    '---------- ----                         ------     -----------
    '1.0        September 27, 2001 TS         Initial version
    '**********************************************************
    Function URLEncodeParameter(ByVal strURLParameter)

        Dim intCounter
        Dim strEncodedURLParameter

        strEncodedURLParameter = ""
        For intCounter = 1 To Len(strURLParameter)
            If (Mid(strURLParameter, intCounter, 1) >= "A" And Mid(strURLParameter, intCounter, 1) <= "Z") Or _
            (Mid(strURLParameter, intCounter, 1) >= "a" And Mid(strURLParameter, intCounter, 1) <= "z") Or _
            (Mid(strURLParameter, intCounter, 1) >= "0" And Mid(strURLParameter, intCounter, 1) <= "9") Then
                strEncodedURLParameter = strEncodedURLParameter & Mid(strURLParameter, intCounter, 1)
            Else
                strEncodedURLParameter = strEncodedURLParameter & "%" & CStr(Hex(Asc(Mid(strURLParameter, intCounter, 1))))
            End If
        Next
        URLEncodeParameter = strEncodedURLParameter
    End Function

    ' -------------------------------------------------------------------------------------------------
    ' Name:    StartEmail
    ' Purpose: Start an e-mail message via Outlook, Lotus Note, or active form
    ' -------------------------------------------------------------------------------------------------
    ' Inputs:
    '   strTo        : The recipient of the email.
    '   strSubject   : The subject of the email
    '   strBody      : The body of the email
    '   strTableName : The table name the email based on.
    '   vntRecordId  : The record Id in the table the email based on.
    ' Returns:
    ' History:
    ' Reversion#    Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 5.0           12/06/2002  DY      Fix Issue 27430: Send email does not work for Lotus Note
    '                                   Move the same functionality to global script in order to easy
    '                                   to maintain.
    ' -------------------------------------------------------------------------------------------------
    Sub StartEmail(ByVal strTo, ByVal strSubject, ByVal strBody, ByVal strTableName, ByVal vntRecordId)
        Const strfEMAIL_TO = "Email_To"
        Const strfSUBJECT = "Subject"
        Const strfBODY = "Body"
        Const strOUTBOUND_EMAIL = "Outbound Email"

        Dim objParams
        Dim vntParameters
        Dim vntTableId

        vntTableId = UIMaster.RSysClient.GetTable(strTableName).TableId

        If UIMaster.MailtoIsOutlook2000 Then
            UIMaster.StartOutlookMailToEx(strTo, strSubject, strBody, vntTableId, vntRecordId)
        ElseIf UIMaster.MailtoIsLotus5 Then
            ' DY 12/06/2002  Fix Issue 27430: Send email does not work for Lotus Note
            UIMaster.StartLotusMailTo(strTo, strSubject, strBody, vntTableId, vntRecordId)
        Else
            objParams = CreateTransitPointParamsObj()
            objParams.AddDefaultField(strfEMAIL_TO, strTo)
            objParams.AddDefaultField(strfSUBJECT, strSubject)
            objParams.AddDefaultField(strfBODY, strBody)
            objParams.SetUserDefParam(1, strTableName)
            objParams.SetUserDefParam(2, vntRecordId)
            vntParameters = objParams.ConstructParams()
            UIMaster.ShowFormModal(strOUTBOUND_EMAIL, System.DBNull.Value, vntParameters)
        End If
    End Sub



    ' -------------------------------------------------------------------------------------------------
    ' Name:    OnEmailLabelClick
    ' Purpose: Thsi script is fired when user click an email label
    ' -------------------------------------------------------------------------------------------------
    ' Inputs:
    ' Returns:
    ' History:
    ' Reversion#    Date        Author  Description
    ' ----------    ----        ------  -----------
    ' 5.0           12/06/2002  DY      Fix Issue 27430: Send email does not work for Lotus Note
    ' -------------------------------------------------------------------------------------------------
    Function OnEmailLabelClick()
        On Error Resume Next
        ' JC @6/14/2002 fixed bug: send email in Active Access does not create comm log entry when using Lotus Notes
        If UIMaster.MailtoIsOutlook2000 Then
            OnEmailLabelClick = False
        ElseIf UIMaster.MailtoIsLotus5 Then
            OnEmailLabelClick = False
        Else
            Dim objParams
            Dim vntParameters
            Dim objFormContext
            Dim strTableName
            Dim vntId
            objFormContext = UIMaster.RUICenter.FormEventObj
            objParams = CreateTransitPointParamsObj()
            objParams.AddDefaultField("Email_To", UIMaster.RUICenter.PrimaryRecordset.Fields(objFormContext.FieldName).Value)
            vntId = UIMaster.RUICenter.recordId
            strTableName = UIMaster.RUICenter.Form.Table.TableName
            objParams.SetUserDefParam(1, strTableName)
            objParams.SetUserDefParam(2, vntId)
            vntParameters = objParams.ConstructParams()
            UIMaster.ShowFormModal("Outbound Email", System.DBNull.Value, vntParameters)
            OnEmailLabelClick = True
        End If
    End Function

    Function OnPhoneLabelClick()
        'Stub function for now.
    End Function

End Module
