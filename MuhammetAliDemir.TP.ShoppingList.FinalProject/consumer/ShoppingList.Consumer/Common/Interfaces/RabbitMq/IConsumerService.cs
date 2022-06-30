namespace ShoppingList.Consumer.Common.Interfaces.RabbitMq
{
    public interface IConsumerService
    {
        void Consume(string queueName, bool IsAcknowledgeAuto);
    }
}
