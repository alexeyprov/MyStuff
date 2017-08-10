// Foo.h : Declaration of the CFoo

#pragma once
#include "resource.h"       // main symbols

#include "KillMe.h"


// CFoo

template <class T>
class ATL_NO_VTABLE CFooBase : 
	public CComObjectRootEx<CComSingleThreadModel>,
	//public CComCoClass<CFoo, &CLSID_Foo>,
	public IDispatchImpl<T, &__uuidof(T), &LIBID_KillMeLib, /*wMajor =*/ 1, /*wMinor =*/ 0>
{
public:
	CFooBase()
	{
	}

//DECLARE_REGISTRY_RESOURCEID(IDR_FOO)


//BEGIN_COM_MAP(CFoo)
//	COM_INTERFACE_ENTRY(IFoo)
//	COM_INTERFACE_ENTRY(IDispatch)
//END_COM_MAP()


	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}
	
	void FinalRelease() 
	{
	}

public:

	STDMETHOD(get_Name)(BSTR* pVal)
	{
		*pVal = ::SysAllocString(L"CFooBase");
		return S_OK;
	}
};

//OBJECT_ENTRY_AUTO(__uuidof(Foo), CFoo)
