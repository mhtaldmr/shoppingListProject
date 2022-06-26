using ShoppingList.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingList.Domain.Entities
{
    public class ListItem : BaseEntity
    {
        public int Quantity { get; set; }
        public bool IsChecked { get; set; } = false;

        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        public int ListId { get; set; }
        [ForeignKey("ListId")]
        public List List { get; set; }

        public int UoMId { get; set; }
        [ForeignKey("UomId")]
        public UnitOfMaterial UoM { get; set; }
    }
}