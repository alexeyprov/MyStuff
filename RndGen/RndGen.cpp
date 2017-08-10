// RndGen.cpp : Implementation of WinMain

#include "stdafx.h"
#include "resource.h"
#include "RndGen.h"

class CRndGenModule : public CAtlExeModuleT< CRndGenModule >
{
public :
	DECLARE_LIBID(LIBID_RndGenLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_RNDGEN, "{E4636BE0-BB40-4891-8065-691B1496786E}")
};

CRndGenModule _AtlModule;


//
extern "C" int WINAPI _tWinMain(HINSTANCE /*hInstance*/, HINSTANCE /*hPrevInstance*/, 
                                LPTSTR /*lpCmdLine*/, int nShowCmd)
{
    return _AtlModule.WinMain(nShowCmd);
}

