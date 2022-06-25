using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetById(string id);
        Task<IEnumerable<User>> GetAll();
        void Create(User user);
        void Delete(User user);
    }
}