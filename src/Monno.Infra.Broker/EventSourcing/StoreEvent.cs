using Monno.EventSourcing;
using Monno.Infra.Broker.Publisher;
using Monno.SharedKernel.Events;
using Newtonsoft.Json;

namespace Monno.Infra.Broker.EventSourcing;

public class StoreEvent(IEventPublisher publisher) : IStoreEvent
{
    public async Task SaveEventAsync(IDomainEvent @event, Guid aggregateId, Guid actorId)
    {
        var eventData = new EventData()
        {
            AggregateId = aggregateId,
            ActorId = actorId,
            EventName = @event.GetType().Name,
            AssemblyQualifiedName = @event.GetType().AssemblyQualifiedName!,
            Payload = JsonConvert.SerializeObject(@event),
            ApplicationName = "Monno.Service.Customers",
            CreatedAt = DateTime.UtcNow
        };

        await publisher.PublishAsync(eventData, "event-store");
    }
}