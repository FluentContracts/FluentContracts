# Benchmarks

A snapshot of `benchmarks/FluentContracts.Benchmarks`, measuring the happy path of representative
checks against the hand-written guard each replaces. Regenerate with:

```bash
dotnet run -c Release --project benchmarks/FluentContracts.Benchmarks
```

## Snapshot

FluentContracts 3.6.1 · BenchmarkDotNet v0.15.4 · Linux Ubuntu 24.04.4 LTS ·
Intel Xeon 2.80GHz (virtualized, 4 cores) · .NET 10.0.0, X64 RyuJIT · library `net8.0` assets

> The run machine is a **virtualized container**, so treat the absolute numbers as indicative and
> the ratios and allocations as the durable signal. Numbers from a quiet physical machine are
> welcome as a replacement snapshot.

| Method                         |        Mean | Allocated |
|------------------------------- |------------:|----------:|
| HandWritten_NotNull            |   0.4909 ns |         – |
| FluentContracts_NotNull        |  90.7370 ns |     192 B |
| HandWritten_GreaterThan        |   0.0112 ns |         – |
| FluentContracts_GreaterThan    |  76.1682 ns |     160 B |
| HandWritten_NotNullOrEmpty     |   1.4588 ns |         – |
| FluentContracts_NotNullOrEmpty |  85.7218 ns |     192 B |
| HandWritten_NullableRange      |   0.6226 ns |         – |
| FluentContracts_NullableRange  |  78.2850 ns |     160 B |
| HandWritten_ListContains       |   5.1460 ns |         – |
| FluentContracts_ListContains   | 145.3106 ns |     288 B |

## Reading it

- **A fluent check costs on the order of 100 ns and one or two small allocations**; a hand-written
  guard is effectively free. For a method called thousands of times per second the difference is
  noise. For a guard inside a genuinely hot loop — millions of calls per second — the hand-written
  form is measurably cheaper, and that is the honest trade the fluent API makes for readability.
- **The allocation is the contract object plus its `Linker`** (`160 B` for a value-type argument;
  strings and lists carry a little more, and `Contain` adds its params array). Removing that
  per-chain allocation is exactly the 4.0.0 conversation the repository's issue list tracks — these
  numbers are its baseline.
- `NullableRange` chains two checks (`NotBeNull().And.BeBetween(...)`) and still allocates once:
  the chain reuses one contract and one linker, so adding checks to an existing chain is close to
  free — the cost is starting the chain, not extending it.
- Failure paths are deliberately not measured. A throwing guard is not a hot path, and exception
  construction dwarfs everything above.
