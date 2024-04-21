using System;
using FluentAssertions;
using FluentContracts.Infrastructure;
using FluentContracts.Tests.Utils;

namespace FluentContracts.Tests;

public abstract class Tests
{
    protected static void TestContract<T, TException>(
        T successfulArgument,
        T failingArgument,
        Func<T, string, Linker<T>> contractAction,
        string argumentName)
        where TException : ArgumentException
    {   
        var satisfied =
            () => contractAction(successfulArgument, null);

        satisfied.Should().NotThrow();
        
        var notSatisfied =
            () => contractAction(failingArgument, null);

        notSatisfied
            .Should()
            .Throw<TException>()
            .WithParameterName(argumentName);
        
        var expectedError = DummyData.GetRandomErrorMessage(argumentName);

        var notSatisfiedWithMessage = 
            () => contractAction(failingArgument, expectedError.Message);

        notSatisfiedWithMessage
            .Should()
            .Throw<TException>()
            .WithParameterName(argumentName)
            .WithMessage(expectedError.ExceptionMessage);
    }
}