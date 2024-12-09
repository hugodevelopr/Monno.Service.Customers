using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monno.Core.Outbox;

namespace Monno.Infra.Repository.Map.Outbox;

public class OutboxMessageMap : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("Outbox", "Outbox");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.EventName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Payload)
            .IsRequired();

        builder.Property(x => x.Sent)
            .IsRequired();

        builder.Property(x => x.Assembly)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.Sent);
        builder.HasIndex(x => x.EventName);
    }
}