VERSION 5.00
Object = "{BAFAF7D3-FBD1-11D1-BC00-406805C10E27}#1.0#0"; "Link.ocx"
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   3195
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   ScaleHeight     =   3195
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows Default
   Begin HyperLink.Link Link1 
      Height          =   195
      Left            =   2040
      TabIndex        =   0
      Top             =   240
      Width           =   735
      _ExtentX        =   1296
      _ExtentY        =   344
      Caption         =   "Link to me"
   End
   Begin VB.Label Label2 
      AutoSize        =   -1  'True
      Caption         =   "for a great resilt"
      Height          =   195
      Left            =   3000
      TabIndex        =   2
      Top             =   240
      Width           =   1080
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      Caption         =   "I'm the best in the world"
      Height          =   195
      Left            =   360
      TabIndex        =   1
      Top             =   240
      Width           =   1665
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Form_Load()
Link1.Link = "http://expoware"
End Sub
