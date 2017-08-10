// QQQDoc.h : interface of the CQQQDoc class
//


#pragma once

class CQQQDoc : public CRichEditDoc
{
protected: // create from serialization only
	CQQQDoc();
	DECLARE_DYNCREATE(CQQQDoc)

// Attributes
public:

// Operations
public:

// Overrides
	public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);
	virtual CRichEditCntrItem* CreateClientItem(REOBJECT* preo) const;

// Implementation
public:
	virtual ~CQQQDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	DECLARE_MESSAGE_MAP()
};


