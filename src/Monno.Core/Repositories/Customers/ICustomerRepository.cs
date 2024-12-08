using Monno.Core.Entities.Customers;

namespace Monno.Core.Repositories.Customers;

public interface ICustomerRepository
{
    Task<Customer?> GetByEmailAsync(string email);
    Task AddAsync(Customer customer);
}