using RabbitMQ.Client;
using ShoppingList.Application.Interfaces.Services.RabbitMq;
using ShoppingList.Application.ViewModels.Response.ListResponses;
using System.Text;
using System.Text.Json;

namespace ShoppingList.Infrastructure.Services.RabbitMq
{
    public class PublisherService : IPublisherService
    {
        private const string _exchangeName = "direct.list";
        private const string _exchangeType = "direct";
        private readonly IRabbitMqConnection _rabbitMqConnection;
        public PublisherService(IRabbitMqConnection rabbitMqConnection) => _rabbitMqConnection = rabbitMqConnection;

        public void Publish(GetListResponseMessage list, string queueName, string routingKey)
        {
            //Creating the RabbitMQ connection with Using Because it must close connection when it send the message!
            using var connection = _rabbitMqConnection.GetRabbitMqConnection();
            using var channel = connection.CreateModel();

            //Creating exchanges and queues
            channel.ExchangeDeclare(
                exchange: _exchangeName,
                type: _exchangeType,
                durable: false,
                autoDelete: false);

            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: true);

            //Binding the exchanges and queues created into eachother with routingkey
            channel.QueueBind(
                queue: queueName,
                exchange: _exchangeName,
                routingKey: routingKey);

            //User is an object, for that changing that into Bytes..
            var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(list));

            //At last, we are publishing the message.
            channel.BasicPublish(
                exchange: _exchangeName,
                routingKey: routingKey,
                basicProperties: null,
                body: messageBody);
        }
    }
}