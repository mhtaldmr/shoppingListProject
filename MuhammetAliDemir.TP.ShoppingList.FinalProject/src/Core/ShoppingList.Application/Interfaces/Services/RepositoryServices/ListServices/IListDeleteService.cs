using ShoppingList.Application.Features.ListFeatures.Commands.Delete;

namespace ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices
{
    public interface IListDeleteService
    {
        Task DeleteList(DeleteListCommand request);
    }
}
