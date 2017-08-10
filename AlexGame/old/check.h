#pragma once

#pragma warning( disable : 4355 )	// 'this' in constructor warning

extern HINSTANCE g_hInstance;

class CParams : public CWindow
{
	CWindow *m_pParent;

	BEGIN_COMMAND_MAP
		COMMAND_ENTRY( IDC_CONNECT, OnConnect_Click )
	END_COMMAND_MAP
public:
	CParams( CWindow *pParent ) : m_pParent( pParent )
	{
	}

protected:
	void OnConnect_Click();
};

class CMainWnd : public CWindow
{
	InfoBoard m_board;
	CParams m_params;
	Game m_game;

	BOOL OnGameReady( WPARAM wParam, LPARAM lParam );
	BOOL OnNextShipCreate( WPARAM wParam, LPARAM lParam );
	BOOL OnWrongShipCell( WPARAM wParam, LPARAM lParam );
	BOOL OnNoGame( WPARAM wParam, LPARAM lParam );
	BOOL OnMyTurn( WPARAM wParam, LPARAM lParam );
	virtual void PreRegisterWindow( WNDCLASS *pWndclass );
	virtual LRESULT OnDestroy();

	BEGIN_MESSAGE_MAP
		MESSAGE_ENTRY( WM_GAME_MYTURN, OnMyTurn )
		MESSAGE_ENTRY( WM_GAME_NOGAME, OnNoGame )
		MESSAGE_ENTRY( WM_GAME_WRONG_SHIP_CELL, OnWrongShipCell )
		MESSAGE_ENTRY( WM_GAME_NEXTSHIP_CREATE, OnNextShipCreate )
		MESSAGE_ENTRY( WM_GAME_READY, OnGameReady )
	END_MESSAGE_MAP

	BEGIN_COMMAND_MAP
		COMMAND_ENTRY( IDC_EXIT, OnExit_Click )
		COMMAND_ENTRY( IDC_RESETGAME, OnResetGame_Click )
	END_COMMAND_MAP
public:
	CMainWnd() : m_params( this )
	{
	}

	void OnResetGame_Click();
	void OnConnect_Click();
	void OnExit_Click();
	void CreateControls();
};

