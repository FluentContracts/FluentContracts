namespace FluentContracts.Contracts.Numeric;

public class NullableDecimalContract(decimal? argumentValue, string argumentName)
    : NumberContract<decimal?, NullableDecimalContract>(argumentValue, argumentName)
{
    protected override decimal? Zero => 0;
}

public class DecimalContract(decimal argumentValue, string argumentName) 
    : NumberContract<decimal, DecimalContract>(argumentValue, argumentName)
{
    protected override decimal Zero => 0;
}