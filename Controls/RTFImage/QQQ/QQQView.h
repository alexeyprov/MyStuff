// QQQView.h : interface of the CQQQView class
//


#pragma once

class CQQQCntrItem;

class CQQQView : public CRichEditView
{
protected: // create from serialization only
	CQQQView();
	DECLARE_DYNCREATE(CQQQView)

// Attributes
public:
	CQQQDoc* GetDocument() const;

// Operations
public:

// Overrides
	public:
virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:
	virtual void OnInitialUpdate(); // called first time after construct
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);

// Implementation
public:
	virtual ~CQQQView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:
	static DWORD CALLBACK StreamReadCallback(DWORD dwCookie, LPBYTE pbBuff, LONG cb, LONG FAR *pcb)
	{
		_ASSERTE(dwCookie != 0);
		_ASSERTE(pcb != NULL);

		return !::ReadFile((HANDLE)dwCookie, pbBuff, cb, (LPDWORD)pcb, NULL);
	}

// Generated message map functions
protected:
	afx_msg void OnDestroy();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnFileOpen();
private:
	bool LoadFile(LPCTSTR lpszFileName);
};

#ifndef _DEBUG  // debug version in QQQView.cpp
inline CQQQDoc* CQQQView::GetDocument() const
   { return reinterpret_cast<CQQQDoc*>(m_pDocument); }
#endif

