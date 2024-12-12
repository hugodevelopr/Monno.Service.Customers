using Monno.Messaging;

namespace Monno.Infra.Broker.EventSourcing;

public class EventData : IIntegrationMessage
{
    public Guid Id { get; set; }
    public Guid AggregateId { get; set; }
    public Guid ActorId { get; set; }
    public string EventName { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
    public string AssemblyQualifiedName { get; set; } = string.Empty;
    public string ApplicationName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}