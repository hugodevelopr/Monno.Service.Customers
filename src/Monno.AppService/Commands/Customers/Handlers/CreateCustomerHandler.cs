using AutoMapper;
using Monno.AppService.Responses.Customers;
using Monno.AppService.Validators.Customers;
using Monno.Core.Commands;
using Monno.Core.Entities.Customers;
using Monno.Core.Repositories.Customers;
using Monno.Core.Services.Validations;
using Monno.Infra.Broker.EventSourcing;
using Monno.SharedKernel;
using Monno.SharedKernel.Validations;

namespace Monno.AppService.Commands.Customers.Handlers;

public sealed class CreateCustomerHandler : ICommandHandler<CreateCustomerCommand, Result<CreateCustomerResponse>>
{
    private readonly IValidationMessageService _validationService;
    private readonly ICustomerRepository _customerRepository;
    private readonly IStoreEvent _storeEvent;
    private readonly IMapper _mapper;

    public CreateCustomerHandler(IValidationMessageService validationService, ICustomerRepository customerRepository, IStoreEvent storeEvent, IMapper mapper)
    {
        _validationService = validationService;
        _customerRepository = customerRepository;
        _storeEvent = storeEvent;
        _mapper = mapper;
    }

    public async Task<Result<CreateCustomerResponse>> HandleAsync(CreateCustomerCommand command)
    {
        command.NotNull(nameof(command));

        var validator = new CreateCustomerValidator(_validationService, _customerRepository);
        var validationResult = await validator.ValidateCommandAsync(command);

        if (validationResult.IsFailure)
            return Result.Fail<CreateCustomerResponse>(validationResult.Errors)!;

        var customer = new Customer();
        customer.Create(command.Email, command.FirstName, command.LastName);

        await _customerRepository.AddAsync(customer);
        
        var changes = customer.GetChanges();

        foreach (var change in changes)
            await _storeEvent.SaveEventAsync(change, customer.Id, customer.Id);

        var response = _mapper.Map<CreateCustomerResponse>(customer);

        return Result.Ok(response);
    }
}