using System.Diagnostics.CodeAnalysis;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmarks>();

/// <summary>
/// 
/// </summary>
[SuppressMessage("Maintainability", "CA1515:Consider making public types internal", Justification = "<Pending>")]
[SuppressMessage("Design", "CA1050:Declare types in namespaces", Justification = "<Pending>")]
public class Benchmarks
{
    /// <summary>
    /// 
    /// </summary>
    [Benchmark]
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public void UseLocal()
    {
        Foo foo = new();
        var bar = foo.Bar;

        var n = 1024 * 1024;
        while (n-- > 0)
        {
            bar++;
        }

        foo.Bar = bar;
    }

    /// <summary>
    /// 
    /// </summary>
    [Benchmark]
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public void UseHeap()
    {
        Foo foo = new();

        var n = 1024 * 1024;
        while (n-- > 0)
        {
            foo.Bar++;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Benchmark]
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public void UseLocalVolatile()
    {
        Foo foo = new();
        var bar = foo.Bar;

        var n = 1024 * 1024;
        while (n-- > 0)
        {
            Volatile.Write(ref bar, Volatile.Read(ref bar) + 1);
        }

        foo.Bar = bar;
    }

    /// <summary>
    /// 
    /// </summary>
    [Benchmark]
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public void UseHeapVolatile()
    {
        Foo foo = new();

        var n = 1024 * 1024;
        while (n-- > 0)
        {
            Volatile.Write(ref foo.Bar, Volatile.Read(ref foo.Bar) + 1);
        }
    }
}

class Foo
{
    public int Bar;
}
