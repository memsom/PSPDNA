#include <pspge.h>
#include <pspdisplay.h>
#include <psputils.h>

#include "graph.h"

// rewrote this based on GFX.c

color_t *draw_buffer;
color_t *disp_buffer;

const int screenb_w = 512;
const int screen_w = 480;
const int screen_h = 272;
const int bsize = screen_h * screenb_w;
int depth = 4;

void initGraf()
{
    draw_buffer = sceGeEdramGetAddr();
    disp_buffer = (color_t *)(sceGeEdramGetAddr() + (bsize * depth));

    sceDisplaySetMode(0, screen_w, screen_h);
    sceDisplaySetFrameBuf(disp_buffer, screenb_w, PSP_DISPLAY_PIXEL_FORMAT_8888, PSP_DISPLAY_SETBUF_NEXTFRAME);
}

void clearGraf(color_t color)
{
    for (int i = 0; i < bsize; i++)
    {
        draw_buffer[i] = color;
    }
}

void swapBufferdGraf()
{
    color_t *temp = disp_buffer;
    disp_buffer = draw_buffer;
    draw_buffer = temp;

    sceKernelDcacheWritebackInvalidateAll();
    sceDisplaySetFrameBuf(disp_buffer, screenb_w, PSP_DISPLAY_PIXEL_FORMAT_8888, PSP_DISPLAY_SETBUF_NEXTFRAME);
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

    int offset = x + (y * screenb_w);

    for (int y1 = 0; y1 < h; y1++)
    {
        for (int x1 = 0; x1 < w; x1++)
        {
            draw_buffer[x1 + offset + y1 * screenb_w] = color;
        }
    }
}

//really this should have a size too.
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

    for (int y1 = 0; y1 < h; y1++)
    {
        for (int x1 = 0; x1 < w; x1++)
        {
            uint32_t* p = image[pixelProgress];
            draw_buffer[x1 + offset +y1 * screenb_w] = *p;
            pixelProgress += 1;
        }
    }
}
