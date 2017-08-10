VERSION 5.00
Begin VB.Form frmPath 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Template path"
   ClientHeight    =   3030
   ClientLeft      =   3435
   ClientTop       =   2355
   ClientWidth     =   4680
   Icon            =   "frmPath.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3030
   ScaleWidth      =   4680
   Begin VB.CommandButton cmdOK 
      Caption         =   "OK"
      Height          =   375
      Left            =   3120
      TabIndex        =   4
      Top             =   2520
      Width           =   1455
   End
   Begin VB.Frame Frame1 
      Caption         =   "Base path"
      Height          =   1215
      Left            =   120
      TabIndex        =   1
      Top             =   1200
      Width           =   4455
      Begin VB.TextBox txtBasePath 
         Height          =   285
         Left            =   240
         TabIndex        =   2
         Top             =   720
         Width           =   3975
      End
      Begin VB.Label Label2 
         Caption         =   "Enter the path:"
         Height          =   255
         Left            =   240
         TabIndex        =   3
         Top             =   480
         Width           =   2415
      End
   End
   Begin VB.Label Label1 
      Caption         =   $"frmPath.frx":000C
      Height          =   855
      Left            =   120
      TabIndex        =   0
      Top             =   240
      Width           =   4455
   End
End
Attribute VB_Name = "frmPath"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub cmdOK_Click()
    If Len(txtBasePath.Text) Then
        g_BasePath = txtBasePath.Text
        If Right(g_BasePath, 1) <> "\" Then
            g_BasePath = g_BasePath + "\"
        End If
    Else
        Beep
        txtBasePath.SetFocus
    End If
    
    Unload Me
End Sub

