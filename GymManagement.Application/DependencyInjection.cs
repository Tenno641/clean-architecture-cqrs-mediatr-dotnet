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
        });

        return services;
    }
}