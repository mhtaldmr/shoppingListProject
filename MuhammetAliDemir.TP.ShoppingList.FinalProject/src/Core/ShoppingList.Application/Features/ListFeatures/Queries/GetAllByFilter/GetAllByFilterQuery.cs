using MediatR;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices;
using ShoppingList.Application.ViewModels.Request.FilterViewModels;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Application.Features.ListFeatures.Queries.GetAllByFilter
{
    public class GetAllByFilterQuery : FilterViewModel, IRequest<Result<PaginationResponse<GetListResponse>>>
    {
        public string UserId { get; set; }
    }

    public class GetAllByFilterQueryHandler : IRequestHandler<GetAllByFilterQuery, Result<PaginationResponse<GetListResponse>>>
    {
        private readonly IListGetByFilterService _listGetByFilterService;

        public GetAllByFilterQueryHandler(IListGetByFilterService listGetByFilterService)
            => _listGetByFilterService = listGetByFilterService;

        public async Task<Result<PaginationResponse<GetListResponse>>> Handle(GetAllByFilterQuery request, CancellationToken cancellationToken)
            => Result.Success(await _listGetByFilterService.GetListByFilter(request), "Successful");
    }
}