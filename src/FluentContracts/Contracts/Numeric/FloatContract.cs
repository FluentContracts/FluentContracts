namespace FluentContracts.Contracts.Numeric;

public class NullableFloatContract(float? argumentValue, string argumentName)
    : ComparableContract<float?, NullableFloatContract>(argumentValue, argumentName);

public class FloatContract(float argumentValue, string argumentName) 
    : ComparableContract<float, FloatContract>(argumentValue, argumentName);