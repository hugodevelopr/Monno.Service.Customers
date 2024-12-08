using Microsoft.AspNetCore.Mvc;
using Monno.SharedKernel;

namespace Monno.Api.Infrastructure.Controllers;

[Produces("application/json")]
public class BaseController : ControllerBase
{
    protected IActionResult Ok(string code) => base.Ok(Envelope.Ok(code));
    protected IActionResult Ok<T>(string code, T result) => base.Ok(Envelope.Ok(code, result));

    protected async Task<IActionResult> Created<T>(SharedKernel.Result<T> result)
    {
        result ??= default!;

        if (result.IsSuccess)
        {
            return await Task.FromResult(Created(string.Empty, result.Value));
        }

        return await Task.FromResult(Error("FAILED", result.Errors));
    }

    protected IActionResult Error(string code, IList<Error> errors) => Ok(Envelope.Error(code, errors));
    protected IActionResult Error(string code, Error error) => Error(code, new List<Error>() { error });
}