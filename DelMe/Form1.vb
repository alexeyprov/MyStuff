Public Class Form1
    Inherits System.Windows.Forms.Form


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblSeed As System.Windows.Forms.Label
    Friend WithEvents txtSeed As System.Windows.Forms.TextBox
    Friend WithEvents lblMin As System.Windows.Forms.Label
    Friend WithEvents lblMax As System.Windows.Forms.Label
    Friend WithEvents txtRandom As System.Windows.Forms.TextBox
    Friend WithEvents lblRandom As System.Windows.Forms.Label
    Friend WithEvents cmdGenerate As System.Windows.Forms.Button
    Friend WithEvents txtMin As System.Windows.Forms.TextBox
    Friend WithEvents txtMax As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblSeed = New System.Windows.Forms.Label
        Me.txtSeed = New System.Windows.Forms.TextBox
        Me.txtMin = New System.Windows.Forms.TextBox
        Me.lblMin = New System.Windows.Forms.Label
        Me.txtMax = New System.Windows.Forms.TextBox
        Me.lblMax = New System.Windows.Forms.Label
        Me.txtRandom = New System.Windows.Forms.TextBox
        Me.lblRandom = New System.Windows.Forms.Label
        Me.cmdGenerate = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(16, 24)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 24)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Test Script"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdGenerate)
        Me.GroupBox1.Controls.Add(Me.txtSeed)
        Me.GroupBox1.Controls.Add(Me.lblSeed)
        Me.GroupBox1.Controls.Add(Me.txtMin)
        Me.GroupBox1.Controls.Add(Me.lblMin)
        Me.GroupBox1.Controls.Add(Me.txtMax)
        Me.GroupBox1.Controls.Add(Me.lblMax)
        Me.GroupBox1.Controls.Add(Me.txtRandom)
        Me.GroupBox1.Controls.Add(Me.lblRandom)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 64)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(280, 200)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Random Numbers Test"
        '
        'lblSeed
        '
        Me.lblSeed.Location = New System.Drawing.Point(8, 24)
        Me.lblSeed.Name = "lblSeed"
        Me.lblSeed.Size = New System.Drawing.Size(80, 20)
        Me.lblSeed.TabIndex = 0
        Me.lblSeed.Text = "&Seed:"
        '
        'txtSeed
        '
        Me.txtSeed.Location = New System.Drawing.Point(96, 24)
        Me.txtSeed.Name = "txtSeed"
        Me.txtSeed.TabIndex = 1
        Me.txtSeed.Text = ""
        '
        'txtMin
        '
        Me.txtMin.Location = New System.Drawing.Point(96, 56)
        Me.txtMin.Name = "txtMin"
        Me.txtMin.TabIndex = 1
        Me.txtMin.Text = ""
        '
        'lblMin
        '
        Me.lblMin.Location = New System.Drawing.Point(8, 56)
        Me.lblMin.Name = "lblMin"
        Me.lblMin.Size = New System.Drawing.Size(80, 20)
        Me.lblMin.TabIndex = 0
        Me.lblMin.Text = "&Minimum:"
        '
        'txtMax
        '
        Me.txtMax.Location = New System.Drawing.Point(96, 88)
        Me.txtMax.Name = "txtMax"
        Me.txtMax.TabIndex = 1
        Me.txtMax.Text = ""
        '
        'lblMax
        '
        Me.lblMax.Location = New System.Drawing.Point(8, 88)
        Me.lblMax.Name = "lblMax"
        Me.lblMax.Size = New System.Drawing.Size(80, 20)
        Me.lblMax.TabIndex = 0
        Me.lblMax.Text = "Ma&ximum:"
        '
        'txtRandom
        '
        Me.txtRandom.Location = New System.Drawing.Point(96, 168)
        Me.txtRandom.Name = "txtRandom"
        Me.txtRandom.ReadOnly = True
        Me.txtRandom.TabIndex = 1
        Me.txtRandom.Text = ""
        '
        'lblRandom
        '
        Me.lblRandom.Location = New System.Drawing.Point(8, 168)
        Me.lblRandom.Name = "lblRandom"
        Me.lblRandom.Size = New System.Drawing.Size(80, 20)
        Me.lblRandom.TabIndex = 0
        Me.lblRandom.Text = "&Value:"
        '
        'cmdGenerate
        '
        Me.cmdGenerate.Location = New System.Drawing.Point(16, 120)
        Me.cmdGenerate.Name = "cmdGenerate"
        Me.cmdGenerate.TabIndex = 2
        Me.cmdGenerate.Text = "&Generate"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "My Dumb Portal"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        OnBtnJump2WebClicked()
    End Sub

    Private Sub cmdGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGenerate.Click
        Dim g As RndGenLib.IGenerator

        g = New RndGenLib.Generator
        If txtSeed.Text.Length > 0 Then
            g.Seed = CInt(txtSeed.Text)
        End If
        txtRandom.Text = CStr(g.NextRandom(CInt(txtMin.Text), CInt(txtMax.Text)))
    End Sub
End Class
