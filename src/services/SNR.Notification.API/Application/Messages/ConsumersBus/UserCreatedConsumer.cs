using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using SNR.Core.Mediator;
using SNR.MessageBus;
using SNR.MessageBus.Integration;
using SNR.Notification.API.Application.Messages.Commands.NotificationCommand;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SNR.Notification.API.Application.Messages.ConsumersBus
{
    public class UserCreatedConsumer : BackgroundService, IUserCreatedConsumer
    {

        private readonly IMessageBusService _messageBusService;
        private readonly IServiceProvider _serviceProvider;

        public UserCreatedConsumer(IMessageBusService messageBusService, IServiceProvider serviceProvider)
        {
            _messageBusService = messageBusService;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageBusService.Subscribe(QueueTypes.NOTIFICATION_USER_CREATED, RegisterConsumer);
            return Task.CompletedTask;
        }

        public void RegisterConsumer(BasicDeliverEventArgs message)
        {
            var byteArray = message.Body.ToArray();
            var messageString = Encoding.UTF8.GetString(byteArray);
            var user = JsonConvert.DeserializeObject<UserCreatedIntegrationEvent>(messageString);

            using (var scope = _serviceProvider.CreateScope())
            {
                var command = new UserCreatedNotificationCommand(user.Name, user.Email);
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                var t = Task.Run(() => mediator.SendCommand(command));
                t.Wait();
            }
        }
    }
}
