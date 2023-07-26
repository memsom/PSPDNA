namespace Psp
{
    public struct Color
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        // this uses the PSP colour encoding
        public uint ToNative()
        {
            return (uint)(((A << 24) | (R << 16) | (G << 8) | B));
        }

        public static Color FromNative(uint value)
        {
            return FromRGBA(
                       (byte)((value >> 16) & 0xFF),
                       (byte)((value >> 8) & 0xFF),
                       (byte)(value & 0xFF),
                       (byte)((value >> 24) & 0xFF));
        }

        public static Color FromRGBA(byte r, byte g, byte b, byte a = 0xff)
        {
            return new Color
            {
                R = r,
                G = g,
                B = b,
                A = a
            };
        }

        // borrowed the definitions from here:
        // https://www.rapidtables.com/web/color/RGB_Color.html
        public static Color White { get; } = FromRGBA(255, 255, 255);
        public static Color Black { get; } = FromRGBA(0, 0, 0);
        public static Color Red { get; } = FromRGBA(255, 0, 0);
        public static Color Green { get; } = FromRGBA(0, 128, 0);
        public static Color Lime { get; } = FromRGBA(0, 255, 0);
        public static Color Blue { get; } = FromRGBA(0, 0, 255);
        public static Color Cyan { get; } = FromRGBA(0, 255, 255);
        public static Color Yellow { get; } = FromRGBA(255, 255, 0);
        public static Color Magenta { get; } = FromRGBA(255, 0, 255);
        public static Color Silver { get; } = FromRGBA(192, 192, 192);
        public static Color Gray { get; } = FromRGBA(128, 128, 128);
        public static Color Maroon { get; } = FromRGBA(128, 0, 0);
        public static Color Olive { get; } = FromRGBA(128, 128, 0);
        public static Color Purple { get; } = FromRGBA(128, 0, 128);
        public static Color Teal { get; } = FromRGBA(0, 128, 128);
        public static Color Navy { get; } = FromRGBA(0, 0, 128);
    }
}
