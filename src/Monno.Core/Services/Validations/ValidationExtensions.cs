using FluentValidation;

namespace Monno.Core.Services.Validations;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, TProperty> WithMessage<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule,
        IValidationMessageService service,
        string keyword)
    {
        var messageValidation = service.GetMessageAsync(keyword);
        messageValidation.Wait();

        var (code, message) = messageValidation.Result;
        
        return rule
            .WithMessage(message)
            .WithErrorCode(code);
    }
}