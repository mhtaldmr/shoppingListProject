using ShoppingList.Application.Features.ListFeatures.Commands.Create;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Application.Interfaces.Services.RepositoryServices
{
    public interface IListCreateService
    {
        Task<GetListResponse> CreateList(CreateListCommand request);
    }
}
