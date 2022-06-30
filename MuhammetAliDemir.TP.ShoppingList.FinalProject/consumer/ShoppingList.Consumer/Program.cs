using ShoppingList.Consumer.Common.Interfaces.MongoDb;
using ShoppingList.Consumer.Common.Interfaces.RabbitMq;
using ShoppingList.Consumer.Common.Models;
using ShoppingList.Consumer.Services.MongoDbservice;
using ShoppingList.Consumer.Services.RabbitMq;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;

        //services.AddHostedService<Worker>();
        services.AddTransient<IRabbitMqConnection, RabbitMqConnection>();
        services.AddTransient<IConsumerService, ConsumerService>();
        services.AddSingleton<IMongoDbService, MongoDbService>();
        services.Configure<ShoppingListDatabaseSettings>(
            configuration.GetSection("ShoppingListMongoDb"));
    })
    .Build();

//Calling the consumer service.
var _consumer = host.Services.GetRequiredService<IConsumerService>();
var _listService = host.Services.GetRequiredService<IMongoDbService>();

ICollection<ItemArch> item;
var list = new ListArch()
{
    Title = "Newlist",
    Description = "Mongodb trials"
};

//await _listService.CreateAsync(list);

var x = _listService.GetAsync();
foreach(var y in x.Result)
Console.WriteLine(y.Id);

//Giving the neccessary context
//IsAcnowledge can be made true. Therefore, messages will be automatically acknowledged.
_consumer.Consume(queueName: "direct.email", IsAcknowledgeAuto: false);


await host.RunAsync();
