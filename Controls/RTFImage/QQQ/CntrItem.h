// CntrItem.h : interface of the CQQQCntrItem class
//

#pragma once

class CQQQDoc;
class CQQQView;

class CQQQCntrItem : public CRichEditCntrItem
{
	DECLARE_SERIAL(CQQQCntrItem)

// Constructors
public:
	CQQQCntrItem(REOBJECT* preo = NULL, CQQQDoc* pContainer = NULL);
		// Note: pContainer is allowed to be NULL to enable IMPLEMENT_SERIALIZE
		//  IMPLEMENT_SERIALIZE requires the class have a constructor with
		//  zero arguments.  Normally, OLE items are constructed with a
		//  non-NULL document pointer

// Attributes
public:
	CQQQDoc* GetDocument()
		{ return reinterpret_cast<CQQQDoc*>(CRichEditCntrItem::GetDocument()); }
	CQQQView* GetActiveView()
		{ return reinterpret_cast<CQQQView*>(CRichEditCntrItem::GetActiveView()); }

	public:
	protected:

// Implementation
public:
	~CQQQCntrItem();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif
};

