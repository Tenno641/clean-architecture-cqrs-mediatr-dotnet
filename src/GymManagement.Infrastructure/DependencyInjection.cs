using System.Text;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Common.Interfaces;
using GymManagement.Domain.Users;
using GymManagement.Infrastructure.Authentication;
using GymManagement.Infrastructure.Authentication.TokenGenerators;
using GymManagement.Infrastructure.Common.Persistence;
using GymManagement.Infrastructure.Gyms.Persistence;
using GymManagement.Infrastructure.Rooms.Persistence;
using GymManagement.Infrastructure.Sessions.Persistence;
using GymManagement.Infrastructure.Subscriptions.Persistence;
using GymManagement.Infrastructure.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GymManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
        services.AddScoped<IGymsRepository, GymsRepository>();
        services.AddScoped<IRoomsRepository, RoomRepository>();
        services.AddScoped<ISessionsRepository, SessionsRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<GymDbContext>());

        services.AddDbContext<GymDbContext>();

        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
       JwtOptions jwtOptions = new JwtOptions
       {
          Issuer = configuration["Jwt:Issuer"],
          Audience = configuration["Jwt:Audience"],
          ExpirationTimeInSeconds = int.TryParse(configuration["Jwt:ExpirationTimeInSeconds"], out int expirationTime) ? expirationTime : 0,
          Secret = configuration["Jwt:Secret"]
       };

       services.AddSingleton(Options.Create(jwtOptions));

       services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidAudience = jwtOptions.Audience,
                   ValidateAudience = false,
                   ValidIssuer = jwtOptions.Issuer,
                   ValidateIssuer = false,
                   ValidateIssuerSigningKey = true,
                   ValidateLifetime = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret!))
               };
           });

       return services;
    }
}