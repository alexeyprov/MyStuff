#pragma once

class CKey
{
public:
// Construction/Destruction
	CKey(HCRYPTPROV hProv, bool bSymmetric);
	virtual ~CKey();

// Operations
	void CreateKey(LPCTSTR szPwd) throw(...);

	operator HCRYPTKEY() const
	{
		return m_hKey;
	}

// Data Members
private:
	HCRYPTPROV m_hProv;
	HCRYPTKEY m_hKey;
	bool m_bSymmetric;
};
