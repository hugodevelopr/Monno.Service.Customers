namespace Monno.Core.Outbox;

public class OutboxMessage
{
    private OutboxMessage()
    {
        Id = Guid.NewGuid();
        Sent = false;
        CreatedAt = DateTime.UtcNow;
    }

    public OutboxMessage(Type type, string payload)
        : this()
    {
        EventName = type.Name;
        Payload = payload;
        Assembly = type.AssemblyQualifiedName!;
    }

    public Guid Id { get; private set; }
    public string EventName { get; private set; } = string.Empty;
    public string Payload { get; private set; } = string.Empty;
    public bool Sent { get; private set; }
    public string Assembly { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
}