#pragma once

class CHostId
{
// Construction/Destruction
public:
	CHostId(void);
	~CHostId(void);

// Attributes
	bool GetHostId(LPBYTE pDest) const;

// Implementation
protected:
	void GetHardDriveId(void);

// Data Members
private:
	BYTE m_arHostID[MAX_ADAPTER_ADDRESS_LENGTH];
	UINT m_nLength;
};
