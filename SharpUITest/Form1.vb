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
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents UiToolbarManager1 As DataDynamics.SharpUI.Toolbars.UiToolbarManager
    Friend WithEvents dockTop1 As DataDynamics.SharpUI.Toolbars.Dock
    Friend WithEvents dockRight1 As DataDynamics.SharpUI.Toolbars.Dock
    Friend WithEvents dockBottom1 As DataDynamics.SharpUI.Toolbars.Dock
    Friend WithEvents dockLeft1 As DataDynamics.SharpUI.Toolbars.Dock
    Friend WithEvents cmNew As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmOpen As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmSave As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmSaveAs As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmPageSetup As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmPrint As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmExit As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmUndo As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmCut As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmCopy As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmPaste As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmDelete As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmFind As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmFindNext As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmReplace As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmGoTo As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmSelectAll As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmDateTime As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmWordWrap As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmFont As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmStatusBar As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmHelpTopics As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents cmAbout As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents mbMain As DataDynamics.SharpUI.Toolbars.Menubar
    Friend WithEvents smFile As DataDynamics.SharpUI.Toolbars.PopupTool
    Friend WithEvents smEdit As DataDynamics.SharpUI.Toolbars.PopupTool
    Friend WithEvents smFormat As DataDynamics.SharpUI.Toolbars.PopupTool
    Friend WithEvents smView As DataDynamics.SharpUI.Toolbars.PopupTool
    Friend WithEvents smHelp As DataDynamics.SharpUI.Toolbars.PopupTool
    Friend WithEvents popupMenu1 As DataDynamics.SharpUI.Toolbars.PopupMenu
    Friend WithEvents tNew As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents tOpen As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents tSave As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents tSaveAs As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents tPageSetup As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents tPrint As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents tExit As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents popupMenu2 As DataDynamics.SharpUI.Toolbars.PopupMenu
    Friend WithEvents tUndo As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents tCut As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents tCopy As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents tPaste As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents tDelete As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents tFind As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents tFindNext As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents tReplace As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents tGoTo As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents tSelectAll As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents tDateTime As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents popupMenu3 As DataDynamics.SharpUI.Toolbars.PopupMenu
    Friend WithEvents tWordWrap As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents tFont As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents popupMenu4 As DataDynamics.SharpUI.Toolbars.PopupMenu
    Friend WithEvents tStatusBar As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents popupMenu5 As DataDynamics.SharpUI.Toolbars.PopupMenu
    Friend WithEvents tHelpTopics As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents tAbout As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents cmToolWindow As DataDynamics.SharpUI.Toolbars.Command
    Friend WithEvents tToolWindow As DataDynamics.SharpUI.Toolbars.ButtonTool
    Friend WithEvents twToolBox As DataDynamics.SharpUI.Toolbars.ToolWindow
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox
        Me.UiToolbarManager1 = New DataDynamics.SharpUI.Toolbars.UiToolbarManager
        Me.mbMain = New DataDynamics.SharpUI.Toolbars.Menubar
        Me.smFile = New DataDynamics.SharpUI.Toolbars.PopupTool
        Me.popupMenu1 = New DataDynamics.SharpUI.Toolbars.PopupMenu
        Me.tNew = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmNew = New DataDynamics.SharpUI.Toolbars.Command
        Me.tOpen = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmOpen = New DataDynamics.SharpUI.Toolbars.Command
        Me.tSave = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmSave = New DataDynamics.SharpUI.Toolbars.Command
        Me.tSaveAs = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmSaveAs = New DataDynamics.SharpUI.Toolbars.Command
        Me.tPageSetup = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmPageSetup = New DataDynamics.SharpUI.Toolbars.Command
        Me.tPrint = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmPrint = New DataDynamics.SharpUI.Toolbars.Command
        Me.tExit = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmExit = New DataDynamics.SharpUI.Toolbars.Command
        Me.smEdit = New DataDynamics.SharpUI.Toolbars.PopupTool
        Me.popupMenu2 = New DataDynamics.SharpUI.Toolbars.PopupMenu
        Me.tUndo = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmUndo = New DataDynamics.SharpUI.Toolbars.Command
        Me.tCut = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmCut = New DataDynamics.SharpUI.Toolbars.Command
        Me.tCopy = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmCopy = New DataDynamics.SharpUI.Toolbars.Command
        Me.tPaste = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmPaste = New DataDynamics.SharpUI.Toolbars.Command
        Me.tDelete = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmDelete = New DataDynamics.SharpUI.Toolbars.Command
        Me.tFind = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmFind = New DataDynamics.SharpUI.Toolbars.Command
        Me.tFindNext = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmFindNext = New DataDynamics.SharpUI.Toolbars.Command
        Me.tReplace = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmReplace = New DataDynamics.SharpUI.Toolbars.Command
        Me.tGoTo = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmGoTo = New DataDynamics.SharpUI.Toolbars.Command
        Me.tSelectAll = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmSelectAll = New DataDynamics.SharpUI.Toolbars.Command
        Me.tDateTime = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmDateTime = New DataDynamics.SharpUI.Toolbars.Command
        Me.smFormat = New DataDynamics.SharpUI.Toolbars.PopupTool
        Me.popupMenu3 = New DataDynamics.SharpUI.Toolbars.PopupMenu
        Me.tWordWrap = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmWordWrap = New DataDynamics.SharpUI.Toolbars.Command
        Me.tFont = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmFont = New DataDynamics.SharpUI.Toolbars.Command
        Me.smView = New DataDynamics.SharpUI.Toolbars.PopupTool
        Me.popupMenu4 = New DataDynamics.SharpUI.Toolbars.PopupMenu
        Me.tStatusBar = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmStatusBar = New DataDynamics.SharpUI.Toolbars.Command
        Me.tToolWindow = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmToolWindow = New DataDynamics.SharpUI.Toolbars.Command
        Me.smHelp = New DataDynamics.SharpUI.Toolbars.PopupTool
        Me.popupMenu5 = New DataDynamics.SharpUI.Toolbars.PopupMenu
        Me.tHelpTopics = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmHelpTopics = New DataDynamics.SharpUI.Toolbars.Command
        Me.tAbout = New DataDynamics.SharpUI.Toolbars.ButtonTool
        Me.cmAbout = New DataDynamics.SharpUI.Toolbars.Command
        Me.twToolBox = New DataDynamics.SharpUI.Toolbars.ToolWindow
        Me.dockTop1 = New DataDynamics.SharpUI.Toolbars.Dock
        Me.dockRight1 = New DataDynamics.SharpUI.Toolbars.Dock
        Me.dockBottom1 = New DataDynamics.SharpUI.Toolbars.Dock
        Me.dockLeft1 = New DataDynamics.SharpUI.Toolbars.Dock
        Me.SuspendLayout()
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox1.Location = New System.Drawing.Point(0, 23)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(292, 250)
        Me.RichTextBox1.TabIndex = 0
        Me.RichTextBox1.Text = ""
        '
        'UiToolbarManager1
        '
        Me.UiToolbarManager1.ActiveToolWindow = Nothing
        Me.UiToolbarManager1.BackColor = System.Drawing.SystemColors.Control
        Me.UiToolbarManager1.Bands.AddRange(New DataDynamics.SharpUI.Toolbars.Band() {Me.mbMain, Me.popupMenu1, Me.popupMenu2, Me.popupMenu3, Me.popupMenu4, Me.popupMenu5})
        Me.UiToolbarManager1.Commands.AddRange(New DataDynamics.SharpUI.Toolbars.Command() {Me.cmNew, Me.cmOpen, Me.cmSave, Me.cmSaveAs, Me.cmPageSetup, Me.cmPrint, Me.cmExit, Me.cmUndo, Me.cmCut, Me.cmCopy, Me.cmPaste, Me.cmDelete, Me.cmFind, Me.cmFindNext, Me.cmReplace, Me.cmGoTo, Me.cmSelectAll, Me.cmDateTime, Me.cmWordWrap, Me.cmFont, Me.cmStatusBar, Me.cmHelpTopics, Me.cmAbout, Me.cmToolWindow})
        '
        'UiToolbarManager1.CustomizeImages
        '
        Me.UiToolbarManager1.CustomizeImages.TransparentColor = System.Drawing.Color.Transparent
        Me.UiToolbarManager1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.UiToolbarManager1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.UiToolbarManager1.MenuFont = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(204, Byte))
        Me.UiToolbarManager1.Parent = Me
        Me.UiToolbarManager1.ToolbarStyle = DataDynamics.SharpUI.Toolbars.ToolbarStyles.Office2003
        Me.UiToolbarManager1.ToolWindows.AddRange(New DataDynamics.SharpUI.Toolbars.ToolWindow() {Me.twToolBox})
        '
        'mbMain
        '
        Me.mbMain.Text = "mbMain"
        Me.mbMain.Tools.AddRange(New DataDynamics.SharpUI.Toolbars.Tool() {Me.smFile, Me.smEdit, Me.smFormat, Me.smView, Me.smHelp})
        Me.mbMain.Name = "mbMain"
        '
        'smFile
        '
        Me.smFile.Name = "smFile"
        Me.smFile.SubBand = Me.popupMenu1
        Me.smFile.Text = "&File"
        '
        'popupMenu1
        '
        Me.popupMenu1.AutoGenerated = True
        Me.popupMenu1.Text = "popupMenu1"
        Me.popupMenu1.Tools.AddRange(New DataDynamics.SharpUI.Toolbars.Tool() {Me.tNew, Me.tOpen, Me.tSave, Me.tSaveAs, Me.tPageSetup, Me.tPrint, Me.tExit})
        Me.popupMenu1.Name = "popupMenu1"
        '
        'tNew
        '
        Me.tNew.Command = Me.cmNew
        Me.tNew.Name = "tNew"
        '
        'cmNew
        '
        Me.cmNew.Category = "File"
        Me.cmNew.Image = CType(resources.GetObject("cmNew.Image"), System.Drawing.Image)
        Me.cmNew.Name = "cmNew"
        Me.cmNew.Shortcuts.Add(New DataDynamics.SharpUI.Toolbars.ShortcutKey("N", "", False, False, True))
        Me.cmNew.Text = "New"
        '
        'tOpen
        '
        Me.tOpen.Command = Me.cmOpen
        Me.tOpen.Name = "tOpen"
        '
        'cmOpen
        '
        Me.cmOpen.Category = "File"
        Me.cmOpen.Image = CType(resources.GetObject("cmOpen.Image"), System.Drawing.Image)
        Me.cmOpen.Name = "cmOpen"
        Me.cmOpen.Text = "Open..."
        '
        'tSave
        '
        Me.tSave.BeginGroup = True
        Me.tSave.Command = Me.cmSave
        Me.tSave.Name = "tSave"
        '
        'cmSave
        '
        Me.cmSave.Category = "File"
        Me.cmSave.Image = CType(resources.GetObject("cmSave.Image"), System.Drawing.Image)
        Me.cmSave.Name = "cmSave"
        Me.cmSave.Shortcuts.Add(New DataDynamics.SharpUI.Toolbars.ShortcutKey("S", "", False, False, True))
        Me.cmSave.Text = "Save"
        '
        'tSaveAs
        '
        Me.tSaveAs.Command = Me.cmSaveAs
        Me.tSaveAs.Name = "tSaveAs"
        '
        'cmSaveAs
        '
        Me.cmSaveAs.Category = "File"
        Me.cmSaveAs.Name = "cmSaveAs"
        Me.cmSaveAs.Text = "Save As..."
        '
        'tPageSetup
        '
        Me.tPageSetup.BeginGroup = True
        Me.tPageSetup.Command = Me.cmPageSetup
        Me.tPageSetup.Name = "tPageSetup"
        '
        'cmPageSetup
        '
        Me.cmPageSetup.Category = "File"
        Me.cmPageSetup.Name = "cmPageSetup"
        Me.cmPageSetup.Text = "Page Setup..."
        '
        'tPrint
        '
        Me.tPrint.Command = Me.cmPrint
        Me.tPrint.Name = "tPrint"
        '
        'cmPrint
        '
        Me.cmPrint.Category = "File"
        Me.cmPrint.Image = CType(resources.GetObject("cmPrint.Image"), System.Drawing.Image)
        Me.cmPrint.Name = "cmPrint"
        Me.cmPrint.Shortcuts.Add(New DataDynamics.SharpUI.Toolbars.ShortcutKey("P", "", False, False, True))
        Me.cmPrint.Text = "Print"
        '
        'tExit
        '
        Me.tExit.BeginGroup = True
        Me.tExit.Command = Me.cmExit
        Me.tExit.Name = "tExit"
        '
        'cmExit
        '
        Me.cmExit.Category = "File"
        Me.cmExit.Name = "cmExit"
        Me.cmExit.Text = "Exit"
        '
        'smEdit
        '
        Me.smEdit.Name = "smEdit"
        Me.smEdit.SubBand = Me.popupMenu2
        Me.smEdit.Text = "&Edit"
        '
        'popupMenu2
        '
        Me.popupMenu2.AutoGenerated = True
        Me.popupMenu2.Text = "popupMenu2"
        Me.popupMenu2.Tools.AddRange(New DataDynamics.SharpUI.Toolbars.Tool() {Me.tUndo, Me.tCut, Me.tCopy, Me.tPaste, Me.tDelete, Me.tFind, Me.tFindNext, Me.tReplace, Me.tGoTo, Me.tSelectAll, Me.tDateTime})
        Me.popupMenu2.Name = "popupMenu2"
        '
        'tUndo
        '
        Me.tUndo.Command = Me.cmUndo
        Me.tUndo.Name = "tUndo"
        '
        'cmUndo
        '
        Me.cmUndo.Category = "Edit"
        Me.cmUndo.Image = CType(resources.GetObject("cmUndo.Image"), System.Drawing.Image)
        Me.cmUndo.Name = "cmUndo"
        Me.cmUndo.Shortcuts.Add(New DataDynamics.SharpUI.Toolbars.ShortcutKey("Z", "", False, False, True))
        Me.cmUndo.Text = "Undo"
        '
        'tCut
        '
        Me.tCut.BeginGroup = True
        Me.tCut.Command = Me.cmCut
        Me.tCut.Name = "tCut"
        '
        'cmCut
        '
        Me.cmCut.Category = "Edit"
        Me.cmCut.Image = CType(resources.GetObject("cmCut.Image"), System.Drawing.Image)
        Me.cmCut.Name = "cmCut"
        Me.cmCut.Shortcuts.Add(New DataDynamics.SharpUI.Toolbars.ShortcutKey("X", "", False, False, True))
        Me.cmCut.Text = "Cut"
        '
        'tCopy
        '
        Me.tCopy.Command = Me.cmCopy
        Me.tCopy.Name = "tCopy"
        '
        'cmCopy
        '
        Me.cmCopy.Category = "Edit"
        Me.cmCopy.Image = CType(resources.GetObject("cmCopy.Image"), System.Drawing.Image)
        Me.cmCopy.Name = "cmCopy"
        Me.cmCopy.Shortcuts.Add(New DataDynamics.SharpUI.Toolbars.ShortcutKey("C", "", False, False, True))
        Me.cmCopy.Text = "Copy"
        '
        'tPaste
        '
        Me.tPaste.Command = Me.cmPaste
        Me.tPaste.Name = "tPaste"
        '
        'cmPaste
        '
        Me.cmPaste.Category = "Edit"
        Me.cmPaste.Image = CType(resources.GetObject("cmPaste.Image"), System.Drawing.Image)
        Me.cmPaste.Name = "cmPaste"
        Me.cmPaste.Shortcuts.Add(New DataDynamics.SharpUI.Toolbars.ShortcutKey("V", "", False, False, True))
        Me.cmPaste.Text = "Paste"
        '
        'tDelete
        '
        Me.tDelete.BeginGroup = True
        Me.tDelete.Command = Me.cmDelete
        Me.tDelete.Name = "tDelete"
        '
        'cmDelete
        '
        Me.cmDelete.Category = "Edit"
        Me.cmDelete.Image = CType(resources.GetObject("cmDelete.Image"), System.Drawing.Image)
        Me.cmDelete.Name = "cmDelete"
        Me.cmDelete.Shortcuts.Add(New DataDynamics.SharpUI.Toolbars.ShortcutKey("Delete", "", False, False, False))
        Me.cmDelete.Text = "cmEdit"
        '
        'tFind
        '
        Me.tFind.BeginGroup = True
        Me.tFind.Command = Me.cmFind
        Me.tFind.Name = "tFind"
        '
        'cmFind
        '
        Me.cmFind.Category = "Edit"
        Me.cmFind.Image = CType(resources.GetObject("cmFind.Image"), System.Drawing.Image)
        Me.cmFind.Name = "cmFind"
        Me.cmFind.Shortcuts.Add(New DataDynamics.SharpUI.Toolbars.ShortcutKey("F", "", False, False, True))
        Me.cmFind.Text = "Find"
        '
        'tFindNext
        '
        Me.tFindNext.Command = Me.cmFindNext
        Me.tFindNext.Name = "tFindNext"
        '
        'cmFindNext
        '
        Me.cmFindNext.Category = "Edit"
        Me.cmFindNext.Name = "cmFindNext"
        Me.cmFindNext.Shortcuts.Add(New DataDynamics.SharpUI.Toolbars.ShortcutKey("F3", "", False, False, False))
        Me.cmFindNext.Text = "Find Next"
        '
        'tReplace
        '
        Me.tReplace.Command = Me.cmReplace
        Me.tReplace.Name = "tReplace"
        '
        'cmReplace
        '
        Me.cmReplace.Category = "Edit"
        Me.cmReplace.Name = "cmReplace"
        Me.cmReplace.Shortcuts.Add(New DataDynamics.SharpUI.Toolbars.ShortcutKey("H", "", False, False, True))
        Me.cmReplace.Text = "Replace"
        '
        'tGoTo
        '
        Me.tGoTo.BeginGroup = True
        Me.tGoTo.Command = Me.cmGoTo
        Me.tGoTo.Name = "tGoTo"
        '
        'cmGoTo
        '
        Me.cmGoTo.Category = "Edit"
        Me.cmGoTo.Name = "cmGoTo"
        Me.cmGoTo.Shortcuts.Add(New DataDynamics.SharpUI.Toolbars.ShortcutKey("G", "", False, False, True))
        Me.cmGoTo.Text = "Go To..."
        '
        'tSelectAll
        '
        Me.tSelectAll.Command = Me.cmSelectAll
        Me.tSelectAll.Name = "tSelectAll"
        '
        'cmSelectAll
        '
        Me.cmSelectAll.Category = "Edit"
        Me.cmSelectAll.Name = "cmSelectAll"
        Me.cmSelectAll.Shortcuts.Add(New DataDynamics.SharpUI.Toolbars.ShortcutKey("A", "", False, False, True))
        Me.cmSelectAll.Text = "Select All"
        '
        'tDateTime
        '
        Me.tDateTime.BeginGroup = True
        Me.tDateTime.Command = Me.cmDateTime
        Me.tDateTime.Name = "tDateTime"
        '
        'cmDateTime
        '
        Me.cmDateTime.Category = "Edit"
        Me.cmDateTime.Name = "cmDateTime"
        Me.cmDateTime.Shortcuts.Add(New DataDynamics.SharpUI.Toolbars.ShortcutKey("F5", "", False, False, False))
        Me.cmDateTime.Text = "Date/Time"
        '
        'smFormat
        '
        Me.smFormat.Name = "smFormat"
        Me.smFormat.SubBand = Me.popupMenu3
        Me.smFormat.Text = "F&ormat"
        '
        'popupMenu3
        '
        Me.popupMenu3.AutoGenerated = True
        Me.popupMenu3.Text = "popupMenu3"
        Me.popupMenu3.Tools.AddRange(New DataDynamics.SharpUI.Toolbars.Tool() {Me.tWordWrap, Me.tFont})
        Me.popupMenu3.Name = "popupMenu3"
        '
        'tWordWrap
        '
        Me.tWordWrap.Command = Me.cmWordWrap
        Me.tWordWrap.Name = "tWordWrap"
        '
        'cmWordWrap
        '
        Me.cmWordWrap.Category = "Format"
        Me.cmWordWrap.Name = "cmWordWrap"
        Me.cmWordWrap.Text = "Word Wrap"
        '
        'tFont
        '
        Me.tFont.Command = Me.cmFont
        Me.tFont.Name = "tFont"
        '
        'cmFont
        '
        Me.cmFont.Category = "Format"
        Me.cmFont.Image = CType(resources.GetObject("cmFont.Image"), System.Drawing.Image)
        Me.cmFont.Name = "cmFont"
        Me.cmFont.Text = "Font"
        '
        'smView
        '
        Me.smView.Name = "smView"
        Me.smView.SubBand = Me.popupMenu4
        Me.smView.Text = "&View"
        '
        'popupMenu4
        '
        Me.popupMenu4.AutoGenerated = True
        Me.popupMenu4.Text = "popupMenu4"
        Me.popupMenu4.Tools.AddRange(New DataDynamics.SharpUI.Toolbars.Tool() {Me.tStatusBar, Me.tToolWindow})
        Me.popupMenu4.Name = "popupMenu4"
        '
        'tStatusBar
        '
        Me.tStatusBar.Command = Me.cmStatusBar
        Me.tStatusBar.Name = "tStatusBar"
        '
        'cmStatusBar
        '
        Me.cmStatusBar.Category = "View"
        Me.cmStatusBar.Name = "cmStatusBar"
        Me.cmStatusBar.Text = "Status Bar"
        '
        'tToolWindow
        '
        Me.tToolWindow.Command = Me.cmToolWindow
        Me.tToolWindow.Name = "tToolWindow"
        '
        'cmToolWindow
        '
        Me.cmToolWindow.Category = "View"
        Me.cmToolWindow.Name = "cmToolWindow"
        Me.cmToolWindow.Text = "Tool Window"
        '
        'smHelp
        '
        Me.smHelp.Name = "smHelp"
        Me.smHelp.SubBand = Me.popupMenu5
        Me.smHelp.Text = "&Help"
        '
        'popupMenu5
        '
        Me.popupMenu5.AutoGenerated = True
        Me.popupMenu5.Text = "popupMenu5"
        Me.popupMenu5.Tools.AddRange(New DataDynamics.SharpUI.Toolbars.Tool() {Me.tHelpTopics, Me.tAbout})
        Me.popupMenu5.Name = "popupMenu5"
        '
        'tHelpTopics
        '
        Me.tHelpTopics.Command = Me.cmHelpTopics
        Me.tHelpTopics.Name = "tHelpTopics"
        '
        'cmHelpTopics
        '
        Me.cmHelpTopics.Category = "Help"
        Me.cmHelpTopics.Image = CType(resources.GetObject("cmHelpTopics.Image"), System.Drawing.Image)
        Me.cmHelpTopics.Name = "cmHelpTopics"
        Me.cmHelpTopics.Text = "Help Topics"
        '
        'tAbout
        '
        Me.tAbout.Command = Me.cmAbout
        Me.tAbout.Name = "tAbout"
        '
        'cmAbout
        '
        Me.cmAbout.Category = "Help"
        Me.cmAbout.Name = "cmAbout"
        Me.cmAbout.Text = "About SDI..."
        '
        'twToolBox
        '
        Me.twToolBox.DockedSize = New System.Drawing.Size(292, 150)
        Me.twToolBox.FloatingSize = New System.Drawing.Size(150, 150)
        Me.twToolBox.Name = "twToolBox"
        Me.twToolBox.Text = "Tool Box"
        Me.twToolBox.UiToolbarManager = Me.UiToolbarManager1
        Me.twToolBox.Visible = False
        Me.twToolBox.Dock(DataDynamics.SharpUI.Toolbars.DockDirections.Right, DataDynamics.SharpUI.Toolbars.Edges.Right)
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
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.dockRight1)
        Me.Controls.Add(Me.dockLeft1)
        Me.Controls.Add(Me.dockTop1)
        Me.Controls.Add(Me.dockBottom1)
        Me.KeyPreview = True
        Me.Name = "Form1"
        Me.Text = "SDI"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmNew_Executed(ByVal sender As Object, ByVal e As DataDynamics.SharpUI.Toolbars.ToolbarManagerEventArgs) Handles cmNew.Executed
        Dim frm As New frmMDI
        frm.Show()
    End Sub

    Private Sub cmExit_Executed(ByVal sender As Object, ByVal e As DataDynamics.SharpUI.Toolbars.ToolbarManagerEventArgs) Handles cmExit.Executed
        Me.Close()
    End Sub

    Private Sub cmOpen_Executed(ByVal sender As Object, ByVal e As DataDynamics.SharpUI.Toolbars.ToolbarManagerEventArgs) Handles cmOpen.Executed
        Dim frm As New frmEzMDI
        frm.Show()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim f As New frmToolbox
        f.Show()
        Me.twToolBox.HostedControl = f
    End Sub

    Private Sub cmToolWindow_Executed(ByVal sender As Object, ByVal e As DataDynamics.SharpUI.Toolbars.ToolbarManagerEventArgs) Handles cmToolWindow.Executed
        Me.twToolBox.Visible = Not Me.twToolBox.Visible
    End Sub
End Class
