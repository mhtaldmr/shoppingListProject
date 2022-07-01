using ShoppingList.Application.Features.CategoryFeatures.Queries.GetAll;
using ShoppingList.Application.ViewModels.Response.CategoryResponses;

namespace ShoppingList.Application.Interfaces.Services.RepositoryServices.CategoryServices
{
    public interface ICategoryGetService
    {
        Task<IEnumerable<GetCategoryResponse>> GetAllCategory(GetAllCategoriesQuery request);
    }
}