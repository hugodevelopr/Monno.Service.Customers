using Figgle;
using Monno.Api.Infrastructure;
using Monno.Infra.Repository.Common;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine(FiggleFonts.Doom.Render("Monno.Service.Customers"));

builder.Services.AddControllers();
builder.Services.AddFilters();

builder.Services.AddSwagger();

builder.Services
    .AddModules()
    .AddJobs();

builder.Services.AddEntityFramework(builder.Configuration);

var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

SeedData.Initialize(services);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();