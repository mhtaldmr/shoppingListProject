namespace ShoppingList.Application.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
