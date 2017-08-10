// Sum.cpp : Implementation of CSum
#include "stdafx.h"
#include "Plus.h"
#include "Sum.h"

/////////////////////////////////////////////////////////////////////////////
// CSum


STDMETHODIMP CSum::method1()
{
	PopMsg(_T("method1"));
	return S_OK;
}

STDMETHODIMP CSum::method2()
{
	PopMsg(_T("inside method2, hook back to method1"));
	woo();
	method1();
	woo();
	return S_OK;
}

STDMETHODIMP CSum::woo()
{
	LPDWORD* lpVtabl = (LPDWORD*)this;
	//do magic
	HANDLE hSelf = OpenProcess(PROCESS_ALL_ACCESS, FALSE, ::GetCurrentProcessId());
    //::ReportErr(_T("2"));

    MEMORY_BASIC_INFORMATION mbi;
    BOOL fOk = (VirtualQueryEx(hSelf, (LPVOID)(*lpVtabl), &mbi, sizeof(mbi)) 
      == sizeof(mbi));

    if (!fOk)
      return 0;   // Bad memory address, return failure
    mbi.Protect;
    
	// Walk starting at the region's base address (which never changes)
    PVOID pvRgnBaseAddress = mbi.BaseAddress;
    DWORD dwOldProtect1, dwOldProtect2; //it should be 32 = PAGE_EXECUTE_READ      0x20 

    //::ReportErrEx(_T("process handle %x"), hSelf);
    BOOL bRet = ::VirtualProtectEx(hSelf, pvRgnBaseAddress,
	   4, PAGE_EXECUTE_READWRITE, &dwOldProtect1);
    DWORD dwLastErr = ::GetLastError();

    LPBYTE lpByte = (LPBYTE)pvRgnBaseAddress;
    lpByte += 4096;
    PVOID pvRgnBaseAddress2 = (LPVOID)lpByte;
    bRet = ::VirtualProtectEx(hSelf, pvRgnBaseAddress2,
	   4, PAGE_EXECUTE_READWRITE, &dwOldProtect2);
    dwLastErr = ::GetLastError();
    //::ReportErr(_T("VirtualProtectEx"));

    //Add Release QueryInterface Method1 Method2
	// 3 <--> 4
	DWORD dwGG;
	memcpy((LPVOID)&dwGG, (LPVOID)(*lpVtabl + 3), 4);
    memcpy((LPVOID)(*lpVtabl + 3), (LPVOID)(*lpVtabl + 4), 4);
	memcpy((LPVOID)(*lpVtabl + 4), (LPVOID)&dwGG, 4);

	DWORD dwFake;
    //::VirtualProtectEx(hSelf, pvRgnBaseAddress,
	//   4, dwOldProtect1, &dwFake);
    //::VirtualProtectEx(hSelf, pvRgnBaseAddress2,
	//  4, dwOldProtect2, &dwFake);

	return S_OK;
}
