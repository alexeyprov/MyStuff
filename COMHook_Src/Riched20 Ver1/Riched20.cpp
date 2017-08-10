// Riched20.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
#include "Riched20.h"
#include "RecvWin.h"
#include "myRichEditOle.h"
#include "ImgHandler.h"
#include "TxtHandler.h"

/************************************
  REVISION LOG ENTRY
  Revision By: Zhang, Zhefu
  E-mail: codetiger@hotmail.com
  Revised on 10/2/2003 
  Comment: This is program code accompanying "COM Interface Hooking and Its Application"
           written by Zhefu Zhang posted on www.codeguru.com 
           You are free to reuse the code on the base of keeping this comment
		   All Right Reserved by author		   
 ************************************/

#define MSN6_CONN 10 //maximum 10 concurrent chat
#define MSN6_INTERFACE 6 //every chat have 6 windowless richedit

//Every Chat Window Has Up to 6 IWindowlessRichEdit
#pragma data_seg("Shared")

//You are using a IA-32 machine, so DWORD = LPVOID
DWORD g_lpIText[MSN6_CONN * MSN6_INTERFACE] = 
                  {NULL, NULL, NULL, NULL, NULL, 
                   NULL, NULL, NULL, NULL, NULL,
				   NULL, NULL, NULL, NULL, NULL, 
				   NULL, NULL, NULL, NULL, NULL,
				   NULL, NULL, NULL, NULL, NULL,
				   NULL, NULL, NULL, NULL, NULL, 
				   NULL, NULL, NULL, NULL, NULL,
				   NULL, NULL, NULL, NULL, NULL, 
				   NULL, NULL, NULL, NULL, NULL, 
				   NULL, NULL, NULL, NULL, NULL,
				   NULL, NULL, NULL, NULL, NULL, 
                   NULL, NULL, NULL, NULL, NULL};  
//Chat Window Handle
HWND  g_hMSNChatWnd[MSN6_CONN] = 
                  {NULL, NULL, NULL, NULL, NULL, 
                   NULL, NULL, NULL, NULL, NULL};   
DWORD g_dwActiveIndex = (DWORD)-1;  
BOOL  g_bInitialized = FALSE;
HWND  g_hRecvWnd = NULL;
#pragma data_seg()

// Instruct the linker to make the Shared section
// readable, writable, and shared.
#pragma comment(linker, "/section:Shared,rws")

BOOL APIENTRY DllMain( HANDLE hModule, 
                       DWORD  ul_reason_for_call, 
                       LPVOID lpReserved
					 )
{
    switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
			if(!g_bInitialized)
			{
				InitializeRecv(TRUE);
			}
			break;
		case DLL_THREAD_ATTACH:
			break;
		case DLL_THREAD_DETACH:
			break;
		case DLL_PROCESS_DETACH:
			if(g_bInitialized)
			{
				InitializeRecv(FALSE);				
			}
			break;
    }
    return TRUE;
}

DWORD GetChatNumber()
{
	int count = 0;
	for(int kk = 0; kk < MSN6_CONN; kk++)
	{
		if(g_hMSNChatWnd[kk] != NULL && g_hMSNChatWnd[kk] != (HWND)(DWORD)-1)
			count++;
	}
	return count;
}

HWND  GetChatHandle(DWORD dwIndex)
{
	if(g_hMSNChatWnd[dwIndex] != NULL && g_hMSNChatWnd[dwIndex] != (HWND)(DWORD)-1)
		return g_hMSNChatWnd[dwIndex];
	else
		return NULL;
}

BOOL  CloseChatHandle(DWORD dwIndex)
{
	g_hMSNChatWnd[dwIndex] = NULL;
    for(int i = 0; i < MSN6_INTERFACE; i++)
	{
		g_lpIText[dwIndex * MSN6_INTERFACE + i] = NULL;
	}
	return TRUE;
}

//0, 1, 3
//addr edit, chat edit, send edit
BOOL  QueryChatContent(DWORD dwIndex)
{
	if(g_hMSNChatWnd[dwIndex] == NULL) return FALSE;
	//Only read the input the area
	for(int i = 3; i < MSN6_INTERFACE - 2; i++)
	{
		{
			BSTR bstr;
			HRESULT hr = ((ITextServices*)::g_lpIText[dwIndex*MSN6_INTERFACE+i])->TxGetText(&bstr);
			if(SUCCEEDED(hr))
			{
				IRichEditOle* pReo = NULL;
				LRESULT lr;
				((ITextServices*)::g_lpIText[dwIndex*MSN6_INTERFACE+i])->TxSendMessage(
                   EM_GETOLEINTERFACE, 0, (LPARAM)(LPVOID*)&pReo, &lr);
				if(lr == 0)
				{
					::ReportErr(_T("pReo fail"));
					continue;
				}
				
                LONG nNumber = pReo->GetObjectCount();  //Your Images' Number
    			GeneralTxtHandler((ITextServices*)::g_lpIText[dwIndex*MSN6_INTERFACE+i]);
	
				PopMsg(_T("No%d edit have  %d image %d"), i, nNumber);
                
				GeneralObjHandler(pReo);
				pReo->Release();
				::SysFreeString(bstr);
			}
		}
	}
	return TRUE;
}

BOOL InitializeRecv(BOOL bInitialize)
{
	if(bInitialize == ::g_bInitialized) return FALSE;
    if(bInitialize)
	{
		//PopMsg(_T("InitializeRecv New"));
		//Create Window
		MyRegisterClass();
		g_hRecvWnd = InitInstance();
        if(!g_hRecvWnd)
			return FALSE;
		//PopMsg(_T("InitializeRecv New OK"));
		::SetTimer(g_hRecvWnd, 101, 10000, NULL); 
	}
	else
	{
		//PopMsg(_T("InitializeRecv Kill"));
		//Destroy Window
		if(!::IsWindow(g_hRecvWnd))
			return FALSE;
		//PopMsg(_T("InitializeRecv Kill OK"));
		::PostMessage(::g_hRecvWnd, WM_CLOSE, 0, 0);
		::g_hRecvWnd = NULL;
	}	
	::g_bInitialized = bInitialize;
	return TRUE;
}

//Note: DWORD(-1) means reserved area!!!
DWORD GetActiveIndex(DWORD dwCurrentIndex, DWORD& NextFreeInterfaceSlot)
{
	if(dwCurrentIndex == (DWORD)-1) //no chat before (or all previous are closed)
	{
		dwCurrentIndex = 0;
		return 0;
	}
	BOOL bHandleUsed = FALSE;
	BOOL bInterfaceUsed = FALSE;
	BOOL bInterfaceUsedup = TRUE;
    if(g_hMSNChatWnd[dwCurrentIndex] != NULL && g_hMSNChatWnd[dwCurrentIndex] != (HWND)(DWORD)-1)
		bHandleUsed = TRUE;
	for(int i = 0; i < MSN6_INTERFACE; i++)
	{
		if(g_lpIText[dwCurrentIndex * MSN6_INTERFACE + i] != NULL && 
			g_lpIText[dwCurrentIndex * MSN6_INTERFACE + i] != (DWORD)-1)
		{
			bInterfaceUsed = TRUE;
		}
		else //null or -1
		{
			bInterfaceUsedup = FALSE;
			NextFreeInterfaceSlot = i;
		}
	}

	if(bHandleUsed && bInterfaceUsed && bInterfaceUsedup)
	{
		for(int kk = 0; kk < MSN6_CONN; kk++)
		{
			bHandleUsed = FALSE;
	        bInterfaceUsed = FALSE;
	        bInterfaceUsedup = TRUE;
            if(g_hMSNChatWnd[kk] != NULL && g_hMSNChatWnd[kk] != (HWND)(DWORD)-1)
	        	bHandleUsed = TRUE;
	        for(int i = 0; i < MSN6_INTERFACE; i++)
			{
		        if(g_lpIText[kk * MSN6_INTERFACE + i] != NULL && 
			        g_lpIText[kk * MSN6_INTERFACE + i] != (DWORD)-1)
				{
					bInterfaceUsed = TRUE;
				}
		        else //null or -1
				{
					bInterfaceUsed = FALSE;
			        NextFreeInterfaceSlot = i;
				}
			}
			//the slot is full
	    	if(bHandleUsed && bInterfaceUsed && bInterfaceUsedup)
			{
				continue;
			}
			dwCurrentIndex = kk;
			return kk; 
		}
		return (DWORD)-1;
	} 
	return (DWORD)-1;
}

BOOL InsertHandle(HWND hChat)
{
	DWORD dwIndex = ::g_dwActiveIndex;
	DWORD dwInterface;
	dwIndex = ::GetActiveIndex(dwIndex, dwInterface);
	
	if(dwIndex == (DWORD)-1)
		return FALSE;
	
	if(::g_hMSNChatWnd[dwIndex] != NULL && ::g_hMSNChatWnd[dwIndex] != (HWND)(DWORD)-1)
		return FALSE;
	::g_hMSNChatWnd[dwIndex] = hChat;
	//set interface
	for(int i = 0; i < MSN6_INTERFACE; i++)
	{
		if(g_lpIText[dwIndex * MSN6_INTERFACE + i] == NULL)
			g_lpIText[dwIndex * MSN6_INTERFACE + i] = (DWORD)-1;
	}
	return TRUE;
}

BOOL InsertInterface(DWORD lpInterface)
{
	DWORD dwIndex = ::g_dwActiveIndex;
	DWORD dwInterface;
	dwIndex = ::GetActiveIndex(dwIndex, dwInterface);
	if(dwIndex == (DWORD)-1)
		return FALSE;
	
	//set interface
	for(int i = 0; i < MSN6_INTERFACE; i++)
	{
		if(g_lpIText[dwIndex * MSN6_INTERFACE + i] == (DWORD)-1 || g_lpIText[dwIndex * MSN6_INTERFACE + i] == (DWORD)NULL)
		{
			g_lpIText[dwIndex * MSN6_INTERFACE + i] = lpInterface;
			if(::g_hMSNChatWnd[dwIndex] == NULL)
				::g_hMSNChatWnd[dwIndex] = (HWND)(DWORD)-1;
			return TRUE;
		}
	}
	return FALSE;
}


/////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////
//Fake COM Interface
RICHED20_API GUID IID_IRichEditOle  
 = { 0x00020D00, 0x0, 0x0, { 0xC0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x46 } };

RICHED20_API GUID IID_IRichEditOleCallback
 = { 0x00020D03, 0x0, 0x0, { 0xC0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x46 } };

RICHED20_API GUID IID_ITextServices
 = { 0x8d33f740, 0xcf58, 0x11ce, {0xa8, 0x9d, 0x00, 0xaa, 0x00, 0x6c, 0xad, 0xc5}};

RICHED20_API GUID IID_ITextHost
 = { 0xc5bdd8d0, 0xd26e, 0x11ce, {0xa8, 0x9e, 0x00, 0xaa, 0x00, 0x6c, 0xad, 0xc5}};

RICHED20_API GUID IID_ITextHost2
 = { 0xc5bdd8d0, 0xd26e, 0x11ce, {0xa8, 0x9e, 0x00, 0xaa, 0x00, 0x6c, 0xad, 0xc5}};

typedef HRESULT (__stdcall *lpCreateTextServices)(IUnknown *punkOuter, ITextHost *pITextHost, IUnknown **ppUnk);
typedef LRESULT (__stdcall *lpREExtendedRegisterClass)(HWND hWnd, UINT Msg, WPARAM wParam, LPARAM lParam);
typedef LRESULT (__stdcall *lpRichEdit10ANSIWndProc)(HWND hWnd, UINT Msg, WPARAM wParam, LPARAM lParam);
typedef LRESULT (__stdcall *lpRichEditANSIWndProc)(HWND hWnd, UINT Msg, WPARAM wParam, LPARAM lParam);

#define NEW_DLL_NAME  _T("\\RichEd20.Dll")
//You MUST dynanically load dll
HRESULT WINAPI CreateTextServices(IUnknown *punkOuter, ITextHost *pITextHost, IUnknown **ppUnk)
{
	TCHAR szLib[MAX_PATH]; //255 is enough
	DWORD dw = GetSystemDirectory(szLib, MAX_PATH);
	if(dw == 0) return 0;
	szLib[dw] = TCHAR('\0');
	::lstrcat(szLib, NEW_DLL_NAME);
	HMODULE hLib = LoadLibrary(szLib);
    if(!hLib) return 0;

    lpCreateTextServices _CreateTextServices = (HRESULT (__stdcall *) 
		(IUnknown*, ITextHost*, IUnknown**))
		::GetProcAddress(hLib, "CreateTextServices");
	if(!_CreateTextServices) return 0;

	HRESULT hr = (_CreateTextServices)(punkOuter, pITextHost, ppUnk);

	ITextServices* lpTx;
	((IUnknown*)(*ppUnk))->QueryInterface(IID_ITextServices, (void**)(&lpTx));
	InsertInterface((DWORD)lpTx);
    //::FreeLibrary(hLib);
	return hr;
}

//Note:
//Protected Storage System Service Protects "RICHEDIT20.DLL" under %SystemRoot%System32
LRESULT WINAPI REExtendedRegisterClass(HINSTANCE hInstance)
{
	return 0;
}

LRESULT WINAPI RichEdit10ANSIWndProc(HWND hWnd, UINT Msg, WPARAM wParam, LPARAM lParam)
{
	return 0;
}

LRESULT WINAPI RichEditANSIWndProc(HWND hWnd, UINT Msg, WPARAM wParam, LPARAM lParam)
{
	return 0;
}
