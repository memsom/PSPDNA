#include <pspctrl.h>
#include "controls.h"

static volatile SceCtrlData data;
static volatile SceCtrlLatch latch;

int getJX() 
{
	return data.Lx;
}

int getJY() 
{
	return data.Ly;
}

void pollPad()
{
    sceCtrlReadBufferPositive(&data, 1);
}

void pollLatch() 
{
	sceCtrlReadLatch(&latch);
}

int isKeyHold(int key) 
{
	return latch.uiPress & key;
}

int isKeyDown(int key) 
{
	return (latch.uiMake & key);
}

int isKeyUp(int key) 
{
	return (latch.uiBreak & key);
}