using ShoppingList.Application.Features.ListFeatures.Queries.GetAllByFilter;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices
{
    public interface IListGetByFilterService
    {
        Task<PaginationResponse<GetListResponse>> GetListByFilter(GetAllByFilterQuery request);
    }
}