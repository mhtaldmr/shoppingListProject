using AutoMapper;
using ShoppingList.Application.Features.ListFeatures.Queries.GetAll;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Infrastructure.Services.RepositoryServices.ListServices
{
    public class ListGetAllService : IListGetAllService
    {
        private readonly IListRepository _repository;
        private readonly IMapper _mapper;

        public ListGetAllService(IListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ICollection<GetListResponse>> GetAll(GetAllListsQuery request)
        {
            var list = await _repository.GetAllListsWithItems();
            if (list is null)
                throw new ArgumentNullException();

            return _mapper.Map<ICollection<GetListResponse>>(list);
        }
    }
}