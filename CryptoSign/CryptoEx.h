#pragma once

#include "except.h"

const LPCTSTR c_arSrcNames[] = {_T("CryptAcquireContext"), _T("CryptCreateHash"),
								_T("CryptHashData"), _T("CryptDeriveKey"),
								_T("CryptEncrypt"), _T("CryptDecrypt")};

class CCryptoException : public CGenericException
{
public:
// Construction/Destruction
	enum ESource
	{
		fnAcquireContext,
		fnCreateHash,
		fnHashData,
		fnDeriveKey,
		fnEncrypt,
		fnDecrypt
	};

	CCryptoException(ESource nErrSrc) 
	{
		m_nErrSrc = nErrSrc;
	}

	~CCryptoException()
	{
	}

// Operations
	virtual void ReportError() const
	{
		_tprintf(_T("Error %x during %s!\n"), GetLastError(), c_arSrcNames[m_nErrSrc]);
	}

// Data Members
private:
	DWORD m_dwError;
	ESource m_nErrSrc;
};

