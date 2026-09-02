namespace FluentContracts.Specifications;

/// <summary>
/// A named, reusable rule a value must satisfy — the extensible half of the library. A contract
/// runs one through <c>Satisfy(specification)</c>; the rule says what it checks and, in
/// <see cref="Expectation"/>, what it expected, so its failures read like the built-in checks':
/// <c>Expected iban to be a valid IBAN, but found "XX00".</c>
/// </summary>
/// <typeparam name="T">The type of value the rule applies to. Contravariant, so a rule written for a
/// base type applies to an argument of a derived type.</typeparam>
public interface ISpecification<in T>
{
    /// <summary>Whether <paramref name="value"/> satisfies the rule.</summary>
    /// <param name="value">The value being checked.</param>
    /// <returns><see langword="true"/> when the rule holds.</returns>
    bool IsSatisfiedBy(T value);

    /// <summary>
    /// The expectation phrase, completing <c>Expected {argument} to …</c>: <c>be a valid IBAN</c>,
    /// <c>have at least 5 items</c>. A phrase rather than a sentence, so rules compose readably —
    /// <c>be a valid IBAN and be in a SEPA country</c> — and the library can add the argument's name
    /// and value. <see langword="null"/> falls back to <c>satisfy the given condition</c>.
    /// </summary>
    string? Expectation { get; }
}
