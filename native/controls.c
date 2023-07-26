#if defined(__PSP__)

#include <pspctrl.h>
#include "controls.h"

static volatile SceCtrlData data;
static volatile SceCtrlLatch latch;

#endif

int getJX()
{
#if defined(__PSP__)
	return data.Lx;
#else
	return 0; // TODO, make this work with SDL
#endif
}

int getJY()
{
#if defined(__PSP__)
	return data.Ly;
#else
	return 0; // TODO, make this work with SDL
#endif
}

void pollPad()
{
#if defined(__PSP__)
	sceCtrlReadBufferPositive(&data, 1);
#endif
}

void pollLatch()
{
#if defined(__PSP__)
	sceCtrlReadLatch(&latch);
#endif
}

int isKeyHold(int key)
{
#if defined(__PSP__)
	return latch.uiPress & key;
#else
	return 0; // TODO, make this work with SDL
#endif
}

int isKeyDown(int key)
{
#if defined(__PSP__)
	return (latch.uiMake & key);
#else
	return 0; // TODO, make this work with SDL
#endif
}

int isKeyUp(int key)
{
#if defined(__PSP__)
	return (latch.uiBreak & key);
#else
	return 0; // TODO, make this work with SDL
#endif
}