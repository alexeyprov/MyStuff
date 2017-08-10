#include "stdafx.h"
#include "hostid.h"
#include ".\hostid.h"

CHostId::CHostId(void)
{
	IP_ADAPTER_INFO ai;
	ULONG sz = sizeof(IP_ADAPTER_INFO);
	DWORD dwErr = ::GetAdaptersInfo(&ai, &sz);

	if (ERROR_SUCCESS == dwErr)
	{
		m_nLength = ai.AddressLength;
		memcpy(m_arHostID, ai.Address, m_nLength);
	}
	else
	{
		GetHardDriveId();
	}
}

CHostId::~CHostId(void)
{
}

bool CHostId::GetHostId(LPBYTE pDest) const
{
	if (m_nLength > 0)
	{
		memcpy(pDest, m_arHostID, m_nLength);
		return true;
	}
	return false;
}

void CHostId::GetHardDriveId()
{
	m_nLength = 0;
}
