using RabbitMQ.Client;
using ShoppingList.Application.Interfaces.Services.RabbitMq;

namespace ShoppingList.Infrastructure.Services.RabbitMq
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        public IConnection GetRabbitMqConnection()
        {
            return new ConnectionFactory()
            {
                HostName = "localhost",
                VirtualHost = "/",
                Port = 5672,
                UserName = "guest",
                Password = "guest"

            }.CreateConnection();
        }
    }
}