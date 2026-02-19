using System.Text.Json.Serialization;
using GymManagement.Application;
using GymManagement.Infrastructure;
using GymManagement.Infrastructure.Common.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false));
});

builder.Services.AddApplication(builder.Configuration["mediator"]!);

builder.Services.AddInfrastructure();

var app = builder.Build();

using IServiceScope scope = app.Services.CreateScope();
GymDbContext context = scope.ServiceProvider.GetRequiredService<GymDbContext>();
context.Database.EnsureDeleted();
context.Database.EnsureCreated();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();