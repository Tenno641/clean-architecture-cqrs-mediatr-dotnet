using System.Text.Json.Serialization;
using GymManagement.Application;
using GymManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false));
});

builder.Services.AddApplication(builder.Configuration["mediator"]!);

builder.Services.AddInfrastructure();

builder.Services.AddProblemDetails();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseEventualConsistencyMiddleware();

app.UseExceptionHandler();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();