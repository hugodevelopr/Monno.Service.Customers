using Microsoft.Extensions.Configuration;

namespace Finvo.Infra.Repository.Data;

public abstract class BaseRepository
{
    protected BaseRepository(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("FinvoDb");
    }

    protected string? ConnectionString { get; }
}