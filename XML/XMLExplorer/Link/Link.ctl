VERSION 5.00
Begin VB.UserControl Link 
   AutoRedraw      =   -1  'True
   ClientHeight    =   3600
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   4800
   BeginProperty Font 
      Name            =   "MS Sans Serif"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   -1  'True
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   ForeColor       =   &H00FF0000&
   MouseIcon       =   "Link.ctx":0000
   MousePointer    =   99  'Custom
   ScaleHeight     =   3600
   ScaleWidth      =   4800
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      Caption         =   "Link"
      ForeColor       =   &H00FF0000&
      Height          =   195
      Left            =   600
      TabIndex        =   0
      Top             =   600
      Width           =   300
   End
End
Attribute VB_Name = "Link"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
'Default Property Values:
Const m_def_Link = "http://www.microsoft.com/mind"
'Const m_def_Link = "http://expoware"
'Property Variables:
Dim m_Link As String
'Dim m_Link As String


Private Sub Label1_Click()
UserControl.HyperLink.NavigateTo Link
Label1.ForeColor = &H800080
End Sub

Private Sub UserControl_Resize()
    Label1.Move 0, 0
    UserControl.Width = Label1.Width
    UserControl.Height = Label1.Height
End Sub
'WARNING! DO NOT REMOVE OR MODIFY THE FOLLOWING COMMENTED LINES!
'MappingInfo=Label1,Label1,-1,Caption
Public Property Get Caption() As String
    Caption = Label1.Caption
End Property

Public Property Let Caption(ByVal New_Caption As String)
    Label1.Caption() = New_Caption
    PropertyChanged "Caption"
    UserControl_Resize
End Property
'
'Public Property Get Link() As String
'    Link = m_Link
'End Property
'
'Public Property Let Link(ByVal New_Link As String)
'    m_Link = New_Link
'    PropertyChanged "Link"
'End Property

'Initialize Properties for User Control
Private Sub UserControl_InitProperties()
'    m_Link = m_def_Link
    m_Link = m_def_Link
End Sub

'Load property values from storage
Private Sub UserControl_ReadProperties(PropBag As PropertyBag)

    Label1.Caption = PropBag.ReadProperty("Caption", "http://expoware")
'    m_Link = PropBag.ReadProperty("Link", m_def_Link)
    m_Link = PropBag.ReadProperty("Link", m_def_Link)
End Sub

'Write property values to storage
Private Sub UserControl_WriteProperties(PropBag As PropertyBag)

    Call PropBag.WriteProperty("Caption", Label1.Caption, "http://expoware")
'    Call PropBag.WriteProperty("Link", m_Link, m_def_Link)
    Call PropBag.WriteProperty("Link", m_Link, m_def_Link)
End Sub


Public Property Get Link() As String
    Link = m_Link
End Property

Public Property Let Link(ByVal New_Link As String)
    m_Link = New_Link
    PropertyChanged "Link"
End Property

