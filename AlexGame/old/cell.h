#pragma once

enum cellState
{
	stateFree	= 0,
	stateBusy	= 1,
	stateDead	= 2,
	stateOff	= 3,
	stateHit	= 4,
	statePrev	= 5
};

class Cell
{
	RECT m_rect;
	cellState m_state;
	int m_posCode;

	void CreateBrushes();

	static int g_uses;
	static std::map< int, HBRUSH > s_brush;
public:
	Cell();
	Cell( const Cell& );
	~Cell();

	void Redraw();
	void Reset();
	virtual void Draw( HDC hdc );
	void SetRect( RECT &rect );
	LPRECT GetRectPtr();
	void SetState( cellState newState );
	cellState GetState();
	void SetPosCode( int posCode );
	int GetPosCode();

	static int CreatePosCode( int xpos, int ypos );
	static void DecodePosCode( int code, int *xpos, int *ypos );
};

enum enuShipType
{
	shipBattleship	= 1,
	shipCruiser		= 2,
	shipDestroyer	= 3,
	shipTorpedo		= 4
};

class Ship
{
protected:
	enuShipType m_type;
	std::vector< Cell* > m_cells;
	BOOL m_bIsLive;
public:
	Ship();
	~Ship();

	virtual BOOL IsCreated() = 0;
	void AttachCell( Cell *pCell );
	void FreeCells();
	BOOL IsLive();
	BOOL IsCellInShip( Cell *pCell );

	inline enuShipType GetType()
	{
		return m_type;
	}
};

class Battleship : public Ship
{
public:
	Battleship();

	virtual BOOL IsCreated();
};

class Cruiser : public Ship
{
public:
	Cruiser();

	virtual BOOL IsCreated();
};

class Destroyer : public Ship
{
public:
	Destroyer();

	virtual BOOL IsCreated();
};

class Torpedo : public Ship
{
public:
	Torpedo();

	virtual BOOL IsCreated();
};

