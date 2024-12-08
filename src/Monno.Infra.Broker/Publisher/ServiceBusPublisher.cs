using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Monno.Infra.Broker.Messages;
using Monno.SharedKernel.Common;
using Newtonsoft.Json;
using System.Text;

namespace Monno.Infra.Broker.Publisher;

public class ServiceBusPublisher(IConfiguration configuration) : IEventPublisher
{
    private readonly string _connectionString = configuration.GetConnectionString("AzureServiceBus")!;

    public async Task PublishAsync<TMessage>(TMessage message, string topicName, CancellationToken cancellation = default) where TMessage : class, IIntegrationMessage
    {
        var options = Options;

        var client = new ServiceBusClient(_connectionString, options);

        var sender = client.CreateSender(topicName);
        var json = JsonConvert.SerializeObject(message);

        var serviceBusMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(json))
        {
            ContentType = "application/json",
            MessageId = HashHelper.Create(json)
        };

        serviceBusMessage.ApplicationProperties.Add("message-type", message.GetType().Name);
        serviceBusMessage.ApplicationProperties.Add("source", "Monno.Services.Customers");

        await sender.SendMessageAsync(serviceBusMessage, cancellation);
        await sender.CloseAsync(cancellation);
    }

    private static ServiceBusClientOptions Options => new()
    {
        RetryOptions = new ServiceBusRetryOptions
        {
            Mode = ServiceBusRetryMode.Exponential,
            MaxRetries = 10,
            MaxDelay = TimeSpan.FromSeconds(10)
        }
    };
}