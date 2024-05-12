using System;

namespace FluentContracts.Tests.Mocks.Data;

public static partial class DummyData
{
    #region Int

    public static int GetInt(NumberOption option = NumberOption.Normal)
    {
        return option switch
        {
            NumberOption.Normal => Faker.Value.Random.Int(-1_000_000, 1_000_000),
            NumberOption.Negative => Faker.Value.Random.Int(-1_000_000, -1),
            NumberOption.Positive => Faker.Value.Random.Int(1, 1_000_000),
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
        };
    }

    public static Pair<int> GetIntPair()
    {
        const int middle = int.MaxValue / 2;
        const int nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.Int(int.MinValue, middle);
        var differentArgument = Faker.Value.Random.Int(nextToMiddle, int.MaxValue);

        return new Pair<int>(testArgument, differentArgument);
    }

    public static uint GetUint()
    {
        return Faker.Value.Random.UInt(500_000U, 1_000_000U);
    }

    public static Pair<uint> GetUintPair()
    {
        const uint middle = uint.MaxValue / 2;
        const uint nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.UInt(uint.MinValue, middle);
        var differentArgument = Faker.Value.Random.UInt(nextToMiddle, uint.MaxValue);

        return new Pair<uint>(testArgument, differentArgument);
    }

    #endregion

    #region Decimal

    public static decimal GetDecimal(NumberOption option = NumberOption.Normal)
    {
        return option switch
        {
            NumberOption.Normal => Faker.Value.Random.Decimal(-1_000_000M, 1_000_000M),
            NumberOption.Negative => Faker.Value.Random.Decimal(-1_000_000M, -1M),
            NumberOption.Positive => Faker.Value.Random.Decimal(1, 1_000_000M),
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
        };
    }

    public static Pair<decimal> GetDecimalPair()
    {
        const decimal middle = 0m / 2;
        const decimal nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.Decimal(0m, middle);
        var differentArgument = Faker.Value.Random.Decimal(nextToMiddle, 1m);

        return new Pair<decimal>(testArgument, differentArgument);
    }

    #endregion

    #region Double

    public static double GetDouble(NumberOption option = NumberOption.Normal)
    {
        return option switch
        {
            NumberOption.Normal => Faker.Value.Random.Double(-1_000_000D, 1_000_000D),
            NumberOption.Negative => Faker.Value.Random.Double(-1_000_000D, -1D),
            NumberOption.Positive => Faker.Value.Random.Double(1D, 1_000_000D),
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
        };
    }

    public static Pair<double> GetDoublePair()
    {
        const double middle = double.MaxValue / 2;
        const double nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.Double(double.MinValue, middle);
        var differentArgument = Faker.Value.Random.Double(nextToMiddle, double.MaxValue);

        return new Pair<double>(testArgument, differentArgument);
    }

    #endregion

    #region Long

    public static long GetLong(NumberOption option = NumberOption.Normal)
    {
        return option switch
        {
            NumberOption.Normal => Faker.Value.Random.Long(-1_000_000L, 1_000_000L),
            NumberOption.Negative => Faker.Value.Random.Long(-1_000_000L, -1L),
            NumberOption.Positive => Faker.Value.Random.Long(1L, 1_000_000L),
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
        };
    }

    public static Pair<long> GetLongPair()
    {
        const long middle = long.MaxValue / 2;
        const long nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.Long(long.MinValue, middle);
        var differentArgument = Faker.Value.Random.Long(nextToMiddle, long.MaxValue);

        return new Pair<long>(testArgument, differentArgument);
    }

    public static ulong GetUlong()
    {
        return Faker.Value.Random.ULong(1_000_000UL, 10_000_000UL);
    }

    public static Pair<ulong> GetUlongPair()
    {
        const ulong middle = ulong.MaxValue / 2;
        const ulong nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.ULong(ulong.MinValue, middle);
        var differentArgument = Faker.Value.Random.ULong(nextToMiddle, ulong.MaxValue);

        return new Pair<ulong>(testArgument, differentArgument);
    }

    #endregion

    #region Float

    public static float GetFloat(NumberOption option = NumberOption.Normal)
    {
        return option switch
        {
            NumberOption.Normal => Faker.Value.Random.Float(-1_000_000F, 1_000_000F),
            NumberOption.Negative => Faker.Value.Random.Float(-1_000_000F, -1F),
            NumberOption.Positive => Faker.Value.Random.Float(1F, 1_000_000F),
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
        };
    }

    public static Pair<float> GetFloatPair()
    {
        const float middle = float.MaxValue / 2;
        const float nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.Float(float.MinValue, middle);
        var differentArgument = Faker.Value.Random.Float(nextToMiddle, float.MaxValue);

        return new Pair<float>(testArgument, differentArgument);
    }

    #endregion

    #region Short

    public static short GetShort(NumberOption option = NumberOption.Normal)
    {
        return option switch
        {
            NumberOption.Normal => Faker.Value.Random.Short(-10_000, 10_000),
            NumberOption.Negative => Faker.Value.Random.Short(-10_000, -1),
            NumberOption.Positive => Faker.Value.Random.Short(1, 10_000),
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
        };
    }

    public static Pair<short> GetShortPair()
    {
        const short middle = short.MaxValue / 2;
        const short nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.Short(short.MinValue, middle);
        var differentArgument = Faker.Value.Random.Short(nextToMiddle, short.MaxValue);

        return new Pair<short>(testArgument, differentArgument);
    }

    public static ushort GetUshort()
    {
        return Faker.Value.Random.UShort(1_000, 40_000);
    }

    public static Pair<ushort> GetUshortPair()
    {
        const ushort middle = ushort.MaxValue / 2;
        const ushort nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.UShort(ushort.MinValue, middle);
        var differentArgument = Faker.Value.Random.UShort(nextToMiddle, ushort.MaxValue);

        return new Pair<ushort>(testArgument, differentArgument);
    }

    #endregion

    #region Byte

    public static byte GetByte()
    {
        return Faker.Value.Random.Byte(50, 100);
    }

    public static Pair<byte> GetBytePair()
    {
        const byte middle = byte.MaxValue / 2;
        const byte nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.Byte(byte.MinValue, middle);
        var differentArgument = Faker.Value.Random.Byte(nextToMiddle, byte.MaxValue);

        return new Pair<byte>(testArgument, differentArgument);
    }

    public static sbyte GetSbyte(NumberOption option = NumberOption.Normal)
    {
        return option switch
        {
            NumberOption.Normal => Faker.Value.Random.SByte(-80, 80),
            NumberOption.Negative => Faker.Value.Random.SByte(-80, -1),
            NumberOption.Positive => Faker.Value.Random.SByte(1, 80),
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
        };
    }

    public static Pair<sbyte> GetSbytePair()
    {
        const sbyte middle = sbyte.MaxValue / 2;
        const sbyte nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.SByte(sbyte.MinValue, middle);
        var differentArgument = Faker.Value.Random.SByte(nextToMiddle, sbyte.MaxValue);

        return new Pair<sbyte>(testArgument, differentArgument);
    }

    #endregion
}