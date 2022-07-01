using ShoppingList.Application.Features.UoMFeatures.Queries.GetAll;
using ShoppingList.Application.ViewModels.Response.UomResponses;

namespace ShoppingList.Application.Interfaces.Services.RepositoryServices.UomServices
{
    public interface IUomGetService
    {
        Task<IEnumerable<GetUomResponse>> GetAllUom(GetAllUomsQuery request);
    }
}