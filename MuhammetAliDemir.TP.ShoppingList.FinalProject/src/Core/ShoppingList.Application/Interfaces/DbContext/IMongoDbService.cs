using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Interfaces.DbContext
{
    public interface IMongoDbService
    {
        Task<List<ListArch>> GetAsync();
        Task<ListArch> GetAsync(string id);
        Task CreateAsync(ListArch ListArch);
        Task UpdateAsync(string id, ListArch ListArch);
        Task RemoveAsync(string id);
    }
}
