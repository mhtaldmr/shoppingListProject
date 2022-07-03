using ShoppingList.Application.ViewModels.Request.ListViewModels;

namespace ShoppingList.Application.ViewModels.Response.ListResponses
{
    public class GetListResponse : ListResponse
    {
        public int Id { get; set; }
    }

    public class GetListResponseMessage : ListResponse
    {
        public string UserId { get; set; }
    }

    public class ListResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<ListItemResponse> Items { get; set; }
    }
}