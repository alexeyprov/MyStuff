// DemoEditorDoc.cpp : implementation of the CDemoEditorDoc class
//

#include "stdafx.h"
#include "DemoEditor.h"

#include "DemoEditorDoc.h"
#include "DemoEditorView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CDemoEditorDoc

IMPLEMENT_DYNCREATE(CDemoEditorDoc, CDocument)

BEGIN_MESSAGE_MAP(CDemoEditorDoc, CDocument)
	//{{AFX_MSG_MAP(CDemoEditorDoc)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CDemoEditorDoc construction/destruction

CDemoEditorDoc::CDemoEditorDoc()
{
	// TODO: add one-time construction code here

}

CDemoEditorDoc::~CDemoEditorDoc()
{
}

BOOL CDemoEditorDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: add reinitialization code here
	// (SDI documents will reuse this document)

	return TRUE;
}



/////////////////////////////////////////////////////////////////////////////
// CDemoEditorDoc serialization

void CDemoEditorDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: add storing code here
	}
	else
	{
		// TODO: add loading code here
	}
}

/////////////////////////////////////////////////////////////////////////////
// CDemoEditorDoc diagnostics

#ifdef _DEBUG
void CDemoEditorDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CDemoEditorDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CDemoEditorDoc commands

BOOL CDemoEditorDoc::OnOpenDocument(LPCTSTR lpszPathName) 
{
	CDemoEditorView *pView = (CDemoEditorView*)GetView();
	if (pView != NULL)
	{
		if(!pView->Load(lpszPathName)) return FALSE;
	}
	return TRUE;
}

BOOL CDemoEditorDoc::OnSaveDocument(LPCTSTR lpszPathName) 
{
	CDemoEditorView *pView = (CDemoEditorView*)GetView();
	if (pView != NULL)
	{
		if(!pView->Save(lpszPathName)) return FALSE;
	}


	return TRUE;
}



CDemoEditorView* CDemoEditorDoc::GetView() const
{
	// find the first view - if there are no views
	// we must return NULL

	POSITION pos = GetFirstViewPosition();
	if (pos == NULL)
		return NULL;

	// find the first view that is a CRichEditView

	CView* pView;
	while (pos != NULL)
	{
		pView = GetNextView(pos);
		if (pView->IsKindOf(RUNTIME_CLASS(CDemoEditorView)))
			return (CDemoEditorView*) pView;
	}

	// can't find one--return NULL

	return NULL;
}
