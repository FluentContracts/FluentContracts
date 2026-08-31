using System;

namespace FluentContracts.Tests.Mocks.Data;

public static partial class DummyData
{
    /// <summary>
    /// An absolute https URI with a random host.
    /// </summary>
    public static Uri GetUri()
    {
        return new Uri($"https://{Faker.Value.Internet.DomainName()}/{Faker.Value.Lorem.Slug()}");
    }

    /// <summary>
    /// Two different absolute https URIs.
    /// </summary>
    public static Pair<Uri> GetUriPair()
    {
        var testArgument = GetUri();

        Uri differentArgument;
        do
        {
            differentArgument = GetUri();
        } while (differentArgument == testArgument);

        return new Pair<Uri>(testArgument, differentArgument);
    }

    /// <summary>
    /// A relative URI, which cannot answer questions about scheme, host or port.
    /// </summary>
    public static Uri GetRelativeUri()
    {
        return new Uri($"/{Faker.Value.Lorem.Slug()}", UriKind.Relative);
    }

    public static Uri GetUriWithScheme(string scheme, int? port = null)
    {
        var authority = port.HasValue
            ? $"{Faker.Value.Internet.DomainName()}:{port.Value}"
            : Faker.Value.Internet.DomainName();

        return new Uri($"{scheme}://{authority}/");
    }

    public static Uri GetLoopbackUri() => new($"http://localhost:{Faker.Value.Random.Int(1024, 65535)}/");

    public static Uri GetFileUri() => new($"file:///tmp/{Faker.Value.Lorem.Slug()}.txt");

    /// <summary>
    /// A <see cref="DateTimeOffset"/> with the given offset from UTC, defaulting to UTC itself.
    /// </summary>
    public static DateTimeOffset GetDateTimeOffset(TimeSpan? offset = null)
    {
        var moment = Faker.Value.Date.Between(new DateTime(2000, 1, 1), new DateTime(2030, 1, 1));

        return new DateTimeOffset(DateTime.SpecifyKind(moment, DateTimeKind.Unspecified), offset ?? TimeSpan.Zero);
    }

    public static Pair<DateTimeOffset> GetDateTimeOffsetPair()
    {
        var testArgument = GetDateTimeOffset();
        var differentArgument = testArgument.AddDays(Faker.Value.Random.Int(1, 500));

        return new Pair<DateTimeOffset>(testArgument, differentArgument);
    }
}
