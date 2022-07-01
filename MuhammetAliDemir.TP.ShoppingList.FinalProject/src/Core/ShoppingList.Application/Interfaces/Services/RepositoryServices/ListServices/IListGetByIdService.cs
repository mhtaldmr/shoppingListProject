using ShoppingList.Application.Features.ListFeatures.Queries.GetById;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices
{
    public interface IListGetByIdService
    {
        Task<GetListResponse> GetListById(GetListByIdQuery request);
    }
}
