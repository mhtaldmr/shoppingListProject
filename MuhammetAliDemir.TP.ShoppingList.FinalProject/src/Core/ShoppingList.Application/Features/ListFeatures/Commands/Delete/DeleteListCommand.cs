using MediatR;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices;
using ShoppingList.Application.ViewModels.Response.BaseResponses;

namespace ShoppingList.Application.Features.ListFeatures.Commands.Delete
{
    public class DeleteListCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteListCommandHandler : IRequestHandler<DeleteListCommand, Result<int>>
    {
        private readonly IListDeleteService _listDeleteService;
        public DeleteListCommandHandler(IListDeleteService listDeleteService)
            => _listDeleteService = listDeleteService;

        public async Task<Result<int>> Handle(DeleteListCommand request, CancellationToken cancellationToken)
        {
            await _listDeleteService.DeleteList(request);
            return Result.Success(request.Id, "Provided key deleted!");
        }
    }
}