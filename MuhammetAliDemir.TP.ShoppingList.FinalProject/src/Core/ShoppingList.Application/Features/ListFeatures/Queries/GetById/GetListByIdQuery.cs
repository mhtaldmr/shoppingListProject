using MediatR;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Application.Features.ListFeatures.Queries.GetById
{
    public class GetListByIdQuery : IRequest<Result<GetListResponse>>
    {
        public int Id { get; set; }
    }

    public class GetListByIdQueryHandler : IRequestHandler<GetListByIdQuery, Result<GetListResponse>>
    {
        private readonly IListGetByIdService _listGetByIdService;
        public GetListByIdQueryHandler(IListGetByIdService listGetByIdService)
            => _listGetByIdService = listGetByIdService;

        public async Task<Result<GetListResponse>> Handle(GetListByIdQuery request, CancellationToken cancellationToken)
            => Result.Success(await _listGetByIdService.GetListById(request), "Successful");
    }
}