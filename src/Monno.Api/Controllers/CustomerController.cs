using Microsoft.AspNetCore.Mvc;
using Monno.Api.Infrastructure.Controllers;
using Monno.AppService.Commands.Customers;
using Monno.Core.Commands;

namespace Monno.Api.Controllers;


[ApiController]
[Route("api/customers")]
public class CustomerController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;

    /// <inheritdoc />
    public CustomerController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var result = await _commandDispatcher.DispatchAsync(command);
        return await Created(result);
    }

    [HttpPost("{customerId:guid}/documents")]
    public async Task<IActionResult> ReceiveDocument([FromRoute] Guid customerId, [FromBody] ReceiveDocumentCommand command)
    {
        command.CustomerId = customerId;

        var result = await _commandDispatcher.DispatchAsync(command);
        return Ok(result);
    }
}