using Monno.Core.Repositories.Messages;
using Monno.Core.Services.Validations;
using Monno.SharedKernel.Services;

namespace Monno.AppService.Services;

public class ValidationMessageService : BaseService, IValidationMessageService
{
    private readonly IValidationMessageRepository _repository;

    /// <inheritdoc />
    public ValidationMessageService(IValidationMessageRepository repository)
    {
        _repository = repository;
    }

    public async Task<(string Code, string Message)> GetMessageAsync(string keyword, string language = "en")
    {
        return await _repository.GetMessageAsync(keyword, language);
    }
}