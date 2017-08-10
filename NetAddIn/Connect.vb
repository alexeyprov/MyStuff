Imports Microsoft.Office.Core
imports Extensibility
imports System.Runtime.InteropServices


#Region " Read me for Add-in installation and setup information. "
' When run, the Add-in wizard prepared the registry for the Add-in.
' At a later time, if the Add-in becomes unavailable for reasons such as:
'   1) You moved this project to a computer other than which is was originally created on.
'   2) You chose 'Yes' when presented with a message asking if you wish to remove the Add-in.
'   3) Registry corruption.
' you will need to re-register the Add-in by building the NetAddInSetup project 
' by right clicking the project in the Solution Explorer, then choosing install.
#End Region

<GuidAttribute("34E4F23E-F5BE-4286-8369-4E020520B034"), ProgIdAttribute("NetAddIn.Connect")> _
Public Class Connect
	
	Implements Extensibility.IDTExtensibility2
	
	Dim applicationObject as Object
    dim addInInstance as object
	
	
	Public Sub OnBeginShutdown(ByRef custom As System.Array) Implements Extensibility.IDTExtensibility2.OnBeginShutdown
	End Sub
	
	Public Sub OnAddInsUpdate(ByRef custom As System.Array) Implements Extensibility.IDTExtensibility2.OnAddInsUpdate
	End Sub
	
	Public Sub OnStartupComplete(ByRef custom As System.Array) Implements Extensibility.IDTExtensibility2.OnStartupComplete
	End Sub
	
	Public Sub OnDisconnection(ByVal RemoveMode As Extensibility.ext_DisconnectMode, ByRef custom As System.Array) Implements Extensibility.IDTExtensibility2.OnDisconnection
	End Sub
	
	Public Sub OnConnection(ByVal application As Object, ByVal connectMode As Extensibility.ext_ConnectMode, ByVal addInInst As Object, ByRef custom As System.Array) Implements Extensibility.IDTExtensibility2.OnConnection
  		applicationObject = application
        addInInstance = addInInst
		
	End Sub

	
End Class
