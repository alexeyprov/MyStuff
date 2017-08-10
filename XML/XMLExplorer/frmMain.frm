VERSION 5.00
Object = "{6B7E6392-850A-101B-AFC0-4210102A8DA7}#1.3#0"; "Comctl32.ocx"
Object = "{38911DA0-E448-11D0-84A3-00DD01104159}#1.1#0"; "COMCT332.OCX"
Object = "{EAB22AC0-30C1-11CF-A7EB-0000C05BAE0B}#1.1#0"; "shdocvw.dll"
Begin VB.Form frmMain 
   Caption         =   "---"
   ClientHeight    =   5010
   ClientLeft      =   165
   ClientTop       =   735
   ClientWidth     =   7995
   Icon            =   "frmMain.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   5010
   ScaleWidth      =   7995
   StartUpPosition =   3  'Windows Default
   Begin SHDocVwCtl.WebBrowser wbShow 
      Height          =   375
      Left            =   5760
      TabIndex        =   12
      Top             =   4200
      Width           =   1575
      ExtentX         =   2778
      ExtentY         =   661
      ViewMode        =   1
      Offline         =   0
      Silent          =   0
      RegisterAsBrowser=   0
      RegisterAsDropTarget=   1
      AutoArrange     =   -1  'True
      NoClientEdge    =   0   'False
      AlignLeft       =   0   'False
      NoWebView       =   0   'False
      HideFileNames   =   0   'False
      SingleClick     =   0   'False
      SingleSelection =   0   'False
      NoFolders       =   0   'False
      Transparent     =   0   'False
      ViewID          =   "{0057D0E0-3573-11CF-AE69-08002B2E1262}"
      Location        =   "http:///"
   End
   Begin VB.PictureBox picSplitter 
      BackColor       =   &H00808080&
      BorderStyle     =   0  'None
      FillColor       =   &H00808080&
      Height          =   4800
      Left            =   5400
      ScaleHeight     =   2090.126
      ScaleMode       =   0  'User
      ScaleWidth      =   780
      TabIndex        =   6
      Top             =   705
      Visible         =   0   'False
      Width           =   72
   End
   Begin VB.ComboBox cboPath 
      Height          =   315
      Left            =   3600
      Style           =   2  'Dropdown List
      TabIndex        =   11
      Top             =   1200
      Visible         =   0   'False
      Width           =   4455
   End
   Begin ComCtl3.CoolBar CoolBar1 
      Align           =   1  'Align Top
      Height          =   450
      Left            =   0
      TabIndex        =   8
      Top             =   0
      Width           =   7995
      _ExtentX        =   14102
      _ExtentY        =   794
      BandCount       =   2
      ImageList       =   "imlIcons"
      VariantHeight   =   0   'False
      EmbossPicture   =   -1  'True
      _CBWidth        =   7995
      _CBHeight       =   450
      _Version        =   "6.7.9782"
      Child1          =   "tbToolbar"
      MinWidth1       =   1995
      MinHeight1      =   390
      Width1          =   1995
      FixedBackground1=   0   'False
      UseCoolbarPicture1=   0   'False
      NewRow1         =   0   'False
      BandEmbossPicture1=   -1  'True
      Caption2        =   "Template:"
      Image2          =   "5"
      Child2          =   "cboTemplate"
      MinHeight2      =   315
      Width2          =   2895
      FixedBackground2=   0   'False
      UseCoolbarPicture2=   0   'False
      NewRow2         =   0   'False
      Begin ComctlLib.Toolbar tbToolbar 
         Height          =   390
         Left            =   165
         TabIndex        =   9
         Top             =   30
         Width           =   1995
         _ExtentX        =   3519
         _ExtentY        =   688
         ButtonWidth     =   635
         ButtonHeight    =   582
         AllowCustomize  =   0   'False
         ImageList       =   "imlIcons"
         _Version        =   327682
         BeginProperty Buttons {0713E452-850A-101B-AFC0-4210102A8DA7} 
            NumButtons      =   7
            BeginProperty Button1 {0713F354-850A-101B-AFC0-4210102A8DA7} 
               Key             =   "New document"
               Object.ToolTipText     =   "New document"
               Object.Tag             =   ""
               ImageIndex      =   1
            EndProperty
            BeginProperty Button2 {0713F354-850A-101B-AFC0-4210102A8DA7} 
               Key             =   "New template"
               Object.ToolTipText     =   "New template"
               Object.Tag             =   ""
               ImageIndex      =   2
            EndProperty
            BeginProperty Button3 {0713F354-850A-101B-AFC0-4210102A8DA7} 
               Object.Tag             =   ""
               Style           =   4
               Object.Width           =   100
               MixedState      =   -1  'True
            EndProperty
            BeginProperty Button4 {0713F354-850A-101B-AFC0-4210102A8DA7} 
               Key             =   "Open"
               Object.ToolTipText     =   "Open template"
               Object.Tag             =   ""
               ImageIndex      =   3
            EndProperty
            BeginProperty Button5 {0713F354-850A-101B-AFC0-4210102A8DA7} 
               Key             =   "Edit"
               Object.ToolTipText     =   "Edit template"
               Object.Tag             =   ""
               ImageIndex      =   7
            EndProperty
            BeginProperty Button6 {0713F354-850A-101B-AFC0-4210102A8DA7} 
               Object.Tag             =   ""
               Style           =   4
               Object.Width           =   60
               MixedState      =   -1  'True
            EndProperty
            BeginProperty Button7 {0713F354-850A-101B-AFC0-4210102A8DA7} 
               Key             =   "Refresh"
               Object.ToolTipText     =   "Refresh"
               Object.Tag             =   ""
               ImageIndex      =   4
            EndProperty
         EndProperty
      End
      Begin VB.ComboBox cboTemplate 
         Height          =   315
         Left            =   3195
         Style           =   2  'Dropdown List
         TabIndex        =   10
         Top             =   60
         Width           =   4710
      End
   End
   Begin VB.FileListBox File1 
      Height          =   2235
      Left            =   2760
      TabIndex        =   7
      Top             =   2760
      Visible         =   0   'False
      Width           =   2295
   End
   Begin ComctlLib.TreeView tvTreeView 
      Height          =   4800
      Left            =   0
      TabIndex        =   5
      Top             =   720
      Width           =   2010
      _ExtentX        =   3545
      _ExtentY        =   8467
      _Version        =   327682
      HideSelection   =   0   'False
      Indentation     =   566
      LabelEdit       =   1
      LineStyle       =   1
      Style           =   7
      ImageList       =   "ImageList1"
      Appearance      =   1
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
   End
   Begin ComctlLib.ListView lvListView 
      Height          =   4800
      Left            =   2040
      TabIndex        =   4
      Top             =   720
      Width           =   3210
      _ExtentX        =   5662
      _ExtentY        =   8467
      View            =   3
      Arrange         =   2
      LabelEdit       =   1
      LabelWrap       =   0   'False
      HideSelection   =   0   'False
      _Version        =   327682
      SmallIcons      =   "imlIcons"
      ForeColor       =   -2147483640
      BackColor       =   -2147483643
      BorderStyle     =   1
      Appearance      =   1
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      NumItems        =   3
      BeginProperty ColumnHeader(1) {0713E8C7-850A-101B-AFC0-4210102A8DA7} 
         Key             =   ""
         Object.Tag             =   ""
         Text            =   "What"
         Object.Width           =   4234
      EndProperty
      BeginProperty ColumnHeader(2) {0713E8C7-850A-101B-AFC0-4210102A8DA7} 
         SubItemIndex    =   1
         Key             =   ""
         Object.Tag             =   ""
         Text            =   "Who"
         Object.Width           =   1764
      EndProperty
      BeginProperty ColumnHeader(3) {0713E8C7-850A-101B-AFC0-4210102A8DA7} 
         SubItemIndex    =   2
         Key             =   ""
         Object.Tag             =   ""
         Text            =   "File"
         Object.Width           =   1764
      EndProperty
   End
   Begin VB.PictureBox picTitles 
      Align           =   1  'Align Top
      Appearance      =   0  'Flat
      ForeColor       =   &H80000008&
      Height          =   300
      Left            =   0
      ScaleHeight     =   270
      ScaleWidth      =   7965
      TabIndex        =   1
      TabStop         =   0   'False
      Top             =   450
      Width           =   7995
      Begin VB.Label lblTitle 
         BorderStyle     =   1  'Fixed Single
         Caption         =   " Details:"
         Height          =   270
         Index           =   1
         Left            =   2160
         TabIndex        =   3
         Tag             =   " ListView:"
         Top             =   0
         Width           =   3210
      End
      Begin VB.Label lblTitle 
         BorderStyle     =   1  'Fixed Single
         Height          =   270
         Index           =   0
         Left            =   0
         TabIndex        =   2
         Tag             =   " TreeView:"
         Top             =   0
         Width           =   2010
      End
   End
   Begin ComctlLib.StatusBar sbStatusBar 
      Align           =   2  'Align Bottom
      Height          =   270
      Left            =   0
      TabIndex        =   0
      Top             =   4740
      Width           =   7995
      _ExtentX        =   14102
      _ExtentY        =   476
      SimpleText      =   ""
      _Version        =   327682
      BeginProperty Panels {0713E89E-850A-101B-AFC0-4210102A8DA7} 
         NumPanels       =   2
         BeginProperty Panel1 {0713E89F-850A-101B-AFC0-4210102A8DA7} 
            AutoSize        =   1
            Object.Width           =   11033
            Picture         =   "frmMain.frx":0442
            Text            =   "Ready"
            TextSave        =   "Ready"
            Object.Tag             =   ""
         EndProperty
         BeginProperty Panel2 {0713E89F-850A-101B-AFC0-4210102A8DA7} 
            AutoSize        =   2
            Picture         =   "frmMain.frx":0534
            Object.Tag             =   ""
            Object.ToolTipText     =   "Working path"
         EndProperty
      EndProperty
   End
   Begin VB.Image imgVSplitter 
      Height          =   120
      Left            =   5520
      MousePointer    =   7  'Size N S
      Top             =   3840
      Width           =   2280
   End
   Begin ComctlLib.ImageList ImageList1 
      Left            =   5880
      Top             =   2040
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   16
      ImageHeight     =   16
      MaskColor       =   16711935
      _Version        =   327682
      BeginProperty Images {0713E8C2-850A-101B-AFC0-4210102A8DA7} 
         NumListImages   =   5
         BeginProperty ListImage1 {0713E8C3-850A-101B-AFC0-4210102A8DA7} 
            Picture         =   "frmMain.frx":0646
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {0713E8C3-850A-101B-AFC0-4210102A8DA7} 
            Picture         =   "frmMain.frx":0758
            Key             =   ""
         EndProperty
         BeginProperty ListImage3 {0713E8C3-850A-101B-AFC0-4210102A8DA7} 
            Picture         =   "frmMain.frx":086A
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {0713E8C3-850A-101B-AFC0-4210102A8DA7} 
            Picture         =   "frmMain.frx":097C
            Key             =   ""
         EndProperty
         BeginProperty ListImage5 {0713E8C3-850A-101B-AFC0-4210102A8DA7} 
            Picture         =   "frmMain.frx":0A8E
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin VB.Image imgSplitter 
      Height          =   4785
      Left            =   3720
      MousePointer    =   9  'Size W E
      Top             =   720
      Width           =   120
   End
   Begin ComctlLib.ImageList imlIcons 
      Left            =   5760
      Top             =   1080
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   16
      ImageHeight     =   16
      MaskColor       =   16711935
      _Version        =   327682
      BeginProperty Images {0713E8C2-850A-101B-AFC0-4210102A8DA7} 
         NumListImages   =   7
         BeginProperty ListImage1 {0713E8C3-850A-101B-AFC0-4210102A8DA7} 
            Picture         =   "frmMain.frx":0BA0
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {0713E8C3-850A-101B-AFC0-4210102A8DA7} 
            Picture         =   "frmMain.frx":0CB2
            Key             =   ""
         EndProperty
         BeginProperty ListImage3 {0713E8C3-850A-101B-AFC0-4210102A8DA7} 
            Picture         =   "frmMain.frx":0DC4
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {0713E8C3-850A-101B-AFC0-4210102A8DA7} 
            Picture         =   "frmMain.frx":0ED6
            Key             =   ""
         EndProperty
         BeginProperty ListImage5 {0713E8C3-850A-101B-AFC0-4210102A8DA7} 
            Picture         =   "frmMain.frx":0FE8
            Key             =   ""
         EndProperty
         BeginProperty ListImage6 {0713E8C3-850A-101B-AFC0-4210102A8DA7} 
            Picture         =   "frmMain.frx":10FA
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {0713E8C3-850A-101B-AFC0-4210102A8DA7} 
            Picture         =   "frmMain.frx":120C
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin VB.Menu mnuFile 
      Caption         =   "&File"
      Begin VB.Menu mnuFileNew 
         Caption         =   "&New"
         Begin VB.Menu mnuFileNewTemplate 
            Caption         =   "&Template"
         End
         Begin VB.Menu mnuFileNewDocument 
            Caption         =   "&Document"
         End
      End
      Begin VB.Menu mnuFileOpen 
         Caption         =   "&Open"
      End
      Begin VB.Menu mnuSep1 
         Caption         =   "-"
      End
      Begin VB.Menu mnuFileEdit 
         Caption         =   "&Edit template"
      End
      Begin VB.Menu mnuSep2 
         Caption         =   "-"
      End
      Begin VB.Menu mnuFileExit 
         Caption         =   "E&xit"
      End
   End
   Begin VB.Menu mnuView 
      Caption         =   "&View"
      Begin VB.Menu mnuViewRefresh 
         Caption         =   "&Refresh"
         Shortcut        =   {F5}
      End
   End
   Begin VB.Menu mnuHelp 
      Caption         =   "&Help"
      Begin VB.Menu mnuHelpAbout 
         Caption         =   "&About..."
      End
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

'/*-----------------------------------------------
'  GLOBALS
'-----------------------------------------------*/
Private g_StdViewPage As String
Private g_isDefaultView As Boolean
Private g_curTitleBar As String
Private g_curUser As String
Private g_mbMoving As Boolean
Private g_itemPrev As ListItem


'/*-----------------------------------------------
'  CONSTANTS
'-----------------------------------------------*/
Const STDTITLE = "XML Explorer"
Const STDVIEWPAGE = "default.htm"
Const ERRORMSG = " <The template file is corrupted or missing.>"
Const TEMPLATEFILE = "template.xmt"
Const UNKNOWN = "<Unknown>"
Const SEP = "\"
Const SPLITLIMIT = 500
Const YSPLITLIMIT = 2000
Const ICON_ROOT = 1
Const ICON_FOLDER = 2
Const ICON_FIELD = 3
Const ICON_INFO = 4
Const ICON_PATH = 5
Const SB_INFO = 1
Const SB_PATH = 2
Const CBAR_TOOLBAR = 1
Const CBAR_COMBO = 2
Const LI_WHO = 1
Const LI_FILE = 2


Private Sub cboTemplate_Click()
    OpenTemplate cboPath.List(cboTemplate.ListIndex)
End Sub

Private Sub CoolBar1_HeightChanged(ByVal NewHeight As Single)
   SizeControls imgSplitter.Left
End Sub

Private Sub Form_Activate()
    AdjustToolbar tbToolbar.hwnd
    CoolBar1.Refresh
End Sub

Private Sub Form_Load()
    Dim i As Integer
    Dim s As String
        
    Set g_itemPrev = Nothing
    picTitles.BorderStyle = 0
    Me.Caption = STDTITLE
    g_curTitleBar = Me.Caption
    g_curUser = UNKNOWN
    
    ' first of all, try to get the registered base path
    g_BasePath = GetSetting(App.Title, "Settings", "BasePath", "")
    If Len(g_BasePath) = 0 Then
        frmPath.txtBasePath = STDBASEPATH
        frmPath.Show vbModal
    End If
    
    If Len(g_BasePath) = 0 Then
        MsgBox "You need to register a base path for XML Explorer to work properly." _
           , , STDTITLE
        Unload Me
        End
    End If
    
    ' read the settings from the registry
    Me.Left = GetSetting(App.Title, "Settings", "MainLeft", 1000)
    Me.Top = GetSetting(App.Title, "Settings", "MainTop", 1000)
    Me.Width = GetSetting(App.Title, "Settings", "MainWidth", 8500)
    Me.Height = GetSetting(App.Title, "Settings", "MainHeight", 6500)
    CoolBar1.Bands.Item(CBAR_COMBO).NewRow = GetSetting(App.Title, "Settings", "NewRow", 0)
    CoolBar1.Bands.Item(CBAR_TOOLBAR).Width = GetSetting(App.Title, "Settings", "BandWidth", 2000)
    
    ' fill the template collection
    For i = 1 To Val(GetSetting(App.Title, "Templates", "Count"))
        AddCombo cboTemplate, _
            GetSetting(App.Title, "Templates\" + CStr(i), "Title"), _
            GetSetting(App.Title, "Templates\" + CStr(i), "Path")
    Next
    
    ' read in the XMT template file and adjust the treeview
    s = GetSetting(App.Title, "Settings", "WorkingPath", "")
    If Len(s) Then
        OpenTemplate s
    End If
    
End Sub


Private Sub Form_Unload(Cancel As Integer)
    Dim i As Integer
    If Me.WindowState <> vbMinimized Then
        SaveSetting App.Title, "Settings", "MainLeft", Me.Left
        SaveSetting App.Title, "Settings", "MainTop", Me.Top
        SaveSetting App.Title, "Settings", "MainWidth", Me.Width
        SaveSetting App.Title, "Settings", "MainHeight", Me.Height
        SaveSetting App.Title, "Settings", "WorkingPath", g_mainPath
        SaveSetting App.Title, "Settings", "NewRow", CoolBar1.Bands.Item(CBAR_COMBO).NewRow
        SaveSetting App.Title, "Settings", "BandWidth", CoolBar1.Bands.Item(CBAR_TOOLBAR).Width
    End If
    
    SaveSetting App.Title, "Settings", "BasePath", g_BasePath
    SaveSetting App.Title, "Templates", "Count", cboTemplate.ListCount
    For i = 1 To cboTemplate.ListCount
        SaveSetting App.Title, "Templates\" + CStr(i), "Title", cboTemplate.List(i - 1)
        SaveSetting App.Title, "Templates\" + CStr(i), "Path", cboPath.List(i - 1)
    Next
    
    Unload Me
End Sub


'/*-----------------------------------------------
' This routine will read in the TEMPLATE.XMT file
' and populate the treeview with its [root],
' [folder] and [field] items.
'-----------------------------------------------*/
Private Function ReadTemplate() As Boolean
On Error GoTo ErrHandler

    Dim xml As New XMLDocument
    Dim xmlItem As IXMLElement
    Dim xmlRoot As IXMLElementCollection
    Dim n As Node
    Dim sCaption, sDisplay, sUser, sTitle As String
    
    ' get the XML template file
    xml.url = g_mainPath & TEMPLATEFILE
    Set xmlRoot = xml.Root.Children
    
    
    ' set the treeview's label
    sTitle = xml.Root.getAttribute("title")
    If Len(sTitle) = 0 Then
        sTitle = "Namespace of: " + g_mainPath
    End If
    lblTitle(0).Caption = " " + sTitle
    
    ' set the caption bar
    sCaption = xml.Root.getAttribute("caption")
    g_curTitleBar = STDTITLE
    If Len(sCaption) Then
        g_curTitleBar = sCaption
        Me.Caption = sCaption
    End If
        
    ' set the user name onto the caption bar
    sUser = xml.Root.getAttribute("user")
    g_curUser = UNKNOWN
    If Len(sUser) Then
        g_curUser = sUser
        Me.Caption = Me.Caption + "  [" + sUser + "]"
    End If
        
    ' set the standard VIEW page
    sDisplay = xml.Root.getAttribute("display")
    If Len(sDisplay) Then
        g_StdViewPage = g_mainPath + sDisplay
        g_isDefaultView = False
    Else
        g_StdViewPage = g_BasePath + STDVIEWPAGE
        g_isDefaultView = True
    End If

        
    ' create [root] nodes
    For Each xmlItem In xmlRoot
        Dim sImg As String
        Dim nIcon As Integer
        
        ' get title and image
        sTitle = xmlItem.getAttribute("title")
        sImg = xmlItem.getAttribute("img")
        
        ' get the right icon
        nIcon = GetNodeIcon(g_mainPath & sImg, ICON_ROOT)
        
        ' add the node storing a lowercase key
        Set n = tvTreeView.Nodes.Add(, , LCase(sTitle), sTitle, _
            nIcon)
        
        ' scan for sub-items
        ReadFolders xmlItem.Children, n
    Next
    
    Set xml = Nothing
    ReadTemplate = True
    Exit Function
    
ErrHandler:
    tvTreeView.Nodes.Clear
    tvTreeView.Style = tvwTextOnly
    tvTreeView.Nodes.Add , , , ERRORMSG
    Exit Function
End Function


'/*-----------------------------------------------
' This routine will read in an XML collection
' and populate the treeview with its [folder]
' and [field] items.
'-----------------------------------------------*/
Private Sub ReadFolders(ByVal xmlFolders As IXMLElementCollection, _
  nRoot As Node)
    Dim xmlItem As IXMLElement
        
    For Each xmlItem In xmlFolders
        Dim sPath, sTitle, sImg As String
        Dim n As Node
        Dim nIcon As Integer
        
        ' get title and image
        sTitle = xmlItem.getAttribute("title")
        sImg = xmlItem.getAttribute("img")
        
        ' get the right icon
        nIcon = GetNodeIcon(g_mainPath & sImg, ICON_FOLDER)
        
        ' add the node storing a lowercase key
        sPath = LCase(nRoot.Key + SEP + sTitle)
        Set n = tvTreeView.Nodes.Add(nRoot, tvwChild, sPath, _
           sTitle, nIcon)
        
        ' scan for sub-items
        ReadFields xmlItem.Children, n
    Next
End Sub


'/*-----------------------------------------------
' This routine will read in an XML collection
' and populate the treeview with its [field]
' items.
'-----------------------------------------------*/
Private Sub ReadFields(ByVal xmlFields As IXMLElementCollection, _
  nRoot As Node)
  
    Dim xmlItem As IXMLElement
    
    For Each xmlItem In xmlFields
        Dim sTitle, sImg, sPath As String
        Dim n As Node
        Dim nIcon As Integer
        
        ' get title and image
        sTitle = xmlItem.getAttribute("title")
        sImg = xmlItem.getAttribute("img")
        
        ' get the right icon and add the node
        nIcon = GetNodeIcon(g_mainPath & sImg, ICON_FIELD)
        
        ' add the node storing a lowercase key
        sPath = LCase(nRoot.Key + SEP + sTitle)
        Set n = tvTreeView.Nodes.Add(nRoot, tvwChild, sPath, _
           sTitle, nIcon)
        n.Tag = "UpdateListView"
    Next
End Sub


'/*-----------------------------------------------
' This routine will read in all the XML documents
' available in the current path and populate
' the treeview.
'-----------------------------------------------*/
Private Sub ReadDocuments()
    Dim xml As New XMLDocument
    Dim xmlLink As IXMLElement
    Dim xmlInfo As IXMLElementCollection
    Dim i As Integer
    Dim n As Node
    Dim sFile, sPath, sRoot, sFolder, sField As String

    ' using a FileListBox control to get all the XMLs
    File1.Path = g_mainPath
    File1.Pattern = "*.xml"
    File1.Refresh

    ' scan the XML files found
On Error Resume Next
    For i = 0 To File1.ListCount - 1
        sFile = g_mainPath + File1.List(i)
        xml.url = sFile
        
        ' make sure objects are set to nothing
        Set xmlLink = Nothing
        Set xmlInfo = Nothing
        Set n = Nothing
        
        ' get the required LINK info from the file
        Set xmlLink = xml.Root.Children.Item("link")
        If xmlLink Is Nothing Then
            GoTo NextFile
        End If
        
        ' read in the link information
        sRoot = xmlLink.getAttribute("Root")
        sFolder = xmlLink.getAttribute("Folder")
        sField = xmlLink.getAttribute("Field")
        
        ' search the node to add using a lowercase key
        sPath = LCase(sRoot + SEP + sFolder + SEP + sField)
        Set n = tvTreeView.Nodes(sPath)
        
        ' get info to display
        Set xmlInfo = xml.Root.Children.Item("info").Children
        If xmlInfo Is Nothing Then
            GoTo NextFile
        End If
        
        ' read in the info
        If Not (n Is Nothing) Then
            ReadInfo xmlInfo, n, sFile
        End If
        
NextFile:
    Next
    
    Set xml = Nothing
    Set n = Nothing
    Set xmlLink = Nothing
End Sub


'/*-----------------------------------------------
' This routine will read in all the info tag
' from a valid XML document. Then it populates
' the treeview.
'-----------------------------------------------*/
Private Sub ReadInfo(ByVal xmlInfo As IXMLElementCollection, _
  nRoot As Node, ByVal sFile As String)
    
    Dim xmlItem As IXMLElement
    Dim sWhat, sWho, sText As String
    Dim n As Node
    Dim i As Integer
    
    
    ' scan the INFO collection
    i = 0
    For Each xmlItem In xmlInfo
        
        ' verify it is a RECORD tag
        ' NB: This is not strictly necessary, but I
        ' require it for clarity.
        If LCase(xmlItem.tagName) <> "record" Then
            GoTo NextItem
        End If
        
        ' read all the possible attributes
        sWhat = xmlItem.getAttribute("what")
        sWho = xmlItem.getAttribute("who")
         
        ' format such string as: WHAT (WHO)
        If Len(sWhat) Then
            sText = sWhat
        End If
        If Len(sWho) Then
            sText = sText + "  (" + sWho + ")"
        End If

        ' add the node (and stores the file name)
        Set n = tvTreeView.Nodes.Add(nRoot, tvwChild, , sText, ICON_INFO)
        n.Tag = sFile + "," + CStr(i)
        i = i + 1
        
NextItem:
   Next
End Sub


'/*-----------------------------------------------
' This routine will display the free text info
' about a single record in a XML document.
'-----------------------------------------------*/
Private Sub ShowData(ByVal li As ListItem)
On Error Resume Next
    Dim xml As New XMLDocument
    Dim xmlItem As IXMLElement
    Dim doc As HTMLDocument
    Dim nPos, i As Integer
    Dim s, sTag, sType As String

    
    ' lock the XML file
    nPos = li.Tag
    xml.url = g_mainPath + li.SubItems(LI_FILE)
    Set xmlItem = xml.Root.Children.Item("info").Children.Item("record", nPos)

    ' ---------------------------
    ' Now is filling in the page
    ' ---------------------------
    
    ' determine which is the view page to follow
    If g_isDefaultView Then
        For i = 0 To xmlItem.Children.length - 1
            sTag = xmlItem.Children.Item(i).tagName
            s = s + "<b>" + sTag + " : </b><i>" + xmlItem.Children.Item(i).Text + "<br></i>"
        Next
        wbShow.Document.All("content").innerHTML = s
    Else
        wbShow.Document.All("what").innerText = li.Text
        wbShow.Document.All("who").innerText = li.SubItems(LI_WHO)

        Set doc = wbShow.Document
        For i = 0 To xmlItem.Children.length - 1
           sTag = xmlItem.Children.Item(i).tagName

           ' examine the TYPE attribute of the tag
           ' 3 types supported: img, link, email
           sType = xmlItem.Children.Item(i).getAttribute("type")

           If LCase(sType) = "img" Then
                doc.All(sTag).src = xmlItem.Children.Item(i).Text
           ElseIf LCase(sType) = "email" Then
                Dim n As Integer
                n = InStr(1, xmlItem.Children.Item(i).Text, "@")
                If n > 0 Then
                    doc.All(sTag).href = "mailto:" + xmlItem.Children.Item(i).Text
                Else
                    doc.All(sTag).removeAttribute ("href")
                End If
           ElseIf LCase(sType) = "link" Then
                doc.All(sTag).href = xmlItem.Children.Item(i).Text
           End If
           
           doc.All(sTag).innerText = xmlItem.Children.Item(i).Text
        Next
    End If
    
    ' make it visible
    ToggleHtmlView True
End Sub

Private Sub ClearData(ByVal li As ListItem)
On Error Resume Next
    Dim xml As New XMLDocument
    Dim xmlItem As IXMLElement
    Dim doc As HTMLDocument
    Dim nPos, i As Integer
    Dim s, sTag As String

    nPos = li.Tag
    xml.url = g_mainPath + li.SubItems(LI_FILE)
    Set xmlItem = xml.Root.Children.Item("info").Children.Item("record", nPos)

    ' ---------------------------
    ' Now is filling in the page
    ' ---------------------------
    
    ' determine which is the view page to follow
    If g_isDefaultView Then
        For i = 0 To xmlItem.Children.length - 1
            sTag = xmlItem.Children.Item(i).tagName
            s = s + "<b>" + sTag + " : </b><i>" + xmlItem.Children.Item(i).Text + "<br></i>"
        Next
        wbShow.Document.All("content").innerHTML = s
    Else
        wbShow.Document.All("what").innerText = li.Text
        wbShow.Document.All("who").innerText = li.SubItems(LI_WHO)

        Set doc = wbShow.Document
        For i = 0 To xmlItem.Children.length - 1
           sTag = xmlItem.Children.Item(i).tagName

           If LCase(sTag) = "logo" Then
                doc.All(sTag).src = ""
           ElseIf LCase(sTag) = "contact" Then
                'MsgBox xmlItem.Children.Item(i).Text
                doc.All(sTag).href = ""
                doc.All(sTag).innerText = ""
           Else
                doc.All(sTag).innerText = ""
           End If
        Next
    End If
    
    ' make it visible
    ToggleHtmlView True
End Sub


'/*-----------------------------------------------
' This routine will run Notepad to allow users
' to edit the current template.
'-----------------------------------------------*/
Private Sub EditTemplate()
   Shell "notepad.exe " + g_mainPath + "template.xmt", vbNormalFocus
End Sub


'/*-----------------------------------------------
' This routine will reload all the info from the
' current working path and refreshes the UI.
'-----------------------------------------------*/
Private Sub ViewRefresh()
    SetText SB_PATH, g_mainPath
    tvTreeView.Nodes.Clear
    lblTitle(0).Caption = ""
    Me.Caption = STDTITLE
    
    ' read the template
    tvTreeView.Enabled = True
    tvTreeView.Style = tvwTreelinesPlusMinusPictureText
    
    If ReadTemplate Then
        ReadDocuments
    End If
    
    If Len(lblTitle(0).Caption) Then
        AddCombo cboTemplate, lblTitle(0).Caption, g_mainPath
    End If
End Sub


'/*-----------------------------------------------
' This routine will allow to select another
' XMT template from another directory.
'-----------------------------------------------*/
Private Sub FileOpen()
    Dim s As String
    Dim shl As New Shell
    Dim f As Folder
        
    ' choose a new working path
    Set f = Nothing
    Set f = shl.BrowseForFolder(Me.hwnd, _
        "Choose a new XML template:", 0, g_BasePath)
    If f Is Nothing Then
        Exit Sub
    End If
    
    ' refresh the view
    OpenTemplate f.Items.Item.Path
                
    Set f = Nothing
    Set shl = Nothing
End Sub


'/*-----------------------------------------------
' This routine opens the specified template
' file.
'-----------------------------------------------*/
Private Sub OpenTemplate(ByVal sTemplate As String)
    g_mainPath = sTemplate
    If Right(g_mainPath, 1) <> "\" Then
        g_mainPath = g_mainPath + "\"
    End If
    
    ViewRefresh
    lvListView.ListItems.Clear
    wbShow.Navigate g_StdViewPage
End Sub


'=======================================================
' UI FEATURES
'=======================================================

Private Sub AddCombo(ByVal cbo As ComboBox, ByVal sText As String, _
  ByVal sPath As String)
    Dim i As Integer
    
    i = SendMessageString(cbo.hwnd, CB_FINDSTRINGEXACT, -1, sText)
    If i < 0 Then
        cbo.AddItem sText
        cboPath.AddItem sPath
        cbo.ListIndex = cbo.ListCount - 1
    Else
        cbo.ListIndex = i
    End If
End Sub


Private Sub imgVSplitter_MouseDown(Button As Integer, Shift As Integer, x As Single, y As Single)
    With imgVSplitter
        picSplitter.Move .Left, .Top, .Width, .Height
    End With
    picSplitter.Visible = True
    g_mbMoving = True
End Sub

Private Sub imgVSplitter_MouseMove(Button As Integer, Shift As Integer, x As Single, y As Single)
    Dim sglPos As Single

    If g_mbMoving Then
        sglPos = y + imgVSplitter.Top
        If sglPos < YSPLITLIMIT Then
            picSplitter.Top = YSPLITLIMIT
        ElseIf sglPos > Me.Height - YSPLITLIMIT Then
            picSplitter.Top = Me.Height - YSPLITLIMIT
        Else
            picSplitter.Top = sglPos
        End If
        picSplitter.Height = 60
        picSplitter.Width = lvListView.Width
    End If

End Sub

Private Sub imgVSplitter_MouseUp(Button As Integer, Shift As Integer, x As Single, y As Single)
    Dim g As Integer
    
    lvListView.Height = picSplitter.Top - lvListView.Top
    g = wbShow.Top + wbShow.Height
    wbShow.Top = picSplitter.Top + 50
    wbShow.Height = g - wbShow.Top

    imgVSplitter.Left = lvListView.Left
    imgVSplitter.Top = lvListView.Top + lvListView.Height + 1
    imgVSplitter.Width = lvListView.Width
    
    picSplitter.Visible = False
    g_mbMoving = False
End Sub

Private Sub lvListView_click()
On Error Resume Next
    If g_itemPrev <> lvListView.SelectedItem _
            And Not g_itemPrev Is Nothing Then
        ClearData g_itemPrev
        Set g_itemPrev = lvListView.SelectedItem
    End If
    
    ShowData lvListView.SelectedItem
End Sub

Private Sub mnuFileEdit_Click()
    EditTemplate
End Sub

Private Sub mnuFileNewTemplate_Click()
    NewTemplate
End Sub

Private Sub mnuFileNewDocument_Click()
    NewDocument
End Sub

Private Sub mnuFileOpen_Click()
    FileOpen
End Sub


Private Sub mnuFileExit_Click()
    Unload Me
End Sub


Private Sub mnuHelpAbout_Click()
    HelpAbout
    
    ' avoiding this, the toolbar looses its flat style...
    AdjustToolbar tbToolbar.hwnd
End Sub


Private Sub tvTreeView_Click()
On Error Resume Next
    Dim li As ListItem
    Dim i As Integer
    Dim n, nSelected As Node
    Dim s, sFile As String
    Dim xml As New XMLDocument
    Dim xmlInfo As IXMLElementCollection
    Dim xmlItem As IXMLElement
    
    SetText SB_INFO, tvTreeView.SelectedItem.FullPath
    wbShow.Refresh
    ToggleHtmlView False
    
    ' skip if the selected node is a root...
    If tvTreeView.SelectedItem.Parent Is Nothing Then
        Exit Sub
    End If
    
    
    ' fill the listview
    If tvTreeView.SelectedItem.Tag = "UpdateListView" Then
        Set nSelected = tvTreeView.SelectedItem
    Else
    If tvTreeView.SelectedItem.Parent.Tag = "UpdateListView" Then
        Set nSelected = tvTreeView.SelectedItem.Parent
    Else
        Exit Sub
    End If
    End If
     
             
    ' fill the listview
    lvListView.ListItems.Clear
    If nSelected.Children = 0 Then
        Exit Sub
    End If
        
    ' get the INFO section from the XML file
    Set n = nSelected.Child
    For i = 0 To nSelected.Children - 1
        Dim x, nPos As Integer
            ' extract the file+ext sub-string and instantiate
            ' the XML document
            
            ' The TAG string is given by "name,pos". First I get
            ' the comma position and then get the nPos which
            ' denotes the record number.
            x = InStr(1, n.Tag, ",")
            nPos = Right(n.Tag, Len(n.Tag) - x)
            
            ' Now truncate the string, istantiate the XML
            ' document and finally remove the path
            sFile = Left(n.Tag, x - 1)
            xml.url = sFile
            sFile = Right(sFile, Len(sFile) - Len(g_mainPath))

            ' Access the INFO collection
            Set xmlInfo = xml.Root.Children.Item("info").Children
            Set xmlItem = xmlInfo.Item(nPos)
            
            ' fill in the WHAT item
            s = xmlItem.getAttribute("WHAT")
            Set li = lvListView.ListItems.Add(, , s, , 6)
            li.Tag = nPos
                
            ' fill in the WHO item
            s = xmlItem.getAttribute("WHO")
            li.SubItems(LI_WHO) = s
            
            ' fill in the FILE item
            li.SubItems(LI_FILE) = sFile
            
            Set n = n.Next
    Next
    
End Sub


Private Sub tvTreeView_DblClick()
    Dim nSelected As Node
    Dim nPos As Integer
    Dim sXmlFile As String

    ' skip if the selected node is a root...
    If tvTreeView.SelectedItem.Parent Is Nothing Then
        Exit Sub
    End If
    
    
    ' get the double-clicked item
    If tvTreeView.SelectedItem.Parent.Tag = "UpdateListView" Then
        Set nSelected = tvTreeView.SelectedItem
    Else
        Exit Sub
    End If

    ' open the associated XML file
    nPos = InStr(1, nSelected.Tag, ",")
    sXmlFile = Left(nSelected.Tag, nPos - 1)
    
    Shell "notepad.exe " + sXmlFile, vbNormalFocus
End Sub

Private Sub tvTreeView_KeyUp(KeyCode As Integer, Shift As Integer)
    If KeyCode = vbKeyPageUp Or KeyCode = vbKeyPageDown Or _
       KeyCode = vbKeyUp Or KeyCode = vbKeyDown Then
        SetText SB_INFO, tvTreeView.SelectedItem.FullPath
    End If
End Sub


Private Sub mnuViewRefresh_Click()
    ViewRefresh
End Sub


Private Sub HelpAbout()
    frmAbout.Caption = "About " + g_curTitleBar
    frmAbout.lblAppName = g_curTitleBar
    frmAbout.lblUserName = g_curUser
    frmAbout.Show vbModal
End Sub


Private Sub NewTemplate()
    frmTemplate.Show vbModal
    AdjustToolbar tbToolbar.hwnd
End Sub


Private Sub NewDocument()
    frmDocument.Show vbModal, Me
    AdjustToolbar tbToolbar.hwnd
End Sub


'=======================================================
' UTILITY
'=======================================================

Private Function GetNodeIcon(ByVal sImg As String, _
   ByVal nDefIcon As Integer) As Integer
On Error Resume Next
    Dim nIndex As Integer
    Dim pic As IPictureDisp
    Dim li As ListImage
    
    ' set the default icon as for the node type
    nIndex = nDefIcon
    Set pic = LoadPicture(sImg)
    
    Set li = Nothing
    Set li = ImageList1.ListImages.Item(sImg)
    If li Is Nothing Then
        nIndex = ImageList1.ListImages.Add(, sImg, pic).Index
    Else
        nIndex = li.Index
    End If
  
    GetNodeIcon = nIndex
End Function

Private Sub SetText(ByVal nPanel As Integer, ByVal sText As String)
    sbStatusBar.Panels.Item(nPanel).Text = sText
End Sub


'=======================================================
' RESIZE Stuff
'=======================================================
Private Sub Form_Resize()
On Error Resume Next
    If Me.Width < 4000 Then Me.Width = 4000
        SizeControls imgSplitter.Left
End Sub


Private Sub imgSplitter_MouseDown(Button As Integer, Shift As Integer, x As Single, y As Single)
    With imgSplitter
        picSplitter.Move .Left, .Top, .Width \ 2, .Height - 20
    End With
    picSplitter.Visible = True
    g_mbMoving = True
End Sub


Private Sub imgSplitter_MouseMove(Button As Integer, Shift As Integer, x As Single, y As Single)
    Dim sglPos As Single
    

    If g_mbMoving Then
        sglPos = x + imgSplitter.Left
        If sglPos < SPLITLIMIT Then
            picSplitter.Left = SPLITLIMIT
        ElseIf sglPos > Me.Width - SPLITLIMIT Then
            picSplitter.Left = Me.Width - SPLITLIMIT
        Else
            picSplitter.Left = sglPos
        End If
    End If
End Sub


Private Sub imgSplitter_MouseUp(Button As Integer, Shift As Integer, x As Single, y As Single)
    SizeControls picSplitter.Left
    picSplitter.Visible = False
    g_mbMoving = False
End Sub


Sub SizeControls(x As Single)
    On Error Resume Next
    
    'set the width
    If x < 1500 Then x = 1500
    If x > (Me.Width - 1500) Then x = Me.Width - 1500
    tvTreeView.Width = x
    imgSplitter.Left = x
    lvListView.Left = x + 40
    lvListView.Width = Me.Width - (tvTreeView.Width + 140)
    lblTitle(0).Width = tvTreeView.Width
    lblTitle(1).Left = lvListView.Left + 20
    lblTitle(1).Width = lvListView.Width - 40

    'top and height
    tvTreeView.Top = CoolBar1.Height + picTitles.Height
    lvListView.Top = tvTreeView.Top
    imgSplitter.Top = tvTreeView.Top
    imgSplitter.Height = tvTreeView.Height
    
    ' the listview is 3/10 of the treeview by default.
    lvListView.Height = 3 * tvTreeView.Height / 10

    If sbStatusBar.Visible Then
        tvTreeView.Height = Me.ScaleHeight - (picTitles.Top + picTitles.Height + sbStatusBar.Height)
    Else
        tvTreeView.Height = Me.ScaleHeight - (picTitles.Top + picTitles.Height)
    End If
    
'========
    lvListView.Height = 3 * tvTreeView.Height / 10
'========
    imgSplitter.Top = tvTreeView.Top
    imgSplitter.Height = tvTreeView.Height
    
    imgVSplitter.Left = lvListView.Left
    imgVSplitter.Top = lvListView.Top + lvListView.Height + 1
    imgVSplitter.Width = lvListView.Width
    imgVSplitter.Height = 40
    
    ' Text1 must be changed to WebBrowser1. (by def 7/10 of the treeview)
    wbShow.Top = lvListView.Top + lvListView.Height + 40
    wbShow.Left = lvListView.Left
    wbShow.Width = lvListView.Width - 20
    wbShow.Height = 7 * tvTreeView.Height / 10 - 20
End Sub


Private Sub TreeView1_DragDrop(Source As Control, x As Single, y As Single)
    If Source = imgSplitter Then
        SizeControls x
    End If
End Sub


Private Sub tbToolBar_ButtonClick(ByVal Button As ComctlLib.Button)
    Select Case Button.Key
        Case "Open"
            FileOpen
        Case "New document"
            NewDocument
        Case "New template"
            NewTemplate
        Case "Refresh"
            ViewRefresh
        Case "Edit"
            EditTemplate
    End Select
End Sub


Private Sub ToggleHtmlView(ByVal fDetailedView As Boolean)
    
If fDetailedView Then
    wbShow.Document.All("content").Style.display = ""
    wbShow.Document.All("empty").Style.display = "none"
Else
    wbShow.Document.All("empty").Style.display = ""
    wbShow.Document.All("content").Style.display = "none"
End If

End Sub


Private Sub setPage(ByVal li As ListItem, ByVal xmlItem As IXMLElement, ByVal bClear As Boolean)
Dim doc As HTMLDocument
Dim i As Integer
Dim sTag As String

If bClear Then
        wbShow.Document.All("what").innerText = li.Text
        wbShow.Document.All("who").innerText = li.SubItems(LI_WHO)

         Set doc = wbShow.Document
         For i = 0 To xmlItem.Children.length - 1
            sTag = xmlItem.Children.Item(i).tagName

            If LCase(sTag) = "logo" Then
                doc.All(sTag).src = ""
            Else
                doc.All(sTag).innerText = ""
            End If
         Next
Else
        wbShow.Document.All("what").innerText = li.Text
        wbShow.Document.All("who").innerText = li.SubItems(LI_WHO)

         Set doc = wbShow.Document
         For i = 0 To xmlItem.Children.length - 1
            sTag = xmlItem.Children.Item(i).tagName

            If LCase(sTag) = "logo" Then
                doc.All(sTag).src = xmlItem.Children.Item(i).Text
            Else
                doc.All(sTag).innerText = xmlItem.Children.Item(i).Text
            End If
         Next
End If
End Sub

