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
    }
}
