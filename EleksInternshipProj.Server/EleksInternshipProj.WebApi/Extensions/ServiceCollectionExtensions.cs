using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

using EleksInternshipProj.Domain.Abstractions;
using EleksInternshipProj.Application.Services;
using EleksInternshipProj.Application.Services.Imp;
using EleksInternshipProj.Infrastructure.Repositories;

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

            return services;
        }

        public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
        {
            string googleClientId = configuration.GetSection("Google")["ClientId"] ?? throw new ArgumentNullException("Google:ClientId");
            string googleClientSecret = configuration.GetSection("Google")["ClientSecret"] ?? throw new ArgumentNullException("Google:ClientSecret");

            string jwtIssuer = configuration.GetSection("Jwt")["Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer");
            string jwtAudience = configuration.GetSection("Jwt")["Audience"] ?? throw new ArgumentNullException("Jwt:Audience");
            string jwtSecret = configuration.GetSection("Jwt")["Secret"] ?? throw new ArgumentNullException("Jwt:Secret");

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
    }
}
