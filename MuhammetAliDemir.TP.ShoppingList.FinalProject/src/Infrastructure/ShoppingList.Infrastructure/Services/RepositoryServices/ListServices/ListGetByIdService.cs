using AutoMapper;
using ShoppingList.Application.Features.ListFeatures.Queries.GetById;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Infrastructure.Services.RepositoryServices.ListServices
{
    public class ListGetByIdService : IListGetByIdService
    {
        private readonly IListRepository _repository;
        private readonly IMapper _mapper;

        public ListGetByIdService(IListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetListResponse> GetListById(GetListByIdQuery request)
        {
            var list = await _repository.GetListByIdWithItem(request.Id);
            if (list is null)
                throw new KeyNotFoundException();

            return _mapper.Map<GetListResponse>(list);
        }
    }
}