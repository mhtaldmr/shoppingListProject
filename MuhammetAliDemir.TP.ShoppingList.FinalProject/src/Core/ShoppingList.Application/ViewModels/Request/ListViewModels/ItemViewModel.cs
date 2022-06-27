namespace ShoppingList.Application.ViewModels.Request.ListViewModels
{
    public class ItemViewModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; } = 0;
        public int UoMId { get; set; } = 1;
        public bool IsChecked { get; set; } = false;
    }
}