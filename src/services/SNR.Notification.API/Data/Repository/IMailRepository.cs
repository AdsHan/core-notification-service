using SNR.Notification.API.Data.DTO;
using System.Threading.Tasks;

namespace SNR.Notification.API.Data.Repository
{
    public interface IMailRepository
    {
        Task<EmailTemplateDTO> GetTemplate(string @event);
    }
}