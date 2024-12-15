using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Monno.Core.Outbox;
using Monno.Infra.Broker;
using Monno.Infra.Broker.Publisher;
using Monno.Messaging;
using Monno.SharedKernel.Common;
using Serilog;

namespace Monno.AppService.Jobs.Outbox;

public class OutboxProcessor : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    /// <inheritdoc />
    public OutboxProcessor(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var outboxRepository = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();
        var publisher = scope.ServiceProvider.GetRequiredService<IEventPublisher>();

        while (!stoppingToken.IsCancellationRequested)
        {
            var events = await outboxRepository.GetUnpublishedEventsAsync();
            var outboxMessages = events.ToList();

            if (events is null || !outboxMessages.Any())
            {
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
                continue;
            }

            foreach (var item in outboxMessages)
            {
                if (ReflectionHelper.PopulateObject(item.Assembly, item.Payload) is not IIntegrationMessage message)
                {
                    Log.Warning($"Unable to create event {item.EventName} with payload {item.Payload}");
                    continue;
                }

                var topicName = BrokerHelper.GetTopicName(item.EventName);

                await publisher.PublishAsync(message, topicName, stoppingToken);
                await outboxRepository.MarkAsPublishedAsync(item);
            }

            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}