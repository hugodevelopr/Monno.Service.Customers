namespace Monno.Core.Dtos;

public class DocumentDto
{
    public string Type { get; set; }  = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string? Issuer { get; set; }
    public string BirthDate { get; set; } = string.Empty;
}