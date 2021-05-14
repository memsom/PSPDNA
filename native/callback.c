#include <pspkernel.h>

static int exitRequest = 0;

int isRunning()
{
    return !exitRequest;
}

int exitCallback(int arg1, int arg2, void* common)
{
    exitRequest = 1;
    return 0;
}

int callbackThread(SceSize args, void* argp)
{
    int cbid = sceKernelCreateCallback("Exit Callback", exitCallback, NULL);
    sceKernelRegisterExitCallback(cbid);

    sceKernelSleepThreadCB();

    return 0;
}

int setupExitCallback()
{
    int threadId = sceKernelCreateThread("update_thread", callbackThread, 0x11, 0xFA0, THREAD_ATTR_USER, 0);

    if(threadId >= 0)
    {
        sceKernelStartThread(threadId, 0, NULL);
    }

    return threadId;
}
