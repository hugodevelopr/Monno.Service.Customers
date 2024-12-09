using System.Text.Json.Serialization;

namespace Monno.Core.Dtos;

public class UserContextDto
{
    public string IpAddress { get; set; } = string.Empty;
}