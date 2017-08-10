// EditView.h : interface of the CMyEditView class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_EDITVIEW_H__95301502_27EB_4EF5_95D2_ED96C62B0EE1__INCLUDED_)
#define AFX_EDITVIEW_H__95301502_27EB_4EF5_95D2_ED96C62B0EE1__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CEditCntrItem;

class CMyEditView : public CRichEditView
{
protected: // create from serialization only
	CMyEditView();
	DECLARE_DYNCREATE(CMyEditView)

// Attributes
public:
	CEditDoc* GetDocument();

	CImageList& GetImageList();

// Operations
public:
	HBITMAP GetImage(CImageList& list, int num);

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMyEditView)
	public:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	protected:
	virtual void OnInitialUpdate(); // called first time after construct
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CMyEditView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:
	IRichEditOle	*m_pRichEditOle;

// Generated message map functions
protected:
	//{{AFX_MSG(CMyEditView)
	afx_msg void OnDestroy();
	//}}AFX_MSG

	afx_msg void OnFaceSelect(UINT nID);

	DECLARE_MESSAGE_MAP()
};

#ifndef _DEBUG  // debug version in EditView.cpp
inline CEditDoc* CMyEditView::GetDocument()
   { return (CEditDoc*)m_pDocument; }
#endif

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_EDITVIEW_H__95301502_27EB_4EF5_95D2_ED96C62B0EE1__INCLUDED_)
