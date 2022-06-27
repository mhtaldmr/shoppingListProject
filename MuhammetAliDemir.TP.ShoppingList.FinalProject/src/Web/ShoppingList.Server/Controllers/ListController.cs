using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using ShoppingList.Domain.Entities;
using ShoppingList.Infrastructure.Persistence.DbContext;

namespace ShoppingList.Server.Controllers
{
    [Route("/lists")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IListRepository _repository;

        private readonly IMapper _mapper;
        private readonly ShoppingListDbContext _context;

        public ListController(IListRepository repository, IMapper mapper, ShoppingListDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost("create")]
        public IActionResult CreateList([FromBody] ListCreateViewModel list)
        {
            var result = _mapper.Map<ListCreateViewModel, List>(list);
            result.UserId = "b1248f2a-99b0-4071-8a6e-60fad489cf02";//HttpContext.GetUserId();

            _repository.Create(result);
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateList([FromBody] ListCreateViewModel list, int id)
        {
            var listToUpdate = await _repository.GetById(id);
            _ = _context.Items.Where(z => z.ListId == id).ToList();

            if (listToUpdate is null)
                return NotFound();

            var result = _mapper.Map<ListCreateViewModel, List>(list);

            listToUpdate.CategoryId = result.CategoryId;
            listToUpdate.Description = result.Description;
            listToUpdate.Title = result.Title;
            listToUpdate.CreatedAt = listToUpdate.CreatedAt;
            listToUpdate.CompletedAt = listToUpdate.CompletedAt;
            listToUpdate.UpdatedAt = DateTime.Now;
            listToUpdate.Items = result.Items.Select(z =>
                new Item { Name = z.Name, Quantity = z.Quantity, UoMId = z.UoMId, IsChecked = z.IsChecked, CreatedAt = z.CreatedAt, UpdatedAt = DateTime.Now }).ToList();

            _repository.Update(listToUpdate);
            return Ok(listToUpdate);
        }


        [HttpDelete("/delete")]
        public async Task<IActionResult> DeleteList([FromQuery] int id)
        {
            var listToDelete = await _repository.GetById(id);
            if (listToDelete is null)
                return NotFound();

            _repository.Delete(listToDelete);
            return NoContent();
        }

        [HttpPatch("/lists/item/{id}")]
        public async Task<IActionResult> DriverWithJsonPatch(int id, [FromBody] JsonPatchDocument<Item> listToPatch)
        {
            var listToUpdate = await _repository.GetById(id);
            var items = _context.Items.Where(z => z.ListId == id).FirstOrDefault();

            if (listToUpdate is null)
                return NotFound($"This driver with id = {id} doesnt exist in the list!");

            //To apply the changes
            listToPatch.ApplyTo(items);

            listToUpdate.UpdatedAt = DateTime.Now;
            listToUpdate.Items = listToUpdate.Items.Select(z =>
                            new Item { Name = z.Name, Quantity = z.Quantity, UoMId = z.UoMId, IsChecked = z.IsChecked, CreatedAt = z.CreatedAt, UpdatedAt = DateTime.Now }).ToList();


            _repository.Update(listToUpdate);
            return Ok(listToUpdate); //Http 200
        }



        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateDriverWithJsonPatch(int id, [FromBody] JsonPatchDocument<List> listToPatch)
        {
            var listToUpdate = await _repository.GetById(id);

            if (listToUpdate is null)
                return NotFound($"This driver with id = {id} doesnt exist in the list!");

            //To apply the changes
            listToPatch.ApplyTo(listToUpdate);

            listToUpdate.UpdatedAt = DateTime.Now;
            listToUpdate.CompletedAt = DateTime.Now;

            _repository.Update(listToUpdate);
            return Ok(listToUpdate); //Http 200
        }



        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllLists()
        {
            return Ok(await _repository.GetAll());
        }

    }
}