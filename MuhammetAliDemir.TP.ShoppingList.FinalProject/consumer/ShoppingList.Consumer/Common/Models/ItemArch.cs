namespace ShoppingList.Consumer.Common.Models
{
    public class ItemArch
    {
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public int? Quantity { get; set; }
        public int? ListId { get; set; }
        public int? UoMId { get; set; }
        public bool? IsChecked { get; set; }
    }
}