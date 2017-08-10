union TorH
{
	HWND hwnd;
	void* p;
};

class CTextSetterBase
{
public:
	CTextSetterBase(TorH* pTorH) :
	  _pTorH(pTorH)
	{
	}

	virtual void SetText(LPCTSTR szText) = 0;
protected:
	TorH* _pTorH;
};

class CPasteTextSetter : 
	public CTextSetterBase
{
public:
	CPasteTextSetter(TorH* pTorH) :
	  CTextSetterBase(pTorH)
	{
	}

	virtual void SetText(LPCTSTR szText)
	{
		//do paste here
	}
};

template<class T>
class CTextSetterT : 
	public CTextSetterBase
{
public:
	CTextSetterT(TorH* pTorH) :
	  CTextSetterBase(pTorH)
	{
	}

	virtual void SetText(LPCTSTR szText)
	{
		//get range
		int r = T::GetRange();
		//set text here
	}
};

class CRangeGetter
{
public:
	static int GetRange()
	{
		return 1;
	}
};

class CSelGetter
{
public:
	static int GetRange()
	{
		return 2;
	}
};

typedef CTextSetterT<CRangeGetter> CFullTextSetter;
typedef CTextSetterT<CSelGetter> CSelTextSetter;

void SetText(LPCTSTR szText, CTextSetterBase* pSetter)
{
	pSetter->SetText(szText);
}

template<class T>
void TestSetter()
{
	TorH th = {0};
	T setter(&th);
	SetText(_T("hello"), &setter);
}