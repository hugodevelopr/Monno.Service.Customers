using Microsoft.EntityFrameworkCore;

namespace Finvo.Infra.Repository.Contexts;

public class MonnoStoreEventDbContext : DbContext
{
    public MonnoStoreEventDbContext(DbContextOptions<MonnoStoreEventDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MonnoStoreEventDbContext).Assembly);
    }
}