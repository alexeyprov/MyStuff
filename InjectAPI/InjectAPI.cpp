// InjectAPI.cpp: implementation the Inject API.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"

extern "C" DWORD GetOutAddress();
extern "C" DWORD GetProcLen();

extern "C" DWORD GetOutAddress2();
extern "C" DWORD GetProcLen2();

extern "C" DWORD GetOutAddress3();
extern "C" DWORD GetProcLen3();

#define PUSH_LEN			5
#define PUSH_OPER_LEN		4
#define JMP_LEN				5
#define JMP_OPER_LEN		4
#define FIRST_BYTES_LEN		PUSH_LEN + JMP_LEN

#define INJDATA_LEN			4 * sizeof(DWORD) + (MAX_PATH + 1) * sizeof(TCHAR)

#define	SYSCALL(f, ec)		if (!(BOOL)f) {__SysError = GetLastError();__Error = IE_SYSAPI_FAILED; goto InjectDLLError;} 

const BYTE jmp = 0xE9;
const BYTE push = 0x68;

const TCHAR szKernel32[] = _T("KERNEL32.DLL");
const TCHAR szLoadLibraryA[] = _T("LoadLibraryA");
const TCHAR szLoadLibraryW[] = _T("LoadLibraryW");
const TCHAR szGetModuleHandleA[] = _T("GetModuleHandleA");
const TCHAR szGetModuleHandleW[] = _T("GetModuleHandleW");
const TCHAR szFreeLibrary[] = _T("FreeLibrary");

//////////////////////////////////////////////////////////////////////

class CInternalInject
{
public:
	_tstring GetFullFileName(LPCTSTR lpszEXEName)
	{
		TCHAR szBuf[MAX_PATH + 1] = _T(""), *sz;
		SearchPath(NULL, lpszEXEName, _T(".exe"), MAX_PATH, szBuf, &sz);
		return szBuf;
	}

	DWORD GetEntryPoint(LPCTSTR lpszFileName)
	{
		HANDLE hFile = CreateFile(lpszFileName, GENERIC_READ, FILE_SHARE_WRITE|FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
		if(INVALID_HANDLE_VALUE == hFile)
		{
			return 0;
		}

		HANDLE hMapFile = CreateFileMapping(hFile, NULL, PAGE_READONLY, 0, 0, NULL);
		CloseHandle(hFile);
		if(!hMapFile)
		{
			return 0;
		}

		LPVOID lpFile = (LPVOID)MapViewOfFile(hMapFile, FILE_MAP_READ, 0, 0, 0);
		CloseHandle(hMapFile);
		
		PIMAGE_OPTIONAL_HEADER poh;
		int ImageHdrOffset = 0;
		DWORD dwEntryPoint = 0;

		if (*((USHORT*)lpFile) == IMAGE_DOS_SIGNATURE)
		{
			ImageHdrOffset = (int)((ULONG *)lpFile)[15] + sizeof (ULONG);
			if (*((ULONG *)((LPBYTE)lpFile + ImageHdrOffset - sizeof (ULONG))) != IMAGE_NT_SIGNATURE)
			{
				UnmapViewOfFile(lpFile);
				return 0;
			}
		}

		poh = (PIMAGE_OPTIONAL_HEADER)((LPBYTE)lpFile + ImageHdrOffset + sizeof(IMAGE_FILE_HEADER));

		if (poh != NULL)
		{
			dwEntryPoint = (DWORD)(poh->AddressOfEntryPoint) + (DWORD)(poh->ImageBase);
		}

		UnmapViewOfFile(lpFile);
		return dwEntryPoint;
	}

	DWORD InjectDLL(PROCESS_INFORMATION& pi, LPCTSTR lpszEXEName, LPCTSTR lpszDLLName, DWORD dwFlags)
	{
		DWORD __SysError = 0;
		DWORD __Error = 0;

		DWORD dwTemp, dwProtect, dwCompleted = 0;
		DWORD dwEntryPoint = GetEntryPoint(GetFullFileName(lpszEXEName).c_str());
		LPBYTE pFirsBytes = new BYTE[GetProcLen() + INJDATA_LEN + PUSH_LEN];
		SYSCALL(ReadProcessMemory(pi.hProcess, (LPVOID)dwEntryPoint, pFirsBytes, GetProcLen() + INJDATA_LEN + PUSH_LEN, NULL), 5);

		VirtualProtectEx(pi.hProcess, (LPVOID)(dwEntryPoint + PUSH_LEN + GetProcLen()), INJDATA_LEN, PAGE_EXECUTE_READWRITE, &dwProtect);

		BYTE PushCmd[PUSH_LEN];
		PushCmd[0] = push;
		dwTemp = dwEntryPoint + PUSH_LEN + GetProcLen();
		memmove(&PushCmd[1], &dwTemp, PUSH_OPER_LEN);
		SYSCALL(WriteProcessMemory(pi.hProcess, (LPVOID)dwEntryPoint, PushCmd, PUSH_LEN, NULL), 5);
		dwEntryPoint += PUSH_LEN;

		SYSCALL(WriteProcessMemory(pi.hProcess, (LPVOID)dwEntryPoint, (LPVOID)GetOutAddress(), GetProcLen(), NULL), 5);

		BYTE ijData[INJDATA_LEN];
	#ifdef _UNICODE
		dwTemp = (DWORD)GetProcAddress((HMODULE)GetModuleHandle(szKernel32), szLoadLibraryW);
	#else
		dwTemp = (DWORD)GetProcAddress((HMODULE)GetModuleHandle(szKernel32), szLoadLibraryA);
	#endif
		memmove(ijData, &dwTemp, sizeof(DWORD));
		dwTemp = (DWORD)GetProcAddress((HMODULE)GetModuleHandle(szKernel32), szFreeLibrary);
		memmove(&ijData[1 * sizeof(DWORD)], &dwTemp, sizeof(DWORD));
		dwTemp = dwFlags & INJECT_UNLOAD;
		memmove(&ijData[2 * sizeof(DWORD)], &dwTemp, sizeof(DWORD));
		memmove(&ijData[3 * sizeof(DWORD)], &dwCompleted, sizeof(DWORD));
		memmove(&ijData[4 * sizeof(DWORD)], lpszDLLName, _tcslen(lpszDLLName) + 1);
		SYSCALL(WriteProcessMemory(pi.hProcess, (LPVOID)(dwEntryPoint + GetProcLen()), (LPVOID)&ijData, INJDATA_LEN, NULL), 5);

		ResumeThread(pi.hThread);
		
		while (true)
		{
			SYSCALL(ReadProcessMemory(pi.hProcess, (LPVOID)(3 * sizeof(DWORD) + (DWORD)(dwEntryPoint + GetProcLen())), &dwCompleted, sizeof(DWORD), NULL), 5);
			if (1 == dwCompleted)
			{
				SuspendThread(pi.hThread);
				CONTEXT Context;
				memset(&Context, 0, sizeof(Context));
				Context.ContextFlags = CONTEXT_INTEGER|CONTEXT_CONTROL;
				SYSCALL(GetThreadContext(pi.hThread, &Context), 5);

				dwEntryPoint = GetEntryPoint(GetFullFileName(lpszEXEName).c_str());
				Context.Eip = dwEntryPoint;
				SYSCALL(WriteProcessMemory(pi.hProcess, (LPVOID)dwEntryPoint, pFirsBytes, GetProcLen() + INJDATA_LEN + PUSH_LEN, NULL), 5);
				VirtualProtectEx(pi.hProcess, (LPVOID)(dwEntryPoint + PUSH_LEN + GetProcLen()), INJDATA_LEN, PAGE_EXECUTE_READWRITE, &dwProtect);

				if (pFirsBytes)
					delete[] pFirsBytes;

				SYSCALL(SetThreadContext(pi.hThread, &Context), 5);
				ResumeThread(pi.hThread);
				return 0;
			}
			else if (-1 == dwCompleted)
			{
				__Error = IE_LOAD_LIB_FAILED;
				goto InjectDLLError;
			}
			Sleep(100);
		}

InjectDLLError:

		VirtualProtectEx(pi.hProcess, (LPVOID)(dwEntryPoint + PUSH_LEN + GetProcLen()), INJDATA_LEN, PAGE_EXECUTE_READWRITE, &dwProtect);
		if (pFirsBytes)
			delete[] pFirsBytes;

		SetLastError(__SysError);
		return __Error;
	}
};

//////////////////////////////////////////////////////////////////////

DWORD WINAPI InjectDLL(LPCTSTR lpszEXEName, LPTSTR lpszCmdLine, LPCTSTR lpszDLLName, DWORD dwFlags)
{
	PROCESS_INFORMATION pi;
	memset(&pi, 0, sizeof(pi));

	try
	{
		if (!lpszEXEName || !lpszEXEName[0] || !lpszDLLName || !lpszDLLName[0])
		{
			SetLastError(ERROR_INVALID_PARAMETER);
			return IE_INVALID_PARAMETER;
		}

		STARTUPINFO si;
		memset(&si, 0, sizeof(si));
		si.cb = sizeof(si);

		CInternalInject InternalInject;

		if (!CreateProcess(InternalInject.GetFullFileName(lpszEXEName).c_str(), lpszCmdLine, NULL, NULL, FALSE, 
			NORMAL_PRIORITY_CLASS|CREATE_SUSPENDED, NULL, NULL, &si, &pi))
		{
			return IE_EXECUTE_FAILED;
		}

		DWORD dwLastError = 0, dwRet = InternalInject.InjectDLL(pi, lpszEXEName, lpszDLLName, dwFlags);

		if (dwRet)
		{
			dwLastError = GetLastError();
			TerminateProcess(pi.hProcess, -1);
		}

		CloseHandle(pi.hThread);
		CloseHandle(pi.hProcess);

		if (!dwRet)
		{
			SetLastError(0);
			return TRUE;
		}
		else
		{
			SetLastError(dwLastError);
			return dwRet;
		}
	}
	catch(...)
	{
		if (pi.hThread)
			CloseHandle(pi.hThread);
		if (pi.hProcess)
			CloseHandle(pi.hProcess);

		SetLastError(ERROR_ACCESS_DENIED);
		return IE_UNEXCEPTION;
	}
}

//////////////////////////////////////////////////////////////////////

DWORD WINAPI UnloadDLL(DWORD dwProcessID, LPCTSTR lpszDLLName, DWORD dwFlags)
{
	HANDLE hProcess = NULL;
	try
	{
		hProcess = OpenProcess(PROCESS_ALL_ACCESS, FALSE, dwProcessID);
		if (!hProcess)
			return IE_ACCESS_FAILED;

		return UnloadDLL2(hProcess, lpszDLLName, dwFlags);
	}
	catch(...)
	{
		if (hProcess)
			CloseHandle(hProcess);

		SetLastError(ERROR_ACCESS_DENIED);
		return IE_UNEXCEPTION;
	}
}

DWORD WINAPI UnloadDLL2(HANDLE hProcess, LPCTSTR lpszDLLName, DWORD dwFlags)
{
	DWORD dwAddress = NULL;
	HANDLE hThread = NULL;
	try
	{
		DWORD __SysError = 0;
		DWORD __Error = 0;

		if (!hProcess)
		{
			SetLastError(ERROR_INVALID_HANDLE);
			return IE_INVALID_PARAMETER;
		}

		if (!lpszDLLName || !lpszDLLName[0])
		{
			SetLastError(ERROR_INVALID_PARAMETER);
			return IE_INVALID_PARAMETER;
		}

		DWORD dwTemp;

		BYTE ijData[INJDATA_LEN];
	#ifdef _UNICODE
		dwTemp = (DWORD)GetProcAddress((HMODULE)GetModuleHandle(szKernel32), szGetModuleHandleW);
	#else
		dwTemp = (DWORD)GetProcAddress((HMODULE)GetModuleHandle(szKernel32), szGetModuleHandleA);
	#endif
		memmove(ijData, &dwTemp, sizeof(DWORD));
		dwTemp = (DWORD)GetProcAddress((HMODULE)GetModuleHandle(szKernel32), szFreeLibrary);
		memmove(&ijData[1 * sizeof(DWORD)], &dwTemp, sizeof(DWORD));
		dwTemp = -1;
		memmove(&ijData[2 * sizeof(DWORD)], &dwTemp, sizeof(DWORD));
		dwTemp = -1;
		memmove(&ijData[3 * sizeof(DWORD)], &dwTemp, sizeof(DWORD));
		
		TCHAR szModuleName[MAX_PATH + sizeof(TCHAR)];
		_tsplitpath(lpszDLLName, NULL, NULL, szModuleName, NULL);
		memmove(&ijData[4 * sizeof(DWORD)], szModuleName, _tcslen(szModuleName) + 1);

		dwAddress = (DWORD)VirtualAllocEx(hProcess, NULL, GetProcLen3() + INJDATA_LEN, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
		if (!dwAddress)
		{
			__Error = IE_SYSAPI_FAILED; 
			goto InjectDLLError;
		}

		SYSCALL(WriteProcessMemory(hProcess, (LPVOID)dwAddress, (LPVOID)GetOutAddress3(), GetProcLen3(), NULL), 5);
		SYSCALL(WriteProcessMemory(hProcess, (LPVOID)(dwAddress + GetProcLen3()), (LPVOID)&ijData, INJDATA_LEN, NULL), 5);

		DWORD dwThreadId;
		hThread = CreateRemoteThread(hProcess, NULL, 0, (LPTHREAD_START_ROUTINE)dwAddress, (LPVOID)(dwAddress + GetProcLen3()), 0, &dwThreadId);
		if (hThread == NULL)
		{
			__Error = IE_SYSAPI_FAILED; 
			goto InjectDLLError;
		}

		WaitForSingleObject(hThread, INFINITE);

		DWORD dwCode;
		GetExitCodeThread(hThread, &dwCode);

		if (-1 == dwCode)
		{
			__Error = IE_UNLOAD_LIB_FAILED;
			goto InjectDLLError;
		}

InjectDLLError:

		if (hThread)
			CloseHandle(hThread);

		if (dwAddress)
			VirtualFreeEx(hProcess, (LPVOID)dwAddress, GetProcLen3() + INJDATA_LEN, MEM_RELEASE);

		SetLastError(__SysError);
		return __Error;
	}
	catch(...)
	{
		if (hThread)
			CloseHandle(hThread);

		if (dwAddress)
			VirtualFreeEx(hProcess, (LPVOID)dwAddress, GetProcLen3() + INJDATA_LEN, MEM_RELEASE);

		SetLastError(ERROR_ACCESS_DENIED);
		return IE_UNEXCEPTION;
	}
}

//////////////////////////////////////////////////////////////////////

DWORD WINAPI AttachDLL(DWORD dwProcessID, LPCTSTR lpszDLLName, DWORD dwFlags)
{
	HANDLE hProcess = NULL;
	try
	{
		hProcess = OpenProcess(PROCESS_ALL_ACCESS, FALSE, dwProcessID);
		if (!hProcess)
			return IE_ACCESS_FAILED;

		return AttachDLL2(hProcess, lpszDLLName, dwFlags);
	}
	catch(...)
	{
		if (hProcess)
			CloseHandle(hProcess);

		SetLastError(ERROR_ACCESS_DENIED);
		return IE_UNEXCEPTION;
	}
}

DWORD WINAPI AttachDLL2(HANDLE hProcess, LPCTSTR lpszDLLName, DWORD dwFlags)
{
	DWORD dwAddress = NULL;
	HANDLE hThread = NULL;
	try
	{
		DWORD __SysError = 0;
		DWORD __Error = 0;

		if (!hProcess)
		{
			SetLastError(ERROR_INVALID_HANDLE);
			return IE_INVALID_PARAMETER;
		}

		if (!lpszDLLName || !lpszDLLName[0])
		{
			SetLastError(ERROR_INVALID_PARAMETER);
			return IE_INVALID_PARAMETER;
		}

		DWORD dwTemp;

		BYTE ijData[INJDATA_LEN];
	#ifdef _UNICODE
		dwTemp = (DWORD)GetProcAddress((HMODULE)GetModuleHandle(szKernel32), szLoadLibraryW);
	#else
		dwTemp = (DWORD)GetProcAddress((HMODULE)GetModuleHandle(szKernel32), szLoadLibraryA);
	#endif
		memmove(ijData, &dwTemp, sizeof(DWORD));
		dwTemp = (DWORD)GetProcAddress((HMODULE)GetModuleHandle(szKernel32), szFreeLibrary);
		memmove(&ijData[1 * sizeof(DWORD)], &dwTemp, sizeof(DWORD));
		dwTemp = dwFlags & INJECT_UNLOAD;
		memmove(&ijData[2 * sizeof(DWORD)], &dwTemp, sizeof(DWORD));
		dwTemp = -1;
		memmove(&ijData[3 * sizeof(DWORD)], &dwTemp, sizeof(DWORD));
		memmove(&ijData[4 * sizeof(DWORD)], lpszDLLName, _tcslen(lpszDLLName) + 1);

		dwAddress = (DWORD)VirtualAllocEx(hProcess, NULL, GetProcLen2() + INJDATA_LEN, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
		if (!dwAddress)
		{
			__Error = IE_SYSAPI_FAILED; 
			goto InjectDLLError;
		}

		SYSCALL(WriteProcessMemory(hProcess, (LPVOID)dwAddress, (LPVOID)GetOutAddress2(), GetProcLen2(), NULL), 5);
		SYSCALL(WriteProcessMemory(hProcess, (LPVOID)(dwAddress + GetProcLen2()), (LPVOID)&ijData, INJDATA_LEN, NULL), 5);

		DWORD dwThreadId;
		hThread = CreateRemoteThread(hProcess, NULL, 0, (LPTHREAD_START_ROUTINE)dwAddress, (LPVOID)(dwAddress + GetProcLen2()), 0, &dwThreadId);
		if (hThread == NULL)
		{
			__Error = IE_SYSAPI_FAILED; 
			goto InjectDLLError;
		}

		WaitForSingleObject(hThread, INFINITE);

		DWORD dwCode;
		GetExitCodeThread(hThread, &dwCode);

		if (-1 == dwCode)
		{
			__Error = IE_LOAD_LIB_FAILED;
			goto InjectDLLError;
		}

InjectDLLError:

		if (hThread)
			CloseHandle(hThread);

		if (dwAddress)
			VirtualFreeEx(hProcess, (LPVOID)dwAddress, GetProcLen2() + INJDATA_LEN, MEM_RELEASE);

		SetLastError(__SysError);
		return __Error;
	}
	catch(...)
	{
		if (hThread)
			CloseHandle(hThread);

		if (dwAddress)
			VirtualFreeEx(hProcess, (LPVOID)dwAddress, GetProcLen2() + INJDATA_LEN, MEM_RELEASE);

		SetLastError(ERROR_ACCESS_DENIED);
		return IE_UNEXCEPTION;
	}
}
