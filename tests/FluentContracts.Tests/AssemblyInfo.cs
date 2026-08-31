using Xunit;

// DummyData draws every value from a single Faker seeded with a fixed number, which only produces
// reproducible data if one test at a time is drawing from it. xUnit runs test classes in parallel by
// default, so the sequence a given test received depended on timing, and System.Random is not safe to
// share across threads. Running serially keeps the seeded data deterministic; the suite takes under a
// second, so there is nothing to gain from the parallelism.
[assembly: CollectionBehavior(DisableTestParallelization = true)]
