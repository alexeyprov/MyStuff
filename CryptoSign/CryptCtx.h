#pragma once

class CCryptoContext
{
public:

	CCryptoContext()
	{
		m_pKey = NULL;
		m_hProv = NULL;

		if (!CryptAcquireContext(&m_hProv, NULL, NULL, PROV_RSA_FULL, 0))
		{
			throw CCryptoException(CCryptoException::fnAcquireContext);
		}

		m_pKey = new CKey(m_hProv, true);
		m_pKey->CreateKey(_T("if you want to f$ck 4 funny, f$ck yourself and save your money!"));
	}

	~CCryptoContext()
	{
		if (m_pKey != NULL)
		{
			delete[] m_pKey;
			m_pKey = NULL;
		}

		// Release the provider handle.
		if (m_hProv != 0)
		{
			CryptReleaseContext(m_hProv, 0);
		}
	}

// Operations
	DWORD Encrypt(LPBYTE pBuffer, DWORD dwBufLen, bool bEstimate = false) throw(...)
	{
		DWORD dwLen = dwBufLen;
		if (bEstimate)
		{
			if (!CryptEncrypt(*m_pKey, NULL, TRUE, 0, NULL, &dwLen, dwBufLen))
			{
				throw CCryptoException(CCryptoException::fnEncrypt);
			}
		}
		else
		{
			if (!CryptEncrypt(*m_pKey, NULL, TRUE, 0, pBuffer, &dwLen, dwBufLen))
			{
				throw CCryptoException(CCryptoException::fnEncrypt);
			}
		}
		return dwLen;
	}

	DWORD Decrypt(LPBYTE pBuffer, DWORD dwBufLen) throw(...)
	{
		DWORD dwLen = dwBufLen;
		if (!CryptDecrypt(*m_pKey, NULL, TRUE, 0, pBuffer, &dwLen))
		{
			throw CCryptoException(CCryptoException::fnDecrypt);
		}
		return dwLen;
	}

// Data Members
private:
	CKey* m_pKey;
	HCRYPTPROV m_hProv;
};
