using ShoppingList.Application.Features.ListFeatures.Queries.GetAllByFilter;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Interfaces.Repositories
{
    public interface IListRepository : IRepository<List>
    {
        Task<IEnumerable<List>> GetAllListsWithItems();
        Task<IEnumerable<List>> GetAllListsByUsers(string userId);
        Task<List> GetListByIdWithItem(int id);
        Task<PaginationResponse<List>> GetAllListsByFilter(GetAllByFilterQuery filter);
    }
}
