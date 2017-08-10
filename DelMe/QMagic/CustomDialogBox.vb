'***************************************************************
'* VBScript -> VB.NET change info:
'*  Class_Initialize -> Cls_Initialize; Sub New has been added
'*  Class_Terminate -> Cls_Terminate; Sub Finalize's been added
'*  Set obj = value -> obj = value
'***************************************************************
' Original code begins here

'Name: CustomDialogBox
'Purpose: Alternative dialog box to the UIMaster default dialog box.  This one uses
'radio buttons as choices instead of hyperlinks.  This is useful if you are creating the
'dialog box at run-time, otherwise it's better to use Global.CMSDialogRadio ().
'
Class CustomDialogBox
    Sub New()
        Cls_Initialize()
    End Sub

    Protected Overrides Sub Finalize()
        Cls_Terminate()
    End Sub

    ' Class Constructor
    Private Sub Cls_Initialize()
        m_vFormParamsMain = UIMaster.CreateFormParams()
        m_vFormParamsRadioButtons = UIMaster.CreateFormParams()
        m_dlgParamRadioButtonMain = UIMaster.CreateFormParam()

        m_dlgParamRadioButtonMain.Value = 0 'default to radio button 1.
        m_dlgParamRadioButtonMain.Type = 1024
        m_strLabel = ""

        m_rfrmRadioTitle = UIMaster.CreateFormParam()
        m_rfrmRadioTitle.Label = " "
        m_rfrmRadioTitle.Value = vbEmpty
        m_rfrmRadioTitle.Type = vbEmpty
        m_rfrmRadioTitle.Skip = True
        m_vFormParamsMain.AddItem(m_rfrmRadioTitle)
    End Sub

    'Class destructor
    Private Sub Cls_Terminate()
        m_vFormParamsMain = Nothing
        m_vFormParamsRadioButtons = Nothing
        m_dlgParamRadioButtonMain = Nothing
        m_rfrmRadioTitle = Nothing
    End Sub

    'This function must be called right after the class instantiation.
    Public Sub SetDialogLabel(ByVal strText)
        m_strLabel = strText
    End Sub

    Public Sub SetDefaultRadioButton(ByVal iIndex)
        m_dlgParamRadioButtonMain.Value = iIndex
    End Sub

    Public Sub AddRadioButton(ByVal strChoiceText)
        Dim radioButton
        radioButton = UIMaster.CreateFormParam()
        radioButton.Label = strChoiceText
        radioButton.Value = vbEmpty
        radioButton.Type = vbEmpty
        m_vFormParamsRadioButtons.AddItem(radioButton)
    End Sub

    'Name: GetIRFormParams
    'Returns: IRFormParams
    Public Function GetIRFormParams()
        m_rfrmRadioTitle.Label = m_strLabel
        m_dlgParamRadioButtonMain.Enums = m_vFormParamsRadioButtons

        m_vFormParamsMain.AddItem(m_dlgParamRadioButtonMain)
        GetIRFormParams = m_vFormParamsMain
    End Function

    'Name: GetUserSelection
    'Returns: The user selected radio button (zero-based).
    Public Function GetUserSelectionIndex()
        GetUserSelectionIndex = m_dlgParamRadioButtonMain.Value
    End Function

    Public Function GetUserSelectionText()
        Dim radioButton
        radioButton = m_vFormParamsRadioButtons.Item(m_dlgParamRadioButtonMain.Value)
        GetUserSelectionText = radioButton.Label
    End Function

    Private m_vFormParamsMain
    Private m_vFormParamsRadioButtons
    Private m_dlgParamRadioButtonMain
    Private m_strLabel
    Private m_rfrmRadioTitle
End Class