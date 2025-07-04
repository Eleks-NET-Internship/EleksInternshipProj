using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Application.Services;
using EleksInternshipProj.Application.Services.Imp;
using EleksInternshipProj.Infrastructure.Repositories;
using EleksInternshipProj.Infrastructure.Extensions;
using EleksInternshipProj.Infrastructure.Services;
using EleksInternshipProj.Infrastructure.BackgroundTasks;
using EleksInternshipProj.Infrastructure.Configuration;

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
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<ISpaceService, SpaceService>();
            services.AddScoped<ITimetableService, TimetableService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationDeliveryService, SignalRDeliveryService>();
            services.AddScoped<ISpaceAuthService, SpaceAuthService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // DI for repositories here
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IMarkerRepository, MarkerRepository>();
            services.AddScoped<ISoloEventRepository, SoloEventRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ISpaceRepository, SpaceRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IUserSpaceRepository, UserSpaceRepository>();
            services.AddScoped<ITimetableRepository, TimetableRepository>();
            
            services.AddScoped<INoteRepository, NoteRepository>();

            return services;
        }

        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = configuration
                .GetSection("EmailSettings")
                .Get<EmailSettings>();

            if (emailSettings is null)
                throw new InvalidOperationException("EmailSettings configuration section is missing or invalid.");

            services.AddSingleton(emailSettings);

            return services;
        }


        public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
        {
            string googleClientId = configuration.GetRequiredConfig("Google", "ClientId");
            string googleClientSecret = configuration.GetRequiredConfig("Google", "ClientSecret");

            string jwtIssuer = configuration.GetRequiredConfig("Jwt", "Issuer");
            string jwtAudience = configuration.GetRequiredConfig("Jwt", "Audience");
            string jwtSecret = configuration.GetRequiredConfig("Jwt", "Secret");

            services.AddAuthorization();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddGoogle(options =>
            {
                options.ClientId = googleClientId;
                options.ClientSecret = googleClientSecret;
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                options.Events.OnRemoteFailure = context =>
                {
                    context.HandleResponse();
                    context.Response.Redirect("https://localhost:4200/login");
                    return Task.CompletedTask;
                };
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Enter JWT token (without 'Bearer' prefix)",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });

            return services;
        }

        public static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddSignalR();

            services.AddHostedService<TaskNotificationWorker>();
            return services;
        }
    }
}
