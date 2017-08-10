'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'' This module contains routines to be called via Form.DoSomething(...)
Option Explicit On 
Module Form
    ' --------------------------------------------------------------------------
    ' Name:    NewFormData
    ' Purpose: This function open 
    ' --------------------------------------------------------------------------
    ' Inputs:
    '   objrForm       : The IRform object reference
    '   vntRecordId    : 
    '   vntParameters  : The Parameters passed to this function
    ' Returns:
    '   LoadFormData   : The loaded form data
    ' Implements Agent : 
    ' History:
    ' Reversion#    Date        Author  Description
    ' ----------    ----        ------  -----------
    '   1.0                                VDI\OlegSh   Initial version
    ' --------------------------------------------------------------------------
    Function NewFormData(ByVal objrForm, ByVal vntParameters)
        Dim vntForm
        Dim rstForm As ADODB.Recordset 'TODO: remove in script
        Dim rstSForm As ADODB.Recordset  'TODO: remove in script
        Dim Parameters

        'UIMaster.RUITop.DisableButton "Approve and Process", False


        vntForm = objrForm.DoNewFormData(vntParameters)
        rstForm = vntForm(0) 'TODO: prepend with set in script
        rstSForm = vntForm(1) 'TODO: prepend with set in script

        Call UIMaster.RUICenter.Form.Execute("AddBudgetDivision", Parameters)

        If Parameters.RecordCount > 0 Then
            With Parameters
                Call .MoveFirst()
                Do While Not .EOF
                    Call rstSForm.AddNew()
                    rstSForm.Fields("Division_").Value = Parameters.Fields.Item("QM_Division_Id").Value
                    rstSForm.Fields.Item("Total_staff_planning").Value = 0.0
                    rstSForm.Fields.Item("Total_staff_realization").Value = 0.0
                    rstSForm.Fields.Item("Total_turnover_planning").Value = 0.0
                    rstSForm.Fields.Item("Total_forecast").Value = 0.0
                    rstSForm.Fields.Item("Total_turnover_realization").Value = 0.0
                    rstSForm.Fields.Item("Average_rate").Value = 0.0
                    Call .MoveNext()
                Loop
            End With

        End If
        Call UIMaster.RUITop.DisableButton("Print report", True)
        Call UIMaster.RUITop.DisableButton("Refresh info", True)

        NewFormData = vntForm

    End Function



    ' --------------------------------------------------------------------------
    ' Name:    LoadFormData
    ' Purpose: This function open 
    ' --------------------------------------------------------------------------
    ' Inputs:
    '   objrForm       : The IRform object reference
    '   vntRecordId    : 
    '   vntParameters  : The Parameters passed to this function
    ' Returns:
    '   LoadFormData   : The loaded form data
    ' Implements Agent :
    ' History:
    ' Reversion#    Date        Author  Description
    ' ----------    ----        ------  -----------
    '   1.0                     VDI\OlegSh   Initial version    
    '   1.1                     VDI\AlexeyP Optimized territory processing
    ' --------------------------------------------------------------------------
    Function LoadFormData(ByVal objrForm, ByVal vntRecordId, ByVal vntParameters)
        Dim vntForm
        Dim rstForm As ADODB.Recordset  'TODO: remove in script
        Dim rstFormSec As ADODB.Recordset 'TODO: remove in script
        Dim rstFormSec2 As ADODB.Recordset 'TODO: remove in script
        Dim rstTerritory As ADODB.Recordset  'TODO: remove in script
        Dim Parameters

        vntForm = objrForm.DoLoadFormData(vntRecordId, vntParameters)
        rstForm = vntForm(0) 'TODO: prepend with set in script
        rstFormSec = vntForm(1) 'TODO: prepend with set in script
        rstFormSec2 = vntForm(2) 'TODO: prepend with set in script
        If rstForm.fields("Workable_hours").value > 1 Then
            With UIMaster.RUICenter
                Call .DisableField("Workable Hours", "Year_period", True)
                Call .DisableField("Workable Hours", "Month_period", True)
            End With
        End If

        '1. obtain territory recordset
        Call UIMaster.RUICenter.Form.Execute("Territories", rstTerritory)

        '2. analyze secondaries     
        If (rstFormSec.RecordCount <> 0) And (rstTerritory.RecordCount <> 0) Then
            With rstFormSec
                Call .MoveFirst()
                ReDim Parameters(2)
                Parameters(0) = rstForm.fields("QM_workable_hours_Id").value
                Do While Not .EOF
                    Parameters(1) = rstFormSec.fields("QM_budget_division_Id").value

                    If Not RstFind(rstFormSec2, Parameters(1)) Then
                        ' territory assigning territory
                        Call rstTerritory.MoveFirst()
                        Do While Not rstTerritory.EOF
                            Call rstFormSec2.AddNew()
                            rstFormSec2.fields("QM_division_ref").value = Parameters(1) ' id of budget division
                            rstFormSec2.fields("QM_budget_id").value = Parameters(0)  ' id of main rec
                            rstFormSec2.fields("Territory_ref").value = _
                                   rstTerritory.fields("Territory_Id").value
                            Call rstTerritory.MoveNext()
                        Loop
                        UIMaster.RUICenter.Dirty = True 'buttons must call Apply in order to work with up-to-date data
                    End If
                    Call .MoveNext()
                Loop
            End With
        End If
        LoadFormData = vntForm

    End Function

    'Sub SaveFormData(objrForm, vntRecordset, vntParameters)
    'Dim rstForm



    '     Set rstForm = vntRecordset(0)
    '     Set rstFormSec = vntRecordset(1)


    'End Sub   

    Function RstFind(ByVal rstFormSec2, ByVal Parameters)
        RstFind = False
        If rstFormSec2.RecordCount = 0 Then
            Exit Function
        End If

        With rstFormSec2
            Call .MoveFirst()
            While Not .EOF
                If UIMaster.RSysClient.EqualIds(.Fields("QM_division_ref").Value, Parameters) Then
                    RstFind = True
                    Exit Function
                End If
                Call .MoveNext()
            End While
        End With
    End Function

    '<alexeyp>
    ' --------------------------------------------------------------------------
    ' Name:    FindDivision
    ' Purpose: Finds DivisionId by BudgetDivisionId in passed recordset
    ' --------------------------------------------------------------------------
    ' Inputs:
    '   rstBudgetDivision : Secondary recordset of QM_budget_division
    '   vntBudgetDivisionId : Primary key to QM budget division record
    ' Returns:
    '   FindDivision : Division id from QM_Division; NullID, if not found
    ' Implements Agent :
    ' History:
    ' Revision#    Date        Author  Description
    ' ----------    ----        ------  -----------
    '   1.0                     VDI\AlexeyP   Initial version
    ' --------------------------------------------------------------------------
    'used by Refresh button script
    Function FindDivision(ByVal rstBudgetDivision, ByVal vntBudgetDivisionId)
        FindDivision = UIMaster.RSysClient.StringToId("0x0000000000000000")
        With rstBudgetDivision
            Call .MoveFirst()
            While Not .EOF
                If UIMaster.RSysClient.EqualIds(.Fields("QM_budget_division_id").value, vntBudgetDivisionId) Then
                    FindDivision = .Fields("Division_").value
                    Exit Function
                End If

                Call .MoveNext()
            End While
        End With
    End Function
    '</alexeyp>                              
End Module
