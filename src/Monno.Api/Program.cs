using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Figgle;
using Monno.Api.Infrastructure;
using Monno.Api.Infrastructure.Settings;
using Monno.Infra.Repository.Common;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine(FiggleFonts.Doom.Render("Monno.Service.Customers"));

var keyVaultName = builder.Configuration["KeyVaultName"];
if (!string.IsNullOrEmpty(keyVaultName))
{
    var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net/");
    builder.Configuration.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());

    builder.Services.AddSingleton(_ => new SecretClient(keyVaultUri, new DefaultAzureCredential()));
}

builder.Logging.AddOpenTelemetry(builder.Configuration);

builder.Services
    .AddMapper()
    .AddFilters()
    .AddVersioning()
    .AddSwagger(builder.Configuration)
    .AddAuthentication(builder.Configuration);

builder.Services
    .AddModules()
    .AddJobs();

builder.Services.AddEntityFramework(builder.Configuration);

var app = builder.Build();

var keycloakSettings = app.Configuration.GetSection("Keycloak").Get<KeycloakSettings>()!;
var keyvaultService = app.Services.GetService<SecretClient>();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

SeedData.Initialize(services);

app.UseOpenApi();
app.UseSwaggerUi(options =>
{
    options.OAuth2Client = new OAuth2ClientSettings()
    {
        AppName = "Monno Service Customers",
        ClientId = keycloakSettings.ClientId,
        ClientSecret = (keyvaultService!.GetSecret("MonnoCustomers-Keycloak-ClientSecret")).Value.Value
    };
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();