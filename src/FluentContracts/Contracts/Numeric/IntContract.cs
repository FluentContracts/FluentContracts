namespace FluentContracts.Contracts.Numeric;

public class NullableIntContract(int? argumentValue, string argumentName)
    : NumberContract<int?, NullableIntContract>(argumentValue, argumentName)
{
    protected override int? Zero => 0;
}

public class IntContract(int argumentValue, string argumentName)
    : NumberContract<int, IntContract>(argumentValue, argumentName)
{
    protected override int Zero => 0;
}

public class NullableUintContract(uint? argumentValue, string argumentName)
    : NumberContract<uint?, NullableUintContract>(argumentValue, argumentName)
{
    protected override uint? Zero => 0;
}

public class UintContract(uint argumentValue, string argumentName) 
    : NumberContract<uint, UintContract>(argumentValue, argumentName)
{
    protected override uint Zero => 0;
}