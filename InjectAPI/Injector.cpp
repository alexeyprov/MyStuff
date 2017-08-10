/*
		Copyright (C) 1999, CAT Computing Center (R).   

		Project:		Injector
		Description:	Main implementation
*/

#include "stdafx.h"

using namespace ConApp;

#include "Injector.h"

CInjector::CInjector(int argc, TCHAR *args[], TCHAR *arge[]) : CConApp(argc, args, arge)
{
	m_bUnload = false;
	m_bUnloadDLL = false;
	m_bAttach = false;
	m_bSilent = false;
}

CInjector::~CInjector()
{
}

int CInjector::OnRun(int nCode)
{
	if (!m_bSilent)
	{
		PrintCopyRight();
		if (m_bAttach)
			_tprintf(_T("Attach \"%s\" to #%s ...\n\n"), m_DLLFile.c_str(), m_ExecFile.c_str());
		else if (m_bUnloadDLL)
			_tprintf(_T("Unload \"%s\" from #%s ...\n\n"), m_DLLFile.c_str(), m_ExecFile.c_str());
		else
			_tprintf(_T("Load \"%s\" to %s ...\n\n"), m_DLLFile.c_str(), m_ExecFile.c_str());
	}

	DWORD dwError = 0;

	if (m_bUnloadDLL)
		dwError = UnloadDLL(_ttol(m_ExecFile.c_str()), m_DLLFile.c_str(), 0);
	else if (m_bAttach)
		dwError = AttachDLL(_ttol(m_ExecFile.c_str()), m_DLLFile.c_str(), m_bUnload ? INJECT_UNLOAD : 0);
	else
		dwError = InjectDLL(m_ExecFile.c_str(), (LPTSTR)m_CmdLine.c_str(), m_DLLFile.c_str(), m_bUnload ? INJECT_UNLOAD : 0);

	if (dwError)
	{
		DWORD dwLastError = GetLastError();
		switch (dwError)
		{
			case IE_EXECUTE_FAILED:
				OnError(1, m_ExecFile.c_str(), dwLastError);
			break;

			case IE_ALLOCATE_FAILED:
				OnError(2, dwLastError);
			break;

			case IE_ACCESS_FAILED:
				OnError(3, dwLastError);
			break;

			case IE_LOAD_LIB_FAILED:
				OnError(4, m_DLLFile.c_str());
			break;

			case IE_SYSAPI_FAILED:
				OnError(5, dwLastError);
			break;

			case IE_UNLOAD_LIB_FAILED:
				OnError(8, m_DLLFile.c_str());
			break;

			case IE_INVALID_PARAMETER:
				OnError(9);
			break;
			

			default:
				OnError(-1);
		}
	}
	return dwError;
}

int CInjector::OnError(int ec, ...)
{
	if (m_bSilent)
		return ec;

	va_list args;
	va_start(args, ec);
	switch(ec)
	{
		case 1:
		{
			LPCTSTR lpName va_arg(args, TCHAR*);
			DWORD dwError = va_arg(args, DWORD); 
			_tprintf(_T("Error %02X : Error to load \"%s\", OS error: %08X\n"), ec, lpName, dwError);
			break;
		}

		case 2:
			_tprintf(_T("Error %02X : Cannot allocate process memory, OS error: %08X\n"), ec, va_arg(args, DWORD));
		break;

		case 3:
			_tprintf(_T("Error %02X : Cannot access to process, OS error: %08X\n"), ec, va_arg(args, DWORD));
		break;

		case 4:
			_tprintf(_T("Error %02X : Cannot load library \"%s\"\n"), ec, va_arg(args, TCHAR*));
		break;

		case 5:
			_tprintf(_T("Error %02X : Cannot perform some operation, OS error: %08X\n"), ec);
		break;

		case 6:
			_tprintf(_T("Error %02X : Specified incompatible commands\n"), ec);
		break;

		case 7:
			_tprintf(_T("Error %02X : No <ExecFile> or ID specified\n"), ec);
		break;

		case 8:
			_tprintf(_T("Error %02X : Cannot unload library \"%s\"\n"), ec, va_arg(args, TCHAR*));
		break;

		case 9:
			_tprintf(_T("Error %02X : Internal error\n"), ec, va_arg(args, TCHAR*));
		break;

		case 10:
			_tprintf(_T("Error %02X : Invalid command \"%s\"\n"), ec, va_arg(args, TCHAR*));
		break;

		case 11:
			_tprintf(_T("Error %02X : No <ExecFile> specified\n"), ec);
		break;

		default:
			_tprintf(_T("Error %08X : Unexception error\n"), ec);
	}
	va_end(args);
	return m_nLastError = ec;
}

inline void CInjector::PrintCopyRight()
{
	_putts(_T("\nInjector 2.5\nCopyright (C) 2001, CAT Computing Center (R)\nMichael Lubushkin (MishaL@moscow.vestedev.com).\n"));
}

inline int CInjector::OnHelp()
{
	PrintCopyRight();
	_putts(_T("Usage:\n\tInjector [options] <dllname> <filename|processID>\n"
		"\nSummary:\n\n\t<dllename> dll to be injected or uloaded\n"
		"\t<filename> EXE-file and command line\n"
		"\tprocessID ID of the process\n"
		"\noptions:\n\n\t/S silent, display no message"
		"\n\t/I unload injected dll immediately"
		"\n\t/A attach dll to process (NT only)"
		"\n\t/U unload specified dll (NT only)\n"));
	return 0;
}

int CInjector::ParseCommandLine()
{
	if (CConApp::ParseCommandLine() == PCL_HELP || m_nArgc < 3)
		return PCL_HELP;

	for (int i = 1; i < m_nArgc; i++)
	{
		if((_tccmp(&m_lpArgS[i][0], _T("-")) == 0) || (_tccmp(&m_lpArgS[i][0], _T("/")) == 0))
		{
			if(_tcsicmp(&m_lpArgS[i][1], _T("I")) == 0)
			{
				m_bUnload = true;
			}
			else if(_tcsicmp(&m_lpArgS[i][1], _T("A")) == 0)
			{
				m_bAttach = true;
			}
			else if(_tcsicmp(&m_lpArgS[i][1], _T("U")) == 0)
			{
				m_bUnloadDLL = true;
			}
			else if(_tcsicmp(&m_lpArgS[i][1], _T("S")) == 0)
			{
				m_bSilent = true;
			}
			else
			{
				OnError(10, m_lpArgS[i]);
				return PCL_QUIT;
			}
		}
		else
		{
			if ((m_bAttach + m_bUnloadDLL + m_bUnload) >= 2)
			{
				OnError(6, m_lpArgS[i]);
				return PCL_QUIT;
			}

			m_DLLFile = m_lpArgS[i];
			m_ExecFile = m_lpArgS[++i];

			if (m_bAttach || m_bUnloadDLL)
				return PCL_RUN;

			for (++i; i < m_nArgc; i++)
			{
				m_CmdLine += m_lpArgS[i];
				if (i < (m_nArgc -1))
				{
					m_CmdLine += _T(' ');
				}
			}
			return PCL_RUN;
		}
	}
	OnError(7);
	return PCL_QUIT;
}

int ConMain()
{
	try
	{
		CInjector Injector(argc, args, arge);
		return Injector.Run();
	}
	catch(...)
	{
		_putts(_T("Unexception error\n"));
	}
	return -1;
}
