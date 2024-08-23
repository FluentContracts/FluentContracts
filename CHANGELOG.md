# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]
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

[Unreleased]: https://github.com/FluentContracts/FluentContracts/compare/2.0.0...HEAD
[2.0.0]: https://github.com/FluentContracts/FluentContracts/compare/1.4.0...2.0.0
[1.4.0]: https://github.com/FluentContracts/FluentContracts/compare/1.3.0...1.4.0
[1.3.0]: https://github.com/FluentContracts/FluentContracts/compare/1.2.0...1.3.0
[1.2.0]: https://github.com/FluentContracts/FluentContracts/compare/1.1.1...1.2.0
[1.1.1]: https://github.com/FluentContracts/FluentContracts/compare/1.0.1...1.1.1
[1.0.1]: https://github.com/FluentContracts/FluentContracts/tree/1.0.1
