namespace Monno.Api.Infrastructure.Requests;

public class ReceiveDocumentRequest
{
    public IFormFile File { get; set; } = default!;
}