// stdafx.cpp : source file that includes just the standard includes
//  stdafx.pch will be the pre-compiled header
//  stdafx.obj will contain the pre-compiled type information

#include "stdafx.h"

#ifdef _ATL_STATIC_REGISTRY
#include <statreg.h>
#include <statreg.cpp>
#endif

#include <atlimpl.cpp>
void PopMsg(LPCTSTR pszFormat, ...) 
{
   va_list argList;
   va_start(argList, pszFormat);

   TCHAR sz[1024];
//#ifdef _UNICODE
//   vswprintf(sz, pszFormat, argList);
//#else
//   vsprintf(sz, pszFormat, argList);
//#endif
   wvsprintf(sz, pszFormat, argList);
   va_end(argList);
   ::MessageBox(NULL, sz, _T("Pop Msg"), MB_OK);
}

void ReportErr(LPCTSTR str)
{
	    LPVOID lpMsgBuf;
        FormatMessage( 
           FORMAT_MESSAGE_ALLOCATE_BUFFER | 
           FORMAT_MESSAGE_FROM_SYSTEM | 
           FORMAT_MESSAGE_IGNORE_INSERTS,
           NULL,
           ::GetLastError(),
           MAKELANGID(LANG_ENGLISH, SUBLANG_ENGLISH_US), // Default language
           (LPTSTR) &lpMsgBuf,
           0,
           NULL 
       );
		::MessageBox( NULL, (LPCTSTR)lpMsgBuf, 
		   str, MB_OK | MB_ICONINFORMATION );
       // Free the buffer.
       LocalFree( lpMsgBuf );
}

void ReportErrEx(LPCTSTR pszFormat, ...) 
{
   va_list argList;
   va_start(argList, pszFormat);

   TCHAR sz[1024];
   wvsprintf(sz, pszFormat, argList);
   va_end(argList);
   
   LPVOID lpMsgBuf;
   FormatMessage( 
         FORMAT_MESSAGE_ALLOCATE_BUFFER | 
         FORMAT_MESSAGE_FROM_SYSTEM | 
         FORMAT_MESSAGE_IGNORE_INSERTS,
         NULL,
         ::GetLastError(),
         MAKELANGID(LANG_ENGLISH, SUBLANG_ENGLISH_US), // Default language
         (LPTSTR) &lpMsgBuf,
         0,
         NULL 
    );
	::MessageBox( NULL, (LPCTSTR)lpMsgBuf, 
		   sz, MB_OK | MB_ICONINFORMATION );
    // Free the buffer.
    LocalFree( lpMsgBuf );
}