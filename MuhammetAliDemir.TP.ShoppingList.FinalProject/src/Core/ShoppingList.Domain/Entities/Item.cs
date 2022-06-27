using ShoppingList.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingList.Domain.Entities
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; }
        public bool IsChecked { get; set; } = false;

        public int ListId { get; set; }
        [ForeignKey("ListId")]
        public List List { get; set; }

        public int UoMId { get; set; }
        [ForeignKey("UoMId")]
        public UnitOfMaterial UoM { get; set; }
    }
}