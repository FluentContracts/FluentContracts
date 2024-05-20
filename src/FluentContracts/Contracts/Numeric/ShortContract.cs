namespace FluentContracts.Contracts.Numeric;

public class NullableShortContract(short? argumentValue, string argumentName)
    : SignedNumberContract<short?, NullableShortContract>(argumentValue, argumentName)
{
    protected override short? Zero => 0;
}

public class ShortContract(short argumentValue, string argumentName)
    : SignedNumberContract<short, ShortContract>(argumentValue, argumentName)
{
    protected override short Zero => 0;
}

public class NullableUshortContract(ushort? argumentValue, string argumentName)
    : NumberContract<ushort?, NullableUshortContract>(argumentValue, argumentName)
{
    protected override ushort? Zero => 0;
}

public class UshortContract(ushort argumentValue, string argumentName) 
    : NumberContract<ushort, UshortContract>(argumentValue, argumentName)
{
    protected override ushort Zero => 0;
}