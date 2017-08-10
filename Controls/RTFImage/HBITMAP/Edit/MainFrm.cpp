// MainFrm.cpp : implementation of the CMainFrame class
//

#include "stdafx.h"
#include "Edit.h"

#include "MainFrm.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CMainFrame

IMPLEMENT_DYNCREATE(CMainFrame, CFrameWnd)

BEGIN_MESSAGE_MAP(CMainFrame, CFrameWnd)
	//{{AFX_MSG_MAP(CMainFrame)
	ON_WM_CREATE()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

static UINT indicators[] =
{
	ID_SEPARATOR,           // status line indicator
	ID_INDICATOR_CAPS,
	ID_INDICATOR_NUM,
	ID_INDICATOR_SCRL,
};

/////////////////////////////////////////////////////////////////////////////
// CMainFrame construction/destruction

CMainFrame::CMainFrame()
{
	// TODO: add member initialization code here
	
}

CMainFrame::~CMainFrame()
{
}

int CMainFrame::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
	if (CFrameWnd::OnCreate(lpCreateStruct) == -1)
		return -1;
	
	if (!m_wndToolBar.CreateEx(this, TBSTYLE_FLAT, WS_CHILD | WS_VISIBLE
		| CBRS_GRIPPER | CBRS_TOOLTIPS | CBRS_FLYBY | CBRS_SIZE_FIXED) ||
		!m_wndToolBar.LoadToolBar(IDR_MAINFRAME))
	{
		TRACE0("Failed to create toolbar\n");
		return -1;      // fail to create
	}

	// In order to allow more than 16 colors in the toolbars
	//

	// Create the list with 18x18 pixel for each image, 
	//   using a 24-bit DIB section, and using a mask.
	m_imgListFaces.Create(18, 18, ILC_COLOR24|ILC_MASK, 18, 1);
	
	// Load the list from resource, and select the transperency color
	CBitmap bmpFaces;
	bmpFaces.LoadBitmap(IDB_BITMAP_FACES);
	m_imgListFaces.Add(&bmpFaces, RGB(255, 255, 255));

	// Finally, set the new image list to the toolbar
	m_wndToolBar.GetToolBarCtrl().SetImageList(&m_imgListFaces);

	if (!m_wndStatusBar.Create(this) ||
		!m_wndStatusBar.SetIndicators(indicators,
		  sizeof(indicators)/sizeof(UINT)))
	{
		TRACE0("Failed to create status bar\n");
		return -1;      // fail to create
	}

	// Put each 5 button in a row
	//
	for (int i = 4; i < 15; i += 5)
	{
		UINT nStyle = m_wndToolBar.GetButtonStyle(i);
		nStyle |= TBBS_WRAPPED;
		m_wndToolBar.SetButtonStyle( i, nStyle );
	}

	// TODO: Delete these three lines if you don't want the toolbar to
	//  be dockable
	m_wndToolBar.EnableDocking(0);
	EnableDocking(CBRS_ALIGN_ANY);
	// Position the floatting toolbar window
	CRect frame;
	GetWindowRect(frame);
	FloatControlBar(&m_wndToolBar, frame.BottomRight() - CPoint(200, 200));

	return 0;
}

BOOL CMainFrame::PreCreateWindow(CREATESTRUCT& cs)
{
	if( !CFrameWnd::PreCreateWindow(cs) )
		return FALSE;
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return TRUE;
}

/////////////////////////////////////////////////////////////////////////////
// CMainFrame diagnostics

#ifdef _DEBUG
void CMainFrame::AssertValid() const
{
	CFrameWnd::AssertValid();
}

void CMainFrame::Dump(CDumpContext& dc) const
{
	CFrameWnd::Dump(dc);
}

#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CMainFrame message handlers

