using Azure.Identity;
using Figgle;
using Monno.Api.Infrastructure;
using Monno.Infra.Repository.Common;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine(FiggleFonts.Doom.Render("Monno.Service.Customers"));

var keyVaultName = builder.Configuration["KeyVaultName"];
if(!string.IsNullOrEmpty(keyVaultName))
{
    var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net/");
    builder.Configuration.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());
}

builder.Services.AddFilters();
builder.Services.AddSwagger();
builder.Services.AddMapper();

builder.Services
    .AddModules()
    .AddJobs();

builder.Services.AddEntityFramework(builder.Configuration);

var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

SeedData.Initialize(services);

app.MapOpenApi();
app.MapScalarApiReference();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "Monno.Service.Customers API");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();