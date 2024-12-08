using Monno.Infra.Broker.Messages;
using Monno.SharedKernel.Events;

namespace Monno.Core.Events;

public class CustomerCreatedEvent : IDomainEvent, IIntegrationMessage
{
    public Guid CustomerId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}