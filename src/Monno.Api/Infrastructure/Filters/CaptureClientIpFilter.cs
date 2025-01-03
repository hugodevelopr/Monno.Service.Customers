using Microsoft.AspNetCore.Mvc.Filters;

namespace Monno.Api.Infrastructure.Filters;

public class CaptureClientIpFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var httpContext = context.HttpContext;

        var clientIp = 
            httpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var header) ? 
                header.FirstOrDefault() : 
                httpContext.Connection.RemoteIpAddress?.ToString();

        if (clientIp == "::1")
            clientIp = "127.0.0.1";

        httpContext.Items.Add("ClientIp", clientIp);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}