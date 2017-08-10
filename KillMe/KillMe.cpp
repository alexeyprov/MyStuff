// KillMe.cpp : Implementation of WinMain

#include "stdafx.h"
#include "resource.h"
#include "KillMe.h"
#include "setters.h"

#include "shellapi.h"

class CKillMeModule : public CAtlExeModuleT< CKillMeModule >
{
public :
	DECLARE_LIBID(LIBID_KillMeLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_KILLME, "{91CA491D-C951-4864-9C8B-0742024FA003}")
};

CKillMeModule _AtlModule;


//
extern "C" int WINAPI _tWinMain(HINSTANCE /*hInstance*/, HINSTANCE /*hPrevInstance*/, 
                                LPTSTR /*lpCmdLine*/, int nShowCmd)
{
    //return _AtlModule.WinMain(nShowCmd);
	//TestSetter<CFullTextSetter>();
	//TestSetter<CSelTextSetter>();
	//TestSetter<CPasteTextSetter>();
	SHFILEINFO sfi = {0};
	DWORD dwRet = ::SHGetFileInfo(_T(".htm"), 0, &sfi, sizeof(sfi), SHGFI_USEFILEATTRIBUTES | SHGFI_TYPENAME);
	return dwRet;
}

static int AddStr(char ** _dest, const char* _tag)
{
	const char* pStop = _tag + 20;
	char* p = *_dest;
	while ((_tag != pStop) && (0 != (*p++ = *_tag++)))
	{
	}
	*_dest = p;
	return 0;
}


