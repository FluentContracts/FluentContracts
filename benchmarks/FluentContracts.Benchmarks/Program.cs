using BenchmarkDotNet.Running;

BenchmarkSwitcher.FromAssembly(typeof(FluentContracts.Benchmarks.GuardClauseBenchmarks).Assembly).Run(args);
