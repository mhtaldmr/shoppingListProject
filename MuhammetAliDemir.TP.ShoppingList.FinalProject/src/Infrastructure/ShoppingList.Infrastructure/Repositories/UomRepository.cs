using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Domain.Entities;
using ShoppingList.Infrastructure.Persistence.DbContext;

namespace ShoppingList.Infrastructure.Repositories
{
    public class UomRepository : Repository<UnitOfMaterial>, IUomRepository
    {
        public UomRepository(ShoppingListDbContext context) : base(context)
        {
        }
    }
}
