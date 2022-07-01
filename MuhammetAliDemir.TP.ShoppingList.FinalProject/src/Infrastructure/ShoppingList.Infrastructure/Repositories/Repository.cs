using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Infrastructure.Persistence.DbContext;

namespace ShoppingList.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ShoppingListDbContext _context;

        public Repository(ShoppingListDbContext context) => _context = context;

        public async Task Create(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();

        public async Task<T> GetById(int id) => await _context.Set<T>().FindAsync(id);

    }
}