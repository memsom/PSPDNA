#include <pspkernel.h>
#include "Psp.Debug.h"

tAsyncCall* Psp_Kernel_nativeExitGame(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    sceKernelExitGame();
}