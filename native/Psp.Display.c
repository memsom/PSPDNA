#include "Compat.h"
#include "Sys.h"

#include "MetaData.h"
#include "Types.h"
#include "Type.h"

#include "Psp.Display.h"

#include <pspdisplay.h>

tAsyncCall* Psp_Display_nativeWaitVblankStart(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    *(int*)pReturnValue = sceDisplayWaitVblankStart();

	return NULL;
}
