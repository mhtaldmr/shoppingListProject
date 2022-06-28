using AutoMapper;
using MediatR;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Response.ListResponse;
using ShoppingList.Application.ViewModels.Response.MainResponse;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Features.ListFeatures.Queries.GetById
{
    public class GetListByIdQuery : IRequest<Result<GetListByIdResponse>>
    {
        public int Id { get; set; }
    }

    public class GetListByIdQueryHandler : IRequestHandler<GetListByIdQuery, Result<GetListByIdResponse>>
    {
        private readonly IListRepository _repository;
        private readonly IMapper _mapper;

        public GetListByIdQueryHandler(IListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<GetListByIdResponse>> Handle(GetListByIdQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetListByIdWithItem(request.Id);
            if (list is null)
                throw new KeyNotFoundException();

            var result = _mapper.Map<List, GetListByIdResponse>(list);
            return Result.Success(result, "Successful");
        }
    }
}
