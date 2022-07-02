using AutoMapper;
using MediatR;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using ShoppingList.Application.ViewModels.Response.ListResponses;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices;

namespace ShoppingList.Application.Features.ListFeatures.Commands.Update
{
    public class UpdateListCommand : ListUpdateViewModel,IRequest<Result<GetListResponse>>
    {
        public int Id { get; set; }
        public string  UserId { get; set; }
    }

    public class UpdateListCommandHandler : IRequestHandler<UpdateListCommand, Result<GetListResponse>>
    {
        private readonly IListUpdateService _listUpdateService;
        public UpdateListCommandHandler(IListRepository repository,IMapper mapper, IListUpdateService listUpdateService)
            => _listUpdateService = listUpdateService;

        public async Task<Result<GetListResponse>> Handle(UpdateListCommand request, CancellationToken cancellationToken)
            => Result.Success(await _listUpdateService.UpdateList(request), "Successful");
    }
}