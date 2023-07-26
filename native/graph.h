#ifndef COMMON_GRAPH_H
#define COMMON_GRAPH_H

#include <stdint.h>

#define color_t uint32_t 

void initGraf();
void clearGraf(uint32_t color);
void swapBufferdGraf();
void drawRectGraf(int x, int y, int w, int h, color_t color);
void drawImageGraf(int x, int y, int w, int h, uint32_t* image);

#endif 