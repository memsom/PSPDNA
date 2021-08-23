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

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static System.IntPtr NativeLoadSurface(string fileName);

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static System.IntPtr NativeCreateTexture(System.IntPtr surface);

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static int NativeDrawTexture(System.IntPtr texture, int x, int y, int w, int h);
  
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static int NativeSetColorKey(System.IntPtr surface, int flag, uint key);

        public static Surface CreateSurface(string bitmap, bool useKey = false, Color key = default)
        {
            var surface = NativeLoadSurface(bitmap);
            if (useKey)
            {
                NativeSetColorKey(surface, useKey ? 1: 0, key.ToNative());
            }

            return new Surface { Handle = surface };
        }

        public static Texture CreateTexture(Surface surface)
        {
            var handle = surface.Handle;
            return new Texture
            {
                Surface = surface,
                Handle = NativeCreateTexture(handle)
            };
        }

        public static int DrawTexture(Texture texture, int x, int y, int w, int h)
        {
            var handle = texture.Handle;
            return NativeDrawTexture(handle, x, y, w, h);
        }
    }

    // this is a first pass... this will probably change
    public class Surface
    {
        public System.IntPtr Handle { get; internal set; } = System.IntPtr.Zero;
    }

    // this is a first pass... this will probably change
    public class Texture
    {
        public Surface Surface { get; internal set; }
        public System.IntPtr Handle { get; internal set; } = System.IntPtr.Zero;
    }
}
