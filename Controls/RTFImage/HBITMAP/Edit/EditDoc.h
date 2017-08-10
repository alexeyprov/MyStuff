// EditDoc.h : interface of the CEditDoc class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_EDITDOC_H__B7BEC17B_09B0_4DA1_98A7_88C293BA6360__INCLUDED_)
#define AFX_EDITDOC_H__B7BEC17B_09B0_4DA1_98A7_88C293BA6360__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


class CEditDoc : public CRichEditDoc
{
protected: // create from serialization only
	CEditDoc();
	DECLARE_DYNCREATE(CEditDoc)

// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CEditDoc)
	public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);
	//}}AFX_VIRTUAL
	virtual CRichEditCntrItem* CreateClientItem(REOBJECT* preo) const;

// Implementation
public:
	virtual ~CEditDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CEditDoc)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_EDITDOC_H__B7BEC17B_09B0_4DA1_98A7_88C293BA6360__INCLUDED_)
