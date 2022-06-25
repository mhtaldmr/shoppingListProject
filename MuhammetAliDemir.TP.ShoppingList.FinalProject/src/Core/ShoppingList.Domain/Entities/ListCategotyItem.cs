namespace ShoppingList.Domain.Entities
{
    public class ListCategotyItem
    {
        public int ListCategoryId { get; set; }
        public ListCategory ListCategory { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int UnitOfMaterialId { get; set; }
        public UnitofMaterial UnitofMaterial { get; set; }
        public int Quantity { get; set; }
    }
}
