namespace ShoppingList.Domain.Entities
{
    public class ListCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ListId { get; set; }
        public List List { get; set; }
    }
}
