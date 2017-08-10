VERSION 5.00
Object = "{6B7E6392-850A-101B-AFC0-4210102A8DA7}#1.2#0"; "COMCTL32.OCX"
Begin VB.Form frmDocument 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "New Document..."
   ClientHeight    =   4920
   ClientLeft      =   2565
   ClientTop       =   1500
   ClientWidth     =   6150
   Icon            =   "frmDocument.frx":0000
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4920
   ScaleWidth      =   6150
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.PictureBox picOptions 
      BorderStyle     =   0  'None
      Height          =   3780
      Index           =   3
      Left            =   -20000
      ScaleHeight     =   3780
      ScaleWidth      =   5685
      TabIndex        =   7
      TabStop         =   0   'False
      Top             =   480
      Width           =   5685
      Begin VB.Frame fraSample4 
         Caption         =   "Sample 4"
         Height          =   1785
         Left            =   2100
         TabIndex        =   10
         Top             =   840
         Width           =   2055
      End
   End
   Begin VB.PictureBox picOptions 
      BorderStyle     =   0  'None
      Height          =   3780
      Index           =   2
      Left            =   -20000
      ScaleHeight     =   3780
      ScaleWidth      =   5685
      TabIndex        =   6
      TabStop         =   0   'False
      Top             =   480
      Width           =   5685
      Begin VB.Frame fraSample3 
         Caption         =   "Sample 3"
         Height          =   1785
         Left            =   1545
         TabIndex        =   9
         Top             =   675
         Width           =   2055
      End
   End
   Begin VB.PictureBox picOptions 
      BorderStyle     =   0  'None
      Height          =   3780
      Index           =   1
      Left            =   -20000
      ScaleHeight     =   3780
      ScaleWidth      =   5685
      TabIndex        =   5
      TabStop         =   0   'False
      Top             =   480
      Width           =   5685
      Begin VB.Frame fraSample2 
         Caption         =   "Sample 2"
         Height          =   1785
         Left            =   645
         TabIndex        =   8
         Top             =   300
         Width           =   2055
      End
   End
   Begin VB.PictureBox picOptions 
      BorderStyle     =   0  'None
      Height          =   3780
      Index           =   0
      Left            =   210
      ScaleHeight     =   3780
      ScaleWidth      =   5685
      TabIndex        =   4
      TabStop         =   0   'False
      Top             =   480
      Width           =   5685
      Begin VB.TextBox txtName 
         Height          =   285
         Left            =   3120
         TabIndex        =   12
         Top             =   360
         Width           =   1095
      End
      Begin VB.TextBox txtDocument 
         Height          =   2895
         Left            =   120
         MultiLine       =   -1  'True
         ScrollBars      =   3  'Both
         TabIndex        =   11
         Text            =   "frmDocument.frx":000C
         Top             =   720
         Width           =   5415
      End
      Begin VB.Label lblPath 
         Height          =   255
         Left            =   840
         TabIndex        =   15
         Top             =   360
         Width           =   2175
      End
      Begin VB.Label Label2 
         Caption         =   ".xml"
         Height          =   255
         Left            =   4320
         TabIndex        =   14
         Top             =   360
         Width           =   735
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         Caption         =   "Name:"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   0
         TabIndex        =   13
         Top             =   360
         Width           =   615
      End
   End
   Begin VB.CommandButton cmdApply 
      Caption         =   "Apply"
      Height          =   375
      Left            =   4920
      TabIndex        =   3
      Top             =   4455
      Width           =   1095
   End
   Begin VB.CommandButton cmdCancel 
      Cancel          =   -1  'True
      Caption         =   "Cancel"
      Height          =   375
      Left            =   3720
      TabIndex        =   2
      Top             =   4455
      Width           =   1095
   End
   Begin VB.CommandButton cmdOK 
      Caption         =   "OK"
      Height          =   375
      Left            =   2490
      TabIndex        =   1
      Top             =   4455
      Width           =   1095
   End
   Begin ComctlLib.TabStrip tbsOptions 
      Height          =   4245
      Left            =   105
      TabIndex        =   0
      Top             =   120
      Width           =   5895
      _ExtentX        =   10398
      _ExtentY        =   7488
      _Version        =   327682
      BeginProperty Tabs {0713E432-850A-101B-AFC0-4210102A8DA7} 
         NumTabs         =   1
         BeginProperty Tab1 {0713F341-850A-101B-AFC0-4210102A8DA7} 
            Caption         =   "General"
            Key             =   ""
            Object.Tag             =   ""
            Object.ToolTipText     =   "Write XML documents"
            ImageVarType    =   2
         EndProperty
      EndProperty
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
   End
End
Attribute VB_Name = "frmDocument"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private g_bAlreadyDone As Boolean

Private Sub cmdApply_Click()
    SaveDocument
End Sub

Private Sub cmdCancel_Click()
    g_bAlreadyDone = False
    Unload Me
End Sub

Private Sub cmdOK_Click()
    If SaveDocument = True Then
        Unload Me
    End If
End Sub

Private Sub Form_Load()
    Me.Move (Screen.Width - Me.Width) / 2, (Screen.Height - Me.Height) / 2
    lblPath = g_mainPath
End Sub

Private Sub tbsOptions_Click()
    Dim i As Integer
    'show and enable the selected tab's controls
    'and hide and disable all others
    For i = 0 To tbsOptions.Tabs.Count - 1
        If i = tbsOptions.SelectedItem.Index - 1 Then
            picOptions(i).Left = 210
            picOptions(i).Enabled = True
        Else
            picOptions(i).Left = -20000
            picOptions(i).Enabled = False
        End If
    Next
    
End Sub

 
Private Function SaveDocument() As Boolean
    Dim sDocument As String
    Dim y As Long
    
    SaveDocument = True
    sDocument = g_mainPath + txtName.Text + ".xml"

On Error GoTo NotFound
    If Len(txtName.Text) > 0 Then
        Open sDocument For Input As #1
        Close #1
    Else
        Beep
        SaveDocument = False
        Exit Function
    End If
    
    If Not g_bAlreadyDone Then
        y = MsgBox("The file already exists. Overwrite?", vbYesNo, "New document...")
        g_bAlreadyDone = True
        If y = vbNo Then
            Exit Function
        End If
    End If

NotFound:
    
    ' save the new document
    Open sDocument For Output As #1
    Print #1, txtDocument.Text
    Close #1
End Function



Private Function GetFileFromPath(ByVal pathName As String, ByVal fExt As Boolean) As String
    Dim sTemp As String
    Dim bExit As Boolean
    Dim i, iStartPos As Integer
        
    iStartPos = 0
    sTemp = pathName
    bExit = False
    
    While Not bExit
        i = InStr(iStartPos + 1, sTemp, "\")
        If i = 0 Then
            GetFileFromPath = Right$(sTemp, Len(sTemp) - iStartPos)
            bExit = True
        Else
            iStartPos = i
        End If
    Wend
    
    If Not fExt Then
        i = InStr(1, GetFileFromPath, ".")
        GetFileFromPath = Left$(GetFileFromPath, i - 1)
    End If
End Function
