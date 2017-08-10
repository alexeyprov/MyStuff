// QQQDoc.cpp : implementation of the CQQQDoc class
//

#include "stdafx.h"
#include "QQQ.h"

#include "QQQDoc.h"
#include "CntrItem.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CQQQDoc

IMPLEMENT_DYNCREATE(CQQQDoc, CRichEditDoc)

BEGIN_MESSAGE_MAP(CQQQDoc, CRichEditDoc)
	// Enable default OLE container implementation
	ON_UPDATE_COMMAND_UI(ID_OLE_EDIT_LINKS, CRichEditDoc::OnUpdateEditLinksMenu)
	ON_COMMAND(ID_OLE_EDIT_LINKS, CRichEditDoc::OnEditLinks)
	ON_UPDATE_COMMAND_UI_RANGE(ID_OLE_VERB_FIRST, ID_OLE_VERB_LAST, CRichEditDoc::OnUpdateObjectVerbMenu)
END_MESSAGE_MAP()


// CQQQDoc construction/destruction

CQQQDoc::CQQQDoc()
{
	// TODO: add one-time construction code here

}

CQQQDoc::~CQQQDoc()
{
}

BOOL CQQQDoc::OnNewDocument()
{
	if (!CRichEditDoc::OnNewDocument())
		return FALSE;

	// TODO: add reinitialization code here
	// (SDI documents will reuse this document)

	return TRUE;
}
CRichEditCntrItem* CQQQDoc::CreateClientItem(REOBJECT* preo) const
{
	return new CQQQCntrItem(preo, const_cast<CQQQDoc*>(this));
}




// CQQQDoc serialization

void CQQQDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: add storing code here
	}
	else
	{
		// TODO: add loading code here
	}

	// Calling the base class CRichEditDoc enables serialization
	//  of the container document's COleClientItem objects.
	// TODO: set CRichEditDoc::m_bRTF = FALSE if you are serializing as text
	CRichEditDoc::Serialize(ar);
}


// CQQQDoc diagnostics

#ifdef _DEBUG
void CQQQDoc::AssertValid() const
{
	CRichEditDoc::AssertValid();
}

void CQQQDoc::Dump(CDumpContext& dc) const
{
	CRichEditDoc::Dump(dc);
}
#endif //_DEBUG


// CQQQDoc commands
