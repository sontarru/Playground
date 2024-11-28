using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmarks>();

public class Benchmarks
{
    [Benchmark]
    public void UseLocal()
    {
        Foo foo = new();
        var bar = foo.Bar;

        var n = 1024 * 1024;
        while(n-- > 0)
        {
            bar++;
        }

        foo.Bar = bar;
    }

    [Benchmark]
    public void UseHeap()
    {
        Foo foo = new();

        var n = 1024 * 1024;
        while(n-- > 0)
        {
             foo.Bar++;
        }
    }

    [Benchmark]
    public void UseLocalVolatile()
    {
        Foo foo = new();
        var bar = foo.Bar;

        var n = 1024 * 1024;
        while(n-- > 0)
        {
            Volatile.Write(ref bar, Volatile.Read(ref bar) + 1);
        }

        foo.Bar = bar;
    }

    [Benchmark]
    public void UseHeapVolatile()
    {
        Foo foo = new();

        var n = 1024 * 1024;
        while(n-- > 0)
        {
            Volatile.Write(ref foo.Bar, Volatile.Read(ref foo.Bar) + 1);
        }
    }
}

public class Foo
{
    public int Bar;
}
