using ShoppingList.Application.ViewModels.Request.ListViewModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingList.Application.ViewModels.Response.ListResponse
{
    public class GetListByIdResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime? CompletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<ListItemViewModel> ListItems { get; set; }
    }
}