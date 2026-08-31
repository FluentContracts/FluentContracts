# FluentContracts

[![NuGet Version](https://img.shields.io/nuget/v/FluentContracts?style=flat-square&logo=nuget&logoColor=white&color=green)](https://www.nuget.org/packages/FluentContracts/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/FluentContracts?style=flat-square&logo=nuget&logoColor=white)](https://www.nuget.org/packages/FluentContracts/)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue?style=flat-square)](https://github.com/FluentContracts/FluentContracts/blob/master/LICENSE)

Argument validation contracts, written the way you would say them out loud.

Inspired by [FluentAssertions](https://github.com/fluentassertions/fluentassertions), and MIT licensed.

## Why another validation library

Libraries like [FluentValidation](https://github.com/FluentValidation/FluentValidation) and `Guard` from the
[.NET Community Toolkit](https://github.com/CommunityToolkit/dotnet) are excellent, and if you already use
them, keep doing so.

This one exists because guard clauses tend to read as noise. FluentContracts aims for something simpler and
more human-readable, the way FluentAssertions does for unit testing.

## Getting started

```
dotnet add package FluentContracts
```

Targets `netstandard2.0` and `net8.0`, so it runs on .NET Framework 4.6.1+, .NET Core, .NET 5 and later, Mono
and Unity. It has **no runtime dependencies**.

## Usage

Validation normally looks like this:

```csharp
public void AddOrder(Order myOrder)
{
    if (myOrder == null) throw new ArgumentNullException(nameof(myOrder));
    if (myOrder.Quantity < 5) throw new OrderQuantityException("Order quantity cannot be less than 5");

    ...
}
```

With FluentContracts it reads like the rule it enforces:

```csharp
public void AddOrder(Order myOrder)
{
    myOrder
        .Must()
        .NotBeNull()
        .And
        .Satisfy<OrderQuantityException>(
            o => o.Quantity >= 5,
            "Order quantity cannot be less than 5");

    ...
}
```

Or as simple as:

```csharp
public int Divide(int a, int b)
{
    b.Must().NotBe(0);
    return a / b;
}
```

The argument name is captured automatically, so the exception points at the right parameter without you
repeating it.

### Your own exceptions

Any check can throw an exception of your choosing:

```csharp
public void AddOrder(Order myOrder)
{
    myOrder.Must().NotBeNull<OrderNullException>();
}
```

This throws `OrderNullException` when `myOrder` is `null`.

## What you can check

Contracts are available for the numeric types, `string`, `char`, `bool`, `Guid`, `DateTime`, `TimeSpan`,
enums, collections, dictionaries, streams, `FileInfo` and `DirectoryInfo` — things like `NotBeNullOrEmpty`,
`BeBetween`, `HaveCountGreaterThan`, `BeEmailAddress`, `Exist` and many more.

The full list lives in
[SupportedContracts.md](https://github.com/FluentContracts/FluentContracts/blob/master/docs/SupportedContracts.md).

## Help needed

The goal is for this to be exhaustive, safe and stable enough for production use on large projects, and help
is very welcome. Open a pull request, file an issue, or start a discussion.

- [Repository](https://github.com/FluentContracts/FluentContracts)
- [Issues](https://github.com/FluentContracts/FluentContracts/issues)
- [Discussions](https://github.com/FluentContracts/FluentContracts/discussions)
- [Changelog](https://github.com/FluentContracts/FluentContracts/blob/master/CHANGELOG.md)
- [Contributing guidelines](https://github.com/FluentContracts/FluentContracts/blob/master/CONTRIBUTING.md)

## License

[MIT](https://github.com/FluentContracts/FluentContracts/blob/master/LICENSE) © Todor Todorov
