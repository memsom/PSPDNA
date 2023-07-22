using System;
using System.Runtime.CompilerServices;

namespace Psp
{
    public static class Debug
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void NativeScreenPrintf(string s);

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern public static void ScreenClear();

        public static void ScreenPrintf(string s, params object[] p)
        {
            NativeScreenPrintf(string.Format(s, p));
        }

        public static void ScreenPrintfLine(string s, params object[] p)
        {
            NativeScreenPrintf(string.Format($"{s}\n", p));
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern public static void ScreenSetXY(int x, int y);

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern public static void ScreenPrintf(int x, int y, string s);

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void NativeWrite(string s);

        public static void Write(string s, params object[] p)
        {
            NativeWrite(string.Format(s, p));
        }

        public static void WriteLine(string s, params object[] p)
        {
            NativeWrite(string.Format($"{s}\n", p));
        }
    }
}
