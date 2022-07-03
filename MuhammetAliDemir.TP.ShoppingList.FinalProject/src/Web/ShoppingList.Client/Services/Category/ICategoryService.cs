using ShoppingList.Client.Data;
using ShoppingList.Client.Data.CategoryResponse;

namespace ShoppingList.Client.Services.Category
{
    public interface ICategoryService
    {
        Task<BaseResponse<CategoryResponse>> GetAllCategories();
    }
}