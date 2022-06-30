namespace ShoppingList.Consumer.Common.Models
{
    public class ShoppingListDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ListsCollectionName { get; set; } = null!;
    }
}
