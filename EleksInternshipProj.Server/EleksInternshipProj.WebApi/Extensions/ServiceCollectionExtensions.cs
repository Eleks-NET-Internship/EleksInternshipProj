using EleksInternshipProj.Application.Services;
using EleksInternshipProj.Application.Services.Imp;
using EleksInternshipProj.Infrastructure.Repositories;
using EleksInternsipProj.Domain.Abstractions;
using EleksInternsipProj.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EleksInternshipProj.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // DI for services here

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
