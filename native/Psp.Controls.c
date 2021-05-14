#include "controls.h"
#include "callback.h"
#include "Compat.h"
#include "Sys.h"

#include "MetaData.h"
#include "Types.h"
#include "Type.h"

#include "Psp.Controls.h"
#include <pspctrl.h>
#include <pspdebug.h>

tAsyncCall* Psp_Controls_nativeJoystickX(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    I32 result = getJX();

    *(I32*)pReturnValue = result;

	return NULL;
}

tAsyncCall* Psp_Controls_nativeJoystickY(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    I32 result = getJY();

    *(I32*)pReturnValue = result;

	return NULL;
}

tAsyncCall* Psp_Controls_nativePollPad(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    pollPad();

    return NULL;
}

tAsyncCall* Psp_Controls_nativePollLatch(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    pollLatch();

    return NULL;
}

tAsyncCall* Psp_Controls_nativeIsKeyHold(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    int key = INTERNALCALL_PARAM(0, I32);
    I32 result = isKeyHold(key);

    *(I32*)pReturnValue = result;

	return NULL;
}


tAsyncCall* Psp_Controls_nativeIsKeyDown(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    int key = INTERNALCALL_PARAM(0, I32);
    I32 result = isKeyDown(key);

    *(I32*)pReturnValue = result;

	return NULL;
}

tAsyncCall* Psp_Controls_nativeIsKeyUp(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    int key = INTERNALCALL_PARAM(0, I32);
    I32 result = isKeyUp(key);

    *(I32*)pReturnValue = result;

	return NULL;
}

tAsyncCall* Psp_State_nativeIsRunning(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    I32 result = isRunning();

    *(I32*)pReturnValue = result;

	return NULL;
}

