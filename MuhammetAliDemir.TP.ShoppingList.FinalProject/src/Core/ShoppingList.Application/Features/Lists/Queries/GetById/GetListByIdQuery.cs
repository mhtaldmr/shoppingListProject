using MediatR;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Response.ListResponse;
using ShoppingList.Application.ViewModels.Response.MainResponse;

namespace ShoppingList.Application.Features.Lists.Queries.GetById
{
    public class GetListByIdQuery : IRequest<Result<GetListByIdResponse>>
    {
        public int Id { get; set; }
    }

    public class GetListByIdQueryHandler : IRequestHandler<GetListByIdQuery, Result<GetListByIdResponse>>
    {
        private readonly IListRepository _repository;

        public GetListByIdQueryHandler(IListRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<GetListByIdResponse>> Handle(GetListByIdQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetById(request.Id);



            return Result.Success(list, "Successful!");
        }
    }
}
