#include "stdafx.h"
#include "cell.h"
#include "workspace.h"
#include "game.h"
#include "board.h"
#include "check.h"

int CMainWnd::OnCreate(LPCREATESTRUCT /*lpCreateStruct*/)
{
	HWND hwnd;

	hwnd = ::CreateWindow( "BUTTON", "Exit", WS_CHILD | WS_VISIBLE, 660, 460, 86, 25, m_hWnd, (HMENU)IDC_EXIT, _Module.m_hInst, 0 );
	hwnd = ::CreateWindow( "BUTTON", "Reset game", WS_CHILD | WS_VISIBLE, 20, 460, 86, 25, m_hWnd, (HMENU)IDC_RESETGAME, _Module.m_hInst, 0 );

	CRect rect(10, 320, 300, 420);
	m_board.Create(m_hWnd, _U_RECT(rect), LPCTSTR(NULL), WS_CHILD | WS_BORDER);
	DWORD dw = ::GetLastError();
	m_board.ShowWindow( SW_SHOW );
	m_board.UpdateWindow();

	m_params.Create(m_hWnd);
	m_params.GetWindowRect(&rect);
	rect.MoveToXY(320, 310);
	m_params.MoveWindow(rect);

	m_game.Init(m_hWnd);
	m_board.SetMessage( "place your ships" );

	SetIcon(::LoadIcon(NULL, IDI_APPLICATION));

	/*CMessageLoop* pLoop = _Module.GetMessageLoop();
	pLoop->AddMessageFilter(this);
	pLoop->AddIdleHandler(this);*/

	return 0;
}

BOOL CMainWnd::OnGameReady(UINT, WPARAM, LPARAM, BOOL&)
{
	m_board.SetCurrentShip( 0 );
	m_board.SetMessage( "connect to opponent" );
	m_board.Invalidate();

	return TRUE;
}

BOOL CMainWnd::OnNextShipCreate(UINT, WPARAM wParam, LPARAM, BOOL&)
{
	m_board.SetCurrentShip( (int)wParam );
	m_board.Invalidate();

	return TRUE;
}

BOOL CMainWnd::OnWrongShipCell(UINT, WPARAM, LPARAM, BOOL&)
{
	m_board.ShowMessage( "this cell can not be used!", TRUE );

	return TRUE;
}

BOOL CMainWnd::OnNoGame(UINT, WPARAM, LPARAM, BOOL&)
{
	m_board.ShowMessage( "not all of ships were created", TRUE );

	return TRUE;
}

BOOL CMainWnd::OnMyTurn(UINT, WPARAM, LPARAM, BOOL&)
{
	m_board.ShowMessage( "Please wait your opponent's turn...", TRUE );

	return TRUE;
}

void CParams::OnConnect_Click(UINT uNotifyCode, int nID, CWindow wnd)
{
	((CMainWnd*)m_pParent)->OnConnect_Click(uNotifyCode, nID, wnd);
}

void CMainWnd::OnConnect_Click(UINT /*uNotifyCode*/, int /*nID*/, CWindow /*wnd*/)
{
	m_board.ShowMessage( "connected" );
}

void CMainWnd::OnResetGame_Click(UINT /*uNotifyCode*/, int /*nID*/, CWindow /*wnd*/)
{
	m_game.Reset();
	m_board.ShowMessage( "place your ships" );
}

void CMainWnd::OnExit_Click(UINT /*uNotifyCode*/, int /*nID*/, CWindow /*wnd*/)
{
	PostMessage( WM_CLOSE );
}

void CMainWnd::OnDestroy()
{
	PostQuitMessage( 0 );
}

CAppModule _Module;


int Run(LPTSTR /*lpstrCmdLine*/ = NULL, int nCmdShow = SW_SHOWDEFAULT)
{
	CMessageLoop theLoop;
	_Module.AddMessageLoop(&theLoop);

	CMainWnd wndMain;

	if (!wndMain.Create(NULL, NULL, _T("Network sea battle")))
	{
		ATLTRACE(_T("Main window creation failed!\n"));
		return 0;
	}

	wndMain.ShowWindow(nCmdShow);

	int nRet = theLoop.Run();

	_Module.RemoveMessageLoop();
	return nRet;
}

int WINAPI _tWinMain(HINSTANCE hInstance, HINSTANCE /*hPrevInstance*/, LPTSTR lpstrCmdLine, int nCmdShow)
{
	HRESULT hRes = _Module.Init(NULL, hInstance);
	hRes;
	ATLASSERT(SUCCEEDED(hRes));

	int nRet = Run(lpstrCmdLine, nCmdShow);

	_Module.Term();
	return nRet;
}