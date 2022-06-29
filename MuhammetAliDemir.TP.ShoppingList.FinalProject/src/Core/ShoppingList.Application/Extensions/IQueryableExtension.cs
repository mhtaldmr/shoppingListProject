using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.ViewModels.Response.BaseResponses;

namespace ShoppingList.Application.Extensions
{
    public static class IQueryableExtension
    {
        public static async Task<PaginationResponse<T>> PaginateListAsync<T>(
            this IQueryable<T> query, int pageSize, int pageNumber) where T : class
        {
            if (query is null)
                throw new ArgumentNullException(nameof(query));

            pageSize = pageSize <= 0 ? 5 : pageSize;
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            var count = await query.CountAsync();
            var result = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return PaginationResponse.Success(result, pageSize, pageNumber, count);
        }
    }
}