namespace ShoppingList.Application.ViewModels.Request.FilterViewModel
{
    public class FilterViewModel
    {
        public int Page { get; set; }
        public byte PageSize { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string? Title { get; set; }
        public int CategoryId { get; set; }
    }
}