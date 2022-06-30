using RabbitMQ.Client;

namespace ShoppingList.Consumer.Common.Interfaces.RabbitMq
{
    public interface IRabbitMqConnection
    {
        IConnection GetRabbitMqConnection();
    }
}
