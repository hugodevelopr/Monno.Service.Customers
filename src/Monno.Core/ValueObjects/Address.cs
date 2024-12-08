using Monno.SharedKernel;

namespace Monno.Core.ValueObjects;

public sealed class Address : IEquatable<Address>
{
    public string Country { get; }
    public string State { get; }
    public string City { get; }
    public string Street { get; }
    public string ZipCode { get; }

    public Address(string country, string state, string city, string street, string zipCode)
    {
        country.NotNullOrEmpty(nameof(country));
        state.NotNullOrEmpty(nameof(state));
        city.NotNullOrEmpty(nameof(city));
        street.NotNullOrEmpty(nameof(street));
        zipCode.NotNullOrEmpty(nameof(zipCode));

        Country = country;
        State = state;
        City = city;
        Street = street;
        ZipCode = zipCode;
    }

    public override string ToString() => $"{Street}, {City}, {State}, {Country}, {ZipCode}";

    public bool Equals(Address? other)
    {
        if (other == null) return false;

        return string.Equals(Country, other.Country, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(State, other.State, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(City, other.City, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(Street, other.Street, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(ZipCode, other.ZipCode, StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object? obj) => Equals(obj as Address);

    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 17;
            hash = hash * 23 + (Country?.ToLowerInvariant().GetHashCode() ?? 0);
            hash = hash * 23 + (State?.ToLowerInvariant().GetHashCode() ?? 0);
            hash = hash * 23 + (City?.ToLowerInvariant().GetHashCode() ?? 0);
            hash = hash * 23 + (Street?.ToLowerInvariant().GetHashCode() ?? 0);
            hash = hash * 23 + (ZipCode?.ToLowerInvariant().GetHashCode() ?? 0);
            
            return hash;
        }
    }

    public static bool operator ==(Address? left, Address? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Address? left, Address? right)
    {
        return !Equals(left, right);
    }
}