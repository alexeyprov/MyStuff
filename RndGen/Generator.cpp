// Generator.cpp : Implementation of CGenerator

#include "stdafx.h"
#include "Generator.h"

#include <time.h>
// CGenerator

STDMETHODIMP CGenerator::InterfaceSupportsErrorInfo(REFIID riid)
{
	static const IID* arr[] = 
	{
		&IID_IGenerator
	};

	for (int i=0; i < sizeof(arr) / sizeof(arr[0]); i++)
	{
		if (InlineIsEqualGUID(*arr[i],riid))
			return S_OK;
	}
	return S_FALSE;
}

STDMETHODIMP CGenerator::get_Seed(LONG* pVal)
{
	*pVal = m_nSeed;
	return S_OK;
}

STDMETHODIMP CGenerator::put_Seed(LONG newVal)
{
	if (newVal <= 0L)
	{
		return E_INVALIDARG;
	}
	m_nSeed = newVal;
	return S_OK;
}

STDMETHODIMP CGenerator::NextRandom(LONG nMinValue, LONG nMaxValue, LONG* pVal)
{
	if (nMinValue >= nMaxValue)
	{
		return E_INVALIDARG;
	}

	if (0L == m_nSeed)
	{
		//Initialize seed from timer
		m_nSeed = (LONG) (UINT) time(NULL);
	}

	srand((UINT) m_nSeed);

	*pVal = nMinValue + int(double(rand()) * (nMaxValue - nMinValue) / RAND_MAX);
	return S_OK;
}
