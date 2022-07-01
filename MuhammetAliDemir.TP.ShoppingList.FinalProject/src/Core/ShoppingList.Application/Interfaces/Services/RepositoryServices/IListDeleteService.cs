using ShoppingList.Application.Features.ListFeatures.Commands.Delete;

namespace ShoppingList.Application.Interfaces.Services.RepositoryServices
{
    public interface IListDeleteService
    {
        Task DeleteList(DeleteListCommand request);
    }
}
