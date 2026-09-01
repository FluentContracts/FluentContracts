using FluentContracts.Contracts;
using FluentContracts.Contracts.Collections;
using FluentContracts.Contracts.IO;
using FluentContracts.Contracts.Numeric;
using FluentContracts.Contracts.Streams;
using FluentContracts.Contracts.Struct;
using FluentContracts.Contracts.Text;
using FluentContracts.Contracts.Web;
using FluentContracts.Infrastructure;
using FluentContracts.Validators;

namespace FluentContracts;

/// <summary>
/// Ends a contract chain with the value it just validated, so the guard and the read are one
/// expression: <c>this.port = config.Port.Must().BeBetween(1, 65535).Value();</c>
/// <para>
/// <c>Value()</c> is only reachable after at least one check has run, returns the unwrapped,
/// non-nullable value, and fails a null argument with <see cref="ArgumentNullException"/> naming
/// the argument — exactly as <c>NotBeNull</c> would, so it is itself a null check plus the unwrap.
/// </para>
/// </summary>
public static class ValueExtensions
{
    /// <summary>Returns the validated <see cref="bool"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static bool Value(this Linker<BoolContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="byte"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static byte Value(this Linker<ByteContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="char"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static char Value(this Linker<CharContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="DateTime"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static DateTime Value(this Linker<DateTimeContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="DateTimeOffset"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static DateTimeOffset Value(this Linker<DateTimeOffsetContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="decimal"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static decimal Value(this Linker<DecimalContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="double"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static double Value(this Linker<DoubleContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="float"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static float Value(this Linker<FloatContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="Guid"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static Guid Value(this Linker<GuidContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="int"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static int Value(this Linker<IntContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="long"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static long Value(this Linker<LongContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="sbyte"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static sbyte Value(this Linker<SbyteContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="short"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static short Value(this Linker<ShortContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="TimeSpan"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static TimeSpan Value(this Linker<TimeSpanContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="uint"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static uint Value(this Linker<UintContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="ulong"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static ulong Value(this Linker<UlongContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="ushort"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static ushort Value(this Linker<UshortContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated enum argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <typeparam name="TEnum">The enum type being checked.</typeparam>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static TEnum Value<TEnum>(this Linker<EnumContract<TEnum>> linker)
        where TEnum : struct, Enum
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="string"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static string Value(this Linker<StringContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue;
    }

    /// <summary>Returns the validated <see cref="Uri"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static Uri Value(this Linker<UriContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue;
    }

    /// <summary>Returns the validated <see cref="Stream"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static Stream Value(this Linker<StreamContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue;
    }

    /// <summary>Returns the validated <see cref="FileInfo"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static FileInfo Value(this Linker<FileInfoContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue;
    }

    /// <summary>Returns the validated <see cref="DirectoryInfo"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static DirectoryInfo Value(this Linker<DirectoryInfoContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue;
    }

    /// <summary>Returns the validated <see cref="IList{T}"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static IList<T> Value<T>(this Linker<ListContract<T>> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue;
    }

    /// <summary>Returns the validated <see cref="IDictionary{TKey,TValue}"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static IDictionary<TKey, TValue> Value<TKey, TValue>(this Linker<DictionaryContract<TKey, TValue>> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue;
    }

    /// <summary>Returns the validated argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <typeparam name="TArgument">The type of the argument being checked.</typeparam>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static TArgument Value<TArgument>(this Linker<ObjectContract<TArgument>> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue;
    }

#if NET8_0_OR_GREATER

    /// <summary>Returns the validated <see cref="DateOnly"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static DateOnly Value(this Linker<DateOnlyContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

    /// <summary>Returns the validated <see cref="TimeOnly"/> argument, ending the chain with the value it checked. A null argument fails with <see cref="ArgumentNullException"/>, as <c>NotBeNull</c> would.</summary>
    /// <param name="linker">The chain being ended.</param>
    /// <returns>The argument's value.</returns>
    public static TimeOnly Value(this Linker<TimeOnlyContract> linker)
    {
        var contract = linker.And;
        Validator.CheckForNotNull(contract.ArgumentValue, contract.ArgumentName);
        return contract.ArgumentValue.Value;
    }

#endif
}
