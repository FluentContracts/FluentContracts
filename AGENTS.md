# AGENTS.md

Guidance for AI coding agents — and a useful orientation for humans — working in this
repository.

## What this project is

FluentContracts is an argument-validation library. Instead of hand-written guard clauses
it offers a fluent, readable chain:

```csharp
myOrder
    .Must()
    .NotBeNull()
    .And
    .Satisfy<OrderQuantityException>(o => o.Quantity >= 5, "Order quantity cannot be less than 5");
```

It ships as the `FluentContracts` NuGet package, targeting `netstandard2.0` and `net8.0`.

## Layout

| Path | What it is |
| --- | --- |
| `src/FluentContracts` | The library |
| `tests/FluentContracts.Tests` | xUnit test suite |
| `build/` | The [NUKE](https://nuke.build) build, written in C# |
| `.github/workflows/` | **Generated** by NUKE — see [CI](#ci) |
| `docs/SupportedContracts.md` | **Generated** by the build — never hand-edit |

## Prerequisites

The .NET SDK version is pinned in `global.json` (10.0.x). The build project itself targets
`net10.0`, so an older SDK cannot run the build.

## Build and test

```bash
./build.sh Test          # compile and run the tests
./build.sh Test Pack     # ...and produce the NuGet package in output/packages
```

The test suite can also be run directly:

```bash
dotnet test tests/FluentContracts.Tests/FluentContracts.Tests.csproj
```

**Warnings are errors** (`TreatWarningsAsErrors` in `Directory.Build.props`). Do not reach
for a blanket suppression to get a green build — fix the cause, or add a narrowly scoped
`NoWarn` with a comment explaining why.

## Branching and releases

This is the part most likely to surprise you.

- **`master` is the only long-lived branch.** Branch off `master`, open a pull request into
  `master`. There is no `develop`.
- **Every merge into `master` is released**: the `release` workflow packs, publishes to
  nuget.org and creates a GitHub release and tag.
- **Pull requests are squash-merged, so the pull request title becomes the commit message.**

### Choosing the version

The version is computed by GitVersion from the most recent tag, and the **patch** is
incremented by default. To release a minor or major version instead, put the directive in
the **pull request title**, because that is the text that becomes the squash commit:

```
Add DateOnly and TimeOnly contracts +semver: minor
Drop the Linker allocation from every check +semver: major
```

Most pull requests need no directive at all — a patch release is the default.

> [!WARNING]
> GitVersion reads these directives out of commit messages. Quoting one in a commit message
> or pull request title while merely *writing about* it will actually request that bump. If
> you need to mention the syntax, do it in a file (like this one), not in a commit message.

Release notes are generated automatically from merged pull request titles, so write titles
that read well in a changelog.

## Code conventions

**Contract structure.** Contracts live in `src/FluentContracts/Contracts/<area>/`. Each type
gets a pair: a sealed entry point (`IntContract`) and a generic, inheritable base
(`IntContract<TContract>`). The hierarchy runs
`BaseContract` → `NullableContract` → `ObjectContract` → `EqualityContract` → specific
contracts.

**Every check returns `Linker<TContract>`** so callers can keep chaining with `.And`.

**Checks do not throw directly.** They delegate to a `Validator.*` method, which throws via
`ThrowHelper`. Keep new checks in that shape.

**Null policy.** Ordering comparisons (`BeGreaterThan`, `BeLessThan`, `BeBetween`,
`BePositive`, `BeNegative`, …) reject `null` with `ArgumentNullException`; the guard lives
inside the `Validator` ordering methods, not at the call sites. Equality checks and the
explicit null checks accept `null`. New checks must follow this split, and
`NullArgumentPolicyTests` exists to keep it honest.

**Multi-targeting.** The library builds for `netstandard2.0` and `net8.0`. When an API is
unavailable on `netstandard2.0`, add a guarded helper to `Infrastructure/Compat.cs` rather
than scattering `#if` through the contracts. `PolySharp` supplies the missing language and
nullable-analysis attributes.

**XML documentation.** Every public check needs a `<summary>` and `<param>` entries; the
package ships the generated XML. Refer to a parameter with `<paramref name="x"/>` and to a
type parameter with `<typeparamref name="T"/>` — `<see cref="x"/>` does not resolve to
parameters and silently produces broken documentation.

## Tests

Tests are required for every new check and every bug fix; a pull request without them will
not be accepted.

- One file per contract, under `tests/FluentContracts.Tests/<area>/`, annotated with
  `[ContractTest("Name")]`.
- Use the `TestContract<T, TContract, TException>` harness in `Tests.cs`. It asserts the
  success path, the failure path, and the custom-message path in one call.
- Test data comes from `DummyData` (backed by Bogus), not hand-rolled literals.
- Cross-cutting behaviour belongs in a dedicated file — see `NullArgumentPolicyTests`.

## CI

| Workflow | Trigger | Does |
| --- | --- | --- |
| `pr` | pull request into `master` | Test and Pack on Linux, Windows and macOS |
| `release` | push to `master` | Test, coverage, Pack, publish to NuGet, create the GitHub release |

Publishing uses [Trusted Publishing](https://learn.microsoft.com/en-us/nuget/nuget-org/trusted-publishing):
the workflow exchanges a GitHub OIDC token for a NuGet key that lasts an hour, so there is
no long-lived API key in the repository.

> [!IMPORTANT]
> **The workflow YAML is generated.** It comes from the `[GitHubActions]` and
> `[NuGetTrustedPublishing]` attributes in `build/Build.GitHub.cs`, and NUKE rewrites the
> files whenever the build runs. Editing `.github/workflows/*.yml` by hand will be undone —
> change the attributes instead, then run `./build.sh Test` to regenerate.

## Housekeeping

- `CHANGELOG.md` is a curated summary of notable changes. Per-release notes are generated
  on the releases page, so only add entries that are worth calling out.
- `docs/SupportedContracts.md` is produced by the `GenerateSupportedContracts` target on
  local builds. Never edit it by hand; change the contracts and rebuild.
