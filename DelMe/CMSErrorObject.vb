'***************************************************************
'* VBScript -> VB.NET change info:
'*  Class_Initialize -> Cls_Initialize; Sub New has been added
'*  Set obj = value -> obj = value
'***************************************************************
' Original code begins here

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
Class CMSErrorObject
    Dim mlngNumber
    Dim mstrSource
    Dim mstrDescription
    Dim mstrHelpContext
    Dim mstrHelpFile

    Sub Initialize(ByVal objErr)
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

    Sub New()
        Cls_Initialize()
    End Sub

    Private Sub Cls_Initialize()
        mlngNumber = 0
        mstrSource = ""
        mstrDescription = ""
        mstrHelpContext = ""
        mstrHelpFile = ""
    End Sub

End Class
