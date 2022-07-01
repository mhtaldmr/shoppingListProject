using AutoMapper;
using MediatR;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.Interfaces.Services.RabbitMq;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Application.Features.ListFeatures.Commands.Update
{
    public class PatchListCommand : IRequest<Result<GetListResponse>>
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class PatchListCommandHandler : IRequestHandler<PatchListCommand, Result<GetListResponse>>
    {
        private readonly IListRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPublisherService _publisherService;

        public PatchListCommandHandler(IListRepository repository, IMapper mapper, IPublisherService publisherService)
        {
            _repository = repository;
            _mapper = mapper;
            _publisherService = publisherService;
        }
        public async Task<Result<GetListResponse>> Handle(PatchListCommand request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetListByIdWithItem(request.Id);
            if (list is null)
                return Result.Fail(new GetListResponse(), new KeyNotFoundException().Message);

            list.IsCompleted = request.IsCompleted;
            list.CompletedAt = DateTime.Now;
            list.UpdatedAt = DateTime.Now;

            //Update the completed field
            await _repository.Update(list);

            //return the result
            var result = _mapper.Map<GetListResponse>(list);
            var message = _mapper.Map<GetListResponseMessage>(list);

            if (list.IsCompleted)
                _publisherService.Publish(message, "direct.list", "direct.list1");

            return Result.Success(result, "Successful");
        }
    }
}
