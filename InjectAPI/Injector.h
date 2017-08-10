/*
		Copyright (C) 1999, CAT Computing Center (R).   

		Project:		Injector
		Description:	Header of main class
*/

class CInjector : public CConApp
{
public:
	CInjector(int argc = 0, TCHAR *args[] = NULL, TCHAR *arge[] = NULL);
	~CInjector();

protected:
	void PrintCopyRight();
	virtual int OnRun(int nCode);
	virtual int OnError(int ec, ...);
	virtual int OnHelp();
	virtual int ParseCommandLine();

protected:
	_tstring m_DLLFile;
	_tstring m_ExecFile;
	_tstring m_CmdLine;
	bool m_bUnload;
	bool m_bAttach;
	bool m_bUnloadDLL;
	bool m_bSilent;
};
