Foo foo1 = new() { Id = 42, Ololo = 73, Name = "foo1" };
Foo foo2 = new() { Id = 42, Ololo = 51, Name = "foo2" };

// чтозанах - ожидаем false, но получаем true...
Console.WriteLine(foo1.GetHashCode() == foo2.GetHashCode());

struct Foo
{
    public int Id;
    public int Ololo;
    public string Name;
}

