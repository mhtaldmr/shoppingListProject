using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.Extensions;
using ShoppingList.Application.Features.ListFeatures.Queries.GetAllByFilter;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Domain.Entities;
using ShoppingList.Infrastructure.Persistence.DbContext;

namespace ShoppingList.Infrastructure.Repositories
{
    public class ListRepository : Repository<List>, IListRepository
    {
        private readonly ShoppingListDbContext _context;
        private readonly UserManager<User> _userManager;

        public ListRepository(ShoppingListDbContext context, UserManager<User> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<List>> GetAllListsWithItems()
            => await _context.Lists.Include(l => l.Items).ToListAsync();
        public async Task<List> GetListByIdWithItem(int id)
            => await _context.Lists.Include(l => l.Items).FirstOrDefaultAsync(z => z.Id == id);


        public async Task<IEnumerable<List>> GetAllListsByUsers(string userId)
        {
            var existingUser = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            var role = await _userManager.GetRolesAsync(existingUser);
            if (role.Contains("admin"))
                return await _context.Lists.Include(l => l.Items).ToListAsync();
            return await _context.Lists.Include(l => l.Items).Where(u => u.UserId == userId).ToListAsync();
        }


        public async Task<PaginationResponse<List>> GetAllListsByFilter(GetAllByFilterQuery filter)
        {
            var existingUser = await _context.Users.Where(u => u.Id == filter.UserId).FirstOrDefaultAsync();
            var role = await _userManager.GetRolesAsync(existingUser);
            var query = _context.Lists.Include(l => l.Items).Where(u => u.UserId == filter.UserId).AsQueryable();

            if (role.Contains("admin"))
                query = _context.Lists.Include(l => l.Items).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(q => q.Title.ToLower().Contains(filter.Title.ToLower()));

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