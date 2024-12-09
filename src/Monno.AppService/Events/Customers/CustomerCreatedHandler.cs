using Monno.Core.Events;
using Monno.Core.Events.DomainEvents;
using Monno.Infra.Broker.Publisher;

namespace Monno.AppService.Events.Customers;

public class CustomerCreatedHandler : IDomainEventHandler<CustomerCreatedEvent>
{
    private readonly IEventPublisher _publisher;

    public CustomerCreatedHandler(IEventPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task HandleAsync(CustomerCreatedEvent @event)
    {
        await _publisher.PublishAsync(@event, "customer-created");
    }
}