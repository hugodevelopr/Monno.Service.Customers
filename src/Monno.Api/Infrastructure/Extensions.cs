using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Monno.Api.Infrastructure.Filters;
using Monno.AppService;
using Monno.AppService.Mappers;
using Monno.Core;
using Monno.Infra.Broker;
using Monno.Infra.Repository;
using Monno.Infra.Repository.Contexts;

namespace Monno.Api.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddModules(this IServiceCollection services)
        => services
            .AddAppService()
            .AddCore()
            .AddRepository()
            .AddBroker();

    public static IServiceCollection AddSwagger(this IServiceCollection services)
        => services.AddOpenApi();

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
        //services.AddHostedService<OutboxProcessor>();

        return services;
    }

    public static IServiceCollection AddEntityFramework(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("MonnoCustomerDb");

        services.AddDbContext<MonnoCustomerDbContext>(options =>
        {
            options.EnableDetailedErrors();
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly("Monno.Api");
                sqlOptions.MigrationsHistoryTable("History", "_Migrations");
            });
        });

        return services;
    }

    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}