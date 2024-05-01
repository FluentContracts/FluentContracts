namespace FluentContracts.Contracts.Numeric;

public class NullableByteContract(byte? argumentValue, string argumentName)
    : ComparableContract<byte?, NullableByteContract>(argumentValue, argumentName);

public class ByteContract(byte argumentValue, string argumentName)
    : ComparableContract<byte, ByteContract>(argumentValue, argumentName);

public class NullableSbyteContract(sbyte? argumentValue, string argumentName)
    : ComparableContract<sbyte?, NullableSbyteContract>(argumentValue, argumentName);

public class SbyteContract(sbyte argumentValue, string argumentName) 
    : ComparableContract<sbyte, SbyteContract>(argumentValue, argumentName);