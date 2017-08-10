// stdafx.h : include file for standard system include files,
//  or project specific include files that are used frequently, but
//      are changed infrequently
//

#if !defined(AFX_STDAFX_H__74228332_69EF_11D5_8895_000000000000__INCLUDED_)
#define AFX_STDAFX_H__74228332_69EF_11D5_8895_000000000000__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#define WIN32_LEAN_AND_MEAN		// Exclude rarely-used stuff from Windows headers

#define STRICT
#include <Windows.h>
#include <imagehlp.h>
#include <tchar.h>

#pragma warning(disable: 4786)
#include <string>
using namespace std;
typedef basic_string<TCHAR> _tstring;

#include "InjectAPI.h"

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_STDAFX_H__74228332_69EF_11D5_8895_000000000000__INCLUDED_)
