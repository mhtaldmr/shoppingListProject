using ShoppingList.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Domain.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
