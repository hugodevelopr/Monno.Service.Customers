using Microsoft.Extensions.DependencyInjection;
using Monno.Core.Events.DomainEvents;
using Monno.SharedKernel.Events;

namespace Monno.Core.Events.Dispatcher;

public class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IDomainEvent
    {
        using var scope = serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IDomainEventHandler<TEvent>>();
        await handler.HandleAsync(@event);
    }
}