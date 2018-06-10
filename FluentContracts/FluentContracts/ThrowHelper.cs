using System;
using System.Runtime.CompilerServices;
using FluentContracts.Exceptions;

namespace FluentContracts
{
	internal static class ThrowHelper
    {
	    [MethodImpl(MethodImplOptions.NoInlining)]
	    public static void ThrowArgumentOutOfRangeException(string parameterName, string message = null)
	    {
		    throw new ArgumentOutOfRangeException(parameterName, message);
	    }

	    [MethodImpl(MethodImplOptions.NoInlining)]
	    public static void ThrowInvalidArgumentValueException(string parameterName, string message = null)
	    {
		    throw new InvalidArgumentValueException(parameterName, message);
	    }
	}
}
