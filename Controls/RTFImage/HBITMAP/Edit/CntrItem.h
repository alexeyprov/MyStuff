// CntrItem.h : interface of the CEditCntrItem class
//

#if !defined(AFX_CNTRITEM_H__8DCF10FE_FF13_4A01_8D5B_11C7A35D1F69__INCLUDED_)
#define AFX_CNTRITEM_H__8DCF10FE_FF13_4A01_8D5B_11C7A35D1F69__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CEditDoc;
class CMyEditView;

class CEditCntrItem : public CRichEditCntrItem
{
	DECLARE_SERIAL(CEditCntrItem)

// Constructors
public:
	CEditCntrItem(REOBJECT* preo = NULL, CEditDoc* pContainer = NULL);
		// Note: pContainer is allowed to be NULL to enable IMPLEMENT_SERIALIZE.
		//  IMPLEMENT_SERIALIZE requires the class have a constructor with
		//  zero arguments.  Normally, OLE items are constructed with a
		//  non-NULL document pointer.

// Attributes
public:
	CEditDoc* GetDocument()
		{ return (CEditDoc*)CRichEditCntrItem::GetDocument(); }
	CMyEditView* GetActiveView()
		{ return (CMyEditView*)CRichEditCntrItem::GetActiveView(); }

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CEditCntrItem)
	public:
	protected:
	//}}AFX_VIRTUAL

// Implementation
public:
	~CEditCntrItem();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CNTRITEM_H__8DCF10FE_FF13_4A01_8D5B_11C7A35D1F69__INCLUDED_)
