VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "CMSErrorObject"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

' ------------------------------------------------------------------------------------------
' Name:    CMSErrorObject
' Purpose: This class is used to store info about the current VBscript Error object.
' ------------------------------------------------------------------------------------------
' Inputs:
' Returns:
' History:
' Revision#     Date        Author  Description
' ----------    ----        ------  -----------
' 4.0           08/01/2001  DY      Initial version
' ------------------------------------------------------------------------------------------
Dim mlngNumber
Dim mstrSource
Dim mstrDescription
Dim mstrHelpContext
Dim mstrHelpFile

Sub Initialize(objErr)
    mlngNumber = objErr.Number
    mstrSource = objErr.Source
    mstrDescription = objErr.Description
    mstrHelpContext = objErr.HelpContext
    mstrHelpFile = objErr.HelpFile
End Sub

Public Function Number()
    Number = mlngNumber
End Function

Public Function Source()
    Source = mstrSource
End Function

Public Function Description()
    Description = mstrDescription
End Function

Public Function HelpContext()
    HelpContext = mstrHelpContext
End Function

Public Function HelpFile()
    HelpFile = mstrHelpFile
End Function

Public Sub Clear()
    mlngNumber = 0
    mstrSource = ""
    mstrDescription = ""
    mstrHelpContext = ""
    mstrHelpFile = ""
End Sub
     
Private Sub Class_Initialize()
    mlngNumber = 0
    mstrSource = ""
    mstrDescription = ""
    mstrHelpContext = ""
    mstrHelpFile = ""
End Sub
