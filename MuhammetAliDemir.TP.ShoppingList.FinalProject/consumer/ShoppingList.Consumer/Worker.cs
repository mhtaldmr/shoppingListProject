using RabbitMQ.Client;
using ShoppingList.Consumer.Common.Interfaces.RabbitMq;

namespace ShoppingList.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly IConsumerService _consumer;
        private readonly IRabbitMqConnection _rabbitMqConnection;

        public Worker(IConsumerService consumer, IRabbitMqConnection rabbitMqConnection)
        {
            _consumer = consumer;
            _rabbitMqConnection = rabbitMqConnection;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //connecting the rabbitmq server
            var connection = _rabbitMqConnection.GetRabbitMqConnection();
            var channel = connection.CreateModel();

            //Creating the listening queue
            channel.QueueDeclare(
                queue: "direct.list",
                durable: false,
                exclusive: false,
                autoDelete: true);

            while (!stoppingToken.IsCancellationRequested)
            {
                ////Giving the neccessary context
                ////IsAcnowledge can be made true. Therefore, messages will be automatically acknowledged.
                _consumer.Consume(queueName: "direct.list", IsAcknowledgeAuto: false, channel);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}