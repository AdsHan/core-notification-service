using MediatR;
using SNR.Core.Commands;
using SNR.Core.Communication;
using SNR.Notification.API.Data.Repository;
using SNR.Notification.API.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SNR.Notification.API.Application.Messages.Commands.NotificationCommand
{
    public class NotificationCommandHandler : CommandHandler,
        IRequestHandler<UserCreatedNotificationCommand, BaseResult>

    {
        private readonly INotificationService _notificationService;
        private readonly IMailRepository _mailRepository;

        public NotificationCommandHandler(INotificationService notificationService, IMailRepository mailRepository)
        {
            _notificationService = notificationService;
            _mailRepository = mailRepository;
        }

        public async Task<BaseResult> Handle(UserCreatedNotificationCommand command, CancellationToken cancellationToken)
        {
            if (!command.Validate()) return command.BaseResult;

            var template = await _mailRepository.GetTemplate("user-created");

            var subject = string.Format(template.Subject, command.Name);
            var content = string.Format(template.Content, command.Name);

            await _notificationService.SendAsync(subject, content, command.Email, command.Name);

            return BaseResult;
        }

    }

}