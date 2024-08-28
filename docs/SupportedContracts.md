# Supported Contracts

> Note: Check the [CHANGELOG](../CHANGELOG.md) to see which of the methods below are released and which ones are still in the making.

## `Base`

- `Satisfy`

## `Nullable` (extends `Base`)

- `(Not)BeNull`

## `Object` (extends `Nullable`)

- `(Not)BeAssignableTo`
- `(Not)BeOfType`

## `Equality` (extends `Object`)

- `(Not)Be`
- `(Not)BeAnyOf`

## `Collection` (extends `Equality`)

- `(Not)BeEmpty`
- `(Not)HaveCountEqualTo`
- `HaveCountBetween`
- `HaveCountGreaterOrEqualTo`
- `HaveCountGreaterThan`
- `HaveCountLessOrEqualTo`
- `HaveCountLessThan`

## `Char` (extends `Object`)

- `(Not)Be`
- `(Not)BeAlphanumeric`
- `(Not)BeAnyOf`
- `(Not)BeAscii`
- `(Not)BeDigit`
- `(Not)BeLetter`
- `(Not)BeLowercase`
- `(Not)BeUppercase`
- `(Not)BeWhiteSpace`
- `BeBetween`
- `BeGreaterOrEqualTo`
- `BeGreaterThan`
- `BeLessOrEqualTo`
- `BeLessThan`

## `String` (extends `Equality`)

- `(Not)BeAlphanumeric`
- `(Not)BeBase64`
- `(Not)BeCreditCardNumber`
- `(Not)BeEmailAddress`
- `(Not)BeEmpty`
- `(Not)BeExistingDirectory`
- `(Not)BeExistingFile`
- `(Not)BeGuid`
- `(Not)BeHexadecimal`
- `(Not)BeIpAddress`
- `(Not)BeLowercase`
- `(Not)BeMatching`
- `(Not)BeNullOrEmpty`
- `(Not)BeNullOrWhiteSpace`
- `(Not)BePalindrome`
- `(Not)BeUppercase`
- `(Not)BeUrl`
- `(Not)BeWhiteSpace`
- `(Not)Contain`
- `(Not)EndWith`
- `(Not)HaveLengthEqualTo`
- `(Not)StartWith`
- `HaveLengthBetween`
- `HaveLengthGreaterOrEqualTo`
- `HaveLengthGreaterThan`
- `HaveLengthLessOrEqualTo`
- `HaveLengthLessThan`

## `Bool` (extends `Object`)

- `(Not)Be`
- `(Not)BeAnyOf`
- `BeFalse`
- `BeTrue`

## `DateTime` (extends `Base`)

- `(Not)Be`
- `(Not)BeAnyOf`
- `(Not)BeFriday`
- `(Not)BeInApril`
- `(Not)BeInAugust`
- `(Not)BeInCurrentMonth`
- `(Not)BeInCurrentYear`
- `(Not)BeInDaylightSaving`
- `(Not)BeInDecember`
- `(Not)BeInFebruary`
- `(Not)BeInJanuary`
- `(Not)BeInJuly`
- `(Not)BeInJune`
- `(Not)BeInMarch`
- `(Not)BeInMay`
- `(Not)BeInMonth`
- `(Not)BeInNovember`
- `(Not)BeInOctober`
- `(Not)BeInSeptember`
- `(Not)BeInTheFuture`
- `(Not)BeInThePast`
- `(Not)BeInYear`
- `(Not)BeLeapYear`
- `(Not)BeLocal`
- `(Not)BeMonday`
- `(Not)BeNull`
- `(Not)BeOnCurrentDay`
- `(Not)BeOnDate`
- `(Not)BeOnDay`
- `(Not)BeOnDayOfYear`
- `(Not)BeSaturday`
- `(Not)BeSunday`
- `(Not)BeThursday`
- `(Not)BeToday`
- `(Not)BeTomorrow`
- `(Not)BeTuesday`
- `(Not)BeUtc`
- `(Not)BeWednesday`
- `(Not)BeWeekday`
- `(Not)BeWeekend`
- `(Not)BeYesterday`
- `BeBetween`
- `BeGreaterOrEqualTo`
- `BeGreaterThan`
- `BeLessOrEqualTo`
- `BeLessThan`

## `Enum` (extends `Object`)

- `(Not)Be`
- `(Not)BeAnyOf`
- `(Not)HaveFlag`

## `Guid` (extends `Object`)

- `(Not)Be`
- `(Not)BeAnyOf`
- `(Not)BeEmpty`

## `TimeSpan` (extends `Base`)

- `(Not)Be`
- `(Not)BeEqualTo`
- `(Not)BeLongerThan`
- `(Not)BeNull`
- `(Not)BeShorterThan`

## `Stream` (extends `Object`)

- `(Not)BeAbleToTimeout`
- `(Not)BeAtPosition`
- `(Not)BeReadable`
- `(Not)BeSeekable`
- `(Not)BeWithLength`
- `(Not)BeWriteable`

## `Byte` (extends `Object`)

- `(Not)Be`
- `(Not)BeAnyOf`
- `(Not)BeEven`
- `(Not)BeOdd`
- `(Not)BeZero`
- `BeBetween`
- `BeGreaterOrEqualTo`
- `BeGreaterThan`
- `BeLessOrEqualTo`
- `BeLessThan`

## `Decimal` (extends `Object`)

- `(Not)Be`
- `(Not)BeAnyOf`
- `(Not)BeNegative`
- `(Not)BePositive`
- `(Not)BeZero`
- `BeBetween`
- `BeGreaterOrEqualTo`
- `BeGreaterThan`
- `BeLessOrEqualTo`
- `BeLessThan`

## `Double` (extends `Object`)

- `(Not)Be`
- `(Not)BeAnyOf`
- `(Not)BeNegative`
- `(Not)BePositive`
- `(Not)BeZero`
- `BeBetween`
- `BeGreaterOrEqualTo`
- `BeGreaterThan`
- `BeLessOrEqualTo`
- `BeLessThan`

## `Float` (extends `Object`)

- `(Not)Be`
- `(Not)BeAnyOf`
- `(Not)BeNegative`
- `(Not)BePositive`
- `(Not)BeZero`
- `BeBetween`
- `BeGreaterOrEqualTo`
- `BeGreaterThan`
- `BeLessOrEqualTo`
- `BeLessThan`

## `Int` (extends `Object`)

- `(Not)Be`
- `(Not)BeAnyOf`
- `(Not)BeEven`
- `(Not)BeNegative`
- `(Not)BeOdd`
- `(Not)BePositive`
- `(Not)BeZero`
- `BeBetween`
- `BeGreaterOrEqualTo`
- `BeGreaterThan`
- `BeLessOrEqualTo`
- `BeLessThan`

## `Long` (extends `Object`)

- `(Not)Be`
- `(Not)BeAnyOf`
- `(Not)BeEven`
- `(Not)BeNegative`
- `(Not)BeOdd`
- `(Not)BePositive`
- `(Not)BeZero`
- `BeBetween`
- `BeGreaterOrEqualTo`
- `BeGreaterThan`
- `BeLessOrEqualTo`
- `BeLessThan`

## `Sbyte` (extends `Object`)

- `(Not)Be`
- `(Not)BeAnyOf`
- `(Not)BeEven`
- `(Not)BeNegative`
- `(Not)BeOdd`
- `(Not)BePositive`
- `(Not)BeZero`
- `BeBetween`
- `BeGreaterOrEqualTo`
- `BeGreaterThan`
- `BeLessOrEqualTo`
- `BeLessThan`

## `Short` (extends `Object`)

- `(Not)Be`
- `(Not)BeAnyOf`
- `(Not)BeEven`
- `(Not)BeNegative`
- `(Not)BeOdd`
- `(Not)BePositive`
- `(Not)BeZero`
- `BeBetween`
- `BeGreaterOrEqualTo`
- `BeGreaterThan`
- `BeLessOrEqualTo`
- `BeLessThan`

## `Uint` (extends `Object`)

- `(Not)Be`
- `(Not)BeAnyOf`
- `(Not)BeEven`
- `(Not)BeOdd`
- `(Not)BeZero`
- `BeBetween`
- `BeGreaterOrEqualTo`
- `BeGreaterThan`
- `BeLessOrEqualTo`
- `BeLessThan`

## `Ulong` (extends `Object`)

- `(Not)Be`
- `(Not)BeAnyOf`
- `(Not)BeEven`
- `(Not)BeOdd`
- `(Not)BeZero`
- `BeBetween`
- `BeGreaterOrEqualTo`
- `BeGreaterThan`
- `BeLessOrEqualTo`
- `BeLessThan`

## `Ushort` (extends `Object`)

- `(Not)Be`
- `(Not)BeAnyOf`
- `(Not)BeEven`
- `(Not)BeOdd`
- `(Not)BeZero`
- `BeBetween`
- `BeGreaterOrEqualTo`
- `BeGreaterThan`
- `BeLessOrEqualTo`
- `BeLessThan`

## `DirectoryInfo` (extends `Nullable`)

- `(Not)BeEmpty`
- `(Not)BeHidden`
- `(Not)BeReadOnly`
- `(Not)Exist`

## `FileInfo` (extends `Nullable`)

- `(Not)BeEmpty`
- `(Not)BeHidden`
- `(Not)BeReadOnly`
- `(Not)Exist`
- `(Not)HaveExtension`
- `(Not)HaveSizeEqualTo`
- `HaveSizeGreaterOrEqualTo`
- `HaveSizeGreaterThan`
- `HaveSizeLessOrEqualTo`
- `HaveSizeLessThan`

## `Dictionary` (extends `Collection`)

- `(Not)ContainKey`
- `(Not)ContainKeyValuePair`
- `(Not)ContainValue`

## `List` (extends `Collection`)

- `(Not)Contain`
- `HaveElementsOfType`
