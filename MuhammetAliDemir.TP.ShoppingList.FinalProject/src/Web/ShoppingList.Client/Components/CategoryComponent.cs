using Microsoft.AspNetCore.Components;
using ShoppingList.Client.Data;
using ShoppingList.Client.Data.CategoryResponse;
using ShoppingList.Client.Services.Category;

namespace ShoppingList.Client.Components
{
    public class CategoryComponent : ComponentBase
    {
        [Inject]
        public ICategoryService CategoryService { get; set; }
        public BaseResponse<CategoryResponse> Category { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Category = await CategoryService.GetAllCategories();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
