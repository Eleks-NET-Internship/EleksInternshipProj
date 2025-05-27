using EleksInternshipProj.Application.Services;
using EleksInternshipProj.Application.Services.Imp;
using EleksInternsipProj.Domain.Abstractions;
using EleksInternsipProj.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace EleksInternshipProj.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // DI for services here
            services.AddScoped<DbContext>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // DI for repositories here
            services.AddScoped<IUserRepository, UserRepository>();


            return services;
        }

    }
}
