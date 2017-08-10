// InjectAPI.h: interface for the Inject API.
//
//////////////////////////////////////////////////////////////////////

#ifndef __INJECTAPI_H__
#define __INJECTAPI_H__

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#define INJECT_UNLOAD	0x01

#define IE_EXECUTE_FAILED	0x01
#define IE_ALLOCATE_FAILED	0x02
#define IE_ACCESS_FAILED	0x03
#define IE_LOAD_LIB_FAILED	0x04
#define IE_SYSAPI_FAILED	0x05
#define IE_UNLOAD_LIB_FAILED	0x06
#define IE_INVALID_PARAMETER	0x07
#define IE_UNEXCEPTION		0xffffffff

/*WinNT and Win9x*/
DWORD WINAPI InjectDLL(LPCTSTR lpszEXEName, LPTSTR lpszCmdLine, LPCTSTR lpszDLLName, DWORD dwFlags); 

/*WinNT only*/
DWORD WINAPI UnloadDLL(DWORD dwProcessID, LPCTSTR lpszDLLName, DWORD dwFlags);
DWORD WINAPI UnloadDLL2(HANDLE hProcess, LPCTSTR lpszDLLName, DWORD dwFlags);

/*WinNT only*/
DWORD WINAPI AttachDLL(DWORD dwProcessID, LPCTSTR lpszDLLName, DWORD dwFlags); 
DWORD WINAPI AttachDLL2(HANDLE hProcess, LPCTSTR lpszDLLName, DWORD dwFlags); 

#endif // __INJECTAPI_H__
