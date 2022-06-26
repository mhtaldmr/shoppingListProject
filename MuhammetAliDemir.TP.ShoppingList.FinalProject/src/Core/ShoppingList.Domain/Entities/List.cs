using ShoppingList.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingList.Domain.Entities
{
    public class List : BaseEntity
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime? CompletedAt { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }


    }
}