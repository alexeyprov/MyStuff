#include "stdafx.h"
#include "board.h"
#include "cell.h"

#define BACK_COLOR RGB(0xca, 0xca, 0xab)
HBRUSH InfoBoard::m_bkBrush = ::CreateSolidBrush(BACK_COLOR);

InfoBoard::InfoBoard()
	: m_currentShip( 0 )
{
	//m_bkColor = RGB( 0xca, 0xca, 0xab );
	//m_bkBrush = ::CreateSolidBrush( m_bkColor );
}

InfoBoard::~InfoBoard()
{
	//::DeleteObject( m_bkBrush );
}

int InfoBoard::OnCreate(LPCREATESTRUCT /*lpCreateStruct*/)
{
	SetIcon(::LoadIcon(NULL, IDI_APPLICATION));
	return 0;
}

void InfoBoard::OnDraw(HDC)
{
	PAINTSTRUCT ps = {0};
	HDC hdc = BeginPaint(&ps);
	DrawMessage(hdc);

	if ( m_currentShip > 0 )
	{
		DrawCurrentShip(hdc);
	}

	EndPaint(&ps);
}

LRESULT InfoBoard::OnTimer( UINT uTimerID, TIMERPROC )
{
	if ( uTimerID == timerMessage )
	{
		ShowMessage( m_lastMessage.c_str() );

		KillTimer( timerMessage );
	}

	return 0;
}

void InfoBoard::DrawMessage( HDC hdc )
{
	::SetBkColor( hdc, BACK_COLOR);
	::TextOut( hdc, 10, 75, m_message.c_str(), (int)m_message.length() );
}

void InfoBoard::ShowMessage( const char* message, BOOL bTmpMessage /*=FALSE*/ )
{
	if ( bTmpMessage )
	{
		m_lastMessage = m_message;

		SetTimer( timerMessage, 2000 );
	}

	SetMessage( message );

	Invalidate();
}

void InfoBoard::DrawCurrentShip( HDC hdc )
{
	::SetBkColor( hdc, BACK_COLOR);

	switch ( m_currentShip )
	{
		case shipTorpedo:		DrawTorpedo( hdc );
			break;
		case shipDestroyer:		DrawDestroyer( hdc );
			break;
		case shipCruiser:		DrawCruiser( hdc );
			break;
		case shipBattleship:	DrawBattleship( hdc );
			break;
	};
}

void InfoBoard::DrawBattleship( HDC hdc )
{
	const char msg[] = "your next ship is: battleship (4 cells)";
	::TextOut( hdc, 10, 10, msg, (int)strlen(msg) );
}

void InfoBoard::DrawCruiser( HDC hdc )
{
	const char msg[] = "your next ship is: cruiser (3 cells)";
	::TextOut( hdc, 10, 10, msg, (int)strlen(msg) );
}

void InfoBoard::DrawDestroyer( HDC hdc )
{
	const char msg[] = "your next ship is: destroyer (2 cells)";
	::TextOut( hdc, 10, 10, msg, (int)strlen(msg) );
}

void InfoBoard::DrawTorpedo( HDC hdc )
{
	const char msg[] = "your next ship is: torpedo boat (1 cell)";
	::TextOut( hdc, 10, 10, msg, (int)strlen(msg) );
}

