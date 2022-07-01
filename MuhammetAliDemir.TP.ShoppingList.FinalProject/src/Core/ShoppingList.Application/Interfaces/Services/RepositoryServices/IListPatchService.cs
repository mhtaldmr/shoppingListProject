using ShoppingList.Application.Features.ListFeatures.Commands.Update;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Application.Interfaces.Services.RepositoryServices
{
    public interface IListPatchService
    {
        Task<GetListResponse> PatchList(PatchListCommand request);
    }
}
