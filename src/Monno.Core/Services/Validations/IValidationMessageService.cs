namespace Monno.Core.Services.Validations;

public interface IValidationMessageService : IDisposable
{
    Task<(string Code, string Message)> GetMessageAsync(string keyword, string language = "en");
}