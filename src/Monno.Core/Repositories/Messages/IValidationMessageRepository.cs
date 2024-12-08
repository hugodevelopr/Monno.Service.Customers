namespace Monno.Core.Repositories.Messages;

public interface IValidationMessageRepository
{
    Task<(string Code, string Message)> GetMessageAsync(string keyword, string language);
}