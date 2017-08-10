VERSION 5.00
Object = "*\AxmlView.vbp"
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.1#0"; "COMDLG32.OCX"
Begin VB.Form Form1 
   Caption         =   "XML tree-Viewer"
   ClientHeight    =   3525
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   ScaleHeight     =   3525
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows Default
   Begin MSComDlg.CommonDialog cdOpen 
      Left            =   720
      Top             =   3000
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   327681
      CancelError     =   -1  'True
      DialogTitle     =   "Open an XML file"
      InitDir         =   "c:\"
   End
   Begin VB.CommandButton cmdLoad 
      Caption         =   "Load XML"
      Height          =   375
      Left            =   2640
      TabIndex        =   1
      Top             =   3000
      Width           =   1575
   End
   Begin xmlView.XmlViewer XmlViewer1 
      Height          =   2415
      Left            =   360
      TabIndex        =   0
      Top             =   240
      Width           =   3735
      _extentx        =   6588
      _extenty        =   4260
      font            =   "Form1.frx":0000
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Const OFFSET = 200

Private Sub cmdLoad_Click()
On Error GoTo Cancel
    cdOpen.ShowOpen
    XmlViewer1.URL = cdOpen.filename
    Exit Sub

Cancel:
End Sub

Private Sub Form_Resize()
    XmlViewer1.Move OFFSET, OFFSET, _
        Form1.Width - 2 * OFFSET, Form1.Height - 5 * OFFSET - cmdLoad.Height
    cmdLoad.Top = XmlViewer1.Top + XmlViewer1.Height + OFFSET
    cmdLoad.Left = OFFSET
End Sub
