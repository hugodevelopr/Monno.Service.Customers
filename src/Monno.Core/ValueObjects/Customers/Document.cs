using Monno.SharedKernel;

namespace Monno.Core.ValueObjects.Customers;

public sealed class Document : IEquatable<Document>
{
    private Document()
    {
    }

    public Document(DocumentType type, string number, string? issuer, string birthDate)
    {
        number.NotNullOrEmpty(nameof(number));
        birthDate.NotNullOrEmpty(nameof(birthDate));

        Type = type;
        Number = number;
        Issuer = issuer;
        BirthDate = birthDate;
    }

    public DocumentType Type { get; }
    public string Number { get; }
    public string? Issuer { get; }
    public string BirthDate { get; }

    public enum DocumentType
    {
        None = 0,
        SSN = 1,
        Passport = 2
    }

    public override string ToString() => $"{Type} - {Number}";

    public bool Equals(Document? other)
    {
        if(other == null) return false;

        return Type == other.Type &&
               string.Equals(Number, other.Number, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(Issuer, other.Issuer, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(BirthDate, other.BirthDate, StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object? obj) => Equals(obj as Document);

    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 17;
            hash = hash * 23 + Type.GetHashCode();
            hash = hash * 23 + (Number?.ToLowerInvariant().GetHashCode() ?? 0);
            hash = hash * 23 + (Issuer?.ToLowerInvariant().GetHashCode() ?? 0);
            hash = hash * 23 + (BirthDate?.ToLowerInvariant().GetHashCode() ?? 0);
            
            return hash;
        }
    }

    public static bool operator ==(Document? left, Document? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Document? left, Document? right)
    {
        return !Equals(left, right);
    }
}