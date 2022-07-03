using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShoppingList.Domain.Entities
{
    public class ListArch
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string UserId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<ItemArch> Items { get; set; }
    }
}