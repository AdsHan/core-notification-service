namespace SNR.MessageBus.Integration
{
    public class UserCreatedIntegrationEvent : Event
    {

        public UserCreatedIntegrationEvent(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; set; }
        public string Email { get; set; }

    }
}