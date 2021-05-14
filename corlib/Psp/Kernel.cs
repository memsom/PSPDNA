using System.Runtime.CompilerServices;

namespace Psp
{
    public static class Kernel
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern public static void ExitGame();
    }
}
