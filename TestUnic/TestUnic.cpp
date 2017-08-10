// TestUnic.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"


int main(int argc, char* argv[])
{
	const wchar_t wszTemp[] = L"\x0430\x0431\x0432\x0433\x0434\x0435";
	char szTemp[6];

	::WideCharToMultiByte(CP_ACP, 0, wszTemp, -1, szTemp, sizeof(szTemp), NULL, NULL);
	return 0;
}
