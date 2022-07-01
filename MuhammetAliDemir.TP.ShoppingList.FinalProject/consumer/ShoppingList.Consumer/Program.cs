using ShoppingList.Consumer;
using ShoppingList.Consumer.Common.Interfaces.MongoDb;
using ShoppingList.Consumer.Common.Interfaces.RabbitMq;
using ShoppingList.Consumer.Common.Models;
using ShoppingList.Consumer.Services.MongoDb;
using ShoppingList.Consumer.Services.RabbitMq;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;

        //registering the interfaces
        services.AddTransient<IRabbitMqConnection, RabbitMqConnection>();
        services.AddTransient<IConsumerService, ConsumerService>();
        services.AddSingleton<IMongoDbService, MongoDbService>();

        //registering the mongodb configs
        services.Configure<ShoppingListDatabaseSettings>(
            configuration.GetSection("ShoppingListMongoDb"));

        //calling the worker background service
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
