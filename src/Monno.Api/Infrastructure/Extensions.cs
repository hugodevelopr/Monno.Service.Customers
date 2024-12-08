using Finvo.Infra.Repository;
using Finvo.Infra.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using Monno.Api.Infrastructure.Filters;
using Monno.AppService;
using Monno.AppService.Jobs.Outbox;
using Monno.Infra.Broker;
using Monno.Infra.Repository;
using Monno.Infra.Repository.Contexts;

namespace Monno.Api.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddModules(this IServiceCollection services)
        => services
            .AddAppService()
            .AddRepository()
            .AddBroker();

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static IServiceCollection AddFilters(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ApiExceptionFilter>();
        });

        return services;
    }

    public static IServiceCollection AddJobs(this IServiceCollection services)
    {
        services.AddHostedService<OutboxProcessor>();

        return services;
    }

    public static IServiceCollection AddEntityFramework(this IServiceCollection services, ConfigurationManager configuration)
    {
        const string assemblyName = "Finvo.Api";
        var customerConnectionString = configuration.GetConnectionString("FinvoDb");
        var storeConnectionString = configuration.GetConnectionString("FinvoStoreEventDb");

        services.AddDbContext<MonnoDbContext>(options =>
        {
            options.EnableDetailedErrors();
            options.UseSqlServer(customerConnectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(assemblyName);
            });
        });

        services.AddDbContext<MonnoStoreEventDbContext>(options =>
        {
            options.EnableDetailedErrors();
            options.UseSqlServer(storeConnectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(assemblyName);
            });
        });

        return services;
    }
}