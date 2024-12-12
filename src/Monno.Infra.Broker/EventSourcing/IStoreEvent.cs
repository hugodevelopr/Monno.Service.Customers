using Monno.SharedKernel.Events;

namespace Monno.Infra.Broker.EventSourcing;

public interface IStoreEvent
{
    Task SaveEventAsync(IDomainEvent @event, Guid aggregateId, Guid actorId);
}