// CntrItem.cpp : implementation of the CEditCntrItem class
//

#include "stdafx.h"
#include "Edit.h"

#include "EditDoc.h"
#include "EditView.h"
#include "CntrItem.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CEditCntrItem implementation

IMPLEMENT_SERIAL(CEditCntrItem, CRichEditCntrItem, 0)

CEditCntrItem::CEditCntrItem(REOBJECT* preo, CEditDoc* pContainer)
	: CRichEditCntrItem(preo, pContainer)
{
	// TODO: add one-time construction code here
	
}

CEditCntrItem::~CEditCntrItem()
{
	// TODO: add cleanup code here
	
}

/////////////////////////////////////////////////////////////////////////////
// CEditCntrItem diagnostics

#ifdef _DEBUG
void CEditCntrItem::AssertValid() const
{
	CRichEditCntrItem::AssertValid();
}

void CEditCntrItem::Dump(CDumpContext& dc) const
{
	CRichEditCntrItem::Dump(dc);
}
#endif

/////////////////////////////////////////////////////////////////////////////
