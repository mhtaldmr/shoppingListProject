using ShoppingList.Domain.Common;

namespace ShoppingList.Domain.Entities
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
