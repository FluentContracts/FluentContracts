using System;

namespace FluentContracts.Primitives
{
	public class GuidContract : Contract<Guid>
    {
	    public GuidContract(Guid argument, int lineNumber = 0, string filePath = "") 
		    : base(argument, lineNumber, filePath)
	    {
	    }

	    public void NotBeEmpty()
	    {
		    Validator.CheckOutOfRange(Argument != Guid.Empty, CallerName);
	    }
    }
}
