using AutoMapper;
using ShoppingList.Application.Features.ListFeatures.Commands.Update;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.Interfaces.Services.RabbitMq;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Infrastructure.Services.RepositoryServices.ListServices
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
            var lists = await _repository.GetAllListsByUsers(request.UserId);
            var listToPatch = lists.FirstOrDefault(l => l.Id == request.Id);
            if (listToPatch is null)
                throw new KeyNotFoundException();

            listToPatch.IsCompleted = request.IsCompleted;

            if (listToPatch.IsCompleted)
                listToPatch.CompletedAt = DateTime.Now;
            listToPatch.UpdatedAt = DateTime.Now;

            //Update the completed field
            await _repository.Update(listToPatch);

            var message = _mapper.Map<GetListResponseMessage>(listToPatch);

            if (listToPatch.IsCompleted)
                _publisherService.Publish(message, "direct.list", "direct.list1");

            //return the result
            return _mapper.Map<GetListResponse>(listToPatch);
        }
    }
}