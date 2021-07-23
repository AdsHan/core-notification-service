using SendGrid;
using SendGrid.Helpers.Mail;
using SNR.Core.Settings;
using System.Threading.Tasks;

namespace SNR.Notification.API.Services
{
    public class NotificationService : INotificationService
    {

        private readonly NotificationSettings _notificationSettings;

        public NotificationService(NotificationSettings notificationSettings)
        {
            _notificationSettings = notificationSettings;

        }

        public async Task SendAsync(string subject, string content, string toEmail, string toName)
        {
            var from = new EmailAddress(_notificationSettings.FromEmail, _notificationSettings.FromName);
            var to = new EmailAddress(toEmail, toName);

            var message = new SendGridMessage
            {
                From = from,
                Subject = subject
            };

            message.AddContent(MimeType.Html, content);
            message.AddTo(to);

            message.SetClickTracking(false, false);
            message.SetOpenTracking(false);
            message.SetGoogleAnalytics(false);
            message.SetSubscriptionTracking(false);

            //await _sendGridClient.SendEmailAsync(message);
        }
    }
}