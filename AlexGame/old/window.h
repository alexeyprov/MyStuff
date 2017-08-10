#pragma once

#define BEGIN_COMMAND_MAP	\
	virtual void ProcessCommandMap( UINT uID ) \
	{	\
		switch ( uID ) {
#define END_COMMAND_MAP } };
#define COMMAND_ENTRY( id, pfn )	\
		case id: pfn();	\
			break;

#define BEGIN_MESSAGE_MAP	\
	virtual BOOL PreProcessMessage( UINT uMsg, WPARAM wParam, LPARAM lParam )	\
	{	BOOL res = FALSE; \
		switch ( uMsg ) {
#define END_MESSAGE_MAP	} return res; };
#define MESSAGE_ENTRY( msg, pfn )	\
		case msg: res = pfn( wParam, lParam );	\
			break;

class CWindow
{
	HINSTANCE m_hInstance;
	HWND m_hwndParent;
	HWND m_hwnd;
	char m_wndclass[0xff];

	static INT_PTR _stdcall DlgProc( HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam );
	static LRESULT __stdcall WndProc( HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam );
	static BOOL RegisterWndClass( CWindow *pThis );

public:
	CWindow();
	~CWindow();

	void Create( HINSTANCE hInstance, HWND hwndParent, DWORD dwStyle, const char* wndclass, 
					const char* header, const RECT *pRect );
	void Create( HINSTANCE hInstance, HWND hwndParent, UINT resID, UINT iid, DWORD dwStyle, POINT &startPoint );
	DWORD GetWindowStyle();
	void SetWindowStyle( DWORD dwStyle );
	void Destroy();
	BOOL ShowWindow( int cmdShow );
	BOOL UpdateWindow();
	BOOL MoveWindow( int x, int y, int width, int height, BOOL bRepaint );
	BOOL MoveWindow( RECT &rect, BOOL bRepaint );
	void Repaint();
	UINT SetTimer( UINT uTimerID, UINT uElapse );
	BOOL KillTimer( UINT uTimerID );

	inline HWND GetHwnd()
	{
		return m_hwnd;
	}

	inline HWND GetParentHwnd()
	{
		return m_hwndParent;
	}

	inline HINSTANCE GetHinstance()
	{
		return m_hInstance;
	}

	inline BOOL PostMessage( UINT message, WPARAM wParam = 0, LPARAM lParam = 0 )
	{
		return ::PostMessage( m_hwnd, message, wParam, lParam );
	}

protected:
	virtual BOOL PreProcessMessage( UINT uMsg, WPARAM wParam, LPARAM lParam );
	virtual void ProcessCommandMap( UINT uCtrlID );
	virtual void PreRegisterWindow( WNDCLASS *pWndclass );
	virtual LRESULT OnTimer( UINT uTimerID );
	virtual LRESULT OnCommand( WORD wCode, UINT uCtrlID, HWND hwndCtrl );
	virtual LRESULT OnCreate( LPCREATESTRUCT crst );
	virtual LRESULT OnPaint();
	virtual void OnDraw( HDC hdc );
	virtual LRESULT OnSize( DWORD dwResizingFlag, WORD width, WORD height );
	virtual LRESULT OnDestroy();
	virtual LRESULT OnRButtonDown( DWORD dwKeyIndicator, WORD x, WORD y );
	virtual LRESULT OnLButtonDown( DWORD dwKeyIndicator, WORD x, WORD y );
};



