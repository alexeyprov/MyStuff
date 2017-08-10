// Edit.h : main header file for the EDIT application
//

#if !defined(AFX_EDIT_H__3FB90911_7105_47D6_AF55_D051E3906CB8__INCLUDED_)
#define AFX_EDIT_H__3FB90911_7105_47D6_AF55_D051E3906CB8__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"       // main symbols

/////////////////////////////////////////////////////////////////////////////
// CEditApp:
// See Edit.cpp for the implementation of this class
//

class CEditApp : public CWinApp
{
public:
	CEditApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CEditApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation
	//{{AFX_MSG(CEditApp)
	afx_msg void OnAppAbout();
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_EDIT_H__3FB90911_7105_47D6_AF55_D051E3906CB8__INCLUDED_)
