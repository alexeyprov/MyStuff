Imports System.Text

Public Class CityView
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtCity As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmbState As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnScale As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents cmdShowImage As System.Web.UI.WebControls.Button
    Protected WithEvents imgResult As System.Web.UI.WebControls.Image

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Private Sub cmdShowImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowImage.Click
        Dim sb As New StringBuilder(200)
        sb.Append("CityViewer.ashx?City=")
        sb.Append(txtCity.Text)
        sb.Append("&State=")
        sb.Append(cmbState.SelectedItem.Text)
        sb.Append("&Scale=")
        sb.Append(2 ^ btnScale.SelectedIndex)
        imgResult.ImageUrl = sb.ToString()
    End Sub
End Class
