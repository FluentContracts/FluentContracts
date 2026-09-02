using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts.Contracts.Web;

/// <summary>
/// The entry point for checks on a <see cref="System.Uri"/> argument. Obtained by calling <c>Must()</c>.
/// </summary>
/// <param name="argumentValue">The value being checked.</param>
/// <param name="argumentName">The name reported when a check fails.</param>
public class UriContract(Uri? argumentValue, string argumentName)
    : UriContract<UriContract>(argumentValue, argumentName);

/// <summary>
/// The inheritable contract for a <see cref="System.Uri"/> argument. A custom contract deriving from it
/// gets every check below and keeps them chainable.
/// </summary>
/// <typeparam name="TContract">The concrete contract type, so every check can return it and keep the chain typed.</typeparam>
public class UriContract<TContract> : EqualityContract<Uri?, TContract>
    where TContract : UriContract<TContract>
{
    /// <summary>
    /// Creates the contract. Called by <c>Must()</c> and by deriving contracts.
    /// </summary>
    /// <param name="argumentValue">The value being checked.</param>
    /// <param name="argumentName">The name reported when a check fails.</param>
    protected UriContract(Uri? argumentValue, string argumentName)
        : base(argumentValue, argumentName)
    {
    }

    /// <summary>
    /// Checks if the value of the <see cref="Uri"/> argument is an absolute URI.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract BeAbsolute(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(a => a.IsAbsoluteUri, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Uri"/> argument is a relative URI.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null</remarks>
    public TContract NotBeAbsolute(string? message = null)
    {
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        Validator.CheckGenericCondition(a => !a.IsAbsoluteUri, ArgumentValue, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the scheme of the <see cref="Uri"/> argument is <paramref name="scheme"/>.
    /// </summary>
    /// <param name="scheme">The expected scheme, compared case-insensitively</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public TContract HaveScheme(string scheme, string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(
            a => string.Equals(a.Scheme, scheme, StringComparison.OrdinalIgnoreCase),
            uri,
            ArgumentName,
            message ?? ChainMessage,
            expectation: $"have the scheme {Validator.Describe(scheme)}");
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the scheme of the <see cref="Uri"/> argument is not <paramref name="scheme"/>.
    /// </summary>
    /// <param name="scheme">The scheme the argument must not have, compared case-insensitively</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public TContract NotHaveScheme(string scheme, string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(
            a => !string.Equals(a.Scheme, scheme, StringComparison.OrdinalIgnoreCase),
            uri,
            ArgumentName,
            message ?? ChainMessage,
            expectation: $"not have the scheme {Validator.Describe(scheme)}");
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Uri"/> argument uses the HTTPS scheme.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public TContract BeHttps(string? message = null) => HaveScheme(Uri.UriSchemeHttps, message);

    /// <summary>
    /// Checks if the value of the <see cref="Uri"/> argument does not use the HTTPS scheme.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public TContract NotBeHttps(string? message = null) => NotHaveScheme(Uri.UriSchemeHttps, message);

    /// <summary>
    /// Checks if the host of the <see cref="Uri"/> argument is <paramref name="host"/>.
    /// </summary>
    /// <param name="host">The expected host, compared case-insensitively</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public TContract HaveHost(string host, string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(
            a => string.Equals(a.Host, host, StringComparison.OrdinalIgnoreCase),
            uri,
            ArgumentName,
            message ?? ChainMessage,
            expectation: $"have the host {Validator.Describe(host)}");
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the host of the <see cref="Uri"/> argument is not <paramref name="host"/>.
    /// </summary>
    /// <param name="host">The host the argument must not have, compared case-insensitively</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public TContract NotHaveHost(string host, string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(
            a => !string.Equals(a.Host, host, StringComparison.OrdinalIgnoreCase),
            uri,
            ArgumentName,
            message ?? ChainMessage,
            expectation: $"not have the host {Validator.Describe(host)}");
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the port of the <see cref="Uri"/> argument is <paramref name="port"/>.
    /// </summary>
    /// <param name="port">The expected port</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>
    /// Also checks for the argument to NOT be null and to be an absolute URI. A URI that does not state a
    /// port carries the default port of its scheme, so <c>https://host</c> has port 443.
    /// </remarks>
    public TContract HavePort(int port, string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(a => a.Port == port, uri, ArgumentName, message ?? ChainMessage,
            expectation: $"have the port {port}");
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the port of the <see cref="Uri"/> argument is not <paramref name="port"/>.
    /// </summary>
    /// <param name="port">The port the argument must not have</param>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>
    /// Also checks for the argument to NOT be null and to be an absolute URI. A URI that does not state a
    /// port carries the default port of its scheme, so <c>https://host</c> has port 443.
    /// </remarks>
    public TContract NotHavePort(int port, string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(a => a.Port != port, uri, ArgumentName, message ?? ChainMessage,
            expectation: $"not have the port {port}");
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Uri"/> argument points at the local host.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public TContract BeLoopback(string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(a => a.IsLoopback, uri, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Uri"/> argument does not point at the local host.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public TContract NotBeLoopback(string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(a => !a.IsLoopback, uri, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Uri"/> argument is a file URI.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public TContract BeFile(string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(a => a.IsFile, uri, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
    }

    /// <summary>
    /// Checks if the value of the <see cref="Uri"/> argument is not a file URI.
    /// </summary>
    /// <param name="message">The optional message to include in the exception if the condition is not satisfied.</param>
    /// <returns>The contract, for chaining more checks</returns>
    /// <remarks>Also checks for the argument to NOT be null and to be an absolute URI</remarks>
    public TContract NotBeFile(string? message = null)
    {
        var uri = GetAbsoluteArgument(message);
        Validator.CheckGenericCondition(a => !a.IsFile, uri, ArgumentName, message ?? ChainMessage);
        return (TContract)this;
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
        Validator.CheckForNotNull(ArgumentValue, ArgumentName, message ?? ChainMessage);
        // The expectation is spelled out because the caller's name here would be this helper's,
        // not the check the user wrote.
        Validator.CheckGenericCondition(a => a.IsAbsoluteUri, ArgumentValue, ArgumentName, message ?? ChainMessage,
            expectation: "be an absolute URI");
        return ArgumentValue;
    }
}
