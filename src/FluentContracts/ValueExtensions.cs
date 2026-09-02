#if NET8_0_OR_GREATER
using System.Numerics;
#endif
using FluentContracts.Contracts;
using FluentContracts.Contracts.Collections;
using FluentContracts.Contracts.IO;
using FluentContracts.Contracts.Numeric;
using FluentContracts.Contracts.Streams;
using FluentContracts.Contracts.Struct;
using FluentContracts.Contracts.Text;
using FluentContracts.Contracts.Web;
using FluentContracts.Validators;

namespace FluentContracts;

/// <summary>
/// Ends a contract chain with the value it just validated, so the guard and the read are one
/// expression: <c>this.port = config.Port.Must().BeBetween(1, 65535).Value();</c>
/// <para>
/// <c>Value()</c> returns the unwrapped, non-nullable value, and fails a null argument with
/// <see cref="ArgumentNullException"/> naming the argument — exactly as <c>NotBeNull</c> would, so
/// it is itself a null check plus the unwrap, and <c>x.Must().Value()</c> alone is a valid guard.
/// </para>
/// </summary>
public static class ValueExtensions
{
    /// <summary>Returns the validated <see cref="bool"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static bool Value(this BoolContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="byte"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static byte Value(this ByteContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="char"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static char Value(this CharContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="DateTime"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static DateTime Value(this DateTimeContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="DateTimeOffset"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static DateTimeOffset Value(this DateTimeOffsetContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="decimal"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static decimal Value(this DecimalContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="double"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static double Value(this DoubleContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="float"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static float Value(this FloatContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="Guid"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static Guid Value(this GuidContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="int"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static int Value(this IntContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="long"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static long Value(this LongContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="sbyte"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static sbyte Value(this SbyteContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="short"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static short Value(this ShortContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="TimeSpan"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static TimeSpan Value(this TimeSpanContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="uint"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static uint Value(this UintContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="ulong"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static ulong Value(this UlongContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="ushort"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static ushort Value(this UshortContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated enum argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <typeparam name="TEnum">The enum type being checked.</typeparam>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static TEnum Value<TEnum>(this EnumContract<TEnum> contract)
        where TEnum : struct, Enum
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="string"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static string Value(this StringContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue;
    }

    /// <summary>Returns the validated <see cref="Uri"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static Uri Value(this UriContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue;
    }

    /// <summary>Returns the validated <see cref="Stream"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static Stream Value(this StreamContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue;
    }

    /// <summary>Returns the validated <see cref="FileInfo"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static FileInfo Value(this FileInfoContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue;
    }

    /// <summary>Returns the validated <see cref="DirectoryInfo"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static DirectoryInfo Value(this DirectoryInfoContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue;
    }

    /// <summary>Returns the validated <see cref="IList{T}"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static IList<T> Value<T>(this ListContract<T> contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue;
    }

    /// <summary>Returns the validated <see cref="IDictionary{TKey,TValue}"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static IDictionary<TKey, TValue> Value<TKey, TValue>(this DictionaryContract<TKey, TValue> contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue;
    }

    /// <summary>Returns the validated argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <typeparam name="TArgument">The type of the argument being checked.</typeparam>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static TArgument Value<TArgument>(this ObjectContract<TArgument> contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue;
    }

#if NET8_0_OR_GREATER

    /// <summary>Returns the validated <see cref="DateOnly"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static DateOnly Value(this DateOnlyContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="TimeOnly"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static TimeOnly Value(this TimeOnlyContract contract)
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated number, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <typeparam name="T">The number type.</typeparam>
    /// <param name="contract">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static T Value<T>(this NumberContract<T> contract)
        where T : struct, INumber<T>
    {
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

#endif
}
