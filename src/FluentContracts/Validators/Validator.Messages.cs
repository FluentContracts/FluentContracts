using System.Collections;
using System.Globalization;
using System.Text;

namespace FluentContracts.Validators;

internal static partial class Validator
{
    private const string NullDescription = "<null>";
    private const string FallbackExpectation = "satisfy the condition";
    private const int MaxDescribedStringLength = 64;
    private const int MaxDescribedItems = 5;

    /// <summary>
    /// The default failure message when the caller supplied none: what was expected of the argument.
    /// </summary>
    public static string Expected(string argumentName, string expectation) =>
        $"Expected {argumentName} to {expectation}.";

    /// <summary>
    /// The default failure message when the caller supplied none: what was expected of the argument,
    /// and the value that failed the expectation.
    /// </summary>
    public static string Expected(string argumentName, string expectation, object? actualValue) =>
        $"Expected {argumentName} to {expectation}, but found {Describe(actualValue)}.";

    private const string ArgumentToken = "{argument}";
    private const string ValueToken = "{value}";

    /// <summary>
    /// A caller-supplied message with its tokens filled: <c>{argument}</c> becomes the argument's
    /// name and <c>{value}</c> its rendered value, so one message can serve any argument. Returns
    /// null when there is no message, so it composes as <c>Custom(...) ?? Expected(...)</c>. A
    /// message without tokens is returned as is; the scan only runs on the failure path.
    /// </summary>
    public static string? Custom(string? message, string argumentName, object? actualValue)
    {
        if (message is null || message.IndexOf('{') < 0) return message;

        return message
            .Replace(ArgumentToken, argumentName)
            .Replace(ValueToken, Describe(actualValue));
    }

    /// <summary>
    /// Renders a value for a failure message: strings quoted and truncated, formattable values in the
    /// invariant culture, collections as their first few items. Messages end up in logs across
    /// machines, so the rendering must not depend on the current culture.
    /// </summary>
    public static string Describe(object? value) =>
        value switch
        {
            null => NullDescription,
            string text => DescribeString(text),
            char character => $"'{character}'",
            bool boolean => boolean ? "true" : "false",
            IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture),
            IEnumerable collection => DescribeCollection(collection),
            _ => value.ToString() ?? NullDescription
        };

    private static string DescribeString(string value) =>
        value.Length <= MaxDescribedStringLength
            ? $"\"{value}\""
            : $"\"{value.Substring(0, MaxDescribedStringLength)}…\"";

    private static string DescribeCollection(IEnumerable collection)
    {
        var builder = new StringBuilder("[");
        var count = 0;

        foreach (var item in collection)
        {
            if (count == MaxDescribedItems)
            {
                builder.Append(", …");
                break;
            }

            if (count > 0) builder.Append(", ");
            builder.Append(Describe(item));
            count++;
        }

        return builder.Append(']').ToString();
    }

    /// <summary>
    /// Turns a check name into the expectation phrase of its failure message:
    /// <c>NotBeUppercase</c> becomes <c>not be uppercase</c>. This is what lets
    /// <see cref="CheckGenericCondition{T}"/> name the expectation without every
    /// call site writing one out.
    /// </summary>
    private static string HumaniseCheckName(string checkName)
    {
        if (string.IsNullOrEmpty(checkName)) return FallbackExpectation;

        var builder = new StringBuilder(checkName.Length + 8);

        foreach (var character in checkName)
        {
            if (char.IsUpper(character))
            {
                if (builder.Length > 0) builder.Append(' ');
                builder.Append(char.ToLowerInvariant(character));
            }
            else
            {
                builder.Append(character);
            }
        }

        return builder.ToString();
    }
}
