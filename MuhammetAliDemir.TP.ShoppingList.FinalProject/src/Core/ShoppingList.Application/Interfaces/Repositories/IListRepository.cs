using ShoppingList.Application.ViewModels.Request.FilterViewModels;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Interfaces.Repositories
{
    public interface IListRepository : IRepository<List>
    {
        Task<IEnumerable<List>> GetAllListsWithItems();
        Task<List> GetListByIdWithItem(int id);
        Task<PaginationResponse<List>> GetAllListsByFilter(FilterViewModel filter);
    }
}
