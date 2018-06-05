using System.Diagnostics;

namespace FluentContracts
{
	public abstract class Contract<T>
    {
	    protected Contract(T argument, int lineNumber = 0, string filePath = "")
	    {
		    Argument = argument;
		    CallerName = CallerNameLocator.GetNameOrDefault(filePath, lineNumber);
	    }

		protected T Argument { get; }
	    protected string CallerName { get; }
	}
}
