using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.Cryptography;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<ParseStringBenchmarks>();

/// <summary>
/// The <see cref="decimal.Parse(string)"/> vs <see cref="double.Parse(string)"/> benchmark.
/// </summary>
[SuppressMessage("Design", "CA1050: Declare types in namespaces", Justification =
    "Not required in a top-level statements program.")]
[SuppressMessage("Maintainability", "CA1515: Consider making public types internal", Justification =
    "Should be public to use from the Benchmark.Net.")]
public class ParseStringBenchmarks
{
    private const int StringsToParseNumber = 5 * 1024;

    private readonly string[] _stringsToParse;

    /// <summary>
    /// Initialize a new instance of the <see cref="ParseStringBenchmarks"/> type.
    /// </summary>
    public ParseStringBenchmarks()
    {
        // Generates an array of strings in the 'dddd.dddd' format.
        _stringsToParse = Enumerable.Repeat(0, StringsToParseNumber).Select(_ =>
            string.Concat(
                Enumerable.Repeat(default(object), 9).Select((_, i) =>
                    (i != 4)
                        ? RandomNumberGenerator.GetInt32(0, 10)
                            .ToString(CultureInfo.InvariantCulture)
                        : ".")))
            .ToArray();
    }

    /// <summary>
    /// Parse a large <c cref="string"/> array to the <c cref="decimal"/> array.
    /// </summary>
    [Benchmark]
    public void ParseDecimal()
    {
        foreach (var s in _stringsToParse)
        {
            _ = decimal.Parse(s, CultureInfo.InvariantCulture);
        }
    }

    /// <summary>
    /// Parse a large <c cref="string"/> array to the <c cref="double"/> array.
    /// </summary>
    [Benchmark]
    public void ParseDouble()
    {
        foreach (var s in _stringsToParse)
        {
            _ = double.Parse(s, CultureInfo.InvariantCulture);
        }
    }
}
