using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Monno.Core.Entities.Customers;
using Monno.Core.Repositories.Customers;
using Serilog;

namespace Monno.Infra.Repository.Data.Customers;

public class CustomerRepository : BaseRepository, ICustomerRepository
{
    /// <inheritdoc />
    public CustomerRepository(IConfiguration configuration) 
        : base(configuration)
    {
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        await using var conn = new SqlConnection(ConnectionString);

        try
        {
            const string query = """
                                    SELECT 
                                        Id,
                                        Email
                                    FROM Customer.Customer
                                    WHERE 
                                        Email = @email
                                 """;

            await conn.OpenAsync();
            return await conn.QueryFirstOrDefaultAsync<Customer>(query, new { email });
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while getting customer by email");
            throw;
        }
        finally
        {
            await conn.CloseAsync();
        }
    }

    public async Task AddAsync(Customer customer)
    {
        await using var conn = new SqlConnection(ConnectionString);

        try
        {
            const string query = """

                                 """;

            await conn.OpenAsync();
            await conn.ExecuteAsync(query, customer);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while adding customer");
            throw;
        }
        finally
        {
            await conn.CloseAsync();
        }
    }
}