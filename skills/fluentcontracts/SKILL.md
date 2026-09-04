---
name: fluentcontracts
description: Write and apply FluentContracts argument validation in C#. Use when adding or reviewing guard clauses, argument checks, parameter validation or precondition checks in a .NET codebase — replacing hand-written `if (x == null) throw new ArgumentNullException(...)` blocks with `x.Must()` chains, choosing the right check, writing failure messages, or adding a reusable rule as an `ISpecification<T>`. Also use when the project already references the FluentContracts NuGet package and new code needs guards that match it.
---

# Apply FluentContracts in C# code

FluentContracts replaces hand-written guard clauses with one readable chain that
throws the right exception, names the argument and says what was expected.

```csharp
public void AddOrder(Order myOrder)
{
    myOrder.Must().NotBeNull().Satisfy<Order>(o => o.Quantity >= 5, "Quantity cannot be less than 5");
}
```

## Before you write a check, confirm it exists

Do **not** invent a check name. The library has a large but finite surface, and
a guessed name is a compile error at best and the wrong assertion at worst.

1. **In the editor**, type `value.Must().` and read IntelliSense — the package
   ships XML documentation for every public member, so the list is authoritative
   for the version the project actually references.
2. **On the command line or from a repository**, the generated catalogue of every
   contract and every check is
   [`docs/SupportedContracts.md`](https://raw.githubusercontent.com/FluentContracts/FluentContracts/master/docs/SupportedContracts.md).
   It is produced by reflection over the built assembly, so it is the complete
   list, and it tracks `master` — a check listed there may not be in the version
   the project has pinned.
3. **The naming system** and the rules that decide overloads, messages and
   exception types are in [`references/cheatsheet.md`](references/cheatsheet.md).
   Read it before writing more than a single check.

If no check fits, do not reach for a raw `if`/`throw` next to a chain — use
`Satisfy` or a specification (below). Both fail through the same machinery, so
the message and the argument name still come out right.

## Adding it to a project

```
dotnet add package FluentContracts
```

`netstandard2.0` and `net8.0`, no runtime dependencies. Then `using FluentContracts;`
— `Must()` is an extension method, and every contract type is reached through it.

## The shape of a guard

**Start with `Must()`.** It captures the argument's name at the call site through
`[CallerArgumentExpression]`, so there is no `nameof` to keep in sync, and it
returns the contract for the argument's static type — `int` gets `IntContract`,
`string` gets `StringContract`, `IList<T>` gets `ListContract<T>`, and so on.

**Chain the checks.** Every check returns the same contract, so checks follow one
another directly. `.And.` is an identity property kept only for reading; use it
when it helps a long chain and leave it out otherwise.

```csharp
public void Connect(string host, int port, IList<string> fallbacks)
{
    host.Must().NotBeNullOrWhiteSpace().And.NotBeMatching(@"\s");
    port.Must().BeBetween(1, 65535);
    fallbacks.Must().NotBeNull().NotBeEmpty().NotContainNull();
}
```

**End with `Value()` when the guard and the read are the same statement.** It
returns the unwrapped, non-nullable value and fails a `null` argument with
`ArgumentNullException` exactly as `NotBeNull` would — so `x.Must().Value()` on
its own is a complete null guard.

```csharp
this.port = config.Port.Must().BeBetween(1, 65535).Value();
this.host = config.Host.Must().NotBeNullOrEmpty().Value();
```

This is the idiom to prefer in a constructor: it collapses the guard and the
assignment, and it removes the nullable warning at the field without a `!`.

## Messages

The `message` parameter is always **last**, after every operand, and it replaces
the default message for that check. It may use `{argument}` and `{value}`, which
are filled in on the failure path:

```csharp
port.Must().BeBetween(1, 65535, "{argument} must be a usable port, got {value}");
```

A message for the **whole chain** goes to `Must()` as its first argument. A
check's own message still wins for that check:

```csharp
environment.Must("This should be prod").NotBe("test").NotBeEmpty();
```

Write the default message first — it already names the argument, the expectation
and the actual value, and it is usually better than a hand-written one. Add a
message only when the *domain* reason is not obvious from the check itself
("must be a usable port"), not to restate the check ("must be between 1 and 65535").

## The one overload trap

A check that takes **several candidate values** has exactly three overloads — one
value, a set, and a set followed by a message — and never `params`:

```csharp
status.Must().BeAnyOf("draft");                                  // one value
status.Must().BeAnyOf(["draft", "published"]);                   // a set
status.Must().BeAnyOf(["draft", "published"], "unknown status"); // a set, then the message
```

So **a message only ever follows a bracketed set**. `BeAnyOf("draft", "published")`
does not compile — deliberately, because on a `string` it would otherwise bind
`"published"` as the message and silently check the wrong thing.

Fixed-arity checks are unambiguous and keep `(operands…, message)` with no
brackets: `NotBe("test", "This should be prod")`, `BeBetween(1, 65535, "…")`.

## Which exception you get

Pick the check that matches what you are asserting and the exception follows; do
not try to steer it.

| You get | For |
| --- | --- |
| `ArgumentNullException` | a null argument — `NotBeNull`, `Value()`, and the implicit null check in `Satisfy` |
| `ArgumentOutOfRangeException` | ordinal checks only — comparisons, ranges, sign, the NaN policy |
| `ArgumentException` | everything else |

**Null and NaN.** Ordering comparisons (`BeGreaterThan`, `BeLessThan`, `BeBetween`,
`BePositive`, `BeNegative`, …) reject `null` with `ArgumentNullException` and
`NaN` with `ArgumentOutOfRangeException` — a `NaN` satisfies no ordering
comparison, as IEEE requires. Equality checks and the explicit null checks accept
`null`. `BeNaN` and `BeFinite` are how you ask about `NaN` on purpose.

**Your own exception type** is available on `Satisfy`, and only there:

```csharp
myOrder.Must()
    .NotBeNull()
    .Satisfy<Order, OrderQuantityException>(o => o.Quantity >= 5, "Order quantity cannot be less than 5");
```

## Rules you check more than once

A predicate that appears in two places is a **specification**: a rule plus the
phrase completing *"Expected `{argument}` to …"*. It fails exactly like a
built-in check.

```csharp
static readonly ISpecification<string> ValidIban =
    Spec.From<string>(s => Iban.IsValid(s), "be a valid IBAN");

iban.Must().Satisfy(ValidIban);
// ArgumentException: Expected iban to be a valid IBAN, but found "XX00". (Parameter 'iban')
```

Specifications compose, and the phrase composes with them — `ValidIban.And(SepaCountry)`
expects *"be a valid IBAN and be in a SEPA country"*, `.Or(...)` and `.Not()`
likewise. A rule needing more room than a lambda derives from `Specification<T>`
and overrides `IsSatisfiedBy`.

The phrase is a **fragment, never a sentence**, and never names the argument or
the value — the library adds both. Write `"be a valid IBAN"`, not
`"iban must be a valid IBAN"`.

## Applying it to existing code

When replacing hand-written guards, work one method at a time and keep the
observable behaviour:

1. **Match the exception type.** A guard throwing `ArgumentNullException` maps to
   `NotBeNull` or `Value()`; a range guard throwing `ArgumentOutOfRangeException`
   maps to a comparison check. If the existing code throws a *different* type on
   purpose, keep it with `Satisfy<T, TException>` rather than changing what
   callers catch.
2. **Drop the `nameof`.** `Must()` captures the name from the call site. Passing
   an explicit `argumentName` is possible but is only for the rare case where the
   captured expression is not the name you want to report.
3. **Do not weaken a check to make it fit.** If nothing in the library expresses
   the rule, `Satisfy` or a specification does — that is what they are for.
4. **Leave non-argument validation alone.** FluentContracts is for arguments and
   preconditions. Business-rule validation that must collect several failures and
   report them together is a different job; use the project's existing validator
   for that.

## What not to do

- **Do not guess a check name.** Confirm it in IntelliSense or the catalogue.
- **Do not use `BeAnyOf(a, b)`** — see the overload trap above.
- **Do not mix a raw `throw` into a chain.** Use `Satisfy` so the failure carries
  the argument name and reads like every other one.
- **Do not use it for test assertions.** It throws `ArgumentException`s; use
  FluentAssertions or the test framework's asserts there.
- **Do not add a message that repeats the check.** The default already names the
  argument, the expectation and the value.
