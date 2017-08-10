#include "stdafx.h"
#include "cell.h"
#include "workspace.h"
#include "game.h"

#pragma warning( disable: 4311 4312 )

Workspace::Workspace()
	: m_pGame( NULL )
{
}

Workspace::~Workspace()
{
}

int Workspace::OnCreate( LPCREATESTRUCT crst )
{
	CreateCells();

	return 0;
}

void Workspace::OnDraw(HDC)
{
	PAINTSTRUCT ps = {0};
	HDC hdc = BeginPaint(&ps);

	DrawRulers(hdc);
	DrawCells(hdc);

	EndPaint(&ps);
}

LRESULT Workspace::OnLButtonDown( DWORD dwKeyIndicator, CPoint pt)
{
	std::vector<Cell>::iterator itCell = 0;

	itCell = std::find_if( m_cells.begin(), m_cells.end(), FindCellByPointPred( pt ) );
	if ( itCell != m_cells.end() )
	{
		if ( OnCellLClicked( itCell.base() ) )
			::InvalidateRect( m_hWnd, itCell->GetRectPtr(), FALSE );
	}

	return 0;
}

LRESULT Workspace::OnRButtonDown( DWORD dwKeyIndicator, CPoint pt)
{
	std::vector<Cell>::iterator itCell = 0;

	itCell = std::find_if( m_cells.begin(), m_cells.end(), FindCellByPointPred( pt ) );
	if ( itCell != m_cells.end() )
	{
		if ( OnCellRClicked( itCell.base() ) )
			::InvalidateRect( m_hWnd, itCell->GetRectPtr(), FALSE );
	}

	return 0;
}

void Workspace::CreateCells()
{
	RECT rect = {0};
	Cell cell;

	rect.left += shiftX;
	rect.right = rectSize + shiftX;
	rect.top += shiftY;
	rect.bottom = rectSize + shiftY;

	for ( int col = 0; col < maxCols; col++ )
	{
		for ( int row = 0; row < maxRows; row++ )
		{
			cell.SetRect( rect );
			cell.SetPosCode( Cell::CreatePosCode( col, row ) );

			m_cells.push_back( cell );

			rect.top += rectSize;
			rect.bottom += rectSize;
		}

		rect.top = shiftY;
		rect.bottom = rectSize + shiftY;
		rect.left += rectSize;
		rect.right += rectSize;
	}
}

void Workspace::DrawCells( HDC hdc )
{
	std::vector<Cell>::iterator itCell = NULL;

	for ( itCell = m_cells.begin(); itCell != m_cells.end(); itCell++ )
	{
		itCell->Draw( hdc );
	}
}

void Workspace::DrawRulers( HDC hdc )
{
	char ch[3] = {0};
	int x = shiftX + 5;
	int y = shiftY - 15;

	for ( int col = 0; col < maxCols; col++ )
	{
		itoa( col + 1, ch, 10 );

		TextOut( hdc, x, y, ch, (int)strlen(ch) );

		x += rectSize;
	}

	memset( ch, 0, 2 );
	ch[0] = 'A';
	x = 5;
	y = shiftY + 5;

	for ( int row = 0; row < maxRows; row++, ch[0]++ )
	{
		TextOut( hdc, x, y, ch, 1 );

		y += rectSize;
	}
}

void Workspace::Reset()
{
	std::vector<Cell>::iterator itCell = NULL;

	for ( itCell = m_cells.begin(); itCell != m_cells.end(); itCell++ )
	{
		itCell->Reset();
	}
}

void Workspace::BindToGame( Game *pGame )
{
	m_pGame = pGame;
}

void Workspace::GetCell( int col, int row, Cell **pCell )
{
	*pCell = NULL;

	std::vector<Cell>::iterator itCell = 0;
	int code = Cell::CreatePosCode( col, row );

	itCell = std::find_if( m_cells.begin(), m_cells.end(), FindCellByCodePred( code ) );
	if ( itCell != m_cells.end() )
	{
		*pCell = itCell.base();
	}
}

void Workspace::GetNearestCells( const Cell *pCell, CellPtrVec *pvec )
{
	int col = 0;
	int row = 0;
	CellVec::iterator itCell = NULL;

	Cell::DecodePosCode( const_cast<Cell*>(pCell)->GetPosCode(), &col, &row );

	int maxcol = col + 1;
	int maxrow = row + 1;

	for ( int c = col - 1; c <= maxcol; c++ )
	{
		for ( int r = row - 1; r <= maxrow; r++ )
		{
			if ( ( c < 0 || r < 0 ) || ( c == col && r == row ) || ( c > maxCols || r > maxRows ) )
				continue;

			int code = Cell::CreatePosCode( c, r );

			itCell = std::find_if( m_cells.begin(), m_cells.end(), FindCellByCodePred( code ) );
			if ( itCell != m_cells.end() )
			{
				if ( itCell->GetState() == stateFree )
					pvec->push_back( itCell.base() );
			}
		}
	}
}

BOOL Workspace::IsCellAvaiable( const Cell *pCell )
{
	BOOL res = TRUE;
	int col = 0;
	int row = 0;
	CellVec::iterator itCell = NULL;

	Cell::DecodePosCode( const_cast<Cell*>(pCell)->GetPosCode(), &col, &row );

	int maxcol = col + 1;
	int maxrow = row + 1;

	for ( int c = col - 1; c <= maxcol; c++ )
	{
		for ( int r = row - 1; r <= maxrow; r++ )
		{
			if ( ( c < 0 || r < 0 ) || ( c == col && r == row ) || ( c > maxCols || r > maxRows ) )
				continue;

			int code = Cell::CreatePosCode( c, r );

			itCell = std::find_if( m_cells.begin(), m_cells.end(), FindCellByCodePred( code ) );
			if ( itCell != m_cells.end() )
			{
				if ( itCell->GetState() != stateFree && itCell->GetState() != statePrev )
				{
					res = FALSE;

					break;
				}
			}
		}

		if ( !res )
			break;
	}

	return res;
}

BOOL Workspace::OnCellLClicked( Cell *pCell )
{
	return FALSE;
}

BOOL Workspace::OnCellRClicked( Cell *pCell )
{
	return FALSE;
}

///////////////////////////////////////////////////////////////////////////////
// derived workspaces implementation

BOOL MyWorkspace::OnCellLClicked( Cell *pCell )
{
	if ( m_pGame )
		return m_pGame->OnMyCellLClicked( pCell );

	return FALSE;
}

BOOL EnemyWorkspace::OnCellLClicked( Cell *pCell )
{
	if ( m_pGame )
		return m_pGame->OnEnemyCellLClicked( pCell );

	return FALSE;
}

