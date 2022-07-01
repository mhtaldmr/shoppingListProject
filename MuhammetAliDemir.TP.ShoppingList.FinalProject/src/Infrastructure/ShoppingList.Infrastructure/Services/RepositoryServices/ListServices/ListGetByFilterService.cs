using AutoMapper;
using ShoppingList.Application.Features.ListFeatures.Queries.GetAllByFilter;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Infrastructure.Services.RepositoryServices.ListServices
{
    public class ListGetByFilterService : IListGetByFilterService
    {
        private readonly IListRepository _repository;
        private readonly IMapper _mapper;

        public ListGetByFilterService(IListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<GetListResponse>> GetListByFilter(GetAllByFilterQuery request)
        {
            var list = await _repository.GetAllListsByFilter(request);
            if (list is null)
                throw new ArgumentNullException();

            var mappedList = _mapper.Map<List<GetListResponse>>(list.PaginatedData);
            return new PaginationResponse<GetListResponse>(mappedList, list.PageSize, list.CurrentPage, list.TotalCount);
        }
    }
}