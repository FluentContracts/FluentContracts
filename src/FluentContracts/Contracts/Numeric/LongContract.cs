namespace FluentContracts.Contracts.Numeric;

public class NullableLongContract(long? argumentValue, string argumentName)
    : NumberContract<long?, NullableLongContract>(argumentValue, argumentName)
{
    protected override long? Zero => 0;
}

public class LongContract(long argumentValue, string argumentName)
    : NumberContract<long, LongContract>(argumentValue, argumentName)
{
    protected override long Zero => 0;
}

public class NullableUlongContract(ulong? argumentValue, string argumentName)
    : NumberContract<ulong?, NullableUlongContract>(argumentValue, argumentName)
{
    protected override ulong? Zero => 0;
}

public class UlongContract(ulong argumentValue, string argumentName) 
    : NumberContract<ulong, UlongContract>(argumentValue, argumentName)
{
    protected override ulong Zero => 0;
}