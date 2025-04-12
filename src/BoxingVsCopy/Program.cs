using System.Diagnostics.CodeAnalysis;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<BoxingVsCopyBenchmark>();

/// <summary>
/// Benchmarks copy vs. boxing performance.
/// </summary>
[SuppressMessage("Design", "CA1050: Declare types in namespaces", Justification =
    "Not required in a top-level statements program.")]
[SuppressMessage("Maintainability", "CA1515: Consider making public types internal", Justification =
    "Should be public to use from the Benchmark.Net.")]
public class BoxingVsCopyBenchmark
{
    private const int Count = 1024 * 1024;

    private readonly Guid[] _x = Enumerable.Repeat(Guid.NewGuid(), Count).ToArray();
    private readonly Guid[] _y = new Guid[Count];
    private readonly object[] _z = new object[Count];

    /// <summary>
    /// Copies x to z directly.
    /// </summary>
    [Benchmark]
    public void DirectCopy()
    {
        for(var i = 0; i < Count; i++)
        {
            _y[i] = _x[i];
        }
    }

    /// <summary>
    /// Copies x to z with boxing.
    /// </summary>
    [Benchmark]
    public void BoxingCopy()
    {
        for(var i = 0; i < Count; i++)
        {
            _z[i] = _x[i];
        }
    }
}
