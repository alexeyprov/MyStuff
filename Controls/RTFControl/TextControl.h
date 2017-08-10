// TextControl.h : Declaration of the CTextControl

#ifndef __TEXTCONTROL_H_
#define __TEXTCONTROL_H_

#include "resource.h"       // main symbols
#include <atlctl.h>
#include <richedit.h>


/////////////////////////////////////////////////////////////////////////////
// CTextControl
class ATL_NO_VTABLE CTextControl : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CStockPropImpl<CTextControl, ITextControl, &IID_ITextControl, &LIBID_RTFCONTROLLib>,
	public CComControl<CTextControl>,
	public IPersistStreamInitImpl<CTextControl>,
	public IOleControlImpl<CTextControl>,
	public IOleObjectImpl<CTextControl>,
	public IOleInPlaceActiveObjectImpl<CTextControl>,
	public IViewObjectExImpl<CTextControl>,
	public IOleInPlaceObjectWindowlessImpl<CTextControl>,
	public IPersistStorageImpl<CTextControl>,
	public ISpecifyPropertyPagesImpl<CTextControl>,
	public IQuickActivateImpl<CTextControl>,
	public IDataObjectImpl<CTextControl>,
	public IProvideClassInfo2Impl<&CLSID_TextControl, NULL, &LIBID_RTFCONTROLLib>,
	public CComCoClass<CTextControl, &CLSID_TextControl>
{
public:
	CContainedWindow m_ctlRichEdit;
	

	CTextControl() :	
		m_ctlRichEdit(RICHEDIT_CLASS, this, 1)
	{
		m_bWindowOnly = TRUE;
	}

DECLARE_REGISTRY_RESOURCEID(IDR_TEXTCONTROL)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CTextControl)
	COM_INTERFACE_ENTRY(ITextControl)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(IViewObjectEx)
	COM_INTERFACE_ENTRY(IViewObject2)
	COM_INTERFACE_ENTRY(IViewObject)
	COM_INTERFACE_ENTRY(IOleInPlaceObjectWindowless)
	COM_INTERFACE_ENTRY(IOleInPlaceObject)
	COM_INTERFACE_ENTRY2(IOleWindow, IOleInPlaceObjectWindowless)
	COM_INTERFACE_ENTRY(IOleInPlaceActiveObject)
	COM_INTERFACE_ENTRY(IOleControl)
	COM_INTERFACE_ENTRY(IOleObject)
	COM_INTERFACE_ENTRY(IPersistStreamInit)
	COM_INTERFACE_ENTRY2(IPersist, IPersistStreamInit)
	COM_INTERFACE_ENTRY(ISpecifyPropertyPages)
	COM_INTERFACE_ENTRY(IQuickActivate)
	COM_INTERFACE_ENTRY(IPersistStorage)
	COM_INTERFACE_ENTRY(IDataObject)
	COM_INTERFACE_ENTRY(IProvideClassInfo)
	COM_INTERFACE_ENTRY(IProvideClassInfo2)
END_COM_MAP()

BEGIN_PROP_MAP(CTextControl)
	PROP_DATA_ENTRY("_cx", m_sizeExtent.cx, VT_UI4)
	PROP_DATA_ENTRY("_cy", m_sizeExtent.cy, VT_UI4)
	PROP_ENTRY("BackColor", DISPID_BACKCOLOR, CLSID_StockColorPage)
	PROP_ENTRY("FillColor", DISPID_FILLCOLOR, CLSID_StockColorPage)
	PROP_ENTRY("Text", DISPID_TEXT, CLSID_NULL)
	// Example entries
	// PROP_ENTRY("Property Description", dispid, clsid)
	// PROP_PAGE(CLSID_StockColorPage)
END_PROP_MAP()

BEGIN_MSG_MAP(CTextControl)
	MESSAGE_HANDLER(WM_CREATE, OnCreate)
	MESSAGE_HANDLER(WM_SETFOCUS, OnSetFocus)
	MESSAGE_HANDLER(WM_DESTROY, OnDestroy)
	CHAIN_MSG_MAP(CComControl<CTextControl>)
ALT_MSG_MAP(1)
	// Replace this with message map entries for superclassed RichEdit
END_MSG_MAP()
// Handler prototypes:
//  LRESULT MessageHandler(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
//  LRESULT CommandHandler(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
//  LRESULT NotifyHandler(int idCtrl, LPNMHDR pnmh, BOOL& bHandled);

	BOOL PreTranslateAccelerator(LPMSG pMsg, HRESULT& hRet)
	{
		if(pMsg->message == WM_KEYDOWN && 
			(pMsg->wParam == VK_LEFT || 
			pMsg->wParam == VK_RIGHT ||
			pMsg->wParam == VK_UP ||
			pMsg->wParam == VK_DOWN))
		{
			hRet = S_FALSE;
			return TRUE;
		}
		//TODO: Add your additional accelerator handling code here
		return FALSE;
	}

	LRESULT OnSetFocus(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
	{
		LRESULT lRes = CComControl<CTextControl>::OnSetFocus(uMsg, wParam, lParam, bHandled);
		if (m_bInPlaceActive)
		{
			DoVerbUIActivate(&m_rcPos,  NULL);
			if(!IsChild(::GetFocus()))
				m_ctlRichEdit.SetFocus();
		}
		return lRes;
	}

	HINSTANCE m_hLibRichEdit;

	LRESULT OnCreate(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/)
	{
		RECT rc;
		GetWindowRect(&rc);
		rc.right -= rc.left;
		rc.bottom -= rc.top;
		rc.top = rc.left = 0;
		m_hLibRichEdit = LoadLibrary(_T("RICHED32.DLL"));
		m_ctlRichEdit.Create(m_hWnd, rc);
		return 0;
	}
	LRESULT OnDestroy(UINT, WPARAM, LPARAM, BOOL&)
	{
		m_ctlRichEdit.DestroyWindow();
		FreeLibrary(m_hLibRichEdit);
		return 0;
	}
	STDMETHOD(SetObjectRects)(LPCRECT prcPos,LPCRECT prcClip)
	{
		IOleInPlaceObjectWindowlessImpl<CTextControl>::SetObjectRects(prcPos, prcClip);
		int cx, cy;
		cx = prcPos->right - prcPos->left;
		cy = prcPos->bottom - prcPos->top;
		::SetWindowPos(m_ctlRichEdit.m_hWnd, NULL, 0,
			0, cx, cy, SWP_NOZORDER | SWP_NOACTIVATE);
		return S_OK;
	}

// IViewObjectEx
	DECLARE_VIEW_STATUS(VIEWSTATUS_SOLIDBKGND | VIEWSTATUS_OPAQUE)

// ITextControl
public:
	OLE_COLOR m_clrBackColor;
	OLE_COLOR m_clrFillColor;
	CComBSTR m_bstrText;
};

#endif //__TEXTCONTROL_H_
