using Microsoft.Extensions.DependencyInjection;
using Monno.AppService.Services;
using Monno.Core.Commands;
using Monno.Core.Services.Validations;
using Monno.SharedKernel.Attributes;

namespace Monno.AppService;

public static class Extensions
{
    public static IServiceCollection AddAppService(this IServiceCollection services)
    {
        services.AddCommandHandlers();

        services.AddTransient<IValidationMessageService, ValidationMessageService>();

        return services;
    }

    private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services.Scan(s =>
            s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>))
                    .WithAttribute(typeof(DecoratorAttribute)))
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
}