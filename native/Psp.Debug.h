#if !defined(__PSP_DEBUG_H)
#define __PSP_DEBUG_H

#include "Types.h"

tAsyncCall* Psp_Debug_nativeScreenPrintf(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall* Psp_Debug_nativeScreenClear(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall* Psp_Debug_nativeScreenSetXY(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall *Psp_Debug_nativeScreenPrintfXY(PTR pThis_, PTR pParams, PTR pReturnValue);

#endif