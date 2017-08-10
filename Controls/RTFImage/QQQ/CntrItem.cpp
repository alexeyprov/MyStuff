// CntrItem.cpp : implementation of the CQQQCntrItem class
//

#include "stdafx.h"
#include "QQQ.h"

#include "QQQDoc.h"
#include "QQQView.h"
#include "CntrItem.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CQQQCntrItem implementation

IMPLEMENT_SERIAL(CQQQCntrItem, CRichEditCntrItem, 0)

CQQQCntrItem::CQQQCntrItem(REOBJECT* preo, CQQQDoc* pContainer)
	: CRichEditCntrItem(preo, pContainer)
{
	// TODO: add one-time construction code here
}

CQQQCntrItem::~CQQQCntrItem()
{
	// TODO: add cleanup code here
}


// CQQQCntrItem diagnostics

#ifdef _DEBUG
void CQQQCntrItem::AssertValid() const
{
	CRichEditCntrItem::AssertValid();
}

void CQQQCntrItem::Dump(CDumpContext& dc) const
{
	CRichEditCntrItem::Dump(dc);
}
#endif

