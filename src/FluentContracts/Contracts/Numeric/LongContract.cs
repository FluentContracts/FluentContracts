namespace FluentContracts.Contracts.Numeric;

public class NullableLongContract(long? argumentValue, string argumentName)
    : ComparableContract<long?, NullableLongContract>(argumentValue, argumentName);

public class LongContract(long argumentValue, string argumentName)
    : ComparableContract<long, LongContract>(argumentValue, argumentName);

public class NullableUlongContract(ulong? argumentValue, string argumentName)
    : ComparableContract<ulong?, NullableUlongContract>(argumentValue, argumentName);

public class UlongContract(ulong argumentValue, string argumentName) 
    : ComparableContract<ulong, UlongContract>(argumentValue, argumentName);