# Benchmarks

A snapshot of `benchmarks/FluentContracts.Benchmarks`, measuring the happy path of representative
checks against the hand-written guard each replaces. Regenerate with:

```bash
dotnet run -c Release --project benchmarks/FluentContracts.Benchmarks
```

All runs below: BenchmarkDotNet v0.15.4 · Linux Ubuntu 24.04.4 LTS · Intel Xeon 2.80GHz
(virtualized, 4 cores) · .NET 10.0.0, X64 RyuJIT · library `net8.0` assets.

> The run machine is a **virtualized container**, so treat the absolute numbers as indicative and
> the ratios and allocations as the durable signal. Numbers from a quiet physical machine are
> welcome as a replacement snapshot.

## 4.0.0 — checks return the contract, the `Linker` is gone

| Method                         |       Mean | Allocated |
|------------------------------- |-----------:|----------:|
| HandWritten_NotNull            |  0.5457 ns |         – |
| FluentContracts_NotNull        |  0.5481 ns |         – |
| HandWritten_GreaterThan        |  0.0005 ns |         – |
| FluentContracts_GreaterThan    |  1.3010 ns |         – |
| HandWritten_NotNullOrEmpty     |  1.4467 ns |         – |
| FluentContracts_NotNullOrEmpty |  2.9869 ns |         – |
| HandWritten_NullableRange      |  0.5028 ns |         – |
| FluentContracts_NullableRange  |  4.9088 ns |         – |
| HandWritten_ListContains       |  5.2082 ns |         – |
| FluentContracts_ListContains   | 39.8739 ns |      64 B |

**Zero allocation is the .NET 10 JIT, not magic.** With the `Linker` gone nothing stores the
contract anywhere, so it no longer escapes, and .NET 10's escape analysis stack-allocates it. That
was proven rather than assumed: the same run with object stack allocation switched off
(`DOTNET_JitObjectStackAllocation=0`, short job) shows exactly the one remaining object —

| Method                         |       Mean | Allocated |
|------------------------------- |-----------:|----------:|
| FluentContracts_NotNull        | 13.5827 ns |      32 B |
| FluentContracts_GreaterThan    | 18.4585 ns |      32 B |
| FluentContracts_NotNullOrEmpty | 13.7950 ns |      32 B |
| FluentContracts_NullableRange  | 22.1561 ns |      32 B |
| FluentContracts_ListContains   | 48.5359 ns |      96 B |

— and that second table is the figure to expect on runtimes without object stack allocation,
such as .NET 8, which this container cannot run directly: **one 32-byte contract per chain,
on the order of 15 ns**. `ListContains` carries the `params` array on top; that path is replaced
by the 4.0.0 overload triad.

## 4.0.0 — `Satisfy` with a predicate and with a specification

The same rule, `quantity > 5`, as the `Func` overload and as an `ISpecification<int?>` built once
(`--job short`, same machine and runtime as above):

| Method                                |       Mean | Allocated |
|-------------------------------------- |-----------:|----------:|
| HandWritten_Satisfy                   |  0.1197 ns |         – |
| FluentContracts_Satisfy_Func          | 18.3117 ns |      40 B |
| FluentContracts_Satisfy_Specification |  2.2613 ns |         – |

With object stack allocation switched off:

| Method                                |       Mean | Allocated |
|-------------------------------------- |-----------:|----------:|
| FluentContracts_Satisfy_Func          | 17.7613 ns |      40 B |
| FluentContracts_Satisfy_Specification | 17.7642 ns |      40 B |

**A specification costs nothing over a predicate.** The validator calls `IsSatisfiedBy` directly
rather than through a delegate, so the only object on either path is the contract — 40 B for an
`int` contract, the 32 B above plus the chain message added in 4.0.0. On .NET 10 the specification
path stack-allocates even that; the predicate path keeps the contract on the heap in this run,
which is the JIT declining to inline the delegate-taking overload, not anything the library does,
and both paths are the same one object on runtimes without object stack allocation.

Two allocations that used to sit on both paths are gone along the way: the generic `is T` pattern in
the type-convert step boxed a `Nullable<T>` argument (24 B on every `Satisfy` against a value-type
contract) and now short-circuits on a same-type conversion, and the specification's method group no
longer becomes a delegate (64 B).

## 3.6.1 — the baseline this replaced

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

Every level of the contract hierarchy allocated its own `Linker` — six objects to start an `int`
chain — which is where the 160 B came from.

## Reading it

- **On any runtime, a chain start is now one 32-byte object and ~15 ns instead of six objects,
  160 B and ~100 ns.** On .NET 10 it is free: no allocation, low single-digit nanoseconds, within
  noise of the hand-written guard for `NotNull`.
- **Extending a chain is free everywhere**: `NullableRange` runs two checks and allocates the same
  as one, because a check returns the contract it was called on.
- Failure paths are deliberately not measured. A throwing guard is not a hot path, and exception
  construction dwarfs everything above.
