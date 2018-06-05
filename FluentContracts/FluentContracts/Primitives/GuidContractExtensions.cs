using System;
using System.Runtime.CompilerServices;

namespace FluentContracts.Primitives
{
	public static class GuidContractExtensions
    {
	    public static GuidContract Must(
		    this Guid guidArgument, 
		    [CallerLineNumber] int lineNumber = 0,
		    [CallerFilePath] string filePath = "") 
	    {
		    return new GuidContract(guidArgument, lineNumber, filePath);
	    }
    }
}
