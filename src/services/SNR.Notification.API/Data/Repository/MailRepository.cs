using MongoDB.Driver;
using SNR.Notification.API.Data.DTO;
using System.Threading.Tasks;

namespace SNR.Notification.API.Data.Repository
{
    public class MailRepository : IMailRepository
    {

        private readonly IMongoCollection<EmailTemplateDTO> _collection;

        public MailRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<EmailTemplateDTO>("email-templates");
        }

        public async Task<EmailTemplateDTO> GetTemplate(string @event)
        {
            return await _collection.Find(c => c.Event == @event).SingleOrDefaultAsync();
        }

    }
}