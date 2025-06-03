using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using EleksInternshipProj.Infrastructure.Data;
using EleksInternshipProj.WebApi.Extensions;

namespace EleksInternshipProj.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // GET Connection String
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            
            // Initialize the database
            builder.Services.AddEntityFrameworkNpgsql().AddDbContext<NavchaykoDbContext>(
                options => options.UseNpgsql(connectionString));
            
            // Add services to the container.
            builder.Services.AddControllers();

            // From extensions
            builder.Services.AddApplicationServices();
            builder.Services.AddRepositories();
            builder.Services.ConfigureAuth(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
