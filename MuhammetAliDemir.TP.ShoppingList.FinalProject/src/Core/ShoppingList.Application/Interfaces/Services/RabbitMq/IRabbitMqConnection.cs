using RabbitMQ.Client;

namespace ShoppingList.Application.Interfaces.Services.RabbitMq
{
    public interface IRabbitMqConnection
    {
        IConnection GetRabbitMqConnection();
    }
}
