// Generator.h : Declaration of the CGenerator

#pragma once
#include "resource.h"       // main symbols

#include "RndGen.h"


// CGenerator

class ATL_NO_VTABLE CGenerator : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CGenerator, &CLSID_Generator>,
	public ISupportErrorInfo,
	public IDispatchImpl<IGenerator, &IID_IGenerator, &LIBID_RndGenLib, /*wMajor =*/ 1, /*wMinor =*/ 0>
{
public:
	CGenerator()
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_GENERATOR)

BEGIN_COM_MAP(CGenerator)
	COM_INTERFACE_ENTRY(IGenerator)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(ISupportErrorInfo)
END_COM_MAP()

// ISupportsErrorInfo
	STDMETHOD(InterfaceSupportsErrorInfo)(REFIID riid);

	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		m_nSeed = 0L;
		return S_OK;
	}
	
	void FinalRelease() 
	{
	}

// IGenerator
public:

	STDMETHOD(get_Seed)(LONG* pVal);
	STDMETHOD(put_Seed)(LONG newVal);
	STDMETHOD(NextRandom)(LONG nMinValue, LONG nMaxValue, LONG* pVal);

// Data Members
private:
	long m_nSeed;
};

OBJECT_ENTRY_AUTO(__uuidof(Generator), CGenerator)
