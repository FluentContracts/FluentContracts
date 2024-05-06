namespace FluentContracts.Contracts.Numeric;

public class NullableDoubleContract(double? argumentValue, string argumentName)
    : ComparableContract<double?, NullableDoubleContract>(argumentValue, argumentName);

public class DoubleContract(double argumentValue, string argumentName) 
    : ComparableContract<double, DoubleContract>(argumentValue, argumentName);