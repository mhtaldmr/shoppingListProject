using MediatR;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Application.Features.ListFeatures.Commands.Update
{
    public class PatchListCommand : ListPatchViewModel, IRequest<Result<GetListResponse>>
    {
        public string UserId { get; set; }
    }

    public class PatchListCommandHandler : IRequestHandler<PatchListCommand, Result<GetListResponse>>
    {
        private readonly IListPatchService _listPatchService;
        public PatchListCommandHandler(IListPatchService listPatchService)
            => _listPatchService = listPatchService;

        public async Task<Result<GetListResponse>> Handle(PatchListCommand request, CancellationToken cancellationToken)
            => Result.Success(await _listPatchService.PatchList(request), "Successful");
    }
}