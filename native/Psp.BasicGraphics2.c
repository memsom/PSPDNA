#include "controls.h"
#include "callback.h"
#include "Compat.h"
#include "Sys.h"

#include "MetaData.h"
#include "Types.h"
#include "Type.h"

#include "Psp.BasicGraphics.h"
#include <pspdebug.h>

#include <SDL2/SDL.h>
#include <SDL2/SDL_ttf.h>

SDL_Window *window = NULL;
SDL_Renderer *renderer;

tAsyncCall *Psp_BasicGraphics_nativeInit2(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    if (SDL_Init(SDL_INIT_VIDEO) < 0) {
        SDL_Log("SDL_Init: %s\n", SDL_GetError());
        return -1;
    }

     // create an SDL window (pspgl enabled)
    window = SDL_CreateWindow("sdl2_psp", 0, 0, 480, 272, 0);
    if (!window) {
        SDL_Log("SDL_CreateWindow: %s\n", SDL_GetError());
        SDL_Quit();
        return -1;
    }

    // create a renderer (OpenGL ES2)
    renderer = SDL_CreateRenderer(window, 0, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);
    if (!renderer) {
        SDL_Log("SDL_CreateRenderer: %s\n", SDL_GetError());
        SDL_Quit();
        return -1;
    }

    return NULL;
}

tAsyncCall *Psp_BasicGraphics_nativeClear2(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    U32 color = INTERNALCALL_PARAM(0, U32);

    int a = (u8)(color >> 24);
    int r = (u8)(color >> 16);
    int g = (u8)(color >> 8);
    int b = (u8)(color >> 0);

    SDL_SetRenderDrawColor(renderer, r, g, b, a);
    SDL_RenderClear(renderer);

    return NULL;
}
tAsyncCall *Psp_BasicGraphics_nativeSwapBuffers2(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    SDL_RenderPresent(renderer);
    return NULL;
}

tAsyncCall *Psp_BasicGraphics_nativeDrawRect2(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    int size = sizeof(I32);
    int x = INTERNALCALL_PARAM(0, I32);
    int y = INTERNALCALL_PARAM(size, I32);
    int w = INTERNALCALL_PARAM(size * 2, I32);
    int h = INTERNALCALL_PARAM(size * 3, I32);
    U32 color = INTERNALCALL_PARAM(size * 4, U32);

    int a = (u8)(color >> 24);
    int r = (u8)(color >> 16);
    int g = (u8)(color >> 8);
    int b = (u8)(color >> 0);

    SDL_SetRenderDrawColor(renderer, r, g, b, a);
    SDL_Rect rect = {x, y, w, h};
    SDL_RenderFillRect(renderer, &rect);

    return NULL;
}