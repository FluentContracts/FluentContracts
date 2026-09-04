# FluentContracts cheatsheet

The rules that decide how a check is written, and the catalogue of what exists.
Read [`../SKILL.md`](../SKILL.md) first for the shape of a guard.

> The [catalogue](#catalogue) below is generated from the library, so it lists every contract and
> check that exists — but it tracks the repository, and the project you are working in may be on an
> older version. Use it to know a check exists; confirm it, and its exact signature, in the project's
> own IntelliSense.

## Which contract an argument gets

`Must()` is an extension method resolved on the argument's **static** type. There
is no `Must<T>()` to call explicitly — declare the variable or parameter as the
type you want checked.

| Argument type | Contract | Notes |
| --- | --- | --- |
| `string` | `StringContract` | |
| `char`, `char?` | `CharContract` | |
| `int`/`uint`/`long`/`ulong`/`short`/`ushort`/`byte`/`sbyte`, and nullable | `IntContract`, `UintContract`, … | one per type |
| `decimal`, `double`, `float`, and nullable | `DecimalContract`, `DoubleContract`, `FloatContract` | |
| any other `INumber<T>` (`Half`, `Int128`, `BigInteger`, `nint`, your own) | `NumberContract<T>` | `net8.0` only; hand-written contracts still win the overload |
| `bool`, `bool?` | `BoolContract` | |
| `Guid`, `Guid?` | `GuidContract` | |
| any `enum`, and nullable | `EnumContract<TEnum>` | |
| `DateTime`, `DateTimeOffset`, `TimeSpan`, and nullable | `DateTimeContract`, `DateTimeOffsetContract`, `TimeSpanContract` | |
| `DateOnly`, `TimeOnly`, and nullable | `DateOnlyContract`, `TimeOnlyContract` | `net8.0` only |
| `IList<T>`, `T[]` | `ListContract<T>` | an array binds to the same contract |
| `IDictionary<TKey, TValue>` | `DictionaryContract<TKey, TValue>` | |
| `Uri` | `UriContract` | |
| `Stream` | `StreamContract` | |
| `FileInfo`, `DirectoryInfo` | `FileInfoContract`, `DirectoryInfoContract` | |
| anything else | `ObjectContract<object>` | the fallback; use `Satisfy<T>` to get at the typed value |

An `IEnumerable<T>` that is not an `IList<T>` falls through to
`ObjectContract<object>`. Take the parameter as `IList<T>` (or materialise it)
when you want the collection checks.

## The inheritance chain

Every contract inherits every check above it:

```
BaseContract        Satisfy, And
  └ NullableContract    (Not)BeNull
      └ ObjectContract      (Not)BeOfType, (Not)BeAssignableTo
          └ EqualityContract    (Not)Be, (Not)BeAnyOf
              └ the per-type contracts
```

So `NotBeNull` and `Be` are available on almost everything, and the per-type
catalogue below lists only what each contract *adds*.

## Naming families

The names are regular. Once you know the family, you can predict the name — but
still confirm it exists before using it.

| Family | Shape | Examples |
| --- | --- | --- |
| State | `Be…` / `NotBe…` | `BeEmpty`, `NotBeNull`, `BeUtc` |
| Ordering | `BeGreaterThan`, `BeGreaterOrEqualTo`, `BeLessThan`, `BeLessOrEqualTo`, `BeBetween` | on every ordered type |
| Membership | `BeAnyOf` / `NotBeAnyOf`, `Contain` / `NotContain` | see the overload rule below |
| Size | `Have<Dimension>EqualTo` / `…GreaterThan` / `…GreaterOrEqualTo` / `…LessThan` / `…LessOrEqualTo` / `…Between` | `HaveCount*` (collections), `HaveLength*` (strings), `HaveSize*` (files) |
| Property | `Have…` | `HaveHost`, `HaveExtension`, `HaveFlag` |
| Custom | `Satisfy` | predicate, or an `ISpecification<T>` |

`(Not)X` in the catalogue means both `X` and `NotX` exist.

## Overload shape

**Multi-value checks** — exactly three overloads, never `params`:

```csharp
public TContract BeAnyOf(T expectedValue);
public TContract BeAnyOf(IEnumerable<T> expectedValues, string? message = null);
```

There is deliberately no `(T value, string message)` overload: with it,
`BeAnyOf("draft", "published")` on a string would compile and silently take
`"published"` as the message. Without it, and without `params`, the rule is one
sentence — **a message only ever follows a bracketed set**.

```csharp
status.Must().BeAnyOf("draft");                                  // ok
status.Must().BeAnyOf(["draft", "published"]);                   // ok
status.Must().BeAnyOf(["draft", "published"], "unknown status"); // ok
status.Must().BeAnyOf("draft", "published");                     // compile error, on purpose
```

**Fixed-arity checks** keep `(operands…, string? message = null)` — the compiler
knows the arity, so there is one way to bind them:

```csharp
name.Must().NotBe("test", "This should be prod");
port.Must().BeBetween(1, 65535, "{argument} must be a usable port, got {value}");
```

## Messages

- Always the **last** parameter.
- Replaces that check's default message.
- May contain `{argument}` (the captured argument name) and `{value}` (the actual
  value), filled in only on the failure path.
- `Must(message)` sets a message for every check in the chain; a check's own
  message wins for that check.

```csharp
environment.Must("This should be prod").NotBe("test").NotBeEmpty();
```

## Exception taxonomy

| Exception | Thrown for |
| --- | --- |
| `ArgumentNullException` | a null argument: `NotBeNull`, `Value()`, and the implicit null check inside `Satisfy` and the ordering checks |
| `ArgumentOutOfRangeException` | ordinal checks only: comparisons, ranges, sign, the NaN policy |
| `ArgumentException` | everything else |

**Null and NaN.** Ordering comparisons reject `null` with `ArgumentNullException`
and `NaN` with `ArgumentOutOfRangeException`; a `NaN` satisfies no ordering
comparison, as IEEE requires. Equality checks and the explicit null checks accept
`null`. `BeNaN`, `NotBeNaN`, `BeFinite` and `BeInfinity` are the deliberate way
to ask about non-finite values.

**A different exception type** is available only on `Satisfy`:

```csharp
order.Must().Satisfy<Order, OrderQuantityException>(o => o.Quantity >= 5);
order.Must().Satisfy<Order, OrderQuantityException>(o => o.Quantity >= 5, "Quantity cannot be less than 5");
order.Must().Satisfy<Order, OrderQuantityException>(ValidOrder); // a specification
```

## Ending a chain with the value

```csharp
this.port = config.Port.Must().BeBetween(1, 65535).Value();
```

`Value()` returns the unwrapped, non-nullable value and fails a `null` argument
with `ArgumentNullException`, exactly as `NotBeNull` would — so it is itself a
null guard. It exists for every contract listed in the table above.

## Specifications

```csharp
using FluentContracts.Specifications;

// one-line form
static readonly ISpecification<string> ValidIban =
    Spec.From<string>(s => Iban.IsValid(s), "be a valid IBAN");

// class form, for a rule that needs more room
sealed class SepaCountry : Specification<string>
{
    public SepaCountry() : base("be in a SEPA country") { }
    public override bool IsSatisfiedBy(string value) => SepaCountries.Contains(value[..2]);
}

iban.Must().Satisfy(ValidIban.And(new SepaCountry()));
// ArgumentException: Expected iban to be a valid IBAN and be in a SEPA country, but found "XX00".
```

- `Expectation` is the phrase completing *"Expected `{argument}` to …"*. A
  **fragment**, never a sentence, and it never names the argument or the value.
- `And`, `Or` and `Not` compose both the predicate and the phrase.
- `Satisfy(ISpecification<T>)` behaves exactly like `Satisfy(Func<T, bool>)`:
  implicit not-null, conversion to `T`, and the same message handling.
- A `null` `Expectation` falls back to *"satisfy the given condition"*.

## A contract of your own

For a domain type that deserves its own checks:

1. Derive from `ObjectContract<TArgument, TContract>` — the generic, inheritable
   base — and add a sealed entry point deriving from it.
2. Every check returns `(TContract)this`, never a new object; a chain is one
   object.
3. Checks do not throw directly; they delegate to a `Validator.*` method.
4. Add a `Must()` extension for the type so it is reachable.

Everything already on the chain then composes with the new checks.

## Catalogue

Every contract and the checks it **adds**; the inherited ones are in the chain above. `(Not)X` means
both `X` and `NotX` exist. The overloads a check takes are not shown — read the overload rules above,
then confirm the signature in IntelliSense.

This section is **generated from the built library** by `./build.sh SyncSkillCatalogue`, and the
build fails if it drifts, so it is complete and current for the revision it came from rather than a
snapshot someone remembered to update.

<!-- BEGIN GENERATED CATALOGUE -->

### Core

- **`Base`** — `Satisfy`
- **`Collection`** (extends `Equality`) — `(Not)BeEmpty`, `(Not)HaveCountEqualTo`, `AllSatisfy`, `AnySatisfy`, `ContainAnyOf`, `HaveCountBetween`, `HaveCountGreaterOrEqualTo`, `HaveCountGreaterThan`, `HaveCountLessOrEqualTo`, `HaveCountLessThan`, `HaveUniqueItems`, `NotContainNull`
- **`Equality`** (extends `Object`) — `(Not)Be`, `(Not)BeAnyOf`
- **`Nullable`** (extends `Base`) — `(Not)BeNull`
- **`Object`** (extends `Nullable`) — `(Not)BeAssignableTo`, `(Not)BeOfType`

### Text

- **`Char`** (extends `Object`) — `(Not)Be`, `(Not)BeAlphanumeric`, `(Not)BeAnyOf`, `(Not)BeAscii`, `(Not)BeDigit`, `(Not)BeLetter`, `(Not)BeLowercase`, `(Not)BeUppercase`, `(Not)BeWhiteSpace`, `BeBetween`, `BeGreaterOrEqualTo`, `BeGreaterThan`, `BeLessOrEqualTo`, `BeLessThan`
- **`String`** (extends `Equality`) — `(Not)BeAlphanumeric`, `(Not)BeBase64`, `(Not)BeCreditCardNumber`, `(Not)BeEmailAddress`, `(Not)BeEmpty`, `(Not)BeExistingDirectory`, `(Not)BeExistingFile`, `(Not)BeGuid`, `(Not)BeHexadecimal`, `(Not)BeIpAddress`, `(Not)BeLowercase`, `(Not)BeMatching`, `(Not)BeNullOrEmpty`, `(Not)BeNullOrWhiteSpace`, `(Not)BePalindrome`, `(Not)BeUppercase`, `(Not)BeUrl`, `(Not)BeWhiteSpace`, `(Not)Contain`, `(Not)EndWith`, `(Not)HaveLengthEqualTo`, `(Not)StartWith`, `HaveLengthBetween`, `HaveLengthGreaterOrEqualTo`, `HaveLengthGreaterThan`, `HaveLengthLessOrEqualTo`, `HaveLengthLessThan`

### Numeric

- **`Byte`** (extends `Object`) — `(Not)Be`, `(Not)BeAnyOf`, `(Not)BeEven`, `(Not)BeOdd`, `(Not)BeZero`, `BeBetween`, `BeGreaterOrEqualTo`, `BeGreaterThan`, `BeLessOrEqualTo`, `BeLessThan`
- **`Decimal`** (extends `Object`) — `(Not)Be`, `(Not)BeAnyOf`, `(Not)BeNegative`, `(Not)BePositive`, `(Not)BeZero`, `BeBetween`, `BeGreaterOrEqualTo`, `BeGreaterThan`, `BeLessOrEqualTo`, `BeLessThan`
- **`Double`** (extends `Object`) — `(Not)Be`, `(Not)BeAnyOf`, `(Not)BeFinite`, `(Not)BeInfinity`, `(Not)BeNaN`, `(Not)BeNegative`, `(Not)BePositive`, `(Not)BeZero`, `BeBetween`, `BeGreaterOrEqualTo`, `BeGreaterThan`, `BeLessOrEqualTo`, `BeLessThan`
- **`Float`** (extends `Object`) — `(Not)Be`, `(Not)BeAnyOf`, `(Not)BeFinite`, `(Not)BeInfinity`, `(Not)BeNaN`, `(Not)BeNegative`, `(Not)BePositive`, `(Not)BeZero`, `BeBetween`, `BeGreaterOrEqualTo`, `BeGreaterThan`, `BeLessOrEqualTo`, `BeLessThan`
- **`Int`** (extends `Object`) — `(Not)Be`, `(Not)BeAnyOf`, `(Not)BeEven`, `(Not)BeNegative`, `(Not)BeOdd`, `(Not)BePositive`, `(Not)BeZero`, `BeBetween`, `BeGreaterOrEqualTo`, `BeGreaterThan`, `BeLessOrEqualTo`, `BeLessThan`
- **`Long`** (extends `Object`) — `(Not)Be`, `(Not)BeAnyOf`, `(Not)BeEven`, `(Not)BeNegative`, `(Not)BeOdd`, `(Not)BePositive`, `(Not)BeZero`, `BeBetween`, `BeGreaterOrEqualTo`, `BeGreaterThan`, `BeLessOrEqualTo`, `BeLessThan`
- **`Number`** (extends `Object`) — `(Not)Be`, `(Not)BeAnyOf`, `(Not)BeEven`, `(Not)BeInfinity`, `(Not)BeNaN`, `(Not)BeNegative`, `(Not)BeOdd`, `(Not)BePositive`, `(Not)BeZero`, `BeBetween`, `BeFinite`, `BeGreaterOrEqualTo`, `BeGreaterThan`, `BeLessOrEqualTo`, `BeLessThan`
- **`Sbyte`** (extends `Object`) — `(Not)Be`, `(Not)BeAnyOf`, `(Not)BeEven`, `(Not)BeNegative`, `(Not)BeOdd`, `(Not)BePositive`, `(Not)BeZero`, `BeBetween`, `BeGreaterOrEqualTo`, `BeGreaterThan`, `BeLessOrEqualTo`, `BeLessThan`
- **`Short`** (extends `Object`) — `(Not)Be`, `(Not)BeAnyOf`, `(Not)BeEven`, `(Not)BeNegative`, `(Not)BeOdd`, `(Not)BePositive`, `(Not)BeZero`, `BeBetween`, `BeGreaterOrEqualTo`, `BeGreaterThan`, `BeLessOrEqualTo`, `BeLessThan`
- **`Uint`** (extends `Object`) — `(Not)Be`, `(Not)BeAnyOf`, `(Not)BeEven`, `(Not)BeOdd`, `(Not)BeZero`, `BeBetween`, `BeGreaterOrEqualTo`, `BeGreaterThan`, `BeLessOrEqualTo`, `BeLessThan`
- **`Ulong`** (extends `Object`) — `(Not)Be`, `(Not)BeAnyOf`, `(Not)BeEven`, `(Not)BeOdd`, `(Not)BeZero`, `BeBetween`, `BeGreaterOrEqualTo`, `BeGreaterThan`, `BeLessOrEqualTo`, `BeLessThan`
- **`Ushort`** (extends `Object`) — `(Not)Be`, `(Not)BeAnyOf`, `(Not)BeEven`, `(Not)BeOdd`, `(Not)BeZero`, `BeBetween`, `BeGreaterOrEqualTo`, `BeGreaterThan`, `BeLessOrEqualTo`, `BeLessThan`

### Values, dates and times

- **`Bool`** (extends `Object`) — `(Not)Be`, `(Not)BeAnyOf`, `BeFalse`, `BeTrue`
- **`DateOnly`** (extends `Equality`) — `(Not)BeInTheFuture`, `(Not)BeInThePast`, `(Not)BeToday`, `(Not)BeWeekday`, `(Not)BeWeekend`, `BeBetween`, `BeGreaterOrEqualTo`, `BeGreaterThan`, `BeLessOrEqualTo`, `BeLessThan`
- **`DateTime`** (extends `Base`) — `(Not)Be`, `(Not)BeAnyOf`, `(Not)BeFriday`, `(Not)BeInApril`, `(Not)BeInAugust`, `(Not)BeInCurrentMonth`, `(Not)BeInCurrentYear`, `(Not)BeInDaylightSaving`, `(Not)BeInDecember`, `(Not)BeInFebruary`, `(Not)BeInJanuary`, `(Not)BeInJuly`, `(Not)BeInJune`, `(Not)BeInMarch`, `(Not)BeInMay`, `(Not)BeInMonth`, `(Not)BeInNovember`, `(Not)BeInOctober`, `(Not)BeInSeptember`, `(Not)BeInTheFuture`, `(Not)BeInThePast`, `(Not)BeInYear`, `(Not)BeLeapYear`, `(Not)BeLocal`, `(Not)BeMonday`, `(Not)BeNull`, `(Not)BeOnCurrentDay`, `(Not)BeOnDate`, `(Not)BeOnDay`, `(Not)BeOnDayOfYear`, `(Not)BeSaturday`, `(Not)BeSunday`, `(Not)BeThursday`, `(Not)BeToday`, `(Not)BeTomorrow`, `(Not)BeTuesday`, `(Not)BeUtc`, `(Not)BeWednesday`, `(Not)BeWeekday`, `(Not)BeWeekend`, `(Not)BeYesterday`, `BeBetween`, `BeGreaterOrEqualTo`, `BeGreaterThan`, `BeLessOrEqualTo`, `BeLessThan`
- **`DateTimeOffset`** (extends `Equality`) — `(Not)BeInTheFuture`, `(Not)BeInThePast`, `(Not)BeUtc`, `(Not)HaveOffset`, `BeBetween`, `BeGreaterOrEqualTo`, `BeGreaterThan`, `BeLessOrEqualTo`, `BeLessThan`
- **`Enum`** (extends `Object`) — `(Not)Be`, `(Not)BeAnyOf`, `(Not)BeDefined`, `(Not)HaveFlag`
- **`Guid`** (extends `Object`) — `(Not)Be`, `(Not)BeAnyOf`, `(Not)BeEmpty`
- **`TimeOnly`** (extends `Equality`) — `(Not)BeBetween`, `BeGreaterOrEqualTo`, `BeGreaterThan`, `BeLessOrEqualTo`, `BeLessThan`
- **`TimeSpan`** (extends `Base`) — `(Not)Be`, `(Not)BeEqualTo`, `(Not)BeLongerThan`, `(Not)BeNull`, `(Not)BeShorterThan`

### Collections

- **`Dictionary`** (extends `Collection`) — `(Not)ContainKey`, `(Not)ContainKeyValuePair`, `(Not)ContainValue`
- **`List`** (extends `Collection`) — `(Not)BeInAscendingOrder`, `(Not)BeInDescendingOrder`, `(Not)Contain`, `HaveElementsOfType`

### URIs

- **`Uri`** (extends `Equality`) — `(Not)BeAbsolute`, `(Not)BeFile`, `(Not)BeHttps`, `(Not)BeLoopback`, `(Not)HaveHost`, `(Not)HavePort`, `(Not)HaveScheme`

### Streams

- **`Stream`** (extends `Object`) — `(Not)BeAbleToTimeout`, `(Not)BeAtPosition`, `(Not)BeReadable`, `(Not)BeSeekable`, `(Not)BeWithLength`, `(Not)BeWriteable`

### Files and directories

- **`DirectoryInfo`** (extends `Nullable`) — `(Not)BeEmpty`, `(Not)BeHidden`, `(Not)BeReadOnly`, `(Not)Exist`
- **`FileInfo`** (extends `Nullable`) — `(Not)BeEmpty`, `(Not)BeHidden`, `(Not)BeReadOnly`, `(Not)Exist`, `(Not)HaveExtension`, `(Not)HaveSizeEqualTo`, `HaveSizeGreaterOrEqualTo`, `HaveSizeGreaterThan`, `HaveSizeLessOrEqualTo`, `HaveSizeLessThan`

<!-- END GENERATED CATALOGUE -->

## Worked replacements

```csharp
// before
if (myOrder == null) throw new ArgumentNullException(nameof(myOrder));
if (myOrder.Quantity < 5) throw new ArgumentOutOfRangeException(nameof(myOrder), "Quantity cannot be less than 5");
// after
myOrder.Must().NotBeNull().Satisfy<Order>(o => o.Quantity >= 5, "Quantity cannot be less than 5");

// before
if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required", nameof(name));
this.name = name;
// after
this.name = name.Must().NotBeNullOrWhiteSpace().Value();

// before
if (port < 1 || port > 65535) throw new ArgumentOutOfRangeException(nameof(port));
// after
port.Must().BeBetween(1, 65535);

// before
if (items == null) throw new ArgumentNullException(nameof(items));
if (items.Count == 0) throw new ArgumentException("At least one item is required", nameof(items));
if (items.Any(i => i is null)) throw new ArgumentException("Items cannot contain nulls", nameof(items));
// after
items.Must().NotBeNull().NotBeEmpty().NotContainNull();

// before — an exception type callers depend on
if (!Iban.IsValid(iban)) throw new InvalidIbanException(iban);
// after — keep the type
iban.Must().Satisfy<string, InvalidIbanException>(Iban.IsValid);
```
