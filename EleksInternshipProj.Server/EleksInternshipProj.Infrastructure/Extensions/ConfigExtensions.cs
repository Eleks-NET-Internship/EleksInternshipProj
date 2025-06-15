using Microsoft.Extensions.Configuration;

namespace EleksInternshipProj.Infrastructure.Extensions
{
    public static class ConfigExtensions
    {
        public static string GetRequiredConfig(this IConfiguration configuration, string section, string key)
        {
            string? configString = configuration.GetSection(section)[key];
            if (string.IsNullOrWhiteSpace(configString))
            {
                throw new InvalidOperationException($"Missing or empty config value: {section}:{key}");
            }
            return configString;
        }
        public static string GetRequiredConfig(this IConfiguration configuration, string key)
        {
            string? configString = configuration[key];
            if (string.IsNullOrWhiteSpace(configString))
            {
                throw new InvalidOperationException($"Missing or empty config value: {key}");
            }
            return configString;
        }
    }
}