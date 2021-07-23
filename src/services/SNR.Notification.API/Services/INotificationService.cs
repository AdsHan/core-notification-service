using System.Threading.Tasks;

namespace SNR.Notification.API.Services
{
    public interface INotificationService
    {
        Task SendAsync(string subject, string content, string toEmail, string toName);
    }
}