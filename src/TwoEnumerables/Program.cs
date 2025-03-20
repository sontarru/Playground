using System.Collections;

Ololo ololo = new();

foreach (Foo foo in ololo)
{
    Console.WriteLine(foo); // "Foo"
}

/* Не компилируется
foreach (Bar bar in ololo)
{
    Console.WriteLine(bar);
}
*/

foreach (var @var in ololo)
{
    Console.WriteLine(@var); // "Foo"
}

foreach (object obj in ololo)
{
    Console.WriteLine(obj); // "Foo"
}

class Ololo : IEnumerable<Foo>, IEnumerable<Bar>
{
    private readonly object[] _objects = [
      new Foo(), new Bar(),
      new Foo(), new Bar(),
      new Foo(), new Bar()
    ];

    public IEnumerator<Foo> GetEnumerator()
        => _objects.OfType<Foo>().GetEnumerator();

    IEnumerator<Bar> IEnumerable<Bar>.GetEnumerator()
        => _objects.OfType<Bar>().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => _objects.OfType<Bar>().GetEnumerator();
}

class Foo
{
}

class Bar
{
}
