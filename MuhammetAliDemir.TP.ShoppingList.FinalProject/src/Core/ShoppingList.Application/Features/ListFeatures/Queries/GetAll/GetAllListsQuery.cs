using AutoMapper;
using MediatR;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Response.ListResponses;
using ShoppingList.Application.ViewModels.Response.BaseResponses;

namespace ShoppingList.Application.Features.ListFeatures.Queries.GetAll
{
    public class GetAllListsQuery : IRequest<Result<ICollection<GetListResponse>>>
    {
    }

    public class GetAllLİstsQueryHandler : IRequestHandler<GetAllListsQuery, Result<ICollection<GetListResponse>>>
    {
        private readonly IListRepository _repository;
        private readonly IMapper _mapper;

        public GetAllLİstsQueryHandler(IListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result<ICollection<GetListResponse>>> Handle(GetAllListsQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetAllListsWithItems();
            if (list is null)
                throw new ArgumentNullException();

            var result = _mapper.Map<ICollection<GetListResponse>>(list);
            return Result.Success(result, $"Successful! Total : {result.Count} list!");
        }
    }
}