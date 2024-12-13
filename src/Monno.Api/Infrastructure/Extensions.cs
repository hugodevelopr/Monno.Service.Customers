using Asp.Versioning;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Monno.Api.Infrastructure.Filters;
using Monno.Api.Infrastructure.Settings;
using Monno.AppService;
using Monno.AppService.Jobs.Outbox;
using Monno.AppService.Mappers;
using Monno.Core;
using Monno.Infra.Broker;
using Monno.Infra.Repository;
using Monno.Infra.Repository.Contexts;
using NSwag.Generation.Processors.Security;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

namespace Monno.Api.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddModules(this IServiceCollection services)
        => services
            .AddAppService()
            .AddCore()
            .AddRepository()
            .AddBroker();

    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var keycloakSettings = configuration.GetSection("Keycloak").Get<KeycloakSettings>()!;

        services.AddOpenApiDocument(options =>
        {
            options.Title = "Monno Service Customers API";
            options.DocumentName = "v1";
            options.Version = "v1";

            options.AddSecurity("oauth2", new NSwag.OpenApiSecurityScheme()
            {
                Type = NSwag.OpenApiSecuritySchemeType.OAuth2,
                Flows = new NSwag.OpenApiOAuthFlows
                {
                    AuthorizationCode = new NSwag.OpenApiOAuthFlow
                    {
                        AuthorizationUrl = $"{keycloakSettings.Authority}/protocol/openid-connect/auth",
                        TokenUrl = $"{keycloakSettings.Authority}/protocol/openid-connect/token",
                        Scopes = new Dictionary<string, string>()
                        {
                            {"openid", "Get token id, profile and email"}
                        }
                    }
                }
            });

            options.OperationProcessors.Add(new OperationSecurityScopeProcessor("oauth2"));
        });

        return services;
    }

    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new HeaderApiVersionReader("api-version");
        });

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

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var keycloakSettings = configuration.GetSection("Keycloak").Get<KeycloakSettings>()!;

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = keycloakSettings.Authority;
                options.RequireHttpsMetadata = true;
                options.Audience = keycloakSettings.ClientId;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidAudiences = new[]
                    {
                        "monno-service-gateway",
                        "monno-service-customers"
                    }
                };
            });

        return services;
    }

    public static void AddOpenTelemetry(this ILoggingBuilder logging, IConfiguration configuration)
    {
        logging.ClearProviders();
        logging.AddOpenTelemetry(x =>
        {
            x.SetResourceBuilder(ResourceBuilder.CreateEmpty()
                .AddService("Monno.Service.Customers"));

            x.IncludeScopes = true;
            x.IncludeFormattedMessage = true;

            x.AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri(configuration["Seq:ServerUrl"]!);
                options.Protocol = OtlpExportProtocol.HttpProtobuf;
                options.Headers = configuration["Seq:ApiKey"]!;
            });
        });
    }
}