using ShoppingList.Application.Features.ListFeatures.Commands.Delete;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.Interfaces.Services.RepositoryServices;

namespace ShoppingList.Infrastructure.Services.RepositoryServices
{
    public class ListDeleteService : IListDeleteService
    {
        private readonly IListRepository _repository;

        public ListDeleteService(IListRepository repository) => _repository = repository;

        public async Task DeleteList(DeleteListCommand request)
        {
            var list = await _repository.GetListByIdWithItem(request.Id);
            if (list is null)
                throw new KeyNotFoundException();

            await _repository.Delete(list);
        }
    }
}
