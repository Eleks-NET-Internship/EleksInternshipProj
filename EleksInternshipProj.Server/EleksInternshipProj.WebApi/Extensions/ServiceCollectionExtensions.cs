using EleksInternshipProj.Application.Services;
using EleksInternshipProj.Application.Services.Imp;
using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace EleksInternshipProj.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // DI for services here
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IMarkerService, MarkerService>();
            services.AddScoped<ISoloEventService, SoloEventService>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // DI for repositories here
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IMarkerRepository, MarkerRepository>();
            services.AddScoped<ISoloEventRepository, SoloEventRepository>();
            return services;
        }

    }
}
