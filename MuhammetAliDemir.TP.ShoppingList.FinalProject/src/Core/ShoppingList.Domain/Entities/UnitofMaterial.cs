using ShoppingList.Domain.Common;

namespace ShoppingList.Domain.Entities
{
    public class UnitOfMaterial
    {
        public int Id { get; set; }
        public string UoMCode { get; set; }
        public string? Description { get; set; }
    }
}
