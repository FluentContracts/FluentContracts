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
| `src/FluentContracts.Analyzers` | Roslyn analyzer, shipped inside the package — see [Analyzers](#analyzers) |
| `src/FluentContracts.Analyzers.CodeFixes` | The analyzer's code fixes, a separate assembly by necessity |
| `tests/FluentContracts.Tests` | xUnit test suite |
| `benchmarks/FluentContracts.Benchmarks` | BenchmarkDotNet suite — see [Benchmarks](#benchmarks) |
| `build/` | The [NUKE](https://nuke.build) build, written in C# |
| `.github/workflows/` | **Generated** by NUKE — see [CI](#ci) |
| `docs/PackageReadme.md` | The readme shown on nuget.org — see [The two readmes](#the-two-readmes) |
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

## Issues first, then pull requests

**Always open an issue before opening a pull request**, whatever the size of the change, and have
the pull request reference it (`Closes #123`). The issue is where the problem or feature is stated in
its own right — what is wrong or missing, and why it matters — before any diff exists; the pull
request is only the implementation. This keeps the repository's history navigable: the issue list is
the record of what was considered and why, and a pull request without one leaves its reasoning
stranded in the diff.

## Branching and releases

This is the part most likely to surprise you.

- **`master` is the only long-lived branch.** Branch off `master`, open a pull request into
  `master`. There is no `develop`.
- **Every merge into `master` is released**: the `release` workflow packs, publishes to
  nuget.org and creates a GitHub release and tag.
- **Pull requests are squash-merged, so the pull request title becomes the commit message.**
- **Every pull request updates `CHANGELOG.md`** — see [Changelog](#changelog).

### Merging without releasing

Not every change belongs in a package. Put the **`skip-release` label** on the pull request and the
merge is still built and tested on `master`, but nothing is packed, published, tagged or released, and
the version does not move.

The release reads the label off the pull request the merge came from, resolved from the merge commit
rather than from anything written in it. So the label is visible on the pull request right up to the
moment you merge, and you can add or remove it there.

> [!IMPORTANT]
> **Do not use the pull request title for this.** GitHub composes the squash commit subject in the
> merge box when that page is *rendered*, so a tab opened before the title was edited will merge the
> title it was holding. That is exactly how a `[skip release]` title once published anyway. The marker
> is still honoured in a commit message, for a direct push and for anything already written down, but
> the label is what to reach for.

Neither is GitHub's `[skip ci]`, which would skip every workflow including the tests.

> [!WARNING]
> Markers are read out of the **whole commit message, body included** — by GitHub for `[skip ci]`
> and by this build for the release one, exactly as GitVersion reads `+semver:` directives. Writing
> one in a commit message while merely *describing* it will trigger it: a body that explains
> `[skip ci]` silently stops CI from running on the pull request at all, and the pull request just
> sits there with no checks. Mention the syntax in a file (like this one), never in a commit
> message or a pull request title. The `skip-release` label carries no such hazard, which is the
> other reason to prefer it.

> [!CAUTION]
> Two files look like documentation but ship **inside the package**: `docs/PackageReadme.md`, which
> becomes the nuget.org listing, and `assets/icon.png`. A published readme cannot be corrected in
> place, so a change to either only reaches anyone through a release — do not skip one.

### Assembling a major version

A breaking change cannot sit half-done on `master`, because every merge releases and the
`skip-release` label defers packing, not the code. A major version is assembled on a temporary
integration branch instead:

- The branch is named `release/<major>.<minor>` (`release/4.0`). Pull requests into it get the same
  `pr` workflow as pull requests into `master` — the generated workflow triggers on `release/*`.
- Each piece of the major version is an ordinary issue-first pull request into that branch.
- One final pull request merges the branch into `master`, titled with the major-version directive,
  and that merge is the release. The branch is deleted afterwards.
- `master` keeps releasing patches from unrelated pull requests throughout.

GitVersion's GitHubFlow treats `release/*` as release branches, so builds on the integration branch
report the upcoming version (`4.0.0-beta.n`); that is expected and nothing is published from there.

### Choosing the version

The version is computed by GitVersion from the most recent tag, and the **patch** is
incremented by default. To release a minor or major version instead, put the directive in
the **pull request title**, because that is the text that becomes the squash commit:

```
Add DateOnly and TimeOnly contracts +semver: minor
Drop the Linker allocation from every check +semver: major
```

Most pull requests need no directive at all — a patch release is the default.

> [!IMPORTANT]
> **On a single-commit pull request, put the directive in the commit subject too.** GitHub's squash
> default takes the merged subject from the pull request title *only when the branch has more than one
> commit*; with exactly one, it uses that commit's own subject and the title is never consulted. Two
> releases were computed wrong this way before it was understood. The release now checks: if the pull
> request asks for a bump the merged commit does not carry, it fails **before** anything is published
> rather than shipping breaking changes as a patch.
>
> The durable fix is a repository setting — Settings → General → Pull Requests → set the squash commit
> **title** to *Default to pull request title*. Then the branch's commit count stops mattering.

> [!WARNING]
> GitVersion reads these directives out of commit messages, so the rule above applies here too:
> quoting one in a commit message or pull request title while merely *writing about* it will
> actually request that bump.

Release notes are generated automatically from merged pull request titles, so write titles
that read well in a changelog.

## Code conventions

**Contract structure.** Contracts live in `src/FluentContracts/Contracts/<area>/`. Each type
gets a pair: a sealed entry point (`IntContract`) and a generic, inheritable base
(`IntContract<TContract>`). The hierarchy runs
`BaseContract` → `NullableContract` → `ObjectContract` → `EqualityContract` → specific
contracts.

**Every check returns the contract itself** (`TContract`) so callers keep chaining; `And` is an
identity property kept so chains written with it read the same. There is no `Linker` — a chain is
one object, and a new check must return `(TContract)this`, never a new object.

**Checks do not throw directly.** They delegate to a `Validator.*` method, which throws via
`ThrowHelper`. Keep new checks in that shape.

**Overload shape for multi-value checks.** A check taking several values comes as a pair — see
`ListContract.Contain`:

```csharp
public TContract Contain(params T[] elements);
public TContract Contain(IEnumerable<T> elements, string? message = null);
```

Never `Check(string? message, params T[] values)`. When `T` is `string`, the compiler binds
`Check("a", "b")` to that overload and silently swallows `"a"` as the message, so the check runs
against the wrong set — it prefers the candidate with more declared parameters when both are
applicable only in expanded form.

`EqualityContract.BeAnyOf` and `NotBeAnyOf` are the existing casualties. They are now `[Obsolete]`,
with `(IEnumerable<T>, string?)` alongside them, and a single value binds to its own overload rather
than being read as a message. Several string values still reach the deprecated overload, because that
call already means something today and the compiler cannot tell the two readings apart; removing the
overload in 4.0.0 is what settles it. The value-type contracts declare the same shape but cannot be
hit by it — a string is not a candidate value for `params int[]` — so they are left alone rather than
deprecated for a defect they do not have.

**Null and NaN policy.** Ordering comparisons (`BeGreaterThan`, `BeLessThan`, `BeBetween`,
`BePositive`, `BeNegative`, …) reject `null` with `ArgumentNullException` and `NaN` with
`ArgumentOutOfRangeException`. Both guards live inside the `Validator` ordering methods, not at the
call sites, so a check added later cannot forget them. Equality checks and the explicit null checks
accept `null`; `BeNaN` and `BeFinite` are how a caller asks about NaN deliberately.

`Comparer<T>` is a *total* order and sorts `NaN` below every other value, which is why `BeNegative`
and `BeLessThan` used to be satisfied by one. IEEE says every ordering comparison with `NaN` is false,
so none of them can be. New checks must follow this split; `NullArgumentPolicyTests` and
`NonFiniteNumberTests` exist to keep it honest.

**Multi-targeting.** The library builds for `netstandard2.0` and `net8.0`. When an API is
unavailable on `netstandard2.0`, add a guarded helper to `Infrastructure/Compat.cs` rather
than scattering `#if` through the contracts. `PolySharp` supplies the missing language and
nullable-analysis attributes.

**XML documentation.** Every public member needs it — types, constructors and properties as well as
checks. A `<summary>`, a `<param>` for each parameter and a `<typeparam>` for each type parameter. The
package ships the generated XML, so an undocumented public member is a blank in every consumer's
IntelliSense.

Refer to a parameter with `<paramref name="x"/>` and to a type parameter with `<typeparamref name="T"/>`
— `<see cref="x"/>` does not resolve to parameters and silently produces broken documentation.

**The compiler enforces this**, so there is nothing to remember: an undocumented public member raises
CS1591, and warnings are errors here, so the build fails. Nothing suppresses it — do not add it back to
`NoWarn` to get a green build.

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

The `release` workflow also mints a short-lived token for a GitHub App, which is what pushes the
finalised changelog back to `master`. The built-in `GITHUB_TOKEN` cannot: `master` is protected, and
`github-actions[bot]` cannot be granted a bypass — GitHub keeps it off the bypass list on purpose,
since the permission would not be scoped to one branch. An App can be granted one. Two repository
secrets hold its credentials, `CHANGELOG_APP_ID` and `CHANGELOG_APP_PRIVATE_KEY`; if either is
missing the release still publishes and the build says the changelog was not pushed.

> [!IMPORTANT]
> **The workflow YAML is generated.** It comes from the `[GitHubActions]` and `[ReleaseWorkflow]`
> attributes in `build/Build.GitHub.cs`, and NUKE rewrites the files whenever the build runs.
> Editing `.github/workflows/*.yml` by hand will be undone — change the attributes instead, then run
> `./build.sh Test` to regenerate.

## The two readmes

`README.md` is the GitHub landing page. `docs/PackageReadme.md` is what nuget.org shows, and it is the
file packed into the package as `README.md`.

They are deliberately separate. nuget.org renders CommonMark only — **no raw HTML** — it resolves no
relative links, and it renders images only from
[an allow-list of domains](https://learn.microsoft.com/en-us/nuget/nuget-org/package-readme-on-nuget-org#allowed-domains-for-images-and-badges).
The repository README breaks all three rules: it uses `<img>` tags, links to files by relative path, and
pulls images from `github.com/.../raw/...`, `repobeats.axiom.co` and `resources.jetbrains.com`, none of
which are trusted. Pointing `PackageReadmeFile` back at `README.md` would put the mangled listing back.

When editing `docs/PackageReadme.md`, keep to plain CommonMark, use absolute `https://` links, and only
use images from allowed domains (`img.shields.io` and `raw.githubusercontent.com` cover most needs). A
published readme cannot be corrected in place — it takes a new package version.

## Changelog

**Every pull request updates `CHANGELOG.md`.** There is no automation for this and no reviewer
will add it for you — an entry that is not written when the change is made is never written.

Add it under `## [Unreleased]`, in the heading that fits, creating the heading if it is missing:

| Heading | For |
| --- | --- |
| `Breaking` | anything that can change the behaviour of code that compiles today |
| `Added` | new contracts and new checks |
| `Fixed` | bug fixes |
| `Deprecated` | public API marked `[Obsolete]`, with what to use instead |
| `Changed` | behaviour that changed without breaking |
| `Packaging` | target frameworks, package contents, metadata |
| `Internal` | build, CI and repository tooling, with no effect on the package |

Write for someone deciding whether to upgrade. Name the contracts affected, and for a behaviour
change say **what the old behaviour was** — that is what tells a reader whether their code is
affected. "Fixed a null bug" tells them nothing; "`BeNegative` used to accept `null` and pass
silently, and now throws `ArgumentNullException`" tells them exactly what to check.

The notes on the releases page are generated from pull request titles. `CHANGELOG.md` is the curated
account that sits on top of them, so the two are not duplicates of each other.

You do not need to rename the section yourself. After a successful publish, the release workflow
renames `## [Unreleased]` to the version it shipped, dates it, leaves a fresh empty `## [Unreleased]`
above it, updates the comparison links, and commits that back to `master`. That commit carries both
`[skip ci]` and `[skip release]`: it is generated, it changes nothing but Markdown, and the tree it
sits on has already been tested by the merge it documents. The step does nothing when the section is
empty, and nothing when that version already has a section, so a re-run is harmless.

## Analyzers

The package ships a Roslyn analyzer from `analyzers/dotnet/cs`, injected into the nupkg by
`FluentContracts.csproj`. Rules live in `src/FluentContracts.Analyzers`, their code fixes in
`src/FluentContracts.Analyzers.CodeFixes` — **two assemblies by necessity** (RS1038): the compiler
loads the analyzer on the command line, where `Microsoft.CodeAnalysis.Workspaces` does not exist,
so only the IDE-only code-fix assembly may reference it.

- Both projects target `netstandard2.0` and reference Roslyn 4.4.0 with `PrivateAssets="all"` —
  the consumer's compiler provides Roslyn at runtime, and nothing may leak into the package's
  dependencies. Raising the Roslyn reference raises the minimum compiler consumers need.
- A new rule goes into `AnalyzerReleases.Unshipped.md` (enforced by RS2008); tests go in
  `tests/FluentContracts.Analyzers.Tests`, which compiles its snippets against the real library so
  they bind the same overloads consumers do.
- When changing the packaging, verify the result like a consumer: pack, restore the nupkg from a
  local feed into a scratch project, and confirm the diagnostic appears in its build output.

Current rules: **FC0001** — a string argument's `BeAnyOf`/`NotBeAnyOf` bound to the deprecated
message-first overload, silently taking the first value as the message.

## Benchmarks

`benchmarks/FluentContracts.Benchmarks` measures the happy path of representative checks against
the hand-written guard each replaces, with allocations. Run it with:

```bash
dotnet run -c Release --project benchmarks/FluentContracts.Benchmarks
```

`docs/Benchmarks.md` holds a curated snapshot of the results, with the machine and runtime stated —
update it when a change plausibly moves the numbers, from a run on your own machine. The benchmarks
are deliberately **not** run in CI: shared runners make the numbers noise. CI only asserts the
project still compiles, which it does by being in the solution.

## Housekeeping

- `docs/SupportedContracts.md` is produced by the `GenerateSupportedContracts` target on
  local builds. Never edit it by hand; change the contracts and rebuild.
