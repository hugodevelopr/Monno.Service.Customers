using Microsoft.Extensions.Configuration;

namespace Monno.Infra.Repository.Data;

public abstract class BaseRepository
{
    protected BaseRepository(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("MonnoCustomerDb");
    }

    protected string? ConnectionString { get; }
}