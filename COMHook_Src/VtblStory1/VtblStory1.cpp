// VtblStory1.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
void testVtabl();

int APIENTRY WinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPSTR     lpCmdLine,
                     int       nCmdShow)
{
 	testVtabl();
	return 0;
}

class classA
{
public:
virtual int method1()
{
return 11;
}
virtual int method2()
{
return 12;
}
virtual int method3()
{
return 13;
}
};
 
class classB: public classA
{
public:
virtual int method1()
{
return 21;
}
int m;
int n;
};
 
class classC : public classB
{
public:
int method1(int a, short b)
{
return 31;
}
};

class classD
{
public:
	int m;
};

void testVtabl()
{
classD D;
D.m = 15;
LPVOID pD = &D;

classC* pC = new classC;
classB* pB = pC;
int y = pB->method1();
 
classB bb;
bb.m = 31;
bb.n = 32;
LPVOID pBB = &bb;
LPVOID pBB2 = &(bb.m);

LPDWORD* lpVtabl = (LPDWORD*)&bb;
HANDLE hSelf = OpenProcess(PROCESS_ALL_ACCESS, FALSE,
  ::GetCurrentProcessId());

MEMORY_BASIC_INFORMATION mbi;
if(VirtualQueryEx(hSelf, (LPVOID)(*lpVtabl), 
    &mbi, sizeof(mbi)) != sizeof(mbi)) return;
   
PVOID pvRgnBaseAddress = mbi.BaseAddress;
DWORD dwOldProtect1, dwOldProtect2;
if(!::VirtualProtectEx(hSelf, pvRgnBaseAddress,
   4, PAGE_EXECUTE_READWRITE, &dwOldProtect1)) return;

BOOL bStridePage = FALSE; //Check if Vtbl Strike 2 Pages
LPBYTE lpByte = (LPBYTE)pvRgnBaseAddress;
lpByte += 4096;
if((DWORD)lpByte < (DWORD)lpVtabl + 4 * 2) //We explain later
  bStridePage = TRUE;

PVOID pvRgnBaseAddress2 = (LPVOID)lpByte;
if(bStridePage)
  if(!::VirtualProtectEx(hSelf, pvRgnBaseAddress2,
	4, PAGE_EXECUTE_READWRITE, &dwOldProtect2)) return;
    
DWORD dw;
memcpy((LPVOID)&dw, (LPVOID)(*lpVtabl), 4);
memcpy((LPVOID)(*lpVtabl), (LPVOID)(*lpVtabl + 1), 4);
memcpy((LPVOID)(*lpVtabl + 1), (LPVOID)&dw, 4);
	
DWORD dwFake;
::VirtualProtectEx(hSelf, pvRgnBaseAddress,
  4, dwOldProtect1, &dwFake);
if(bStridePage)
::VirtualProtectEx(hSelf, pvRgnBaseAddress2,
   4, dwOldProtect2, &dwFake);

//Compiler sometimes addicts to optimization
y = bb.method1(); //21
y = bb.method2(); //22

return;
}

