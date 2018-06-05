using System;
using System.Runtime.CompilerServices;

namespace FluentContracts
{
	internal static class ThrowHelper
    {
	    [MethodImpl(MethodImplOptions.NoInlining)]
	    public static void ThrowArgumentOutOfRangeException(string parameterName, string message = null)
	    {
		    throw new ArgumentOutOfRangeException(parameterName, message);
	    }
	}
}
