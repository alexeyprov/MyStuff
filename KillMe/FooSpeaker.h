// FooSpeaker.h : Declaration of the CFooSpeaker

#pragma once
#include "resource.h"       // main symbols

#include "KillMe.h"


// CFooSpeaker

class ATL_NO_VTABLE CFooSpeaker : 
	//public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CFooSpeaker, &CLSID_FooSpeaker>,
	//public IDispatchImpl<IFooSpeaker, &IID_IFooSpeaker, &LIBID_KillMeLib, /*wMajor =*/ 1, /*wMinor =*/ 0>
	public CFooBase<IFooSpeaker>
{
public:
	CFooSpeaker()
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_FOOSPEAKER)

BEGIN_COM_MAP(CFooSpeaker)
	COM_INTERFACE_ENTRY(IFoo)
	COM_INTERFACE_ENTRY(IFooSpeaker)
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

	STDMETHOD(Speak)(BSTR* ret);
};

OBJECT_ENTRY_AUTO(__uuidof(FooSpeaker), CFooSpeaker)
