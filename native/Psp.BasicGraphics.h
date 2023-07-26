#if !defined(__PSP_BASICGRAPHICS_H)
#define __PSP_BASICGRAPHICS_H

#include "Types.h"

tAsyncCall *Psp_BasicGraphics_nativeInit(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall *Psp_BasicGraphics_nativeClear(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall *Psp_BasicGraphics_nativeSwapBuffers(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall *Psp_BasicGraphics_nativeDrawRect(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall *Psp_BasicGraphics_nativeDrawImage(PTR pThis_, PTR pParams, PTR pReturnValue);

tAsyncCall *Psp_BasicGraphics_nativeInit2(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall *Psp_BasicGraphics_nativeClear2(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall *Psp_BasicGraphics_nativeSwapBuffers2(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall *Psp_BasicGraphics_nativeDrawRect2(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall *Psp_BasicGraphics_nativeDrawText2(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall *Psp_BasicGraphics_nativeLoadSurface2(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall *Psp_BasicGraphics_nativeCreateTexture2(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall *Psp_BasicGraphics_nativeSetColorKey2(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall *Psp_BasicGraphics_nativeDrawTexture2(PTR pThis_, PTR pParams, PTR pReturnValue);

#endif