// QQQ.h : main header file for the QQQ application
//
#pragma once

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"       // main symbols


// CQQQApp:
// See QQQ.cpp for the implementation of this class
//

class CQQQApp : public CWinApp
{
public:
	CQQQApp();


// Overrides
public:
	virtual BOOL InitInstance();

// Implementation
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CQQQApp theApp;