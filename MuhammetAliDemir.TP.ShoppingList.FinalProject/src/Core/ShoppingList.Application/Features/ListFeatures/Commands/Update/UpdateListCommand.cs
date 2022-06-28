using MediatR;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Response.ListResponse;
using ShoppingList.Application.ViewModels.Response.MainResponse;

namespace ShoppingList.Application.Features.ListFeatures.Commands.Update
{
    public class UpdateListCommand : IRequest<Result<GetListResponse>>
    {

    }

    public class UpdateListCommandHandler : IRequestHandler<UpdateListCommand, Result<GetListResponse>>
    {
        private readonly IListRepository _repository;

        public UpdateListCommandHandler(IListRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<GetListResponse>> Handle(UpdateListCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
