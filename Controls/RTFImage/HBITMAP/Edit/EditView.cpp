// EditView.cpp : implementation of the CMyEditView class
//

#include "stdafx.h"
#include "Edit.h"

#include "EditDoc.h"
#include "CntrItem.h"
#include "EditView.h"
#include "MainFrm.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CMyEditView

IMPLEMENT_DYNCREATE(CMyEditView, CRichEditView)

BEGIN_MESSAGE_MAP(CMyEditView, CRichEditView)
	//{{AFX_MSG_MAP(CMyEditView)
	ON_WM_DESTROY()
	//}}AFX_MSG_MAP
	// Standard printing commands
	ON_COMMAND_RANGE(ID_BUTTON32774, ID_BUTTON32788, OnFaceSelect)
	ON_COMMAND(ID_FILE_PRINT, CRichEditView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, CRichEditView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, CRichEditView::OnFilePrintPreview)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMyEditView construction/destruction

CMyEditView::CMyEditView()
{
	m_pRichEditOle = NULL;
}

CMyEditView::~CMyEditView()
{
}

BOOL CMyEditView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CRichEditView::PreCreateWindow(cs);
}

CImageList& CMyEditView::GetImageList()
{
	CMainFrame *pFrame = (CMainFrame *)AfxGetMainWnd();
	return pFrame->m_imgListFaces;
}

void CMyEditView::OnInitialUpdate()
{
	CRichEditView::OnInitialUpdate();

	// Get the Controller Interface from the RichEdit Control
	//
	if (m_pRichEditOle == NULL)
		m_pRichEditOle = GetRichEditCtrl().GetIRichEditOle();
	ASSERT(m_pRichEditOle != NULL);

	// Unregister this edit control as a potential target for OLE drag-and-drop
	//
	::RevokeDragDrop(m_hWnd);

	// Set the printing margins (720 twips = 1/2 inch).
	SetMargins(CRect(720, 720, 720, 720));
}

HBITMAP CMyEditView::GetImage(CImageList& list, int num)
{
	CBitmap dist;
	CClientDC dc(NULL);

	IMAGEINFO ii;
	list.GetImageInfo(num, &ii);

	int nWidth = ii.rcImage.right - ii.rcImage.left;
	int nHeight = ii.rcImage.bottom - ii.rcImage.top;

	dist.CreateCompatibleBitmap(&dc, nWidth, nHeight);
	CDC memDC;
	memDC.CreateCompatibleDC(&dc);
	CBitmap* pOldBitmap = memDC.SelectObject(&dist);
	
	memDC.FillSolidRect(0, 0, nWidth, nHeight, 
			GetRichEditCtrl().SetBackgroundColor(TRUE, 0));
	list.Draw(&memDC, num, CPoint(0, 0), ILD_NORMAL);

	memDC.SelectObject(pOldBitmap);

	return (HBITMAP)dist.Detach();
}

/////////////////////////////////////////////////////////////////////////////
// CMyEditView printing

BOOL CMyEditView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}


void CMyEditView::OnDestroy()
{
	m_pRichEditOle->Release();

	// Deactivate the item on destruction; this is important
	// when a splitter view is being used.
   CRichEditView::OnDestroy();
   COleClientItem* pActiveItem = GetDocument()->GetInPlaceActiveItem(this);
   if (pActiveItem != NULL && pActiveItem->GetActiveView() == this)
   {
      pActiveItem->Deactivate();
      ASSERT(GetDocument()->GetInPlaceActiveItem(this) == NULL);
   }
}


/////////////////////////////////////////////////////////////////////////////
// CMyEditView diagnostics

#ifdef _DEBUG
void CMyEditView::AssertValid() const
{
	CRichEditView::AssertValid();
}

void CMyEditView::Dump(CDumpContext& dc) const
{
	CRichEditView::Dump(dc);
}

CEditDoc* CMyEditView::GetDocument() // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CEditDoc)));
	return (CEditDoc*)m_pDocument;
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CMyEditView message handlers

#include "ImageDataObject.h"

void CMyEditView::OnFaceSelect(UINT nID)
{
	int nFace = nID - ID_BUTTON32774;
	
	// Get the bitmap from the imagelist
	//
	HBITMAP hBitmap = GetImage(GetImageList(), nFace);

	if (hBitmap)
	{
		// Insert the bitmap to the richedit control at the current location
		// 
		CImageDataObject::InsertBitmap(m_pRichEditOle, hBitmap);
	}
}
