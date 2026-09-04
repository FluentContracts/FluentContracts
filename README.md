<img alt="Logo" width="100px" src="https://github.com/FluentContracts/FluentContracts/raw/master/assets/icon.png"/>

# FluentContracts
[![NuGet Version](https://img.shields.io/nuget/v/FluentContracts?style=for-the-badge&logo=nuget&logoColor=white&color=green)](https://www.nuget.org/packages/FluentContracts/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/FluentContracts?style=for-the-badge&logo=nuget&logoColor=white)](https://www.nuget.org/packages/FluentContracts/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue?style=for-the-badge)](LICENSE)

Argument validation that reads like the rule it enforces, and fails with a message that says what
was expected of which argument. Inspired by [FluentAssertions](https://github.com/fluentassertions/fluentassertions).

```
dotnet add package FluentContracts
```

## Why another validation library

[FluentValidation](https://github.com/FluentValidation/FluentValidation) and `Guard` from the
[.NET Community Toolkit](https://github.com/CommunityToolkit/dotnet) are excellent, and if you already
use them, keep doing so.

This one exists because guard clauses tend to read as noise: three lines of `if` and `throw` for one
rule, a `nameof` to keep in sync, an exception type to pick, and a message to write — or, more often,
not write. FluentContracts makes the rule the whole statement, the way FluentAssertions does for a
test, and puts the argument name, the exception type and a readable message in for you.

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
[SupportedContracts.md](docs/SupportedContracts.md).

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
  [Benchmarks.md](docs/Benchmarks.md).
- Ships a Roslyn analyzer for the misuses that compile but check the wrong thing (none open at the
  moment; see [the analyzer project](src/FluentContracts.Analyzers/README.md)).
- Every public member has XML documentation, so all of the above is in IntelliSense.

## The agent skill

The rules that make a chain correct — which check to reach for, where the message goes, which
exception a check throws — are documented, but documentation does not reach a coding agent working in
*your* project. So they ship as an **agent skill** too: `fluentcontracts`, packaged as a plugin for
Claude Code, Codex and Gemini CLI.

With it installed, an agent asked to add argument validation confirms a check exists instead of
guessing one, chains from `Must()` and ends with `Value()`, keeps the message last, respects the
single bracketed-set rule for `BeAnyOf` and its family, lets the check decide the exception, and
reaches for `Satisfy` or an `ISpecification<T>` rather than dropping a raw `throw` into a chain.

It is not in the NuGet package — it is served from this repository, so you install it once per
machine and it applies to every project you use FluentContracts in.

### Installing it

**Claude Code** — add this repository as a plugin marketplace, then install the plugin:

```
/plugin marketplace add FluentContracts/FluentContracts
/plugin install fluentcontracts@fluentcontracts
```

Pin it to a released version by adding the marketplace at a tag instead —
`FluentContracts/FluentContracts@plugin-v1.0.0`. Every release that moves the plugin version tags it.

**Codex** — the same repository is a Codex plugin marketplace:

```
codex plugin marketplace add https://github.com/FluentContracts/FluentContracts
codex plugin install fluentcontracts
```

**Gemini CLI** — the repository is a Gemini extension, and the skills are discovered from it:

```
gemini extensions install https://github.com/FluentContracts/FluentContracts
```

**Any other agent** — the skill is a plain folder. Copy
[`skills/fluentcontracts`](skills/fluentcontracts) into wherever your harness looks for skills.

### Using it

You do not invoke it. It carries a description of when it applies, and the agent loads it on its own
once the work is about argument validation — "add guards to this constructor", "replace these
`if`/`throw` blocks", "validate the options before we use them". Naming FluentContracts in the ask
makes it certain.

Two files, both worth reading yourself:
[`SKILL.md`](skills/fluentcontracts/SKILL.md) is the guidance, and
[`references/cheatsheet.md`](skills/fluentcontracts/references/cheatsheet.md) is the catalogue of what
exists plus the rules that decide overloads, messages and exception types.

## Help needed 🙏

The goal is for this to be exhaustive, safe and stable enough for production use on large projects,
and help is very welcome: a check that is missing, a message that could read better, a platform that
is not covered. Open an issue first, then a pull request — [CONTRIBUTING.md](CONTRIBUTING.md) and
[AGENTS.md](AGENTS.md) describe the conventions, and the latter is written for coding agents as well
as people.

## Repository 🚧

### Builds

|     Type      | Status                                                                                                                                                                                                                                                     |
|:-------------:|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
|    Release    | [![Release](https://img.shields.io/github/actions/workflow/status/FluentContracts/FluentContracts/release.yml?branch=master&style=for-the-badge&logo=nuget&logoColor=white&label=Build%20%26%20Release)](https://github.com/FluentContracts/FluentContracts/actions/workflows/release.yml) |
| Code Coverage | [![Coveralls](https://img.shields.io/coverallsCoverage/github/FluentContracts/FluentContracts?branch=master&style=for-the-badge&logo=coveralls&logoColor=white)](https://coveralls.io/github/FluentContracts/FluentContracts)                                |

Pull requests are built and tested on Linux, Windows and macOS by the
[`pr`](https://github.com/FluentContracts/FluentContracts/actions/workflows/pr.yml) workflow. Every
merge into `master` is a release; [CHANGELOG.md](CHANGELOG.md) is the curated account of each one.

### Status

![Alt](https://repobeats.axiom.co/api/embed/5aeeab6e5ce07439108408d66453df63f9379eeb.svg "Repobeats analytics image")

### How to build locally

The .NET SDK version is pinned in `global.json`. Then:

```bash
./build.sh Test          # compile and run the tests (build.cmd on Windows)
./build.sh Test Pack     # ...and produce the package in output/packages
```

## Where to find me 🕵️

[![Blog](https://img.shields.io/badge/Blog-todorov.bg-black.svg?style=for-the-badge&logo=jekyll&logoColor=white)](https://todorov.bg)
[![X](https://img.shields.io/badge/twitter-%40totollygeek-lightgreen.svg?style=for-the-badge&logo=x&logoColor=white)](https://twitter.com/totollygeek)
[![LinkedIn](https://img.shields.io/badge/linkedin-totollygeek-blue.svg?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/totollygeek)
[![Mastodon](https://img.shields.io/badge/Mastodon-%40totollygeek@infosec.exchange-darkblue.svg?style=for-the-badge&logo=mastodon&logoColor=white)](https://infosec.exchange/@totollygeek)
[![Threads](https://img.shields.io/badge/Threads-%40totollygeek-red.svg?style=for-the-badge&logo=threads&logoColor=white)](https://www.threads.net/@totollygeek)
[![BlueSky](https://img.shields.io/badge/BlueSky-totollygeek.com-lightblue.svg?style=for-the-badge&logo=bluesky&logoColor=white)](https://bsky.app/profile/totollygeek.com)
[![Linktree](https://img.shields.io/badge/Linktree-totollygeek-yellow.svg?style=for-the-badge&logo=linktree&logoColor=white)](https://linktr.ee/totollygeek)
[![Email](https://img.shields.io/badge/Email-fluentcontracts@pm.me-blue.svg?style=for-the-badge&logo=proton&logoColor=white)](mailto://fluentcontracts@pm.me)

## Special thanks 🙇‍♂️

#### [Matthias Koch](https://twitter.com/matkoch87)
> The creator of [NUKE](https://nuke.build), because I cannot build any .NET project without it and because he helped me tremendously in setting up the repository and everything around this project. (_I have also copy-pasted, like his entire build and some markdown files_ 🤫)

#### [Dennis Doomen](https://twitter.com/ddoomen)
> The "[FluentAssertions](https://fluentassertions.com/)" guy. This whole project was inspired by how that library works and I might have copy-pasted also parts of his repo too 😏

## Technology Sponsors 💻
<img alt="JetBrains Logo" width="300px" src="https://resources.jetbrains.com/storage/products/company/brand/logos/jetbrains.png"/>

> Special thanks to [JetBrains](https://www.jetbrains.com/) for supplying a free license for [Rider](https://www.jetbrains.com/rider/), which is my primary IDE of choice for this project!

Icon made by [IconMonk](https://www.flaticon.com/authors/icon-monk) from [Flaticon](https://www.flaticon.com)
