using System;
using System.Runtime.CompilerServices;

namespace Psp
{
    public static class State
    {
		[MethodImpl(MethodImplOptions.InternalCall)]
		extern static int NativeIsRunning();

		public static bool IsRunning()
		{
			return (NativeIsRunning() == 1);
		}
	}
}
