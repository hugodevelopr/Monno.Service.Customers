using Monno.SharedKernel.Events;

namespace Monno.Core.Events.DomainEvents;

public interface IDomainEventHandler<in TEvent> where TEvent : class, IDomainEvent
{
    Task HandleAsync(TEvent @event);
}