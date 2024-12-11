using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monno.Core.Entities.Customers;

namespace Monno.Infra.Repository.Map.Customers;

public class CustomerMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer", "Customer");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.State)
            .IsRequired();

        builder.OwnsOne(x => x.Name, name =>
        {
            name.Property(n => n.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(100)
                .IsRequired();

            name.Property(n => n.LastName)
                .HasColumnName("LastName")
                .HasMaxLength(200)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Document, document =>
        {
            document.Property(d => d.Number)
                .HasColumnName("DocumentNumber")
                .HasMaxLength(20)
                .IsRequired();

            document.Property(d => d.Type)
                .HasColumnName("DocumentType")
                .HasMaxLength(20)
                .IsRequired();
        });

        builder.Property(x => x.IpAddress)
            .HasMaxLength(50);

        builder.HasIndex(x => x.Id);
        builder.HasIndex(x => x.Email);
    }
}