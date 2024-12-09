using Microsoft.Extensions.DependencyInjection;
using Monno.Core.Commands;
using Monno.Core.Commands.Dispatcher;
using Monno.Core.Events.Dispatcher;
using Monno.Core.Events.DomainEvents;

namespace Monno.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
        => services
            .AddInMemoryCommandDispatcher()
            .AddInMemoryDomainEventDispatcher();

    private static IServiceCollection AddInMemoryCommandDispatcher(this IServiceCollection services)
        => services
            .AddSingleton<ICommandDispatcher, CommandDispatcher>();

    private static IServiceCollection AddInMemoryDomainEventDispatcher(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
        return services;
    }
}