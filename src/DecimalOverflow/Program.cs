using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using Db db = new(
    new DbContextOptionsBuilder<Db>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options);

Foo foo = new()
{
    Bar = 1000000
};

db.Foos.Add(foo);

try
{
    db.SaveChanges();
}
catch (ValidationException e)
{
    Console.WriteLine($"Ololo: {e.Message}");
}

class Foo
{
    public int Id { get; set; }

    [Range(0, 1000)]
    public int Bar { get; set; }
}

class Db: DbContext
{
    public DbSet<Foo> Foos { get; set; }

    public Db(DbContextOptions<Db> options): base(options)
    {
    }

    public override int SaveChanges()
    {
        ValidateEntities();
        return base.SaveChanges();
    }

    private void ValidateEntities()
    {
        foreach(var entry in ChangeTracker.Entries().Where(e =>
            e.State == EntityState.Added || e.State == EntityState.Modified))
        {
            var entity = entry.Entity;
            Validator.ValidateObject(entity, new(entity), validateAllProperties: true);
        }
    }
}
