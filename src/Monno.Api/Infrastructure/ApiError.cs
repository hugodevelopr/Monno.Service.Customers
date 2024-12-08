namespace Monno.Api.Infrastructure;

public class ApiError(string? message, string? detail)
{
    public string? Message { get; private set; } = message;
    public string? Detail { get; private set; } = detail;
}