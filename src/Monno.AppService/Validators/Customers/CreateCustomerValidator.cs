using System.Net;
using FluentValidation;
using Monno.AppService.Commands.Customers;
using Monno.Core.Repositories.Customers;
using Monno.Core.Services.Validations;

namespace Monno.AppService.Validators.Customers;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    private readonly ICustomerRepository _repository;

    public CreateCustomerValidator(IValidationMessageService service, ICustomerRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(service, "NOT_EMPTY");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage(service, "EMAIL_INVALID");

        RuleFor(x => x.Email)
            .MustAsync(EmailAlreadyExists)
            .WithMessage(service, "EMAIL_ALREADY_EXISTS");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(service, "NOT_EMPTY");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(service, "NOT_EMPTY");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(service, "NOT_EMPTY");
    }

    private async Task<bool> EmailAlreadyExists(string email, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByEmailAsync(email);
        return customer is null;
    }
}