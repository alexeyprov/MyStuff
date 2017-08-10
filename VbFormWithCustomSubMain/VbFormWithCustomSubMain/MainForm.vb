Public Class MainForm
	<STAThread()> _
	Public Shared Sub Main(ByVal cmdArgs() As String)
		Debug.Print("In customized sub Main")
		Application.Run(New MainForm())
	End Sub
End Class
