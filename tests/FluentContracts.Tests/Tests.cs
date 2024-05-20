using System;
using FluentAssertions;
using FluentContracts.Contracts;
using FluentContracts.Infrastructure;
using FluentContracts.Tests.Mocks.Data;

namespace FluentContracts.Tests;

public abstract class Tests
{
    protected static void TestContract<T, TContract, TException>(
        T successfulArgument,
        T failingArgument,
        Func<T, string, Linker<TContract>> contractAction,
        string argumentName,
        bool skipMessageThrow = false)
        where TException : ArgumentException
    {   
        var satisfied =
            () => contractAction(successfulArgument, null);

        satisfied
            .Should()
            .NotThrow($"Success argument \"{successfulArgument}\" must not throw");
        
        var notSatisfied =
            () => contractAction(failingArgument, null);

        notSatisfied
            .Should()
            .Throw<TException>($"Failing argument \"{failingArgument}\" must throw \"{nameof(TException)}\"")
            .WithParameterName(argumentName);

        if (skipMessageThrow) return;
        
        var expectedError = DummyData.GetRandomErrorMessage(argumentName);

        var notSatisfiedWithMessage =
            () => contractAction(failingArgument, expectedError.Message);

        notSatisfiedWithMessage
            .Should()
            .Throw<TException>($"Failing argument \"{failingArgument}\" must throw \"{nameof(TException)}\"")
            .WithParameterName(argumentName)
            .WithMessage(expectedError.ExceptionMessage);
    }
    
    protected static void TestContract<T, TContract, TException>(
        T successfulArgument,
        T failingArgument,
        string errorMessage,
        Func<T, string, Linker<TContract>> contractAction)
        where TException : Exception
    {   
        var satisfied =
            () => contractAction(successfulArgument, null);

        satisfied
            .Should()
            .NotThrow($"Success argument \"{successfulArgument}\" must not throw");
        
        var notSatisfied =
            () => contractAction(failingArgument, null);

        notSatisfied
            .Should()
            .Throw<TException>($"Failing argument \"{failingArgument}\" must throw \"{nameof(TException)}\"");

        var notSatisfiedWithMessage = 
            () => contractAction(failingArgument, errorMessage);

        if (errorMessage != null)
        {
            notSatisfiedWithMessage
                .Should()
                .Throw<TException>($"Failing argument \"{failingArgument}\" must throw \"{nameof(TException)}\"")
                .WithMessage(errorMessage);
        }
        else
        {
            notSatisfiedWithMessage
                .Should()
                .Throw<TException>($"Failing argument \"{failingArgument}\" must throw \"{nameof(TException)}\"");
        }
    }
}