#pragma once
#include <Richedit.h>
#include <richole.h>
#include <atlstr.h>

class /*ATL_NO_VTABLE*/ CRichEditCallback :
	//public CComObjectRootEx<CComSingleThreadModel>,
	//public CComCoClass<CRichEditCallback, &GUID_NULL>,
	//public IDispatchImpl<IRichEditOleCallback, &IID_IRichEditOleCallback, &GUID_NULL, /*wMajor =*/ 1, /*wMinor =*/ 0>
	public IRichEditOleCallback	
{
public:
	//CRichEditCallback(void);
	//~CRichEditCallback(void);

	// *** IUnknown methods ***
	STDMETHOD(QueryInterface) (THIS_ REFIID riid, LPVOID FAR * lplpObj)
	{
		return E_NOTIMPL;
	}
	STDMETHOD_(ULONG,AddRef) (THIS) 
	{
		return 0;
	}
	STDMETHOD_(ULONG,Release) (THIS)
	{
		return 0;
	}

	// *** IRichEditOleCallback methods ***
	STDMETHOD(GetNewStorage) (THIS_ LPSTORAGE FAR * lplpstg)
	{
		//return ::StgCreateStorageEx(L"mystorage.stg", 
		//		STGM_READWRITE | STGM_CREATE | STGM_DELETEONRELEASE | STGM_SHARE_EXCLUSIVE, 
		//		STGFMT_DOCFILE, 
		//		0,
		//		NULL, 
		//		NULL, 
		//		IID_IStorage, 
		//		reinterpret_cast<LPVOID*>(lplpstg));
		//HRESULT hr = S_OK;
		//static DWORD dwStorageCounter;
		//if(!m_pStorage)
		//{
		//	hr = StgCreateStorageEx(/*CAtlStringW(CComVariant(dwStorageCounter)) +*/ L"mysuperstorage.stg", 
		//		STGM_READWRITE | STGM_CREATE | STGM_DELETEONRELEASE | STGM_SHARE_EXCLUSIVE, 
		//		STGFMT_DOCFILE, 
		//		0,
		//		NULL, 
		//		NULL, 
		//		IID_IStorage, 
		//		(LPVOID*)&m_pStorage.p);
		//}
		//hr = m_pStorage->QueryInterface(IID_IStorage, (LPVOID*)lplpstg);

		//return hr;

		CComPtr<ILockBytes> lpLB;
		HRESULT hr = ::CreateILockBytesOnHGlobal(NULL, TRUE, &lpLB);
		if (FAILED(hr))
		{
			return hr;
		}
		
		ATLASSERT(lpLB != NULL);

		return ::StgCreateDocfileOnILockBytes(lpLB,
			STGM_SHARE_EXCLUSIVE|STGM_CREATE|STGM_READWRITE, 0, lplpstg);
	}
	STDMETHOD(GetInPlaceContext) (THIS_ LPOLEINPLACEFRAME FAR * lplpFrame,
		LPOLEINPLACEUIWINDOW FAR * lplpDoc,
		LPOLEINPLACEFRAMEINFO lpFrameInfo) 
	{
		return S_OK;
	}
	STDMETHOD(ShowContainerUI) (THIS_ BOOL fShow)
	{
		return S_OK;
	}
	STDMETHOD(QueryInsertObject) (THIS_ LPCLSID lpclsid, LPSTORAGE lpstg,
		LONG cp) 
	{
		//HRESULT hr = lpstg->Revert();
		return S_OK;
	}
	STDMETHOD(DeleteObject) (THIS_ LPOLEOBJECT lpoleobj) 
	{
		return S_OK;
	}
	STDMETHOD(QueryAcceptData) (THIS_ LPDATAOBJECT lpdataobj,
		CLIPFORMAT FAR * lpcfFormat, DWORD reco,
		BOOL fReally, HGLOBAL hMetaPict) 
	{
		return S_OK;
	}
	STDMETHOD(ContextSensitiveHelp) (THIS_ BOOL fEnterMode) 
	{
		return S_OK;
	}
	STDMETHOD(GetClipboardData) (THIS_ CHARRANGE FAR * lpchrg, DWORD reco,
		LPDATAOBJECT FAR * lplpdataobj) 
	{ 
		return S_OK;
	}
	STDMETHOD(GetDragDropEffect) (THIS_ BOOL fDrag, DWORD grfKeyState,
		LPDWORD pdwEffect) 
	{
		return S_OK;
	}
	STDMETHOD(GetContextMenu) (THIS_ WORD seltype, LPOLEOBJECT lpoleobj,
		CHARRANGE FAR * lpchrg,
		HMENU FAR * lphmenu) 
	{
		return S_OK;
	}

private:
	CComPtr<IStorage> m_pStorage;
};
