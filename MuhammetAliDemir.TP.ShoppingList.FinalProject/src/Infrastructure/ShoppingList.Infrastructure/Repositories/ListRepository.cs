using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.Extensions;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Request.FilterViewModel;
using ShoppingList.Application.ViewModels.Response.MainResponse;
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

        public async Task<PaginationResponse<List>> GetAllUsersWithFilter(FilterViewModel filter)
        {
            var query = _context.Lists.Include(l => l.Items).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(q => q.Title.Contains(filter.Title));

            if (filter.CategoryId >= 1 && filter.CategoryId <= 5)
                query = query.Where(q => q.CategoryId == filter.CategoryId);

            if (filter.CreatedAt.HasValue)
                query = query.Where(q => q.CreatedAt > filter.CreatedAt.Value);

            if (filter.CompletedAt.HasValue)
                query = query.Where(q => q.CompletedAt > filter.CompletedAt.Value);

            return await query.PaginateListAsync(filter.PageSize, filter.Page);
        }
    }
}