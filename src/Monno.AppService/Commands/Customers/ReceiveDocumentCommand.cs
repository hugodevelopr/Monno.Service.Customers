using System.Text.Json.Serialization;
using Monno.AppService.Responses.Customers;
using Monno.Core.Commands;
using Monno.Core.Dtos;
using Monno.SharedKernel;

namespace Monno.AppService.Commands.Customers;

public class ReceiveDocumentCommand : ICommand<Result<ReceiveDocumentResponse>>
{
    public Stream DocumentFile { get; set; } = default!;
    public DocumentDto Document { get; set; } = new();
    public AddressDto Address { get; set; } = new();

    [JsonIgnore]
    public Guid CustomerId { get; set; }
}