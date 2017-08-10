#pragma once

extern HINSTANCE g_hInstance;

enum
{
	WM_GAME_MYTURN		= WM_USER + 100,
	WM_GAME_ENEMYTURN,
	WM_GAME_NOGAME,
	WM_GAME_WRONG_SHIP_CELL,
	WM_GAME_NEXTSHIP_CREATE,
	WM_GAME_READY
};

class Game
{
	HWND m_hwndParent;

	enum gameState
	{
		gameEmpty		= 0,
		gameMyTurn		= 1,
		gameEnemyTurn	= 2,
		gameWon			= 3,
		gameLost		= 4,
		gameReady		= 5
	};

	enum
	{
		battleshipCount	= 1,
		cruiserCount	= 2,
		destroyerCount	= 3,
		torpedoCount	= 4
	};

	std::vector< Ship* > m_ships;
	Ship* m_pCurrentShip;
	int m_shipCount;

	MyWorkspace m_wsMy;
	EnemyWorkspace m_wsEnemy;

	gameState m_gameState;

	BOOL CreateNextShip();
	void ClearShips();
	BOOL PostParentMessage( UINT uMsg, WPARAM wParam = 0, LPARAM lParam = 0 );

	class FindShipByCellPred
	{
		Cell *m_cell;
	public:
		explicit FindShipByCellPred( Cell *pCell ) : m_cell( pCell ) {}

		bool operator() ( Ship &ship )
		{
			return ( ship.IsCellInShip( m_cell ) ) ? true : false;
		}
	};
public:
	Game();
	~Game();

	void Reset();
	void Init( HWND hwndParent );
	BOOL OnMyCellLClicked( Cell *pCell );
	BOOL OnEnemyCellLClicked( Cell *pCell );
};

