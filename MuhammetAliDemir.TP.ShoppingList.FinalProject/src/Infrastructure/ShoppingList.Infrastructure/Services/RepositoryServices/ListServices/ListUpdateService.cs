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
            var list = await _repository.GetListByIdWithItem(request.Id);
            if (list is null)
                throw new KeyNotFoundException();

            //Updating the list 
            list.CategoryId = request.CategoryId != default ? request.CategoryId : list.CategoryId;
            list.Description = request.Description != default ? request.Description : list.Description;
            list.Title = request.Title != default ? request.Title : list.Title;
            list.UpdatedAt = DateTime.Now;

            //Updating the items in the list
            foreach (var item in list.Items)
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
            await _repository.Update(list);
            //return the last value
            return _mapper.Map<GetListResponse>(list);
        }
    }
}