#if !defined(__PSP_CONTROLS_H)
#define __PSP_CONTROLS_H

#include "Types.h"

tAsyncCall* Psp_Controls_nativeJoystickX(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall* Psp_Controls_nativeJoystickY(PTR pThis_, PTR pParams, PTR pReturnValue);

tAsyncCall* Psp_Controls_nativePollPad(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall* Psp_Controls_nativePollLatch(PTR pThis_, PTR pParams, PTR pReturnValue);

tAsyncCall* Psp_Controls_nativeIsKeyHold(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall* Psp_Controls_nativeIsKeyDown(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall* Psp_Controls_nativeIsKeyUp(PTR pThis_, PTR pParams, PTR pReturnValue);

tAsyncCall* Psp_State_nativeIsRunning(PTR pThis_, PTR pParams, PTR pReturnValue);
tAsyncCall* Psp_State_setAppName(PTR pThis_, PTR pParams, PTR pReturnValue);

#endif