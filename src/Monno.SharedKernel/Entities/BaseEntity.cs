using Monno.SharedKernel.Aggregates;

namespace Monno.SharedKernel.Entities;

public abstract class BaseEntity : AggregateRoot
{
    public Guid Id { get; set; }
}