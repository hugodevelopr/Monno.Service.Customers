using Monno.Core.Events;
using Monno.Core.ValueObjects.Customers;
using Monno.SharedKernel.Entities;
using Monno.SharedKernel.Events;

namespace Monno.Core.Entities.Customers;

public class Customer : BaseEntity
{
    public string Email { get; private set; } = string.Empty;
    public CustomerName Name { get; private set; } = default!;
    public Document Document { get; private set; } = default!;
    public CustomerState State { get; private set; }
    public string IpAddress { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastUpdatedAt { get; private set; }

    public void Create(string email, string firstName, string lastName)
    {
        Id = Guid.NewGuid();
        Email = email;
        Name = new CustomerName(firstName, lastName);
        State = CustomerState.WaitingForActivation;
        CreatedAt = DateTime.UtcNow;
        LastUpdatedAt = null;

        var @event = new CustomerCreatedEvent()
        {
            CustomerId = Id,
            Email = Email,
            FirstName = Name.FirstName,
            LastName = Name.LastName
        };

        ApplyChange(@event);
    }

    public enum CustomerState
    {
        WaitingForActivation = 1,
        Active = 2,
        Inactive = 3,
        Blocked = 4,
        Deleted = 5
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