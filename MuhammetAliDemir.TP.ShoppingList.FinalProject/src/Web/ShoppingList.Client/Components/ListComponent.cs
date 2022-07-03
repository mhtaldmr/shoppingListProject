using Microsoft.AspNetCore.Components;
using ShoppingList.Client.Data;
using ShoppingList.Client.Data.ListResponse;
using ShoppingList.Client.Services.List;

namespace ShoppingList.Client.Components
{
    public class ListComponent : ComponentBase
    {
        [Inject]
        public IListService ListService { get; set; }
        public BaseResponse<ListResponse> List { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                List = await ListService.GetAllLists();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}