using ShoppingList.Application.Features.ListFeatures.Queries.GetAll;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices
{
    public interface IListGetAllService
    {
        Task<ICollection<GetListResponse>> GetAll(GetAllListsQuery request);
    }
}