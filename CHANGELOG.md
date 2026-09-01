# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

Every merge into `master` is released, and the per-release notes are generated from the
merged pull-requests on the [releases page](https://github.com/FluentContracts/FluentContracts/releases).
This file is the curated summary of notable changes on top of those.

## [Unreleased]

## [3.6.1] / 2026-09-01
### Packaging
- The `net8.0` assets declare trimming and Native AOT compatibility (`IsAotCompatible`), so a
  trimmed or AOT-published app no longer warns about the package. The library's one reflection
  path — constructing a user-defined exception for checks like `Satisfy<T, TException>` — is
  annotated with `DynamicallyAccessedMembers` through the whole call chain, which keeps the
  exception's `(string message)` constructor through trimming; verified on a `TrimMode=full`
  publish and a Native AOT publish, both of which run every path including that one. The trim
  analyzers now run on every build with warnings as errors, so an unannotated reflection use
  cannot be added silently. `netstandard2.0` ships the same annotated code via PolySharp's
  attribute polyfills; behaviour there is unchanged.

## [3.6.0] / 2026-09-01
### Added
- `Value()` ends a chain with the value it just validated, so the guard and the read are one
  expression: `this.port = config.Port.Must().BeBetween(1, 65535).Value();`. It returns the
  unwrapped, non-nullable value (`int` from an `int?` argument), and a null argument fails with
  `ArgumentNullException` naming the argument, exactly as `NotBeNull` would. Available on every
  contract, and only after at least one check has run.
- `DateOnlyContract` and `TimeOnlyContract`, on the `net8.0` target (the types do not exist on
  `netstandard2.0`, which simply does not see the new `Must()` overloads). `DateOnly` gets equality,
  `BeAnyOf`, the comparison family, `BeBetween`, `BeInThePast`/`BeInTheFuture`/`BeToday` — driven by
  the same `IDateTimeProvider` the `DateTime` contract uses, so tests can pin the clock — and
  `BeWeekday`/`BeWeekend`. `TimeOnly` gets equality, `BeAnyOf`, comparisons and `BeBetween`/
  `NotBeBetween` with `TimeOnly.IsBetween`'s semantics: start inclusive, end exclusive, and the
  window wraps midnight, so between 22:00 and 02:00 contains 23:30.
- `BeDefined` and `NotBeDefined` on the enum contract: `(DayOfWeek)9` is representable and flowed
  through every existing check; `BeDefined` rejects it. On a `[Flags]` enum an undeclared
  combination of flags is not defined, matching `Enum.IsDefined`.
- `BeInAscendingOrder`, `BeInDescendingOrder` and their `Not` mirrors on the list contract, each
  with an `IComparer<T>` overload. Non-strict — equal neighbours are in order, matching
  `List<T>.Sort` — and the failure names the first out-of-order neighbours. An empty or
  single-element list is vacuously in every order, so it satisfies the positive checks and fails
  the negations.

### Fixed
- `NotBeShorterThan` and `NotBeLongerThan` on the `TimeSpan` contract used to **throw** for a span
  exactly equal to the expected value — they were implemented as strict `>` and `<` instead of the
  actual negations `>=` and `<=`. A span equal to the bound now passes both, since it is neither
  shorter nor longer.

## [3.5.0] / 2026-09-01
### Changed
- Every check that fails without a caller-supplied message now says what was expected of which
  argument, and what the value actually was: `Expected quantity to be greater than 5, but found 3.`
  instead of the framework's `Specified argument was out of the range of valid values.` The old
  default messages carried no information at all, so code that matched on them exactly is the only
  code that can notice — a caller-supplied message still replaces the default entirely, and the
  exception types, `ParamName` and check semantics are unchanged. Values are rendered in the
  invariant culture; strings are quoted and truncated at 64 characters, collections at 5 items;
  credit-card checks deliberately never put the value in the message.
- Contract failures no longer point inside the library. The validator and throw-helper frames are
  hidden from the stack trace (`[StackTraceHidden]`) and skipped by the debugger
  (`[DebuggerStepThrough]`), so the trace starts at the check the caller wrote. On runtimes older
  than .NET 6 (the `netstandard2.0` target on .NET Framework) stack traces keep the frames — the
  attribute is ignored there.

### Internal
- Every change now starts as an issue, and the pull request references it — recorded in `AGENTS.md`
  and `CONTRIBUTING.md`.

## [3.4.0] / 2026-09-01
### Added
- `BeNaN`, `NotBeNaN`, `BeInfinity`, `NotBeInfinity`, `BeFinite` and `NotBeFinite` on the `double` and
  `float` contracts — the values an ordering comparison cannot express, now assertable deliberately.
  `BeFinite` is the one to reach for before comparing.

### Fixed
- `NaN` no longer satisfies an ordering check on a `double` or `float` argument.
  `double.NaN.Must().BeNegative()` and `.BeLessThan(0)` both used to **pass**, because `Comparer<T>` is
  a total order that sorts `NaN` below every other value — so the contract went silently unenforced,
  the same shape as the null policy fixed in 3.0.0. Every ordering check now rejects `NaN` with
  `ArgumentOutOfRangeException`, matching IEEE, where no ordering comparison with `NaN` is true.
  Infinity is deliberately untouched: it orders correctly.

### Internal
- The changelog push now clears the Authorization header `actions/checkout` leaves configured for
  github.com. Git sends that header whatever credentials a remote URL carries, so the push went out as
  `github-actions[bot]` rather than as the app however the remote was spelled, and 3.3.0's changelog
  commit was rejected by the very rules the app is exempt from. Verified by watching what git actually
  sends: one Authorization header before the reset, none after.
- The 3.3.0 section was written by running the release's own transform for that version, since the
  push that should have written it never landed.

## [3.3.0] / 2026-09-01
### Internal
- Corrected why a merge can lose its `+semver:` directive, in `AGENTS.md`, `CONTRIBUTING.md` and the
  message the release fails with. It is not a stale merge page: GitHub's squash default takes the
  merged subject from the pull request title only when the branch has more than one commit, and with
  exactly one it uses that commit's own subject, so a directive in the title never reaches the commit
  the release reads. Both losses so far were single-commit pull requests; every multi-commit one kept
  its marker.

### Added
- `BeAnyOf` and `NotBeAnyOf` take a sequence with the message second — `BeAnyOf(IEnumerable<T> values,
  string? message = null)` — matching `ListContract.Contain` and unable to confuse a value for a message.

### Deprecated
- `BeAnyOf(string? message, params T[] values)` and `NotBeAnyOf(string? message, params T[] values)` on
  `EqualityContract`. When the argument is a string, the compiler prefers this overload for a call like
  `BeAnyOf("a", "b")` — it has more declared parameters, and both are applicable only in expanded form —
  so `"a"` became the message and only `"b"` was checked. Callers now get a compiler warning naming the
  trap; the overload is removed in 4.0.0. The value-type contracts declare the same shape but cannot be
  reached by it, and are untouched.

### Fixed
- `BeAnyOf` and `NotBeAnyOf` with a **single** value on a string argument. `"a".Must().BeAnyOf("a")` used
  to throw and `"a".Must().NotBeAnyOf("a")` used to pass, both because the value was taken as the message
  and the set was left empty — so the first could never succeed and the second could never fail. A single
  value now binds to its own overload. Several values on a string argument still reach the deprecated
  overload and keep the old behaviour, which only removing it can settle.
- The release workflow finalises `CHANGELOG.md` again. The commit message was assembled with quotes of
  our own around values that the build's argument handler also quotes, so `git commit -m` received a
  nested pair, took the tail of the message as pathspecs and failed. 3.2.0 published normally but its
  `## [Unreleased]` section was left unrenamed, and the release run went red after the package was
  already out.
- Finalising no longer rewrites `## [Unreleased]` where an entry quotes it in prose. The heading was
  matched anywhere in the file rather than at the start of a line, so the entry describing this very
  step was mangled when the section was renamed.

### Internal
- A merge is now held back from releasing by the `skip-release` **label** on its pull request, read
  from the pull request the merge commit came from. `[skip release]` in a commit message still works,
  for a direct push and for anything already written down, but it is no longer the mechanism to reach
  for: GitHub fills the squash commit subject in when the merge box is rendered, so a pull request
  titled with the marker can merge without it — which is what happened after 3.2.0, and only the run
  being cancelled mid-build stopped an unintended 3.2.1 from being published.
- A release now stops if its pull request asks for a `+semver:` bump that the merged commit does not
  carry. The directive is read from the commit message, but the commit subject is composed from the
  title when the merge box is rendered, so a title edited afterwards can lose it and ship breaking
  changes as a patch. The check runs before anything is published, so failing costs nothing.
- A failure while finalising the changelog can no longer fail the release. The step already ran after
  the package was pushed, but its exception still left the workflow red, which reads as a failed
  release; it now reports the problem and leaves the run green.
- The changelog push is logged again. Only the command carrying the app token is hidden, so a rejected
  push says why instead of failing silently.

## [3.2.0] / 2026-08-31
### Added
- Five checks on `CollectionContract`, so they are available on every collection contract — `List`,
  `Array` and `Dictionary` alike: `ContainAnyOf` (at least one of the given elements is present),
  `HaveUniqueItems`, `NotContainNull`, `AllSatisfy` and `AnySatisfy`. `AllSatisfy` and `AnySatisfy`
  take a predicate over the element type; on a dictionary that element is a `KeyValuePair<TKey, TValue>`.
  An empty collection satisfies `AllSatisfy` and fails `AnySatisfy`, and a collection of a non-nullable
  value type always satisfies `NotContainNull`.

### Fixed
- `NotContain` on a list no longer accepts a collection that holds *some* of the given elements. It was
  implemented as the negation of `Contain`, which asks whether the collection holds them **all**, so
  `list.Must().NotContain(2, 99)` passed for `[1, 2, 3]` — the forbidden `2` was present and the
  contract was silently not enforced. It now fails if *any* of the elements is present. Passing a single
  element was always correct and is unchanged, as is `Contain`, which still requires every element.
  Calling `NotContain()` with no elements now passes instead of throwing, matching `Contain()`.

### Packaging
- The XML documentation shipped in the package now covers **every** public member. Only the checks
  were documented before, so the contract types, their constructors, `BaseContract`'s `ArgumentValue`
  and `ArgumentName`, the `Must()` extension classes, `Month`, `ParseOptions` and `IDateTimeProvider`
  all appeared blank in consumers' IntelliSense. That is 118 members newly documented, and the
  package's XML goes from 735 entries to 882.

### Internal
- `AGENTS.md` now requires XML documentation on every public member rather than only on the checks,
  and `CS1591` is no longer suppressed in the library project, so an undocumented public member fails
  the build rather than silently shipping a blank.
- The release workflow now finalises `CHANGELOG.md` itself: after a successful publish it renames
  `## [Unreleased]` to the version it shipped, dates it, leaves a fresh empty section above it,
  updates the comparison links and commits that back to `master`. The commit is pushed with a
  short-lived GitHub App token, because the built-in `GITHUB_TOKEN` cannot be given a bypass on a
  protected branch; without the app's secrets the release still publishes and only the changelog
  commit is skipped.
- A pull request whose title contains `[skip release]` is built and tested on `master` but not
  packed, published or tagged, so changes that do not belong in a package no longer force a version.

## [3.1.0] / 2026-08-31
### Added
- `UriContract` (`Uri.Must()`): `(Not)BeAbsolute`, `(Not)HaveScheme`, `(Not)BeHttps`, `(Not)HaveHost`,
  `(Not)HavePort`, `(Not)BeLoopback` and `(Not)BeFile`, plus the inherited equality and null checks.
  Checks that read the scheme, host or port require an absolute URI: those properties throw for a
  relative one, so the contract fails first rather than letting `InvalidOperationException` escape.
- `DateTimeOffsetContract` (`DateTimeOffset.Must()`): comparisons (`BeGreaterThan`,
  `BeGreaterOrEqualTo`, `BeLessThan`, `BeLessOrEqualTo`, `BeBetween`), `(Not)BeUtc`,
  `(Not)HaveOffset` and `(Not)BeInThePast` / `(Not)BeInTheFuture`, which accept an explicit reference
  point or take the current moment from an injected `IDateTimeProvider`.

### Fixed
- `Satisfy<T>` no longer throws `InvalidCastException` from inside the library when the argument is
  not a `T`. It now fails as a contract violation naming the argument, honouring the supplied message
  and the requested exception type.
- A user-defined exception is now built through its `(string message)` constructor instead of having
  the message assigned to a private field. The constructor used to be skipped entirely, so any work it
  did was silently lost, and an exception that stores its own message rather than deferring to
  `Exception` ended up with no message at all. Exceptions without such a constructor keep the previous
  behaviour, so no message is lost.

### Packaging
- The nuget.org listing now uses its own readme (`docs/PackageReadme.md`). The repository README relies
  on raw HTML, relative links and images from domains nuget.org does not render, so it appeared mangled
  on the package page.

### Internal
- `AGENTS.md` now requires every pull request to add a changelog entry, and says which heading to use
  and how to word it.
- `DummyData.GetDateTime(SpecificDay)` picked a month at random and then asked for the requested day
  inside it, which throws when that month never reaches it: a 31st failed about 42% of the time. It now
  picks among the months that can hold the day.
- Tests run serially. `DummyData` draws from a single `Faker` seeded with a fixed number, which only
  yields reproducible data when one test draws at a time, and `System.Random` is not safe to share
  across threads.

## [3.0.0] / 2026-08-30
### Breaking
- Ordering comparisons now reject a `null` argument with `ArgumentNullException`. Previously
  `BeNegative` and `NotBePositive` accepted `null` and **passed silently**, while `BePositive`
  and `NotBeNegative` threw `ArgumentOutOfRangeException`. Code that relied on either of those
  behaviours will now see `ArgumentNullException` instead. Equality and null checks are
  unchanged and still accept `null`. Affects the `Int`, `Long`, `Short`, `Sbyte`, `Double`,
  `Float` and `Decimal` contracts. The guard now lives inside the `Validator` ordering methods,
  so it cannot be forgotten by a future check.

  Note that the change of target frameworks below is *not* breaking for consumers: a `net6.0`
  or `net7.0` project resolves the `netstandard2.0` assets.

### Changed
- `master` is now the only long-lived branch. Releases are produced automatically on every
  merge into it, replacing the manual GitFlow release process.
- Packages are published with NuGet Trusted Publishing (OIDC), so no long-lived NuGet API
  key is stored in the repository.

### Packaging
- Target frameworks changed from `net6.0` (end of support) to `netstandard2.0` and `net8.0`.
  `netstandard2.0` widens support to .NET Framework 4.6.1+, Mono and Unity consumers.
  The package still has no runtime dependencies.
- The package now ships XML documentation, so the contract summaries appear in IntelliSense.
- The README is now included in the package and rendered on the nuget.org listing.
- Fixed 166 XML doc references that pointed at parameters with `cref` instead of `paramref`,
  which previously left them unresolved in the generated documentation.

### `FileInfoContract`
- Added `HaveSizeEqualTo`
- Added `HaveSizeLessThan`
- Added `HaveSizeLessOrEqualTo`
- Added `HaveSizeGreaterThan`
- Added `HaveSizeGreaterOrEqualTo`

## [2.1.0] / 2024-08-23
### `ObjectContract`
- Added `(Not)BeOfType<T>` and `(Not)BeOfType(type)`
- Added `(Not)BeAssignableTo(type)` and `(Not)BeAssignableTo<T>`

### `IntContract`, `UintContract`, `LongContract`, `UlongContract`, `ShortContract`, `UshortContract`, `ByteContract`, `SbyteContract`
- Added `(Not)BeOdd`
- Added `(Not)BeEven`

### `DictionaryContract` (newly added contract)
- Added `(Not)ContainKey` and `(Not)ContainValue`
- Added `(Not)ContainKeyValuePair(KeyValuePair)` and `(Not)ContainKeyValuePair(key, value)`

### `FileInfoContract` (newly added contract)
- Added `(Not)Exist`
- Added `(Not)HaveExtension`
- Added `(Not)BeReadOnly`
- Added `(Not)BeHidden`
- Added `(Not)BeEmpty`

### `DirectoryInfoContract` (newly added contract)
- Added `(Not)Exist`
- Added `(Not)BeReadOnly`
- Added `(Not)BeHidden`
- Added `(Not)BeEmpty`

### `TimeSpanContract` (newly added contract)
- Added `(Not)Be`
- Added `(Not)BeNull`
- Added `(Not)BeShorterThan`
- Added `(Not)BeLongerThan`
- Added `(Not)BeEqualTo`

## [2.0.0] / 2024-06-16
### General
- Enabled `<Nullable>` on the project for the library, as I had forgotten about it and this was causing a lot of the contracts to be missing on nullable types
- This led to a major refactoring, as it turned out I was not inheriting the contracts correctly. This was causing many extension to not work or to cause compiler warnings.
- Removed `Be<TException>` since it did not seem to fit in the general idea. Most likely I will enable custom exceptions in general. Stay tuned for that. For now it is only available on `NotNull` and `Satisfy` contracts.

### `BaseContract`
- Added `Satisfy<TException>` that can throw custom exceptions

### `StringContract`
- Added `(Not)BeExistingFile`
- Added `(Not)BeExistingDirectory`
- Added `(Not)BeHexadecimal`
- Added `(Not)BeBase64`
- Added `(Not)BeCreditCardNumber`

### `CollectionContract`
- Renamed `(Not)BeWithCount` to `(Not)HaveCountEqualTo` to match the others like that
- Added `HaveCountGreaterThan`, `HaveCountGreaterOrEqualTo`, `HaveCountLessThan`, `HaveCountLessOrEqualTo` and `HaveCountBetween` to validate the count of elements in a collection.

### `DateTimeContract`
- Added `(Not)BeOnDate` with `DateTime` parameter
- Added `(Not)BeInThePast`
- Added `(Not)BeInTheFuture`
- Added `(Not)BeToday`
- Added `(Not)BeTomorrow`
- Added `(Not)BeYesterday`
- Added `(Not)BeInMonth`
- Added `(Not)BeOnDay`
- Added `(Not)BeInYear`
- Added `(Not)BeOnCurrentDay`
- Added `(Not)BeInCurrentMonth`
- Added `(Not)BeInCurrentYear`
- Added `(Not)BeOnDayOfYear`
- Added `(Not)BeWeekend`
- Added `(Not)BeWeekday`

## [1.4.0] / 2024-05-20
- Added `(Not)BeEmailAddress` for validating if a string is an email address. [suggested by [@matkoch87](https://x.com/matkoch87/status/1787511006085705889)]
- Added `(Not)BeMatching` to validate an argument against a regex pattern
- Added `(Not)StartWith` and `(Not)EndWith` 
- Added `(Not)BeIpAddress` to validate is the string is a valid IP address
- Added `(Not)BeGuid`
- Added `(Not)Url`
- Added `(Not)BePalindrome` to validate if a string is a palindrome (when reversed the string remains the same)
- Added `(Not)HaveLengthEqualTo`, `HaveLengthGreaterThan`, `HaveLengthGreaterOrEqualTo`, `HaveLengthLessThan`, `HaveLengthLessOrEqualTo` and `HaveLengthBetween` to validate the length of a string.
- Added `(Not)BeAlphanumeric`
- Added `(Not)BeAlphanumeric` to `CharContract`
- Added `(Not)BePositive`, `(Not)BeNegative` and `(Not)BeZero` for all numeric contracts
- Added `HaveElementsOfType<TElement>` to `ListContract`
- Added `(Not)BeOnDate` to `DateTimeContracts`
- Removed `PureAttribute` from all contract methods, as it was producing an unpleasant compiler warning - [CA1806](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1806)
 
## [1.3.0] / 2024-05-06
- Added `sbyte` contracts
- Added `ushort` contracts
- Added `ulong` contracts
- Added `uint` contracts
- Added contracts for `Array`
- Added contracts for `List<T>`
- Added contracts for `Enums`
- Added contracts for `Streams`

## [1.2.0] / 2024-04-29
- Added a `CAHNGELOG` (this one right here 😏)
- Added `DateTimeContracts` for validating arguments of type `DateTime`
- Renamed `NumberContract` to `ComparableContract`

## [1.1.1] / 2024-04-26
- Added contracts for `char`
- Add user defined exceptions for `Be`
- Added `[NotNull]` attribute to arguments of `BeNotNull` so compiler knows it was checked afterwards

## [1.0.1] / 2024-04-23
- Initial release

[Unreleased]: https://github.com/FluentContracts/FluentContracts/compare/3.6.1...HEAD
[3.6.1]: https://github.com/FluentContracts/FluentContracts/compare/3.6.0...3.6.1
[3.6.0]: https://github.com/FluentContracts/FluentContracts/compare/3.5.0...3.6.0
[3.5.0]: https://github.com/FluentContracts/FluentContracts/compare/3.4.0...3.5.0
[3.4.0]: https://github.com/FluentContracts/FluentContracts/compare/3.3.0...3.4.0
[3.3.0]: https://github.com/FluentContracts/FluentContracts/compare/3.2.0...3.3.0
[3.2.0]: https://github.com/FluentContracts/FluentContracts/compare/3.1.0...3.2.0
[3.1.0]: https://github.com/FluentContracts/FluentContracts/compare/3.0.0...3.1.0
[3.0.0]: https://github.com/FluentContracts/FluentContracts/compare/2.1.0...3.0.0
[2.1.0]: https://github.com/FluentContracts/FluentContracts/compare/2.0.0...2.1.0
[2.0.0]: https://github.com/FluentContracts/FluentContracts/compare/1.4.0...2.0.0
[1.4.0]: https://github.com/FluentContracts/FluentContracts/compare/1.3.0...1.4.0
[1.3.0]: https://github.com/FluentContracts/FluentContracts/compare/1.2.0...1.3.0
[1.2.0]: https://github.com/FluentContracts/FluentContracts/compare/1.1.1...1.2.0
[1.1.1]: https://github.com/FluentContracts/FluentContracts/compare/1.0.1...1.1.1
[1.0.1]: https://github.com/FluentContracts/FluentContracts/tree/1.0.1
