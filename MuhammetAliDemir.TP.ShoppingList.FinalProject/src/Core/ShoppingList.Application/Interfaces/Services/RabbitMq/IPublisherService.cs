using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Interfaces.Services.RabbitMq
{
    public interface IPublisherService
    {
        void Publish(List list, string queueName, string routingKey);
    }
}
