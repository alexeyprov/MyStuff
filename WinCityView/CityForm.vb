Public Class CityForm
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        m_viewer = New CityViewer
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If

        MyBase.Dispose(disposing)
        m_viewer = Nothing
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents lblCity As System.Windows.Forms.Label
    Private WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents lblState As System.Windows.Forms.Label
    Private WithEvents cmbState As System.Windows.Forms.ComboBox
    Private WithEvents grpScale As System.Windows.Forms.GroupBox
    Private WithEvents pctResult As System.Windows.Forms.PictureBox
    Private WithEvents btnOne As System.Windows.Forms.RadioButton
    Private WithEvents btnTwo As System.Windows.Forms.RadioButton
    Private WithEvents btnEight As System.Windows.Forms.RadioButton
    Private WithEvents btnFour As System.Windows.Forms.RadioButton
    Private WithEvents btnSixteen As System.Windows.Forms.RadioButton
    Private WithEvents btnThirtyTwo As System.Windows.Forms.RadioButton
    Friend WithEvents lblShow As System.Windows.Forms.LinkLabel
    Private WithEvents btnClose As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(CityForm))
        Me.lblCity = New System.Windows.Forms.Label
        Me.txtCity = New System.Windows.Forms.TextBox
        Me.lblState = New System.Windows.Forms.Label
        Me.cmbState = New System.Windows.Forms.ComboBox
        Me.grpScale = New System.Windows.Forms.GroupBox
        Me.btnOne = New System.Windows.Forms.RadioButton
        Me.btnTwo = New System.Windows.Forms.RadioButton
        Me.btnEight = New System.Windows.Forms.RadioButton
        Me.btnFour = New System.Windows.Forms.RadioButton
        Me.btnSixteen = New System.Windows.Forms.RadioButton
        Me.btnThirtyTwo = New System.Windows.Forms.RadioButton
        Me.pctResult = New System.Windows.Forms.PictureBox
        Me.lblShow = New System.Windows.Forms.LinkLabel
        Me.btnClose = New System.Windows.Forms.Button
        Me.grpScale.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblCity
        '
        Me.lblCity.BackColor = System.Drawing.Color.Transparent
        Me.lblCity.Location = New System.Drawing.Point(8, 16)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(100, 16)
        Me.lblCity.TabIndex = 0
        Me.lblCity.Text = "&City:"
        Me.lblCity.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtCity
        '
        Me.txtCity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCity.BackColor = System.Drawing.SystemColors.Desktop
        Me.txtCity.Location = New System.Drawing.Point(120, 12)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(256, 20)
        Me.txtCity.TabIndex = 1
        Me.txtCity.Text = ""
        '
        'lblState
        '
        Me.lblState.BackColor = System.Drawing.Color.Transparent
        Me.lblState.Location = New System.Drawing.Point(8, 48)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(100, 16)
        Me.lblState.TabIndex = 0
        Me.lblState.Text = "&State:"
        Me.lblState.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbState
        '
        Me.cmbState.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbState.BackColor = System.Drawing.SystemColors.Desktop
        Me.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbState.Items.AddRange(New Object() {"AL", "AK", "AR", "AZ", "CA", "CO", "CT", "DC", "DE", "FL", "GA", "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VA", "VT", "WA", "WI", "WV", "WY"})
        Me.cmbState.Location = New System.Drawing.Point(120, 43)
        Me.cmbState.Name = "cmbState"
        Me.cmbState.Size = New System.Drawing.Size(256, 21)
        Me.cmbState.TabIndex = 2
        '
        'grpScale
        '
        Me.grpScale.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpScale.BackColor = System.Drawing.Color.Transparent
        Me.grpScale.Controls.Add(Me.btnOne)
        Me.grpScale.Controls.Add(Me.btnTwo)
        Me.grpScale.Controls.Add(Me.btnEight)
        Me.grpScale.Controls.Add(Me.btnFour)
        Me.grpScale.Controls.Add(Me.btnSixteen)
        Me.grpScale.Controls.Add(Me.btnThirtyTwo)
        Me.grpScale.Location = New System.Drawing.Point(8, 72)
        Me.grpScale.Name = "grpScale"
        Me.grpScale.Size = New System.Drawing.Size(368, 88)
        Me.grpScale.TabIndex = 3
        Me.grpScale.TabStop = False
        Me.grpScale.Text = "Scale"
        '
        'btnOne
        '
        Me.btnOne.Location = New System.Drawing.Point(12, 24)
        Me.btnOne.Name = "btnOne"
        Me.btnOne.Size = New System.Drawing.Size(80, 24)
        Me.btnOne.TabIndex = 0
        Me.btnOne.Text = "1 meter"
        '
        'btnTwo
        '
        Me.btnTwo.Location = New System.Drawing.Point(12, 56)
        Me.btnTwo.Name = "btnTwo"
        Me.btnTwo.Size = New System.Drawing.Size(80, 24)
        Me.btnTwo.TabIndex = 0
        Me.btnTwo.Text = "2 meters"
        '
        'btnEight
        '
        Me.btnEight.Checked = True
        Me.btnEight.Location = New System.Drawing.Point(110, 56)
        Me.btnEight.Name = "btnEight"
        Me.btnEight.Size = New System.Drawing.Size(80, 24)
        Me.btnEight.TabIndex = 0
        Me.btnEight.TabStop = True
        Me.btnEight.Text = "8 meters"
        '
        'btnFour
        '
        Me.btnFour.Location = New System.Drawing.Point(110, 24)
        Me.btnFour.Name = "btnFour"
        Me.btnFour.Size = New System.Drawing.Size(80, 24)
        Me.btnFour.TabIndex = 0
        Me.btnFour.Text = "4 meters"
        '
        'btnSixteen
        '
        Me.btnSixteen.Location = New System.Drawing.Point(208, 24)
        Me.btnSixteen.Name = "btnSixteen"
        Me.btnSixteen.Size = New System.Drawing.Size(80, 24)
        Me.btnSixteen.TabIndex = 0
        Me.btnSixteen.Text = "16 meters"
        '
        'btnThirtyTwo
        '
        Me.btnThirtyTwo.Location = New System.Drawing.Point(208, 56)
        Me.btnThirtyTwo.Name = "btnThirtyTwo"
        Me.btnThirtyTwo.Size = New System.Drawing.Size(80, 24)
        Me.btnThirtyTwo.TabIndex = 0
        Me.btnThirtyTwo.Text = "32 meters"
        '
        'pctResult
        '
        Me.pctResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pctResult.BackColor = System.Drawing.Color.Transparent
        Me.pctResult.Location = New System.Drawing.Point(8, 192)
        Me.pctResult.Name = "pctResult"
        Me.pctResult.Size = New System.Drawing.Size(384, 363)
        Me.pctResult.TabIndex = 4
        Me.pctResult.TabStop = False
        '
        'lblShow
        '
        Me.lblShow.BackColor = System.Drawing.Color.Transparent
        Me.lblShow.Location = New System.Drawing.Point(8, 168)
        Me.lblShow.Name = "lblShow"
        Me.lblShow.TabIndex = 5
        Me.lblShow.TabStop = True
        Me.lblShow.Text = "Show City"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.Green
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(384, 16)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(16, 16)
        Me.btnClose.TabIndex = 6
        '
        'CityForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(408, 560)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblShow)
        Me.Controls.Add(Me.pctResult)
        Me.Controls.Add(Me.grpScale)
        Me.Controls.Add(Me.cmbState)
        Me.Controls.Add(Me.txtCity)
        Me.Controls.Add(Me.lblCity)
        Me.Controls.Add(Me.lblState)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "CityForm"
        Me.Text = "City View"
        Me.TransparencyKey = System.Drawing.Color.White
        Me.grpScale.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub lblShow_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblShow.LinkClicked
        Dim ctl As RadioButton
        Dim nScale As Integer = 0
        'Which radio is selected?
        For Each ctl In Me.grpScale.Controls
            If ctl.Checked Then
                nScale = CInt(Microsoft.VisualBasic.Left(ctl.Text, 2))
                Exit For
            End If
        Next

        If (nScale <> 0) And (cmbState.SelectedItem <> "") Then
            'Show Image
            pctResult.Image = m_viewer.GetCityImage(txtCity.Text, cmbState.SelectedItem, nScale, _
                pctResult.Width, pctResult.Height)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    'Data Members
    Private m_viewer As CityViewer
End Class
