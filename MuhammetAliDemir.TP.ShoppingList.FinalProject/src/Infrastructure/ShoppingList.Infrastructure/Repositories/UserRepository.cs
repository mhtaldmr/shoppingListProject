using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Domain.Entities;
using ShoppingList.Infrastructure.Persistence.DbContext;

namespace ShoppingList.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ShoppingListDbContext _context;

        public UserRepository(ShoppingListDbContext context) => _context = context;

        public async Task<User> GetById(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user is null ? throw new ArgumentNullException("Userid does not exist!") : user;
        }

        public async Task<IEnumerable<User>> GetAll() => await _context.Users.ToListAsync();

        public void Create(User user) => _context.Users.Add(user);
      
        public void Delete(User user) => _context.Users.Remove(user);
        
    }
}
