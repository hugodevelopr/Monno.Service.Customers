using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Monno.Api.Infrastructure.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var message = context.Exception.Message;
        var stackTrace = context.Exception.StackTrace;

        var error = new ApiError(message, stackTrace);

        context.Result = new ObjectResult(error)
        {
            StatusCode = 500
        };
    }
}