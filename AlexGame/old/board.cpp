#include "stdafx.h"
#include "board.h"
#include "cell.h"

InfoBoard::InfoBoard()
	: m_currentShip( 0 )
{
	m_bkColor = RGB( 0xca, 0xca, 0xab );
	m_bkBrush = ::CreateSolidBrush( m_bkColor );
}

InfoBoard::~InfoBoard()
{
	::DeleteObject( m_bkBrush );
}

void InfoBoard::PreRegisterWindow( WNDCLASS *pWndclass )
{
	CWindow::PreRegisterWindow( pWndclass );

	pWndclass->hIcon			= ::LoadIcon( NULL, IDI_APPLICATION );
	pWndclass->hbrBackground	= m_bkBrush;
}

void InfoBoard::OnDraw( HDC hdc )
{
	DrawMessage( hdc );

	if ( m_currentShip > 0 )
	{
		DrawCurrentShip( hdc );
	}
}

LRESULT InfoBoard::OnTimer( UINT uTimerID )
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
	::SetBkColor( hdc, m_bkColor );
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

	Repaint();
}

void InfoBoard::DrawCurrentShip( HDC hdc )
{
	::SetBkColor( hdc, m_bkColor );

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

