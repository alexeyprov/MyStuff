#include "stdafx.h"
#include "key.h"

#include "CryptoEx.h"

CKey::CKey(HCRYPTPROV hProv, bool bSymmetric)
{
	m_hProv = hProv;
	m_bSymmetric = bSymmetric;
}

CKey::~CKey()
{
	if (m_hKey != NULL)
	{
		::CryptDestroyKey(m_hKey);
	}
}

void CKey::CreateKey(LPCTSTR szPwd) throw(...)
{
	HCRYPTHASH hHash = NULL;
	DWORD dwLen = 0;

	// Create a hash object.
	if (!CryptCreateHash(m_hProv, CALG_MD5, 0, 0, &hHash)) 
	{
		throw CCryptoException(CCryptoException::fnCreateHash);
	}

	// Hash the password string.
	dwLen = _tcslen(szPwd) * sizeof(TCHAR);
	if (!CryptHashData(hHash, (BYTE*) szPwd, dwLen, 0))
	{
		::CryptDestroyHash(hHash);
		throw CCryptoException(CCryptoException::fnHashData);
	}

	// Create a key based on the hash of the password.
	if (!CryptDeriveKey(m_hProv, (m_bSymmetric) ? CALG_RC4 : CALG_RSA_SIGN, hHash, CRYPT_EXPORTABLE, &m_hKey))
	{
		::CryptDestroyHash(hHash);
		throw CCryptoException(CCryptoException::fnDeriveKey);
	}

	::CryptDestroyHash(hHash);
}