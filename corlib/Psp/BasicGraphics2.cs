using System.Runtime.CompilerServices;

namespace Psp
{
    // If you are using the app menu, you really can't mix
    // graphics libs... you must use BasicGraphics2...
    public static class BasicGraphics2
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern public static void Init();

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void NativeClear(uint color);

        public static void Clear(Color color)
        {
            NativeClear(color.ToNative());
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern public static void SwapBuffers();

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void NativeDrawRect(int x, int y, int w, int h, uint color);

        public static void DrawRect(int x, int y, int w, int h, Color color)
        {
            NativeDrawRect(x, y, w, h, color.ToNative());
        }

        public static void DrawRect(Rect rect, Color color)
        {
            DrawRect(rect.X, rect.Y, rect.Width, rect.Height, color);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static void NativeDrawText(string text, int x, int y, uint color);

        public static void DrawText(string text, int x, int y, Color color)
        {
            NativeDrawText(text, x, y, color.ToNative());
        }
    }
}
