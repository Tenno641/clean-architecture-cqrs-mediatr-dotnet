using System.Text.Json.Serialization;
using GymManagement.Api.Services;
using GymManagement.Application;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Infrastructure;
using GymManagement.Infrastructure.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false));
});

builder.Services.AddApplication(builder.Configuration["mediator"]!);

builder.Services.AddInfrastructure();

builder.Services.AddProblemDetails();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(builder.Configuration);

builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

app.UseEventualConsistencyMiddleware();

app.UseExceptionHandler();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();