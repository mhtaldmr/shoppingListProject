using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Domain.Entities;
using ShoppingList.Infrastructure.Persistence.DbContext;

namespace ShoppingList.Infrastructure.Repositories
{
    internal class ItemRepository : Repository<Item>, IItemRepository
    {
        private readonly ShoppingListDbContext _context;

        public ItemRepository(ShoppingListDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetItemsByListId(int id) 
            => await _context.Items.Where(x => x.ListId == id).ToListAsync();
    }
}
