using AutoMapper;
using ShoppingList.Application.Features.ListFeatures.Commands.Update;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.Interfaces.Services.RabbitMq;
using ShoppingList.Application.Interfaces.Services.RepositoryServices;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Infrastructure.Services.RepositoryServices
{
    public class ListPatchService : IListPatchService
    {
        private readonly IListRepository _repository;
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public ListPatchService(IListRepository repository, IPublisherService publisherService, IMapper mapper)
        {
            _repository = repository;
            _publisherService = publisherService;
            _mapper = mapper;
        }
        public async Task<GetListResponse> PatchList(PatchListCommand request)
        {
            var list = await _repository.GetListByIdWithItem(request.Id);
            if (list is null)
                throw new KeyNotFoundException();

            list.IsCompleted = request.IsCompleted;

            if (list.IsCompleted)
                list.CompletedAt = DateTime.Now;
            list.UpdatedAt = DateTime.Now;

            //Update the completed field
            await _repository.Update(list);

            var message = _mapper.Map<GetListResponseMessage>(list);

            if (list.IsCompleted)
                _publisherService.Publish(message, "direct.list", "direct.list1");

            //return the result
            return _mapper.Map<GetListResponse>(list);
        }
    }
}