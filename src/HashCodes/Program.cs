using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Globalization", "CA1303: Do not pass literals as localized parameters")]

Foo foo1 = new() { Id = 42, Ololo = 73, Name = "foo1" };
Foo foo2 = new() { Id = 42, Ololo = 51, Name = "foo2" };

// чтозанах - ожидаем false, но получаем true...
Console.WriteLine("Compare struct hashcodes.");
Console.WriteLine(foo1.GetHashCode() == foo2.GetHashCode());

Bar bar1 = new(42, "Ololo");
Bar bar2 = new(42, "Ololo");

// ожидаем true
Console.WriteLine("Compare record hashcodes.");
Console.WriteLine(bar1.GetHashCode() == bar2.GetHashCode());

class Foo
{
    public int Id;
    public int Ololo;
    public required string Name { get; set; }
}

record Bar(int Id, string Name);
