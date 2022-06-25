using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Application.ViewModels.Request.ListViewModels
{
    public class ListCreateViewModel
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
    }
}