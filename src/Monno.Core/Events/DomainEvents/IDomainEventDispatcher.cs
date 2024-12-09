using Monno.SharedKernel.Events;

namespace Monno.Core.Events.DomainEvents;

public interface IDomainEventDispatcher
{
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IDomainEvent;
}