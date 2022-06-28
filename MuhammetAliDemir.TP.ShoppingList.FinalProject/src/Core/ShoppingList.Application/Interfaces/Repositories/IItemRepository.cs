using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Interfaces.Repositories
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<IEnumerable<Item>> GetItemsByListId(int id);
    }
}
