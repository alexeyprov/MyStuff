Attribute VB_Name = "Defs"
Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Long, ByVal hWnd2 As Long, ByVal lpsz1 As String, ByVal lpsz2 As String) As Long
Public Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long) As Long
Public Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long, ByVal dwNewLong As Long) As Long
Public Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Long, ByVal hWndInsertAfter As Long, ByVal x As Long, ByVal y As Long, ByVal cx As Long, ByVal cy As Long, ByVal wFlags As Long) As Long
Public Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long
Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long

'--------
Public Const SWP_DRAWFRAME = &H20
Public Const SWP_NOMOVE = &H2
Public Const SWP_NOSIZE = &H1
'--------

Public Declare Function SendMessageString Lib "user32" _
   Alias "SendMessageA" ( _
   ByVal hwnd As Long, ByVal wMsg As Long, _
   ByVal wParam As Long, ByVal lParam As String _
) As Long

Public Const GWL_STYLE = (-16)
Public Const CB_FINDSTRINGEXACT = &H158
Public Const TBSTYLE_COOL = &H8800
Public Const STDBASEPATH = "c:\xml\templates\"

Public fMainForm As frmMain
Public g_BasePath As String
Public g_mainPath As String


Public Sub AdjustToolbar(ByVal hwndParent As Long)
    hwndTB = FindWindowEx(hwndParent, 0, "ToolbarWindow32", vbNullString)
    dwStyle = GetWindowLong(hwndTB, GWL_STYLE)
    SetWindowLong hwndTB, GWL_STYLE, dwStyle + TBSTYLE_COOL
    'SetWindowPos hwndTB, 0, 0, 0, 0, 0, SWP_NOSIZE + SWP_NOMOVE + SWP_DRAWFRAME
End Sub


Sub Main()
    Set fMainForm = New frmMain
    fMainForm.Show
End Sub





