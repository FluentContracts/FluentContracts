# FluentContracts

[![NuGet Version](https://img.shields.io/nuget/v/FluentContracts?style=flat-square&logo=nuget&logoColor=white&color=green)](https://www.nuget.org/packages/FluentContracts/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/FluentContracts?style=flat-square&logo=nuget&logoColor=white)](https://www.nuget.org/packages/FluentContracts/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue?style=flat-square)](https://github.com/FluentContracts/FluentContracts/blob/master/LICENSE)

Argument validation that reads like the rule it enforces, and fails with a message that says what
was expected of which argument.

```
dotnet add package FluentContracts
```

## A guard clause, before and after

```csharp
public void AddOrder(Order myOrder)
{
    if (myOrder == null) throw new ArgumentNullException(nameof(myOrder));
    if (myOrder.Quantity < 5) throw new ArgumentOutOfRangeException(nameof(myOrder), "Quantity cannot be less than 5");
}
```

```csharp
public void AddOrder(Order myOrder)
{
    myOrder.Must().NotBeNull().Satisfy<Order>(o => o.Quantity >= 5, "Quantity cannot be less than 5");
}
```

`Must()` starts a chain on any value. Every check returns the same chain, so checks follow one
another with no glue; `.And.` between them is optional and purely for reading. The argument's name
is captured at the call site, so exceptions point at the right parameter without `nameof`.

## What every check gives you

**A failure message that names the argument, the expectation and the value.**

```csharp
port.Must().BeBetween(1, 65535);
// ArgumentOutOfRangeException: Expected port to be between 1 and 65535, but found 70000. (Parameter 'port')

email.Must().BeEmailAddress();
// ArgumentException: Expected email to be a valid email address, but found "not-an-email". (Parameter 'email')

pages.Must().BeInAscendingOrder();
// ArgumentException: Expected pages to be in ascending order, but 5 appears before 3. (Parameter 'pages')
```

**Your own message when you want one.** It is always the last parameter, it replaces the default, and
it may use `{argument}` and `{value}`:

```csharp
port.Must().BeBetween(1, 65535, "{argument} must be a usable port, got {value}");
// ArgumentOutOfRangeException: port must be a usable port, got 70000 (Parameter 'port')

environment.Must("This should be prod").NotBe("test").NotBeEmpty();
// one message for every check in the chain; a check's own message still wins
```

**The validated value back**, so the guard and the read are one expression:

```csharp
this.port = config.Port.Must().BeBetween(1, 65535).Value();
```

`Value()` unwraps a nullable and fails a `null` argument exactly as `NotBeNull` would.

**The right exception type**, without choosing it:

| Failure | Throws |
|---|---|
| a `null` argument | `ArgumentNullException` |
| a comparison, range, sign or NaN check | `ArgumentOutOfRangeException` |
| everything else — equality, format, containment, type, your own rules | `ArgumentException` |

Or the exception you name: `myOrder.Must().NotBeNull<OrderNullException>()` and
`Satisfy<Order, OrderQuantityException>(o => o.Quantity >= 5, "...")` throw yours.

**Frames you can read.** The library hides its own frames from the stack trace, so a failure points
at the check you wrote.

## What you can check

Every type gets `BeNull`/`NotBeNull`, `Be`/`NotBe`, `BeAnyOf`/`NotBeAnyOf` and `Satisfy`; then
each adds what makes sense for it. The full list is in
[SupportedContracts.md](https://github.com/FluentContracts/FluentContracts/blob/master/docs/SupportedContracts.md).

**Numbers** — `int`, `long`, `short`, `byte`, their unsigned forms, `float`, `double`, `decimal`, and
on .NET 8+ any `INumber<T>` such as `Half`, `Int128` or `BigInteger`:

```csharp
quantity.Must().BePositive().BeLessOrEqualTo(100);
retries.Must().BeBetween(1, 5);
page.Must().BeEven();
ratio.Must().BeFinite();          // neither NaN nor infinity
```

A comparison never passes on `NaN` and never passes on `null`; `BeNaN` and `BeFinite` are how you
ask about NaN on purpose.

**Text and characters** — shape, format and content:

```csharp
name.Must().NotBeNullOrWhiteSpace().HaveLengthLessOrEqualTo(64);
code.Must().BeAlphanumeric().BeUppercase();
input.Must().BeEmailAddress();    // also BeUrl, BeIpAddress, BeGuid, BeBase64, BeHexadecimal, BeCreditCardNumber
path.Must().BeExistingFile();
slug.Must().BeMatching("^[a-z0-9-]+$");
title.Must().Contain("draft", StringComparison.OrdinalIgnoreCase);   // Ordinal by default
initial.Must().BeLetter().BeUppercase();
```

**Dates and times** — `DateTime`, `DateTimeOffset`, `TimeSpan`, and on .NET 8+ `DateOnly` and
`TimeOnly`:

```csharp
start.Must().BeInTheFuture().BeWeekday();
deadline.Must().BeBetween(start, end);
timeout.Must().BeLongerThan(TimeSpan.FromSeconds(1));
stamp.Must().BeUtc();
booking.Must().BeInCurrentYear().NotBeInDecember();
```

Checks that need the current time take a clock, so tests can pin it:
`start.Must(dateTimeProvider: clock).BeInTheFuture()`.

**Collections and dictionaries** — arrays, `IList<T>`, `IDictionary<TKey, TValue>`:

```csharp
items.Must().NotBeEmpty().HaveCountLessOrEqualTo(100).HaveUniqueItems();
tags.Must().Contain("public").NotContainNull();
scores.Must().BeInDescendingOrder().AllSatisfy(s => s >= 0);
settings.Must().ContainKey("region").NotContainKey("legacy");
```

`BeAnyOf`, `Contain` and `ContainAnyOf` take one value or one bracketed set:
`state.Must().BeAnyOf(["draft", "published"], "Not a known state")`.

**Enums, GUIDs, booleans**:

```csharp
role.Must().BeDefined().NotBe(Role.Guest);
flags.Must().HaveFlag(Permissions.Read);
id.Must().NotBeEmpty();
enabled.Must().BeTrue();
```

**Files, directories, streams and URIs**:

```csharp
file.Must().Exist().NotBeEmpty().HaveExtension(".json").HaveSizeLessThan(1_000_000);
folder.Must().Exist().NotBeReadOnly();
stream.Must().BeReadable().BeSeekable();
endpoint.Must().BeAbsolute().BeHttps().HaveHost("api.example.com");
```

**Any object** — null, type, and a rule of your own:

```csharp
payload.Must().NotBeNull().BeOfType<OrderPlaced>();
handler.Must().BeAssignableTo<IHandler>();
myOrder.Must().Satisfy<Order>(o => o.Quantity >= 5);
```

## Your own rules

A rule you check in more than one place is a specification: a predicate and the phrase that
completes *"Expected `{argument}` to …"*. Its failure reads exactly like a built-in check:

```csharp
static readonly ISpecification<string> ValidIban =
    Spec.From<string>(s => Iban.IsValid(s), "be a valid IBAN");

iban.Must().Satisfy(ValidIban);
// ArgumentException: Expected iban to be a valid IBAN, but found "XX00". (Parameter 'iban')
```

Rules compose — `ValidIban.And(SepaCountry)` expects *"be a valid IBAN and be in a SEPA country"*,
`ValidIban.Not()` expects *"not be a valid IBAN"* — and a rule that needs more room than a lambda
derives from `Specification<T>` and overrides `IsSatisfiedBy`.

A type of your own can get a contract of its own: derive from `ObjectContract<T, TContract>`, add
checks that return the contract, and add a `Must()` extension for the type. Every check above then
chains with yours.

## The package

- Targets `netstandard2.0` and `net8.0`: .NET Framework 4.6.1+, .NET Core, .NET 5 and later, Mono, Unity.
- No runtime dependencies.
- Trimming and Native AOT compatible on `net8.0`, verified on a trimmed and an AOT-published app.
- A chain is one small object, and on .NET 10 the JIT keeps it off the heap entirely; see
  [Benchmarks.md](https://github.com/FluentContracts/FluentContracts/blob/master/docs/Benchmarks.md).
- Ships a Roslyn analyzer for the misuses that compile but check the wrong thing (none open at the moment).

## Links

- [Repository](https://github.com/FluentContracts/FluentContracts)
- [Supported contracts](https://github.com/FluentContracts/FluentContracts/blob/master/docs/SupportedContracts.md)
- [Changelog](https://github.com/FluentContracts/FluentContracts/blob/master/CHANGELOG.md)
- [Issues](https://github.com/FluentContracts/FluentContracts/issues) and
  [Discussions](https://github.com/FluentContracts/FluentContracts/discussions)
- [Contributing](https://github.com/FluentContracts/FluentContracts/blob/master/CONTRIBUTING.md)

## License

[MIT](https://github.com/FluentContracts/FluentContracts/blob/master/LICENSE) © Todor Todorov
