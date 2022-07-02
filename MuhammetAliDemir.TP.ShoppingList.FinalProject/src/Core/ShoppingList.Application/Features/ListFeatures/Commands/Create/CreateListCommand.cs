using MediatR;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Application.Features.ListFeatures.Commands.Create
{
    public class CreateListCommand : ListCreateViewModel, IRequest<Result<GetListResponse>>
    {
        public string UserId { get; set; }
    }

    public class CreateListCommandHandler : IRequestHandler<CreateListCommand, Result<GetListResponse>>
    {
        private readonly IListCreateService _listCreateService;
        public CreateListCommandHandler(IListCreateService listCreateService)
            => _listCreateService = listCreateService;

        public async Task<Result<GetListResponse>> Handle(CreateListCommand request, CancellationToken cancellationToken)
            => Result.Success(await _listCreateService.CreateList(request), "Successful");
    }
}