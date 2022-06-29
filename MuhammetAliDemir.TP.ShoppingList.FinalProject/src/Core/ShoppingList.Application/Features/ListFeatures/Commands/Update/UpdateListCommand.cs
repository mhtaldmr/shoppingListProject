using AutoMapper;
using MediatR;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using ShoppingList.Application.ViewModels.Response.ListResponses;
using ShoppingList.Application.ViewModels.Response.BaseResponses;

namespace ShoppingList.Application.Features.ListFeatures.Commands.Update
{
    public class UpdateListCommand : IRequest<Result<GetListResponse>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; } = 1;
        public ICollection<ListItemActionViewModel> Items { get; set; }
    }

    public class UpdateListCommandHandler : IRequestHandler<UpdateListCommand, Result<GetListResponse>>
    {
        private readonly IListRepository _repository;
        private readonly IMapper _mapper;

        public UpdateListCommandHandler(IListRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<GetListResponse>> Handle(UpdateListCommand request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetListByIdWithItem(request.Id);
            if (list is null)
                return Result.Fail(new GetListResponse(), new KeyNotFoundException().Message);

            //Updating the list 
            list.CategoryId = request.CategoryId;
            list.Description = request.Description;
            list.Title = request.Title;
            list.UpdatedAt = DateTime.Now;

            //Updating the items in the list
            foreach (var item in list.Items)
            {
                var itemToUpdate = request.Items.SingleOrDefault(a => a.Id == item.Id);
                if (itemToUpdate != null && item.Id == itemToUpdate.Id)
                {
                    item.Name = itemToUpdate.Name;
                    item.Quantity = itemToUpdate.Quantity;
                    item.UoMId = itemToUpdate.UoMId;
                    item.IsChecked = itemToUpdate.IsChecked;
                    item.UpdatedAt = DateTime.Now;
                }
            }

            //Update result
            await _repository.Update(list);
            //return the last value
            var result = _mapper.Map<GetListResponse>(list);
            return Result.Success(result, "Successful");
        }
    }
}
