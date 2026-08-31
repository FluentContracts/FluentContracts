using System;
using FluentAssertions;
using FluentContracts.Contracts.Web;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests.Web;

[ContractTest("Uri")]
public class UriContractTests : Tests
{
    [Fact]
    public void Test_Must_BeNull()
    {
        TestContract<Uri?, UriContract, ArgumentOutOfRangeException>(
            null,
            DummyData.GetUri(),
            (testArgument, message) => testArgument.Must().BeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeNull()
    {
        TestContract<Uri?, UriContract, ArgumentNullException>(
            DummyData.GetUri(),
            null,
            (testArgument, message) => testArgument.Must().NotBeNull(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_Be()
    {
        var pair = DummyData.GetUriPair();

        TestContract<Uri?, UriContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().Be(pair.TestArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBe()
    {
        var pair = DummyData.GetUriPair();

        TestContract<Uri?, UriContract, ArgumentOutOfRangeException>(
            pair.TestArgument,
            pair.DifferentArgument,
            (testArgument, message) => testArgument.Must().NotBe(pair.DifferentArgument, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeAbsolute()
    {
        TestContract<Uri?, UriContract, ArgumentOutOfRangeException>(
            DummyData.GetUri(),
            DummyData.GetRelativeUri(),
            (testArgument, message) => testArgument.Must().BeAbsolute(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeAbsolute()
    {
        TestContract<Uri?, UriContract, ArgumentOutOfRangeException>(
            DummyData.GetRelativeUri(),
            DummyData.GetUri(),
            (testArgument, message) => testArgument.Must().NotBeAbsolute(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_HaveScheme()
    {
        TestContract<Uri?, UriContract, ArgumentOutOfRangeException>(
            DummyData.GetUriWithScheme("ftp"),
            DummyData.GetUriWithScheme("https"),
            (testArgument, message) => testArgument.Must().HaveScheme("ftp", message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotHaveScheme()
    {
        TestContract<Uri?, UriContract, ArgumentOutOfRangeException>(
            DummyData.GetUriWithScheme("https"),
            DummyData.GetUriWithScheme("ftp"),
            (testArgument, message) => testArgument.Must().NotHaveScheme("ftp", message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_HaveScheme_Is_Case_Insensitive()
    {
        DummyData.GetUriWithScheme("https")
            .Must()
            .Invoking(c => c.HaveScheme("HTTPS"))
            .Should()
            .NotThrow("a URI scheme is case insensitive");
    }

    [Fact]
    public void Test_Must_BeHttps()
    {
        TestContract<Uri?, UriContract, ArgumentOutOfRangeException>(
            DummyData.GetUriWithScheme("https"),
            DummyData.GetUriWithScheme("http"),
            (testArgument, message) => testArgument.Must().BeHttps(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeHttps()
    {
        TestContract<Uri?, UriContract, ArgumentOutOfRangeException>(
            DummyData.GetUriWithScheme("http"),
            DummyData.GetUriWithScheme("https"),
            (testArgument, message) => testArgument.Must().NotBeHttps(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_HaveHost()
    {
        TestContract<Uri?, UriContract, ArgumentOutOfRangeException>(
            new Uri("https://expected.example.com/path"),
            new Uri("https://other.example.com/path"),
            (testArgument, message) => testArgument.Must().HaveHost("expected.example.com", message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotHaveHost()
    {
        TestContract<Uri?, UriContract, ArgumentOutOfRangeException>(
            new Uri("https://other.example.com/path"),
            new Uri("https://expected.example.com/path"),
            (testArgument, message) => testArgument.Must().NotHaveHost("expected.example.com", message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_HavePort()
    {
        TestContract<Uri?, UriContract, ArgumentOutOfRangeException>(
            DummyData.GetUriWithScheme("https", 8443),
            DummyData.GetUriWithScheme("https", 9443),
            (testArgument, message) => testArgument.Must().HavePort(8443, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotHavePort()
    {
        TestContract<Uri?, UriContract, ArgumentOutOfRangeException>(
            DummyData.GetUriWithScheme("https", 9443),
            DummyData.GetUriWithScheme("https", 8443),
            (testArgument, message) => testArgument.Must().NotHavePort(8443, message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_HavePort_Uses_The_Scheme_Default_When_Unstated()
    {
        new Uri("https://example.com/")
            .Must()
            .Invoking(c => c.HavePort(443))
            .Should()
            .NotThrow("a URI without an explicit port carries the default port of its scheme");
    }

    [Fact]
    public void Test_Must_BeLoopback()
    {
        TestContract<Uri?, UriContract, ArgumentOutOfRangeException>(
            DummyData.GetLoopbackUri(),
            DummyData.GetUri(),
            (testArgument, message) => testArgument.Must().BeLoopback(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeLoopback()
    {
        TestContract<Uri?, UriContract, ArgumentOutOfRangeException>(
            DummyData.GetUri(),
            DummyData.GetLoopbackUri(),
            (testArgument, message) => testArgument.Must().NotBeLoopback(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_BeFile()
    {
        TestContract<Uri?, UriContract, ArgumentOutOfRangeException>(
            DummyData.GetFileUri(),
            DummyData.GetUri(),
            (testArgument, message) => testArgument.Must().BeFile(message),
            "testArgument");
    }

    [Fact]
    public void Test_Must_NotBeFile()
    {
        TestContract<Uri?, UriContract, ArgumentOutOfRangeException>(
            DummyData.GetUri(),
            DummyData.GetFileUri(),
            (testArgument, message) => testArgument.Must().NotBeFile(message),
            "testArgument");
    }

    /// <summary>
    /// <see cref="Uri.Scheme"/>, <see cref="Uri.Host"/>, <see cref="Uri.Port"/> and
    /// <see cref="Uri.IsLoopback"/> throw <see cref="InvalidOperationException"/> for a relative URI.
    /// Every check that reads them must fail the contract instead of letting that escape.
    /// </summary>
    public static TheoryData<string, Action<Uri>> ChecksNeedingAnAbsoluteUri =>
        new()
        {
            { "HaveScheme", u => u.Must().HaveScheme("https") },
            { "NotHaveScheme", u => u.Must().NotHaveScheme("https") },
            { "BeHttps", u => u.Must().BeHttps() },
            { "NotBeHttps", u => u.Must().NotBeHttps() },
            { "HaveHost", u => u.Must().HaveHost("example.com") },
            { "NotHaveHost", u => u.Must().NotHaveHost("example.com") },
            { "HavePort", u => u.Must().HavePort(443) },
            { "NotHavePort", u => u.Must().NotHavePort(443) },
            { "BeLoopback", u => u.Must().BeLoopback() },
            { "NotBeLoopback", u => u.Must().NotBeLoopback() },
            { "BeFile", u => u.Must().BeFile() },
            { "NotBeFile", u => u.Must().NotBeFile() }
        };

    [Theory]
    [MemberData(nameof(ChecksNeedingAnAbsoluteUri))]
    public void Relative_uri_fails_the_contract_rather_than_throwing_InvalidOperationException(
        string check,
        Action<Uri> act)
    {
        var relative = DummyData.GetRelativeUri();

        FluentActions.Invoking(() => act(relative))
            .Should()
            .Throw<ArgumentOutOfRangeException>($"\"{check}\" cannot be answered for a relative URI");
    }

    [Theory]
    [MemberData(nameof(ChecksNeedingAnAbsoluteUri))]
    public void Null_uri_is_rejected_with_ArgumentNullException(string check, Action<Uri> act)
    {
        FluentActions.Invoking(() => act(null!))
            .Should()
            .Throw<ArgumentNullException>($"\"{check}\" must reject a null argument");
    }
}
