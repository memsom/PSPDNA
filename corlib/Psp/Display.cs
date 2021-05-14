using System.Runtime.CompilerServices;

namespace Psp
{
    public static class Display
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern public static int WaitVblankStart();
    }
}
