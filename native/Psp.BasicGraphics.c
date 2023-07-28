#include "controls.h"
#include "callback.h"
#include "Compat.h"
#include "Sys.h"

#include "MetaData.h"
#include "Types.h"
#include "Type.h"

#include "Psp.BasicGraphics.h"
#include "graph.h"
#include "System.Array.h"

#if defined(__PSP__)
#include <pspdebug.h>
#endif

tAsyncCall *Psp_BasicGraphics_nativeInit(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    initGraf();

    return NULL;
}

tAsyncCall *Psp_BasicGraphics_nativeClear(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    color_t color = INTERNALCALL_PARAM(0, U32);
    clearGraf(color);

    return NULL;
}
tAsyncCall *Psp_BasicGraphics_nativeSwapBuffers(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    swapBufferdGraf();

    return NULL;
}

tAsyncCall *Psp_BasicGraphics_nativeDrawRect(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    int size = sizeof(I32);
    int x = INTERNALCALL_PARAM(0, I32);
    int y = INTERNALCALL_PARAM(size, I32);
    int w = INTERNALCALL_PARAM(size * 2, I32);
    int h = INTERNALCALL_PARAM(size * 3, I32);
    U32 c = INTERNALCALL_PARAM(size * 4, U32);

    drawRectGraf(x, y, w, h, c);

    return NULL;
}

tAsyncCall *Psp_BasicGraphics_nativeDrawImage(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    int size = sizeof(I32);
    int x = INTERNALCALL_PARAM(0, I32);
    int y = INTERNALCALL_PARAM(size, I32);
    int w = INTERNALCALL_PARAM(size * 2, I32);
    int h = INTERNALCALL_PARAM(size * 3, I32);
    HEAP_PTR* p = INTERNALCALL_PARAM(size * 4, HEAP_PTR*);
    PTR pFirstElement = SystemArray_LoadElementAddress(*p, 0);
    
    uint32_t* dst = (uint32_t*)pFirstElement;

    drawImageGraf(x, y, w, h, dst);

    return NULL;
}