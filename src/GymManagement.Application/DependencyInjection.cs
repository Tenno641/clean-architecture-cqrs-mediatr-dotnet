using FluentValidation;
using GymManagement.Application.Behaviors;
using GymManagement.Application.Gyms.Commands.CreateGym;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, string licenseKey)
    {
        services.AddMediatR(configuration =>
        {
            configuration.LicenseKey = licenseKey;
            configuration.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
        });

        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

        return services;
    }
}