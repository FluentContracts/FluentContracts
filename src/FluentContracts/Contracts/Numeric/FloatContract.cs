namespace FluentContracts.Contracts.Numeric;

public class NullableFloatContract(float? argumentValue, string argumentName)
    : NumberContract<float?, NullableFloatContract>(argumentValue, argumentName)
{
    protected override float? Zero => 0;
}

public class FloatContract(float argumentValue, string argumentName) 
    : NumberContract<float, FloatContract>(argumentValue, argumentName)
{
    protected override float Zero => 0;
}