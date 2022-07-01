using ShoppingList.Application.Features.ListFeatures.Commands.Update;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices
{
    public interface IListUpdateService
    {
        Task<GetListResponse> UpdateList(UpdateListCommand request);
    }
}
