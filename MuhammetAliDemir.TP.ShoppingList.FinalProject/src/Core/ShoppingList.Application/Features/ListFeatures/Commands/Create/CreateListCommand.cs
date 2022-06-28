using AutoMapper;
using MediatR;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using ShoppingList.Application.ViewModels.Response.ListResponse;
using ShoppingList.Application.ViewModels.Response.MainResponse;
using ShoppingList.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Application.Features.ListFeatures.Commands.Create
{
    public class CreateListCommand : IRequest<Result<GetListResponse>>
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public int CategoryId { get; set; } = 1;
        public string UserId { get; set; }
        public ICollection<ListItemViewModel> ListItems { get; set; }
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
            var listToAdd = _mapper.Map<CreateListCommand, List>(request);
            //create the list
            _repository.Create(listToAdd);

            //returning the user response
            var result = _mapper.Map<List, GetListResponse>(listToAdd);
            return Result.Success(result, "Successful");
        }
    }
}