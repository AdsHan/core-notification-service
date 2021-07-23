
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SNR.Notification.API.Services;

namespace SNR.Notification.API.Configuration
{
    public static class NotificationConfiguration
    {
        public static IServiceCollection AddNotificationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Esta vida útil funciona melhor para serviços leves e sem estado
            services.AddTransient<INotificationService, NotificationService>();

            return services;
        }
    }
}