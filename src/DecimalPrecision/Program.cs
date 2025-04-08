using System.Globalization;
using System.Reflection;

// В .NET для decimal положено максимум 28 знаков после запятой, поэтому вот это
// напечатает 'True'
var s = "0.0000000000000000000000000000001";
Console.WriteLine(decimal.Parse(s, CultureInfo.InvariantCulture) == 0);

//
// Но...
//

object x = 1m;

x.GetType().GetField("_flags", BindingFlags.Instance | BindingFlags.NonPublic)
    ?.SetValue(x, 0x00_1c_00_00);

// Напечатает 0.0000000000000000000000000000001 (31 знак после запятой)
Console.WriteLine(x);

// И даже арифметика работает
// 0.0000000000000000000000000000002
Console.WriteLine((decimal)x + (decimal)x);
Console.WriteLine(2* (decimal)x);
