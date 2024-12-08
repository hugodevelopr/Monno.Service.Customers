using Monno.AppService.Responses.Customers;
using Monno.Core.Commands;
using Monno.Core.Dtos;
using Monno.SharedKernel;

namespace Monno.AppService.Commands.Customers;

public class CreateCustomerCommand : ICommand<Result<CreateCustomerResponse>>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public UserContextDto UserContext { get; set; } = new();

}