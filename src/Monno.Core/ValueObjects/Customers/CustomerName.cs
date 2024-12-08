using Monno.SharedKernel;

namespace Monno.Core.ValueObjects.Customers;

public sealed class CustomerName : IEquatable<CustomerName>
{
    public string FirstName { get; }
    public string LastName { get; }
    public string FullName => $"{FirstName} {LastName}";

    public CustomerName(string firstName, string lastName)
    {
        firstName.NotNullOrEmpty(nameof(firstName));
        lastName.NotNullOrEmpty(nameof(lastName));

        FirstName = firstName;
        LastName = lastName;
    }


    public bool Equals(CustomerName? other)
    {
        if (other == null) return false;

        return string.Equals(FirstName, other.FirstName, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(LastName, other.LastName, StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString() => FullName;
    public override bool Equals(object? obj) => Equals(obj as CustomerName);

    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 17;
            hash = hash * 23 + (FirstName?.ToLowerInvariant().GetHashCode() ?? 0);
            hash = hash * 23 + (LastName?.ToLowerInvariant().GetHashCode() ?? 0);

            return hash;
        }
    }

    public static bool operator ==(CustomerName? left, CustomerName? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(CustomerName? left, CustomerName? right)
    {
        return !Equals(left, right);
    }
}