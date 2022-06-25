using ShoppingList.Domain.Common;

namespace ShoppingList.Domain.Entities
{
    public class List : BaseEntity
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
