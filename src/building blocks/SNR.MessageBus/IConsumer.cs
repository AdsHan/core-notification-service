using RabbitMQ.Client.Events;

namespace SNR.MessageBus
{
    public interface IConsumer
    {
        void RegisterConsumer(BasicDeliverEventArgs message);
    }
}
