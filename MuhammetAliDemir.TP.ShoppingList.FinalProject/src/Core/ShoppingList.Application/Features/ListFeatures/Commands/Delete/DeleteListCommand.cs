using MediatR;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Response.BaseResponses;

namespace ShoppingList.Application.Features.ListFeatures.Commands.Delete
{
    public class DeleteListCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteListCommandHandler : IRequestHandler<DeleteListCommand, Result<int>>
    {
        private readonly IListRepository _repository;

        public DeleteListCommandHandler(IListRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(DeleteListCommand request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetListByIdWithItem(request.Id);
            if (list is null)
                return Result.Fail(0, new KeyNotFoundException().Message);

            await _repository.Delete(list );
            return Result.Success(request.Id, "Provided key deleted!");
        }
    }
}
