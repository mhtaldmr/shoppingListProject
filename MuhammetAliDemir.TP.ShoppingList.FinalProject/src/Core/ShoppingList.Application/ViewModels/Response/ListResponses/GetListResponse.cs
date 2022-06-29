using ShoppingList.Application.ViewModels.Request.ListViewModels;

namespace ShoppingList.Application.ViewModels.Response.ListResponses
{
    public class GetListResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<ListItemActionViewModel> Items { get; set; }
    }
}