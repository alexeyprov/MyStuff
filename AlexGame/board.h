#pragma once

class InfoBoard : public CWindowImpl<InfoBoard>
{
	enum enuTimers
	{
		timerMessage	= 1
	};

	//COLORREF m_bkColor;
	static HBRUSH m_bkBrush;
	std::string m_message;
	std::string m_lastMessage;
	int m_currentShip;

	void OnDraw( HDC hdc );
	LRESULT OnTimer(UINT uTimerID, TIMERPROC);
	int OnCreate(LPCREATESTRUCT /*lpCreateStruct*/);

	void DrawCurrentShip( HDC hdc );
	void DrawBattleship( HDC hdc );
	void DrawCruiser( HDC hdc );
	void DrawDestroyer( HDC hdc );
	void DrawTorpedo( HDC hdc );
	void DrawMessage( HDC hdc );

	BEGIN_MSG_MAP_EX(InfoBoard)
		MSG_WM_PAINT(OnDraw)
		MSG_WM_TIMER(OnTimer)
		MSG_WM_CREATE(OnCreate)
	END_MSG_MAP()

public:

	DECLARE_WND_CLASS_EX(_T("InfoBoard"), CS_HREDRAW | CS_VREDRAW, m_bkBrush - 1)

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

