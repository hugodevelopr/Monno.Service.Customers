using Monno.Messaging;
using Monno.SharedKernel.Events;

namespace Monno.Core.Events;

public class CustomerCreatedEvent : IDomainEvent, IIntegrationMessage
{
    public Guid CustomerId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}