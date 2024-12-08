using Microsoft.Extensions.DependencyInjection;
using Monno.Infra.Broker.EventSourcing;
using Monno.Infra.Broker.Publisher;

namespace Monno.Infra.Broker;

public static class Extensions
{
    public static IServiceCollection AddBroker(this IServiceCollection services)
    => services
        .AddScoped<IEventPublisher, ServiceBusPublisher>()
        .AddScoped<IStoreEvent, StoreEvent>();
}