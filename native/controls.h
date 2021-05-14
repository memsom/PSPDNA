#ifndef COMMON_CONTROLS_H
#define COMMON_CONTROLS_H

int getJX();
int getJY();

void pollPad();
void pollLatch();

int isKeyHold(int key); // returns 1(true) if key is down
int isKeyDown(int key);
int isKeyUp(int key);

#endif