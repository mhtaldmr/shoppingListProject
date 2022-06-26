using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Extensions;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Server.Controllers
{
    [Route("/lists")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IListRepository _repository;

        public ListController(IListRepository repository) => _repository = repository;

        [HttpPost("create")]
        public  IActionResult CreateList([FromBody] ListCreateViewModel list)
        {
            var newList = new List
            {
                Description = list.Description,
                Title = list.Title,
                UserId = HttpContext.GetUserId()
            };

            _repository.Create(newList);
            return Ok(newList);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllLists()
        {
            return Ok(await _repository.GetAll());
        }

    }
}