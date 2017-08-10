// QQQView.cpp : implementation of the CQQQView class
//

#include "stdafx.h"
#include "QQQ.h"

#include "QQQDoc.h"
#include "CntrItem.h"
#include "QQQView.h"
#include ".\qqqview.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CQQQView

IMPLEMENT_DYNCREATE(CQQQView, CRichEditView)

BEGIN_MESSAGE_MAP(CQQQView, CRichEditView)
	ON_WM_DESTROY()
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, CRichEditView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, CRichEditView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, CRichEditView::OnFilePrintPreview)
	ON_COMMAND(ID_FILE_OPEN, OnFileOpen)
END_MESSAGE_MAP()

// CQQQView construction/destruction

CQQQView::CQQQView()
{
	// TODO: add construction code here

}

CQQQView::~CQQQView()
{
}

BOOL CQQQView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CRichEditView::PreCreateWindow(cs);
}

void CQQQView::OnInitialUpdate()
{
	CRichEditView::OnInitialUpdate();

	// Set the printing margins (720 twips = 1/2 inch)
	SetMargins(CRect(720, 720, 720, 720));
}


// CQQQView printing

BOOL CQQQView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}


void CQQQView::OnDestroy()
{
	// Deactivate the item on destruction; this is important
	// when a splitter view is being used
   COleClientItem* pActiveItem = GetDocument()->GetInPlaceActiveItem(this);
   if (pActiveItem != NULL && pActiveItem->GetActiveView() == this)
   {
      pActiveItem->Deactivate();
      ASSERT(GetDocument()->GetInPlaceActiveItem(this) == NULL);
   }
   CRichEditView::OnDestroy();
}



// CQQQView diagnostics

#ifdef _DEBUG
void CQQQView::AssertValid() const
{
	CRichEditView::AssertValid();
}

void CQQQView::Dump(CDumpContext& dc) const
{
	CRichEditView::Dump(dc);
}

CQQQDoc* CQQQView::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CQQQDoc)));
	return (CQQQDoc*)m_pDocument;
}
#endif //_DEBUG


// CQQQView message handlers

void CQQQView::OnFileOpen()
{
	CFileDialog dlg(TRUE, _T("RTF"), NULL, OFN_HIDEREADONLY|OFN_PATHMUSTEXIST, _T("RTF Files (*.rtf)|*.rtf"));
	if (IDOK == dlg.DoModal())
	{
		LoadFile(dlg.GetPathName());
	}
}

bool CQQQView::LoadFile(LPCTSTR lpszFileName)
{
	_ASSERTE(lpszFileName != NULL);

	HANDLE hFile = ::CreateFile(lpszFileName, GENERIC_READ, 0, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL | FILE_FLAG_SEQUENTIAL_SCAN, NULL);
	if(hFile == INVALID_HANDLE_VALUE)
	{
		return false;
	}

	EDITSTREAM es;
	es.dwCookie = (DWORD)hFile;
	es.dwError = 0;
	es.pfnCallback = StreamReadCallback;
	::SendMessage(m_hWnd, EM_STREAMIN, SF_RTF, (LPARAM) &es);
	::CloseHandle(hFile);
	return !(BOOL)es.dwError;
}