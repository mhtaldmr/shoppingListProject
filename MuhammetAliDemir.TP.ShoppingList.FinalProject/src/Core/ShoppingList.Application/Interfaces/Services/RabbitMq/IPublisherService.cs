using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Application.Interfaces.Services.RabbitMq
{
    public interface IPublisherService
    {
        void Publish(GetListResponseMessage list, string queueName, string routingKey);
    }
}
