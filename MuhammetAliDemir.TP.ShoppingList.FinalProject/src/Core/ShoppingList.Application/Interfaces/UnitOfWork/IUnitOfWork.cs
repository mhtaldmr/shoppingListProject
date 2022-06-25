using ShoppingList.Application.Interfaces.Repositories;

namespace ShoppingList.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();

        IListRepository List { get; }
    }
}
