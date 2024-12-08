using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Monno.Core.Outbox;
using Serilog;

namespace Monno.Infra.Repository.Data.Outbox;

public class OuboxRepository : BaseRepository, IOutboxRepository
{
    /// <inheritdoc />
    public OuboxRepository(IConfiguration configuration) 
        : base(configuration)
    {
    }

    public async Task<IEnumerable<OutboxMessage>> GetUnpublishedEventsAsync()
    {
        await using var conn = new SqlConnection(ConnectionString);

        try
        {
            const string query = """
                                    SELECT
                                        Id,
                                        AggregateId,
                                        Type,
                                        Data,
                                        CreatedAt
                                    FROM
                                        Outbox.Outbox
                                    WHERE
                                        Sent = 0
                                    ORDER BY CreatedAt
                                 """;

            await conn.OpenAsync();
            return (await conn.QueryAsync<OutboxMessage>(query)).ToList();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while getting unpublished events");
            throw;
        }
        finally
        {
            await conn.CloseAsync();
        }
    }

    public async Task MarkAsPublishedAsync(OutboxMessage outboxMessage)
    {
        await using var conn = new SqlConnection(ConnectionString);

        try
        {
            const string query = """
                                  UPDATE Outbox.Outbox
                                  SET
                                    Sent = 1
                                  WHERE
                                    Id = @Id
                                  """;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while marking event as published");
            throw;
        }
        finally
        {
            await conn.CloseAsync();
        }
    }
}