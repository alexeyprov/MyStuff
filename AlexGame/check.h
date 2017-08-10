#pragma once

#pragma warning( disable : 4355 )	// 'this' in constructor warning

class CParams : public CDialogImpl<CParams>
{
	const CWindow *m_pParent;

	BEGIN_MSG_MAP_EX(CParams)
		COMMAND_ID_HANDLER_EX( IDC_CONNECT, OnConnect_Click )
	END_MSG_MAP()
public:
	enum { IDD = IDD_DIALOG1};

	CParams(const CWindow *pParent ) : m_pParent( pParent )
	{
	}

protected:
	void OnConnect_Click(UINT /*uNotifyCode*/, int /*nID*/, CWindow /*wnd*/);
};

class CMainWnd : public CWindowImpl<CMainWnd, CWindow, CFrameWinTraits>
{
	InfoBoard m_board;
	CParams m_params;
	Game m_game;

	BOOL OnGameReady(UINT, WPARAM, LPARAM, BOOL&);
	BOOL OnNextShipCreate(UINT, WPARAM, LPARAM, BOOL&);
	BOOL OnWrongShipCell(UINT, WPARAM, LPARAM, BOOL&);
	BOOL OnNoGame(UINT, WPARAM, LPARAM, BOOL&);
	BOOL OnMyTurn(UINT, WPARAM, LPARAM, BOOL&);

	void OnDestroy();
	int OnCreate(LPCREATESTRUCT /*lpCreateStruct*/);

	BEGIN_MSG_MAP_EX(CMainWnd)
		MSG_WM_CREATE(OnCreate)
		MSG_WM_DESTROY(OnDestroy);
		MESSAGE_HANDLER( WM_GAME_MYTURN, OnMyTurn )
		MESSAGE_HANDLER( WM_GAME_NOGAME, OnNoGame )
		MESSAGE_HANDLER( WM_GAME_WRONG_SHIP_CELL, OnWrongShipCell )
		MESSAGE_HANDLER( WM_GAME_NEXTSHIP_CREATE, OnNextShipCreate )
		MESSAGE_HANDLER( WM_GAME_READY, OnGameReady )
		COMMAND_ID_HANDLER_EX( IDC_EXIT, OnExit_Click )
		COMMAND_ID_HANDLER_EX( IDC_RESETGAME, OnResetGame_Click )
		COMMAND_ID_HANDLER_EX( IDC_CONNECT, OnConnect_Click )
	END_MSG_MAP()
public:
	DECLARE_WND_CLASS_EX(_T("Sea Battle"), CS_HREDRAW | CS_VREDRAW, COLOR_WINDOW - 1)

	CMainWnd() : m_params( this )
	{
	}

	void OnResetGame_Click(UINT /*uNotifyCode*/, int /*nID*/, CWindow /*wnd*/);
	void OnConnect_Click(UINT /*uNotifyCode*/, int /*nID*/, CWindow /*wnd*/);
	void OnExit_Click(UINT /*uNotifyCode*/, int /*nID*/, CWindow /*wnd*/);
};

