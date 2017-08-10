// DemoEditorView.h : interface of the CDemoEditorView class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_DEMOEDITORVIEW_H__38D0A65B_50E4_4E9F_9A7F_A754F386BEBA__INCLUDED_)
#define AFX_DEMOEDITORVIEW_H__38D0A65B_50E4_4E9F_9A7F_A754F386BEBA__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "Custom/SyntaxColorSpellChecker.h"

class CDemoEditorView : public CCtrlView
{
protected: // create from serialization only
	CDemoEditorView();
	DECLARE_DYNCREATE(CDemoEditorView)

// Attributes
public:
	CDemoEditorDoc* GetDocument();

private:

	CSyntaxColorSpellChecker m_colorizer;
	// some stuff
	CString			m_strOrgSuggest;
	CStringArray	m_tSuggestlist;
	int				m_bChangeAll;
	int				m_nextedComments;
	int				m_bSpellEnabled;
	BOOL			m_bInContextMenu;
	EDITSTREAM		m_es;

	CHARRANGE		m_ContextRange;
	bool			readFile(LPCSTR  sFileName);
	bool			writeFile(LPCSTR  sFileName);
public:

	BOOL			Load(LPCSTR file);
	BOOL			Save(LPCSTR file);

	void			OnInitMenuPopup(CMenu* pMenu, UINT nIndex, BOOL bSysMenu);
	void			UpdateDynaMenu(CCmdUI* pCmdUI,CStringArray & list,CString & m_strOriginal);

	CRichEditCtrl& GetRichEditCtrl() const
					{ return *(CRichEditCtrl*)this; }




// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CDemoEditorView)
	public:
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	protected:
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);

	//}}AFX_VIRTUAL

	afx_msg void OnChangeEdit() ;
	afx_msg void OnMsgfilterEdit(NMHDR* pNMHDR, LRESULT* pResult) ;
	afx_msg void OnRButtonDown(UINT nFlags, CPoint point);

	afx_msg void OnEditCut();
	afx_msg void OnEditCopy();
	afx_msg void OnEditPaste();
	afx_msg void OnEditClear();
	afx_msg void OnEditUndo();


	void OnSuggestUI(CCmdUI* pCmdUI);
	void OnSuggest(UINT id );
	afx_msg void OnAddtodictionnary();
	afx_msg void OnUpdateAddtodictionnary(CCmdUI* pCmdUI);
	afx_msg void OnIgnoralways();
	afx_msg void OnUpdateIgnoralways(CCmdUI* pCmdUI);

// Implementation
public:
	virtual ~CDemoEditorView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CDemoEditorView)
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
	afx_msg void OnDestroy();
	afx_msg void OnUpdateEditUndo(CCmdUI* pCmdUI);
	afx_msg void OnUpdateEditCopy(CCmdUI* pCmdUI);
	afx_msg void OnUpdateEditPaste(CCmdUI* pCmdUI);
	afx_msg void OnUpdateEditCut(CCmdUI* pCmdUI);
	afx_msg BOOL OnSetCursor(CWnd* pWnd, UINT nHitTest, UINT message);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

#ifndef _DEBUG  // debug version in DemoEditorView.cpp
inline CDemoEditorDoc* CDemoEditorView::GetDocument()
   { return (CDemoEditorDoc*)m_pDocument; }
#endif

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_DEMOEDITORVIEW_H__38D0A65B_50E4_4E9F_9A7F_A754F386BEBA__INCLUDED_)
