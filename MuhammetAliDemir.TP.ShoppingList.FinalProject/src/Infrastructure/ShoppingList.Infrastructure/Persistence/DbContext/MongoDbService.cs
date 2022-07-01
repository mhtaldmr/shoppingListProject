using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShoppingList.Application.Interfaces.DbContext;
using ShoppingList.Domain.Common;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Infrastructure.Persistence.DbContext
{
    public class MongoDbService : IMongoDbService
    {
        private readonly IMongoCollection<ListArch> _listCollection;
        public MongoDbService(IOptions<MongoDbSettings> options)
        {
            var mongoclient = new MongoClient(options.Value.ConnectionString);
            var mongoDatabase = mongoclient.GetDatabase(options.Value.DatabaseName);
            _listCollection = mongoDatabase.GetCollection<ListArch>(options.Value.ListsCollectionName);
        }

        public async Task<List<ListArch>> GetAsync() =>
            await _listCollection.Find(_ => true).ToListAsync();

        public async Task<ListArch?> GetAsync(string id) =>
            await _listCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ListArch newListArch) =>
            await _listCollection.InsertOneAsync(newListArch);

        public async Task UpdateAsync(string id, ListArch updatedListArch) =>
            await _listCollection.ReplaceOneAsync(x => x.Id == id, updatedListArch);

        public async Task RemoveAsync(string id) =>
            await _listCollection.DeleteOneAsync(x => x.Id == id);
    }
}
