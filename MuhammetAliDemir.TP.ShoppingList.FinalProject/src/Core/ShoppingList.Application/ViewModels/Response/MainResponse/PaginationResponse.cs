namespace ShoppingList.Application.ViewModels.Response.MainResponse
{
    public static class PaginationResponse
    {
        public static PaginationResponse<T> Success<T>(List<T> data, int pageSize, int currentPage, int totalCount)
            => new(data, pageSize, currentPage, totalCount);
    }

    public class PaginationResponse<T>
    {
        public PaginationResponse(List<T> data, int pageSize, int currentPage, int totalCount)
        {
            Data = data;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            TotalCount = totalCount;
        }

        public List<T> Data { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}