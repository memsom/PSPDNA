#include <pspge.h>
#include <pspdisplay.h>
#include <psputils.h>

#include "graph.h"

color_t* displayp;
color_t* drawp;

const int screenb_w = 512;
const int screen_w = 480;
const int screen_h = 272;
const int bsize = screen_h * screenb_w;
int depth = 4;

void initGraf()
{
    drawp = sceGeEdramGetAddr();
    displayp = (color_t*)(sceGeEdramGetAddr() + (bsize * depth));

    sceDisplaySetMode(0, screen_w, screen_h);
    sceDisplaySetFrameBuf(displayp, screenb_w, PSP_DISPLAY_PIXEL_FORMAT_8888, PSP_DISPLAY_SETBUF_NEXTFRAME);
}

void clearGraf(color_t color)
{
    for(int i = 0; i < bsize; i++)
    {
        drawp[i] = color;
    }
}

void swapBufferdGraf()
{
    color_t* temp = drawp;
    displayp = drawp;
    drawp = temp;

    sceKernelDcacheWritebackInvalidateAll();
    sceDisplaySetFrameBuf(displayp, screenb_w, PSP_DISPLAY_PIXEL_FORMAT_8888, PSP_DISPLAY_SETBUF_NEXTFRAME);
}

void drawRectGraf(int x, int y, int w, int h, color_t color)
{
    if (x > screen_w) x = screen_w;
    if(y > screen_h) y = screen_h;
    if(x + w > screen_w) w = screen_w - x;
    if(y + h > screen_h) h = screen_h - y;

    int offset = x +(y * screenb_w);

    for (int yy = 0; yy < h; yy++)
        for (int xx = 0; xx < w; xx++)
            drawp[xx + offset +yy * screenb_w] = color;
}
