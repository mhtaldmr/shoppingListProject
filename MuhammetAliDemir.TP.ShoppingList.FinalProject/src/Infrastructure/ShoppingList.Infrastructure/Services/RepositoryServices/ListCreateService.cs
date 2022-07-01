using AutoMapper;
using ShoppingList.Application.Features.ListFeatures.Commands.Create;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.Interfaces.Services.RepositoryServices;
using ShoppingList.Application.ViewModels.Response.ListResponses;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Infrastructure.Services.RepositoryServices
{
    public class ListCreateService : IListCreateService
    {
        private readonly IMapper _mapper;
        private readonly IListRepository _repository;

        public ListCreateService(IMapper mapper, IListRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GetListResponse> CreateList(CreateListCommand request)
        {
            //mapping the request model to domain model
            var list = _mapper.Map<List>(request);
            //create the list
            await _repository.Create(list);

            //returning the user response
            return _mapper.Map<GetListResponse>(list);
        }
    }
}