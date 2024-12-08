using Monno.SharedKernel.Entities;
using Monno.SharedKernel.Events;

namespace Monno.Core.Entities.Messages;

public class ValidationMessage : BaseEntity
{
    public ValidationMessage(string keyword, string errorCode, string message, string language = "en")
    {
        Id = Guid.NewGuid();
        Keyword = keyword;
        ErrorCode = errorCode;
        Message = message;
        Language = language;
    }

    public string Keyword { get; private set; }
    public string ErrorCode { get; private set; }
    public string Message { get; private set; }
    public string Language { get; private set; }

    protected override void Apply(IDomainEvent @event)
    {
    }
}