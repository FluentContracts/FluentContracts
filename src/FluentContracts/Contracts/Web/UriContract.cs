using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Web;

public class UriContract(Uri? argumentValue, string argumentName)
    : UriContract<UriContract>(argumentValue, argumentName);

public class UriContract<TContract> : EqualityContract<Uri?, TContract>
    where TContract : UriContract<TContract>
{
    private readonly Linker<TContract> _linker;

    protected UriContract(Uri? argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
        _linker = new Linker<TContract>((TContract)this);
    }

    /// <summary>
    /// Checks if the value of the <see cref="Uri"/> argument is an absolute URI.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> BeAbsolute(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.IsAbsoluteUri, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Uri"/> argument is a relative URI.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public Linker<TContract> NotBeAbsolute(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => !a.IsAbsoluteUri, ArgumentValue, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the scheme of the <see cref="Uri"/> argument is <paramref name="scheme"/>.
    /// </summary>
    /// <param name="scheme">The expected scheme, compared case-insensitively</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public Linker<TContract> HaveScheme(string scheme, string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(
            a => string.Equals(a.Scheme, scheme, StringComparison.OrdinalIgnoreCase),
            uri,
            ArgumentName,
            message);
        return _linker;
    }

    /// <summary>
    /// Checks if the scheme of the <see cref="Uri"/> argument is not <paramref name="scheme"/>.
    /// </summary>
    /// <param name="scheme">The scheme the argument must not have, compared case-insensitively</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public Linker<TContract> NotHaveScheme(string scheme, string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(
            a => !string.Equals(a.Scheme, scheme, StringComparison.OrdinalIgnoreCase),
            uri,
            ArgumentName,
            message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Uri"/> argument uses the HTTPS scheme.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public Linker<TContract> BeHttps(string? message = null) => HaveScheme(Uri.UriSchemeHttps, message);

    /// <summary>
    /// Checks if the value of the <see cref="Uri"/> argument does not use the HTTPS scheme.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public Linker<TContract> NotBeHttps(string? message = null) => NotHaveScheme(Uri.UriSchemeHttps, message);

    /// <summary>
    /// Checks if the host of the <see cref="Uri"/> argument is <paramref name="host"/>.
    /// </summary>
    /// <param name="host">The expected host, compared case-insensitively</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public Linker<TContract> HaveHost(string host, string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(
            a => string.Equals(a.Host, host, StringComparison.OrdinalIgnoreCase),
            uri,
            ArgumentName,
            message);
        return _linker;
    }

    /// <summary>
    /// Checks if the host of the <see cref="Uri"/> argument is not <paramref name="host"/>.
    /// </summary>
    /// <param name="host">The host the argument must not have, compared case-insensitively</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public Linker<TContract> NotHaveHost(string host, string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(
            a => !string.Equals(a.Host, host, StringComparison.OrdinalIgnoreCase),
            uri,
            ArgumentName,
            message);
        return _linker;
    }

    /// <summary>
    /// Checks if the port of the <see cref="Uri"/> argument is <paramref name="port"/>.
    /// </summary>
    /// <param name="port">The expected port</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>
    /// Also checks for the argument to NOT be null and to be an absolute URI. A URI that does not state a
    /// port carries the default port of its scheme, so <c>https://host</c> has port 443.
    /// </remarks>
    public Linker<TContract> HavePort(int port, string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(a => a.Port == port, uri, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the port of the <see cref="Uri"/> argument is not <paramref name="port"/>.
    /// </summary>
    /// <param name="port">The port the argument must not have</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>
    /// Also checks for the argument to NOT be null and to be an absolute URI. A URI that does not state a
    /// port carries the default port of its scheme, so <c>https://host</c> has port 443.
    /// </remarks>
    public Linker<TContract> NotHavePort(int port, string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(a => a.Port != port, uri, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Uri"/> argument points at the local host.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public Linker<TContract> BeLoopback(string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(a => a.IsLoopback, uri, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Uri"/> argument does not point at the local host.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public Linker<TContract> NotBeLoopback(string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(a => !a.IsLoopback, uri, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Uri"/> argument is a file URI.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public Linker<TContract> BeFile(string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(a => a.IsFile, uri, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Uri"/> argument is not a file URI.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>Linker for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public Linker<TContract> NotBeFile(string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(a => !a.IsFile, uri, ArgumentName, message);
        return _linker;
    }

    /// <summary>
    /// Returns the argument once it is known to be a non-null, absolute URI.
    /// </summary>
    /// <remarks>
    /// <see cref="Uri.Scheme"/>, <see cref="Uri.Host"/>, <see cref="Uri.Port"/> and
    /// <see cref="Uri.IsLoopback"/> all throw <see cref="InvalidOperationException"/> for a relative URI,
    /// so every check reading them fails the contract first rather than letting that escape.
    /// </remarks>
    private Uri GetAbsoluteArgument(string? message)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message);
        Validator.CheckGenericCondition(a => a.IsAbsoluteUri, ArgumentValue, ArgumentName, message);
        return ArgumentValue;
    }
}
