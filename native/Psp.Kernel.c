#if defined(__PSP__)
#include <pspkernel.h>
#else
#include <stdlib.h>
#endif

#include "Psp.Debug.h"

tAsyncCall *Psp_Kernel_nativeExitGame(PTR pThis_, PTR pParams, PTR pReturnValue)
{
#if defined(__PSP__)
    sceKernelExitGame();
#else
    exit(1);
#endif
}