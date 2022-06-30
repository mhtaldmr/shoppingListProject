using ShoppingList.Consumer;
using ShoppingList.Consumer.Common.Interfaces.RabbitMq;
using ShoppingList.Consumer.Services.RabbitMq;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddTransient<IRabbitMqConnection, RabbitMqConnection>();
        services.AddTransient<IConsumerService, ConsumerService>();
    })
    .Build();

//Calling the consumer service.
var _consumer = host.Services.GetRequiredService<IConsumerService>();

//Giving the neccessary context
//IsAcnowledge can be made true. Therefore, messages will be automatically acknowledged.
_consumer.Consume(queueName: "direct.email", IsAcknowledgeAuto: false);


await host.RunAsync();
