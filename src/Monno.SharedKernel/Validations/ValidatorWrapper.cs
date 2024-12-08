using FluentValidation;

namespace Monno.SharedKernel.Validations;

public static class ValidatorWrapper
{
    public static async Task<Result> ValidateCommandAsync<TCommand>(this IValidator<TCommand> validator, TCommand command)
    {
        if (validator == null)
            throw new ArgumentNullException(nameof(validator));

        if (command == null)
            throw new ArgumentNullException(nameof(command));

        var result = await validator.ValidateAsync(new ValidationContext<TCommand>(command));

        var errors = result.Errors
            .Select(failure => new Error(failure.ErrorCode, failure.ErrorMessage))
            .ToList();

        return errors.Any() ? Result.Fail(new List<Error>(errors)) : Result.Ok();
    }
}