using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Application.ViewModels.Request.ListViewModels
{
    public class ListViewModel : ListModel
    {
        public ICollection<ListItemViewModel> Items { get; set; }
    }

    public class ListCreateViewModel : ListModel
    {
        public ICollection<ListItemCreateViewModel> Items { get; set; }
    }

    public class ListModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; } = 1;
    }
}