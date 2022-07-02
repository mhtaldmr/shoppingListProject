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
            var lists = await _repository.GetAllListsByUsers(request.UserId);
            var listToShow = lists.FirstOrDefault(l => l.Id == request.Id);
            if (listToShow is null)
                throw new KeyNotFoundException();

            return _mapper.Map<GetListResponse>(listToShow);
        }
    }
}