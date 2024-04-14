using System;
using Bogus;

namespace FluentContracts.Tests.Utils;

public static class DummyData
{
    private static Lazy<Faker> Faker { get; } = new Lazy<Faker>(() => new Faker());
    public static (string Message, string ExceptionMessage) GetRandomErrorMessage(string parameterName)
    {
        
        var message = Faker.Value.Lorem.Sentence(10);
        var exceptionMessage = $"{message} (Parameter '{parameterName}')";
        return (message, exceptionMessage);
    }

    public static string GetRandomString()
    {
        return Faker.Value.Random.String(5, 10);
    }
}