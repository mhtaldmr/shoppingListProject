using ShoppingList.Client.Data;
using ShoppingList.Client.Data.UomResponse;

namespace ShoppingList.Client.Services.UnitOfMaterials
{
    public interface IUomService
    {
        Task<BaseResponse<UomResponse>> GetAllUnits();
    }
}