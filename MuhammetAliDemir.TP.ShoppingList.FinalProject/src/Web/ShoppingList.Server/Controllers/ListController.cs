using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Features.ListFeatures.Queries.GetById;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using ShoppingList.Domain.Entities;
using ShoppingList.Infrastructure.Persistence.DbContext;

namespace ShoppingList.Server.Controllers
{
    [Route("/lists")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class ListController : ControllerBase
    {
        private readonly IListRepository _repository;

        private readonly IMapper _mapper;
        private readonly ShoppingListDbContext _context;
        private readonly IMediator _mediator;

        public ListController(IListRepository repository, IMapper mapper, ShoppingListDbContext context,IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLists()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpGet("/getbyid")]
        public async Task<IActionResult> GetListById([FromQuery] GetListByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }



        [HttpPost("create")]
        public IActionResult CreateList([FromBody] ListViewModel list)
        {
            var listToAdd = _mapper.Map<ListViewModel, List>(list);
            listToAdd.UserId = "9cecb3b1-a4f1-45fa-883d-5873d86d4f5a";//HttpContext.GetUserId();

            _repository.Create(listToAdd);
            return Ok(listToAdd);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateList([FromBody] ListViewModel list, int id)
        {
            var listToUpdate = await _repository.GetById(id);
            _ = _context.Items.Where(z => z.ListId == id).ToList();

            if (listToUpdate is null)
                return NotFound($"This List with id = {id} doesnt exist!");

            listToUpdate.CategoryId = list.CategoryId;
            listToUpdate.Description = list.Description;
            listToUpdate.Title = list.Title;
            listToUpdate.UpdatedAt = DateTime.Now;

            foreach (var item in listToUpdate.Items)
            {
                var itemToChange = list.ListItems.SingleOrDefault(a => a.Name == item.Name);
                if (itemToChange != null && item.Name == itemToChange.Name)
                {
                    item.Name = itemToChange.Name;
                    item.Quantity = itemToChange.Quantity;
                    item.UoMId = itemToChange.UoMId;
                    item.IsChecked = itemToChange.IsChecked;
                    item.UpdatedAt = DateTime.Now;
                }
            }
            _repository.Update(listToUpdate);
            return Ok(listToUpdate);
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateListByJsonPatch(int id, [FromBody] JsonPatchDocument<List> list)
        {
            var listToUpdate = await _repository.GetById(id);

            if (listToUpdate is null)
                return NotFound($"This List with id = {id} doesnt exist!");

            //To apply the changes
            list.ApplyTo(listToUpdate);

            listToUpdate.UpdatedAt = DateTime.Now;
            listToUpdate.CompletedAt = DateTime.Now;

            _repository.Update(listToUpdate);
            return Ok(listToUpdate); //Http 200
        }


        [HttpDelete("/delete")]
        public async Task<IActionResult> DeleteList([FromQuery] int id)
        {
            var listToDelete = await _repository.GetById(id);

            if (listToDelete is null)
                return NotFound($"This List with id = {id} doesnt exist!");

            _repository.Delete(listToDelete);
            return NoContent();
        }
    }
}