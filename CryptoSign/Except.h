#pragma once

class CGenericException
{
// Construction/Destruction
public:
	CGenericException()
	{
		m_dwError = GetLastError();
	}

	virtual ~CGenericException()
	{
	}

// Operations
	virtual void ReportError() const = 0;

// Data Members
protected:
	DWORD m_dwError;
};

class COutOfMemoryException : public CGenericException
{
public:
	virtual void ReportError() const
	{
		_tprintf(_T("Out of memory!\n"));
	}
};

class CBadEncodingException : public CGenericException
{
public:
	enum ECauseOfFailure
	{
		cofBadLength,
		cofBadChar,
	};
	
	CBadEncodingException(ECauseOfFailure cof)
	{
		m_cof = cof;
	}

	// Attributes
	ECauseOfFailure GetCauseOfFailure() const
	{
		return m_cof;
	}

	// Overrides
	virtual void ReportError() const
	{
		switch (m_cof)
		{
		case cofBadLength:
			_tprintf(_T("Invalid length of encoded string!\n"));
			break;
		case cofBadChar:
			_tprintf(_T("Invalid character encountered!\n"));
			break;
		}
	}

	// Data Members
private:
	ECauseOfFailure m_cof;
};
