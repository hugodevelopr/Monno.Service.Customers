namespace Monno.Core.Outbox;

public interface IOutboxRepository
{
    Task<IEnumerable<OutboxMessage>> GetUnpublishedEventsAsync();
    Task MarkAsPublishedAsync(OutboxMessage outboxMessage);
}