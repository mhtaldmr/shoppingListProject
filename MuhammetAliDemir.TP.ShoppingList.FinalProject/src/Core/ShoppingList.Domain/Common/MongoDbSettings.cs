namespace ShoppingList.Domain.Common
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ListsCollectionName { get; set; } = null!;
    }
}
