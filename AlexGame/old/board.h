#pragma once

class InfoBoard : public CWindow
{
	enum enuTimers
	{
		timerMessage	= 1
	};

	COLORREF m_bkColor;
	HBRUSH m_bkBrush;
	std::string m_message;
	std::string m_lastMessage;
	int m_currentShip;

	virtual void PreRegisterWindow( WNDCLASS *pWndclass );
	virtual void OnDraw( HDC hdc );
	virtual LRESULT OnTimer( UINT uTimerID );

	void DrawCurrentShip( HDC hdc );
	void DrawBattleship( HDC hdc );
	void DrawCruiser( HDC hdc );
	void DrawDestroyer( HDC hdc );
	void DrawTorpedo( HDC hdc );
	void DrawMessage( HDC hdc );
public:
	InfoBoard();
	~InfoBoard();

	inline void SetMessage( const char *message )
	{
		m_message = message;
	}

	inline void SetCurrentShip( int ship )
	{
		m_currentShip = ship;
	}

	void ShowMessage( const char* message, BOOL bTmpMessage = FALSE );
};

