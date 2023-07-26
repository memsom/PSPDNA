#include "controls.h"
#include "callback.h"
#include "Compat.h"
#include "Sys.h"

#include "MetaData.h"
#include "Types.h"
#include "Type.h"
#include "System.String.h"

#include "Psp.Controls.h"

#if defined(__PSP__)
#include <pspctrl.h>
#include <pspdebug.h>
#endif

tAsyncCall *Psp_Controls_nativeJoystickX(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    I32 result = getJX();

    *(I32 *)pReturnValue = result;

    return NULL;
}

tAsyncCall *Psp_Controls_nativeJoystickY(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    I32 result = getJY();

    *(I32 *)pReturnValue = result;

    return NULL;
}

tAsyncCall *Psp_Controls_nativePollPad(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    pollPad();

    return NULL;
}

tAsyncCall *Psp_Controls_nativePollLatch(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    pollLatch();

    return NULL;
}

tAsyncCall *Psp_Controls_nativeIsKeyHold(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    int key = INTERNALCALL_PARAM(0, I32);
    I32 result = isKeyHold(key);

    *(I32 *)pReturnValue = result;

    return NULL;
}

tAsyncCall *Psp_Controls_nativeIsKeyDown(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    int key = INTERNALCALL_PARAM(0, I32);
    I32 result = isKeyDown(key);

    *(I32 *)pReturnValue = result;

    return NULL;
}

tAsyncCall *Psp_Controls_nativeIsKeyUp(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    int key = INTERNALCALL_PARAM(0, I32);
    I32 result = isKeyUp(key);

    *(I32 *)pReturnValue = result;

    return NULL;
}

tAsyncCall *Psp_State_nativeIsRunning(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    I32 result = isRunning();

    *(I32 *)pReturnValue = result;

    return NULL;
}

char *pAppName = NULL;

tAsyncCall *Psp_State_setAppName(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    HEAP_PTR pStr = ((HEAP_PTR *)pParams)[0];

    // we need to convert the System.String to a char*
    STRING2 str;
    U32 i, strLen;
    str = SystemString_GetString(pStr, &strLen);
    char str8[strLen + 1];
    U32 start = 0;
    for (i = 0; i < strLen; i++)
    {
        unsigned char c = str[start + i] & 0xff;
        str8[i] = c ? c : '?';
    }
    str8[i] = 0;

    // copy the data to the internal holder
    pAppName = malloc(strLen + 1);
    strncpy(pAppName, &str8, strLen + 1);

    return NULL;
}
