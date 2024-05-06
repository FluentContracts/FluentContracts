namespace FluentContracts.Contracts.Numeric;

public class NullableShortContract(short? argumentValue, string argumentName)
    : ComparableContract<short?, NullableShortContract>(argumentValue, argumentName);

public class ShortContract(short argumentValue, string argumentName)
    : ComparableContract<short, ShortContract>(argumentValue, argumentName);

public class NullableUshortContract(ushort? argumentValue, string argumentName)
    : ComparableContract<ushort?, NullableUshortContract>(argumentValue, argumentName);

public class UshortContract(ushort argumentValue, string argumentName) 
    : ComparableContract<ushort, UshortContract>(argumentValue, argumentName);