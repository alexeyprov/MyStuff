#include "stdafx.h"
#include "cell.h"

int Cell::g_uses = 0;
std::map< int, HBRUSH> Cell::s_brush;

enum
{
	brWhite		= 1,
	brGray		= 2,
	brGreen		= 3,
	brRed		= 4,
	brBlue		= 5
};

Cell::Cell() 
	: m_state( stateFree )
	, m_posCode( 0 )
{
	CreateBrushes();

	++g_uses;
}

Cell::Cell( const Cell& cell )
	: m_state( cell.m_state )
	, m_posCode( cell.m_posCode )
	, m_rect( cell.m_rect )
{
	CreateBrushes();

	++g_uses;
}

Cell::~Cell()
{
	if ( --g_uses <= 0 && s_brush.size() > 0 )
	{
		std::map< int, HBRUSH >::iterator it = 0;

		for ( it = s_brush.begin(); it != s_brush.end(); it++ )
		{
			::DeleteObject( it->second );
		}
	}
}

// static
int Cell::CreatePosCode( int xpos, int ypos )
{
	int code = 0;

	code = xpos << 16;
	code += ypos;

	return code;
}

void Cell::DecodePosCode( int code, int *xpos, int *ypos )
{
	*xpos = code >> 16;
	*ypos = code << 16 >> 16;
}

// methods
void Cell::CreateBrushes()
{
	if ( s_brush.size() == 0 )
	{
		s_brush[ brGreen ]	= ::CreateSolidBrush( RGB( 0x00, 0xff, 0x00 ) );
		s_brush[ brRed ]	= ::CreateSolidBrush( RGB( 0xff, 0x10, 0x10 ) );
		s_brush[ brBlue ]	= ::CreateSolidBrush( RGB( 0x80, 0x80, 0xff ) );
	}
}

void Cell::Draw( HDC hdc )
{
//	int iBrush = ( m_state == stateFree || m_state == stateOff ) ? WHITE_BRUSH : LTGRAY_BRUSH;
	HGDIOBJ hBr = 0;

	switch ( m_state )
	{
		case stateFree:
		case stateOff:
			hBr = ::GetStockObject(WHITE_BRUSH);

			break;
		case statePrev:
			hBr = ::GetStockObject(LTGRAY_BRUSH);

			break;
		case stateBusy:
			hBr = s_brush[ brBlue ];

			break;
	}

	::SelectObject( hdc, ::GetStockObject( BLACK_PEN ) );
//	::SelectObject( hdc, ::GetStockObject( iBrush ) );
	::SelectObject( hdc, hBr );

	::Rectangle( hdc, m_rect.left, m_rect.top, m_rect.right, m_rect.bottom );

	if ( m_state == stateOff )
	{
		int cx = ((m_rect.right - m_rect.left) / 2) + m_rect.left;
		int cy = ((m_rect.bottom - m_rect.top) / 2) + m_rect.top;

		::SelectObject( hdc, s_brush[ brGreen ] );

		::Ellipse( hdc, cx - 5, cy - 5, cx + 5, cy + 5 );
	}
}

void Cell::SetRect( RECT &rect )
{
	m_rect = rect;
}

LPRECT Cell::GetRectPtr()
{
	return &m_rect;
}

void Cell::SetState( cellState newState )
{
	m_state = newState;
}

cellState Cell::GetState()
{
	return m_state;
}

void Cell::SetPosCode( int posCode )
{
	m_posCode = posCode;
}

int Cell::GetPosCode()
{
	return m_posCode;
}

void Cell::Reset()
{
	m_state = stateFree;
}

/////////////////////////////////////////////////////////////////////////////////
// ships implementation

Ship::Ship()
	: m_bIsLive( TRUE )
{
}

Ship::~Ship()
{
}

void Ship::AttachCell( Cell *pCell )
{
	m_cells.push_back( pCell );

	if ( IsCreated() )
	{
		std::vector< Cell* >::iterator itCell = NULL;

		for ( itCell = m_cells.begin(); itCell != m_cells.end(); itCell++ )
		{
			(*itCell)->SetState( stateBusy );
		}
	}
}

void Ship::FreeCells()
{
	std::vector< Cell* >::iterator itCell = NULL;

	for ( itCell = m_cells.begin(); itCell != m_cells.end(); itCell++ )
	{
		(*itCell)->Reset();
	}

	m_cells.clear();
}

BOOL Ship::IsLive()
{
	BOOL res = m_bIsLive;

	if ( res )
	{
		res = FALSE;
		std::vector< Cell* >::iterator itCell = NULL;

		for ( itCell = m_cells.begin(); itCell != m_cells.end(); itCell++ )
		{
			if ( (*itCell)->GetState() != stateDead )
			{
				res = TRUE;
				break;
			}
		}
	}

	return res;
}

BOOL Ship::IsCellInShip( Cell *pCell )
{
	BOOL res = FALSE;

	if ( std::find( m_cells.begin(), m_cells.end(), pCell ) != m_cells.end() )
		res = TRUE;

	return res;
}

// Battleship
Battleship::Battleship()
{
	m_type = shipBattleship;
}

BOOL Battleship::IsCreated()
{
	return ( m_cells.size() == 4 );
}

// Cruiser
Cruiser::Cruiser()
{
	m_type = shipCruiser;
}

BOOL Cruiser::IsCreated()
{
	return ( m_cells.size() == 3 );
}

// Destroyer
Destroyer::Destroyer()
{
	m_type = shipDestroyer;
}

BOOL Destroyer::IsCreated()
{
	return ( m_cells.size() == 2 );
}

// Torpedo
Torpedo::Torpedo()
{
	m_type = shipTorpedo;
}

BOOL Torpedo::IsCreated()
{
	return ( m_cells.size() == 1 );
}

