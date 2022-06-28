using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Domain.Entities;
using ShoppingList.Infrastructure.Persistence.DbContext;

namespace ShoppingList.Infrastructure.Repositories
{
    public class ListRepository : Repository<List>, IListRepository
    {
        private readonly ShoppingListDbContext _context;

        public ListRepository(ShoppingListDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<List>> GetAllListsWithItems() 
            => await _context.Lists.Include(l => l.Items).ToListAsync();

        public async Task<List> GetListByIdWithItem(int id) 
            => await _context.Lists.Include(l => l.Items).FirstOrDefaultAsync(z => z.Id == id);
    }
}
