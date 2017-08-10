// FooListener.h : Declaration of the CFooListener

#pragma once
#include "resource.h"       // main symbols

#include "KillMe.h"


// CFooListener

class ATL_NO_VTABLE CFooListener : 
	//public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CFooListener, &CLSID_FooListener>,
	//public IDispatchImpl<IFooListener, &IID_IFooListener, &LIBID_KillMeLib, /*wMajor =*/ 1, /*wMinor =*/ 0>
	public CFooBase<IFooListener>
{
public:
	CFooListener()
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_FOOLISTENER)

BEGIN_COM_MAP(CFooListener)
	COM_INTERFACE_ENTRY(IFoo)
	COM_INTERFACE_ENTRY(IFooListener)
	COM_INTERFACE_ENTRY(IDispatch)
END_COM_MAP()


	//DECLARE_PROTECT_FINAL_CONSTRUCT()

	//HRESULT FinalConstruct()
	//{
	//	return S_OK;
	//}
	//
	//void FinalRelease() 
	//{
	//}

public:

	STDMETHOD(Listen)(BSTR data);
};

OBJECT_ENTRY_AUTO(__uuidof(FooListener), CFooListener)
