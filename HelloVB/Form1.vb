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
    Private WithEvents lblGrid As System.Windows.Forms.Label
    Friend WithEvents AxMSFlexGrid1 As AxMSFlexGridLib.AxMSFlexGrid

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.lblGrid = New System.Windows.Forms.Label
        Me.AxMSFlexGrid1 = New AxMSFlexGridLib.AxMSFlexGrid
        CType(Me.AxMSFlexGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblGrid
        '
        Me.lblGrid.Location = New System.Drawing.Point(8, 8)
        Me.lblGrid.Name = "lblGrid"
        Me.lblGrid.Size = New System.Drawing.Size(104, 16)
        Me.lblGrid.TabIndex = 0
        Me.lblGrid.Text = "Grid:"
        '
        'AxMSFlexGrid1
        '
        Me.AxMSFlexGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AxMSFlexGrid1.Location = New System.Drawing.Point(0, 24)
        Me.AxMSFlexGrid1.Name = "AxMSFlexGrid1"
        Me.AxMSFlexGrid1.OcxState = CType(resources.GetObject("AxMSFlexGrid1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMSFlexGrid1.Size = New System.Drawing.Size(296, 248)
        Me.AxMSFlexGrid1.TabIndex = 1
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.AxMSFlexGrid1)
        Me.Controls.Add(Me.lblGrid)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.AxMSFlexGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
