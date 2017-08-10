#include "stdafx.h"
#include "cell.h"
#include "workspace.h"
#include "game.h"

Game::Game() 
	: m_gameState( gameEmpty )
	, m_pCurrentShip( 0 )
{
}

Game::~Game()
{
	ClearShips();
}

void Game::Init( HWND hwndParent )
{
	m_hwndParent = hwndParent;

	RECT rect1 = { 10, 10, 310, 310 };
	RECT rect2 = { 340, 10, 640, 310 };

	m_wsMy.Create( g_hInstance, hwndParent, WS_CHILD | WS_BORDER | WS_VISIBLE, "Workspace", 0, &rect1 );
	m_wsEnemy.Create( g_hInstance, hwndParent, WS_CHILD | WS_BORDER | WS_VISIBLE, "Workspace", 0, &rect2 );

	m_wsMy.BindToGame( this );
	m_wsEnemy.BindToGame( this );

	PostParentMessage( WM_GAME_NEXTSHIP_CREATE, shipBattleship );
}

void Game::Reset()
{
	m_gameState = gameEmpty;
	m_pCurrentShip = NULL;
	m_shipCount = 0;

	ClearShips();

	m_wsMy.Reset();
	m_wsEnemy.Reset();

	m_wsMy.Repaint();
	m_wsEnemy.Repaint();
}

void Game::ClearShips()
{
	std::vector< Ship* >::iterator itShip = NULL;

	for ( itShip = m_ships.begin(); itShip != m_ships.end(); itShip++ )
	{
		delete *itShip;
	}

	m_ships.clear();
}

BOOL Game::CreateNextShip()
{
	BOOL res = TRUE;

	if ( m_pCurrentShip == NULL )
	{
		m_pCurrentShip = new Battleship;
	}
	else if ( m_pCurrentShip->GetType() == shipBattleship )
	{
		m_pCurrentShip = new Cruiser;
		m_shipCount = 1;
	}
	else if ( m_pCurrentShip->GetType() == shipCruiser )
	{
		if ( m_shipCount < cruiserCount )
		{
			m_pCurrentShip = new Cruiser;
			++m_shipCount;
		}
		else
		{
			m_pCurrentShip = new Destroyer;
			m_shipCount = 1;
		}
	}
	else if ( m_pCurrentShip->GetType() == shipDestroyer )
	{
		if ( m_shipCount < destroyerCount )
		{
			m_pCurrentShip = new Destroyer;
			++m_shipCount;
		}
		else
		{
			m_pCurrentShip = new Torpedo;
			m_shipCount = 1;
		}
	}
	else if ( m_pCurrentShip->GetType() == shipTorpedo )
	{
		if ( m_shipCount < torpedoCount )
		{
			m_pCurrentShip = new Torpedo;
			++m_shipCount;
		}
		else
		{
			// report all ships were created
			res = FALSE;
			m_pCurrentShip = NULL;
			m_shipCount = 0;
			m_gameState = gameReady;

			PostParentMessage( WM_GAME_READY );
		}
	}

	m_ships.push_back( m_pCurrentShip );

	return res;
}

BOOL Game::PostParentMessage( UINT uMsg, WPARAM wParam /*= 0*/, LPARAM lParam /*= 0*/ )
{
	return ::PostMessage( m_hwndParent, uMsg, wParam, lParam );
}

BOOL Game::OnMyCellLClicked( Cell *pCell )
{
	if ( m_gameState == gameEmpty )
	{
		if ( pCell->GetState() != stateFree || !m_wsMy.IsCellAvaiable( pCell ) )
		{
			PostParentMessage( WM_GAME_WRONG_SHIP_CELL );

			return FALSE;
		}

		// create first ship
		if ( !m_pCurrentShip )
		{
			CreateNextShip();
		}

		pCell->SetState( statePrev );
		m_pCurrentShip->AttachCell( pCell );

		if ( m_pCurrentShip->IsCreated() )
		{
			if ( CreateNextShip() )
				PostParentMessage( WM_GAME_NEXTSHIP_CREATE, m_pCurrentShip->GetType() );

			m_wsMy.Repaint();
		}
	}
	else if ( m_gameState == gameEnemyTurn )
	{
		PostParentMessage( WM_GAME_MYTURN );
	}

	return TRUE;
}

BOOL Game::OnEnemyCellLClicked( Cell *pCell )
{
	if ( m_gameState == gameEmpty )
	{
		PostParentMessage( WM_GAME_NOGAME );
	}
	else if ( m_gameState = gameMyTurn )
	{
		pCell->SetState( stateOff );

		PostParentMessage( WM_GAME_MYTURN );
	}

	return TRUE;
}

