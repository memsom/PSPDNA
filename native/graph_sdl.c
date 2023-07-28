#include "graph.h"

#include <SDL2/SDL.h>
#include <stdlib.h>

#include "Types.h"

SDL_Window* graph_window = NULL;
SDL_Renderer* graph_renderer;

const int screenb_w = 512;
const int screen_w = 480;
const int screen_h = 272;

#if defined(_WIN32)

// windows can't handle consts that are not constant apparenltly

const int bsize = 130560;

#else

const int bsize = screen_h * screenb_w;

#endif

int depth = 4;

void initGraf()
{
	if (SDL_Init(SDL_INIT_VIDEO) < 0)
	{
		SDL_Log("SDL_Init: %s\n", SDL_GetError());
		exit(1);
	}

	// create an SDL  (pspgl enabled)
	graph_window = SDL_CreateWindow("sdl2_psp", 0, 0, 480, 272, 0);
	if (!graph_window)
	{
		SDL_Log("SDL_CreateWindow: %s\n", SDL_GetError());
		SDL_Quit();
		exit(1);
	}

	// create a graph_renderer (OpenGL ES2)
	graph_renderer = SDL_CreateRenderer(graph_window, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);
	if (!graph_renderer)
	{
		SDL_Log("SDL_CreateRenderer: %s\n", SDL_GetError());
		SDL_Quit();
		exit(1);
	}
}

void clearGraf(color_t color)
{
	int a = (U8)(color >> 24);
	int r = (U8)(color >> 16);
	int g = (U8)(color >> 8);
	int b = (U8)(color >> 0);

	SDL_SetRenderDrawColor(graph_renderer, r, g, b, a);
	SDL_RenderClear(graph_renderer);
}

void swapBufferdGraf()
{
	SDL_RenderPresent(graph_renderer);
}

void drawRectGraf(int x, int y, int w, int h, color_t color)
{
	if (x > screen_w)
	{
		x = screen_w;
	}
	if (y > screen_h)
	{
		y = screen_h;
	}
	if (x + w > screen_w)
	{
		w = screen_w - x;
	}
	if (y + h > screen_h)
	{
		h = screen_h - y;
	}

	int a = (U8)(color >> 24);
	int r = (U8)(color >> 16);
	int g = (U8)(color >> 8);
	int b = (U8)(color >> 0);

	SDL_SetRenderDrawColor(graph_renderer, r, g, b, a);
	SDL_Rect rect = { x, y, w, h };
	SDL_RenderFillRect(graph_renderer, &rect);
}

// really this should have a size too.
void drawImageGraf(int x, int y, int w, int h, uint32_t* image)
{
	if (x > screen_w)
	{
		x = screen_w;
	}
	if (y > screen_h)
	{
		y = screen_h;
	}
	if (x + w > screen_w)
	{
		w = screen_w - x;
	}
	if (y + h > screen_h)
	{
		h = screen_h - y;
	}

	int offset = x + (y * screenb_w);
	int pixelProgress = 0;

	// this is probably sub optimal
	for (int y1 = 0; y1 < h; y1++)
	{
		for (int x1 = 0; x1 < w; x1++)
		{
			uint32_t color = *((uint32_t*)image[pixelProgress]);
			int a = (U8)(color >> 24);
			int r = (U8)(color >> 16);
			int g = (U8)(color >> 8);
			int b = (U8)(color >> 0);

			SDL_SetRenderDrawColor(graph_renderer, r, g, b, a);
			SDL_RenderDrawPoint(graph_renderer, x, y);
			pixelProgress += 1;
		}
	}
}
