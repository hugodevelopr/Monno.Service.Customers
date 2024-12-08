using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monno.Core.Entities.Messages;

namespace Monno.Infra.Repository.Map.Messages;

public class ValidationMessageMap : IEntityTypeConfiguration<ValidationMessage>
{
    public void Configure(EntityTypeBuilder<ValidationMessage> builder)
    {
        builder.ToTable("Validation", "Message");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Keyword)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Language)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.ErrorCode)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(x => x.Message)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasIndex(x => new { x.Keyword, x.Language });
    }
}