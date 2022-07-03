namespace ShoppingList.Client.Data.ListResponse
{
    public class ListItemResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int UoMId { get; set; }
        public bool IsChecked { get; set; } = false;
    }
}