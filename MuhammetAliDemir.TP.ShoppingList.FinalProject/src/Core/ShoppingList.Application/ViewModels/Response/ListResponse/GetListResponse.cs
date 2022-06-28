using ShoppingList.Application.ViewModels.Request.ListViewModels;

namespace ShoppingList.Application.ViewModels.Response.ListResponse
{
    public class GetListResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<ListItemViewModel> Items { get; set; }
    }
}