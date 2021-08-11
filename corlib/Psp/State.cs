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

		// this is a hack, but if we set this we will
		// be able to chain in the next app when the
        // last app starts.
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern static void SetAppName(string appname);
	}
}
