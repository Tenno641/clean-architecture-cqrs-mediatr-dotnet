using GymManagement.Application.Common;
using GymManagement.Infrastructure.Subscriptions.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ISubscriptionsRepository, SubscriptionsRepository>();
        return services;
    }
}