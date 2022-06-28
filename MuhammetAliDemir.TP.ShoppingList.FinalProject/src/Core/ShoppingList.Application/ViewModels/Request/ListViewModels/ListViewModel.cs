using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Application.ViewModels.Request.ListViewModels
{
    public class ListViewModel
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public int CategoryId { get; set; } = 1;
        public ICollection<ListItemViewModel> ListItems { get; set; }
    }
}