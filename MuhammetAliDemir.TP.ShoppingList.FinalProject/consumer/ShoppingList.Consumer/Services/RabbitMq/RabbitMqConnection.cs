using RabbitMQ.Client;
using ShoppingList.Consumer.Common.Interfaces.RabbitMq;

namespace ShoppingList.Consumer.Services.RabbitMq
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
