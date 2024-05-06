namespace FluentContracts.Contracts.Numeric;

public class NullableDecimalContract(decimal? argumentValue, string argumentName)
    : ComparableContract<decimal?, NullableDecimalContract>(argumentValue, argumentName);

public class DecimalContract(decimal argumentValue, string argumentName) 
    : ComparableContract<decimal, DecimalContract>(argumentValue, argumentName);