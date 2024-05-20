namespace FluentContracts.Contracts.Numeric;

public class NullableByteContract(byte? argumentValue, string argumentName)
    : NumberContract<byte?, NullableByteContract>(argumentValue, argumentName)
{
    protected override byte? Zero => 0;
}

public class ByteContract(byte argumentValue, string argumentName)
    : NumberContract<byte, ByteContract>(argumentValue, argumentName)
{
    protected override byte Zero => 0;
}

public class NullableSbyteContract(sbyte? argumentValue, string argumentName)
    : SignedNumberContract<sbyte?, NullableSbyteContract>(argumentValue, argumentName)
{
    protected override sbyte? Zero => 0;
}

public class SbyteContract(sbyte argumentValue, string argumentName) 
    : SignedNumberContract<sbyte, SbyteContract>(argumentValue, argumentName)
{
    protected override sbyte Zero => 0;
}