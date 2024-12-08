using Microsoft.Extensions.DependencyInjection;
using Monno.Core.Outbox;
using Monno.Core.Repositories.Customers;
using Monno.Core.Repositories.Messages;
using Monno.Infra.Repository.Data.Customers;
using Monno.Infra.Repository.Data.Messages;
using Monno.Infra.Repository.Data.Outbox;

namespace Monno.Infra.Repository;

public static class Extensions
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    => services
        .AddTransient<ICustomerRepository, CustomerRepository>()
        .AddTransient<IValidationMessageRepository, ValidationMessageRepository>()
        .AddTransient<IOutboxRepository, OuboxRepository>();
}