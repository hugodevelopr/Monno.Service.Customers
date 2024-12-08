using Monno.Infra.Broker.Messages;

namespace Monno.Infra.Broker.Publisher;

public interface IEventPublisher
{
    Task PublishAsync<TMessage>(TMessage message, string topic, CancellationToken cancellation = default)
        where TMessage : class, IIntegrationMessage;
}