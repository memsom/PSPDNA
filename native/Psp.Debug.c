#include "Compat.h"
#include "Sys.h"

#include "System.String.h"

#include "MetaData.h"
#include "Types.h"
#include "Type.h"

#include <pspdebug.h>
#include <pspiofilemgr.h>

// this is basically the same as System_Console_Write(..)
tAsyncCall *Psp_Debug_nativeScreenPrintf(PTR pThis_, PTR pParams, PTR pReturnValue)
{
	HEAP_PTR string;
	STRING2 str;
	U32 i, strLen;

	I32 SUB_LEN = 129;
	string = *(HEAP_PTR *)pParams;
	unsigned char str8[SUB_LEN];
	str = SystemString_GetString(string, &strLen);
	U32 start = 0;
	U32 thisLen = (strLen > SUB_LEN) ? SUB_LEN : strLen;
	for (i = 0; i < thisLen; i++)
	{
		unsigned char c = str[start + i] & 0xff;
		str8[i] = c ? c : '?';
	}
	str8[i] = 0;

	pspDebugScreenPrintf(str8);
	sceIoWrite(2, str8, thisLen);
	sceIoWrite(2, "\n", 1);

	return NULL;
}

tAsyncCall *Psp_Debug_nativeScreenPrintfXY(PTR pThis_, PTR pParams, PTR pReturnValue)
{

    I32 offset = 0;
	I32 x = INTERNALCALL_PARAM(offset, I32);
	offset += sizeof(I32);
	I32 y = INTERNALCALL_PARAM(offset, I32);
    
    HEAP_PTR string;
	STRING2 str;
	U32 i, strLen;

	PTR pParamsx = pParams += (sizeof(I32) * 2);
	I32 SUB_LEN = 2;
	string = *(HEAP_PTR *)pParamsx;
	unsigned char str8[SUB_LEN];
	str = SystemString_GetString(string, &strLen);
	U32 start = 0;
	U32 thisLen = (strLen > SUB_LEN) ? SUB_LEN : strLen;
	for (i = 0; i < thisLen; i++)
	{
		unsigned char c = str[start + i] & 0xff;
		str8[i] = c ? c : '?';
	}
	str8[i] = 0;

    pspDebugScreenSetXY((int)x, (int)y);
	pspDebugScreenPrintf(str8);

	return NULL;
}

tAsyncCall *Psp_Debug_nativeScreenClear(PTR pThis_, PTR pParams, PTR pReturnValue)
{
	pspDebugScreenClear();

	return NULL;
}

tAsyncCall *Psp_Debug_nativeScreenSetXY(PTR pThis_, PTR pParams, PTR pReturnValue)
{
	int x = INTERNALCALL_PARAM(0, I32);
	int y = INTERNALCALL_PARAM(sizeof(I32), I32);

	pspDebugScreenSetXY(x, y);

	return NULL;
}
