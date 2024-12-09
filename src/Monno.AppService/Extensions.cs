using Microsoft.Extensions.DependencyInjection;
using Monno.AppService.Commands.Customers;
using Monno.AppService.Commands.Customers.Handlers;
using Monno.AppService.Events.Customers;
using Monno.AppService.Responses.Customers;
using Monno.AppService.Services;
using Monno.Core.Commands;
using Monno.Core.Events;
using Monno.Core.Events.DomainEvents;
using Monno.Core.Services.Validations;
using Monno.SharedKernel.Attributes;

namespace Monno.AppService;

public static class Extensions
{
    public static IServiceCollection AddAppService(this IServiceCollection services)
    {
        services.AddCommandHandlers();
        services.AddDomainEventHandlers();
        services.AddServices();

        return services;
    }

    private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services.Scan(s =>
            s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>))
                    .WithoutAttribute(typeof(DecoratorAttribute)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        services.Scan(s =>
            s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>))
                    .WithoutAttribute(typeof(DecoratorAttribute)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return services;
    }

    private static IServiceCollection AddDomainEventHandlers(this IServiceCollection services)
    {
        services.AddTransient<IDomainEventHandler<CustomerCreatedEvent>, CustomerCreatedHandler>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IValidationMessageService, ValidationMessageService>();
        
        return services;
    }
}