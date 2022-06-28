using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Interfaces.Repositories
{
    public interface IListRepository : IRepository<List>
    {
        Task<IEnumerable<List>> GetAllListsWithItems();
        Task<List> GetListByIdWithItem(int id);
    }
}
