using AutoMapper;
using ShoppingList.Application.Features.ListFeatures.Commands.Update;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices;
using ShoppingList.Application.ViewModels.Response.ListResponses;

namespace ShoppingList.Infrastructure.Services.RepositoryServices.ListServices
{
    public class ListUpdateService : IListUpdateService
    {
        private readonly IListRepository _repository;
        private readonly IMapper _mapper;

        public ListUpdateService(IListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetListResponse> UpdateList(UpdateListCommand request)
        {
            var lists = await _repository.GetAllListsByUsers(request.UserId);
            var listToUpdate = lists.FirstOrDefault(l => l.Id == request.Id);
            if (listToUpdate is null)
                throw new KeyNotFoundException();

            //Updating the list 
            listToUpdate.CategoryId = request.CategoryId != default ? request.CategoryId : listToUpdate.CategoryId;
            listToUpdate.Description = request.Description != default ? request.Description : listToUpdate.Description;
            listToUpdate.Title = request.Title != default ? request.Title : listToUpdate.Title;
            listToUpdate.UpdatedAt = DateTime.Now;

            //Updating the items in the list
            foreach (var item in listToUpdate.Items)
            {
                var itemToUpdate = request.Items.SingleOrDefault(a => a.Id == item.Id);
                if (itemToUpdate != null && item.Id == itemToUpdate.Id)
                {
                    item.Name = itemToUpdate.Name != default ? itemToUpdate.Name : item.Name;
                    item.Quantity = itemToUpdate.Quantity != default ? itemToUpdate.Quantity : item.Quantity;
                    item.UoMId = itemToUpdate.UoMId != default ? itemToUpdate.UoMId : item.UoMId;
                    item.IsChecked = itemToUpdate.IsChecked != default ? itemToUpdate.IsChecked : item.IsChecked;
                    item.UpdatedAt = DateTime.Now;
                }
            }

            //Update result
            await _repository.Update(listToUpdate);
            //return the last value
            return _mapper.Map<GetListResponse>(listToUpdate);
        }
    }
}