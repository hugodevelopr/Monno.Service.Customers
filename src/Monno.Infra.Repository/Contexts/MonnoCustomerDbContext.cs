using Microsoft.EntityFrameworkCore;
using Monno.Core.Entities.Messages;
using Monno.SharedKernel.Entities;

namespace Monno.Infra.Repository.Contexts;

public class MonnoCustomerDbContext : DbContext
{
    /// <inheritdoc />
    public MonnoCustomerDbContext(DbContextOptions<MonnoCustomerDbContext> options)
        : base(options)
    {
    }

    public DbSet<ValidationMessage> ValidationMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MonnoCustomerDbContext).Assembly);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;
            if (typeof(BaseEntity).IsAssignableFrom(clrType))
            {
                modelBuilder.Entity(clrType).Ignore("Version");
            }
        }
    }
}