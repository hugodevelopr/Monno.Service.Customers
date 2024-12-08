using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Monno.Core.Repositories.Messages;
using Serilog;

namespace Monno.Infra.Repository.Data.Messages;

public class ValidationMessageRepository : BaseRepository, IValidationMessageRepository
{
    /// <inheritdoc />
    public ValidationMessageRepository(IConfiguration configuration)
        : base(configuration)
    {
    }

    public async Task<(string Code, string Message)> GetMessageAsync(string keyword, string language)
    {
        await using var conn = new SqlConnection(ConnectionString);

        try
        {
            const string query = """
                                    SELECT 
                                        ErrorCode AS Code,
                                        Message
                                    FROM 
                                        Messages.ValidationMessage
                                    WHERE 
                                        Keyword = @keyword AND Language = @language
                                 """;

            await conn.OpenAsync();
            return await conn.QueryFirstOrDefaultAsync<(string, string)>(query,
                new
                {
                    keyword,
                    language
                });
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while getting validation message");
            throw;
        }
        finally
        {
            await conn.CloseAsync();
        }
    }
}