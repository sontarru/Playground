using System.Diagnostics.CodeAnalysis;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using Microsoft.Diagnostics.Tracing.AutomatedAnalysis;

// BenchmarkRunner.Run<BoxingVsCopyBenchmark>();
BenchmarkRunner.Run<NewClassVsStructBenchmark>();

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

sealed class FooClass
{
    public FooClass(int id) => Id = id;

    public int Id { get; init; }
}

struct FooStruct
{
    public FooStruct(int id) => Id = id;

    public int Id { get; init; }
}

/// <summary>
/// Benchmarks new [class] vs. new [struct].
/// </summary>
[SuppressMessage("Design", "CA1050: Declare types in namespaces", Justification =
    "Not required in a top-level statements program.")]
[SuppressMessage("Maintainability", "CA1515: Consider making public types internal", Justification =
    "Should be public to use from the Benchmark.Net.")]
public class NewClassVsStructBenchmark
{
    private const int Count = 1024 * 1024;

    private readonly FooClass[] _classes = new FooClass[Count];
    private readonly FooStruct[] _structs = new FooStruct[Count];

    /// <summary>
    /// Benchmarks new [class].
    /// </summary>
    [Benchmark]
    public void NewClass()
    {
        for(var i = 0; i < Count; i++)
        {
            _classes[i] = new FooClass(i);
        }
    }

    /// <summary>
    /// Benchmarks new [struct].
    /// </summary>
    [Benchmark]
    public void NewStruct()
    {
        for(var i = 0; i < Count; i++)
        {
            _structs[i] = NewStruct(i);
        }
    }

    private static FooStruct NewStruct(int i) => new FooStruct(i);
}
