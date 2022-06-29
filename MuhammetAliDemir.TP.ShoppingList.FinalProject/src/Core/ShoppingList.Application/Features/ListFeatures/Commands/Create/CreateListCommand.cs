using AutoMapper;
using MediatR;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using ShoppingList.Application.ViewModels.Response.ListResponses;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Features.ListFeatures.Commands.Create
{
    public class CreateListCommand : IRequest<Result<GetListResponse>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; } = 1;
        public string UserId { get; set; }
        public ICollection<ListItemViewModel> Items { get; set; }
    }

    public class CreateListCommandHandler : IRequestHandler<CreateListCommand, Result<GetListResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IListRepository _repository;

        public CreateListCommandHandler(IMapper mapper, IListRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<Result<GetListResponse>> Handle(CreateListCommand request, CancellationToken cancellationToken)
        {
            //mapping the request model to domain model
            var list = _mapper.Map<List>(request);
            //create the list
            await _repository.Create(list);

            //returning the user response
            var result = _mapper.Map<GetListResponse>(list);
            return Result.Success(result, "Successful");
        }
    }
}