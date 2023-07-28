#if defined(__PSP__)

#include <pspctrl.h>
#include "controls.h"

static volatile SceCtrlData data;
static volatile SceCtrlLatch latch;

#else

#include <stdbool.h>
#include <SDL2/SDL.h>
#include "dummypspkeys.h"

// these are latched
bool down = false;
bool up = false;
bool left = false;
bool right = false;

bool triangle = false;
bool square = false;
bool circle = false;
bool cross = false;

bool start = false;
bool select = false;

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


// the pad in not implemented for non PSP yet.
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
#else
	// for non PSP this is where we need to do all the SDL keyboard handling
	SDL_Event e;

	if (SDL_PollEvent(&e)) 
	{ 
		if (e.type == SDL_QUIT) exit(1);
		else if (e.type == SDL_KEYDOWN)
		{
			switch (e.key.keysym.sym)
			{
			case SDLK_UP:
				up = true;
				break;

			case SDLK_DOWN:
				down = true;
				break;

			case SDLK_LEFT:
				left = true;
				break;

			case SDLK_RIGHT:
				right = true;
				break;

			case SDLK_w:
				triangle = true;
				break;

			case SDLK_s:
				circle = true;
				break;

			case SDLK_z:
				cross = true;
				break;

			case SDLK_a:
				square = true;
				break;

			case SDLK_o:
				select = true;
				break;

			case SDLK_p:
				start = true;
				break;
			}
		}
		else if (e.type == SDL_KEYUP)
		{
			switch (e.key.keysym.sym)
			{
			case SDLK_UP:
				up = false;
				break;

			case SDLK_DOWN:
				down = false;
				break;

			case SDLK_LEFT:
				left = false;
				break;

			case SDLK_RIGHT:
				right = false;
				break;

			case SDLK_w:
				triangle = false;
				break;

			case SDLK_s:
				circle = false;
				break;

			case SDLK_z:
				cross = false;
				break;

			case SDLK_a:
				square = false;

			case SDLK_o:
				select = false;
				break;

			case SDLK_p:
				start = false;
				break;
			}
		}
	}
#endif
}

int isKeyHold(int key)
{
#if defined(__PSP__)
	return latch.uiPress & key;
#else

	switch (key)
	{
	case PSP_CTRL_UP:
		return up;
	case PSP_CTRL_DOWN:
		return down;
	case PSP_CTRL_LEFT:
		return left;
	case PSP_CTRL_RIGHT:
		return right;
	case PSP_CTRL_SQUARE:
		return square;
	case PSP_CTRL_CIRCLE:
		return circle;
	case PSP_CTRL_TRIANGLE:
		return triangle;
	case PSP_CTRL_CROSS:
		return cross;
	case PSP_CTRL_START:
		return start;
	case PSP_CTRL_SELECT:
		return select;
	default:
		break;
	}

	return 0; // TODO, make this work with SDL
#endif
}

int isKeyDown(int key)
{
#if defined(__PSP__)
	return (latch.uiMake & key);
#else
	switch (key)
	{
	case PSP_CTRL_UP:
		return up;
	case PSP_CTRL_DOWN:
		return down;
	case PSP_CTRL_LEFT:
		return left;
	case PSP_CTRL_RIGHT:
		return right;
	case PSP_CTRL_SQUARE:
		return square;
	case PSP_CTRL_CIRCLE:
		return circle;
	case PSP_CTRL_TRIANGLE:
		return triangle;
	case PSP_CTRL_CROSS:
		return cross;
	case PSP_CTRL_START:
		return start;
	case PSP_CTRL_SELECT:
		return select;
	default:
		break;
	}

	return 0; // TODO, make this work with SDL
#endif
}

int isKeyUp(int key)
{
#if defined(__PSP__)
	return (latch.uiBreak & key);
#else
	switch (key)
	{
	case PSP_CTRL_UP:
		return up;
	case PSP_CTRL_DOWN:
		return down;
	case PSP_CTRL_LEFT:
		return left;
	case PSP_CTRL_RIGHT:
		return right;
	case PSP_CTRL_SQUARE:
		return square;
	case PSP_CTRL_CIRCLE:
		return circle;
	case PSP_CTRL_TRIANGLE:
		return triangle;
	case PSP_CTRL_CROSS:
		return cross;
	case PSP_CTRL_START:
		return start;
	case PSP_CTRL_SELECT:
		return select;
	default:
		break;
	}

	return 0; // TODO, make this work with SDL
#endif
}