using Microsoft.EntityFrameworkCore;
using Monno.Core.Entities.Messages;

namespace Monno.Infra.Repository.Contexts;

public class MonnoDbContext : DbContext
{
    public MonnoDbContext(DbContextOptions<MonnoDbContext> options)
        : base(options)
    {
    }

    public DbSet<ValidationMessage> ValidationMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MonnoDbContext).Assembly);
    }
}