using Finvo.Infra.Repository.Data;
using Microsoft.Extensions.Configuration;
using Monno.Core.Outbox;

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
        throw new NotImplementedException();
    }

    public async Task MarkAsPublishedAsync(OutboxMessage outboxMessage)
    {
        throw new NotImplementedException();
    }
}