VERSION 5.00
Begin VB.Form frmPortal 
   Caption         =   "Dummy Portal (VB 6.0)"
   ClientHeight    =   3195
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   ScaleHeight     =   3195
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdTestScript 
      Caption         =   "Test Script"
      Height          =   375
      Left            =   960
      TabIndex        =   0
      Top             =   360
      Width           =   975
   End
End
Attribute VB_Name = "frmPortal"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub cmdTestScript_Click()
    Dim sFieldName
    Dim objParam
    Dim vntArgument

    '1. Find real name by title
    sFieldName = GetDisconnectedFieldNameEx(strfDISCONNECTED_COMPANY)
    
    If "" = sFieldName Then
        Exit Sub
    End If

    '2. Prepare call
    Set objParam = CreateTransitPointParamsObj()
    Call objParam.SetUserDefParam(1, UIMaster.RUICenter.PrimaryRecordset(sFieldName).Value)
    vntArgument = objParam.ConstructParams()
    ''vParams = "Microsoft" '...
    Call UIMaster.RUICenter.Form.Execute(strmGET_COMPANY_URL, vntArgument)

    '3. Process result
    Call MsgBox(objParam.GetUserDefParam(1, vntArgument))

End Sub

Sub OnBtnListContactsClicked()
    Dim sFieldName
    Dim objParam As TransitPointParams 'TODO: remove in script
    Dim vntArgument
    Dim tableId
    Dim selected
    Dim rs

    '1. Find real name by title
    sFieldName = GetDisconnectedFieldNameEx(strfDISCONNECTED_COMPANY)
    If "" = sFieldName Then
        Exit Sub
    End If

    '2. Prepare call
    objParam = CreateTransitPointParamsObj() 'TODO: prepend with Set in script
    Call objParam.SetUserDefParam(1, UIMaster.RUICenter.PrimaryRecordset(sFieldName).Value)
    vntArgument = objParam.ConstructParams()
    ''vParams = "Microsoft" '...
    Call UIMaster.RUICenter.Form.Execute(strmGET_COMPANY_CONTACTS, vntArgument)

    '3. Process result
    ''        Call MsgBox(objParam.GetUserDefParam(1, vntArgument))
    tableId = objParam.GetUserDefParam(1, vntArgument)
    rs = objParam.GetUserDefParam(2, vntArgument) 'TODO: prepend with Set in script
    If Not (rs Is Nothing) Then
        selected = UIMaster.ShowSelectionListModal(tableId, objParam.GetUserDefParam(2, vntArgument))
    Else
        Call MsgBox("Company is not found or has no contacts.")
    End If
End Sub
