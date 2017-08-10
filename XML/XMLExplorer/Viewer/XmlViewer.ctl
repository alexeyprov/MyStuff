VERSION 5.00
Object = "{6B7E6392-850A-101B-AFC0-4210102A8DA7}#1.2#0"; "COMCTL32.OCX"
Begin VB.UserControl XmlViewer 
   ClientHeight    =   3600
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   4800
   ScaleHeight     =   3600
   ScaleWidth      =   4800
   Begin ComctlLib.TreeView tvTreeView 
      Height          =   2055
      Left            =   840
      TabIndex        =   0
      Top             =   600
      Width           =   3375
      _ExtentX        =   5953
      _ExtentY        =   3625
      _Version        =   327682
      HideSelection   =   0   'False
      Indentation     =   531
      LabelEdit       =   1
      LineStyle       =   1
      Style           =   7
      Appearance      =   1
   End
End
Attribute VB_Name = "XmlViewer"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
'Global Variables:
Private g_xmlDoc As XMLDocument

'Default Property Values:
Const m_def_URL = ""

'Property Variables:
Dim m_URL As String

'Event Declarations:
'Event Collapse(Node As Node) 'MappingInfo=tvTreeView,tvTreeView,-1,Collapse
'Event Expand(Node As Node) 'MappingInfo=tvTreeView,tvTreeView,-1,Expand


Private Sub UserControl_Resize()
    tvTreeView.Move 0, 0, UserControl.Width, UserControl.Height
End Sub

'WARNING! DO NOT REMOVE OR MODIFY THE FOLLOWING COMMENTED LINES!
'MappingInfo=tvTreeView,tvTreeView,-1,Font
Public Property Get Font() As Font
Attribute Font.VB_Description = "Returns a Font object."
Attribute Font.VB_UserMemId = -512
    Set Font = tvTreeView.Font
End Property

Public Property Set Font(ByVal New_Font As Font)
    Set tvTreeView.Font = New_Font
    PropertyChanged "Font"
End Property

'WARNING! DO NOT REMOVE OR MODIFY THE FOLLOWING COMMENTED LINES!
'MappingInfo=tvTreeView,tvTreeView,-1,ImageList
Public Property Get ImageList() As Object
Attribute ImageList.VB_Description = "Returns/sets the ImageList control to be used."
    Set ImageList = tvTreeView.ImageList
End Property

Public Property Set ImageList(ByVal New_ImageList As Object)
    Set tvTreeView.ImageList = New_ImageList
    PropertyChanged "ImageList"
End Property

'WARNING! DO NOT REMOVE OR MODIFY THE FOLLOWING COMMENTED LINES!
'MappingInfo=tvTreeView,tvTreeView,-1,SelectedItem
Public Property Get SelectedItem() As INode
Attribute SelectedItem.VB_Description = "Returns/sets a value which determines if a ListItem or Node object is selected."
    Set SelectedItem = tvTreeView.SelectedItem
End Property

Public Property Set SelectedItem(ByVal New_SelectedItem As INode)
    Set tvTreeView.SelectedItem = New_SelectedItem
    PropertyChanged "SelectedItem"
End Property

Public Property Get URL() As String
Attribute URL.VB_Description = "Sets and reads the name of the XML file to be viewed."
    URL = m_URL
End Property

Public Property Let URL(ByVal New_URL As String)
    m_URL = New_URL
    PropertyChanged "URL"
    
    ' load the XML file using the MSXML control
    tvTreeView.Nodes.Clear
    LoadXMLFile New_URL
End Property

'Initialize Properties for User Control
Private Sub UserControl_InitProperties()
    m_URL = m_def_URL
End Sub

'Load property values from storage
Private Sub UserControl_ReadProperties(PropBag As PropertyBag)

    Set Font = PropBag.ReadProperty("Font", Ambient.Font)
    Set ImageList = PropBag.ReadProperty("ImageList", Nothing)
    Set SelectedItem = PropBag.ReadProperty("SelectedItem", Nothing)
    m_URL = PropBag.ReadProperty("URL", m_def_URL)
End Sub

'Write property values to storage
Private Sub UserControl_WriteProperties(PropBag As PropertyBag)

    Call PropBag.WriteProperty("Font", Font, Ambient.Font)
    Call PropBag.WriteProperty("ImageList", ImageList, Nothing)
    Call PropBag.WriteProperty("SelectedItem", SelectedItem, Nothing)
    Call PropBag.WriteProperty("URL", m_URL, m_def_URL)
End Sub


'----------------------------------------------------------
' Loading the XML file...
'----------------------------------------------------------
Private Sub LoadXMLFile(ByVal sFile As String)
On Error GoTo ErrHandler
    Dim nRoot As Node
    
    Set g_xmlDoc = Nothing
    
    Set g_xmlDoc = New XMLDocument
    g_xmlDoc.URL = sFile
    
    ' add the root node and scan the rest of the document
    Dim s As String
    s = g_xmlDoc.root.tagName + "  (" + sFile + ")"
    Set nRoot = tvTreeView.Nodes.Add(, , , s)
    ScanDocument nRoot, g_xmlDoc.root.children
    Exit Sub
    
ErrHandler:
    MsgBox "An error occurred while reading the file:" + _
           vbCrLf + sFile + vbCrLf + _
           "Check the syntax of the file.", , "XML Tree-Viewer"
End Sub


Private Sub ScanDocument(ByVal nParent As Node, ByVal xml As IXMLElementCollection)
On Error GoTo NextItem

    Dim xmlItem As IXMLElement
    Dim n As Node
    Dim s As String
    
    If xml Is Nothing Then
    Exit Sub
    End If
    For Each xmlItem In xml
        s = xmlItem.tagName
        If xmlItem.children Is Nothing Then
            If Len(xmlItem.Text) Then
                s = s + "  (" + xmlItem.Text + ")"
            End If
        ElseIf xmlItem.children.length <= 1 Then
            If Len(xmlItem.Text) Then
                s = s + "  (" + xmlItem.Text + ")"
            End If
        Else
            s = s + "  (" + CStr(xmlItem.children.length) + ")" + x
        End If
        
        Set n = AddTreeItem(nParent, s)
        ScanDocument n, xmlItem.children
NextItem:
    Next
End Sub
    

Private Function AddTreeItem(ByVal nParent As Node, ByVal sText As String) As Node
    Dim n As Node
    
    Set n = tvTreeView.Nodes.Add(nParent, tvwChild, , sText)
    Set AddTreeItem = n
End Function


