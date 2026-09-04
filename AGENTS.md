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
| `skills/` | The agent skill, served in place to every harness; see [The agent skill](#the-agent-skill) |
| `.github/workflows/` | **Generated** by NUKE — see [CI](#ci) |
| `docs/PackageReadme.md` | The readme shown on nuget.org — see [The two readmes](#the-two-readmes) |
| `docs/SupportedContracts.md` | **Generated** by the build — never hand-edit |
| `skills/fluentcontracts/references/cheatsheet.md` | Its catalogue section is **generated** — never hand-edit inside the markers |

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

**A merge that changes nothing the package carries does not release, whatever it says.** Before
publishing, the build diffs the packaged paths — `src/`, `docs/PackageReadme.md`, `assets/` and
`Directory.Build.props` — against the last version tag, and stops if none of them moved. Most changes
that do not belong in a package are held back by this alone, with nothing to remember.

That guard exists because the label below cannot cover every case. `skip-release` is read off a
**pull request**, and a direct push to `master` has none — so a file edited through GitHub's web
interface releases a package, which is exactly how `4.0.1` shipped as a byte-identical republish of
`4.0.0` from a commit that only removed a link from `FUNDING.yml`. The marker in a commit message was
the only defence, at the moment it is least likely to be written.

The version tag it compares against is matched as `[0-9]*`, so the `plugin-v<version>` tags that
[the agent skill](#the-agent-skill) creates are not mistaken for release tags. A check that cannot
run — no tag reachable, a failing git invocation — says so and lets the release proceed, because
silently blocking a real release is the worse failure.

Put the **`skip-release` label** on the pull request for the other case: packaged content genuinely
changed and you still want to hold the release back. The merge is still built and tested on `master`,
but nothing is packed, published, tagged or released, and the version does not move.

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

**Overload shape for multi-value checks.** A check that takes several values has exactly three
overloads — one value, a set, and a set followed by the message — and never `params`:

```csharp
public TContract BeAnyOf(T expectedValue);
public TContract BeAnyOf(IEnumerable<T> expectedValues, string? message = null);
```

There is deliberately no `(T value, string message)` overload. With it, `BeAnyOf("draft",
"published")` on a string would compile and silently take `"published"` as the message — the trap
3.3.0 deprecated and 4.0.0 removed. Without it, and without `params` (which C# requires to be last,
so no message could ever follow it), the rule is one sentence: **a message only ever follows a
bracketed set**. `BeAnyOf("draft", "published")` is a compile error, which `OverloadShapeTests` in
the analyzer test project pins by compiling snippets against the real library.

Fixed-arity checks keep `(operands…, string? message = null)`: the compiler knows their arity, so
`NotBe("test", "This should be prod")` binds one way only.

**Messages.** The `message` parameter is the mechanism, always after the operands. It may carry
`{argument}` and `{value}`, filled on the failure path by `Validator.Custom`; every throw site in a
validator is `Custom(message, argumentName, actual) ?? Expected(argumentName, …)`, so a new
validator gets both behaviours by following that shape. `Must()` takes a chain-wide message as its
first parameter, stored as `ChainMessage` on the contract; every check passes
`message ?? ChainMessage` to the validators, and a new check must do the same.

**Specifications.** The extensibility point is `ISpecification<T>` in `src/FluentContracts/Specifications/`:
`IsSatisfiedBy` plus an `Expectation` phrase that completes "Expected `{argument}` to …", so a
user's rule fails through the same `Validator.Expected` machinery as a built-in check.
`Satisfy(ISpecification<T>)` mirrors `Satisfy(Func<T, bool>)` in every respect — implicit not-null,
conversion to `T`, `message ?? ChainMessage` — and the two must not drift apart. `Spec.From` is the
one-line form, `Specification<T>` the class form, and `And`/`Or`/`Not` compose both the predicate
and the phrase. A phrase is a fragment, never a sentence, and never names the argument: the library
adds the name and the value. `Validator` stays static and internal; nothing about a specification
reaches into it.

**Exception taxonomy.** `ArgumentNullException` for a null argument. `ArgumentOutOfRangeException`
only for ordinal checks — comparisons, ranges, sign, the NaN policy — thrown by
`ThrowHelper.ThrowArgumentOutOfRangeException` from the ordering validators and from
`CheckOrdinalCondition`. `ArgumentException` for everything else, via
`ThrowHelper.ThrowArgumentException` and `CheckGenericCondition`. A new check picks the validator
that matches what it asserts, and `ExceptionTaxonomyTests` pins the type per family.

**Null and NaN policy.** Ordering comparisons (`BeGreaterThan`, `BeLessThan`, `BeBetween`,
`BePositive`, `BeNegative`, …) reject `null` with `ArgumentNullException` and `NaN` with
`ArgumentOutOfRangeException`. Both guards live inside the `Validator` ordering methods, not at the
call sites, so a check added later cannot forget them. Equality checks and the explicit null checks
accept `null`; `BeNaN` and `BeFinite` are how a caller asks about NaN deliberately.

`Comparer<T>` is a *total* order and sorts `NaN` below every other value, which is why `BeNegative`
and `BeLessThan` used to be satisfied by one. IEEE says every ordering comparison with `NaN` is false,
so none of them can be. New checks must follow this split; `NullArgumentPolicyTests` and
`NonFiniteNumberTests` exist to keep it honest.

The validators' NaN switch only knows `double` and `float`. The generic `NumberContract<T>`
(`net8.0` only, for any `INumber<T>` without a hand-written contract) asks the type itself through
`Validator.CheckForNumberNotNaN` at the top of every ordering check, so a new check there must call
it too; `NumberContractTests` pins the policy for `Half`. `NumberContractResolutionTests` pins that
the hand-written contracts keep winning `Must()` resolution — a non-generic overload beats the
generic one on a tie — so a new hand-written numeric contract needs a line there.

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

A new check also has to reach the **agent skill**, whose cheatsheet is what a coding agent consults
instead of guessing a check name. That part is generated: run `./build.sh SyncSkillCatalogue`, commit
the result, and bump the plugin version. `CheckSkillCatalogue` fails the build until you have — see
[The agent skill](#the-agent-skill).

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

Current rules: none. **FC0001** policed the message-first `BeAnyOf` overload and was retired with it
in 4.0.0 — recorded under *Removed Rules* in `AnalyzerReleases.Shipped.md`. The projects and the
packaging stay in place for the next rule.

## The agent skill

`skills/fluentcontracts/` teaches a coding agent to apply FluentContracts in a **consumer** project:
which check to reach for, where the message goes, the one bracketed-set overload rule, the exception
taxonomy, the null and NaN policy, and specifications. `AGENTS.md` is guidance for someone working in
this repository; the skill is the same knowledge sent to where the library is actually used.

**The repository root is the plugin.** There is exactly one copy of `skills/` and all three harnesses
read it in place, which is why the plugin manifests sit at the root rather than under a plugin
directory of their own:

| Surface | Manifest | How it finds the skills |
| --- | --- | --- |
| Claude Code | `.claude-plugin/plugin.json`, listed in `.claude-plugin/marketplace.json` | the entry's `source` is `./`, the marketplace root, and Claude Code's default `skills/` scan runs against it |
| Codex | `.codex-plugin/plugin.json`, listed in `.agents/plugins/marketplace.json` | the manifest's `skills` is `./skills/`, relative to the same root |
| Gemini CLI | `gemini-extension.json` | it discovers `skills/` at the root |

Do not reintroduce a plugin directory holding a second copy of the tree. It was the first shape this
took and it is worse twice over: the copy is real duplication that has to be regenerated and gated
against drift, and it buys nothing this layout does not already give.

Replacing such a copy with a **symlink** is worse still, which is worth knowing before anyone
suggests it. Git for Windows tests at clone time whether it can create links; where it cannot — the
default, absent Developer Mode — it sets `core.symlinks=false` and writes the link out as a small
text file holding the target path. The plugin would ship no skills at all, on exactly the platform
least likely to be the one you tested on, with nothing anywhere to say so.

One caveat is worth recording. A marketplace-root `source` is documented for Claude Code. Codex
requires only that `source.path` be relative to the marketplace root, `./`-prefixed and inside that
root — all of which `./` satisfies — but every example in its documentation is a subdirectory, so the
root form is undocumented rather than blessed there. If it turns out to be unsupported, the fallback
is a Codex-only plugin directory carrying its own copy of the skills, and the drift gate that a copy
needs.

### Changing a skill

1. Edit under `skills/`. There is nowhere else to edit; that is the point of the layout. The one
   exception is the cheatsheet's catalogue section, which is generated — see below.
2. **Bump the version by hand in all four manifests** — the two plugin manifests, the entry in
   `.claude-plugin/marketplace.json`, and `gemini-extension.json`. Additive content is a minor bump;
   a correction is a patch.

The bump is the step that is easy to skip and expensive to miss: clients read the declared version to
decide whether an installed plugin is stale, so skills edited without one never reach an agent that
already holds the old copy. Nothing downstream complains — the pull request is green and the skills
are correct in the repository.

### How it reaches people

All three harnesses install **from the repository**, not from a package or a release, so a merge into
`master` is what ships a skill change and there is nothing to publish. Claude Code and Codex clone
the marketplace repository. Gemini CLI prefers a GitHub release asset and falls back to cloning —
which is what happens here, because it looks for `{platform}.{arch}.{name}.{ext}` or
`{platform}.{name}.{ext}` and treats a lone asset as a generic fallback, and a release of this
repository carries several assets (the package, its symbols, the plugin archive) matching none of
those patterns.

What a release adds is only **pinning**: `TagPluginRelease` tags the merge commit `plugin-v<version>`
so an installation can be fixed to a known version, and `PackPlugin`'s archive rides along on the
GitHub release as a copy of what was published.

> [!IMPORTANT]
> The `skip-release` label does **not** hold back the plugin tag, and must not be made to. The label
> controls the *package*; the plugin carries its own hand-bumped version. A change to the skill alone
> is exactly the case that wants both — no package release, because the package did not change, and
> the plugin tagged, because it did. That is why `TagPluginRelease` triggers off `Pack` rather than
> `Publish`: `Publish` is skipped by the label, and anything triggered by it would go with it.

### The catalogue is generated from the library

`references/cheatsheet.md` carries a catalogue of every contract and the checks it adds, between
`<!-- BEGIN GENERATED CATALOGUE -->` and `<!-- END GENERATED CATALOGUE -->`. **Never hand-edit inside
those markers.** `./build.sh SyncSkillCatalogue` regenerates the section from the built assembly, by
the same reflection that produces `docs/SupportedContracts.md`, grouping contracts by the namespace
they are declared in — so a contract added under a new `Contracts/<Area>/` folder lands in its own
section with nothing to maintain by hand.

`CheckSkillCatalogue` fails the build when the committed catalogue no longer matches the library, and
names the contracts that differ rather than saying a file changed:

```
The skill's catalogue no longer matches the library:
  `Int`'s checks changed.
    library:    - **`Int`** … `BeLessThan`, `BePerfectSquare`
    cheatsheet: - **`Int`** … `BeLessThan`
```

This is the gate that matters most, and it is the one that is easiest to be sceptical of, so: the
cheatsheet is how an agent answers *"does this check exist?"* without guessing. A catalogue that has
fallen behind the library is **worse than no catalogue** — it reads as authoritative and is wrong,
and the agent has no way to tell. Adding a check and forgetting the skill is the easiest possible
miss, because the two live nowhere near each other and every test still passes.

Regenerating changes `skills/`, so `CheckPluginVersion` will then require a plugin version bump in
the same change. That is intended, not an annoyance to route around: agents holding the old plugin
would otherwise keep the stale catalogue. Adding checks is a minor bump.

### What the build enforces

All of it hangs off `Test`, so the existing `pr` and `release` workflows already run it and neither
the workflows nor the attributes that generate them had to change.

| Target | Fails when |
| --- | --- |
| `CheckSkillDocuments` | a skill violates the [Agent Skills specification](https://agentskills.io/specification) — most often a frontmatter `name` that no longer matches its directory, which every harness requires and which makes the skill silently fail to load |
| `CheckSkillCatalogue` | the library has a contract or check the cheatsheet does not, or the other way round |
| `CheckPluginManifests` | the manifests disagree about the version, the plugin's name, or where it lives |
| `CheckPluginVersion` | what the plugin publishes changed against the base ref and the version did not move **up** |

Three more targets produce rather than check. `SyncSkillCatalogue` regenerates the cheatsheet's
catalogue — it is the generator, so fix a stale catalogue by running it, never by hand. `PackPlugin` archives the plugin into `output/plugins`,
and the release attaches it next to the packages; it stages the manifests and `skills/` into a
temporary tree rather than zipping the root, since the root is the whole library.
`TagPluginRelease` tags the commit that published a plugin version as `plugin-v<version>`, so an
installation can be pinned to it; it is idempotent, because every merge into `master` runs it while
the version only moves when a skill changes.

`CheckPluginVersion` is the one that needs history, because "the content changed and the version did
not" is not a property of one snapshot. It compares against `FLUENTCONTRACTS_PLUGIN_BASE_REF`, else
the pull request's base branch, else — on a push to `master` — the previous commit, else
`origin/master`. Both workflows check out with `fetch-depth: 0`, so on CI the base is always there
and a **skip is a failure**: a check that cannot run is not a check. Locally a skip is ordinary — a
shallow clone, a fresh worktree, a base that was never fetched.

Two things it insists on beyond "the version differs". It has to go **up**, so resolving a conflict
by keeping the lower number is refused. And the previous-commit comparison on `master` is the only
place a *collision* is visible: two branches that both bump 1.2.0 to 1.3.0 merge without a conflict —
each side made the identical edit — and neither pull request's check ever saw the other, because
GitHub does not re-run a pull request's checks when its base moves. With several skill-touching pull
requests open at once, give each a distinct version, or bump once more after the last one lands.

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
