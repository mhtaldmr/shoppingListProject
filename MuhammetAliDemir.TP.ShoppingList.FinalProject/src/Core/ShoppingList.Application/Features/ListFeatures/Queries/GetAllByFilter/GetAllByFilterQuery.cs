using AutoMapper;
using MediatR;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Request.FilterViewModels;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Application.Features.ListFeatures.Queries.GetAllByFilter
{
    public class GetAllByFilterQuery : FilterViewModel, IRequest<Result<PaginationResponse<GetListResponse>>>
    {
    }

    public class GetAllByFilterQueryHandler : IRequestHandler<GetAllByFilterQuery, Result<PaginationResponse<GetListResponse>>>
    {
        private readonly IListRepository _repository;
        private readonly IMapper _mapper;

        public GetAllByFilterQueryHandler(IListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<PaginationResponse<GetListResponse>>> Handle(GetAllByFilterQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetAllListsByFilter(request);
            if (list is null)
                throw new ArgumentNullException();

            var mappedList = _mapper.Map<List<GetListResponse>>(list.PaginatedData);
            var result = new PaginationResponse<GetListResponse>(mappedList, list.PageSize, list.CurrentPage, list.TotalCount);
            return Result.Success(result, "Successful");
        }
    }
}