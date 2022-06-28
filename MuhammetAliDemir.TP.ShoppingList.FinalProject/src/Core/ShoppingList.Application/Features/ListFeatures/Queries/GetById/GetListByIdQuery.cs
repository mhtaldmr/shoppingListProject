using AutoMapper;
using MediatR;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Response.ListResponse;
using ShoppingList.Application.ViewModels.Response.MainResponse;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Features.ListFeatures.Queries.GetById
{
    public class GetListByIdQuery : IRequest<Result<GetListResponse>>
    {
        public int Id { get; set; }
    }

    public class GetListByIdQueryHandler : IRequestHandler<GetListByIdQuery, Result<GetListResponse>>
    {
        private readonly IListRepository _repository;
        private readonly IMapper _mapper;

        public GetListByIdQueryHandler(IListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<GetListResponse>> Handle(GetListByIdQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetListByIdWithItem(request.Id);
            if (list is null)
                return Result.Fail(new GetListResponse(), new KeyNotFoundException().Message);

            var result = _mapper.Map<List, GetListResponse>(list);
            return Result.Success(result, "Successful");
        }
    }
}