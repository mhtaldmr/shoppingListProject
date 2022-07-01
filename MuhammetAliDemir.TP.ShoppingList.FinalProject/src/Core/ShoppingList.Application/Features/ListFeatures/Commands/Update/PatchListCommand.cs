using MediatR;
using ShoppingList.Application.Interfaces.Services.RepositoryServices;
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
        private readonly IListPatchService _listPatchService;
        public PatchListCommandHandler(IListPatchService listPatchService)
            => _listPatchService = listPatchService;

        public async Task<Result<GetListResponse>> Handle(PatchListCommand request, CancellationToken cancellationToken)
            => Result.Success(await _listPatchService.PatchList(request), "Successful");
    }
}