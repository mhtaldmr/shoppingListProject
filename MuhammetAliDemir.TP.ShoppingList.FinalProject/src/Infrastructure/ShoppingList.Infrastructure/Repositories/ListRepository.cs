using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Domain.Entities;
using ShoppingList.Infrastructure.Persistence.DbContext;

namespace ShoppingList.Infrastructure.Repositories
{
    public class ListRepository : Repository<List>, IListRepository
    {
        public ListRepository(ShoppingListDbContext context) : base(context)
        {
        }
    }
}
