using ShoppingList.Application.Features.ListFeatures.Commands.Delete;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices;

namespace ShoppingList.Infrastructure.Services.RepositoryServices.ListServices
{
    public class ListDeleteService : IListDeleteService
    {
        private readonly IListRepository _repository;

        public ListDeleteService(IListRepository repository) => _repository = repository;

        public async Task DeleteList(DeleteListCommand request)
        {
            var lists = await _repository.GetAllListsByUsers(request.UserId);
            var listToDelete = lists.FirstOrDefault(l => l.Id == request.Id);

            if (listToDelete is null)
                throw new KeyNotFoundException();

            await _repository.Delete(listToDelete);
        }
    }
}