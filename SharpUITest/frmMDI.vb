Public Class frmMDI
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
    Friend WithEvents UiToolbarManager1 As DataDynamics.SharpUI.Toolbars.UiToolbarManager
    Friend WithEvents dockTop1 As DataDynamics.SharpUI.Toolbars.Dock
    Friend WithEvents dockRight1 As DataDynamics.SharpUI.Toolbars.Dock
    Friend WithEvents dockBottom1 As DataDynamics.SharpUI.Toolbars.Dock
    Friend WithEvents dockLeft1 As DataDynamics.SharpUI.Toolbars.Dock
    Friend WithEvents cmFile As DataDynamics.SharpUI.Toolbars.PopupCommand
    Friend WithEvents cmNew As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents mbMain As DataDynamics.SharpUI.Toolbars.Menubar
    Friend WithEvents smFile As DataDynamics.SharpUI.Toolbars.PopupTool
    Friend WithEvents smWindows As DataDynamics.SharpUI.Toolbars.PopupTool
    Friend WithEvents popupMenu1 As DataDynamics.SharpUI.Toolbars.PopupMenu
    Friend WithEvents tNew As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents popupMenu2 As DataDynamics.SharpUI.Toolbars.PopupMenu
    Friend WithEvents mdiListTool1 As DataDynamics.SharpUI.Toolbars.MdiListTool
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.UiToolbarManager1 = New DataDynamics.SharpUI.Toolbars.UiToolbarManager
        Me.dockTop1 = New DataDynamics.SharpUI.Toolbars.Dock
        Me.dockRight1 = New DataDynamics.SharpUI.Toolbars.Dock
        Me.dockBottom1 = New DataDynamics.SharpUI.Toolbars.Dock
        Me.dockLeft1 = New DataDynamics.SharpUI.Toolbars.Dock
        Me.cmFile = New DataDynamics.SharpUI.Toolbars.PopupCommand
        Me.cmNew = New DataDynamics.SharpUI.Toolbars.Command
        Me.mbMain = New DataDynamics.SharpUI.Toolbars.Menubar
        Me.smFile = New DataDynamics.SharpUI.Toolbars.PopupTool
        Me.smWindows = New DataDynamics.SharpUI.Toolbars.PopupTool
        Me.popupMenu1 = New DataDynamics.SharpUI.Toolbars.PopupMenu
        Me.tNew = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.popupMenu2 = New DataDynamics.SharpUI.Toolbars.PopupMenu
        Me.mdiListTool1 = New DataDynamics.SharpUI.Toolbars.MdiListTool
        Me.SuspendLayout()
        '
        'UiToolbarManager1
        '
        Me.UiToolbarManager1.ActiveToolWindow = Nothing
        Me.UiToolbarManager1.BackColor = System.Drawing.SystemColors.Control
        Me.UiToolbarManager1.Bands.AddRange(New DataDynamics.SharpUI.Toolbars.Band() {Me.mbMain, Me.popupMenu1, Me.popupMenu2})
        Me.UiToolbarManager1.Commands.AddRange(New DataDynamics.SharpUI.Toolbars.Command() {Me.cmFile, Me.cmNew})
        '
        'UiToolbarManager1.CustomizeImages
        '
        Me.UiToolbarManager1.CustomizeImages.TransparentColor = System.Drawing.Color.Transparent
        Me.UiToolbarManager1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.UiToolbarManager1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.UiToolbarManager1.MenuFont = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(204, Byte))
        Me.UiToolbarManager1.Parent = Me
        'Do not modify or remove this property.
        Me.dockTop1.Name = "dockTop1"
        Me.dockTop1.UiToolbarManager = UiToolbarManager1
        'Do not modify or remove this property.
        Me.dockRight1.Name = "dockRight1"
        Me.dockRight1.UiToolbarManager = UiToolbarManager1
        'Do not modify or remove this property.
        Me.dockBottom1.Name = "dockBottom1"
        Me.dockBottom1.UiToolbarManager = UiToolbarManager1
        'Do not modify or remove this property.
        Me.dockLeft1.Name = "dockLeft1"
        Me.dockLeft1.UiToolbarManager = UiToolbarManager1
        '
        'cmFile
        '
        Me.cmFile.Commands.AddRange(New DataDynamics.SharpUI.Toolbars.Command() {Me.cmNew})
        Me.cmFile.Name = "cmFile"
        Me.cmFile.Text = "File"
        '
        'cmNew
        '
        Me.cmNew.Name = "cmNew"
        Me.cmNew.Text = "New"
        '
        'mbMain
        '
        Me.mbMain.Tools.AddRange(New DataDynamics.SharpUI.Toolbars.Tool() {Me.smFile, Me.smWindows})
        Me.mbMain.Name = "mbMain"
        '
        'smFile
        '
        Me.smFile.Name = "smFile"
        Me.smFile.SubBand = Me.popupMenu1
        Me.smFile.Text = "&File"
        '
        'smWindows
        '
        Me.smWindows.Name = "smWindows"
        Me.smWindows.SubBand = Me.popupMenu2
        Me.smWindows.Text = "&Windows"
        '
        'popupMenu1
        '
        Me.popupMenu1.AutoGenerated = True
        Me.popupMenu1.Tools.AddRange(New DataDynamics.SharpUI.Toolbars.Tool() {Me.tNew})
        Me.popupMenu1.Name = "popupMenu1"
        '
        'tNew
        '
        Me.tNew.Command = Me.cmNew
        Me.tNew.Name = "tNew"
        '
        'popupMenu2
        '
        Me.popupMenu2.AutoGenerated = True
        Me.popupMenu2.Tools.AddRange(New DataDynamics.SharpUI.Toolbars.Tool() {Me.mdiListTool1})
        Me.popupMenu2.Name = "popupMenu2"
        '
        'mdiListTool1
        '
        Me.mdiListTool1.CaptionStyle = DataDynamics.SharpUI.Toolbars.ToolCaptionStyles.TextOnly
        Me.mdiListTool1.DisplayItemNumbers = False
        Me.mdiListTool1.Grouped = True
        Me.mdiListTool1.Name = "mdiListTool1"
        Me.mdiListTool1.Text = "mdiListTool1"
        Me.mdiListTool1.TextAlign = DataDynamics.SharpUI.Toolbars.ToolAlignTypes.Near
        '
        'frmMDI
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.dockRight1)
        Me.Controls.Add(Me.dockLeft1)
        Me.Controls.Add(Me.dockTop1)
        Me.Controls.Add(Me.dockBottom1)
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.Name = "frmMDI"
        Me.Text = "Parent MDI"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private m_ctr As Integer = 1

    Private Sub cmNew_Executed(ByVal sender As Object, ByVal e As DataDynamics.SharpUI.Toolbars.ToolbarManagerEventArgs) Handles cmNew.Executed
        Dim f As New frmChild
        f.MdiParent = Me
        f.Text = "MDI Child " & CStr(m_ctr)
        m_ctr = m_ctr + 1
        f.Show()
    End Sub
End Class
