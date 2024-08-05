using System;

namespace FluentContracts.Tests.Mocks.Data;

public static partial class DummyData
{
    #region Int

    public static int? GetNullableInt(
        NumberOption option = NumberOption.Normal,
        int minValue = -1_000_000,
        int maxValue = 1_000_000) => GetInt(option, minValue, maxValue);

    public static int GetInt(
        NumberOption option = NumberOption.Normal,
        int minValue = -1_000_000,
        int maxValue = 1_000_000)
    {
        return option switch
        {
            NumberOption.Normal => Faker.Value.Random.Int(minValue, maxValue),
            NumberOption.Negative => Faker.Value.Random.Int(minValue, -1),
            NumberOption.Positive => Faker.Value.Random.Int(1, maxValue),
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
        };
    }

    public static Pair<int?> GetNullableIntPair(
        int minValue = -1_000_000,
        int maxValue = 1_000_000)
    {
        var pair = GetIntPair(minValue, maxValue);

        return new Pair<int?>(pair.TestArgument, pair.DifferentArgument);
    }

    public static Pair<int> GetIntPair(
        int minValue = -1_000_000,
        int maxValue = 1_000_000)
    {
        int middle = (maxValue - minValue) / 2 + minValue;
        int nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.Int(minValue, middle);
        var differentArgument = Faker.Value.Random.Int(nextToMiddle, maxValue);

        return new Pair<int>(testArgument, differentArgument);
    }

    public static uint? GetNullableUint() => GetUint();
    
    public static uint GetUint()
    {
        return Faker.Value.Random.UInt(500_000U, 1_000_000U);
    }
    
    public static Pair<uint?> GetNullableUintPair()
    {
        var pair = GetUintPair();

        return new Pair<uint?>(pair.TestArgument, pair.DifferentArgument);
    }

    public static Pair<uint> GetUintPair()
    {
        const uint middle = uint.MaxValue / 2;
        const uint nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.UInt(uint.MinValue, middle);
        var differentArgument = Faker.Value.Random.UInt(nextToMiddle);

        return new Pair<uint>(testArgument, differentArgument);
    }

    #endregion

    #region Decimal

    public static decimal? GetNullableDecimal(NumberOption option = NumberOption.Normal) =>
        GetDecimal(option);

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

    public static Pair<decimal?> GetNullableDecimalPair()
    {
        var pair = GetDecimalPair();

        return new Pair<decimal?>(pair.TestArgument, pair.DifferentArgument);
    }

    public static Pair<decimal> GetDecimalPair()
    {
        const decimal middle = 0m / 2;
        const decimal nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.Decimal(0m, middle);
        var differentArgument = Faker.Value.Random.Decimal(nextToMiddle);

        return new Pair<decimal>(testArgument, differentArgument);
    }

    #endregion

    #region Double

    public static double? GetNullableDouble(NumberOption option = NumberOption.Normal) =>
        GetDouble(option);

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

    public static Pair<double?> GetNullableDoublePair()
    {
        var pair = GetDoublePair();

        return new Pair<double?>(pair.TestArgument, pair.DifferentArgument);
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

    public static long? GetNullableLong(NumberOption option = NumberOption.Normal) =>
        GetLong(option);
    
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

    public static Pair<long?> GetNullableLongPair()
    {
        var pair = GetLongPair();

        return new Pair<long?>(pair.TestArgument, pair.DifferentArgument);
    }

    public static Pair<long> GetLongPair()
    {
        const long middle = long.MaxValue / 2;
        const long nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.Long(long.MinValue, middle);
        var differentArgument = Faker.Value.Random.Long(nextToMiddle);

        return new Pair<long>(testArgument, differentArgument);
    }

    public static ulong? GetNullableUlong() => GetUlong();

    public static ulong GetUlong()
    {
        return Faker.Value.Random.ULong(1_000_000UL, 10_000_000UL);
    }
    
    public static Pair<ulong?> GetNullableUlongPair()
    {
        var pair = GetUlongPair();

        return new Pair<ulong?>(pair.TestArgument, pair.DifferentArgument);
    }

    public static Pair<ulong> GetUlongPair()
    {
        const ulong middle = ulong.MaxValue / 2;
        const ulong nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.ULong(ulong.MinValue, middle);
        var differentArgument = Faker.Value.Random.ULong(nextToMiddle);

        return new Pair<ulong>(testArgument, differentArgument);
    }

    #endregion

    #region Float

    public static float? GetNullableFloat(NumberOption option = NumberOption.Normal) =>
        GetFloat(option);

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

    public static Pair<float?> GetNullableFloatPair()
    {
        var pair = GetFloatPair();

        return new Pair<float?>(pair.TestArgument, pair.DifferentArgument);
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

    public static short? GetNullableShort(NumberOption option = NumberOption.Normal) =>
        GetShort(option);
    
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

    public static Pair<short?> GetNullableShortPair()
    {
        var pair = GetShortPair();

        return new Pair<short?>(pair.TestArgument, pair.DifferentArgument);
    }

    public static Pair<short> GetShortPair()
    {
        const short middle = short.MaxValue / 2;
        const short nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.Short(short.MinValue, middle);
        var differentArgument = Faker.Value.Random.Short(nextToMiddle);

        return new Pair<short>(testArgument, differentArgument);
    }

    public static ushort? GetNullableUshort() => GetUshort();
    
    public static ushort GetUshort()
    {
        return Faker.Value.Random.UShort(1_000, 40_000);
    }
    
    public static Pair<ushort?> GetNullableUshortPair()
    {
        var pair = GetUshortPair();

        return new Pair<ushort?>(pair.TestArgument, pair.DifferentArgument);
    }

    public static Pair<ushort> GetUshortPair()
    {
        const ushort middle = ushort.MaxValue / 2;
        const ushort nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.UShort(ushort.MinValue, middle);
        var differentArgument = Faker.Value.Random.UShort(nextToMiddle);

        return new Pair<ushort>(testArgument, differentArgument);
    }

    #endregion

    #region Byte

    public static byte? GetNullableByte() => GetByte();
    
    public static byte GetByte()
    {
        return Faker.Value.Random.Byte(50, 100);
    }

    public static Pair<byte?> GetNullableBytePair()
    {
        var pair = GetBytePair();

        return new Pair<byte?>(pair.TestArgument, pair.DifferentArgument);
    }

    public static Pair<byte> GetBytePair()
    {
        const byte middle = byte.MaxValue / 2;
        const byte nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.Byte(byte.MinValue, middle);
        var differentArgument = Faker.Value.Random.Byte(nextToMiddle);

        return new Pair<byte>(testArgument, differentArgument);
    }

    public static sbyte? GetNullableSbyte(NumberOption option = NumberOption.Normal) => GetSbyte(option);
    
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

    public static Pair<sbyte?> GetNullableSbytePair()
    {
        var pair = GetSbytePair();

        return new Pair<sbyte?>(pair.TestArgument, pair.DifferentArgument);
    }
    
    public static Pair<sbyte> GetSbytePair()
    {
        const sbyte middle = sbyte.MaxValue / 2;
        const sbyte nextToMiddle = middle + 1;

        var testArgument = Faker.Value.Random.SByte(sbyte.MinValue, middle);
        var differentArgument = Faker.Value.Random.SByte(nextToMiddle);

        return new Pair<sbyte>(testArgument, differentArgument);
    }

    #endregion
}