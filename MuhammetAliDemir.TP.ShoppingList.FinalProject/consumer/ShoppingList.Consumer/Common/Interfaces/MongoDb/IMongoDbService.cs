using ShoppingList.Consumer.Common.Models;

namespace ShoppingList.Consumer.Common.Interfaces.MongoDb
{
    public interface IMongoDbService
    {
        Task<List<ListArch>> GetAsync();
        Task<ListArch> GetAsync(string id);
        Task CreateAsync(ListArch newListArch);
        Task UpdateAsync(string id, ListArch updatedListArch);
        Task RemoveAsync(string id);
    }
}
