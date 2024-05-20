namespace FluentContracts.Tests.Mocks.Data;

public readonly struct Pair<T>(T testArgument, T differentArgument)
{
    public T TestArgument { get; init; } = testArgument;
    public T DifferentArgument { get; init; } = differentArgument;
}