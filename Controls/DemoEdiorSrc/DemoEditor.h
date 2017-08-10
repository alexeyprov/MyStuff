// DemoEditor.h : main header file for the DEMOEDITOR application
//

#if !defined(AFX_DEMOEDITOR_H__AFF97147_D62E_4FFD_88C6_1D5A410932F7__INCLUDED_)
#define AFX_DEMOEDITOR_H__AFF97147_D62E_4FFD_88C6_1D5A410932F7__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"       // main symbols

/////////////////////////////////////////////////////////////////////////////
// CDemoEditorApp:
// See DemoEditor.cpp for the implementation of this class
//

class CDemoEditorApp : public CWinApp
{
public:
	CDemoEditorApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CDemoEditorApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation
	//{{AFX_MSG(CDemoEditorApp)
	afx_msg void OnAppAbout();
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_DEMOEDITOR_H__AFF97147_D62E_4FFD_88C6_1D5A410932F7__INCLUDED_)
