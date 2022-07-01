using MediatR;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Application.Features.ListFeatures.Queries.GetAll
{
    public record GetAllListsQuery : IRequest<Result<ICollection<GetListResponse>>>;

    public class GetAllListsQueryHandler : IRequestHandler<GetAllListsQuery, Result<ICollection<GetListResponse>>>
    {
        private readonly IListGetAllService _listGetAllService;
        public GetAllListsQueryHandler(IListGetAllService listGetAllService)
            => _listGetAllService = listGetAllService;

        public async Task<Result<ICollection<GetListResponse>>> Handle(GetAllListsQuery request, CancellationToken cancellationToken)
            => Result.Success(await _listGetAllService.GetAll(request), "Successful");
    }
}