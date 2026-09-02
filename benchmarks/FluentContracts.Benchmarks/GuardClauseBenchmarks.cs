using BenchmarkDotNet.Attributes;

namespace FluentContracts.Benchmarks;

/// <summary>
/// The happy path of representative checks against the hand-written guard each replaces — the
/// per-check overhead a caller pays for the fluent form, and what it allocates (the contract and
/// its <c>Linker</c> are the known costs). Failure paths are deliberately not measured: a throwing
/// guard is not a hot path.
/// </summary>
[MemoryDiagnoser]
public class GuardClauseBenchmarks
{
    private readonly string _name = "totollygeek";
    private readonly int _quantity = 42;
    private readonly int? _port = 8080;
    private readonly List<int> _pages = [1, 2, 3, 4, 5, 6, 7, 8];

    [Benchmark(Baseline = true)]
    public string HandWritten_NotNull()
    {
        if (_name is null) throw new ArgumentNullException(nameof(_name));
        return _name;
    }

    [Benchmark]
    public string FluentContracts_NotNull()
    {
        return _name.Must().NotBeNull().Value();
    }

    [Benchmark]
    public int HandWritten_GreaterThan()
    {
        if (_quantity <= 5) throw new ArgumentOutOfRangeException(nameof(_quantity));
        return _quantity;
    }

    [Benchmark]
    public int FluentContracts_GreaterThan()
    {
        _quantity.Must().BeGreaterThan(5);
        return _quantity;
    }

    [Benchmark]
    public string HandWritten_NotNullOrEmpty()
    {
        if (string.IsNullOrEmpty(_name)) throw new ArgumentException("empty", nameof(_name));
        return _name;
    }

    [Benchmark]
    public string FluentContracts_NotNullOrEmpty()
    {
        return _name.Must().NotBeNullOrEmpty().Value();
    }

    [Benchmark]
    public int HandWritten_NullableRange()
    {
        if (_port is null) throw new ArgumentNullException(nameof(_port));
        if (_port.Value is < 1 or > 65535) throw new ArgumentOutOfRangeException(nameof(_port));
        return _port.Value;
    }

    [Benchmark]
    public int FluentContracts_NullableRange()
    {
        return _port.Must().NotBeNull().And.BeBetween(1, 65535).Value();
    }

    [Benchmark]
    public List<int> HandWritten_ListContains()
    {
        if (!_pages.Contains(5)) throw new ArgumentOutOfRangeException(nameof(_pages));
        return _pages;
    }

    [Benchmark]
    public List<int> FluentContracts_ListContains()
    {
        _pages.Must().Contain(5);
        return _pages;
    }
}
