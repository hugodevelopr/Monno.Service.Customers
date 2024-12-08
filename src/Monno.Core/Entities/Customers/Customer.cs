using Monno.Core.Events;
using Monno.Core.ValueObjects;
using Monno.Core.ValueObjects.Customers;
using Monno.SharedKernel.Entities;
using Monno.SharedKernel.Events;

namespace Monno.Core.Entities.Customers;

public class Customer : BaseEntity
{
    public string Email { get; private set; } = string.Empty;
    public CustomerName Name { get; private set; } = default!;
    public Document Document { get; private set; } = default!;
    public Address Address { get; private set; } = default!;
    public string IpAddress { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    public void Create(string email, string firstName, string lastName)
    {
        Id = Guid.NewGuid();
        Email = email;
        Name = new CustomerName(firstName, lastName);
        CreatedAt = DateTime.UtcNow;

        var @event = new CustomerCreatedEvent()
        {
            CustomerId = Id,
            Email = Email,
            FirstName = Name.FirstName,
            LastName = Name.LastName
        };

        ApplyChange(@event);
    }

    protected override void Apply(IDomainEvent @event)
    {
        switch (@event)
        {
            case CustomerCreatedEvent e:
                break;
        }
    }
}