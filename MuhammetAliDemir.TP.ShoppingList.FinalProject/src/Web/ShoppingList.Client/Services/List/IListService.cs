using ShoppingList.Client.Data;
using ShoppingList.Client.Data.ListResponse;

namespace ShoppingList.Client.Services.List
{
    public interface IListService
    {
        Task<BaseResponse<ListResponse>> GetAllLists();
    }
}
