namespace FluentContracts.Contracts.Numeric;

public class NullableIntContract(int? argumentValue, string argumentName)
    : ComparableContract<int?, NullableIntContract>(argumentValue, argumentName);

public class IntContract(int argumentValue, string argumentName)
    : ComparableContract<int, IntContract>(argumentValue, argumentName);

public class NullableUintContract(uint? argumentValue, string argumentName)
    : ComparableContract<uint?, NullableUintContract>(argumentValue, argumentName);

public class UintContract(uint argumentValue, string argumentName) 
    : ComparableContract<uint, UintContract>(argumentValue, argumentName);