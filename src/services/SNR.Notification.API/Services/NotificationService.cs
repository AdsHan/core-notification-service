using SNR.Core.Settings;
using System.Net;
using System.Net.Mail;
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
            var from = new MailAddress(_notificationSettings.FromEmail, _notificationSettings.FromName);
            var to = new MailAddress(toEmail, toName);

            var message = new MailMessage(from, to);

            message.Subject = subject;
            message.Body = content;
            message.IsBodyHtml = true;

            // No caso do smtp do Google lembrar:
            // 1) Habilitar a opção "acesso a app menos seguro" 
            // 2) Desabilitar verificação em duas etapas

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(_notificationSettings.FromEmail, "SuaSenhaSecreta"),
                EnableSsl = true
            };

            client.Send(message);

        }
    }
}