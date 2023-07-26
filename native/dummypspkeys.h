#ifndef __DUMMYPSPKEYS
#define __DUMMYPSPKEYS

#ifdef __cplusplus
extern "C"
{
#endif

    enum PspCtrlButtons
    {
        /** Select button. */
        PSP_CTRL_SELECT = 0x000001,
        /** Start button. */
        PSP_CTRL_START = 0x000008,
        /** Up D-Pad button. */
        PSP_CTRL_UP = 0x000010,
        /** Right D-Pad button. */
        PSP_CTRL_RIGHT = 0x000020,
        /** Down D-Pad button. */
        PSP_CTRL_DOWN = 0x000040,
        /** Left D-Pad button. */
        PSP_CTRL_LEFT = 0x000080,
        /** Left trigger. */
        PSP_CTRL_LTRIGGER = 0x000100,
        /** Right trigger. */
        PSP_CTRL_RTRIGGER = 0x000200,
        /** Triangle button. */
        PSP_CTRL_TRIANGLE = 0x001000,
        /** Circle button. */
        PSP_CTRL_CIRCLE = 0x002000,
        /** Cross button. */
        PSP_CTRL_CROSS = 0x004000,
        /** Square button. */
        PSP_CTRL_SQUARE = 0x008000,
        /** Home button. In user mode this bit is set if the exit dialog is visible. */
        PSP_CTRL_HOME = 0x010000,
        /** Hold button. */
        PSP_CTRL_HOLD = 0x020000,
        /** Music Note button. */
        PSP_CTRL_NOTE = 0x800000,
        /** Screen button. */
        PSP_CTRL_SCREEN = 0x400000,
        /** Volume up button. */
        PSP_CTRL_VOLUP = 0x100000,
        /** Volume down button. */
        PSP_CTRL_VOLDOWN = 0x200000,
        /** Wlan switch up. */
        PSP_CTRL_WLAN_UP = 0x040000,
        /** Remote hold position. */
        PSP_CTRL_REMOTE = 0x080000,
        /** Disc present. */
        PSP_CTRL_DISC = 0x1000000,
        /** Memory stick present. */
        PSP_CTRL_MS = 0x2000000,
    };

#ifdef __cplusplus
}
#endif

#endif