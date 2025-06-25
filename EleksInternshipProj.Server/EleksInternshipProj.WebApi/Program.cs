using Microsoft.EntityFrameworkCore;

using EleksInternshipProj.Infrastructure.Data;
using EleksInternshipProj.WebApi.Extensions;
using EleksInternshipProj.Infrastructure.Hubs;

namespace EleksInternshipProj.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            // GET Connection String
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            
            // Initialize the database
            builder.Services.AddEntityFrameworkNpgsql().AddDbContext<NavchaykoDbContext>(
                options => options.UseNpgsql(connectionString));
            
            // Add services to the container.
            builder.Services.AddControllers();

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", policy =>
                {
                    policy.WithOrigins("https://localhost:4200")
                          .AllowCredentials()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // From extensions
            builder.Services.AddApplicationServices();
            builder.Services.AddRepositories();
            builder.Services.AddHostedServices();
            builder.Services.ConfigureAuth(builder.Configuration);
            builder.Services.AddAppConfiguration(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors("AllowAngularApp");

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.MapHub<NotificationHub>("/hubs/notifications")
                .RequireAuthorization();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
