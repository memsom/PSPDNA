#include "Compat.h"
#include "Sys.h"

#include "MetaData.h"
#include "Types.h"
#include "Type.h"

#include "Psp.Display.h"

#if defined(__PSP__)
#include <pspdisplay.h>
#endif

tAsyncCall *Psp_Display_nativeWaitVblankStart(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    
#if defined(__PSP__)
    *(int *)pReturnValue = sceDisplayWaitVblankStart();
#else
    printf("VBlank called\n");
#endif

    return NULL;
}
