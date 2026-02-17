using System.Text.Json.Serialization;
using GymManagement.Application;
using GymManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services
    .AddApplication(builder.Configuration["mediator"]!)
    .AddInfrastructure();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();