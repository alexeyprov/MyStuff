#include "stdafx.h"
#include "cell.h"
#include "workspace.h"
#include "game.h"
#include "board.h"
#include "check.h"

HINSTANCE g_hInstance;

BOOL CMainWnd::OnGameReady( WPARAM wParam, LPARAM lParam )
{
	m_board.SetCurrentShip( 0 );
	m_board.SetMessage( "connect to opponent" );
	m_board.Repaint();

	return TRUE;
}

BOOL CMainWnd::OnNextShipCreate( WPARAM wParam, LPARAM lParam )
{
	m_board.SetCurrentShip( (int)wParam );
	m_board.Repaint();

	return TRUE;
}

BOOL CMainWnd::OnWrongShipCell( WPARAM wParam, LPARAM lParam )
{
	m_board.ShowMessage( "this cell can not be used!", TRUE );

	return TRUE;
}

BOOL CMainWnd::OnNoGame( WPARAM wParam, LPARAM lParam )
{
	m_board.ShowMessage( "not all of ships were created", TRUE );

	return TRUE;
}

BOOL CMainWnd::OnMyTurn( WPARAM wParam, LPARAM lParam )
{
	m_board.ShowMessage( "Please wait your opponent's turn...", TRUE );

	return TRUE;
}

void CParams::OnConnect_Click()
{
	((CMainWnd*)m_pParent)->OnConnect_Click();
}

void CMainWnd::OnConnect_Click()
{
	m_board.ShowMessage( "connected" );
}

void CMainWnd::OnResetGame_Click()
{
	m_game.Reset();

	m_board.ShowMessage( "place your ships" );
}

void CMainWnd::OnExit_Click()
{
	PostMessage( WM_CLOSE );
}

void CMainWnd::CreateControls()
{
	HWND hwnd;

	hwnd = ::CreateWindow( "BUTTON", "Exit", WS_CHILD | WS_VISIBLE, 660, 460, 86, 25, GetHwnd(), (HMENU)IDC_EXIT, g_hInstance, 0 );
	hwnd = ::CreateWindow( "BUTTON", "Reset game", WS_CHILD | WS_VISIBLE, 20, 460, 86, 25, GetHwnd(), (HMENU)IDC_RESETGAME, g_hInstance, 0 );

	RECT rect = { 10, 320, 300, 420 };
	m_board.Create( GetHinstance(), GetHwnd(), WS_CHILD | WS_BORDER, "InfoBoard", 0, &rect );
	m_board.ShowWindow( SW_SHOW );
	m_board.UpdateWindow();

	POINT sp = { 320, 310 };
	m_params.Create( GetHinstance(), GetHwnd(), IDD_DIALOG1, 1001, WS_CHILD, sp );
	m_params.ShowWindow( SW_SHOW );
	m_params.UpdateWindow();

	m_game.Init( GetHwnd() );
	m_board.SetMessage( "place your ships" );
}

void CMainWnd::PreRegisterWindow( WNDCLASS *pWndclass )
{
	CWindow::PreRegisterWindow( pWndclass );

	pWndclass->hIcon			= ::LoadIcon( NULL, IDI_APPLICATION );
	pWndclass->hbrBackground	= (HBRUSH)COLOR_WINDOW;
}

LRESULT CMainWnd::OnDestroy()
{
	PostQuitMessage( 0 );

	return 0;
}

int __stdcall WinMain( HINSTANCE hInst, HINSTANCE hPrev, LPSTR lpCmdLine, int nCmdShow )
{
	g_hInstance = hInst;

	MSG msg;

	CMainWnd mainWnd;

	try
	{
		mainWnd.Create( g_hInstance, NULL, WS_OVERLAPPEDWINDOW | WS_DLGFRAME,
			"MainWndClass", "Network sea battle", 0 );

		mainWnd.CreateControls();

		mainWnd.ShowWindow( nCmdShow );
		mainWnd.UpdateWindow();
	}
	catch (std::exception &e)
	{
		::MessageBox( mainWnd.GetHwnd(), e.what(), "error message", MB_OK );

		mainWnd.PostMessage( WM_CLOSE );
	}

	int nRet = 0;

	while ( (nRet = GetMessage(&msg, NULL, 0, 0)) != 0 ) 
    { 
		if ( nRet == -1 )
		{
			mainWnd.Destroy();

			return FALSE;
		}
		else
		{
			TranslateMessage(&msg); 
			DispatchMessage(&msg); 
		}
    } 


	return 0;
}

