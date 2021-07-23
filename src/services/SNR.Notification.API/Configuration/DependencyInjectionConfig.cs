using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SNR.Core.Mediator;
using SNR.Notification.API.Application.Messages.Commands.NotificationCommand;
using SNR.Notification.API.Data.Repository;

namespace SNR.Notification.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddMediatR(typeof(UserCreatedNotificationCommand));

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IMailRepository, MailRepository>();

            return services;
        }
    }
}