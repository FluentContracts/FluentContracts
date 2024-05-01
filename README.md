<img alt="Logo" width="100px" src="https://github.com/FluentContracts/FluentContracts/raw/develop/assets/icon.png"/>

# FluentContracts
[![NuGet Version](https://img.shields.io/nuget/v/FluentContracts?style=for-the-badge&logo=nuget&logoColor=white&color=green)](https://www.nuget.org/packages/FluentContracts/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/FluentContracts?style=for-the-badge&logo=nuget&logoColor=white)](https://www.nuget.org/packages/FluentContracts/)

API for defining argument validation contracts in a fluent manner.

Inspired by [FluentAssertions](https://github.com/fluentassertions/fluentassertions)

## Why another validation library

I am  perfectly aware of the other libraries out there, that are doing the same stuff.

Libraries like [FluentValidation](https://github.com/FluentValidation/FluentValidation) and `Guard` from [.NET Community Toolkit](https://github.com/CommunityToolkit/dotnet) are awesome 
and have tons of functionality, support and experience. If you like those and use them already, that is fine.

I did this one, because I don't really like how the other libraries require you to write, in order to achieve the validation.
It is pretty verbose for my taste. I wanted to make something more simple and "human-readable", the same way [FluentAssertions](https://github.com/fluentassertions/fluentassertions) does it for unit testing.

## Usage

The usage of the contracts is pretty simple. You can use the extension methods everywhere you want to do a validation of some variable.

Generally when we write methods, constructors, etc., we do validation like that:
```csharp
public void AddOrder(Order myOrder)
{
    if (myOrder == null) throw new ArgumentNullException(nameof(myOrder));    
    if (myOrder.Quantity < 5) throw new OrderQuantityException("Order quantity cannot be less than 5");
    
    ...
}
```

With `FluentContracts` your code will look like this:
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

or as simple as:

```csharp
public int Divide(int a, int b)
{
    b.Must().NotBe(0);    
    return a / b;
}
```
### User defined exceptions

You can also throw your own exception like that:
```csharp
public void AddOrder(Order myOrder)
{
    myOrder.Must().NotBeNull<OrderNullException>();
}
```

This will throw an instance of `OrderNullException` if `myOrder` is `null`.

## Supported contracts

|   Contract   |    Extends    | Contracts                                                                                                                                                                                                                                                                                                                                                                                                                                       |
|:------------:|:-------------:|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
|    `Base`    |       -       | `Satisfy`                                                                                                                                                                                                                                                                                                                                                                                                                                       |
|  `Nullable`  |    `Base`     | `(Not)BeNull`                                                                                                                                                                                                                                                                                                                                                                                                                                   |
|  `Equality`  |  `Nullable`   | `(Not)Be`                                                                                                                                                                                                                                                                                                                                                                                                                                       |
| `Comparable` |  `Equality`   | `(Not)BeAnyOf`, `BeBetween`, `BeGreaterThan`, `BeGreaterOrEqualTo`, `BeLessThan`, `BeLessOrEqualTo`                                                                                                                                                                                                                                                                                                                                             |
| `Collection` |  `Equality`   | `(Not)BeEmpty`, `(Not)BeWithCount`                                                                                                                                                                                                                                                                                                                                                                                                              |
|    `bool`    | `Comparable`  | `BeTrue`, `BeFalse`                                                                                                                                                                                                                                                                                                                                                                                                                             |
|    `char`    | `Comparable`  | `(Not)BeDigit`, `(Not)BeLetter`, `(Not)BeLowercase`, `(Not)BeUppercase`, `(Not)BeWhiteSpace`, `(Not)BeAscii`                                                                                                                                                                                                                                                                                                                                    |
|    `Guid`    | `Comparable`  | `(Not)BeEmpty`                                                                                                                                                                                                                                                                                                                                                                                                                                  |
|   `string`   | `Comparable`  | `(Not)BeEmpty`, `(Not)BeNullOrEmpty`, `(Not)BeWhiteSpace`, `(Not)BeNullOrWhiteSpace`, `(Not)BeUppercase`, `(Not)BeLowercase`, `(Not)Contain`                                                                                                                                                                                                                                                                                                    |
|  `DateTime`  | `Comparables` | `(Not)BeInDaylightSaving`, `(Not)BeLeapYear`, `(Not)BeInJanuary`, `(Not)BeInFebruary`, `(Not)BeInMarch`, `(Not)BeInApril`, `(Not)BeInMay`, `(Not)BeInJune`, `(Not)BeInJuly`, `(Not)BeInAugust`, `(Not)BeInSeptember`, `(Not)BeInOctober`, `(Not)BeInNovember`, `(Not)BeInDecember`, `(Not)BeUtc`, `(Not)BeLocal`, `(Not)BeMonday`, `(Not)BeTuesday`, `(Not)BeWednesday`, `(Not)BeThursday`, `(Not)BeFriday`, `(Not)BeSaturday`, `(Not)BeSunday` |

## Help needed 🙏

My goal for this project is to become as exhaustive, safe and stable as possible, so people can use it in production and on big projects.
So I need some help. If you are interested in helping out just send a pull request, open an issue, etc.

## Repository 🚧

### Builds

|     Type      | Status                                                                                                                                                                                                                                                                 |
|:-------------:|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
|   Dev Build   | [![Dev Linux](https://img.shields.io/github/actions/workflow/status/FluentContracts/FluentContracts/dev-linux.yml?branch=dev&style=for-the-badge&logo=linux&logoColor=white)](https://github.com/FluentContracts/FluentContracts/actions)                              |
|   Dev Build   | [![Dev Windows](https://img.shields.io/github/actions/workflow/status/FluentContracts/FluentContracts/dev-windows.yml?branch=dev&style=for-the-badge&logo=windows10&logoColor=white)](https://github.com/FluentContracts/FluentContracts/actions)                      |
|   Dev Build   | [![Dev MacOS](https://img.shields.io/github/actions/workflow/status/FluentContracts/FluentContracts/dev-macos.yml?branch=dev&style=for-the-badge&logo=Apple&logoColor=white)](https://github.com/FluentContracts/FluentContracts/actions)                              |
| Code Coverage | [![Coveralls](https://img.shields.io/coverallsCoverage/github/FluentContracts/FluentContracts?branch=dev&style=for-the-badge&logo=coveralls&logoColor=white)](https://coveralls.io/github/FluentContracts/FluentContracts)                                             |
|    Release    | [![Release](https://img.shields.io/github/actions/workflow/status/FluentContracts/FluentContracts/master-release.yml?branch=master&style=for-the-badge&logo=nuget&logoColor=white&label=NuGet%20Packages)](https://github.com/FluentContracts/FluentContracts/actions) |

### Status

![Alt](https://repobeats.axiom.co/api/embed/5aeeab6e5ce07439108408d66453df63f9379eeb.svg "Repobeats analytics image")

### How to build locally

- Clone repos
- Run `build.cmd`

## Where to find me 🕵️

[![Blog](https://img.shields.io/badge/Blog-todorov.bg-black.svg?style=for-the-badge&logo=jekyll&logoColor=white)](https://todorov.bg)
[![X](https://img.shields.io/badge/twitter-%40totollygeek-lightgreen.svg?style=for-the-badge&logo=x&logoColor=white)](https://twitter.com/totollygeek)
[![LinkedIn](https://img.shields.io/badge/linkedin-totollygeek-blue.svg?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/totollygeek)
[![Mastodon](https://img.shields.io/badge/Mastodon-%40totollygeek@infosec.exchange-darkblue.svg?style=for-the-badge&logo=mastodon&logoColor=white)](https://infosec.exchange/@totollygeek)
[![Threads](https://img.shields.io/badge/Threads-%40totollygeek-red.svg?style=for-the-badge&logo=threads&logoColor=white)](https://www.threads.net/@totollygeek)
[![BlueSky](https://img.shields.io/badge/BlueSky-totollygeek.com-lightblue.svg?style=for-the-badge&logo=bluesky&logoColor=white)](https://bsky.app/profile/totollygeek.com)
[![Linktree](https://img.shields.io/badge/Linktree-totollygeek-yellow.svg?style=for-the-badge&logo=linktree&logoColor=white)](https://linktr.ee/totollygeek)
[![Email](https://img.shields.io/badge/Email-fluentcontracts@pm.me-blue.svg?style=for-the-badge&logo=proton&logoColor=white)](mailto://fluentcontracts@pm.me)


## Credits 🙇‍♂️

#### [Matthias Koch](https://twitter.com/matkoch87)
> The creator of [NUKE](https://nuke.build), because I cannot build any .NET project without it and because he helped me tremendously in setting up the repository and everything around this project. (_I have also copy-pasted, like his entire build and some markdown files_ 🤫)

#### [Dennis Doomen](https://twitter.com/ddoomen)
> The "[FluentAssertions](https://fluentassertions.com/)" guy. This whole project was inspired by how that library works and I might have copy-pasted also parts of his repo too 😏

Icon made by [IconMonk](https://www.flaticon.com/authors/icon-monk) from [Flaticon](https://www.flaticon.com) 
