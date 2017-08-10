#include <windows.h>
#include <stdexcept>
#include <vector>
#include <list>
#include <algorithm>
#include "window.h"

#pragma warning( disable: 4311 4312 )

enum
{
	WM_SKIP_MSG		= WM_USER + 1000
};

CWindow::CWindow() 
	: m_hInstance( NULL)
	, m_hwnd( 0 )
	, m_hwndParent( 0 )
{
	memset( m_wndclass, 0, sizeof( m_wndclass ) );
}

CWindow::~CWindow()
{
}

BOOL CWindow::RegisterWndClass( CWindow *pThis )
{
	WNDCLASS wcx = {0};

	pThis->PreRegisterWindow( &wcx );

	if ( ::GetClassInfo( pThis->m_hInstance, wcx.lpszClassName, &wcx ))
		return TRUE;

	return ::RegisterClass( &wcx );
}

void CWindow::Create( HINSTANCE hInstance, HWND hwndParent, DWORD dwStyle, const char* wndclass, 
						const char* header, const RECT *pRect )
{
	if ( m_hwnd )
		throw std::runtime_error( "window has been already created" );

	m_hwndParent = hwndParent;
	m_hInstance = hInstance;

	if ( wndclass )
	{
		strcpy( m_wndclass, wndclass );
	}

	if ( !RegisterWndClass( this ) )
		throw std::runtime_error( "could not register window class" );

	int x		= (pRect) ? pRect->left : CW_USEDEFAULT;
	int y		= (pRect) ? pRect->top : CW_USEDEFAULT;
	int width	= (pRect) ? pRect->right - pRect->left : CW_USEDEFAULT;
	int height	= (pRect) ? pRect->bottom - pRect->top : 0;

	m_hwnd = ::CreateWindow(	m_wndclass,
								header,
								dwStyle,
								x,
								y,
								width,
								height,
								hwndParent,
								NULL,
								m_hInstance,
								this );
	if ( m_hwnd == NULL )
	{
		char buf[0xff] = {0};
		sprintf( buf, "could not create window, error code: %d", ::GetLastError() );

		throw std::runtime_error( buf );
	}
}

void CWindow::Create( HINSTANCE hInstance, HWND hwndParent, UINT resID, UINT iid, DWORD dwStyle, POINT &startPoint )
{
	if ( m_hwnd )
		throw std::runtime_error( "window has been already created" );

	m_hwndParent = hwndParent;
	m_hInstance = hInstance;

	m_hwnd = ::CreateDialogParam( hInstance, MAKEINTRESOURCE( resID ), hwndParent, DlgProc, (LPARAM)this );

	if ( m_hwnd == NULL )
	{
		char buf[0xff] = {0};
		sprintf( buf, "could not create window, error code: %d", ::GetLastError() );

		throw std::runtime_error( buf );
	}

	RECT locRect = {0};
	::GetWindowRect( m_hwnd, &locRect );

	SetWindowStyle( dwStyle );
	::SetWindowLong( m_hwnd, GWL_ID, iid );
	::SetParent( m_hwnd, hwndParent );

	MoveWindow( startPoint.x, startPoint.y, locRect.right - locRect.left, locRect.bottom - locRect.top, TRUE );
}

void CWindow::Destroy()
{
	::DestroyWindow( m_hwnd );
}

BOOL CWindow::ShowWindow( int cmdShow )
{
	return ::ShowWindow( m_hwnd, cmdShow );
}

BOOL CWindow::UpdateWindow()
{
	return ::UpdateWindow( m_hwnd );
}

void CWindow::SetWindowStyle( DWORD dwStyle )
{
	::SetWindowLong( m_hwnd, GWL_STYLE, dwStyle );
}

DWORD CWindow::GetWindowStyle()
{
	return ::GetWindowLong( m_hwnd, GWL_STYLE );
}

BOOL CWindow::MoveWindow( int x, int y, int width, int height, BOOL bRepaint )
{
	return ::MoveWindow( m_hwnd, x, y, width, height, bRepaint );
}

BOOL CWindow::MoveWindow( RECT &rect, BOOL bRepaint )
{
	return MoveWindow( rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top, bRepaint );
}

void CWindow::Repaint()
{
	::InvalidateRect( m_hwnd, NULL, TRUE );
	::SendMessage( m_hwnd, WM_PAINT, 0, 0 );
}

UINT CWindow::SetTimer( UINT uTimerID, UINT uElapse )
{
	return (UINT)::SetTimer( m_hwnd, uTimerID, uElapse, NULL );
}

BOOL CWindow::KillTimer( UINT uTimerID )
{
	return ::KillTimer( m_hwnd, uTimerID );
}

INT_PTR _stdcall CWindow::DlgProc( HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam )
{
	return CWindow::WndProc( hwnd, uMsg, wParam, lParam );
}

LRESULT __stdcall CWindow::WndProc( HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam )
{
	LRESULT res = 0;

	CWindow *pThis = 0;
	if ( uMsg != WM_CREATE && uMsg != WM_INITDIALOG )
	{
		pThis = (CWindow*)::GetWindowLong( hwnd, GWL_USERDATA );
	}
	else if ( uMsg == WM_INITDIALOG )
	{
		pThis = (CWindow*)lParam;
	}
	else
	{
		pThis = (CWindow*)((LPCREATESTRUCT)lParam)->lpCreateParams;
	}

	if ( pThis && pThis->PreProcessMessage( uMsg, wParam, lParam ) )
	{
		uMsg = WM_SKIP_MSG;
	}

	switch ( uMsg )
	{
		case WM_SKIP_MSG:
			break;
		case WM_CREATE:
		case WM_INITDIALOG:
			{
				::SetWindowLong( hwnd, GWL_USERDATA, reinterpret_cast<long>( pThis ) );

				res = pThis->OnCreate( (LPCREATESTRUCT)lParam );
			}
			break;
		case WM_SIZE:
			res = pThis->OnSize( (DWORD)wParam, LOWORD(lParam), HIWORD(lParam) );

			break;
		case WM_PAINT:
			res = pThis->OnPaint();

			break;
		case WM_TIMER:
			res = pThis->OnTimer( (UINT)wParam );

			break;
		case WM_COMMAND:
			res = pThis->OnCommand( HIWORD(wParam), LOWORD(wParam), (HWND)lParam );

			break;
		case WM_LBUTTONDOWN:
			res = pThis->OnLButtonDown( (DWORD)wParam, LOWORD(lParam), HIWORD(lParam) );

			break;
		case WM_RBUTTONDOWN:
			res = pThis->OnRButtonDown( (DWORD)wParam, LOWORD(lParam), HIWORD(lParam) );

			break;
		case WM_CLOSE:
			::DestroyWindow( hwnd );

		case WM_DESTROY:
			res = pThis->OnDestroy();

			break;

			break;
		default:
			return ::DefWindowProc( hwnd, uMsg, wParam, lParam );
	}

	return res;
}

BOOL CWindow::PreProcessMessage( UINT uMsg, WPARAM wParam, LPARAM lParam )
{
	return FALSE;
}

void CWindow::PreRegisterWindow( WNDCLASS *pWndclass )
{
	pWndclass->style			= 0;//CS_HREDRAW | CS_VREDRAW;
	pWndclass->lpfnWndProc		= CWindow::WndProc;
	pWndclass->cbClsExtra		= 0;
	pWndclass->cbWndExtra		= 0;
	pWndclass->hInstance		= m_hInstance;
	pWndclass->hCursor			= ::LoadCursor( NULL, IDC_ARROW );
	pWndclass->hbrBackground	= (HBRUSH)::GetStockObject( WHITE_BRUSH );
	pWndclass->lpszMenuName		= NULL;
	pWndclass->lpszClassName	= m_wndclass;
}

LRESULT CWindow::OnCreate( LPCREATESTRUCT crst )
{
	return 0;
}

LRESULT CWindow::OnPaint()
{
	RECT rect = {0};
	BOOL bUpdateRect = ::GetUpdateRect( m_hwnd, &rect, FALSE );
	PAINTSTRUCT ps = {0};

	if ( bUpdateRect )
		BeginPaint( m_hwnd, &ps );

	OnDraw( ps.hdc );

	if ( bUpdateRect )
		EndPaint( m_hwnd, &ps );

	return 0;
}

void CWindow::OnDraw( HDC hdc )
{
}

LRESULT CWindow::OnLButtonDown( DWORD dwKeyIndicator, WORD x, WORD y )
{
	return 0;
}

LRESULT CWindow::OnRButtonDown( DWORD dwKeyIndicator, WORD x, WORD y )
{
	return 0;
}

LRESULT CWindow::OnDestroy()
{
	return 0;
}

LRESULT CWindow::OnSize( DWORD dwResizingFlag, WORD width, WORD height )
{
	return 0;
}

LRESULT CWindow::OnTimer( UINT uTimerID )
{
	return 0;
}

LRESULT CWindow::OnCommand( WORD wCode, UINT uCtrlID, HWND hwndCtrl )
{
	ProcessCommandMap( uCtrlID );

	return 0;
}

void CWindow::ProcessCommandMap( UINT uCtrlID )
{
}

