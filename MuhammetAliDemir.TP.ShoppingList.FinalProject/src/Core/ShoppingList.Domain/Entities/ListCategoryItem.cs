namespace ShoppingList.Domain.Entities
{
    public class ListCategoryItem
    {
        public int ListCategoryId { get; set; }
        public ListCategory ListCategory { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int UnitOfMaterialId { get; set; }
        public UnitOfMaterial UnitOfMaterial { get; set; }
        public int Quantity { get; set; }
    }
}
