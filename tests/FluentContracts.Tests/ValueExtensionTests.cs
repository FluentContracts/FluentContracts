using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using FluentContracts.Tests.Mocks;
using FluentContracts.Tests.Mocks.Data;
using FluentContracts.Tests.TestAttributes;
using Xunit;

namespace FluentContracts.Tests;

/// <summary>
/// <c>Value()</c> ends a chain with the value it just validated: unwrapped for nullable-wrapped
/// arguments, the same instance for references, and failing a null argument with
/// <see cref="ArgumentNullException"/> naming the argument — the same contract <c>NotBeNull</c>
/// enforces, so ending a chain with <c>Value()</c> is itself a null check.
/// </summary>
[ContractTest("Value")]
public class ValueExtensionTests : Tests
{
    [Fact]
    public void Value_unwraps_a_nullable_value_type()
    {
        int? port = 8080;

        int value = port.Must().BeBetween(1, 65535).Value();

        value.Should().Be(8080);
    }

    [Fact]
    public void Value_returns_the_reference_it_validated()
    {
        var name = "totollygeek";

        name.Must().NotBeNullOrEmpty().Value().Should().BeSameAs(name);
    }

    [Fact]
    public void Value_on_a_null_argument_fails_naming_the_argument()
    {
        int? missing = null;

        FluentActions
            .Invoking(() => missing.Must().BeNull().Value())
            .Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(missing));
    }

    [Fact]
    public void Value_returns_the_validated_list_instance()
    {
        IList<int> pages = [1, 2, 3];

        pages.Must().BeInAscendingOrder().Value().Should().BeSameAs(pages);
    }

    [Fact]
    public void Value_returns_the_validated_enum()
    {
        var day = DayOfWeek.Friday;

        day.Must().BeDefined().Value().Should().Be(DayOfWeek.Friday);
    }

    [Fact]
    public void Value_returns_the_validated_guid()
    {
        var id = Guid.NewGuid();

        Guid value = id.Must().NotBeEmpty().Value();

        value.Should().Be(id);
    }

    /// <summary>
    /// One <c>Value()</c> per contract exists so the caller gets the unwrapped type back rather than
    /// <c>Nullable&lt;T&gt;</c>, and each one is a separate method that can be forgotten or wired to the
    /// wrong member. These walk every overload the library ships, so a new contract that ships without
    /// its <c>Value()</c> — or with one returning the wrong thing — shows up here.
    /// </summary>
    [Fact]
    public void Value_unwraps_every_nullable_value_type_contract()
    {
        bool? flag = true;
        byte? small = 7;
        char? initial = 'k';
        DateTime? stamp = new DateTime(2026, 6, 1, 12, 0, 0, DateTimeKind.Utc);
        DateTimeOffset? offsetStamp = new DateTimeOffset(stamp.Value, TimeSpan.Zero);
        decimal? price = 19.99m;
        double? ratio = 0.5d;
        float? weight = 1.5f;
        long? ticks = 9_000_000_000L;
        sbyte? delta = -3;
        short? year = 2026;
        TimeSpan? timeout = TimeSpan.FromSeconds(30);
        uint? count = 42u;
        ulong? size = 4_000_000_000UL;
        ushort? port = 8080;

        flag.Must().BeTrue().Value().Should().BeTrue();
        small.Must().NotBeNull().Value().Should().Be((byte)7);
        initial.Must().NotBeNull().Value().Should().Be('k');
        stamp.Must().NotBeNull().Value().Should().Be(stamp.Value);
        offsetStamp.Must().NotBeNull().Value().Should().Be(offsetStamp.Value);
        price.Must().NotBeNull().Value().Should().Be(19.99m);
        ratio.Must().NotBeNull().Value().Should().Be(0.5d);
        weight.Must().NotBeNull().Value().Should().Be(1.5f);
        ticks.Must().NotBeNull().Value().Should().Be(9_000_000_000L);
        delta.Must().NotBeNull().Value().Should().Be((sbyte)-3);
        year.Must().NotBeNull().Value().Should().Be((short)2026);
        timeout.Must().NotBeNull().Value().Should().Be(TimeSpan.FromSeconds(30));
        count.Must().NotBeNull().Value().Should().Be(42u);
        size.Must().NotBeNull().Value().Should().Be(4_000_000_000UL);
        port.Must().NotBeNull().Value().Should().Be((ushort)8080);
    }

    [Fact]
    public void Value_returns_the_same_instance_for_every_reference_contract()
    {
        var uri = new Uri("https://fluentcontracts.github.io");
        Stream stream = new MockStream();
        var file = DummyData.GetFileInfo(this);
        var directory = DummyData.GetDirectoryInfo(this);
        IDictionary<string, string> headers = new Dictionary<string, string> { ["accept"] = "text/plain" };
        object payload = new();

        uri.Must().NotBeNull().Value().Should().BeSameAs(uri);
        stream.Must().NotBeNull().Value().Should().BeSameAs(stream);
        file.Must().NotBeNull().Value().Should().BeSameAs(file);
        directory.Must().NotBeNull().Value().Should().BeSameAs(directory);
        headers.Must().NotBeNull().Value().Should().BeSameAs(headers);
        payload.Must().NotBeNull().Value().Should().BeSameAs(payload);
    }

    [Fact]
    public void Value_on_a_null_reference_argument_fails_naming_the_argument()
    {
        Uri? missing = null;

        FluentActions
            .Invoking(() => missing.Must().BeNull().Value())
            .Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(missing));
    }
}
