namespace FluentContracts.Contracts.Numeric;

public class NullableDoubleContract(double? argumentValue, string argumentName)
    : NumberContract<double?, NullableDoubleContract>(argumentValue, argumentName)
{
    protected override double? Zero => 0;
}

public class DoubleContract(double argumentValue, string argumentName) 
    : NumberContract<double, DoubleContract>(argumentValue, argumentName)
{
    protected override double Zero => 0;
}