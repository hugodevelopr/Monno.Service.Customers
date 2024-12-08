using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Monno.Core.Entities.Messages;
using Monno.Infra.Repository.Contexts;
using Newtonsoft.Json;

namespace Monno.Infra.Repository.Common;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new MonnoDbContext(serviceProvider.GetRequiredService<DbContextOptions<MonnoDbContext>>());

        if (!context.ValidationMessages.Any())
        {
            var file = Path.Combine(AppContext.BaseDirectory, "Infrastructure", "Settings", "Seed", "validation-message.json");

            if (File.Exists(file))
            {
                var json = File.ReadAllText(file);
                var messages = JsonConvert.DeserializeObject<List<ValidationMessage>>(json)!;

                foreach (var item in messages)
                {
                    var message = new ValidationMessage(item.Keyword, item.ErrorCode, item.Message);
                    
                    context.ValidationMessages.Add(message);
                    context.SaveChanges();
                }
            }
        }
    }
}