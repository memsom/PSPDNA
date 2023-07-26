#include "controls.h"
#include "callback.h"
#include "Compat.h"
#include "Sys.h"

#include "MetaData.h"
#include "Types.h"
#include "Type.h"
#include "System.String.h"

#include "Psp.BasicGraphics.h"

#if defined(__PSP__)
#include <pspdebug.h>
#endif

#include <SDL2/SDL.h>
#include <SDL2/SDL_ttf.h>

SDL_Window *window = NULL;
SDL_Renderer *renderer;
TTF_Font *defaultFont = NULL; // for now we just use the default font

tAsyncCall *Psp_BasicGraphics_nativeInit2(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    if (SDL_Init(SDL_INIT_VIDEO) < 0)
    {
        SDL_Log("SDL_Init: %s\n", SDL_GetError());
        return -1;
    }

    // create an SDL window (pspgl enabled)
    window = SDL_CreateWindow("sdl2_psp", 0, 0, 480, 272, 0);
    if (!window)
    {
        SDL_Log("SDL_CreateWindow: %s\n", SDL_GetError());
        SDL_Quit();
        return -1;
    }

    // create a renderer (OpenGL ES2)
    renderer = SDL_CreateRenderer(window, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);
    if (!renderer)
    {
        SDL_Log("SDL_CreateRenderer: %s\n", SDL_GetError());
        SDL_Quit();
        return -1;
    }

    if (TTF_Init() == 0)
    {
        defaultFont = TTF_OpenFont("Fonts/arial.ttf", 12);
    }

    return NULL;
}

tAsyncCall *Psp_BasicGraphics_nativeClear2(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    U32 color = INTERNALCALL_PARAM(0, U32);

    int a = (U8)(color >> 24);
    int r = (U8)(color >> 16);
    int g = (U8)(color >> 8);
    int b = (U8)(color >> 0);

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

    int a = (U8)(color >> 24);
    int r = (U8)(color >> 16);
    int g = (U8)(color >> 8);
    int b = (U8)(color >> 0);

    SDL_SetRenderDrawColor(renderer, r, g, b, a);
    SDL_Rect rect = {x, y, w, h};
    SDL_RenderFillRect(renderer, &rect);

    return NULL;
}

tAsyncCall *Psp_BasicGraphics_nativeDrawText2(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    // only call this if we loaded the font correctly
    if (defaultFont)
    {
        // get the data from the params...
        HEAP_PTR pStr = ((HEAP_PTR *)pParams)[0];
        int x = ((int *)pParams)[1];
        int y = ((int *)pParams)[2];
        U32 color = ((int *)pParams)[3];

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

        // decode the colour
        int a = (U8)(color >> 24);
        int r = (U8)(color >> 16);
        int g = (U8)(color >> 8);
        int b = (U8)(color >> 0);

        // this code is basically the same as the Tet demo originally used.
        int w, h;
        TTF_SizeText(defaultFont, str8, &w, &h);
        SDL_Surface *msgsurf = TTF_RenderText_Blended(defaultFont, str8, (SDL_Color){r, g, b, a});
        SDL_Texture *msgtex = SDL_CreateTextureFromSurface(renderer, msgsurf);
        SDL_Rect fromrec = {0, 0, msgsurf->w, msgsurf->h};
        SDL_Rect torec = {x, y, msgsurf->w, msgsurf->h};
        SDL_RenderCopy(renderer, msgtex, &fromrec, &torec);
        SDL_DestroyTexture(msgtex);
        SDL_FreeSurface(msgsurf);
    }
    return NULL;
}

static SDL_Surface *last;

tAsyncCall *Psp_BasicGraphics_nativeLoadSurface2(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    // read the param - this will be a path
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

    // we can nor load the value
    SDL_Surface *surface = SDL_LoadBMP(str8);

    last = surface;

    *(HEAP_PTR *)pReturnValue = (HEAP_PTR)surface;

    return NULL;
}

tAsyncCall *Psp_BasicGraphics_nativeCreateTexture2(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    // read the param - this will be a path
    HEAP_PTR pSurface = ((HEAP_PTR *)pParams)[0];

    SDL_Texture *pTexture = SDL_CreateTextureFromSurface(renderer, pSurface);

    //Crash("%i\n%i", pSurface, pTexture);

    *(HEAP_PTR *)pReturnValue = (HEAP_PTR)pTexture;

    return NULL;
}

tAsyncCall *Psp_BasicGraphics_nativeSetColorKey2(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    // read the param - this will be a path
    HEAP_PTR pSurface = ((HEAP_PTR *)pParams)[0];
    int flag = ((int *)pParams)[1];
    U32 key = ((U32 *)pParams)[2];

    int result = SDL_SetColorKey(pSurface, flag, key);

    *(int *)pReturnValue = (int *)result;

    return NULL;
}

tAsyncCall *Psp_BasicGraphics_nativeDrawTexture2(PTR pThis_, PTR pParams, PTR pReturnValue)
{
    HEAP_PTR pTexture = ((HEAP_PTR *)pParams)[0];
    int x = ((int *)pParams)[1];
    int y = ((int *)pParams)[2];
    int w = ((int *)pParams)[3];
    int h = ((int *)pParams)[4];

    int result = SDL_RenderCopy(renderer, pTexture, NULL,&(SDL_Rect){x, y, w, h});

    *(int *)pReturnValue = (int *)result;

    return NULL;
}