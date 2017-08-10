VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "CustomDialogBox"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private m_vFormParamsMain
Private m_vFormParamsRadioButtons
Private m_dlgParamRadioButtonMain
Private m_strLabel
Private m_rfrmRadioTitle


'Name: CustomDialogBox
'Purpose: Alternative dialog box to the UIMaster default dialog box.  This one uses
'radio buttons as choices instead of hyperlinks.  This is useful if you are creating the
'dialog box at run-time, otherwise it's better to use Global.CMSDialogRadio ().
'

' Class Constructor
Private Sub Class_Initialize()
            Set m_vFormParamsMain = UIMaster.CreateFormParams()
            Set m_vFormParamsRadioButtons = UIMaster.CreateFormParams()
            Set m_dlgParamRadioButtonMain = UIMaster.CreateFormParam()
            
            m_dlgParamRadioButtonMain.Value = 0 'default to radio button 1.
            m_dlgParamRadioButtonMain.Type = 1024
            m_strLabel = ""
            
    Set m_rfrmRadioTitle = UIMaster.CreateFormParam()
    m_rfrmRadioTitle.Label = " "
    m_rfrmRadioTitle.Value = vbEmpty
    m_rfrmRadioTitle.Type = vbEmpty
    m_rfrmRadioTitle.Skip = True
    m_vFormParamsMain.AddItem m_rfrmRadioTitle
End Sub

'Class destructor
Private Sub Class_Terminate()
            Set m_vFormParamsMain = Nothing
            Set m_vFormParamsRadioButtons = Nothing
            Set m_dlgParamRadioButtonMain = Nothing
            Set m_rfrmRadioTitle = Nothing
End Sub

'This function must be called right after the class instantiation.
Public Sub SetDialogLabel(strText)
        m_strLabel = strText
End Sub

Public Sub SetDefaultRadioButton(iIndex)
        m_dlgParamRadioButtonMain.Value = iIndex
End Sub

Public Sub AddRadioButton(strChoiceText)
        Dim radioButton
        Set radioButton = UIMaster.CreateFormParam()
        radioButton.Label = strChoiceText
        radioButton.Value = vbEmpty
        radioButton.Type = vbEmpty
        m_vFormParamsRadioButtons.AddItem radioButton
End Sub

'Name: GetIRFormParams
'Returns: IRFormParams
Public Function GetIRFormParams()
    m_rfrmRadioTitle.Label = m_strLabel
        m_dlgParamRadioButtonMain.Enums = m_vFormParamsRadioButtons
        
        m_vFormParamsMain.AddItem m_dlgParamRadioButtonMain
        Set GetIRFormParams = m_vFormParamsMain
End Function

'Name: GetUserSelection
'Returns: The user selected radio button (zero-based).
Public Function GetUserSelectionIndex()
        GetUserSelectionIndex = m_dlgParamRadioButtonMain.Value
End Function

Public Function GetUserSelectionText()
        Dim radioButton
        Set radioButton = m_vFormParamsRadioButtons.Item(m_dlgParamRadioButtonMain.Value)
        GetUserSelectionText = radioButton.Label
End Function



