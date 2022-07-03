using Microsoft.AspNetCore.Components;
using ShoppingList.Client.Data;
using ShoppingList.Client.Data.UomResponse;
using ShoppingList.Client.Services.UnitOfMaterials;

namespace ShoppingList.Client.Components
{
    public class UomComponent : ComponentBase
    {
        [Inject]
        public IUomService UomService { get; set; }
        public BaseResponse<UomResponse> Units { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Units = await UomService.GetAllUnits();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
