// Sum.h : Declaration of the CSum

#ifndef __SUM_H_
#define __SUM_H_

#include "resource.h"       // main symbols

/////////////////////////////////////////////////////////////////////////////
// CSum
class ATL_NO_VTABLE CSum : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CSum, &CLSID_Sum>,
	public ISum
{
public:
	CSum()
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_SUM)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CSum)
	COM_INTERFACE_ENTRY(ISum)
END_COM_MAP()

// ISum
public:
	STDMETHOD(woo)();
	STDMETHOD(method2)();
	STDMETHOD(method1)();
};

#endif //__SUM_H_
