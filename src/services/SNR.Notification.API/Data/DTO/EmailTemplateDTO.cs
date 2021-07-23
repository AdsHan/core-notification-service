using System;

namespace SNR.Notification.API.Data.DTO
{
    public class EmailTemplateDTO
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Event { get; set; }
    }
}