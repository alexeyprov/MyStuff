#pragma once

extern HINSTANCE g_hInstance;

class Cell;
class Game;

class Workspace : public CWindow
{
public:
	enum
	{
		maxCols		= 10,
		maxRows		= 10,
		rectSize	= 25,
		shiftX		= 20,
		shiftY		= 20
	};

	typedef std::vector<Cell> CellVec;
	typedef std::vector<Cell*> CellPtrVec;

private:
	CellVec m_cells;

	void DrawRulers( HDC hdc );
	void DrawCells( HDC hdc );
	void CreateCells();

	class FindCellByPointPred
	{
		POINT m_pt;
	public:
		explicit FindCellByPointPred( POINT pt ) : m_pt( pt ) {}

		bool operator() (Cell &cell)
		{
			return (::PtInRect( cell.GetRectPtr(), m_pt )) ? true : false;
		}
	};

	class FindCellByCodePred
	{
		int m_code;
	public:
		explicit FindCellByCodePred( int code ) : m_code( code ) { }

		bool operator() (Cell &cell)
		{
			return cell.GetPosCode() == m_code;
		}
	};

public:
	Workspace();
	~Workspace();

	BOOL IsCellAvaiable( const Cell *pCell );
	void GetNearestCells( const Cell *pCell, CellPtrVec *pvec );
	void Reset();
	void BindToGame( Game *pGame );
	void GetCell( int col, int row, Cell **pCell );

protected:
	Game *m_pGame;

	virtual LRESULT OnCreate( LPCREATESTRUCT crst );
	virtual void OnDraw( HDC hdc );
	virtual LRESULT OnLButtonDown( DWORD dwKeyIndicator, WORD x, WORD y );
	virtual LRESULT OnRButtonDown( DWORD dwKeyIndicator, WORD x, WORD y );
	virtual BOOL OnCellLClicked( Cell *pCell );
	virtual BOOL OnCellRClicked( Cell *pCell );
};

class MyWorkspace : public Workspace
{
public:
	virtual BOOL OnCellLClicked( Cell *pCell );
};

class EnemyWorkspace : public Workspace
{
public:
	virtual BOOL OnCellLClicked( Cell *pCell );
};

