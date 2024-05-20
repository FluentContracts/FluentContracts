namespace FluentContracts.Contracts.Numeric;

public class NullableFloatContract(float? argumentValue, string argumentName)
    : SignedNumberContract<float?, NullableFloatContract>(argumentValue, argumentName)
{
    protected override float? Zero => 0;
}

public class FloatContract(float argumentValue, string argumentName) 
    : SignedNumberContract<float, FloatContract>(argumentValue, argumentName)
{
    protected override float Zero => 0;
}