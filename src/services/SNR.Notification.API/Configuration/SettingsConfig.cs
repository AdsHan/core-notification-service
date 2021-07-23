using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SNR.Core.Settings;

namespace RTO.Auth.API.Configuration
{
    public static class SettingsConfig
    {
        public static IServiceCollection AddSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            var tokenSettings = new TokenSettings();
            new ConfigureFromConfigurationOptions<TokenSettings>(configuration.GetSection("Token")).Configure(tokenSettings);
            services.AddSingleton(tokenSettings);


            var connectionSettings = new ConnectionSettings();
            new ConfigureFromConfigurationOptions<ConnectionSettings>(configuration.GetSection("ConnectionStrings")).Configure(connectionSettings);
            services.AddSingleton(connectionSettings);


            var notificationSettings = new NotificationSettings();
            new ConfigureFromConfigurationOptions<NotificationSettings>(configuration.GetSection("Notification")).Configure(notificationSettings);
            services.AddSingleton(notificationSettings);

            return services;
        }

    }
}