using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Monno.Api.Infrastructure.Controllers;
using Monno.Api.Infrastructure.Requests;
using Monno.AppService.Commands.Customers;
using Monno.Core.Commands;

namespace Monno.Api.Controllers;

[ApiVersion("1.0")]
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
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> ReceiveDocument([FromRoute] Guid customerId, [FromBody] ReceiveDocumentRequest request)
    {
        using var ms = new MemoryStream();
        await request.File.CopyToAsync(ms);

        ms.Position = 0;

        var command = new ReceiveDocumentCommand()
        {
            DocumentFile = ms,
            CustomerId = customerId,
        };

        var result = await _commandDispatcher.DispatchAsync(command);
        return Ok(result);
    }
}