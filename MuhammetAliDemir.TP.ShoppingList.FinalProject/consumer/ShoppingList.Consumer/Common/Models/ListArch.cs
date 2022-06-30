namespace ShoppingList.Consumer.Common.Models
{
    public class ListArch : BaseArch
    {
        public string? Title { get; set; }
        public int? CategoryId { get; set; } = 1;
        public string? UserId { get; set; }
        public bool? IsCompleted { get; set; } = false;
        public DateTime? CompletedAt { get; set; }
        public virtual ICollection<ItemArch>? Items { get; set; }
    }
}
