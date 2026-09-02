namespace FluentContracts.Specifications;

/// <summary>
/// A base for a specification written as a class: implement <see cref="IsSatisfiedBy"/> and pass
/// the expectation phrase to the constructor. For a one-line rule, <see cref="Spec.From{T}"/> needs
/// no class at all.
/// </summary>
/// <typeparam name="T">The type of value the rule applies to.</typeparam>
public abstract class Specification<T> : ISpecification<T>
{
    /// <summary>Creates the specification with its expectation phrase.</summary>
    /// <param name="expectation">The phrase completing <c>Expected {argument} to …</c>, or
    /// <see langword="null"/> for the generic fallback.</param>
    protected Specification(string? expectation = null)
    {
        Expectation = expectation;
    }

    /// <inheritdoc/>
    public string? Expectation { get; }

    /// <inheritdoc/>
    public abstract bool IsSatisfiedBy(T value);
}

/// <summary>
/// Builds specifications without declaring a class, and composes them.
/// </summary>
public static class Spec
{
    /// <summary>
    /// A specification from a predicate and an expectation phrase — the one-line form of a reusable
    /// rule: <c>Spec.From&lt;Order&gt;(o => o.Quantity >= 5, "have at least 5 items")</c>.
    /// </summary>
    /// <typeparam name="T">The type of value the rule applies to.</typeparam>
    /// <param name="predicate">The rule.</param>
    /// <param name="expectation">The phrase completing <c>Expected {argument} to …</c>.</param>
    /// <returns>The specification.</returns>
    public static ISpecification<T> From<T>(Func<T, bool> predicate, string? expectation = null)
    {
        Validators.Validator.CheckForNotNull(predicate, nameof(predicate));

        return new PredicateSpecification<T>(predicate, expectation);
    }

    /// <summary>A specification satisfied only when both <paramref name="left"/> and <paramref name="right"/> are.</summary>
    /// <typeparam name="T">The type of value the rules apply to.</typeparam>
    /// <param name="left">The first rule.</param>
    /// <param name="right">The second rule.</param>
    /// <returns>The composed specification, expecting <c>{left} and {right}</c>.</returns>
    public static ISpecification<T> And<T>(this ISpecification<T> left, ISpecification<T> right)
    {
        Validators.Validator.CheckForNotNull(left, nameof(left));
        Validators.Validator.CheckForNotNull(right, nameof(right));

        return new PredicateSpecification<T>(
            value => left.IsSatisfiedBy(value) && right.IsSatisfiedBy(value),
            Join(left, right, "and"));
    }

    /// <summary>A specification satisfied when either <paramref name="left"/> or <paramref name="right"/> is.</summary>
    /// <typeparam name="T">The type of value the rules apply to.</typeparam>
    /// <param name="left">The first rule.</param>
    /// <param name="right">The second rule.</param>
    /// <returns>The composed specification, expecting <c>{left} or {right}</c>.</returns>
    public static ISpecification<T> Or<T>(this ISpecification<T> left, ISpecification<T> right)
    {
        Validators.Validator.CheckForNotNull(left, nameof(left));
        Validators.Validator.CheckForNotNull(right, nameof(right));

        return new PredicateSpecification<T>(
            value => left.IsSatisfiedBy(value) || right.IsSatisfiedBy(value),
            Join(left, right, "or"));
    }

    /// <summary>A specification satisfied exactly when <paramref name="specification"/> is not.</summary>
    /// <typeparam name="T">The type of value the rule applies to.</typeparam>
    /// <param name="specification">The rule to negate.</param>
    /// <returns>The negated specification, expecting <c>not {expectation}</c>.</returns>
    public static ISpecification<T> Not<T>(this ISpecification<T> specification)
    {
        Validators.Validator.CheckForNotNull(specification, nameof(specification));

        return new PredicateSpecification<T>(
            value => !specification.IsSatisfiedBy(value),
            specification.Expectation is null ? null : "not " + specification.Expectation);
    }

    private static string? Join<T>(ISpecification<T> left, ISpecification<T> right, string word) =>
        left.Expectation is null || right.Expectation is null
            ? left.Expectation ?? right.Expectation
            : $"{left.Expectation} {word} {right.Expectation}";

    private sealed class PredicateSpecification<T>(Func<T, bool> predicate, string? expectation)
        : Specification<T>(expectation)
    {
        public override bool IsSatisfiedBy(T value) => predicate(value);
    }
}
