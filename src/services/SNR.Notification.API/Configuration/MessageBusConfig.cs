using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SNR.MessageBus;
using SNR.Notification.API.Application.Messages.ConsumersBus;

namespace SNR.Notification.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetConnectionString("RabbitMQCs")).AddHostedService<UserCreatedConsumer>();
        }
    }
}