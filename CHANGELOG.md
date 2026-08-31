# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

Every merge into `master` is released, and the per-release notes are generated from the
merged pull-requests on the [releases page](https://github.com/FluentContracts/FluentContracts/releases).
This file is the curated summary of notable changes on top of those.

## [Unreleased]
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

### Internal
- `AGENTS.md` now requires every pull request to add a changelog entry, and says which heading to use
  and how to word it.

### Packaging
- The nuget.org listing now uses its own readme (`docs/PackageReadme.md`). The repository README relies
  on raw HTML, relative links and images from domains nuget.org does not render, so it appeared mangled
  on the package page.


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

[Unreleased]: https://github.com/FluentContracts/FluentContracts/compare/2.1.0...HEAD
[2.1.0]: https://github.com/FluentContracts/FluentContracts/compare/2.0.0...2.1.0
[2.0.0]: https://github.com/FluentContracts/FluentContracts/compare/1.4.0...2.0.0
[1.4.0]: https://github.com/FluentContracts/FluentContracts/compare/1.3.0...1.4.0
[1.3.0]: https://github.com/FluentContracts/FluentContracts/compare/1.2.0...1.3.0
[1.2.0]: https://github.com/FluentContracts/FluentContracts/compare/1.1.1...1.2.0
[1.1.1]: https://github.com/FluentContracts/FluentContracts/compare/1.0.1...1.1.1
[1.0.1]: https://github.com/FluentContracts/FluentContracts/tree/1.0.1
