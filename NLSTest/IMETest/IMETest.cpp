// IMETest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

int _tmain(int argc, _TCHAR* argv[])
{
	_tprintf(_T("IME is %s\n"), 
		(0 != ::GetSystemMetrics(SM_IMMENABLED)) ?
		_T("enabled") :
		_T("disabled"));
	return 0;
}

