; CLW file contains information for the MFC ClassWizard

[General Info]
Version=1
LastClass=CMyEditView
LastTemplate=CDialog
NewFileInclude1=#include "stdafx.h"
NewFileInclude2=#include "Edit.h"
LastPage=0

ClassCount=5
Class1=CEditApp
Class2=CEditDoc
Class3=CMyEditView
Class4=CMainFrame

ResourceCount=3
Resource1=IDR_MAINFRAME
Resource2=IDR_CNTR_INPLACE
Class5=CAboutDlg
Resource3=IDD_ABOUTBOX

[CLS:CEditApp]
Type=0
HeaderFile=Edit.h
ImplementationFile=Edit.cpp
Filter=N

[CLS:CEditDoc]
Type=0
HeaderFile=EditDoc.h
ImplementationFile=EditDoc.cpp
Filter=N

[CLS:CMyEditView]
Type=0
HeaderFile=EditView.h
ImplementationFile=EditView.cpp
Filter=C
BaseClass=CRichEditView
VirtualFilter=VWC
LastObject=CMyEditView


[CLS:CMainFrame]
Type=0
HeaderFile=MainFrm.h
ImplementationFile=MainFrm.cpp
Filter=T
BaseClass=CFrameWnd
VirtualFilter=fWC
LastObject=CMainFrame




[CLS:CAboutDlg]
Type=0
HeaderFile=Edit.cpp
ImplementationFile=Edit.cpp
Filter=D

[DLG:IDD_ABOUTBOX]
Type=1
Class=CAboutDlg
ControlCount=4
Control1=IDC_STATIC,static,1342177283
Control2=IDC_STATIC,static,1342308480
Control3=IDC_STATIC,static,1342308352
Control4=IDOK,button,1342373889

[MNU:IDR_MAINFRAME]
Type=1
Class=CMainFrame
Command1=ID_FILE_NEW
Command2=ID_FILE_OPEN
Command3=ID_FILE_SAVE
Command4=ID_FILE_SAVE_AS
Command5=ID_FILE_PRINT
Command6=ID_FILE_PRINT_PREVIEW
Command7=ID_FILE_PRINT_SETUP
Command8=ID_FILE_MRU_FILE1
Command9=ID_APP_EXIT
Command10=ID_EDIT_UNDO
Command11=ID_EDIT_CUT
Command12=ID_EDIT_COPY
Command13=ID_EDIT_PASTE
Command14=ID_EDIT_PASTE_SPECIAL
Command15=ID_EDIT_SELECT_ALL
Command16=ID_EDIT_FIND
Command17=ID_EDIT_REPEAT
Command18=ID_EDIT_REPLACE
Command19=ID_OLE_INSERT_NEW
Command20=ID_OLE_EDIT_LINKS
Command21=ID_OLE_EDIT_PROPERTIES
Command22=ID_OLE_VERB_FIRST
Command23=ID_VIEW_TOOLBAR
Command24=ID_VIEW_STATUS_BAR
Command25=ID_APP_ABOUT
CommandCount=25

[MNU:IDR_CNTR_INPLACE]
Type=1
Class=CMyEditView
Command1=ID_FILE_NEW
Command2=ID_FILE_OPEN
Command3=ID_FILE_SAVE
Command4=ID_FILE_SAVE_AS
Command5=ID_FILE_PRINT
Command6=ID_FILE_PRINT_PREVIEW
Command7=ID_FILE_PRINT_SETUP
Command8=ID_FILE_MRU_FILE1
Command9=ID_APP_EXIT
CommandCount=9

[ACL:IDR_MAINFRAME]
Type=1
Class=CMainFrame
Command1=ID_FILE_NEW
Command2=ID_FILE_OPEN
Command3=ID_FILE_SAVE
Command4=ID_FILE_PRINT
Command5=ID_EDIT_UNDO
Command6=ID_EDIT_CUT
Command7=ID_EDIT_COPY
Command8=ID_EDIT_PASTE
Command9=ID_EDIT_SELECT_ALL
Command10=ID_EDIT_FIND
Command11=ID_EDIT_REPEAT
Command12=ID_EDIT_REPLACE
Command13=ID_OLE_EDIT_PROPERTIES
Command14=ID_EDIT_UNDO
Command15=ID_EDIT_CUT
Command16=ID_EDIT_COPY
Command17=ID_EDIT_PASTE
Command18=ID_NEXT_PANE
Command19=ID_PREV_PANE
Command20=ID_CANCEL_EDIT_CNTR
CommandCount=20

[ACL:IDR_CNTR_INPLACE]
Type=1
Class=CMyEditView
Command1=ID_FILE_NEW
Command2=ID_FILE_OPEN
Command3=ID_FILE_SAVE
Command4=ID_FILE_PRINT
Command5=ID_NEXT_PANE
Command6=ID_PREV_PANE
Command7=ID_CANCEL_EDIT_CNTR
CommandCount=7

[TB:IDR_MAINFRAME]
Type=1
Class=CMainFrame
Command1=ID_BUTTON32774
Command2=ID_BUTTON32775
Command3=ID_BUTTON32776
Command4=ID_BUTTON32777
Command5=ID_BUTTON32778
Command6=ID_BUTTON32779
Command7=ID_BUTTON32780
Command8=ID_BUTTON32781
Command9=ID_BUTTON32782
Command10=ID_BUTTON32783
Command11=ID_BUTTON32784
Command12=ID_BUTTON32785
Command13=ID_BUTTON32786
Command14=ID_BUTTON32787
Command15=ID_BUTTON32788
CommandCount=15

