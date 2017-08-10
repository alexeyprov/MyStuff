// CryptoSign.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "wincrypt.h"

#include "CryptoEx.h"
#include "key.h"
#include "HostId.h"
#include "Convers.h"
#include "CryptCtx.h"

#define BUFFER_SIZE 256

int main(int argc, char* argv[])
{
	BYTE *pBuffer = NULL;
	DWORD dwLen;
	DWORD i;
	CHostId id;
	BYTE ar[] = {
					0x73, 0x14, 0x19, 0xE7,
					0xF1, 0x00, 0x63, 0x4C, 0x11,
					0x29, 0xAC, 0x27, 0x95, 0x3C, 0xFF, 0x83, 0x5D
				};
	int arLens[] = {4, 5, 8};
	char* pszArray = NULL;

	// Get a handle to the default provider.
	try
	{
		CCryptoContext ctx;

		// Part 1 - Encoding and decoding
		for (i = 0; i < 3; i++)
		{
			dwLen = CConversion::RequiredStringLength(arLens[i]);
			pszArray = new char[dwLen + 1];
			CConversion::ByteArray2String(ar + ((0 == i) ? 0 : arLens[i - 1]), arLens[i], pszArray);

			printf("%s\n", pszArray);

			ATLASSERT(CConversion::RequiredBinaryLength(pszArray) == arLens[i]);
		
			pBuffer = new BYTE[arLens[i]];
			CConversion::String2ByteArray(pszArray, pBuffer);

			ATLASSERT(0 == memcmp(pBuffer, ar + ((0 == i) ? 0 : arLens[i - 1]), arLens[i]));

			delete[] pszArray;
			pszArray = NULL;

			delete[] pBuffer;
			pBuffer = NULL;
		}

		// Part 2 - Encryption

		for (i = 0; i < 3; i++)
		{
			dwLen = ctx.Encrypt(NULL, arLens[i], true);

			printf(_T("%d bytes for %d-bytes length array\n"), dwLen, arLens[i]);

			pBuffer = new BYTE[dwLen];
			memcpy(pBuffer, ar + ((0 == i) ? 0 : arLens[i - 1]), arLens[i]);
			
			dwLen = ctx.Encrypt(pBuffer, arLens[i]);

			dwLen = ctx.Decrypt(pBuffer, dwLen);
			ATLASSERT(dwLen == arLens[i]);
			ATLASSERT(0 == memcmp(pBuffer, ar + ((0 == i) ? 0 : arLens[i - 1]), arLens[i]));

			delete[] pBuffer;
			pBuffer = NULL;
		}
	}
	catch (CGenericException& ex)
	{
		ex.ReportError();
	}

	if (pszArray != NULL)
	{
		delete[] pszArray;
	}

	// Free memory to be used to store signature.
	if (pBuffer != NULL)
	{
		delete[] pBuffer;
	}
}

